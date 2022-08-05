namespace SpendingTracker.Client.Store.Transactions.Actions;

public class DeleteTransactionAction
{
    public int Id { get; }

    public DeleteTransactionAction(int id)
    {
        Id = id;
    }
}