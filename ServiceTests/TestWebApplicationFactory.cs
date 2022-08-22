using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpendingTracker.Server;

namespace ServiceTests;

public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<FinanceContext>));

            services.Remove(descriptor);

            services.AddDbContext<FinanceContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDb");
            },ServiceLifetime.Transient, ServiceLifetime.Transient);
            
            var sp = services.BuildServiceProvider();
            
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<FinanceContext>();

                db.Database.EnsureCreated();
            }
        });
    }
}