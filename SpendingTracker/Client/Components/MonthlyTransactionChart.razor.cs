using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Components;

public partial class MonthlyTransactionChart
{
    public bool IsLoading { get; set; } = false;
    public List<Transaction> AllTransactions { get; set; } = new List<Transaction>();

    public string[] ThisMonthsTransactionsDataLabels { get; set; }
    public double[] ThisMonthsTransactionsData { get; set; }
    public int Index = -1;
    public string ChartInnerLabel => Index < 0 ? "Total" : ThisMonthsTransactionsDataLabels[Index];
    public string ChartInnerData => Index < 0 ? ThisMonthsTransactionsData?.Sum().ToString("0.00") : ThisMonthsTransactionsData[Index].ToString("0.00");

    [Inject] public ITransactionsService TransactionsService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        AllTransactions = await TransactionsService.GetAllTransactions();

        GroupThisMonthsTransactionData();

        IsLoading = false;
    }

    private void GroupThisMonthsTransactionData()
    {
        var thisMonthsTransactions = AllTransactions
            .Where(x =>
                x.DateOfTransaction.Date.Month == DateTime.Now.Date.Month).ToList();

        if (thisMonthsTransactions.Any())
        {
            var transactionsWithCategories = thisMonthsTransactions.Where(x =>
                x.Categories != null &&
                x.Categories.Count == 1);

            var groupedTransactions = transactionsWithCategories
                .GroupBy(g => g.Categories!.First().Description);

            var dataLabels = groupedTransactions.Select(x => x.Key).ToList();
            var data = groupedTransactions
                .Select(x => x.Sum(y => (double)y.Amount))
                .ToList();

            var transactionsWithNoCategories =
                thisMonthsTransactions.Where(x => x.Categories is null || !x.Categories.Any()).ToList();

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