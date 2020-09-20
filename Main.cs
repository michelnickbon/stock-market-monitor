using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockMarketMonitor.Services;

namespace StockMarketMonitor
{
    public static class StockMarketMonitor
    {
        [FunctionName("StockMarketMonitor")]
        public async static Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Get stock info
            var stockService = new StockInfoService();
            await stockService.GetStockInfo("IBM");

            // Send email notification
        }
    }
}
