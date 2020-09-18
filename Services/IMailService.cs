using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public interface IMailService
	{
		Task SendEmail(List<Tuple<string, int>> equityList);
	}
}
