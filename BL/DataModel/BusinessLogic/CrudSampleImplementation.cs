using BL.DataModel.IBusinessLogic;
using BL.Models;
using DAL.GenericRepositoryData;

namespace BL.DataModel.BusinessLogic;

public class CrudSampleImplementation : 
    IDataRepository<SignUp_Customer> 
{
    public Task<IEnumerable<SignUp_Customer>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<SignUp_Customer> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveData(List<SignUp_Customer> models)
    {
        throw new NotImplementedException();
    }

    public Task UpdateData(SignUp_Customer model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteData(int id)
    {
        throw new NotImplementedException();
    }
}