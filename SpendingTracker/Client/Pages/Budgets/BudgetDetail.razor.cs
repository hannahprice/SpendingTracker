using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetDetail
    {
        [Parameter] public string Id { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IBudgetsService BudgetsService { get; set; }
        private Budget Budget { get; set; } = new Budget();
        private bool IsLoading { get; set; } = false;
        private bool DialogVisible { get; set; } = false;
        
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            Budget = await BudgetsService.GetBudget(int.Parse(Id));
            IsLoading = false;
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
                Snackbar.Add($"Error removing budget: {Budget.Amount}", Severity.Error);
            }
        }
    }
}