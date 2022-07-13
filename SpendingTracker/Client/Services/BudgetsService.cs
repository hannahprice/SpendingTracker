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

        public Task<List<Budget>> GetAllBudgets()
        {
            return _httpClient.GetFromJsonAsync<List<Budget>>("api/Budgets");
        }

        public async Task<int> AddBudget(Budget budget)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Budgets", budget);

            if (!result.IsSuccessStatusCode) return default;
            
            var data = await result.Content.ReadAsStringAsync();
            return int.Parse(data);
        }

        public Task<Budget> GetBudget(int id)
        {
            return _httpClient.GetFromJsonAsync<Budget>($"api/Budgets/{id}");
        }

        public Task DeleteBudget(int id)
        {
            return _httpClient.DeleteAsync($"api/Budgets/{id}");
        }
    }
}