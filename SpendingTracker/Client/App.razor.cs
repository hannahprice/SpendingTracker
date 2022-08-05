using Fluxor;
using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client;

public partial class App
{
    [Inject] private IDispatcher Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new LoadBudgetsAction());
        Dispatcher.Dispatch(new LoadTransactionsAction());
    }
}