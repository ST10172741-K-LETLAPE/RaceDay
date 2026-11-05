using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _api;
        public HomeController(ApiClient api) => _api = api;
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");
            var eventsDoc = await _api.GetAsync("/api/Events", token);
            ViewBag.Events = eventsDoc;
            return View();
        }
        public IActionResult Privacy() => View();
    }
}

