using StockMarketMonitor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public interface IStockInfoServices
	{
		public Task<StockInfo> GetStockInfo();
	}
}
