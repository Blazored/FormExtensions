using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace ClientSideBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddSingleton(typeof(IStringLocalizer), typeof(StringLocalizer<SharedLocalization.SharedResources>));

            await builder.Build().RunAsync();
        }
    }
}