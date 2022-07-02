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

        public Transaction Transaction { get; set; } = new Transaction();
        public List<Category> AvailableCategories { get; set; } = new List<Category>();
        public List<Subcategory> AvailableSubcategories { get; set; } = new List<Subcategory>();
        public MudChip[]? SelectedCategories { get; set; }
        public MudChip[]? SelectedSubcategories { get; set; }
        public DateTime? SelectedDatetime { get; set; }
        public bool? Success { get; set; } = null;
        public MudForm Form { get; set; }

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

                Form.Reset();
            }
        }

        private List<Category> GetSelectedCategoriesForSubmit()
        {
            var selectedCategoryDescriptions = SelectedCategories?.Select(c => c.Text).ToList();
            var test = AvailableCategories.Where(x => selectedCategoryDescriptions.Contains(x.Description)).ToList();
            test.ForEach(x => x.Subcategories = null);
            return test;
        }

        private List<Subcategory> GetSelectedSubcategoriesForSubmit()
        {
            var selectedSubcategoryDescriptions = SelectedSubcategories?.Select(c => c.Text).ToList();
            return AvailableSubcategories.Where(x => selectedSubcategoryDescriptions.Contains(x.Description)).ToList();
        }
    }
}