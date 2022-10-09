using System.Net.Http.Json;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public class BudgetsService : IBudgetsService
    {
        private readonly HttpClient _httpClient;

        public BudgetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Budget>?> GetAllBudgets()
        {
            return await _httpClient.GetFromJsonAsync<List<Budget>>("api/Budgets");
        }

        public async Task<int> AddBudget(Budget budget)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Budgets", budget);

            if (!result.IsSuccessStatusCode) return default;
            
            return await result.Content.ReadFromJsonAsync<int>();
        }

        public async Task<Budget?> GetBudget(int id)
        {
            return await _httpClient.GetFromJsonAsync<Budget>($"api/Budgets/{id}");
        }

        public async Task<bool> DeleteBudget(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Budgets/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}