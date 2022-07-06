using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SpendingTracker.Server;
using Xunit;

namespace ServiceTests;

public class GetCategoriesTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _appFactory;
    private readonly HttpClient _httpClient;

    public GetCategoriesTests(TestWebApplicationFactory<Program> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }
    
    [Fact]
    public async Task Database_IsSeededWithExpectedCategories()
    {
        var expectedCategoryNames = new List<string>
        {
            "Bills", "Shopping", "Travel", "Health", "Leisure", "Holidays", "Miscellaneous"
        };
            
        var response = await _httpClient.GetAsync("api/Categories");
        response.EnsureSuccessStatusCode();
        
        var dbContext = GetDbContext();

        dbContext.Should().NotBeNull();
        dbContext!.Categories.Should().NotBeNull();
        dbContext.Categories.Should().HaveCount(7);
        dbContext.Categories.Select(c => c.Description).Should().BeEquivalentTo(expectedCategoryNames);
    }

    private FinanceContext? GetDbContext()
    {
        var scope = _appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        return scope.ServiceProvider.GetService<FinanceContext>();
    }
}