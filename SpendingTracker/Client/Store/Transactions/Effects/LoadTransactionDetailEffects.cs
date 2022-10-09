using Fluxor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Transactions.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Effects;

public class LoadTransactionDetailEffects
{
    private readonly ITransactionsService TransactionsService;

    public LoadTransactionDetailEffects(ITransactionsService transactionsService)
    {
        TransactionsService = transactionsService;
    }
    
    [EffectMethod]
    public async Task HandleLoadTransactionDetailAction(LoadTransactionDetailAction action, IDispatcher dispatcher)
    {
        var transaction = await TransactionsService.GetTransaction(action.Id);
        dispatcher.Dispatch(new LoadTransactionDetailResultAction(transaction));
    }
}