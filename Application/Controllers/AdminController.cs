using ClassLibrary.Models;
using ClassLibrary.repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {  
            private readonly IAdminrepo _repo; // Use the IAdminRepo interface

            public AdminController(IAdminrepo adminRepo) // Dependency injection of IAdminRepo
            {
                _repo = adminRepo;
            }

            // GET: api/<AdminController>
            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var admins = await _repo.GetAllAdmins();
                return Ok(admins);
            }

            // GET api/<AdminController>/5
            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var admin = await _repo.GetByIdAdmin(id);
                if (admin == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(admin);
                }
            }

            // POST api/<AdminController>
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] Admin admin) // Assume Admin is your model class
            {
                if (admin == null)
                {
                    return BadRequest("Invalid admin data");
                }

                var success = await _repo.AddAdmin(admin);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not add admin");
                }
            }

            // PUT api/<AdminController>/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] Admin admin)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Admin data is invalid");
                }
                if (admin == null)
                    return null;
                admin.Id = id;
                var success = await _repo.UpdateAdmin(admin);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not update admin");
                }
            }

            // DELETE api/<AdminController>/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _repo.DeleteAdmin(id);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not delete admin");
                }
            }
        // POST api/<AdminController>/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Admin login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest("Login request must contain both username and password.");
            }

            var admin = await _repo.GetByIdusername(login.Username);

            if (admin == null)
            {
                return Unauthorized("No such user exists.");
            }

            // Here, use your method to check the password. This example directly compares the password,
            // which you should replace with a proper hash verification method.
            // bool isValidPassword = VerifyPassword(login.Password, admin.Password);
            bool isValidPassword = login.Password == admin.Password; // Placeholder, replace with actual verification

            if (!isValidPassword)
            {
                return Unauthorized("Incorrect password.");
            }

            // Upon successful verification, return an appropriate response.
            // In a real application, you might issue a JWT token or another form of session token here.
            return Ok(new { message = "Login successful." });
        }
    }
    }

