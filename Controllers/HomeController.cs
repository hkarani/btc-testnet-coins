using btcTestnetCoins.Model;
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
        public ActionResult Index(Payout payout)
        {
            string? address = payout.Address;
            Console.WriteLine(address);

            return View(payout);
        }
    }
}