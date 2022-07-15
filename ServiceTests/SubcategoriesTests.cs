﻿using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;
using Xunit;

namespace ServiceTests;

public class SubcategoriesTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _appFactory;
    private readonly HttpClient _httpClient;

    public SubcategoriesTests(TestWebApplicationFactory<Program> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public void DatabaseIsSeededWithSomeSubcategories()
    {
        var dbContext = Utilities.GetDbContext(_appFactory);

        dbContext.Should().NotBeNull();
        dbContext!.Subcategories.Should().NotBeNullOrEmpty();
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
        
        var dbContext = Utilities.GetDbContext(_appFactory);
        var addedSubcategory = dbContext!.Subcategories.Single(c => c.Id == newSubcategoryId);
        addedSubcategory.CategoryId.Should().Be(1);

        await RemoveAddedSubcategory(dbContext, addedSubcategory);
    }

    private async Task RemoveAddedSubcategory(FinanceContext dbContext, Subcategory addedSubcategory)
    {
        dbContext.Subcategories.Remove(addedSubcategory);
        await dbContext.SaveChangesAsync();
    }
}