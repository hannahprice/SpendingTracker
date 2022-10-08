using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

[UsesVerify]
public class CategoriesTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _appFactory;
    private readonly HttpClient _httpClient;
    private readonly VerifySettings _verifySettings;

    public CategoriesTests(TestWebApplicationFactory appFactory)
    {
        _verifySettings = new VerifySettings();
        _verifySettings.UseDirectory("./Snapshots/Categories");
        
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }
    
    [Fact]
    public async Task DatabaseIsSeededWithExpectedCategories()
    {
        // var dbContext = Utilities.GetDbContext(_appFactory);

        // dbContext.Should().NotBeNull();
        var categories = await Utilities.GetCategories(_appFactory);
        categories.Should().NotBeNullOrEmpty();
        var categoryNames = categories.Select(c => c.Description);

        await Verify(categoryNames, _verifySettings);
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

        await Verify(response, _verifySettings);
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
        
        // var dbContext = Utilities.GetDbContext(_appFactory);
        var categories = await Utilities.GetCategories(_appFactory);

        categories.Should().Contain(c => c.Id == newCategoryId);
        await Utilities.RemoveCategory(_appFactory, newCategoryId);
        // await RemoveAddedCategory(dbContext, newCategoryId);
    }

    [Fact]
    public async Task GetCategorySuccess()
    {
        var response = await _httpClient.GetFromJsonAsync<Category>($"api/Categories/2");

        response.Should().NotBeNull();
        await Verify(response, _verifySettings);
    }

    [Fact]
    public async Task DeleteCategorySuccess()
    {
        var category = new Category
        {
            Id = 9,
            Description = "CategoryToBeDeleted"
        };
        
        var addResponse = await _httpClient.PostAsJsonAsync("api/Categories", category);
        addResponse.EnsureSuccessStatusCode();

        var response = await _httpClient.DeleteAsync($"api/Categories/{category.Id}");
        response.EnsureSuccessStatusCode();
        
        var categories = await Utilities.GetCategories(_appFactory);
        categories.Should().NotContain(c => c.Id == 9);
    }
    
    // private async Task RemoveAddedCategory(FinanceContext dbContext, int addedCategoryId)
    // {
    //     dbContext.Categories.Remove(dbContext.Categories.Single(c => c.Id == addedCategoryId));
    //     await dbContext.SaveChangesAsync();
    // }
}