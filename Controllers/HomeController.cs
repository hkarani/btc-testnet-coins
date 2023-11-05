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

				PayoutData payoutData = await Payout.SendTestNetCoins(payoutAddress.DestinationAddress);

				if(payoutData != null) {
					TempData["Success"] = $"Payout failed";
					Console.WriteLine("Sending transaction failed");
				}

				if(payoutData?.State == PayoutState.AwaitingPayment)
				{

					TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";
					Console.WriteLine("BTC sent to subsequent user");
					findUserByIP.LastAccesed = DateTime.Now;
					findUserByIP.NumberOfTimesAccessed = +1;
					return RedirectToAction(nameof(Index));
				}

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

				PayoutData payoutData = await Payout.SendTestNetCoins(payoutAddress.DestinationAddress);

				if (payoutData != null)
				{
					TempData["Success"] = $"Payout failed";
					Console.WriteLine("Sending transaction failed");
				}

				if (payoutData?.State == PayoutState.AwaitingPayment)
				{

					TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";
					Console.WriteLine("BTC sent to subsequent user");
					return RedirectToAction(nameof(Index));
				}
				
			}
            return RedirectToAction(nameof(Index));
		}		      
           
    }
}