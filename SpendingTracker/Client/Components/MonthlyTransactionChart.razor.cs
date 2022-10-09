using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Components;

public partial class MonthlyTransactionChart
{
    [Inject] public ITransactionsService TransactionsService { get; set; }
    private bool IsLoading { get; set; } = false;
    private List<Transaction>? AllTransactions { get; set; } = new List<Transaction>();

    private string[] ThisMonthsTransactionsDataLabels { get; set; }
    private double[]? ThisMonthsTransactionsData { get; set; }
    private int Index = -1;
    private string ChartInnerLabel => Index < 0 ? "Total" : ThisMonthsTransactionsDataLabels[Index];
    private string ChartInnerData => Index < 0 ? ThisMonthsTransactionsData?.Sum().ToString("0.00") : ThisMonthsTransactionsData[Index].ToString("0.00");
    
    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        AllTransactions = await TransactionsService.GetAllTransactions();

        GroupThisMonthsTransactionData();

        IsLoading = false;
    }

    private void GroupThisMonthsTransactionData()
    {
        var thisMonthsTransactions = AllTransactions!
            .Where(x =>
                x.DateOfTransaction.Date.Month == DateTime.Now.Date.Month).ToList();

        if (thisMonthsTransactions.Any())
        {
            var transactionsWithCategories = thisMonthsTransactions.Where(x => x.Category != null);

            var groupedTransactions = transactionsWithCategories
                .GroupBy(g => g.Category.Description);
            
            var dataLabels = groupedTransactions.Select(x => x.Key).ToList();
            var data = groupedTransactions
                .Select(x => x.Sum(y => (double)y.Amount))
                .ToList();
            
            var transactionsWithNoCategories =
                thisMonthsTransactions.Where(x => x.Category is null).ToList();

            if (transactionsWithNoCategories.Any())
            {
                var totalSum = transactionsWithNoCategories.Sum(x => x.Amount);
                dataLabels.Add("Uncategorised");
                data.Add((double)totalSum);
            }
            
            ThisMonthsTransactionsDataLabels = dataLabels.ToArray();
            ThisMonthsTransactionsData = data.ToArray();
        }
    }
}