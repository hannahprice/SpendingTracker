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
        public ICategoriesService CategoriesService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public Transaction Transaction { get; set; } = new Transaction();
        public List<Category> AvailableCategories { get; set; } = new List<Category>();
        public List<Subcategory> AvailableSubcategories { get; set; } = new List<Subcategory>();
        public MudChip[]? SelectedCategories { get; set; }
        public MudChip[]? SelectedSubcategories { get; set; }
        public DateTime? SelectedDatetime { get; set; }
        public bool? Success { get; set; } = null;
        public MudForm Form { get; set; }
        public bool AddingMultiple { get; set; } = false;

        public CultureInfo EnGbCulture = CultureInfo.GetCultureInfo("en-GB");

        protected override async Task OnInitializedAsync()
        {
            Transaction.IsOutwardPayment = true;
            Transaction.IsReoccurring = false;

            AvailableCategories = await CategoriesService.GetAllCategories();
            AvailableSubcategories = AvailableCategories.SelectMany(x => x.Subcategories).ToList();
        }

        public async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                if (SelectedCategories is not null && SelectedCategories.Any())
                {
                    Transaction.Categories = GetSelectedCategoriesForSubmit();
                }

                if (SelectedSubcategories is not null && SelectedSubcategories.Any())
                {
                    Transaction.Subcategories = GetSelectedSubcategoriesForSubmit();
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

        private List<Category> GetSelectedCategoriesForSubmit()
        {
            var selectedCategoryDescriptions = SelectedCategories?.Select(c => c.Text).ToList();
            var categories = AvailableCategories.Where(x => selectedCategoryDescriptions.Contains(x.Description)).ToList();
            categories.ForEach(x => x.Subcategories = null);
            return categories;
        }

        private List<Subcategory> GetSelectedSubcategoriesForSubmit()
        {
            var selectedSubcategoryDescriptions = SelectedSubcategories?.Select(c => c.Text).ToList();
            return AvailableSubcategories.Where(x => selectedSubcategoryDescriptions.Contains(x.Description)).ToList();
        }
    }
}