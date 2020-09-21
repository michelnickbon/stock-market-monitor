using Newtonsoft.Json.Linq;
using StockMarketMonitor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public class StockInfoService : IStockInfoServices
	{
		/// <summary> Requests stock data </summary>
		/// <param name="symbol"> Stock symbol/ticker </param>
		/// <returns> Stock model containing symbol and open price </returns>
		public async Task<StockInfo> GetStockInfo(string symbol)
		{
			try
			{
				HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(BuildEndpointURL(symbol));
				webRequest.Method = "GET";
				HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

				using Stream stream = webResponse.GetResponseStream();
				StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

				// Complex mapping to model, so this will do for now
				var objResult = JObject.Parse(await reader.ReadToEndAsync());
				var timeSeries = objResult["Time Series (Daily)"].ToObject<Dictionary<string, Dictionary<string, string>>>().First().Value;

				var stock = new StockInfo()
				{
					Symbol = symbol,
					OpenPrice = double.Parse(timeSeries.Values.First(), CultureInfo.InvariantCulture)
				};

				return stock;
			}
			catch (WebException ex)
			{
				throw ex;
			}
		}

		/// <summary> Builds a dynamic endpoint url </summary>
		/// <param name="symbol"> Stock symbol/ticker </param>
		/// <returns> Endpoint url in string format </returns>
		public string BuildEndpointURL(string symbol)
		{
			return string.Concat(Environment.GetEnvironmentVariable("API_STOCK_BASE_URL"), "symbol=", symbol, Environment.GetEnvironmentVariable("API_KEY"));
		}
	}
}
