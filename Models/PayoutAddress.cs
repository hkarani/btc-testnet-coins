using System.ComponentModel.DataAnnotations;

namespace btcTestnetCSoins.Models
{
	public class PayoutAddress
	{
		[Required(ErrorMessage = "*Please enter a testnet address to receive coins")]
		[RegularExpression(@"\b(tb(0([ac-hj-np-z02-9]{39}|[ac-hj-np-z02-9]{59})|1[ac-hj-np-z02-9]{8,87})|[mn2][a-km-zA-HJ-NP-Z1-9]{25,39})\b",
			ErrorMessage = "*Invalid Bitcoin testnet address")]
		public string? DestinationAddress { get; set; }
		
	}
}
