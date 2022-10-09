using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class AddBudget
    {
        [Inject] private IDispatcher Dispatcher { get; set; } = default!;
        [Inject] private IState<BudgetsState> BudgetsState { get; set; } = default!;
        private Budget Budget { get; set; } = new Budget();
        private Category SelectedCategory { get; set; } = new Category();
        private List<Subcategory> SelectedSubcategories { get; set; } = new List<Subcategory>();
        private MudForm Form { get; set; } = default!;
        
        private async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                if (SelectedCategory.Description != null)
                {
                    Budget.Category = SelectedCategory;
                    Budget.Category.Subcategories = null;

                    if (SelectedSubcategories.Any())
                    {
                        Budget.Subcategories = SelectedSubcategories;
                    }
                }

                Dispatcher.Dispatch(new AddBudgetAction(Budget));
                Form.Reset();
            }
        }

        private void ToggleMultiAdd()
        {
            Dispatcher.Dispatch(new ToggleMultiAddAction());
        }
    }
}