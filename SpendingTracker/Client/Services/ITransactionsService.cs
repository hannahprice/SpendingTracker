using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface ITransactionsService
    {
        Task<List<Transaction>?> GetAllTransactions();

        Task<int> AddTransaction(Transaction transaction);
        Task<Transaction?> GetTransaction(int id);
        Task<bool> DeleteTransaction(int id);
    }
}