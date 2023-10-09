namespace btcTestnetCoins.Models
{
	public class PayoutDetails
	{
		public string? AddressDestination { get; set; }
		public string PaymentMethod = "BTC";
		public static double Amount = 0.0001;

		public PayoutDetails(string AddressDestination) { 
			this.AddressDestination = AddressDestination;
		}

	}
}
