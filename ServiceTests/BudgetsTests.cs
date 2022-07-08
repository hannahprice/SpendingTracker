using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;
using Xunit;

namespace ServiceTests;

public class BudgetsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _appFactory;
    private readonly HttpClient _httpClient;

    public BudgetsTests(TestWebApplicationFactory<Program> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task GetBudgetsSuccess()
    {
        var addBudgetTask1 = InsertTestBudget(1);
        var addBudgetTask2 = InsertTestBudget(2);
        var addBudgetTask3 = InsertTestBudget(3);
        await Task.WhenAll(addBudgetTask1, addBudgetTask2, addBudgetTask3);

        var response = await _httpClient.GetFromJsonAsync<List<Budget>>("api/Budgets");
        
        response.Should().NotBeNullOrEmpty();
        response.Should().BeInDescendingOrder(c => c.Id);
        response.Should().AllSatisfy(c =>
        {
            c.Categories.Should().NotBeNullOrEmpty();
            c.Subcategories.Should().NotBeNullOrEmpty();
        });
        
        var dbContext = Utilities.GetDbContext(_appFactory);

        var removeBudgetTask1 = RemoveAddedBudget(dbContext!,1);
        var removeBudgetTask2 = RemoveAddedBudget(dbContext!,2);
        var removeBudgetTask3 = RemoveAddedBudget(dbContext!,3);
        await Task.WhenAll(removeBudgetTask1, removeBudgetTask2, removeBudgetTask3);
    }

    [Fact]
    public async Task AddBudgetSuccess()
    {
        var budget = new Budget()
        {
            Id = 4, Amount = 50.00m, Frequency = Frequency.Weekly,
            Categories = new List<Category> { new Category { Id = 1 } },
            Subcategories = new List<Subcategory> {new Subcategory{Id = 1}}
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/Budgets", budget);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var newBudgetId = int.Parse(responseContent);
        
        var dbContext = Utilities.GetDbContext(_appFactory);
        dbContext!.Budgets.Should().Contain(c => c.Id == newBudgetId);

        await RemoveAddedBudget(dbContext, 4);
    }

    [Fact]
    public async Task GetBudgetSuccess()
    {
        await InsertTestBudget(5);

        var response = await _httpClient.GetFromJsonAsync<Budget>("api/Budgets/5");
        response.Should().NotBeNull();
        response!.Id.Should().Be(5);
        
        var dbContext = Utilities.GetDbContext(_appFactory);

        await RemoveAddedBudget(dbContext!, 5);
    }

    private async Task InsertTestBudget(int id)
    {
        var budget = new Budget()
        {
            Id = id, Amount = 12.00m, Frequency = Frequency.Weekly,
            Categories = new List<Category> { new Category { Id = 1 } },
            Subcategories = new List<Subcategory> {new Subcategory{Id = 1}}
        };
        var response = await _httpClient.PostAsJsonAsync("api/Budgets", budget);
        response.EnsureSuccessStatusCode();
    }

    private async Task RemoveAddedBudget(FinanceContext dbContext, int addedBudgetId)
    {
        dbContext.Budgets.Remove(dbContext.Budgets.Single(c => c.Id == addedBudgetId));
        await dbContext.SaveChangesAsync();
    }
}