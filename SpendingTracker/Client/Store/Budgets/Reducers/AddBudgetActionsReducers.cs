using Fluxor;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Reducers;

public static class AddBudgetActionsReducers
{
    [ReducerMethod(typeof(AddBudgetAction))]
    public static BudgetsState ReduceAddBudgetAction(BudgetsState state) =>
        new BudgetsState(isLoading: true, budgets: state.Budgets, budgetDetail: state.BudgetDetail, multiAddEnabled: state.MultiAddEnabled);

    [ReducerMethod]
    public static BudgetsState ReduceAddBudgetResultAction(BudgetsState state, AddBudgetResultAction action)
    {
        var budgets = new List<Budget>();
        budgets.AddRange(state.Budgets);
        budgets.Add(action.Budget);
        return new BudgetsState(isLoading: false, budgets: budgets, budgetDetail: state.BudgetDetail, multiAddEnabled: state.MultiAddEnabled);
    }

    [ReducerMethod(typeof(ToggleMultiAddAction))]
    public static BudgetsState ReduceToggleMultiAddAction(BudgetsState state) =>
        new BudgetsState(isLoading: state.IsLoading, budgets: state.Budgets, budgetDetail: state.BudgetDetail,
            multiAddEnabled: !state.MultiAddEnabled);
}