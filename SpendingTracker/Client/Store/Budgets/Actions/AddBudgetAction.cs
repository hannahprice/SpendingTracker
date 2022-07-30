using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Actions;

public class AddBudgetAction
{
    public Budget Budget { get; }

    public AddBudgetAction(Budget budget)
    {
        Budget = budget;
    }
}