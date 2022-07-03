using SpendingTracker.Shared.Models;
using System.Text;
using System.Text.Json;

namespace SpendingTracker.Client.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await JsonSerializer.DeserializeAsync<List<Category>>
                (await _httpClient.GetStreamAsync("api/Categories"), _jsonOptions);
        }

        public async Task<int> AddCategory(Category category)
        {
            var json = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Categories", json);

            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                return int.Parse(data);
            }

            return default;
        }
    }
}