using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;
using System.Text.Json;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        public EventsController(RaceDayDbContext context) { _context = context; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var events = await _context.Events.ToListAsync();
            Console.WriteLine($"[API] GET /api/Events returned {events.Count} events");
            return Ok(events);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] JsonElement body)
        {
            Console.WriteLine($"[API] POST /api/Events body: {body}");
            try
            {
                var title = "Soweto 10K Run";
                if(body.TryGetProperty("title", out var t) && t.ValueKind == JsonValueKind.String) title = t.GetString() ?? title;
                else if(body.TryGetProperty("Title", out t) && t.ValueKind == JsonValueKind.String) title = t.GetString() ?? title;

                var location = "Orange Farm Stadium";
                if(body.TryGetProperty("location", out var l) && l.ValueKind == JsonValueKind.String) location = l.GetString() ?? location;
                else if(body.TryGetProperty("Location", out l) && l.ValueKind == JsonValueKind.String) location = l.GetString() ?? location;

                var description = body.ToString();
                if(body.TryGetProperty("description", out var d) && d.ValueKind == JsonValueKind.String) description = d.GetString() ?? description;
                else if(body.TryGetProperty("Description", out d) && d.ValueKind == JsonValueKind.String) description = d.GetString() ?? description;

                var bannerUrl = "";
                if(body.TryGetProperty("bannerUrl", out var b) && b.ValueKind == JsonValueKind.String) bannerUrl = b.GetString() ?? "";
                else if(body.TryGetProperty("BannerUrl", out b) && b.ValueKind == JsonValueKind.String) bannerUrl = b.GetString() ?? "";

                var ev = new Event
                {
                    Title = title,
                    Location = location,
                    Description = description,
                    BannerUrl = bannerUrl,
                    Status = "Upcoming",
                    EventDate = DateTime.UtcNow.AddDays(14),
                    CreatedAt = DateTime.UtcNow
                };

                _context.Events.Add(ev);
                await _context.SaveChangesAsync();
                Console.WriteLine($"[API] INSERTED Event Id={ev.EventId} Title={ev.Title} - DB now has event!");
                return Ok(ev);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] ERROR: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
