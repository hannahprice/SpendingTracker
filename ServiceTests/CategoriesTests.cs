using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;
using Xunit;

namespace ServiceTests;

public class CategoriesTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _appFactory;
    private readonly HttpClient _httpClient;

    public CategoriesTests(TestWebApplicationFactory<Program> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }
    
    [Fact]
    public void DatabaseIsSeededWithExpectedCategories()
    {
        var expectedCategoryNames = new List<string>
        {
            "Bills", "Shopping", "Travel", "Health", "Leisure", "Holidays", "Miscellaneous"
        };
            
        var dbContext = Utilities.GetDbContext(_appFactory);

        dbContext.Should().NotBeNull();
        dbContext!.Categories.Should().NotBeNullOrEmpty();
        dbContext.Categories.Select(c => c.Description).Should().BeEquivalentTo(expectedCategoryNames);
    }

    [Fact]
    public async Task GetCategoriesSuccess()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Category>>("api/Categories");
        
        response.Should().NotBeNullOrEmpty();
        response.Should().BeInDescendingOrder(c => c.Id);
        response.Should().AllSatisfy(c =>
        {
            c.Subcategories.Should().NotBeNullOrEmpty();
        });
    }

    [Fact]
    public async Task AddCategorySuccess()
    {
        var newCategory = new Category
        {
            Description = "TestCategory"
        };

        var response = await _httpClient.PostAsJsonAsync("api/Categories", newCategory);

        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var newCategoryId = int.Parse(responseContent);
        
        var dbContext = Utilities.GetDbContext(_appFactory);
        dbContext!.Categories.Should().Contain(c => c.Id == newCategoryId);

        await RemoveAddedCategory(dbContext, newCategoryId);
    }

    private async Task RemoveAddedCategory(FinanceContext dbContext, int addedCategoryId)
    {
        dbContext.Categories.Remove(dbContext.Categories.Single(c => c.Id == addedCategoryId));
        await dbContext.SaveChangesAsync();
    }
}