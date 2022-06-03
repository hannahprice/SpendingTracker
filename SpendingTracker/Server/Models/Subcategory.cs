namespace SpendingTracker.Server.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public List<Budget>? Budgets { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
