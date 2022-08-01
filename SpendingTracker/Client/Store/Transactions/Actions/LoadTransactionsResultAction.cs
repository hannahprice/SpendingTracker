using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Actions;

public class LoadTransactionsResultAction
{
    public IEnumerable<Transaction>? Transactions { get; }

    public LoadTransactionsResultAction(IEnumerable<Transaction>? transactions)
    {
        Transactions = transactions;
    }
}