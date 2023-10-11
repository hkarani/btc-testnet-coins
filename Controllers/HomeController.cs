using btcTestnetCoins.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace btcTestnetCoins.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
			var payoutDetails = new PayoutDetails
			{
			};
            return View();
        }

		[HttpPost]
		public IActionResult SendBitcoin(PayoutDetails payoutDetails)
		{
			var payOutDetailsObj = new PayoutDetails
			{
				DestinationAddress = payoutDetails.DestinationAddress,
				Amount = "15.00",
				PaymentMethod = "BTC",
				Approved = "true",

			};

			var payOutDict = new Dictionary<string, string> { 
				["destination"] = payOutDetailsObj.DestinationAddress, 
				["amount"] = payOutDetailsObj.Amount, 
				["paymentMethod"] = payOutDetailsObj.PaymentMethod,
				["approved"] = payOutDetailsObj.Approved 
			};
		
			string payOutDetailsJson = JsonSerializer.Serialize(payOutDict);
			 



			//string baseUrl = "https://docs.btcpayserver.org/api/v1/stores/EEw2ecPHLKTQ3mAJkfR5Y56FF2W4yp8vgJxKQncdNVf7/payouts";
			//string token = "";
			

			//string jsonData = "{\"destination\": \"{}\", \"amount\": \"value2\",  \"paymentMethod\": \"value2\",  \"approved\": \"value2\"}";
			//using (HttpClient client = new HttpClient())
			//{
			//	client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			//	// Create a content object with the JSON data
			//	var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			//	// Send a POST request with JSON data in the request body
			//	HttpResponseMessage response = await client.PostAsync(baseUrl, content);

			//	if (response.IsSuccessStatusCode)
			//	{
			//		string responseContent = await response.Content.ReadAsStringAsync();
			//		Console.WriteLine(responseContent);
			//	}
			//	else
			//	{
			//		Console.WriteLine($"Error: {response.StatusCode}");
			//	}
			//}

			Console.WriteLine(payOutDetailsJson);
			return View(payoutDetails);           
		}
           
           
    }
}