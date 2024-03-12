using ClassLibrary.data;
using ClassLibrary.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.repo.Recordrepo;

namespace ClassLibrary.repo
{
    public  class Recordrepo:IRecordrepo
        {
            private readonly IDataAcc _db;

            public Recordrepo(IDataAcc db)
            {
                _db = db;
            }

            public async Task<IEnumerable<Record>> GetAllRecords()
            {
                string query = "SELECT * FROM record";
                return await _db.GetData<Record, dynamic>(query, null);
            }

            public async Task<Record> GetRecordById(int id)
            {
                string query = "SELECT * FROM record WHERE id = @Id";
                var result = await _db.GetData<Record, dynamic>(query, new { Id = id });
                return result.FirstOrDefault();
            }

            public async Task<IEnumerable<Record>> GetRecordsByEmpId(int empId)
            {
                string query = "SELECT * FROM record WHERE emp_id = @Emp_Id";
                return await _db.GetData<Record, dynamic>(query, new { Emp_Id = empId });
            }

            public async Task<bool> CreateRecord(Record record)
            {
              
                string query = "INSERT INTO record ( emp_id) VALUES ( @Emp_Id)";
                await _db.SaveData(query, record);
                return true;
           
            }
        public async Task UpdateCheckoutTime(int id)
        {
            string query = "UPDATE record SET checkout = GETDATE() WHERE emp_id = @Id;";
            await _db.SaveData(query, new {  Id= id });
        }
        public async Task<IEnumerable<Record>> GetTodayRecordsByEmpId(int empId)
        {
            var query = @"
                SELECT * 
                FROM record
                WHERE emp_id = @EmpId 
                AND CAST(checkin AS DATE) = CAST(GETDATE() AS DATE);";

            var parameters = new { EmpId = empId };
            var result= await _db.GetData<Record, dynamic>(query, parameters);
            return result;
        }
    }

}

