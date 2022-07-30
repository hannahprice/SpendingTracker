using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetList
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IState<BudgetsState> BudgetsState { get; set; }
        [Inject] private IDispatcher Dispatcher { get; set; }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new LoadBudgetsAction());
        }

        private void BudgetClicked(TableRowClickEventArgs<Budget> eventArgs)
        {
            var id = eventArgs.Item.Id;
            NavigationManager.NavigateTo($"/budgets/{id}");
        }
    }
}