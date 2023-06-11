namespace BL.Models.DTOs;

public class BookRideDTO
{
    public int? CustomerId { get; set; }
    public decimal? PickUpLatitude { get; set; }
    public decimal? PickUpLongitude { get; set; }
    public decimal? DestinationLatitude { get; set; }
    public decimal? DestinationLongitude { get; set; }
    public string? ObjectDataType { get; set; }
    public string? Comments { get; set; }
    public string? Date { get; set; }
    public string? Time { get; set; }
    public int Price { get; set; }
}