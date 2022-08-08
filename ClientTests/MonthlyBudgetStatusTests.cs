using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SpendingTracker.Client.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace ClientTests;

public class MonthlyBudgetStatusTests : TestContext
{
    private readonly Mock<IBudgetsService> _budgetsService;
    private readonly Mock<ITransactionsService> _transactionsService;

    public MonthlyBudgetStatusTests()
    {
        _budgetsService = new Mock<IBudgetsService>();
        _transactionsService = new Mock<ITransactionsService>();

        Services.AddSingleton(_budgetsService.Object);
        Services.AddSingleton(_transactionsService.Object);
    }

    [Fact]
    public void WhenNoBudgetsOrTransactions_DisplaysTableHeadersAndNoTableRows()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(new List<Budget>());
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(new List<Transaction>());
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();
        
        // Assert
        var tableBody = component.Find("tbody");
        tableBody.MarkupMatches(@"<tbody class=""mud-table-body""></tbody>");
    }
    
    [Fact]
    public void WhenBudgetsExistForOtherFrequencies_DisplaysTableHeadersAndNoTableRows()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(new List<Budget>{new Budget{Amount = 77, Frequency = Frequency.Weekly}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(new List<Transaction>());
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();
        
        // Assert
        var tableBody = component.Find("tbody");
        tableBody.MarkupMatches(@"<tbody class=""mud-table-body""></tbody>");
    }

    [Fact]
    public void WhenNoBudgetsOrTransactions_DisplaysHeaderCells()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(new List<Budget>());
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(new List<Transaction>());
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();
        
        // Assert
        var tableHeaderCells = component.FindAll("th");
        tableHeaderCells.Count.Should().Be(4);
    }
    
    [Fact]
    public void WhenMonthlyBudgetButNoTransactionsForThisMonth_DisplaysBudgetStatusRow()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{new Budget{Id = 23, Amount = 65m, Frequency = Frequency.Monthly, Categories = 
                new List<Category>{new Category{Description = "Bills"}}}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(new List<Transaction>());

        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var tableBodyCells = component.FindAll("tbody td");
        tableBodyCells.Count.Should().Be(4);
    }
    
    [Fact]
    public void WhenMonthlyBudgetAndTransactionForThisMonth_DisplaysBudgetCategory()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{new Budget{Id = 23, Amount = 65m, Frequency = Frequency.Monthly, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}}}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{new Transaction{Amount = 15m, DateOfTransaction = DateTime.Now, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}} }});
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var categoryCell = component.Find("td:nth-child(1)");
        categoryCell.MarkupMatches(@"<td class=""mud-table-cell"">Bills</td>");
    }
    
    [Fact]
    public void WhenMonthlyBudgetAndTransactionForThisMonth_DisplaysBudgetAmount()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{new Budget{Id = 23, Amount = 65m, Frequency = Frequency.Monthly, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}}}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{new Transaction{Amount = 15m, DateOfTransaction = DateTime.Now, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}} }});
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var amountCell = component.Find("td:nth-child(2)");
        amountCell.MarkupMatches(@"<td class=""mud-table-cell"">£65</td>");
    }

    [Fact]
    public void WhenMonthlyBudgetAndTransactionForThisMonth_DisplaysTotalSpendTowardsBudget()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{new Budget{Id = 23, Amount = 65m, Frequency = Frequency.Monthly, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}}}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{new Transaction{Amount = 15m, DateOfTransaction = DateTime.Now, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}} }});
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var totalSpendCell = component.Find("td:nth-child(3)");
        totalSpendCell.MarkupMatches(@"<td class=""mud-table-cell"">£15</td>");
    }

    [Fact]
    public void WhenSpendWithinBudget_DisplaysTickIcon()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{new Budget{Id = 23, Amount = 65m, Frequency = Frequency.Monthly, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}}}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{new Transaction{Amount = 15m, DateOfTransaction = DateTime.Now, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}} }});
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var checkIconTitle = component.Find("td:nth-child(4) svg title");
        checkIconTitle.InnerHtml.Should().Be("Check");
    }

    [Fact]
    public void WhenSpendOverBudget_DisplaysWarningIcon()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{new Budget{Id = 23, Amount = 65m, Frequency = Frequency.Monthly, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}}}});
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{new Transaction{Amount = 75m, DateOfTransaction = DateTime.Now, 
                Categories = new List<Category>{new Category{Id = 1, Description = "Bills"}} }});
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var checkIconTitle = component.Find("td:nth-child(4) svg title");
        checkIconTitle.InnerHtml.Should().Be("Warning");
    }
}