using Moq;
using Newtonsoft.Json.Linq;
using StockMarketMonitor.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace StockMarketMonitor.UnitTest
{
	public class ServicesTests
	{
		private Mock<INotificationService> _notificationService;
		private Mock<IHttpRequestService> _httpRequestService;
		private readonly string symbol = "IBM";

		public ServicesTests()
		{
			Environment.SetEnvironmentVariable("API_KEY", "&apikey=TESTEST");
			Environment.SetEnvironmentVariable("API_STOCK_BASE_URL", "https://www.testing.com/query?function=TEST_SERIES&");
			_httpRequestService = new Mock<IHttpRequestService>();
		}

		[Fact]
		public void HttpRequestService_BuildsEndpointURL()
		{
			var url = new HttpRequestService().BuildEndpointURL(symbol);
			Assert.Equal("https://www.testing.com/query?function=TEST_SERIES&symbol=IBM&apikey=TESTEST", url);
		}

		[Fact]
		public async void HttpRequestService_ReturnsStockQuotes()
		{
			_httpRequestService.Setup(h => h.GetStockQuotes(It.IsAny<string>())).ReturnsAsync(new JObject());
			var response = await _httpRequestService.Object.GetStockQuotes("www.test.com");
			Assert.True(response.GetType() == typeof(JObject));
		}

		[Fact]
		public void NotificationService_EmailMessageBodyGetBuilt()
		{
			var stocks = new List<Stock>() { new Stock("Symbol") { OpenPrice = 0 } };
			var messageBody = new NotificationService().BuildEmailMessageBody(stocks);

			Assert.NotNull(messageBody);
			Assert.Contains("Markets are open, here are you opening quotes", messageBody);
		}

		[Fact]
		public void NotificationService_EmailGetsSent()
		{
			throw new NotImplementedException();
		}

	}
}
