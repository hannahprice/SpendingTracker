using Fluxor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Transactions.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Effects;

public class LoadTransactionsEffects
{
    private readonly ITransactionsService TransactionsService;

    public LoadTransactionsEffects(ITransactionsService transactionsService)
    {
        TransactionsService = transactionsService;
    }

    [EffectMethod]
    public async Task HandleLoadTransactionsAction(LoadTransactionsAction action, IDispatcher dispatcher)
    {
        var transactions = await TransactionsService.GetAllTransactions();
        dispatcher.Dispatch(new LoadTransactionsResultAction(transactions));
    }
}