﻿using ClassLibrary.Models;
using ClassLibrary.repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
            private readonly IEmployeerepo _repo; // Inject the IEmployeeRepo

            public EmployeController(IEmployeerepo repo)
            {
                _repo = repo;
            }

            // GET: api/Employee
            [HttpGet]
            public async Task<IActionResult> GetAllEmployees()
            {
                var employees = await _repo.GetAllEmployees();
                return Ok(employees);
            }

            // GET api/Employee/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetEmployeeById(int id)
            {
                var employee = await _repo.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }

            // POST api/Employee
            [HttpPost]
            public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var success = await _repo.AddEmployee(employee);
                if (!success)
                {
                    return BadRequest(false);
                }
                return Ok(true);
            }

            // PUT api/Employee/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Employee data is invalid");
                }
            if (employee == null)
                return null;
            employee.Id = id;

            var success = await _repo.UpdateEmployee(employee);
                if (!success)
                {
                    return BadRequest("Could not update the employee");
                }
                return Ok("Employee updated successfully");
            }

            // DELETE api/Employee/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEmployee(int id)
            {
                var success = await _repo.DeleteEmployee(id);
                if (!success)
                {
                    return BadRequest("Could not delete the employee");
                }
                return Ok("Employee deleted successfully");
            }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] employeeLogin login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest("Login request must contain both username and password.");
            }

            var employee = await _repo.LoginEmp(login.Username,login.Password);

            if (employee == null)
            {
                return Unauthorized("No such user exists.");
            }

            // Here, use your method to check the password. This example directly compares the password,
            // which you should replace with a proper hash verification method.
          
            return Ok(employee);
        }
    }
}
