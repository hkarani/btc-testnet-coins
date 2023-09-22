using Microsoft.AspNetCore.Mvc;

namespace btcTestnetCoins.Controllers
{
	public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}