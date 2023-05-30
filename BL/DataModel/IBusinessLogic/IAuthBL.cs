using BL.Models;

namespace BL.DataModel.IBusinessLogic;

public interface IAuthBL
{
    Task<Login?> GetDataforAuth(string email, string password);
}