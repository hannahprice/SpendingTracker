namespace SpendingTracker.Client.Store.Budgets.Actions;

public class DeleteBudgetAction
{
    public int Id { get; }

    public DeleteBudgetAction(int id)
    {
        Id = id;
    }
}