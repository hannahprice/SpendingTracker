using Fluxor;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Store.Budgets.Reducers;

public static class DeleteBudgetActionsReducers
{
    [ReducerMethod(typeof(DeleteBudgetAction))]
    public static BudgetsState ReduceDeleteBudgetAction(BudgetsState state)
        => new BudgetsState(isLoading: true, budgets: state.Budgets, budgetDetail: state.BudgetDetail, multiAddEnabled: state.MultiAddEnabled);

    [ReducerMethod]
    public static BudgetsState ReduceDeleteBudgetResultAction(BudgetsState state, DeleteBudgetResultAction action)
    {
        if (action.Success)
        {
            var budgets = state.Budgets!.Where(x => x.Id != action.Id);   
            return new BudgetsState(isLoading: false, budgets: budgets, budgetDetail: null, multiAddEnabled: state.MultiAddEnabled);
        }

        return new BudgetsState(isLoading: false, budgets: state.Budgets, budgetDetail: state.BudgetDetail, multiAddEnabled: state.MultiAddEnabled);
    }
}