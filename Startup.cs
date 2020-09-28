using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StockMarketMonitor.Services;

[assembly: FunctionsStartup(typeof(StockMarketMonitor.Startup))]

namespace StockMarketMonitor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IHttpRequestService>((s) => {
                return new HttpRequestService();
            });

            builder.Services.AddSingleton<INotificationService>((s) => {
                return new NotificationService();
            });
        }
    }
}
