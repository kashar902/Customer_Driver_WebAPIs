using BL.DataModel.IBusinessLogic;
using BL.Models;
using BL.Models.DTOs;
using DataAccessLayer.IServices;

namespace BL.DataModel.BusinessLogic;

public class AuthBl : IAuthBL
{
    private readonly IGenericCrudService? _service;
    private static string SpForLoginWithEmailAndPassword { get; } = "Sp_ForLoginWithEmailAndPassword";
    private static string SpInsertSignUpCustomer { get; } = "Sp_InsertSignUpCustomer";
    private static string SpGetDataOnEmail { get; } = "[dbo].[Sp_GetDataOnEmail]";
    private static string SpEmailVerification { get; } = "[dbo].[Sp_EmailVerification]";
    private static string GetCustomerById { get; } = "[dbo].[GetCustomerById]";


    public AuthBl(IGenericCrudService service)
    {
        _service = service;
    }


    public async Task<Login?> GetDataForAuth(string email, string password)
    {
        var result = await _service!.LoadData<Login, dynamic>(
            SpForLoginWithEmailAndPassword,
            new { email, password });

        return result.FirstOrDefault();
    }

    public async Task<SignUp_CustomerModel?> GetByIdCustomer(int id)
    {
        var result = await _service!.LoadData<SignUp_CustomerModel, dynamic>(
            GetCustomerById,
            new { Id = id });

        return result.FirstOrDefault();
    }

    public async Task SignUpCustomer(SignUp_CustomerModel model)
    {
        await _service!.SaveData(SpInsertSignUpCustomer,
           new { model.Username, model.Email, model.Password, model.ProfileImage });
    }

    public async Task<SignUp_CustomerResponse?> GetDataOnEmail(string email)
    {
        var result = await _service!.LoadData<SignUp_CustomerResponse, dynamic>(
            SpGetDataOnEmail,
            new { email });

        return result.FirstOrDefault();
    }
    public async Task<SignUp_CustomerResponse?> UpdateEmailStatus(string email)
    {
        var result = await _service!.LoadData<SignUp_CustomerResponse, dynamic>(
            SpEmailVerification,
            new { email });

        return result.FirstOrDefault();
    }


}