using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpendingTracker.Server;

namespace ServiceTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlTestcontainer _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithDatabase(new MsSqlTestcontainerConfiguration
        {
            Database = "TestFinanceTracker",
            // Username = "hannah",
            Password = "test"
        }).Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<FinanceContext>));

            services.Remove(descriptor!);

            services.AddDbContext<FinanceContext>(options =>
            {
                options.UseSqlServer(_dbContainer.ConnectionString);
            });
            
            // services.AddDbContext<FinanceContext>(options =>
            // {
            //     options.UseInMemoryDatabase("InMemoryTestDb");
            // },ServiceLifetime.Transient, ServiceLifetime.Transient);
            
            var sp = services.BuildServiceProvider();
            
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<FinanceContext>();

                db.Database.EnsureCreated();
            }
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}