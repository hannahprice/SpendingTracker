using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

public class BudgetsTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _appFactory;
    private readonly HttpClient _httpClient;
    
    public BudgetsTests(TestWebApplicationFactory appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task GetBudgetsSuccess()
    {
        var newBudgetId1 = await InsertTestBudget();
        var newBudgetId2 = await InsertTestBudget();

        var response = await _httpClient.GetFromJsonAsync<List<Budget>>("api/Budgets");
        
        response.Should().NotBeNullOrEmpty();
        response.Should().BeInDescendingOrder(c => c.Id);

        await Utilities.RemoveBudgets(_appFactory, new int[]{newBudgetId1, newBudgetId2});
    }

    [Fact]
    public async Task AddBudgetSuccess()
    {
        var budget = new Budget()
        {
            Amount = 50.00m, Frequency = Frequency.Weekly,
            Category = new Category{Id = 1,},
            Subcategories = new List<Subcategory> {new Subcategory{Id = 1}}
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/Budgets", budget);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var newBudgetId = int.Parse(responseContent);
        
        var budgets = await Utilities.GetBudgets(_appFactory);

        budgets.Should().Contain(c => c.Id == newBudgetId);
        await Utilities.RemoveBudget(_appFactory, newBudgetId);
    }

    [Fact]
    public async Task GetBudgetSuccess()
    {
        var newBudgetId = await InsertTestBudget();

        var response = await _httpClient.GetFromJsonAsync<Budget>($"api/Budgets/{newBudgetId}");
        response.Should().NotBeNull();
        response!.Id.Should().Be(newBudgetId);
        
        await Utilities.RemoveBudget(_appFactory, newBudgetId);
    }

    [Fact]
    public async Task DeleteBudgetSuccess()
    {
        var newBudgetId = await InsertTestBudget();

        var response = await _httpClient.DeleteAsync($"api/Budgets/{newBudgetId}");
        response.EnsureSuccessStatusCode();
        
        var budgets = await Utilities.GetBudgets(_appFactory);
        budgets.Should().NotContain(c => c.Id == newBudgetId);
    }
    
    #region TestHelpers

    private async Task<int> InsertTestBudget()
    {
        var budget = new Budget()
        {
            Amount = 12.00m, Frequency = Frequency.Weekly,
            Category = new Category{Id = 1},
            Subcategories = new List<Subcategory> {new Subcategory{Id = 1}}
        };
        var response = await _httpClient.PostAsJsonAsync("api/Budgets", budget);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return int.Parse(responseContent);
    }

    #endregion
}