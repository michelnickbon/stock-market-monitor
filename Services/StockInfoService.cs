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
	public class StockInfoService : IStockInfoServices
	{
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;

		public async Task<StockInfo> GetStockInfo(string symbol)
        {
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(BuildEndpointURL(symbol));
                webRequest.Method = "GET";
                webResponse = (HttpWebResponse)webRequest.GetResponse();

                using Stream stream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

                var obj = JObject.Parse(await reader.ReadToEndAsync());
                var metaData = obj["Meta Data"].ToObject<Dictionary<string, string>>();
                var timeSeries = obj["Time Series (Daily)"].ToObject<Dictionary<string, Dictionary<string, string>>>();

                var stock = new StockInfo
                {
                    Symbol = "test"
                };

                return stock;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public string BuildEndpointURL(string symbol)
        {
            return string.Concat(Environment.GetEnvironmentVariable("API_STOCK_BASE_URL"), "symbol=", symbol, Environment.GetEnvironmentVariable("API_KEY"));
        }
    }
}
