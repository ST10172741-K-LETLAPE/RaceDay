using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        public EnrolmentsController(RaceDayDbContext context) => _context = context;

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
            => Ok(await _context.Enrolments.Where(e => e.UserId == userId).Include(e => e.Category).ToListAsync());

        [HttpGet("my")]
        public async Task<IActionResult> GetMy([FromQuery] int userId)
            => Ok(await _context.Enrolments.Where(e => e.UserId == userId).Include(e => e.Category).Include(e => e.Category.Event).ToListAsync());

        [HttpGet("/api/events/{eventId}/enrolments")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var enrolments = await _context.Enrolments
               .Include(e => e.Category)
               .Include(e => e.User)
               .Where(e => e.Category.EventId == eventId)
               .ToListAsync();
            return Ok(enrolments);
        }

        [HttpPost]
        public async Task<IActionResult> Enrol(Enrolment enrol)
        {
            if (await _context.Enrolments.AnyAsync(e => e.UserId == enrol.UserId && e.CategoryId == enrol.CategoryId))
                return Conflict("Already enrolled");
            _context.Enrolments.Add(enrol);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByUser), new { userId = enrol.UserId }, enrol);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var enrol = await _context.Enrolments.FindAsync(id);
            if (enrol == null) return NotFound();
            _context.Enrolments.Remove(enrol);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
