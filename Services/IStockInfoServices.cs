using StockMarketMonitor.Models;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public interface IStockInfoServices
	{
		public Task<StockInfo> GetStockInfo(string symbol);

		public string BuildEndpointURL(string symbol);
	}
}
