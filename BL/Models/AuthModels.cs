using BL.Validations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BL.Models;

public class SignUp_Customer
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    [MaxFileResolution(5)]
    [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
    public IFormFile? ProfileImage { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
    [Compare("Password")]
    public string? ConfimPassword { get; set; }
}


public class SignUp_CustomerModel
{
    public string? Username { get; set; }
    
    public string? ProfileImage { get; set; }
    
    public string? Email { get; set; }
    
    public string? Password { get; set; }
}
public class Login
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
    public string? RoleName { get; set; } = "Student";
}
public class LoginResponseDriver
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
    public string? RoleName { get; set; } = "Driver";
}