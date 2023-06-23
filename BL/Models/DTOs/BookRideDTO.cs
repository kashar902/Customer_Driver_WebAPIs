using Microsoft.AspNetCore.Http;

namespace BL.Models.DTOs;

public class BookRideDTO
{
    public int? CustomerId { get; set; }
    public string? PickUpLocation { get; set; }
    public string? DestinationLocation { get; set; }
    public decimal? PickUpLatitude { get; set; }
    public decimal? PickUpLongitude { get; set; }
    public decimal? DestinationLatitude { get; set; }
    public decimal? DestinationLongitude { get; set; }
    public string? ObjectDataType { get; set; }
    public string? Comments { get; set; }
    public string? Date { get; set; }
    public string? Time { get; set; }
    public int Price { get; set; }
    public List<IFormFile>? Attachments { get; set; }
}