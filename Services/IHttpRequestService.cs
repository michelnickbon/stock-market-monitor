using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public interface IHttpRequestService
	{
		Task<JObject> GetStockQuotes(string url);

		string BuildEndpointURL(string symbol);
	}
}
