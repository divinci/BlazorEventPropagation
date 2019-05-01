using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorEventPropagation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
                .SetMinimumLevel(LogLevel.Trace));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
