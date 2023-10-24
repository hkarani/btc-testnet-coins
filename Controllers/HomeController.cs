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
		public async Task <IActionResult> SendBitcoin(PayoutAddress payoutAddress)
		{
			
			//var btcpayServerUri = new Uri("https://testnet.demo.btcpayserver.org");
			//var apiKey = Environment.GetEnvironmentVariable("API_KEY");
			//var storeId = Environment.GetEnvironmentVariable("STORE_ID");

			//var client = new BTCPayServerClient(btcpayServerUri, apiKey);

			//var payoutRequest = new CreatePayoutThroughStoreRequest
			//{
			//	Amount = (decimal?)0.002,
			//	PaymentMethod = "BTC",
			//	Destination = payoutAddress.DestinationAddress,
			//	Approved = true
			//};

			//var payoutData = await client.CreatePayout(storeId, payoutRequest);

			//if (payoutData.State == PayoutState.AwaitingPayment)
			//{
			//	;
			//	Console.WriteLine($"Payout has been initialized. TX ID: {payoutData.Id}");
			//}
			//else
			//{
			//	Console.WriteLine($"Payout failed with status: {payoutData.State}");
			//}
			if (ModelState.IsValid)
			{
				TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";
				return RedirectToAction(nameof(Index));
			}

			Console.WriteLine("View returned");
			return RedirectToAction(nameof(Index));
		}
			


			
           
           
    }
}