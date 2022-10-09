using System.Net.Http.Json;
using FluentAssertions;
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
        
        var categories = await Utilities.GetCategories(_appFactory);

        categories.Should().Contain(c => c.Id == newCategoryId);
        await Utilities.RemoveCategory(_appFactory, newCategoryId);
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
            Description = "CategoryToBeDeleted"
        };
        
        var addResponse = await _httpClient.PostAsJsonAsync("api/Categories", category);
        addResponse.EnsureSuccessStatusCode();
        
        var responseContent = await addResponse.Content.ReadAsStringAsync();
        var newCategoryId = int.Parse(responseContent);

        var response = await _httpClient.DeleteAsync($"api/Categories/{newCategoryId}");
        response.EnsureSuccessStatusCode();
        
        var categories = await Utilities.GetCategories(_appFactory);
        categories.Should().NotContain(c => c.Id == newCategoryId);
    }
}