using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Store.Transactions.Effects;

public class DeleteTransactionEffects
{
    private readonly ITransactionsService TransactionsService;
    private readonly NavigationManager NavigationManager;
    private readonly ISnackbar SnackBar;

    public DeleteTransactionEffects(ITransactionsService transactionsService, NavigationManager navigationManager, ISnackbar snackbar)
    {
        TransactionsService = transactionsService;
        NavigationManager = navigationManager;
        SnackBar = snackbar;
    }

    [EffectMethod]
    public async Task HandleDeleteTransactionAction(DeleteTransactionAction action, IDispatcher dispatcher)
    {
        var deleteSuccess = await TransactionsService.DeleteTransaction(action.Id);
        dispatcher.Dispatch(new DeleteTransactionResultAction(action.Id, deleteSuccess));
    }
    
    [EffectMethod]
    public Task HandleDeleteTransactionResultAction(DeleteTransactionResultAction action, IDispatcher dispatcher)
    {
        if (action.Success)
        {
            SnackBar.Add($"Transaction removed", Severity.Success);
            NavigationManager.NavigateTo("/transactions", false);
        }
        else
        {
            SnackBar.Add($"Error removing transaction", Severity.Error);
        }
        
        return Task.CompletedTask;
    }
}