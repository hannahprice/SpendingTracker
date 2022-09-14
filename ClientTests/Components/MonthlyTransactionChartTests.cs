using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SpendingTracker.Client.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace ClientTests.Components;

public class MonthlyTransactionChartTests : TestContext
{
    private readonly Mock<ITransactionsService> _transactionsService;
    
    public MonthlyTransactionChartTests()
    {
        _transactionsService = new Mock<ITransactionsService>();

        Services.AddSingleton(_transactionsService.Object);
    }

    [Fact]
    public void WhenNoTransactionsForThisMonth_DisplaysMessage()
    {
        // Arrange
        SetupSingleTransactionForLastMonth();
        
        // Act
        var component = RenderComponent<MonthlyTransactionChart>();
        
        // Assert
        var message = component.Find("span");
        message.InnerHtml.Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public void WhenTransactionsForThisMonthButNoCategories_DisplaysMessage()
    {
        // Arrange
        SetupSingleTransactionWithNoCategory();
        
        // Act
        var component = RenderComponent<MonthlyTransactionChart>();
        
        // Assert
        var message = component.Find("span");
        message.InnerHtml.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void WhenTransactionsForThisMonth_DisplaysTotalSpendInChart()
    {
        // Arrange
        SetupSingleTransaction(30.99m);
        
        // Act
        var component = RenderComponent<MonthlyTransactionChart>();

        // Assert
        var chartText = component.FindAll(".donut-inner-text");
        chartText[0].InnerHtml.Should().Be("Total");
        chartText[1].InnerHtml.Trim().Should().Be("£30.99");
    }

    [Fact]
    public void WhenMultipleTransactionsForThisMonth_DisplaysTotalSpendInChart()
    {
        // Arrange
        SetupMultipleTransactions(10.00m, 40.00m);
        
        // Act
        var component = RenderComponent<MonthlyTransactionChart>();

        // Assert
        var chartText = component.FindAll(".donut-inner-text");
        chartText[0].InnerHtml.Should().Be("Total");
        chartText[1].InnerHtml.Trim().Should().Be("£50.00");
    }

    [Fact]
    public void WhenTransactionsForThisMonth_DisplaysChartLegend()
    {
        // Arrange
        SetupSingleTransaction(30.99m);
        
        // Act
        var component = RenderComponent<MonthlyTransactionChart>();

        // Assert
        var legend = component.Find(".mud-chart-legend p");
        legend.InnerHtml.Should().Be("Bills");
    }

    [Fact]
    public void WhenChartSectionClicked_DisplaysTotalOfClickedSection()
    {
        // Arrange
        SetupTransactionsForDifferentCategories(10.00m, 40.00m);
        var component = RenderComponent<MonthlyTransactionChart>();
        var chartSection = component.FindAll("svg circle.mud-donut-segment");
        
        // Act
        chartSection[0].Click();

        // Assert
        var chartText = component.FindAll(".donut-inner-text");
        chartText[1].InnerHtml.Trim().Should().Be("£10.00");
    }
    
    #region TestHelpers
    
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
    
    private void SetupSingleTransactionWithNoCategory()
    {
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>
            {
                new Transaction
                {
                    Amount = 44.99m, 
                    DateOfTransaction = DateTime.Now, 
                    Category = null
                }
            });
    }
    
    private void SetupSingleTransactionForLastMonth()
    {
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(
            new List<Transaction>
            {
                new Transaction
                {
                    Amount = 10.55m, 
                    DateOfTransaction = DateTime.Now.AddMonths(-1), 
                    Category = new Category{Id = 1, Description = "Bills"}
                }
            });
    }

    private void SetupMultipleTransactions(params decimal[] amounts)
    {
        var transactions = new List<Transaction>(amounts.Length);
        transactions.AddRange(amounts.Select(amount => 
            new Transaction { Amount = amount, DateOfTransaction = DateTime.Now, Category = new Category { Id = 1, Description = "Bills" } }));

        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(transactions);
    }

    private void SetupTransactionsForDifferentCategories(decimal firstCategoryAmount, decimal secondCategoryAmount)
    {
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                Amount = firstCategoryAmount, 
                DateOfTransaction = DateTime.Now, 
                Category = new Category{Id = 1, Description = "Bills"}
            },
            new Transaction
            {
                Amount = secondCategoryAmount, 
                DateOfTransaction = DateTime.Now, 
                Category = new Category{Id = 2, Description = "Shopping"}
            }
        };
        
        _transactionsService.Setup(x => x.GetAllTransactions()).ReturnsAsync(transactions);
    }

    #endregion
}