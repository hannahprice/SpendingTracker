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
        MudChip[]? SelectedCategories { get; set; }
        MudChip[]? SelectedSubcategories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AvailableCategories = await CategoriesService.GetAllCategories();
            AvailableSubcategories = AvailableCategories.SelectMany(x => x.Subcategories).ToList();
        }
    }
}
