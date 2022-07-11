using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetDetail
    {
        [Parameter]
        public string Id { get; set; }

        public Budget Budget { get; set; } = new Budget();
        public bool IsLoading { get; set; } = false;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }
        
        [Inject]
        public IBudgetsService BudgetsService { get; set; }

        public bool DialogVisible { get; set; } = false;
        
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            Budget = await BudgetsService.GetBudget(int.Parse(Id));
            IsLoading = false;
        }

        public void ToggleDialog() => DialogVisible = !DialogVisible;

        public async Task DeleteBudget()
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