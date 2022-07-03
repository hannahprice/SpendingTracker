using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpendingTracker.Client;
using SpendingTracker.Client.Services;
using MudBlazor.Services;
using MudBlazor;

namespace Company.WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddTransient<ITransactionsService, TransactionsService>();
            builder.Services.AddTransient<ICategoriesService, CategoriesService>();
            builder.Services.AddTransient<ISubcategoriesService, SubcategoriesService>();
            builder.Services.AddTransient<IBudgetsService, BudgetsService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
            });

            await builder.Build().RunAsync();
        }
    }
}