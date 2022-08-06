using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;
using System.Globalization;
using Fluxor;
using SpendingTracker.Client.Store.Transactions;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class AddTransaction
    {
        [Inject] private IState<TransactionsState> TransactionsState { get;set; }
        [Inject] private IDispatcher Dispatcher { get; set; }
        private Transaction Transaction { get; set; } = new Transaction();
        private List<Category> SelectedCategories { get; set; } = new List<Category>();
        private List<Subcategory> SelectedSubcategories { get; set; } = new List<Subcategory>();
        private DateTime? SelectedDatetime { get; set; }
        private MudForm Form { get; set; }

        private CultureInfo EnGbCulture = CultureInfo.GetCultureInfo("en-GB");

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            Transaction.IsOutwardPayment = true;
            Transaction.IsReoccurring = false;
        }

        private async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                if (SelectedCategories.Any())
                {
                    Transaction.Categories = SelectedCategories;
                    Transaction.Categories.ForEach(x => x.Subcategories = null);
                }

                if (SelectedSubcategories.Any())
                {
                    Transaction.Subcategories = SelectedSubcategories;
                }

                Transaction.DateOfTransaction = SelectedDatetime.Value;

                Dispatcher.Dispatch(new AddTransactionAction(Transaction));
                Form.Reset();
            }
        }
        
        private void ToggleMultiAdd()
        {
            Dispatcher.Dispatch(new ToggleMultiAddAction());
        }
    }
}