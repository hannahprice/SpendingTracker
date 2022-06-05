using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactions();
    }
}
