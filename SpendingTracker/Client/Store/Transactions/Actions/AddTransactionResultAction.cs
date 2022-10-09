using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Actions;

public class AddTransactionResultAction
{
    public bool Success { get; }
    public Transaction? Transaction { get; }

    public AddTransactionResultAction(bool success, Transaction? transaction)
    {
        Success = success;
        Transaction = transaction;
    }
}