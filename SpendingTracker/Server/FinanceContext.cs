using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Models;

namespace SpendingTracker.Server
{
    public class FinanceContext : DbContext
    {
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Budget> Budgets => Set<Budget>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\\mssqllocaldb;Database=FinanceTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seedCategories = new List<Category>
            {
                new Category { Id = 1, Description = "House payment" },
                new Category { Id = 2, Description = "Council tax" },
                new Category { Id = 3, Description = "Gas" },
                new Category { Id = 3, Description = "Electric" },
                new Category { Id = 3, Description = "Water" },
                new Category { Id = 3, Description = "Phone" },
                new Category { Id = 3, Description = "Internet" },
                new Category { Id = 3, Description = "Car tax" },
                new Category { Id = 3, Description = "Car insurance" },
                new Category { Id = 3, Description = "Fuel" },
                new Category { Id = 3, Description = "MOT" },
                new Category { Id = 3, Description = "Car service" },
                new Category { Id = 3, Description = "Breakdown cover" },
                new Category { Id = 3, Description = "Phone insurance" },
                new Category { Id = 3, Description = "Travel insurance" },
                new Category { Id = 3, Description = "Eating out" },
                new Category { Id = 3, Description = "Drinks" },
                new Category { Id = 3, Description = "Days out" },
                new Category { Id = 3, Description = "Takeaways" },
                new Category { Id = 3, Description = "Groceries" },
                new Category { Id = 3, Description = "Toiletries" },
                new Category { Id = 3, Description = "Household items" },
                new Category { Id = 3, Description = "Gifts" },
                new Category { Id = 3, Description = "Train tickets" },
                new Category { Id = 3, Description = "Flights" },
                new Category { Id = 3, Description = "Flight extras" },
                new Category { Id = 3, Description = "Taxi fare" },
                new Category { Id = 3, Description = "Holiday accomodation" },
                new Category { Id = 3, Description = "App subscription" },
                new Category { Id = 3, Description = "Charity donation" },
                new Category { Id = 3, Description = "Tv" },
                new Category { Id = 3, Description = "Gym membership" },
                new Category { Id = 3, Description = "Prescriptions" },
                new Category { Id = 3, Description = "Medicine" }
            };

            modelBuilder.Entity<Category>().HasData(seedCategories);
        }
         
    }
}
