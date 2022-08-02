using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Actions;

public class LoadTransactionDetailResultAction
{
    public Transaction? Transaction { get; }

    public LoadTransactionDetailResultAction(Transaction? transaction)
    {
        Transaction = transaction;
    }
}