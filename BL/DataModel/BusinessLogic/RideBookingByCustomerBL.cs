

using BL.DataModel.IBusinessLogic;
using BL.Models;
using DAL.IServices;
using Newtonsoft.Json;

namespace BL.DataModel.BusinessLogic;

public class RideBookingByCustomerBl : IRideBookingByCustomer
{
    private readonly IGenericCrudService _service;
    private static string SpInsertSignUpCustomer { get; } = "[SaveCustomerRideBooking]";
    private static string SpGetAll { get; } = "[GetAllCustomerRideBookings]";
    private static string SpGetbyid { get; } = "[dbo].[GetCustomerRideBookingById]";


    public RideBookingByCustomerBl(IGenericCrudService service)
    {
        _service = service;
    }
    
  
    public async Task BookRideByCustomemr(RideBookingModel model)
    {
         await _service!.SaveData(SpInsertSignUpCustomer,
            new { BookingData  = JsonConvert.SerializeObject(model) });
    }

    public async Task<IEnumerable<RideBookingModel>> GetAll() =>
        await _service.LoadData<RideBookingModel, dynamic>(SpGetAll, new { });

    public async Task<RideBookingModel?> GetById(string id)
    {
        IEnumerable<RideBookingModel?> response = 
            await _service.LoadData<RideBookingModel, dynamic>(SpGetbyid, new { Id = id });
        return response.FirstOrDefault();

    }
}
