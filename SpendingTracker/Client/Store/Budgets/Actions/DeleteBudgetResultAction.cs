namespace SpendingTracker.Client.Store.Budgets.Actions;

public class DeleteBudgetResultAction
{
    public int Id { get; }
    public bool Success { get; }

    public DeleteBudgetResultAction(int id, bool success)
    {
        Id = id;
        Success = success;
    }
}