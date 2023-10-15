using Microsoft.AspNetCore.Mvc;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using btcTestnetCSoins.Models;

namespace btcTestnetCoins.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
			
            return View();
        }

		[HttpPost]
		public async Task <IActionResult> SendBitcoin(PayoutAddress payoutDetails)
		{
			var btcpayServerUri = new Uri("https://testnet.demo.btcpayserver.org");
			var apiKey = "7f75398b1cf1e841d23d91e3667a35e456668fe3";
			var storeId = "EEw2ecPHLKTQ3mAJkfR5Y56FF2W4yp8vgJxKQncdNVf7";

			var client = new BTCPayServerClient(btcpayServerUri, apiKey);

			var payoutRequest = new CreatePayoutThroughStoreRequest
			{
				Amount = (decimal?)0.002,
				PaymentMethod = "BTC",
				Destination = payoutDetails.DestinationAddress,
				Approved = true
			};

			var payoutData = await client.CreatePayout(storeId, payoutRequest);

			if (payoutData.State == PayoutState.AwaitingPayment)
			{
				Console.WriteLine($"Payout has been initialized. TX ID: {payoutData.Id}");
			}
			else
			{
				Console.WriteLine($"Payout failed with status: {payoutData.State}");
			}

			return View(payoutData);
		}
           
           
    }
}