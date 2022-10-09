using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

[UsesVerify]
public class SubcategoriesTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _appFactory;
    private readonly HttpClient _httpClient;
    private readonly VerifySettings _verifySettings;

    public SubcategoriesTests(TestWebApplicationFactory appFactory)
    {
        _verifySettings = new VerifySettings();
        _verifySettings.UseDirectory("./Snapshots/Subcategories");
        
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task DatabaseIsSeededWithSomeSubcategories()
    {
        var subcategories = await TestHelpers.GetSubcategories(_appFactory);

        subcategories.Should().NotBeNullOrEmpty();
        await Verify(subcategories, _verifySettings);
    }

    [Fact]
    public async Task AddSubcategorySuccess()
    {
        var newSubcategory = new Subcategory()
        {
            Description = "TestSubcategory",
            CategoryId = 1
        };

        var response = await _httpClient.PostAsJsonAsync("api/Subcategories", newSubcategory);

        response.EnsureSuccessStatusCode();
        
        var newSubcategoryId = await response.Content.ReadFromJsonAsync<int>();

        var subcategories = await TestHelpers.GetSubcategories(_appFactory);
        var addedSubcategory = subcategories.Single(c => c.Id == newSubcategoryId);
        addedSubcategory.CategoryId.Should().Be(1);

        await TestHelpers.RemoveSubcategory(_appFactory, newSubcategoryId);
    }

    [Fact]
    public async Task GetSubcategorySuccess()
    {
        var response = await _httpClient.GetFromJsonAsync<Subcategory>("api/Subcategories/2");

        response.Should().NotBeNull();
        await Verify(response, _verifySettings);
    }
    
    [Fact]
    public async Task DeleteSubcategorySuccess()
    {
        var subcategory = new Subcategory
        {
            Description = "SubcategoryToBeDeleted",
            CategoryId = 5
        };
        
        var addResponse = await _httpClient.PostAsJsonAsync("api/Subcategories", subcategory);
        addResponse.EnsureSuccessStatusCode();
        
        var newSubcategoryId = await addResponse.Content.ReadFromJsonAsync<int>();

        var response = await _httpClient.DeleteAsync($"api/Subcategories/{newSubcategoryId}");
        response.EnsureSuccessStatusCode();
        
        var subcategories = await TestHelpers.GetSubcategories(_appFactory);
        subcategories.Should().NotContain(c => c.Id == newSubcategoryId);
    }
}