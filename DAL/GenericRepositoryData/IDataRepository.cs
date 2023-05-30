namespace DAL.GenericRepositoryData;

public interface IDataRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    
    Task<T> GetById(int id);
    
    Task SaveData(List<T> models);
    
    Task UpdateData(T model);
    
    Task DeleteData(int id);
    
}