using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    public class ResultsController : Controller
    {
        private readonly Services.ApiClient _api;
        public ResultsController(Services.ApiClient api) { _api = api; }

        // Organizer: Results entry page to capture and publish finish times
        public async Task<IActionResult> Entry(int eventId)
        {
            var token = HttpContext.Session.GetString("token");
            ViewBag.EventId = eventId;
            var results = await _api.GetAsync($"/api/Results/event/{eventId}", token);
            var enrols = await _api.GetAsync($"/api/Enrolments/event/{eventId}", token);
            ViewBag.Enrolments = enrols;
            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> Publish(int eventId, int enrolmentId, string finishTime, int position)
        {
            var token = HttpContext.Session.GetString("token");
            var payload = new { eventId, enrolmentId, finishTime, position, published = true };
            await _api.PostAsync("/api/Results", payload, token);
            return RedirectToAction("Entry", new { eventId });
        }

        // Participant: My Results page - personal race history
        public async Task<IActionResult> MyResults()
        {
            var token = HttpContext.Session.GetString("token");
            if(string.IsNullOrEmpty(token)) return RedirectToAction("Login","Auth");
            var results = await _api.GetAsync("/api/Results/my", token);
            return View(results);
        }
    }
}
