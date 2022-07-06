using SpendingTracker.Server;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

public static class Utilities
{
    public static void InitializeDbForTests(FinanceContext db)
    {
        db.Transactions.Add((new Transaction() { Id = 1, Amount = 23.99m, Description = "Card payment to Sainburys", DateOfTransaction = DateTime.Now, IsReoccurring = false, IsOutwardPayment = true }));
        db.SaveChanges();
    }
}