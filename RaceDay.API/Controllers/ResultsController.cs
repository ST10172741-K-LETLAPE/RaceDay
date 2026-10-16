using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        public ResultsController(RaceDayDbContext context) => _context = context;

        [HttpGet("api/enrolments/{id}/result")]
        public async Task<IActionResult> GetByEnrolment(int id)
        {
            var result = await _context.Results.FirstOrDefaultAsync(r => r.EnrolmentId == id);
            return result == null? NotFound() : Ok(result);
        }

        [HttpPost("api/enrolments/{id}/result")]
        public async Task<IActionResult> CreateResult(int id, Result result)
        {
            if (await _context.Results.AnyAsync(r => r.EnrolmentId == id))
                return Conflict("Result already exists for this enrolment");
            result.EnrolmentId = id;
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByEnrolment), new { id }, result);
        }

        [HttpPut("api/results/{id}")]
        public async Task<IActionResult> UpdateResult(int id, Result updated)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null) return NotFound();
            result.FinishTime = updated.FinishTime;
            result.Position = updated.Position;
            result.Status = updated.Status;
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        [HttpGet("api/results/my")]
        public async Task<IActionResult> GetMyResults([FromQuery] int userId)
        {
            var myResults = await _context.Results
               .Include(r => r.Enrolment)
               .Where(r => r.Enrolment.UserId == userId)
               .ToListAsync();
            return Ok(myResults);
        }
    }
}
