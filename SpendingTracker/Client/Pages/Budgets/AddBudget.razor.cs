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
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public Budget Budget { get; set; } = new Budget();
        public List<Category> SelectedCategories { get; set; } = new List<Category>();
        public List<Subcategory> SelectedSubcategories { get; set; } = new List<Subcategory>();
        public bool? Success { get; set; } = null;
        public MudForm Form { get; set; }
        public bool AddingMultiple { get; set; } = false;
        
        private async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                if (SelectedCategories.Any())
                {
                    Budget.Categories = SelectedCategories;
                    Budget.Categories.ForEach(x => x.Subcategories = null);

                    if (SelectedSubcategories.Any())
                    {
                        Budget.Subcategories = SelectedSubcategories;
                    }
                }

                var createdId = await BudgetsService.AddBudget(Budget);
                Success = createdId > 0;

                AddSnackBarMessage();
                Form.Reset();

                if (!AddingMultiple)
                {
                    NavigationManager.NavigateTo("/budgets", false);
                }
            }
        }

        private void AddSnackBarMessage()
        {
            if (Success!.Value)
            {
                Snackbar.Add($"Budget added: {Budget.Amount}", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Error adding budget: {Budget.Amount}", Severity.Error);
            }
        }
    }
}