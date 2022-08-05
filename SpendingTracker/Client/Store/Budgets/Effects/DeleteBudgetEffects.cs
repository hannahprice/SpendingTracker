using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Store.Budgets.Effects;

public class DeleteBudgetEffects
{
    private readonly IBudgetsService BudgetsService;
    private readonly NavigationManager NavigationManager;
    private readonly ISnackbar SnackBar;
    
    public DeleteBudgetEffects(IBudgetsService budgetsService, NavigationManager navigationManager, ISnackbar snackBar)
    {
        BudgetsService = budgetsService;
        NavigationManager = navigationManager;
        SnackBar = snackBar;
    }

    [EffectMethod]
    public async Task HandleDeleteBudgetAction(DeleteBudgetAction action, IDispatcher dispatcher)
    {
        var deleteSuccess = await BudgetsService.DeleteBudget(action.Id);
        dispatcher.Dispatch(new DeleteBudgetResultAction(action.Id, deleteSuccess));
    }

    [EffectMethod]
    public Task HandleDeleteBudgetResultAction(DeleteBudgetResultAction action, IDispatcher dispatcher)
    {
        if (action.Success)
        {
            SnackBar.Add($"Budget removed", Severity.Success);
            NavigationManager.NavigateTo("/budgets", false);
        }
        else
        {
            SnackBar.Add($"Error removing budget", Severity.Error);
        }
        
        return Task.CompletedTask;
    }
}