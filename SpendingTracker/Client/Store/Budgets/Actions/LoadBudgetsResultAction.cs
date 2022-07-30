using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Actions;

public class LoadBudgetsResultAction
{
    public IEnumerable<Budget>? Budgets { get; }

    public LoadBudgetsResultAction(IEnumerable<Budget>? budgets)
    {
        Budgets = budgets;
    }
}