using SpendingTracker.Shared.Models;
using System.Text;
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

        public async Task<int> AddTransaction(Transaction transaction)
        {
            var json = new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Transactions", json);

            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                return int.Parse(data);
            }

            return default;
        }
    }
}