namespace SpendingTracker.Client.Store.Transactions.Actions;

public class DeleteTransactionResultAction
{
    public int Id { get; }
    public bool Success { get; }

    public DeleteTransactionResultAction(int id, bool success)
    {
        Id = id;
        Success = success;
    }
}