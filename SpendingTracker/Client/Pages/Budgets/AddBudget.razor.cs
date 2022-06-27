using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class AddBudget
    {
        [Inject]
        public IBudgetsService BudgetsService { get; set; }
        [Inject]
        public ICategoriesService CategoriesService { get; set; }
        public Budget Budget { get; set; } = new Budget();
        public List<Category> AvailableCategories { get; set; } = new List<Category>();
        public List<Subcategory> AvailableSubcategories { get; set; } = new List<Subcategory>();
        public MudChip[]? SelectedCategories { get; set; }
        public MudChip[]? SelectedSubcategories { get; set; }
        public bool? Success { get; set; } = null;
        public MudForm Form { get; set; }
        protected override async Task OnInitializedAsync()
        {
            AvailableCategories = await CategoriesService.GetAllCategories();
            AvailableSubcategories = AvailableCategories.SelectMany(x => x.Subcategories).ToList();
        }

        public async Task Submit()
        {
            // todo handle validation of cats
            if (SelectedCategories is not null && SelectedCategories.Any())
            {
                Budget.Categories = GetSelectedCategoriesForSubmit();
                
                if (SelectedSubcategories is not null && SelectedSubcategories.Any())
                {
                    Budget.Subcategories = GetSelectedSubcategoriesForSubmit();
                }
 
                var createdId = await BudgetsService.AddBudget(Budget);

                Success = createdId > 0;

                Form.Reset();
            }
        }

        private List<Category> GetSelectedCategoriesForSubmit()
        {
            var selectedCategoryDescriptions = SelectedCategories.Select(c => c.Text).ToList();
            var test = AvailableCategories.Where(x => selectedCategoryDescriptions.Contains(x.Description)).ToList();
            test.ForEach(x => x.Subcategories = null);
            return test;
        }

        private List<Subcategory> GetSelectedSubcategoriesForSubmit()
        {
            var selectedSubcategoryDescriptions = SelectedSubcategories.Select(c => c.Text).ToList();
            return AvailableSubcategories.Where(x => selectedSubcategoryDescriptions.Contains(x.Description)).ToList();
        }
    }
}
