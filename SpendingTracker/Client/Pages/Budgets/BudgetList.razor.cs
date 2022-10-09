using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetList
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IState<BudgetsState> BudgetsState { get; set; } = default!;

        private void BudgetClicked(TableRowClickEventArgs<Budget> eventArgs)
        {
            var id = eventArgs.Item.Id;
            NavigationManager.NavigateTo($"/budgets/{id}");
        }
    }
}