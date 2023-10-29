using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace BTCTestnetCoins.Models
{


	public class PayoutAddress
	{

		[Required(ErrorMessage = "Please enter an address")]
		[RegularExpression(@"\b(tb(0([ac-hj-np-z02-9]{39}|[ac-hj-np-z02-9]{59})|1[ac-hj-np-z02-9]{8,87})|[mn2][a-km-zA-HJ-NP-Z1-9]{25,39})\b", 
			ErrorMessage="Invalid Testnet Address")]
		public string? DestinationAddress { get; set; } = "";
		public string? GoogleCaptureToken { get; set; }

	}
	
}
