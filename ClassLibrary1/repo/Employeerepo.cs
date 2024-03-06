using ClassLibrary.data;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.repo
{
    public class Employeerepo : IEmployeerepo
    {

        private readonly IDataAcc _db; // Using the same IDataAcc interface for data access

        public Employeerepo(IDataAcc db)
        {
            _db = db;
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            try
            {
                string query = "INSERT INTO employee (first_name, last_name, username, password) VALUES (@First_Name, @Last_Name, @Username, @Password)";
                await _db.SaveData(query, new { employee.First_Name, employee.Last_Name, employee.Username, employee.Password });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                string query = "DELETE FROM employee WHERE id = @Id";
                await _db.SaveData(query, new { Id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                string query = "SELECT * FROM employee WHERE id = @Id";
                var result = await _db.GetData<Employee, dynamic>(query, new { Id = id });
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                string query = "SELECT * FROM employee";
                var employees = await _db.GetData<Employee, dynamic>(query, new { });
                return employees;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                string query = "UPDATE employee SET first_name = @First_Name, last_name = @Last_Name, username = @Username, password = @Password WHERE id = @Id";
                await _db.SaveData(query, new { employee.Id, employee.First_Name, employee.Last_Name, employee.Username, employee.Password });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

