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

        // POST: api/Record
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody] Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repo.CreateRecord(record);
            // Assuming record.Id is populated by the database upon insertion
            return CreatedAtAction(nameof(GetRecordById), new { id = record.Id }, record);
        }

    }
}
