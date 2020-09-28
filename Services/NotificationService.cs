using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System;
using StockMarketMonitor.Models;

namespace StockMarketMonitor.Services
{
	public class NotificationService : INotificationService
	{
		/// <summary> Sends stock information emails </summary>
		/// <param name="stocks"> List of Stocks </param>
		/// <param name="recipients"> List of Recipients </param>
		public async Task SendEmailNotification(List<Stock> stocks, List<Recipient> recipients)
		{
			try
			{
				var message = new MimeMessage();
				message.Body = new TextPart("html") { Text = BuildEmailMessageBody(stocks) };
				message.Subject = Environment.GetEnvironmentVariable("DEFAULT_SUBJECT");
				message.From.Add(new MailboxAddress("N/A", Environment.GetEnvironmentVariable("DEFAULT_SENDER")));

				using var client = new SmtpClient();
				foreach (var recipient in recipients)
				{
					message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
					await client.ConnectAsync(Environment.GetEnvironmentVariable("SMTP_SERVER"), 587, false);
					await client.AuthenticateAsync(Environment.GetEnvironmentVariable("SMTP_AUTH_USER"), Environment.GetEnvironmentVariable("SMTP_AUTH_PASS"));
					await client.SendAsync(message);
				}

				await client.DisconnectAsync(true);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary> Sends stock information text messages </summary>
		/// <param name="stocks"> List of Stocks </param>
		/// <param name="recipients"> List of Recipients </param>
		public async Task SendTextMessageNotification(List<Stock> stocks, List<Recipient> recipients)
		{
			throw new NotImplementedException();
		}

		/// <summary> Builds a message body for the email notifcation </summary>
		/// <param name="stocks"> List of Stocks </param>
		/// <returns> Message body in string format </returns>
		public string BuildEmailMessageBody(List<Stock> stocks)
		{
			var body = "<h1> Markets are open, here are you opening quotes</h1>";
			foreach (var stock in stocks)
			{
				body += string.Concat("<p>", stock.Symbol, ":", stock.OpenPrice, "</p>");
			}
			return body;
		}
	}
}
