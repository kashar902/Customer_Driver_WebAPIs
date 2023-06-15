using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.IServices;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Services;

public class GenericCrudService : IGenericCrudService
{
private readonly IConfiguration _config;

    public GenericCrudService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string sp,
        U parameters)
    {
        using IDbConnection con = new SqlConnection(_config.GetConnectionString("Con_Str"));
        if (con.State != ConnectionState.Open) con.Open();

        return await con.QueryAsync<T>(
            sp, parameters, commandType: CommandType.StoredProcedure);
    }


    
    public async Task SaveData<T>(
    string sp,
    T parameters)
    {
        using IDbConnection con = new SqlConnection(_config.GetConnectionString("Con_Str"));
        if (con.State != ConnectionState.Open) con.Open();

        await con.ExecuteAsync(
            sp, parameters, commandType: CommandType.StoredProcedure);
    }
}
