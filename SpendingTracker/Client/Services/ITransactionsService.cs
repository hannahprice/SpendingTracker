using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface ITransactionsService
    {
        Task<List<Transaction>> GetAllTransactions();
    }
}
