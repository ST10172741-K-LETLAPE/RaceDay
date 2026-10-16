using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        public EventsController(RaceDayDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _context.Events.Include(e => e.Categories).Include(e => e.EventRoute).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ev = await _context.Events.Include(e => e.Categories).Include(e => e.EventRoute).Include(e => e.Weather)
               .FirstOrDefaultAsync(e => e.EventId == id);
            return ev == null? NotFound() : Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return Ok(ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Event updated)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            ev.Title = updated.Title;
            ev.Description = updated.Description;
            ev.EventDate = updated.EventDate;
            ev.Location = updated.Location;
            await _context.SaveChangesAsync();
            return Ok(ev);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
