using BL.DataModel.IBusinessLogic;
using BL.Models;
using BL.Models.DTOs;
using DAL.GlobalModels;
using DAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FYP_Project.Controllers;

[Route("api/[controller]")]
[ApiController]
//AUTH COntroller
public class AuthController : ControllerBase
{
    private readonly IAuthBL _data;
    private readonly IConfiguration _configuration;
    private readonly IMailService _mailService;
    private readonly IOtpService _otpService;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public AuthController(IAuthBL data, IConfiguration configuration, IMailService mailService, IOtpService otpService, IWebHostEnvironment webHostEnvironment)
    {
        _data = data;
        _configuration = configuration;
        _mailService = mailService;
        _otpService = otpService;
        _webHostEnvironment = webHostEnvironment;
    }

    [AllowAnonymous]
    [HttpPost("u/r/Signup")]
    public async Task<IActionResult> Register([FromForm] SignUp_Customer model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model State is not Valid!");

        if (model.ProfileImage == null || model.ProfileImage.Length == 0)
            return BadRequest(new { message = "Image file is required." });

        string picUrl = UploadImageFunction(model.ProfileImage);

        SignUp_CustomerModel signUpCustomerModel = new()
        {
            ProfileImage = picUrl,
            Username = model.Username,
            Email = model.Email,
            Password = model.Password
        };

        await _data.SignUpCustomer(signUpCustomerModel);


        SignUp_CustomerResponse response = await _data.GetDataOnEmail(model.Email);
        List<string> maillist = new() { response.Email };

        MailClassModel mailClassModel = new()
        {
            ToMailIds = maillist,
            Body = GetBodySendOtp(_otpService.GenerateOTP()),
            Subject = "OTP 'ONE TIME PASSWORD!'",
            IsHtmlBody = false,
        };

        string mailResponse = await _mailService.Sendmail(mailClassModel, _configuration["Credentials:EmailToSendMail"]!,
            _configuration["Credentials:Password"]!);


        if (mailResponse.Equals("Mail Sent Succefully!"))
            return Ok("SignUp Successful, Verify OTP!");

        return Content("Issue Occur in Signing you up, Try Again!");
    }



    [AllowAnonymous]
    [HttpGet("user/login")]
    public async Task<IActionResult> Login([Required] string Email, [Required] string Password)
    {
        if (string.IsNullOrWhiteSpace(Email)) return BadRequest("Email Required!");
        if (string.IsNullOrWhiteSpace(Password)) return BadRequest("Password Required!");

        var result = await _data.GetDataforAuth(Email, Password);

        if (result is null)
            return Unauthorized("Invalid credentials.");

        var jwt = GenerateJwt(result.UserId.ToString(), Email, result!.RoleName!);

        Login loginresponse = new()
        {
            UserId = result.UserId,
            Email = Email,
            Password = Password,
            Token = jwt,
            RoleName = result.RoleName
        };

        return Ok(loginresponse);
    }

    [HttpGet("otpverification")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyOtp(int otp, string email)
    {
        if (otp == 0)
            return NotFound("otp can not be null");

        var data = await _data.GetDataOnEmail(email);

        if (data == null)
            return Content("Email Not Found");

        bool result = _otpService.ValidateOTP(otp);
        if (result)
            await _data.UpdateEmailStatus(email);
        return Ok("Verified!");

    }

    private string GenerateJwt(string userId, string email, string role)
    {
        SymmetricSecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
        var credentials = 
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetValue<string>("Jwt:Subject")),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Sid, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(_configuration.GetValue<string>("Jwt:Issuer"),
            _configuration.GetValue<string>("Jwt:Audiance"),
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        string data = new JwtSecurityTokenHandler().WriteToken(token);
        return data;
    }



    private string UploadImageFunction(IFormFile ImageFile)
    {

        string webRootPath = _webHostEnvironment.WebRootPath;
        string uploadsPath = Path.Combine(webRootPath, "Images");
        Directory.CreateDirectory(uploadsPath);

        string uniqueFileName = "Fyp_Customer_" + Guid.NewGuid().ToString("N") + Path.GetExtension(ImageFile.FileName);
        string filePath = Path.Combine(uploadsPath, uniqueFileName);

        string saveData = _configuration["DomainName"] + "/Images/" + uniqueFileName;

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            ImageFile.CopyTo(stream);
        }
        return saveData;
    }
    private string GetBodySendOtp(int generateOtp)
    {
        return $"your Otp is {generateOtp}";
    }
}