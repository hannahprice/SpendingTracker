using Fluxor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Store.Budgets.Effects;

public class LoadBudgetDetailEffects
{
    private readonly IBudgetsService BudgetsService;

    public LoadBudgetDetailEffects(IBudgetsService budgetsService)
    {
        BudgetsService = budgetsService;
    }

    [EffectMethod]
    public async Task HandleLoadBudgetDetailAction(LoadBudgetDetailAction action, IDispatcher dispatcher)
    {
        var budget = await BudgetsService.GetBudget(action.Id);
        dispatcher.Dispatch(new LoadBudgetDetailResultAction(budget));
    }
}