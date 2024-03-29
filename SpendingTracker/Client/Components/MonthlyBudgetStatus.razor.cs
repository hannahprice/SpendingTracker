﻿using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Components;

public partial class MonthlyBudgetStatus
{
    [Inject] private IBudgetsService BudgetsService { get; set; } = default!;
    [Inject] private ITransactionsService TransactionsService { get; set; } = default!;
    private List<Budget> AllBudgets { get; set; } = new List<Budget>();
    private List<Transaction> AllTransactions { get; set; } = new List<Transaction>();
    private bool IsLoading { get; set; }

    private List<MonthlyBudgetStatusViewModel> MonthlyBudgetStatuses { get; set; } =
        new List<MonthlyBudgetStatusViewModel>();

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        
        var getBudgetsTask = BudgetsService.GetAllBudgets();
        var getTransactionsTask = TransactionsService.GetAllTransactions();
        
        await Task.WhenAll(getBudgetsTask, getTransactionsTask);

        if (getBudgetsTask.Result != null && getBudgetsTask.Result.Any())
        {
            var monthlyBudgets = getBudgetsTask.Result.Where(x => x.Frequency == Frequency.Monthly);
            
            var thisMonthsTransactions = getTransactionsTask.Result?.Where(x =>
                x.DateOfTransaction.Date.Month == DateTime.Now.Date.Month).ToList();

            foreach (var budget in monthlyBudgets)
            {
                var budgetStatus = GetBudgetStatus(budget, thisMonthsTransactions);
                MonthlyBudgetStatuses.Add(budgetStatus);
            }    
        }
        
        IsLoading = false;
    }

    private static MonthlyBudgetStatusViewModel GetBudgetStatus(Budget budget, List<Transaction>? thisMonthsTransactions)
    {
        var transactionsForThisBudget =
            thisMonthsTransactions?.Where(x => x.Category != null && x.Category.Id == budget.Category?.Id).ToList();
        
        var currentMonthSpendTowardsBudget = transactionsForThisBudget?.Select(x => x.Amount).Sum();
            
        return new MonthlyBudgetStatusViewModel()
        {
            BudgetDescription = budget.Category!.Description,
            BudgetAmount = budget.Amount,
            CurrentMonthSpendForBudget = currentMonthSpendTowardsBudget ?? decimal.Zero,
            WithinBudget = currentMonthSpendTowardsBudget <= budget.Amount
        };
    }
}

public class MonthlyBudgetStatusViewModel
{
    public string BudgetDescription { get; init; } = default!;
    public decimal BudgetAmount { get; init; }
    public decimal CurrentMonthSpendForBudget { get; init; }
    public bool WithinBudget { get; init; }
}