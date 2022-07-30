using Fluxor;
using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetDetail
    {
        [Parameter] public string Id { get; set; }
        [Inject] private IState<BudgetsState> BudgetsState { get; set; }
        [Inject] private IDispatcher Dispatcher { get; set; }
        private bool DialogVisible { get; set; } = false;
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new LoadBudgetDetailAction(int.Parse(Id)));
        }

        private void ToggleDialog() => DialogVisible = !DialogVisible;

        private void DeleteBudget()
        {
            Dispatcher.Dispatch(new DeleteBudgetAction(int.Parse(Id)));
            ToggleDialog();
        }
    }
}