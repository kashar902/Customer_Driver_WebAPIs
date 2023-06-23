using System.ComponentModel.DataAnnotations;

namespace BL.Models;

public class AcceptBookingsModel
{
    public int Id { get; set; }
    public string? BookingId { get; set; }
    public int? DriverId { get; set; }
    public string? BookingDate { get; set; }
}


public record AcceptBookingDTO(
       [Required] string BookingId, [Required] int DriverId
    );
