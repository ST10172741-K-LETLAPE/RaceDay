using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Services.ApiClient _api;
        public DashboardController(Services.ApiClient api) { _api = api; }

        public async Task<IActionResult> Dashboard()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");
            
            try
            {
                var events = await _api.GetAsync("/api/Events", token);
                return View(events); // can be null, view handles it
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dashboard error: {ex.Message}");
                return View(null);
            }
        }

        public async Task<IActionResult> Enrolments(int eventId)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login","Auth");
            try
            {
                var enrols = await _api.GetAsync($"/api/Enrolments/event/{eventId}", token);
                ViewBag.EventId = eventId;
                return View(enrols);
            }
            catch
            {
                ViewBag.EventId = eventId;
                return View(null);
            }
        }
    }
}
