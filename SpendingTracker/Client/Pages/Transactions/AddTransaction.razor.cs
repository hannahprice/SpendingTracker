using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;
using System.Globalization;

namespace SpendingTracker.Client.Pages.Transactions
{
    public partial class AddTransaction
    {
        [Inject]
        public ITransactionsService TransactionsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public Transaction Transaction { get; set; } = new Transaction();
        public List<Category> SelectedCategories { get; set; } = new List<Category>();
        public List<Subcategory> SelectedSubcategories { get; set; } = new List<Subcategory>();
        public DateTime? SelectedDatetime { get; set; }
        public bool? Success { get; set; } = null;
        public MudForm Form { get; set; }
        public bool AddingMultiple { get; set; } = false;

        public CultureInfo EnGbCulture = CultureInfo.GetCultureInfo("en-GB");

        protected override async Task OnInitializedAsync()
        {
            Transaction.IsOutwardPayment = true;
            Transaction.IsReoccurring = false;
        }

        public async Task Submit()
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

                var createdId = await TransactionsService.AddTransaction(Transaction);
                Success = createdId > 0;

                AddSnackBarMessage();
                Form.Reset();

                if (!AddingMultiple)
                {
                    NavigationManager.NavigateTo("/transactions", false);
                }
            }
        }

        private void AddSnackBarMessage()
        {
            if (Success.Value)
            {
                Snackbar.Add($"Transaction added: {Transaction.Description}", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Error adding transaction: {Transaction.Description}", Severity.Error);
            }
        }
    }
}