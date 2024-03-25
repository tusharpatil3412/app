using ClassLibrary.Models;
using ClassLibrary.repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordrepo _repo;

        public RecordController(IRecordrepo repo)
        {
            _repo = repo;
        }

        // GET: api/Record
        [HttpGet]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _repo.GetAllRecords();
            return Ok(records);
        }

        // GET: api/Record/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            var record = await _repo.GetRecordById(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // GET: api/Record/ByEmpId/1
        [HttpGet("ByEmpId/{empId}")]
        public async Task<IActionResult> GetRecordsByEmpId(int empId)
        {
            var records = await _repo.GetRecordsByEmpId(empId);
            if (records == null) return NotFound($"No records found for emp_id: {empId}");
            return Ok(records);
        }
        [HttpGet("Today")]
        public async Task<IActionResult> GetRecordsToday()
        {
            var records = await _repo.GetToday();
            if (records == null) return NotFound($"No records found ");
            return Ok(records);
        }

        // POST: api/Record
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody] EmpCheckIn record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var records = await _repo.GetTodayRecordsByEmpId(record.Emp_Id);
            if (records == null || !records.Any())
            {
                var result = await _repo.CreateRecord(record);

                // Assuming record.Id is populated by the database upon insertion
                return Ok(result);
            }
            else { return BadRequest(ModelState); }
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> UpdateCheckout(int EmpId)
        {
            var records = await _repo.GetTodayRecordsByEmpId(EmpId);
            if (records == null || !records.Any())
                return Ok(null);
            try
            {
                await _repo.UpdateCheckoutTime(EmpId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return Ok(null);
            }
        }
        [HttpGet("TodayByEmpId/{empId}")]
        public async Task<IActionResult> GetTodayRecordsByEmpId(int empId)
        {
            var records = await _repo.GetTodayRecordsByEmpId(empId);
            if (records == null || !records.Any())
                return Ok(null);

            return Ok(records);
        }
        
        [HttpGet("getRecordsByRange")]
        public async Task<IActionResult> GetRecordsByRange(int empid, string startdt, string enddt)
        {
            try
            {
                var results = await _repo.GetRecordsByCheckinDateRange(empid,startdt,enddt);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
