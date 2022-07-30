using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Client.Store.Budgets.Actions;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetDetail
    {
        [Parameter] public string Id { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IBudgetsService BudgetsService { get; set; }
        [Inject] private IState<BudgetsState> BudgetsState { get; set; }
        [Inject] private IDispatcher Dispatcher { get; set; }
        private bool DialogVisible { get; set; } = false;
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new LoadBudgetDetailAction(int.Parse(Id)));
        }

        private void ToggleDialog() => DialogVisible = !DialogVisible;

        private async Task DeleteBudget()
        {
            try
            {
                await BudgetsService.DeleteBudget(int.Parse(Id));
                
                ToggleDialog();
                Snackbar.Add($"Budget removed", Severity.Success);
                
                NavigationManager.NavigateTo("/budgets", false);
            }
            catch
            {
                Snackbar.Add($"Error removing budget: {BudgetsState.Value.BudgetDetail?.Amount}", Severity.Error);
            }
        }
    }
}