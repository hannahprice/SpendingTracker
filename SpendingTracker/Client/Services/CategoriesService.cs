using System.Net.Http.Json;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient _httpClient;

        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("api/Categories");
        }

        public async Task<int> AddCategory(Category category)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Categories", category);

            if (!result.IsSuccessStatusCode) return default;
            var data = await result.Content.ReadAsStringAsync();
            
            return int.Parse(data);
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"api/Categories/{id}");
        }
        
        public async Task DeleteCategory(int id)
        {
            await _httpClient.DeleteAsync($"api/Categories/{id}");
        }
    }
}