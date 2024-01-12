using Microsoft.Extensions.DependencyInjection;
using Mc2.CrudTest.ServerHelper.IoC;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Mc2.CrudTest.Presentation.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddApiClientService(x => x.ApiBaseAddress = $"https://localhost:9081");

            //var apiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS");
            //builder.Services.AddApiClientService(x => x.ApiBaseAddress = apiBaseAddress);

            var app = builder.Build();
            await app.RunAsync();
        }
    }
}