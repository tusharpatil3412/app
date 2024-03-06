using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Extensions.Configuration;
using Dapper;
namespace ClassLibrary.data
{
    public class DataAcc : IDataAcc
    {
        private readonly IConfiguration _config;



        public DataAcc(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<T>> GetData<T, P>(string query, P parametetrs, string connectionid = "key")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionid));
            return await connection.QueryAsync<T>(query, parametetrs);
        }
        public async Task SaveData<P>(string query, P parametetrs, string connectionid = "key")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionid));
            await connection.ExecuteAsync(query, parametetrs);
        }

    }
}

