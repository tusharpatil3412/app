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
        Task<bool> CreateRecord(EmpCheckIn record);
        Task<IEnumerable<Record>> GetToday();
        Task<IEnumerable<Record>> GetRecordsByEmpId(int empId);
        Task UpdateCheckoutTime(int empId);
        Task<IEnumerable<Record>> GetTodayRecordsByEmpId(int empId);
        //Task<List<Record>> GetRecordsByCheckinDateRange(dateracord  record);
        Task<List<Record>> GetRecordsByCheckinDateRange(int empid, string startdt, string enddt);
    }
}
