using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class TransactionList
    {
        [Inject]
        public ITransactionsService TransactionsService { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public bool IsLoading { get; set; } = false;
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Transactions = await TransactionsService.GetAllTransactions();

            IsLoading = false;
        }

        public void TransactionClicked(TableRowClickEventArgs<Transaction> eventArgs)
        {
            var id = eventArgs.Item.Id;
            NavigationManager.NavigateTo($"/transactions/{id}");
        }
    }
}