using SpendingTracker.Shared.Models;
using System.Text;
using System.Text.Json;

namespace SpendingTracker.Client.Services
{
    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public SubcategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<int> AddSubcategory(Subcategory subcategory)
        {
            var json = new StringContent(JsonSerializer.Serialize(subcategory), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Subcategories", json);

            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                return int.Parse(data);
            }

            return default;
        }
    }
}