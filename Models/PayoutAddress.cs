using System.ComponentModel.DataAnnotations;

namespace btcTestnetCSoins.Models
{
	public class PayoutAddress
	{
		[Required]
		public string? DestinationAddress { get; set; }
		
	}
}
