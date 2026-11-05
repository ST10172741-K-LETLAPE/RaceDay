using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly Services.ApiClient _api;
        private readonly Services.BlobStorageService _blob;
        public EventsController(Services.ApiClient api, Services.BlobStorageService blob) { _api = api; _blob = blob; }

        public async Task<IActionResult> Index()
        {
            var eventsDoc = await _api.GetAsync("/api/Events", HttpContext.Session.GetString("token"));
            return View(eventsDoc);
        }

        public async Task<IActionResult> Details(int id)
        {
            var token = HttpContext.Session.GetString("token");
            var ev = await _api.GetAsync($"/api/Events/{id}", token);
            ViewBag.EventId = id;
            // Also get enrolments count for organizer summary
            try {
                var enrols = await _api.GetAsync($"/api/Enrolments/event/{id}", token);
                ViewBag.EnrolmentCount = enrols?.RootElement.GetArrayLength() ?? 0;
            } catch { ViewBag.EnrolmentCount = 0; }
            return View(ev);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Organizer") return RedirectToAction("Login", "Auth");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string location, DateTime eventDate, string description, IFormFile? bannerFile, string status)
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("role");
            if (role != "Organizer") return RedirectToAction("Login", "Auth");

            string bannerUrl = "";
            if (bannerFile != null)
            {
                bannerUrl = await _blob.UploadAsync(bannerFile);
            }

            var payload = new
            {
                title,
                location,
                eventDate,
                description,
                bannerUrl,
                status = string.IsNullOrEmpty(status) ? "Upcoming" : status
            };

            var result = await _api.PostAsync("/api/Events", payload, token);
            if (result != null)
                return RedirectToAction("Dashboard", "Dashboard");
            
            ViewBag.Error = "Failed to create event. Check API is running on port 5131.";
            return View();
        }

        public async Task<IActionResult> MyEnrolments()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            var my = await _api.GetAsync("/api/Enrolments/my", token);
            return View(my);
        }

        [HttpPost]
        public async Task<IActionResult> Enrol(int eventId, int categoryId)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            var payload = new { eventId, categoryId };
            var result = await _api.PostAsync("/api/Enrolments", payload, token);
            return RedirectToAction("MyEnrolments");
        }
    }
}
