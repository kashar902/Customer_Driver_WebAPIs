using BL.DataModel.IBusinessLogic;
using BL.Models;
using BL.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FYP_Project.Controllers;

[Route("api/user/c/")]
[ApiController]
public class CustomerActionsController : ControllerBase
{
    private readonly IRideBookingByCustomer _rideBookingData;

    public CustomerActionsController(IRideBookingByCustomer data)
    {
        _rideBookingData = data;
    }

    [HttpPost("booking")]
    [AllowAnonymous]
    public async Task<IActionResult> postBooking(BookRideDTO request)
    {
        RideBookingModel model = new()
        {
            id = Guid.NewGuid().ToString(),
            CustomerId = request.CustomerId,
            pLatitude = request.PickUpLatitude,
            pLongitude = request.PickUpLongitude,
            dLatitude = request.DestinationLatitude,
            dLongitude = request.DestinationLongitude,
            objectDataType = request.ObjectDataType,
            Comments = request.Comments,
            DateVal = request.Date,
            TimeVal = request.Time,
            Price = request.Price
        };

        await _rideBookingData.BookRideByCustomemr(model);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("Booking/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _rideBookingData.GetById(id);
        if (response?.CustomerId != 1)
        {
            return Content($"Booking {id} not Found!");
        }
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("Booking")]
    public async Task<IActionResult> GetById()
    {
        var response = await _rideBookingData.GetAll();
        if (response?.Count() == 0)
        {
            return Content($"No Booking Available!");
        }
        return Ok(response);
    }
}