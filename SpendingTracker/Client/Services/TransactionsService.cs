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

        public async Task<List<Transaction>?> GetAllTransactions()
        {
            return await _httpClient.GetFromJsonAsync<List<Transaction>>("api/Transactions");
        }

        public async Task<int> AddTransaction(Transaction transaction)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Transactions", transaction);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<int>();
            }

            return default;
        }

        public async Task<Transaction?> GetTransaction(int id)
        {
            return await _httpClient.GetFromJsonAsync<Transaction>($"api/Transactions/{id}");
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var result = await  _httpClient.DeleteAsync($"api/Transactions/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}