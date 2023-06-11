using BL.Models;

namespace BL.DataModel.IBusinessLogic;

public interface IRideBookingByCustomer
{
    Task BookRideByCustomemr(RideBookingModel model);

    Task<IEnumerable<RideBookingModel>> GetAll();

    Task<RideBookingModel?> GetById(string id);
}