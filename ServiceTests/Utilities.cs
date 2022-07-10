using Microsoft.Extensions.DependencyInjection;
using SpendingTracker.Server;
using SpendingTracker.Shared.Models;

namespace ServiceTests;

public static class Utilities
{
    public static FinanceContext? GetDbContext(TestWebApplicationFactory<Program> appFactory)
    {
        var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        return scope.ServiceProvider.GetService<FinanceContext>();
    }
}