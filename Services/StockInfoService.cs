using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace StockMarketMonitor.Services
{
	public class StockInfoService
	{
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        private Stream stream;
        private readonly string apiUrl;

        public StockInfoService()
        {
            apiUrl = Environment.GetEnvironmentVariable("API_STOCK_URL");
        }

		public void GetStockInfo()
		{
            webRequest = (HttpWebRequest)WebRequest.Create(string.Format(apiUrl));
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();

            var responseString = string.Empty;
            using (stream = webResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                responseString = reader.ReadToEnd();
            }
        }
    }
}
