using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Transactions;
using SpendingTracker.Client.Store.Transactions.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class TransactionList
    {
        //[Inject]
        //public ITransactionsService TransactionsService { get; set; }

        //public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        //public bool IsLoading { get; set; } = false;
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IState<TransactionsState> TransactionsState { get; set; }
        [Inject] private IDispatcher Dispatcher { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new LoadTransactionsAction());
            //IsLoading = true;

            //Transactions = await TransactionsService.GetAllTransactions();

            //IsLoading = false;
        }

        private void TransactionClicked(TableRowClickEventArgs<Transaction> eventArgs)
        {
            var id = eventArgs.Item.Id;
            NavigationManager.NavigateTo($"/transactions/{id}");
        }
    }
}