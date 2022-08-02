using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Transactions;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Pages.Transactions;

public partial class TransactionDetail
{
    [Parameter] public string Id { get; set; }
    [Inject] private ITransactionsService TransactionsService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] private IState<TransactionsState> TransactionsState { get; set; }
    [Inject] private IDispatcher Dispatcher { get; set; }
    private bool DialogVisible { get; set; } = false;

    private void ToggleDialog() => DialogVisible = !DialogVisible;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new LoadTransactionDetailAction(int.Parse(Id)));   
    }

    private async Task DeleteTransaction()
    {
        try
        {
            await TransactionsService.DeleteTransaction(int.Parse(Id));

            ToggleDialog();
            Snackbar.Add($"Transaction removed", Severity.Success);
            NavigationManager.NavigateTo("/transactions", false);
        }
        catch
        {
            Snackbar.Add($"Error removing transaction", Severity.Error);
        }
    }
}