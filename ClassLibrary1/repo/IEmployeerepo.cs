using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.repo
{
    public interface IEmployeerepo
    {

        Task<bool> AddEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<bool> UpdateEmployee(Employee employee);
        Task<Employee> LoginEmp(string username, string password);
    }
}
