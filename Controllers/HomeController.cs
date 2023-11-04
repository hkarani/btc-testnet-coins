using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using BTCTestnetCoins.Models;
using BTCTestnetCoins.Data;
using BTCTestnetCoins.Utilities;
using Newtonsoft.Json;
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

			var response = payoutAddress.GoogleCaptureToken;
			var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
			BTCTestnetCoinsDbContext dbCtx = new();

			var findUserByIP = dbCtx.Users.FirstOrDefault(ip  => ip.IpAddress == userIpAddress);
			if (findUserByIP != null)
			{
				if(findUserByIP.IsBlocked.GetValueOrDefault())
				{
					TempData["UserStatus"] = "Your Ip has been blocked. Try again later";
					Console.WriteLine("User is Blocked");
					return RedirectToAction(nameof(Index));
				}

				if (!findUserByIP.IsEligible.GetValueOrDefault())
				{
					var eligibleTime = findUserByIP.LastAccesed.AddDays(2);
					TempData["UserStatus"] = $"Your Ip is ineligile for coins. Try again after {eligibleTime}";
					Console.WriteLine("User is ineligible");
					return RedirectToAction(nameof(Index));
				}

				TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";
				findUserByIP.LastAccesed = DateTime.Now;
				findUserByIP.NumberOfTimesAccessed =+ 1;
				return RedirectToAction(nameof(Index));
			}

			if (ModelState.IsValid)
			{				;
				var isCaptchaValid = await HandleCaptcha.IsCaptchaValid(response, userIpAddress);
				if(!isCaptchaValid)
				{
					TempData["Captcha"] = $"You have failed the bot test";
					return RedirectToAction(nameof(Index));				
				}

				
				User user = new()
				{
					IpAddress = userIpAddress,
					FirstTimeAccesed = DateTime.Now,
					NumberOfTimesAccessed = 1,
					LastAccesed = DateTime.Now

				};

				dbCtx.Users.Add(user);
				dbCtx.SaveChanges();

				TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";
				Console.WriteLine("BTC sent to first time user");
				return RedirectToAction(nameof(Index));
			}
            return RedirectToAction(nameof(Index));
		}		      
           
    }
}