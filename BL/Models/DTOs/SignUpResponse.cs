using Microsoft.Extensions.Primitives;

namespace BL.Models.DTOs;

public class SignUp_CustomerResponse
{
    public int userID { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? ProfileImage { get; set; }
    public string? Message { get; set; }

}