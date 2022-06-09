using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface IBudgetsService
    {
        Task<List<Budget>> GetAllBudgets();
    }
}
