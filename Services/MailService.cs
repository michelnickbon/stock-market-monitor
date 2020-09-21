using StockMarketMonitor.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	class MailService : IMailService
	{
		public MailService() { }

		public async Task SendEmailNotification(List<StockInfo> stocks)
		{
			try
			{
				MailMessage mail = new MailMessage();
				SmtpClient smtpServer = new SmtpClient(Environment.GetEnvironmentVariable("SMTP_SERVER"));

				mail.From = new MailAddress("StockMarketMonitor");
				mail.To.Add(Environment.GetEnvironmentVariable("RECIPIENT"));
				mail.Subject = "StockMarketMonitor - Markets are open, here are your opening quotes";
				mail.Body = "This is for testing SMTP mail from GMAIL";

				smtpServer.Port = 587;
				smtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
				smtpServer.EnableSsl = true;

				await smtpServer.SendMailAsync(mail);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
