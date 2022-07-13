﻿using System.Net.Http.Json;
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

        public Task<List<Category>> GetAllCategories()
        {
            return _httpClient.GetFromJsonAsync<List<Category>>("api/Categories");
        }

        public async Task<int> AddCategory(Category category)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Categories", category);

            if (!result.IsSuccessStatusCode) return default;
            var data = await result.Content.ReadAsStringAsync();
            
            return int.Parse(data);
        }
    }
}