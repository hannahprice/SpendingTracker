using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetList
    {
        [Inject]
        public IBudgetsService BudgetsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<Budget> Budgets { get; set; } = new List<Budget>();
        public bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Budgets = await BudgetsService.GetAllBudgets();

            IsLoading = false;
        }

        public void BudgetClicked(TableRowClickEventArgs<Budget> eventArgs)
        {
            var id = eventArgs.Item.Id;
            NavigationManager.NavigateTo($"/budgets/{id}");
        }
    }
}