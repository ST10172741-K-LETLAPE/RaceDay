using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Services.ApiClient _api;
        private readonly Services.BlobStorageService _blob;
        public ProfileController(Services.ApiClient api, Services.BlobStorageService blob) { _api = api; _blob = blob; }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");
            if(string.IsNullOrEmpty(token)) return RedirectToAction("Login","Auth");
            var profile = await _api.GetAsync("/api/Users/me", token);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string fullName, string email, IFormFile? profilePicture)
        {
            var token = HttpContext.Session.GetString("token");
            string profilePicUrl = "";
            if(profilePicture != null)
            {
                profilePicUrl = await _blob.UploadAsync(profilePicture);
            }
            var payload = new { fullName, email, profilePictureUrl = profilePicUrl };
            await _api.PostAsync("/api/Users/update", payload, token);
            if(!string.IsNullOrEmpty(profilePicUrl))
                HttpContext.Session.SetString("profilePic", profilePicUrl);
            return RedirectToAction("Index");
        }
    }
}
