using Microsoft.AspNetCore.Components;
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
        public IBudgetsService BudgetsService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            Budget = await BudgetsService.GetBudget(int.Parse(Id));
            IsLoading = false;
        }
    }
}