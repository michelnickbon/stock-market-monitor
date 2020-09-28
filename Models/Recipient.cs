
namespace StockMarketMonitor.Models
{
	public class Recipient
	{
		public Recipient(string name, string email, string phoneNumber)
		{
			Email = email;
			PhoneNumber = phoneNumber;
			Name = name;
		}

		public string Name { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }
	}
}
