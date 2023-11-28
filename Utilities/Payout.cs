using BTCPayServer.Client;
using BTCPayServer.Client.Models;

namespace BTCTestnetCoins.Utilities
{
	public class Payout
	{
		public static async Task<(PayoutData payoutData, bool Success)> SendTestNetCoins(string destinationAddress)
		{
			var btcpayServerUri = new Uri("https://testnet.demo.btcpayserver.org");
			var apiKey = Environment.GetEnvironmentVariable("API_KEY");
			var storeId = Environment.GetEnvironmentVariable("STORE_ID");

			var client = new BTCPayServerClient(btcpayServerUri, apiKey);

			var payoutRequest = new CreatePayoutThroughStoreRequest
			{
				Amount = (decimal?)0.0001,
				PaymentMethod = "BTC",
				Destination = destinationAddress,
				Approved = true
			};
			try
			{
				var payoutData = await client.CreatePayout(storeId, payoutRequest);
				return new(payoutData, true);

			}catch(Exception)
			{
				return new(new PayoutData { }, false);	
			}
		}
	}
}
