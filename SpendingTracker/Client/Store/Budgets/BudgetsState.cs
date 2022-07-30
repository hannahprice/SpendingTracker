using Fluxor;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets;

[FeatureState]
public class BudgetsState
{
    public bool IsLoading { get; }
    public IEnumerable<Budget>? Budgets { get; }
    public Budget? BudgetDetail { get; }
    public bool MultiAddEnabled { get; }

    private BudgetsState() { }

    public BudgetsState(bool isLoading, IEnumerable<Budget>? budgets, Budget? budgetDetail, bool multiAddEnabled)
    {
        IsLoading = isLoading;
        Budgets = budgets ?? Array.Empty<Budget>();
        BudgetDetail = budgetDetail;
        MultiAddEnabled = multiAddEnabled;
    }
}