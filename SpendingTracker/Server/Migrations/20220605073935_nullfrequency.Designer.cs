﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpendingTracker.Server;

#nullable disable

namespace SpendingTracker.Server.Migrations
{
    [DbContext(typeof(FinanceContext))]
    [Migration("20220605073935_nullfrequency")]
    partial class nullfrequency
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BudgetCategory", b =>
                {
                    b.Property<int>("BudgetsId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.HasKey("BudgetsId", "CategoriesId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("BudgetCategory");
                });

            modelBuilder.Entity("BudgetSubcategory", b =>
                {
                    b.Property<int>("BudgetsId")
                        .HasColumnType("int");

                    b.Property<int>("SubcategoriesId")
                        .HasColumnType("int");

                    b.HasKey("BudgetsId", "SubcategoriesId");

                    b.HasIndex("SubcategoriesId");

                    b.ToTable("BudgetSubcategory");
                });

            modelBuilder.Entity("CategoryTransaction", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "TransactionsId");

                    b.HasIndex("TransactionsId");

                    b.ToTable("CategoryTransaction");
                });

            modelBuilder.Entity("SpendingTracker.Server.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("SpendingTracker.Server.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Bills"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Shopping"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Travel"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Health"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Leisure"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Holidays"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Miscellaneous"
                        });
                });

            modelBuilder.Entity("SpendingTracker.Server.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Rent"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Mortgage"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Council tax"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Gas"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            Description = "Electric"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            Description = "Water"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 1,
                            Description = "Internet"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 1,
                            Description = "Phone data plan"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 1,
                            Description = "Phone insurance"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 1,
                            Description = "TV subscription"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 2,
                            Description = "Groceries"
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 2,
                            Description = "Toiletries"
                        },
                        new
                        {
                            Id = 13,
                            CategoryId = 2,
                            Description = "Household items"
                        },
                        new
                        {
                            Id = 14,
                            CategoryId = 2,
                            Description = "Clothes"
                        },
                        new
                        {
                            Id = 15,
                            CategoryId = 3,
                            Description = "Fuel"
                        },
                        new
                        {
                            Id = 16,
                            CategoryId = 3,
                            Description = "MOT"
                        },
                        new
                        {
                            Id = 17,
                            CategoryId = 3,
                            Description = "Car service"
                        },
                        new
                        {
                            Id = 18,
                            CategoryId = 3,
                            Description = "Breakdown cover"
                        },
                        new
                        {
                            Id = 19,
                            CategoryId = 3,
                            Description = "Car tax"
                        },
                        new
                        {
                            Id = 20,
                            CategoryId = 3,
                            Description = "Car insurance"
                        },
                        new
                        {
                            Id = 21,
                            CategoryId = 3,
                            Description = "Car repairs"
                        },
                        new
                        {
                            Id = 22,
                            CategoryId = 3,
                            Description = "Taxi fare"
                        },
                        new
                        {
                            Id = 23,
                            CategoryId = 3,
                            Description = "Train tickets"
                        },
                        new
                        {
                            Id = 24,
                            CategoryId = 4,
                            Description = "Gym membership"
                        },
                        new
                        {
                            Id = 25,
                            CategoryId = 4,
                            Description = "Medicine"
                        },
                        new
                        {
                            Id = 26,
                            CategoryId = 4,
                            Description = "Prescriptions"
                        },
                        new
                        {
                            Id = 27,
                            CategoryId = 5,
                            Description = "Dining out"
                        },
                        new
                        {
                            Id = 28,
                            CategoryId = 5,
                            Description = "Drinks"
                        },
                        new
                        {
                            Id = 29,
                            CategoryId = 5,
                            Description = "Days out"
                        },
                        new
                        {
                            Id = 30,
                            CategoryId = 5,
                            Description = "Takeaways"
                        },
                        new
                        {
                            Id = 31,
                            CategoryId = 5,
                            Description = "Games"
                        },
                        new
                        {
                            Id = 32,
                            CategoryId = 5,
                            Description = "Books"
                        },
                        new
                        {
                            Id = 33,
                            CategoryId = 5,
                            Description = "Craft supplies"
                        },
                        new
                        {
                            Id = 34,
                            CategoryId = 6,
                            Description = "Accommodation"
                        },
                        new
                        {
                            Id = 35,
                            CategoryId = 6,
                            Description = "Flights"
                        },
                        new
                        {
                            Id = 36,
                            CategoryId = 6,
                            Description = "Airport parking"
                        },
                        new
                        {
                            Id = 37,
                            CategoryId = 6,
                            Description = "Transfers"
                        },
                        new
                        {
                            Id = 38,
                            CategoryId = 6,
                            Description = "Travel insurance"
                        },
                        new
                        {
                            Id = 39,
                            CategoryId = 6,
                            Description = "Holiday activities"
                        },
                        new
                        {
                            Id = 40,
                            CategoryId = 7,
                            Description = "Gifts"
                        },
                        new
                        {
                            Id = 41,
                            CategoryId = 7,
                            Description = "Charity donations"
                        },
                        new
                        {
                            Id = 42,
                            CategoryId = 7,
                            Description = "App subscriptions"
                        },
                        new
                        {
                            Id = 43,
                            CategoryId = 7,
                            Description = "Miscellaneous purchases"
                        });
                });

            modelBuilder.Entity("SpendingTracker.Server.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<DateTime>("DateOfTransaction")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOutwardPayment")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReoccurring")
                        .HasColumnType("bit");

                    b.Property<int?>("ReoccuringFrequency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("SubcategoryTransaction", b =>
                {
                    b.Property<int>("SubcategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionsId")
                        .HasColumnType("int");

                    b.HasKey("SubcategoriesId", "TransactionsId");

                    b.HasIndex("TransactionsId");

                    b.ToTable("SubcategoryTransaction");
                });

            modelBuilder.Entity("BudgetCategory", b =>
                {
                    b.HasOne("SpendingTracker.Server.Models.Budget", null)
                        .WithMany()
                        .HasForeignKey("BudgetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpendingTracker.Server.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BudgetSubcategory", b =>
                {
                    b.HasOne("SpendingTracker.Server.Models.Budget", null)
                        .WithMany()
                        .HasForeignKey("BudgetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpendingTracker.Server.Models.Subcategory", null)
                        .WithMany()
                        .HasForeignKey("SubcategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryTransaction", b =>
                {
                    b.HasOne("SpendingTracker.Server.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpendingTracker.Server.Models.Transaction", null)
                        .WithMany()
                        .HasForeignKey("TransactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpendingTracker.Server.Models.Subcategory", b =>
                {
                    b.HasOne("SpendingTracker.Server.Models.Category", null)
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubcategoryTransaction", b =>
                {
                    b.HasOne("SpendingTracker.Server.Models.Subcategory", null)
                        .WithMany()
                        .HasForeignKey("SubcategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpendingTracker.Server.Models.Transaction", null)
                        .WithMany()
                        .HasForeignKey("TransactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpendingTracker.Server.Models.Category", b =>
                {
                    b.Navigation("Subcategories");
                });
#pragma warning restore 612, 618
        }
    }
}
