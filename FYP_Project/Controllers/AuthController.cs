using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BL.DataModel.IBusinessLogic;
using BL.Models;
using DAL.GenericRepositoryData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FYP_Project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthBL _data;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthBL data, IConfiguration configuration)
    {
        _data = data;
        _configuration = configuration;
    }

    
    
    [AllowAnonymous]
    [HttpGet("user/login")]
    public async Task<IActionResult> Login(string Email, string Password)
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
    
    private string GenerateJwt(string userId, string email, string role)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
}