namespace SpendingTracker.Server.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public bool IsOutwardPayment { get; set; }
        public bool IsReoccurring { get; set; }
        public Frequency ReoccuringFrequency { get; set; }
        public DateOnly DateOfTransaction { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
