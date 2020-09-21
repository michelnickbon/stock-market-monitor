using MimeKit;
using StockMarketMonitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System;

namespace StockMarketMonitor.Services
{
	class MailService : IMailService
	{
		public MailService() { }

		/// <summary> Sends stock information notifications/emails </summary>
		/// <param name="symbol"> List of Stock model </param>
		public async Task SendEmailNotification(List<StockInfo> stocks)
		{
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("N/A", Environment.GetEnvironmentVariable("DEFAULT_SENDER")));
				message.To.Add(new MailboxAddress("N/A", Environment.GetEnvironmentVariable("DEFAULT_RECIPIENT")));
				message.Subject = Environment.GetEnvironmentVariable("DEFAULT_SUBJECT");
				message.Body = new TextPart("html") { Text = BuildMessageBody(stocks) };

				using var client = new SmtpClient();
				await client.ConnectAsync(Environment.GetEnvironmentVariable("SMTP_SERVER"), 587, false);
				await client.AuthenticateAsync(Environment.GetEnvironmentVariable("SMTP_AUTH_USER"), Environment.GetEnvironmentVariable("SMTP_AUTH_PASS"));
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary> Builds a message body for the email notifcation </summary>
		/// <param name="symbol"> List of Stock model </param>
		/// <returns> Message body in string format </returns>
		public string BuildMessageBody(List<StockInfo> stocks)
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
