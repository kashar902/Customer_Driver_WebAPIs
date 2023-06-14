using BL.Models;

namespace BL.DataModel.IBusinessLogic;

public interface IDriverBL
{
    Task<bool> CheckDriverOnEmail(string Email);
    Task<DriverModel> GetDriverOnEmail(string Email);
    Task SignUpDriver(DriverModel model);
}