using System.Net.Http.Json;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly HttpClient _httpClient;

        public TransactionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Transaction>> GetAllTransactions()
        {
            return _httpClient.GetFromJsonAsync<List<Transaction>>("api/Transactions");
        }

        public async Task<int> AddTransaction(Transaction transaction)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Transactions", transaction);

            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                return int.Parse(data);
            }

            return default;
        }

        public Task<Transaction> GetTransaction(int id)
        {
            return _httpClient.GetFromJsonAsync<Transaction>($"api/Transactions/{id}");
        }
    }
}