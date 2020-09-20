using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace StockMarketMonitor.Services
{
	class StockInfoService
	{
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        private Stream stream;
        private readonly string url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=demo";

		public void GetStockInfo()
		{
            webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
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
