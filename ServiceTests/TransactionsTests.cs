using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;
using Xunit;

namespace ServiceTests;

[UsesVerify]
public class TransactionsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _appFactory;
    private readonly HttpClient _httpClient;
    private readonly VerifySettings _verifySettings;

    public TransactionsTests(TestWebApplicationFactory<Program> appFactory)
    {
        _verifySettings = new VerifySettings();
        _verifySettings.UseDirectory("./Snapshots/Transactions");
        
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task GetTransactionsSuccess()
    {
        var addTransactionTask1 = InsertTestTransaction(1);
        var addTransactionTask2 = InsertTestTransaction(2);
        var addTransactionTask3 = InsertTestTransaction(3);
        await Task.WhenAll(addTransactionTask1, addTransactionTask2, addTransactionTask3);

        var response = await _httpClient.GetFromJsonAsync<List<Transaction>>("api/Transactions");

        response.Should().NotBeNullOrEmpty();
        response.Should().BeInDescendingOrder(c => c.Id);
        response.Should().AllSatisfy(c =>
        {
            c.Categories.Should().NotBeNullOrEmpty();
            c.Subcategories.Should().NotBeNullOrEmpty();
        });

        await Verify(response, _verifySettings);

        var dbContext = Utilities.GetDbContext(_appFactory);

        var removeTransactionTask1 = RemoveAddedTransaction(dbContext!, 1);
        var removeTransactionTask2 = RemoveAddedTransaction(dbContext!, 2);
        var removeTransactionTask3 = RemoveAddedTransaction(dbContext!, 3);
        await Task.WhenAll(removeTransactionTask1, removeTransactionTask2, removeTransactionTask3);
    }

    [Fact]
    public async Task AddTransactionSuccess()
    {
        var transaction = new Transaction()
        {
            Id = 4, Amount = 55.99m, Description = "Card payment to Sainsbury's", IsReoccurring = false,
            IsOutwardPayment = true, DateOfTransaction = DateTime.Now,
            Categories = new List<Category> { new Category { Id = 1 } },
            Subcategories = new List<Subcategory> { new Subcategory { Id = 1 } }
        };

        var response = await _httpClient.PostAsJsonAsync("api/Transactions", transaction);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var newTransactionId = int.Parse(responseContent);

        var dbContext = Utilities.GetDbContext(_appFactory);
        dbContext!.Transactions.Should().Contain(c => c.Id == newTransactionId);

        await RemoveAddedTransaction(dbContext, 4);
    }

    [Fact]
    public async Task GetTransactionSuccess()
    {
        const int id = 5;
        await InsertTestTransaction(id);

        var response = await _httpClient.GetFromJsonAsync<Transaction>($"api/Transactions/{id}");

        response.Should().NotBeNull();
        response!.Id.Should().Be(id);
        await Verify(response, _verifySettings);

        var dbContext = Utilities.GetDbContext(_appFactory);

        await RemoveAddedTransaction(dbContext!, id);
    }

    [Fact]
    public async Task DeleteBudgetSuccess()
    {
        const int id = 6;
        await InsertTestTransaction(id);

        var response = await _httpClient.DeleteAsync($"api/Transactions/{id}");
        response.EnsureSuccessStatusCode();

        var dbContext = Utilities.GetDbContext(_appFactory);
        dbContext!.Transactions.Should().NotContain(c => c.Id == id);
    }

    #region TestHelpers

    private async Task InsertTestTransaction(int id)
    {
        var transaction = new Transaction()
        {
            Id = id, Amount = 55.99m, Description = "Card payment to Sainsbury's", IsReoccurring = false,
            IsOutwardPayment = true, DateOfTransaction = DateTime.Now,
            Categories = new List<Category> { new Category { Id = 1 } },
            Subcategories = new List<Subcategory> { new Subcategory { Id = 1 } }
        };
        var response = await _httpClient.PostAsJsonAsync("api/Transactions", transaction);
        response.EnsureSuccessStatusCode();
    }

    private async Task RemoveAddedTransaction(FinanceContext dbContext, int addedTransactionId)
    {
        dbContext.Transactions.Remove(dbContext.Transactions.Single(t => t.Id == addedTransactionId));
        await dbContext.SaveChangesAsync();
    }

    #endregion
}