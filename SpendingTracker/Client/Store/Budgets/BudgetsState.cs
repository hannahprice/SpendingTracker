using Fluxor;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets;

[FeatureState]
public class BudgetsState
{
    public bool IsLoading { get; }
    public IEnumerable<Budget>? Budgets { get; }
    public Budget? BudgetDetail { get; }

    private BudgetsState() { }

    public BudgetsState(bool isLoading, IEnumerable<Budget>? budgets, Budget? budgetDetail)
    {
        IsLoading = isLoading;
        Budgets = budgets ?? Array.Empty<Budget>();
        BudgetDetail = budgetDetail;
    }
}