using BL.Models;
using BL.Models.DTOs;

namespace BL.DataModel.IBusinessLogic;

public interface IDriverBL
{
    Task<bool> CheckDriverOnEmail(string email);
    Task<DriverModel> GetDriverOnEmail(string email);
    Task SignUpDriver(DriverModel model);
    Task<DriverModel> GetDriverForLogin(string email, string password);
    Task UpdateEmailStatusOnDriver(string email);
    Task<IEnumerable<BookingInfoDto>> GetBookingInfo(string email);
}