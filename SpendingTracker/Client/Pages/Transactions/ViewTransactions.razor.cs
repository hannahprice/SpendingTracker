using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class ViewTransactions
    {

        [Inject]
        public ITransactionService TransactionService { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>(0);
        public bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            
            Transactions = await TransactionService.GetAllTransactions();
            
            IsLoading = false;
        }
    }
}
