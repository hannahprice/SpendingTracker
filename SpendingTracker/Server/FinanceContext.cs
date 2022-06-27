using Microsoft.EntityFrameworkCore;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server
{
    public class FinanceContext : DbContext
    {
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Subcategory> Subcategories => Set<Subcategory>();

        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=FinanceTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seedCategories = new List<Category>
            {
                new Category { Id = 1, Description = "Bills"},
                new Category { Id = 2, Description = "Shopping"},
                new Category { Id = 3, Description = "Travel"},
                new Category { Id = 4, Description = "Health"},
                new Category { Id = 5, Description = "Leisure"},
                new Category { Id = 6, Description = "Holidays"},
                new Category { Id = 7, Description = "Miscellaneous"}
            };

            var seedSubcategories = new List<Subcategory>
            {
                // Bills
                new Subcategory { Id = 1, CategoryId = 1, Description = "Rent" },
                new Subcategory { Id = 2, CategoryId = 1, Description = "Mortgage" },
                new Subcategory { Id = 3, CategoryId = 1, Description = "Council tax" },
                new Subcategory { Id = 4, CategoryId = 1, Description = "Gas" },
                new Subcategory { Id = 5, CategoryId = 1, Description = "Electric" },
                new Subcategory { Id = 6, CategoryId = 1, Description = "Water" },
                new Subcategory { Id = 7, CategoryId = 1, Description = "Internet" },
                new Subcategory { Id = 8, CategoryId = 1, Description = "Phone data plan" },
                new Subcategory { Id = 9, CategoryId = 1, Description = "Phone insurance" },
                new Subcategory { Id = 10, CategoryId = 1, Description = "TV subscription" },

                // Shopping
                new Subcategory { Id = 11, CategoryId = 2, Description = "Groceries" },
                new Subcategory { Id = 12, CategoryId = 2, Description = "Toiletries" },
                new Subcategory { Id = 13, CategoryId = 2, Description = "Household items" },
                new Subcategory { Id = 14, CategoryId = 2, Description = "Clothes" },

                // Travel
                new Subcategory { Id = 15, CategoryId = 3, Description = "Fuel" },
                new Subcategory { Id = 16, CategoryId = 3, Description = "MOT" },
                new Subcategory { Id = 17, CategoryId = 3, Description = "Car service" },
                new Subcategory { Id = 18, CategoryId = 3, Description = "Breakdown cover" },
                new Subcategory { Id = 19, CategoryId = 3, Description = "Car tax" },
                new Subcategory { Id = 20, CategoryId = 3, Description = "Car insurance" },
                new Subcategory { Id = 21, CategoryId = 3, Description = "Car repairs" },
                new Subcategory { Id = 22, CategoryId = 3, Description = "Taxi fare" },
                new Subcategory { Id = 23, CategoryId = 3, Description = "Train tickets" },

                // Health
                new Subcategory { Id = 24, CategoryId = 4, Description = "Gym membership" },
                new Subcategory { Id = 25, CategoryId = 4, Description = "Medicine" },
                new Subcategory { Id = 26, CategoryId = 4, Description = "Prescriptions" },

                // Leisure
                new Subcategory { Id = 27, CategoryId = 5, Description = "Dining out" },
                new Subcategory { Id = 28, CategoryId = 5, Description = "Drinks" },
                new Subcategory { Id = 29, CategoryId = 5, Description = "Days out" },
                new Subcategory { Id = 30, CategoryId = 5, Description = "Takeaways" },
                new Subcategory { Id = 31, CategoryId = 5, Description = "Games" },
                new Subcategory { Id = 32, CategoryId = 5, Description = "Books" },
                new Subcategory { Id = 33, CategoryId = 5, Description = "Craft supplies" },

                // Holidays
                new Subcategory { Id = 34, CategoryId = 6, Description = "Accommodation" },
                new Subcategory { Id = 35, CategoryId = 6, Description = "Flights" },
                new Subcategory { Id = 36, CategoryId = 6, Description = "Airport parking" },
                new Subcategory { Id = 37, CategoryId = 6, Description = "Transfers" },
                new Subcategory { Id = 38, CategoryId = 6, Description = "Travel insurance" },
                new Subcategory { Id = 39, CategoryId = 6, Description = "Holiday activities" },

                // Miscellaneous 
                new Subcategory { Id = 40, CategoryId = 7, Description = "Gifts" },
                new Subcategory { Id = 41, CategoryId = 7, Description = "Charity donations" },
                new Subcategory { Id = 42, CategoryId = 7, Description = "App subscriptions" },
                new Subcategory { Id = 43, CategoryId = 7, Description = "Miscellaneous purchases" }
            };            

            modelBuilder.Entity<Category>().HasData(seedCategories);
            modelBuilder.Entity<Subcategory>().HasData(seedSubcategories);

            modelBuilder.Entity<Budget>()
                .Property(b => b.Amount)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Transaction>()
                .Property(b => b.Amount)
                .HasPrecision(7, 2);
        }
    }
}
