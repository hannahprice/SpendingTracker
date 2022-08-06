using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Store.Transactions.Effects;

public class AddTransactionEffects
{
    private readonly ITransactionsService TransactionsService;
    private readonly ISnackbar Snackbar;
    private readonly NavigationManager NavigationManager;
    private readonly IState<TransactionsState> TransactionsState;

    public AddTransactionEffects(ITransactionsService transactionsService, ISnackbar snackbar, NavigationManager navigationManager, IState<TransactionsState> transactionsState)
    {
        TransactionsService = transactionsService;
        Snackbar = snackbar;
        NavigationManager = navigationManager;
        TransactionsState = transactionsState;
    }
    
    [EffectMethod]
    public async Task HandleAddTransactionAction(AddTransactionAction action, IDispatcher dispatcher)
    {
        var createdId = await TransactionsService.AddTransaction(action.Transaction);
        var addedTransaction = await TransactionsService.GetTransaction(createdId);
        dispatcher.Dispatch(new AddTransactionResultAction(createdId > 0, addedTransaction));
    }
    
    [EffectMethod]
    public Task HandleAddTransactionResultAction(AddTransactionResultAction action, IDispatcher dispatcher)
    {
        if (action.Success)
        {
            Snackbar.Add($"Transaction added", Severity.Success);
        }
        else
        {
            Snackbar.Add($"Error adding transaction", Severity.Error);
        }

        if (!TransactionsState.Value.MultiAddEnabled)
        {
            NavigationManager.NavigateTo("/transactions", false);
        }
        
        return Task.CompletedTask;
    }
}