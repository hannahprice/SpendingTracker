﻿namespace SpendingTracker.Shared.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public Frequency Frequency { get; set; }
        public Category? Category { get; set; }
        public List<Subcategory>? Subcategories { get; set; }
    }
}
