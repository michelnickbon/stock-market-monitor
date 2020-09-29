using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockMarketMonitor.Models;
using StockMarketMonitor.Services;

namespace StockMarketMonitor
{
    public class StockMarketMonitor
    {
        private readonly IHttpRequestService _httpService;
        private readonly INotificationService _notificationService;

        public StockMarketMonitor(IHttpRequestService httpService, INotificationService notificationService)
        {
            _httpService = httpService;
            _notificationService = notificationService;
        }

        [FunctionName("StockMarketMonitor")]
        public async Task Run([TimerTrigger("00 30 15 * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            // Example: Configure stocks and recipients
            List<Stock> stocks = new List<Stock>() { new Stock("IBM"), new Stock("AAPL") };
            List<Recipient> recipients = new List<Recipient>() { new Recipient("test guy", "test@gmail.com", "+46738748766") };

            // Gather opening prices
            foreach (var stock in stocks)
            {
                var quotes = await _httpService.GetStockQuotes(_httpService.BuildEndpointURL(stock.Symbol));
                var timeSeries = quotes["Time Series (Daily)"].ToObject<Dictionary<string, Dictionary<string, string>>>().First().Value;
                stock.OpenPrice = double.Parse(timeSeries.Values.First(), CultureInfo.InvariantCulture);
                stocks.Add(stock);
            }

            // Send notifications
            await _notificationService.SendEmailNotification(stocks, recipients);
            await _notificationService.SendTextMessageNotification(stocks, recipients);
        }
    }
}
