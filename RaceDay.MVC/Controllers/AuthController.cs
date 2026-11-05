using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RaceDay.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly Services.ApiClient _api;
        public AuthController(Services.ApiClient api) { _api = api; }

        [HttpGet] public IActionResult Register() => View();
        [HttpGet] public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string email, string password, string role)
        {
            var payload = new { fullName, email, password, role };
            var result = await _api.PostAsync("/api/Auth/register", payload, null);
            if (result != null) return RedirectToAction("Login");
            ViewBag.Error = "Registration failed.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var payload = new { email, password };
            var result = await _api.PostAsync("/api/Auth/login", payload, null);
            if (result == null)
            {
                ViewBag.Error = "Invalid login";
                return View();
            }
            var token = result.RootElement.TryGetProperty("token", out var t) ? t.GetString() : "";
            var r = result.RootElement.TryGetProperty("role", out var rr) ? rr.GetString() : "Participant";
            var em = result.RootElement.TryGetProperty("email", out var e) ? e.GetString() : email;
            HttpContext.Session.SetString("token", token ?? "");
            HttpContext.Session.SetString("role", r ?? "Participant");
            HttpContext.Session.SetString("email", em ?? email);
            if (r == "Organizer") return RedirectToAction("Dashboard", "Dashboard");
            else return RedirectToAction("Index", "Events");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

