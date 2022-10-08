using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;
using Xunit;

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
        var subcategories = await Utilities.GetSubcategories(_appFactory);

        // dbContext.Should().NotBeNull();
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
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var newSubcategoryId = int.Parse(responseContent);

        var subcategories = await Utilities.GetSubcategories(_appFactory);
        var addedSubcategory = subcategories.Single(c => c.Id == newSubcategoryId);
        addedSubcategory.CategoryId.Should().Be(1);

        // await RemoveAddedSubcategory(dbContext, addedSubcategory);
        await Utilities.RemoveSubcategory(_appFactory, newSubcategoryId);
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
            Id = 45,
            Description = "SubcategoryToBeDeleted",
            CategoryId = 5
        };
        
        var addResponse = await _httpClient.PostAsJsonAsync("api/Subcategories", subcategory);
        addResponse.EnsureSuccessStatusCode();

        var response = await _httpClient.DeleteAsync($"api/Subcategories/{subcategory.Id}");
        response.EnsureSuccessStatusCode();
        
        // var dbContext = Utilities.GetDbContext(_appFactory);
        var subcategories = await Utilities.GetSubcategories(_appFactory);
        subcategories.Should().NotContain(c => c.Id == 45);
    }
    
    // private async Task RemoveAddedSubcategory(FinanceContext dbContext, Subcategory addedSubcategory)
    // {
    //     dbContext.Subcategories.Remove(addedSubcategory);
    //     await dbContext.SaveChangesAsync();
    // }
}