using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Transactions;

public partial class TransactionDetail
{
    [Parameter]
    public string Id { get; set; }
    
    public Transaction Transaction { get; set; } = new Transaction();
    public bool IsLoading { get; set; } = false;

    [Inject]
    public ITransactionsService TransactionsService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Transaction = await TransactionsService.GetTransaction(int.Parse(Id));    
        IsLoading = false;
    }
}