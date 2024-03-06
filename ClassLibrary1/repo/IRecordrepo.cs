using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.repo
{
    public interface IRecordrepo
    {
        Task<IEnumerable<Record>> GetAllRecords();
        Task<Record> GetRecordById(int id);
        Task CreateRecord(Record record);
        Task<IEnumerable<Record>> GetRecordsByEmpId(int empId);
    }
}
