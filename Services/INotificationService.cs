using StockMarketMonitor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	public interface INotificationService
	{
		Task SendEmailNotification(List<Stock> stocks, List<Recipient> recipients);

		Task SendTextMessageNotification(List<Stock> stocks, List<Recipient> recipients);

		string BuildEmailMessageBody(List<Stock> stocks);
	}
}
