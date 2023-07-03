using BL.DataModel.IBusinessLogic;
using BL.Models;
using DataAccessLayer.IServices;

namespace BL.DataModel.BusinessLogic;


public class AcceptBookingsBL : IAcceptBookingsBL
{
    private readonly IGenericCrudService _services;

    public AcceptBookingsBL(IGenericCrudService services)
    {
        _services = services;
    }

    public async Task<string> AcceptBooking(AcceptBookingDTO model)
    {

        await _services.SaveData("[dbo].[sp_SaveAcceptBookingByDriver]", new
        {
            model.BookingId,
            model.DriverId
        });
        return "Success";
    }

    public async Task<string> DeclineBooking(AcceptBookingDTO model)
    {
        await _services.SaveData("[dbo].[sp_SaveAcceptBookingByDriver]",
            new
            { model.BookingId, model.DriverId });
        return "Declined!";
    }
}
