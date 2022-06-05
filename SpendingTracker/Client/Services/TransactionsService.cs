using SpendingTracker.Shared.Models;
using System.Text.Json;

namespace SpendingTracker.Client.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonOptions;

        public TransactionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await JsonSerializer.DeserializeAsync<List<Transaction>>
                (await _httpClient.GetStreamAsync("api/Transactions"), _jsonOptions);
        }
    }
}
