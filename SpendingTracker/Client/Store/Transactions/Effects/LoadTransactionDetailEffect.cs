using Fluxor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Store.Transactions.Effects;

public class LoadTransactionDetailEffect
{
    private readonly ITransactionsService TransactionsService;

    public LoadTransactionDetailEffect(ITransactionsService transactionsService)
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