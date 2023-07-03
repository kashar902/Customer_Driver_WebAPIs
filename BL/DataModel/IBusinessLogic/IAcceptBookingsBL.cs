using BL.Models;

namespace BL.DataModel.IBusinessLogic;

public interface IAcceptBookingsBL
{
    Task<string> AcceptBooking(AcceptBookingDTO model);
    Task<string> DeclineBooking(AcceptBookingDTO accept);
}