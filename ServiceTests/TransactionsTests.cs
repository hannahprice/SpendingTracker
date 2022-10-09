using System.Net.Http.Json;
using FluentAssertions;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

public class TransactionsTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _appFactory;
    private readonly HttpClient _httpClient;

    public TransactionsTests(TestWebApplicationFactory appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task GetTransactionsSuccess()
    {
        var newTransactionId1 = await InsertTestTransaction();
        var newTransactionId2 = await InsertTestTransaction();

        var response = await _httpClient.GetFromJsonAsync<List<Transaction>>("api/Transactions");

        response.Should().NotBeNullOrEmpty();
        response.Should().BeInDescendingOrder(c => c.Id);
        response.Should().AllSatisfy(c =>
        {
            c.Category.Should().NotBeNull();
            c.Subcategories.Should().NotBeNullOrEmpty();
        });
        
        await Utilities.RemoveTransactions(_appFactory, new int[]{ newTransactionId1, newTransactionId2 });
    }

    [Fact]
    public async Task AddTransactionSuccess()
    {
        var transaction = new Transaction()
        {
            Amount = 55.99m, Description = "Card payment to Sainsbury's", IsReoccurring = false,
            IsOutwardPayment = true, DateOfTransaction = DateTime.Now,
            Category = new Category{Id = 1},
            Subcategories = new List<Subcategory> { new Subcategory { Id = 1 } }
        };

        var response = await _httpClient.PostAsJsonAsync("api/Transactions", transaction);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var newTransactionId = int.Parse(responseContent);

        var transactions = await Utilities.GetTransactions(_appFactory);
        transactions.Should().Contain(c => c.Id == newTransactionId);

        await Utilities.RemoveTransaction(_appFactory, newTransactionId);
    }

    [Fact]
    public async Task GetTransactionSuccess()
    {
        var newTransactionId = await InsertTestTransaction();

        var response = await _httpClient.GetFromJsonAsync<Transaction>($"api/Transactions/{newTransactionId}");

        response.Should().NotBeNull();
        response!.Id.Should().Be(newTransactionId);
        
        await Utilities.RemoveTransaction(_appFactory, newTransactionId);
    }

    [Fact]
    public async Task DeleteBudgetSuccess()
    {
        var newTransactionId = await InsertTestTransaction();

        var response = await _httpClient.DeleteAsync($"api/Transactions/{newTransactionId}");
        response.EnsureSuccessStatusCode();

        var transactions = await Utilities.GetTransactions(_appFactory);
        transactions.Should().NotContain(c => c.Id == newTransactionId);
    }

    #region TestHelpers

    private async Task<int> InsertTestTransaction()
    {
        var transaction = new Transaction()
        {
            Amount = 55.99m, Description = "Card payment to Sainsbury's", IsReoccurring = false,
            IsOutwardPayment = true, DateOfTransaction = DateTime.Now,
            Category = new Category{Id = 1},
            Subcategories = new List<Subcategory> { new Subcategory { Id = 1 } }
        };
        var response = await _httpClient.PostAsJsonAsync("api/Transactions", transaction);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return int.Parse(responseContent);
    }

    #endregion
}