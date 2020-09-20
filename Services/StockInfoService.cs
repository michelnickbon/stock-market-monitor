using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockMarketMonitor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public class StockInfoService
	{
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;

        public StockInfoService() { }

		public async Task<StockInfo> GetStockInfo()
		{
            webRequest = (HttpWebRequest)WebRequest.Create(string.Format(Environment.GetEnvironmentVariable("API_STOCK_URL")));
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();

            using Stream stream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            string responseString = await reader.ReadToEndAsync();

            var resultObject = JObject.Parse(responseString);
            var metaData = resultObject["Meta Data"].ToObject<Dictionary<string, string>>();
            var timeSeries = resultObject["Time Series (Daily)"].ToObject<Dictionary<string, Dictionary<string, string>>>();

            var stock = new StockInfo();
            stock.Symbol = "test";

            return stock;
        }
    }
}
