using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Shared.Models;
using System.Globalization;
using Fluxor;
using SpendingTracker.Client.Store.Transactions;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class AddTransaction
    {
        [Inject] private IState<TransactionsState> TransactionsState { get;set; } = default!;
        [Inject] private IDispatcher Dispatcher { get; set; } = default!;
        private Transaction Transaction { get; set; } = new Transaction();
        private Category? SelectedCategory { get; set; } = new Category();
        private List<Subcategory>? SelectedSubcategories { get; set; } = new List<Subcategory>();
        private DateTime? SelectedDatetime { get; set; }
        private MudForm? Form { get; set; }

        private readonly CultureInfo EnGbCulture = CultureInfo.GetCultureInfo("en-GB");

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            Transaction.IsOutwardPayment = true;
            Transaction.IsReoccurring = false;
        }

        private async Task Submit()
        {
            await Form!.Validate();

            if (Form.IsValid)
            {
                if (SelectedCategory != null)
                {
                    Transaction.Category = SelectedCategory;
                    Transaction.Category.Subcategories = null;
                }

                if (SelectedSubcategories != null && SelectedSubcategories.Any())
                {
                    Transaction.Subcategories = SelectedSubcategories;
                }

                if (SelectedDatetime != null)
                {
                    Transaction.DateOfTransaction = SelectedDatetime.Value;
                }

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