using SpendingTracker.Shared.Models;
using System.Text.Json;

namespace SpendingTracker.Client.Services
{
    public class BudgetsService : IBudgetsService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public BudgetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Budget>> GetAllBudgets()
        {
            return await JsonSerializer.DeserializeAsync<List<Budget>>
                (await _httpClient.GetStreamAsync("api/Budgets"), _jsonOptions);
        }
    }
}
