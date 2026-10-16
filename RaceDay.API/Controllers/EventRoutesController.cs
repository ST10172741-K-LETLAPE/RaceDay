using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventRoutesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        public EventRoutesController(RaceDayDbContext context) => _context = context;

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var route = await _context.EventRoutes.FirstOrDefaultAsync(r => r.EventId == eventId);
            return route == null? NotFound() : Ok(route);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventRoute route)
        {
            _context.EventRoutes.Add(route);
            await _context.SaveChangesAsync();
            return Ok(route);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EventRoute updated)
        {
            var route = await _context.EventRoutes.FindAsync(id);
            if (route == null) return NotFound();
            route.GpxData = updated.GpxData;
            route.DistanceKm = updated.DistanceKm;
            route.ElevationGainM = updated.ElevationGainM;
            await _context.SaveChangesAsync();
            return Ok(route);
        }
    }
}
