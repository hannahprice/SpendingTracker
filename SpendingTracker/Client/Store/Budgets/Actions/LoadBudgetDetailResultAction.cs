using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Actions;

public class LoadBudgetDetailResultAction
{
    public Budget? Budget { get; }

    public LoadBudgetDetailResultAction(Budget? budget)
    {
        Budget = budget;
    }
}