using SpendingTracker.Shared.Models;
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
    }
}
