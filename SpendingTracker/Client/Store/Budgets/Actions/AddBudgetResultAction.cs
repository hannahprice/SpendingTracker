using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Actions;

public class AddBudgetResultAction
{
    public bool Success { get; }
    public Budget Budget { get; }

    public AddBudgetResultAction(bool success, Budget budget)
    {
        Success = success;
        Budget = budget;
    }
}