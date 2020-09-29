using Moq;
using StockMarketMonitor.Services;
using System;
using Xunit;

namespace StockMarketMonitor.UnitTest
{
	public class ServicesTests
	{
		private Mock<INotificationService> _notificationService;
		private Mock<IHttpRequestService> _httpRequestService;

		private readonly string testKey = "&apikey=GDUJE493";
		private readonly string testBaseUrl = "www.testing.com/query?function=TIME_SERIES_DAILY&";
		private readonly string testSymbol = "IBM";

		public ServicesTests()
		{
			_httpRequestService = new Mock<IHttpRequestService>();
			_httpRequestService.Setup(h => h.BuildEndpointURL(testSymbol)).Returns(string.Concat(testBaseUrl, "symbol=", testSymbol, testKey));
		}

		[Fact]
		public void HttpRequestService_BuildsEndpointURL()
		{
			var expected = "www.testing.com/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=GDUJE493";
			Assert.Equal(expected, _httpRequestService.Object.BuildEndpointURL(testSymbol));
		}
	}
}
