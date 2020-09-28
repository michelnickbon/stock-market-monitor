using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public class HttpRequestService : IHttpRequestService
	{

		/// <summary> Retrieves daily quotes for a specific stock </summary>
		/// <param name="url"> AlphaVantage endpoint URL </param>
		/// <returns> JObject result with quotes </returns>
		public async Task<JObject> GetStockQuotes(string url)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.Method = "GET";
			HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

			using Stream stream = webResponse.GetResponseStream();
			StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

			return JObject.Parse(await reader.ReadToEndAsync());
		}

		/// <summary> Builds a dynamic endpoint url </summary>
		/// <param name="symbol"> Stock symbol </param>
		/// <returns> Endpoint url in string format </returns>
		public string BuildEndpointURL(string symbol)
		{
			return string.Concat(Environment.GetEnvironmentVariable("API_STOCK_BASE_URL"), "symbol=", symbol, Environment.GetEnvironmentVariable("API_KEY"));
		}
	}
}
