using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Budgets
{
    public partial class BudgetManagement
    {
        [Inject]
        public IBudgetsService BudgetsService { get; set; }
        public List<Budget> Budgets { get; set; }
        public bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Budgets = await BudgetsService.GetAllBudgets();

            IsLoading = false;
        }
    }

}