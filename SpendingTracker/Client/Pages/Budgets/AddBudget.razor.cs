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
        [Inject] private IDispatcher Dispatcher { get; set; }
        [Inject] private IState<BudgetsState> BudgetsState { get; set; }
        private Budget Budget { get; set; } = new Budget();
        private List<Category> SelectedCategories { get; set; } = new List<Category>();
        private List<Subcategory> SelectedSubcategories { get; set; } = new List<Subcategory>();
        private MudForm Form { get; set; }
        
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