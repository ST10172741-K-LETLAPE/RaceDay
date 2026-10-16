using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/events/{eventId}/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        public WeatherController(RaceDayDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetWeather(int eventId)
        {
            var weather = await _context.Weathers
               .Where(w => w.EventId == eventId)
               .OrderByDescending(w => w.RecordedAt)
               .ToListAsync();
            return Ok(weather);
        }

        [HttpPost]
        public async Task<IActionResult> AddWeather(int eventId, Weather weather)
        {
            weather.EventId = eventId;
            weather.RecordedAt = System.DateTime.Now;
            _context.Weathers.Add(weather);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWeather), new { eventId }, weather);
        }
    }
}
