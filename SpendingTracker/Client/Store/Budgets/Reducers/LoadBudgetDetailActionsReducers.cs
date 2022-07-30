using Fluxor;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Store.Budgets.Reducers;

public static class LoadBudgetDetailActionsReducers
{
    [ReducerMethod(typeof(LoadBudgetDetailAction))]
    public static BudgetsState ReduceLoadBudgetDetailAction(BudgetsState state) =>
        new BudgetsState(isLoading: true, budgets: state.Budgets, budgetDetail: null, multiAddEnabled: state.MultiAddEnabled);

    [ReducerMethod]
    public static BudgetsState ReduceLoadBudgetDetailResultAction(BudgetsState state,
        LoadBudgetDetailResultAction action) =>
        new BudgetsState(isLoading: false, budgets: state.Budgets, budgetDetail: action.Budget, multiAddEnabled: state.MultiAddEnabled);
}