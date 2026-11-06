using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly Services.ApiClient _api;
        public CategoriesController(Services.ApiClient api) { _api = api; }

        // List categories for an event
        public async Task<IActionResult> Index(int eventId)
        {
            var token = HttpContext.Session.GetString("token");
            ViewBag.EventId = eventId;
            var cats = await _api.GetAsync($"/api/Categories/event/{eventId}", token);
            if(cats == null) cats = await _api.GetAsync($"/api/Categories", token);
            return View(cats);
        }

        [HttpGet]
        public IActionResult Create(int eventId)
        {
            ViewBag.EventId = eventId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int eventId, string name, string ageGroup, string distance, int maxParticipants)
        {
            var token = HttpContext.Session.GetString("token");
            var payload = new { eventId, name, ageGroup, distance, maxParticipants };
            var result = await _api.PostAsync("/api/Categories", payload, token);
            return RedirectToAction("Index", new { eventId });
        }
    }
}
