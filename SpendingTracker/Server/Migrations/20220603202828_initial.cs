using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendingTracker.Server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOutwardPayment = table.Column<bool>(type: "bit", nullable: false),
                    IsReoccurring = table.Column<bool>(type: "bit", nullable: false),
                    ReoccuringFrequency = table.Column<int>(type: "int", nullable: false),
                    DateOfTransaction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetCategory",
                columns: table => new
                {
                    BudgetsId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCategory", x => new { x.BudgetsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BudgetCategory_Budgets_BudgetsId",
                        column: x => x.BudgetsId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTransaction",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    TransactionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTransaction", x => new { x.CategoriesId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_CategoryTransaction_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTransaction_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetSubcategory",
                columns: table => new
                {
                    BudgetsId = table.Column<int>(type: "int", nullable: false),
                    SubcategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetSubcategory", x => new { x.BudgetsId, x.SubcategoriesId });
                    table.ForeignKey(
                        name: "FK_BudgetSubcategory_Budgets_BudgetsId",
                        column: x => x.BudgetsId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetSubcategory_Subcategories_SubcategoriesId",
                        column: x => x.SubcategoriesId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoryTransaction",
                columns: table => new
                {
                    SubcategoriesId = table.Column<int>(type: "int", nullable: false),
                    TransactionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoryTransaction", x => new { x.SubcategoriesId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_SubcategoryTransaction_Subcategories_SubcategoriesId",
                        column: x => x.SubcategoriesId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubcategoryTransaction_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Bills" },
                    { 2, "Shopping" },
                    { 3, "Travel" },
                    { 4, "Health" },
                    { 5, "Leisure" },
                    { 6, "Holidays" },
                    { 7, "Miscellaneous" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Description" },
                values: new object[,]
                {
                    { 1, 1, "Rent" },
                    { 2, 1, "Mortgage" },
                    { 3, 1, "Council tax" },
                    { 4, 1, "Gas" },
                    { 5, 1, "Electric" },
                    { 6, 1, "Water" },
                    { 7, 1, "Internet" },
                    { 8, 1, "Phone data plan" },
                    { 9, 1, "Phone insurance" },
                    { 10, 1, "TV subscription" },
                    { 11, 2, "Groceries" },
                    { 12, 2, "Toiletries" },
                    { 13, 2, "Household items" },
                    { 14, 2, "Clothes" },
                    { 15, 3, "Fuel" },
                    { 16, 3, "MOT" },
                    { 17, 3, "Car service" },
                    { 18, 3, "Breakdown cover" },
                    { 19, 3, "Car tax" },
                    { 20, 3, "Car insurance" },
                    { 21, 3, "Car repairs" },
                    { 22, 3, "Taxi fare" },
                    { 23, 3, "Train tickets" },
                    { 24, 4, "Gym membership" },
                    { 25, 4, "Medicine" },
                    { 26, 4, "Prescriptions" },
                    { 27, 5, "Dining out" },
                    { 28, 5, "Drinks" },
                    { 29, 5, "Days out" },
                    { 30, 5, "Takeaways" },
                    { 31, 5, "Games" },
                    { 32, 5, "Books" },
                    { 33, 5, "Craft supplies" },
                    { 34, 6, "Accommodation" },
                    { 35, 6, "Flights" },
                    { 36, 6, "Airport parking" },
                    { 37, 6, "Transfers" },
                    { 38, 6, "Travel insurance" },
                    { 39, 6, "Holiday activities" },
                    { 40, 7, "Gifts" },
                    { 41, 7, "Charity donations" },
                    { 42, 7, "App subscriptions" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Description" },
                values: new object[] { 43, 7, "Miscellaneous purchases" });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_CategoriesId",
                table: "BudgetCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetSubcategory_SubcategoriesId",
                table: "BudgetSubcategory",
                column: "SubcategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTransaction_TransactionsId",
                table: "CategoryTransaction",
                column: "TransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcategoryTransaction_TransactionsId",
                table: "SubcategoryTransaction",
                column: "TransactionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetCategory");

            migrationBuilder.DropTable(
                name: "BudgetSubcategory");

            migrationBuilder.DropTable(
                name: "CategoryTransaction");

            migrationBuilder.DropTable(
                name: "SubcategoryTransaction");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
