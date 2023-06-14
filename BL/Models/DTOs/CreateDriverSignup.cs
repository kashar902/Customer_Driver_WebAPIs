using Microsoft.AspNetCore.Http;

namespace BL.Models.DTOs;

public class CreateDriverSignup
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public string? Country { get; set; }
    public string? Cities { get; set; }
    public string? CreatedDate { get; set; }
    public string? Category { get; set; }
    public string? Type { get; set; }
    public string? NumberPlate { get; set; }
    public string? CnicNumber { get; set; }
    public IFormFile? CnicFront { get; set; }
    public IFormFile? CnicBack { get; set; }
    public string? LicenseNumber { get; set; }
    public IFormFile? LicenseFront { get; set; }
    public IFormFile? LicenseBack { get; set; }

}