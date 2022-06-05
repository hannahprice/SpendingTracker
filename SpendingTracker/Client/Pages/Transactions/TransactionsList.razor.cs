using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class TransactionsList
    {

        [Inject]
        public ITransactionsService TransactionsService { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            
            Transactions = await TransactionsService.GetAllTransactions();
            
            IsLoading = false;
        }
    }
}
