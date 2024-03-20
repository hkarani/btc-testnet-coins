using Microsoft.AspNetCore.Mvc;
using BTCPayServer.Client.Models;
using BTCTestnetCoins.Models;
using BTCTestnetCoins.Data;
using BTCTestnetCoins.Utilities;
namespace btcTestnetCoins.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			BTCTestnetCoinsDbContext dbCtx = new();	
			var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();		
			var findUserByIP = dbCtx.Users.FirstOrDefault(ip  => ip.IpAddress == userIpAddress);

			if(findUserByIP != null)
			{
				var IsEligible = findUserByIP.IsEligible.GetValueOrDefault() ? "True" : "False";
				var eligibleTime = findUserByIP.LastAccesed.AddDays(2);
				ViewData["EligibleDate"] = eligibleTime.ToString();
				ViewData["Eligiblity"] = IsEligible;
			}
			else
			{
				ViewData["Eligiblity"] = "True";				
			}		
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
				//"Logic for blocked" user
				if (findUserByIP.IsBlocked.GetValueOrDefault())
				{
					TempData["UserStatus"] = "Your Ip has been blocked. Try again later";					
					return RedirectToAction("Index");
				}
				//Logic for ineligible user");
				if (!findUserByIP.IsEligible.GetValueOrDefault())
				{

					var eligibleTime = findUserByIP.LastAccesed.AddDays(2);
					TempData["UserStatus"] = $"Your Ip is ineligile for coins. Try again after {eligibleTime}";				
					return RedirectToAction("Index");
				}

				var (payoutData, Success) = await Payout.SendTestNetCoins(payoutAddress.DestinationAddress);

				//Logic for failed payout transaction
				if (!Success) {
					TempData["Success"] = $"Payout failed. The address you entered is invalid or has already been used on this service";

					return RedirectToAction("Index");
				}

				// Send Testnet Coins to subsequent user");
				if (payoutData.State == PayoutState.AwaitingPayment)
				{

					TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";
					findUserByIP.LastAccesed = DateTime.Now;
					findUserByIP.NumberOfTimesAccessed = +1;
					await dbCtx.AddAsync(findUserByIP);
					await dbCtx.SaveChangesAsync();
					return RedirectToAction("Index");
				}

			}

			if (ModelState.IsValid)
			{				
				var captchaResult = await HandleCaptcha.IsCaptchaValid(response, userIpAddress);
				if(captchaResult.Success == false)
				{
					await dbCtx.AddAsync(captchaResult);
					await dbCtx.SaveChangesAsync();
					TempData["Captcha"] = $"Something bad happened during the bot test. Please reload the page or try again later";
					return RedirectToAction("Index");				
				}

				if(captchaResult.CaptchaScore < 0.5)
				{
					await dbCtx.AddAsync(captchaResult);
					await dbCtx.SaveChangesAsync();
					TempData["Captcha"] = $"You have failed the bot test. Please reload the page and try again later";
					return RedirectToAction("Index");

				}

				User user = new()
				{
					IpAddress = userIpAddress,
					FirstTimeAccesed = DateTime.Now,
					NumberOfTimesAccessed = 1,
					LastAccesed = DateTime.Now

				};

				dbCtx.Users.Add(user);
				await dbCtx.SaveChangesAsync();

				var (payoutData, Success) = await Payout.SendTestNetCoins(payoutAddress.DestinationAddress);

				if (!Success)
				{
					//"Payout failure from BTCPayServer"
					TempData["Success"] = $"Payout failed. The address you entered is invalid or has already been used on this service";									
					return RedirectToAction("Index");
				}

				if (payoutData.State == PayoutState.AwaitingPayment)
				{
					//"BTC sent to subsequent user";

					TempData["Success"] = $"0.002 BTC sent.Awaiting Confimation!";					
					return RedirectToAction("Index");
				}
				
			}
			var captchaResultTest = await HandleCaptcha.IsCaptchaValid(response, userIpAddress);

			TempData["Success"] = $"{captchaResultTest} {response}";

			return RedirectToAction("Index");
		}

		public IActionResult Errors()
		{

			return View();
		}

	}
}