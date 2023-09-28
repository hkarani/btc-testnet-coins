using btcTestnetCoins.Models;
using Microsoft.AspNetCore.Mvc;

namespace btcTestnetCoins.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult SendBitcoin(Payout payout)
		{
           
            
			return View(payout);
           
		}
           
           
    }
}