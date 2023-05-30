using System.ComponentModel.DataAnnotations;

namespace BL.Models;

public class SignUp_Customer
{
    [Required]
    public string? Username { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    [Compare("Password")]
    public string? ConfimPassword { get; set; }
}

public class Login
{
    
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
    public string? RoleName { get; set; }
}