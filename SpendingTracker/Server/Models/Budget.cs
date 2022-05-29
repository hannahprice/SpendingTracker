namespace SpendingTracker.Server.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public Frequency Frequency { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
