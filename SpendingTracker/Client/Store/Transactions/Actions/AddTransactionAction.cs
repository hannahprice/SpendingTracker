using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Actions;

public class AddTransactionAction
{
    public Transaction Transaction { get; }

    public AddTransactionAction(Transaction transaction)
    {
        Transaction = transaction;
    }
}