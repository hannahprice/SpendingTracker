using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Store.Transactions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class TransactionList
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IState<TransactionsState> TransactionsState { get; set; } = default!;

        private void TransactionClicked(TableRowClickEventArgs<Transaction> eventArgs)
        {
            var id = eventArgs.Item.Id;
            NavigationManager?.NavigateTo($"/transactions/{id}");
        }
    }
}