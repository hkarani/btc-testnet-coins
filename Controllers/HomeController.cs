using btcTestnetCoins.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

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
            using (var httpClient = new HttpClient()) {
				
				object pullPayment =
                {
                    destination: payout.Address,
					amount:"",
                    paymentMethod: ""

				}
            }
				
                    "amount":"",
                    "paymentMethod": ""

            
                var response = 
            }   
            
			return View(payout);
           
		}
           
           
    }
}