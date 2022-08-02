namespace SpendingTracker.Client.Store.Transactions.Actions;

public class LoadTransactionDetailAction
{
    public int Id { get; }

    public LoadTransactionDetailAction(int id)
    {
        Id = id;
    }
}