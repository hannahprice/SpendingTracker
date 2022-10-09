using Fluxor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Effects;

public class LoadBudgetsEffects
{
    private readonly IBudgetsService BudgetsService;

    public LoadBudgetsEffects(IBudgetsService budgetsService)
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