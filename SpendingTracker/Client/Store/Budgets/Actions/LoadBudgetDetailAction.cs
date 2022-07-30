namespace SpendingTracker.Client.Store.Budgets.Actions;

public class LoadBudgetDetailAction
{
    public int Id { get; }

    public LoadBudgetDetailAction(int id)
    {
        Id = id;
    }
}