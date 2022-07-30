using Fluxor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Store.Budgets.Effects;

public class LoadBudgetsEffect
{
    private readonly IBudgetsService BudgetsService;

    public LoadBudgetsEffect(IBudgetsService budgetsService)
    {
        BudgetsService = budgetsService;
    }

    [EffectMethod]
    public async Task HandleLoadBudgetsAction(LoadBudgetsAction action, IDispatcher dispatcher)
    {
        var budgets = await BudgetsService.GetAllBudgets();
        dispatcher.Dispatch(new LoadBudgetsResultAction(budgets));
    }
}