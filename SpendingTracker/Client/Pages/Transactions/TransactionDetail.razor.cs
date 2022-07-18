using Microsoft.AspNetCore.Components;
using MudBlazor;
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
    [Inject]
    private ISnackbar Snackbar { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public bool DialogVisible { get; set; } = false;

    public void ToggleDialog() => DialogVisible = !DialogVisible;
    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Transaction = await TransactionsService.GetTransaction(int.Parse(Id));    
        IsLoading = false;
    }

    public async Task DeleteTransaction()
    {
        try
        {
            await TransactionsService.DeleteTransaction(int.Parse(Id));

            ToggleDialog();
            Snackbar.Add($"Transaction removed", Severity.Success);
            NavigationManager.NavigateTo("/transactions", false);
        }
        catch
        {
            Snackbar.Add($"Error removing transaction: {Transaction.Description}", Severity.Error);
        }
    }
}