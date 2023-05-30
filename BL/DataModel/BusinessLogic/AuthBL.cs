using BL.DataModel.IBusinessLogic;
using BL.Models;
using DAL.IServices;

namespace BL.DataModel.BusinessLogic;

public class AuthBL : IAuthBL
{
    private readonly IGenericCrudService? _service;
    private static string Sp_ForLoginWithEmailAndPassword { get; } = "";

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

}