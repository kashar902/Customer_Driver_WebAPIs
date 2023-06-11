namespace BL.Models;

public class RideBookingModel
{
    public string id { get; set; }
    public int? CustomerId { get; set; }
    public decimal? pLatitude { get; set; }
    public decimal? pLongitude { get; set; }
    public decimal? dLatitude { get; set; }
    public decimal? dLongitude { get; set; }
    public string? objectDataType { get; set; }
    public string? Comments { get; set; }
    public string? DateVal { get; set; }
    public string? TimeVal { get; set; }
    public int Price { get; set; }
    public int? Status { get; set; }
    public string? CreatedDate { get; set; }
}