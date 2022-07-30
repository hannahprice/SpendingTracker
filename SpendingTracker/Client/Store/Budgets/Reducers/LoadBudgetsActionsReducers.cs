using Fluxor;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Store.Budgets.Reducers;

public static class LoadBudgetsActionsReducers
{
    [ReducerMethod(typeof(LoadBudgetsAction))]
    public static BudgetsState ReduceLoadBudgetsAction(BudgetsState state) =>
        new BudgetsState(isLoading: true, budgets: null, budgetDetail: state.BudgetDetail);

    [ReducerMethod]
    public static BudgetsState ReduceLoadBudgetsResultAction(BudgetsState state, LoadBudgetsResultAction action) =>
        new BudgetsState(isLoading: false, budgets: action.Budgets, state.BudgetDetail);
}