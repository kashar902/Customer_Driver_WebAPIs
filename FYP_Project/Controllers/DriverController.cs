using System.Runtime.CompilerServices;
using BL.DataModel.IBusinessLogic;
using BL.Models;
using BL.Models.DTOs;
using DAL.GlobalModels;
using DAL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FYP_Project.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class DriverController: ControllerBase
{
    private readonly IDriverBL _logic;    
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;
    private readonly IMailService _mailService;
    private readonly IOtpService _otpService;

    public DriverController(IDriverBL logic, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IMailService mailService, IOtpService otpService)
    {
        _logic = logic;
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
        _mailService = mailService;
        _otpService = otpService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] CreateDriverSignup request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is Not Valid");
        }
        bool checkRegisteredEmail = await _logic.
            CheckDriverOnEmail(request?.Email!);
        if (checkRegisteredEmail)
        {
            return Content("Email Already Registered!");
        }

        DriverModel model = new()
        {
            ID = Guid.NewGuid().ToString(), 
            Email = request.Email, 
            password = request.Password, 
            Category = request.Category, 
            Cities = request.Cities, 
            Country = request.Country,
            Type = request.Type, 
            CnicFront = UploadImageFunction(request.CnicFront!), 
            CnicBack = UploadImageFunction(request.CnicBack!) ,
            CnicNumber = request.CnicNumber,
            Username = request.Username,
            IsActive = true,
            CreatedDate = DateTime.UtcNow.ToString(),
            NumberPlate = request.NumberPlate,
            LicenseNumber = request.LicenseNumber,
            ProfileImage = UploadImageFunction(request.ProfileImage!),
            LiFront =UploadImageFunction(request.LicenseFront!) ,
            LiBack = UploadImageFunction(request.LicenseBack!)
        };

        await _logic.SignUpDriver(model);
        var response = await _logic.GetDriverOnEmail(model.Email!);
        
        MailClassModel mailClassModel = new()
        {
            ToMailIds =  new() { response.Email! },
            Body = GetBodySendOtp(_otpService.GenerateOTP(), request.Email!),
            Subject = "OTP 'ONE TIME PASSWORD!'",
            IsHtmlBody = false,
        };


        string mailResponse = await _mailService.Sendmail(mailClassModel, _configuration["Credentials:EmailToSendMail"]!,
            _configuration["Credentials:Password"]!);

        if (mailResponse.Equals("Mail Sent Succefully!"))
            return Ok(response);

        return BadRequest("Mail Not Sent Successfully!");
    }

    private string GetBodySendOtp(int generateOtp, string requestEmail)
    {
        return $"your Email: {requestEmail} Otp is {generateOtp}";
    }


    private string UploadImageFunction(IFormFile ImageFile)
    {
        if (ImageFile == null) throw new ArgumentNullException(nameof(ImageFile));

        string webRootPath = _webHostEnvironment.WebRootPath;
        string uploadsPath = Path.Combine(webRootPath, "Images");
        Directory.CreateDirectory(uploadsPath);

        string uniqueFileName = "Fyp_Customer_" + Guid.NewGuid().ToString("N") + Path.GetExtension(ImageFile.FileName);
        string filePath = Path.Combine(uploadsPath, uniqueFileName);

        string saveData = _configuration["DomainName"] + "/Images/" + uniqueFileName;

        using var stream = new FileStream(filePath, FileMode.Create);
        ImageFile.CopyTo(stream);
        return saveData;
    }
    
}