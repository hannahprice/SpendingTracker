using Fluxor;
using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Pages;

public partial class Index
{
    [Inject] private IDispatcher Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new LoadBudgetsAction());
        Dispatcher.Dispatch(new LoadTransactionsAction());
    }
}