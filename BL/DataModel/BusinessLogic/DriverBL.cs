using BL.DataModel.IBusinessLogic;
using BL.Models;
using BL.Models.DTOs;
using DataAccessLayer.IServices;
using Newtonsoft.Json;

namespace BL.DataModel.BusinessLogic;

public class DriverBL : IDriverBL
{
    private readonly IGenericCrudService _service;

    public DriverBL(IGenericCrudService service)
    {
        _service = service;
    }


    public async Task<bool> CheckDriverOnEmail(string email)
    {
        var response = await _service.
            LoadData<DriverModel, dynamic>(
            "GetRecordByEmail", new { Email = email });
        if (!response.Any())
        {
            return false;
        }
        return true;
    }
    
    public async Task<DriverModel> GetDriverOnEmail(string email)
    {
        var response = await _service.
            LoadData<DriverModel, dynamic>(
            "GetRecordByEmail", new { Email = email });
        return response.FirstOrDefault()!;
    }
    
    public async Task<IEnumerable<BookingInfoDto>> GetBookingInfo(string email)
    {
        return await _service.
            LoadData<BookingInfoDto, dynamic>(
            "[dbo].[Sp_GetBookingsDoneByDriver]", new { email });
        
    }
    public async Task<DriverModel> GetDriverForLogin(string email, string password)
    {
        var response = await _service.
            LoadData<DriverModel, dynamic>(
            "[dbo].[Sp_DriverLogin]", new { email, password });
        return response.FirstOrDefault()!;
    }
    
    public async Task UpdateEmailStatusOnDriver(string email) =>
        await _service.SaveData("[dbo].[Sp_UpdateDriverEmailStatus]", new { email });

    public async Task SignUpDriver(DriverModel model) => 
        await _service.SaveData("SaveDataUsingJsonDriver", new { JsonData = JsonConvert.SerializeObject(model) });

    
}