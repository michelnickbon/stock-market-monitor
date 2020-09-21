using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockMarketMonitor.Models;
using StockMarketMonitor.Services;

namespace StockMarketMonitor
{
    public static class StockMarketMonitor
    {
        [FunctionName("StockMarketMonitor")]
        public async static Task Run([TimerTrigger("00 30 15 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Example config, can be inputed as parameters aswell
            List<string> symbols = new List<string>() {"IBM", "AAPL", "MSFT" };
            List<StockInfo> stocks = new List<StockInfo>();

            // Get stock info
            var stockService = new StockInfoService();
            foreach (var symbol in symbols)
            {
                var stock = await stockService.GetStockInfo(symbol);
                stocks.Add(stock);
            }

            // Send email notification
            var emailService = new MailService();
            await emailService.SendEmailNotification(stocks);
        }
    }
}
