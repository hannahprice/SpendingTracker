using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Components;

public partial class YearlyTransactionChart
{
    [Inject] public ITransactionsService TransactionsService { get; set; }

    private List<Transaction> AllTransactions { get; set; } = new List<Transaction>();
    private bool IsLoading { get; set; } = false;
    private List<ChartSeries> Series { get; set; } = new List<ChartSeries>();
    private string[] xAxisLabels { get; set; } = new string[]{};

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        AllTransactions = await TransactionsService.GetAllTransactions();
        if (AllTransactions.Any())
        {
            GroupThisYearsTransactionData();
        }
        IsLoading = false;
    }

    private void GroupThisYearsTransactionData()
    {
        var chartData = new Dictionary<string, List<double>>();
        var thisYearsTransactions = AllTransactions
            .Where(x =>
                x.DateOfTransaction.Date.Year == DateTime.Now.Date.Year).ToList();
        
        var thisYearsCategories = thisYearsTransactions.Where(x => x.Category != null)
            .Select(x => x.Category.Description).Distinct().ToList();

        foreach (var category in thisYearsCategories)
        {
            chartData.Add(category, new List<double>());
        }

        var (months, monthNames) = GetPastMonthsForThisYear();
        xAxisLabels = monthNames.ToArray();

        foreach (var month in months)
        {
            var transactionsForMonth = thisYearsTransactions.Where(x => x.DateOfTransaction.Month == month).ToList();
            
            var transactionsWithCategories = transactionsForMonth.Where(x => x.Category != null).ToList();
            
            var groupedTransactions = transactionsWithCategories
                .GroupBy(g => g.Category.Description).ToList();
            
            foreach (var chartSeries in chartData)
            {
                var transactionsForThisGroup = groupedTransactions.Where(x => x.Key == chartSeries.Key).ToList();

                if (!transactionsForThisGroup.Any())
                {
                    chartSeries.Value.Add(0);
                }
                else
                {
                    foreach (var group in transactionsForThisGroup)
                    {
                        chartSeries.Value.Add(group.Sum(x => (double)x.Amount));
                    }
                }
            }
        }

        foreach (var chartSeries in chartData)
        {
            Series.Add(new ChartSeries{Name = chartSeries.Key, Data = chartSeries.Value.ToArray()});
        }
    }

    private static (List<int> months, List<string> monthNames) GetPastMonthsForThisYear()
    {
        var months = new List<int>();
        var monthNames = new List<string>();
        
        for (var i = 1; i <= DateTime.Now.Month; i++)
        {
            months.Add(i);
            monthNames.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
        }

        return (months, monthNames);
    }
}