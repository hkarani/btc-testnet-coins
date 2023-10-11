namespace btcTestnetCoins.Models
{
	public class PayoutDetails
	{
		public string DestinationAddress { get; set; } = string.Empty;
		public string? PaymentMethod { get; set; }
		public string? Amount { get; set; }
		public string? Approved { get; set; }

	}
}
