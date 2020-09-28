namespace StockMarketMonitor.Services
{
	public class Stock
	{
		public Stock(string symbol)
		{
			Symbol = symbol;
			OpenPrice = 0;
		}

		public string Symbol { get; set; }

		public double OpenPrice { get; set; }
	}
}
