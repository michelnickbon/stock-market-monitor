using StockMarketMonitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public interface IMailService
	{
		Task SendEmailNotification(List<StockInfo> stocks);
	}
}
