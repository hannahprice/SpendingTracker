using System.Net.Http.Json;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly HttpClient _httpClient;

        public SubcategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> AddSubcategory(Subcategory subcategory)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Subcategories", subcategory);

            if (!result.IsSuccessStatusCode) return default;
            
            return await result.Content.ReadFromJsonAsync<int>();
        }
        
        public async Task<Subcategory?> GetSubcategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<Subcategory>($"api/Subcategories/{id}");
        }

        public async Task DeleteSubcategory(int id)
        {
            await _httpClient.DeleteAsync($"api/Subcategories/{id}");
        }
    }
}