using Fluxor;
using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Store.Transactions;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Pages.Transactions;

public partial class TransactionDetail
{
    [Parameter] public string? Id { get; set; }
    [Inject] private IState<TransactionsState> TransactionsState { get; set; } = default!;
    [Inject] private IDispatcher Dispatcher { get; set; } = default!;
    private bool DialogVisible { get; set; } = false;

    private void ToggleDialog() => DialogVisible = !DialogVisible;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (Id != null)
        {
            Dispatcher.Dispatch(new LoadTransactionDetailAction(int.Parse(Id)));
        }
    }

    private void DeleteTransaction()
    {
        if (Id != null)
        {
            Dispatcher.Dispatch(new DeleteTransactionAction(int.Parse(Id)));
            ToggleDialog();
        }
    }
}