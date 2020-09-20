using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StockMarketMonitor.Services
{
	class MailService : IMailService
	{
        private MailMessage mail;
        private SmtpClient smtpServer;

        public MailService() {}

		public async Task SendEmail(List<Tuple<string, int>> equityList)
		{
			try
			{
				mail = new MailMessage();
				smtpServer = new SmtpClient("smtp.gmail.com");

				mail.From = new MailAddress("your_email_address@gmail.com");
				mail.To.Add("to_address");
				mail.Subject = "Test Mail";
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
