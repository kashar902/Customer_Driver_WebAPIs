using BL.DataModel.IBusinessLogic;
using BL.Models;
using DAL.IServices;
using Newtonsoft.Json;

namespace BL.DataModel.BusinessLogic;

public class DriverBL : IDriverBL
{
    private readonly IGenericCrudService _service;

    public DriverBL(IGenericCrudService service)
    {
        _service = service;
    }


    public async Task<bool> CheckDriverOnEmail(string Email)
    {
        var response = await _service.
            LoadData<DriverModel, dynamic>(
            "GetRecordByEmail", new { Email });
        if (response is null || !response.Any())
        {
            return false;
        }
        return true;
    }
    
    public async Task<DriverModel> GetDriverOnEmail(string Email)
    {
        var response = await _service.
            LoadData<DriverModel, dynamic>(
            "GetRecordByEmail", new { Email });
        return response.FirstOrDefault()!;
    }

    public async Task SignUpDriver(DriverModel model) => 
        await _service.SaveData("SaveDataUsingJsonDriver", new { JsonData = JsonConvert.SerializeObject(model) });

    
}