using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

public static class Utilities
{
    #region Budgets

    public static async Task RemoveBudget(TestWebApplicationFactory appFactory, int addedId)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();
        
        dbContext!.Budgets.Remove(dbContext.Budgets.Single(c => c.Id == addedId));
        await dbContext.SaveChangesAsync();
    }
    
    public static async Task RemoveBudgets(TestWebApplicationFactory appFactory, int[] addedIds)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();

        foreach (var id in addedIds)
        {
            dbContext!.Budgets.Remove(dbContext.Budgets.Single(c => c.Id == id));
        }
        
        await dbContext!.SaveChangesAsync();
    }

    public static async Task<List<Budget>> GetBudgets(TestWebApplicationFactory appFactory)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();

        return await dbContext!.Budgets.ToListAsync();
    }

    #endregion

    #region Transactions

    public static async Task RemoveTransaction(TestWebApplicationFactory appFactory, int addedId)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();
        
        dbContext!.Transactions.Remove(dbContext.Transactions.Single(c => c.Id == addedId));
        await dbContext.SaveChangesAsync();
    }
    
    public static async Task RemoveTransactions(TestWebApplicationFactory appFactory, int[] addedIds)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();

        foreach (var id in addedIds)
        {
            dbContext!.Transactions.Remove(dbContext.Transactions.Single(c => c.Id == id));
        }
        
        await dbContext!.SaveChangesAsync();
    }
    
    public static async Task<List<Transaction>> GetTransactions(TestWebApplicationFactory appFactory)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();

        return await dbContext!.Transactions.ToListAsync();
    }

    #endregion

    #region Categories

    public static async Task RemoveCategory(TestWebApplicationFactory appFactory, int addedId)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();
        
        dbContext!.Categories.Remove(dbContext.Categories.Single(c => c.Id == addedId));
        await dbContext.SaveChangesAsync();
    }
    
    public static async Task<List<Category>> GetCategories(TestWebApplicationFactory appFactory)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();

        return await dbContext!.Categories.ToListAsync();
    }

    #endregion

    #region Subcategories

    public static async Task RemoveSubcategory(TestWebApplicationFactory appFactory, int addedId)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();
        
        dbContext!.Subcategories.Remove(dbContext.Subcategories.Single(c => c.Id == addedId));
        await dbContext.SaveChangesAsync();
    }
    
    public static async Task<List<Subcategory>> GetSubcategories(TestWebApplicationFactory appFactory)
    {
        using var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<FinanceContext>();

        return await dbContext!.Subcategories.ToListAsync();
    }

    #endregion
}