namespace SpendingTracker.Server.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Transaction>? Transactions { get; set; }
        public List<Budget>? Budgets { get; set; }
    }
}
