using BL.Models;
using BL.Models.DTOs;

namespace BL.DataModel.IBusinessLogic;

public interface IAuthBL
{
    Task<Login?> GetDataforAuth(string email, string password);

    Task SignUpCustomer(SignUp_CustomerModel model);

    Task<SignUp_CustomerResponse?> GetDataOnEmail(string email);

    Task<SignUp_CustomerResponse?> UpdateEmailStatus(string email);

    Task<SignUp_CustomerModel?> GetByIdCustomer(int Id);

}