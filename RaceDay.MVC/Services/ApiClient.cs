using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RaceDay.MVC.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        public ApiClient(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri("http://localhost:5131");
        }

        public async Task<JsonDocument?> GetAsync(string url, string? token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var res = await _http.GetAsync(url);
                if (!res.IsSuccessStatusCode) return null;
                var json = await res.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(json)) return null;
                return JsonDocument.Parse(json);
            }
            catch { return null; }
        }

        public async Task<JsonDocument?> PostAsync(string url, object payload, string? token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(url, content);
                var respJson = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode) return null;
                if (string.IsNullOrWhiteSpace(respJson)) return null;
                return JsonDocument.Parse(respJson);
            }
            catch { return null; }
        }
    }
}
