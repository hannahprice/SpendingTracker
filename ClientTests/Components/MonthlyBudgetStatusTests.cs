using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using SpendingTracker.Client.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace ClientTests.Components;

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
        tableHeaderCells.Count.Should().Be(3);
    }
    
    [Fact]
    public void WhenMonthlyBudgetButNoTransactionsForThisMonth_DisplaysBudgetStatusRow()
    {
        // Arrange
        SetupSingleBudget(65m);
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(new List<Transaction>());

        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var tableBodyCells = component.FindAll("tbody td");
        tableBodyCells.Count.Should().Be(3);
    }
    
    [Fact]
    public void WhenMonthlyBudgetAndTransactionForThisMonth_DisplaysBudgetCategory()
    {
        // Arrange
        SetupSingleBudget(65m);
        SetupSingleTransaction(15m);
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var categoryCell = component.Find("td:nth-child(1)");
        categoryCell.InnerHtml.Should().Be("Bills");
    }
    
    [Fact]
    public void WhenMonthlyBudgetAndTransactionForThisMonth_DisplaysBudgetAmount()
    {
        // Arrange
        SetupSingleBudget(65m);
        SetupSingleTransaction(15m);
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var amountCell = component.Find("td:nth-child(2)");
        amountCell.InnerHtml.Should().Be("£65");
    }

    [Fact]
    public void WhenMonthlyBudgetAndTransactionForThisMonth_DisplaysTotalSpendTowardsBudget()
    {
        // Arrange
        SetupSingleBudget(65m);
        SetupSingleTransaction(15m);
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var totalSpendCell = component.Find("td:nth-child(3)");
        totalSpendCell.InnerHtml.Should().Contain("£15");
    }

    [Fact]
    public void WhenSpendWithinBudget_DisplaysTickIcon()
    {
        // Arrange
        SetupSingleBudget(65m);
        SetupSingleTransaction(15m);
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var checkIconTitle = component.Find("td:nth-child(3) svg title");
        checkIconTitle.InnerHtml.Should().Be("Check");
    }

    [Fact]
    public void WhenSpendOverBudget_DisplaysWarningIcon()
    {
        // Arrange
        SetupSingleBudget(65m);
        SetupSingleTransaction(75m);
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var checkIconTitle = component.Find("td:nth-child(3) svg title");
        checkIconTitle.InnerHtml.Should().Be("Warning");
    }

    [Fact]
    public void WhenMultipleTransactionsForThisMonth_DisplaysTotalSpendTowardsBudget()
    {
        // Arrange
        SetupSingleBudget(65m);
        
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{
                GetTransaction(15m, 1, "Bills"),
                GetTransaction(20m, 1, "Bills")
            });
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var totalSpendCell = component.Find("td:nth-child(3)");
        totalSpendCell.InnerHtml.Should().Contain("£35");
    }

    [Fact]
    public void WhenMultipleBudgets_DisplaysMultipleBudgetStatuses()
    {
        // Arrange
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>{
                GetBudget(65m,1, "Bills"),
                GetBudget(200m,2, "Shopping")
            });
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{
                GetTransaction(15m, 1, "Bills"),
                GetTransaction(130m, 2, "Shopping")
            });
        
        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();
        
        // Assert
        var tableRows = component.FindAll("tbody tr");
        tableRows.Count.Should().Be(2);
    }

    [Fact]
    public void OnlyDisplaysTransactionsForCurrentMonth()
    {
        // Arrange
        SetupSingleBudget(65m);
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>{
                GetTransaction(15m, 1, "Bills"),
                new Transaction{DateOfTransaction = DateTime.Now.AddMonths(-2), Amount = 120m}
            });

        // Act
        var component = RenderComponent<MonthlyBudgetStatus>();

        // Assert
        var totalSpendCell = component.Find("td:nth-child(3)");
        totalSpendCell.InnerHtml.Should().Contain("£15");
    }
    
    #region TestHelpers

    private static Transaction GetTransaction(decimal amount, int categoryId, string category)
    {
        return new Transaction
        {
            Amount = amount, 
            DateOfTransaction = DateTime.Now, 
            Category = new Category{Id = categoryId, Description = category}
        };
    }
    
    private static Budget GetBudget(decimal amount, int categoryId, string category)
    {
        return new Budget
        {
            Amount = amount, 
            Frequency = Frequency.Monthly,
            Category = new Category{Id = categoryId, Description = category}
        };
    }

    private void SetupSingleBudget(decimal amount)
    {
        _budgetsService.Setup(x => x.GetAllBudgets()).ReturnsAsync(
            new List<Budget>
            {
                GetBudget(amount,1, "Bills")
            });
    }

    private void SetupSingleTransaction(decimal amount)
    {
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>
            {
                new Transaction
                {
                    Amount = amount, 
                    DateOfTransaction = DateTime.Now, 
                    Category = new Category{Id = 1, Description = "Bills"}
                }
            });
    }
    
    #endregion
}