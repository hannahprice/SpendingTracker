using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpendingTracker.Server;

namespace ServiceTests;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext(this IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FinanceContext>));
        if (descriptor != null) services.Remove(descriptor);
    }
    public static void EnsureDatabaseCreated(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<FinanceContext>();

        context.Database.EnsureCreated();
    }
}