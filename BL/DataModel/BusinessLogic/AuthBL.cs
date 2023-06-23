using BL.DataModel.IBusinessLogic;
using BL.Models;
using BL.Models.DTOs;
using DataAccessLayer.IServices;

namespace BL.DataModel.BusinessLogic;

public class AuthBL : IAuthBL
{
    private readonly IGenericCrudService? _service;
    private static string Sp_ForLoginWithEmailAndPassword { get; } = "Sp_ForLoginWithEmailAndPassword";
    private static string Sp_InsertSignUpCustomer { get; } = "Sp_InsertSignUpCustomer";
    private static string Sp_GetDataOnEmail { get; } = "[dbo].[Sp_GetDataOnEmail]";
    private static string Sp_EmailVerification { get; } = "[dbo].[Sp_EmailVerification]";
    private static string GetCustomerById { get; } = "[dbo].[GetCustomerById]";


    public AuthBL(IGenericCrudService service)
    {
        _service = service;
    }


    public async Task<Login?> GetDataforAuth(string email, string password)
    {
        var result = await _service!.LoadData<Login, dynamic>(
            Sp_ForLoginWithEmailAndPassword,
            new { email, password });

        return result.FirstOrDefault();
    }

    public async Task<SignUp_CustomerModel?> GetByIdCustomer(int Id)
    {
        var result = await _service!.LoadData<SignUp_CustomerModel, dynamic>(
            GetCustomerById,
            new { Id });

        return result.FirstOrDefault();
    }

    public async Task SignUpCustomer(SignUp_CustomerModel model)
    {
        await _service!.SaveData(Sp_InsertSignUpCustomer,
           new { model.Username, model.Email, model.Password, model.ProfileImage });
    }

    public async Task<SignUp_CustomerResponse?> GetDataOnEmail(string email)
    {
        var result = await _service!.LoadData<SignUp_CustomerResponse, dynamic>(
            Sp_GetDataOnEmail,
            new { email });

        return result.FirstOrDefault();
    }
    public async Task<SignUp_CustomerResponse?> UpdateEmailStatus(string email)
    {
        var result = await _service!.LoadData<SignUp_CustomerResponse, dynamic>(
            Sp_EmailVerification,
            new { email });

        return result.FirstOrDefault();
    }


}