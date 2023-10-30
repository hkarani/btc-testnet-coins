namespace BTCTestnetCoins.Models
{
	public class CaptchaResponse
	{
		public Guid? ResponseId { get; set; }
		public string? Ip { get; set; }
		public bool? Success { get; set; }
		public string? ChallengeTimeStamp { get; set; }		
		public decimal? CaptchaScore { get; set; }
		public IEnumerable<string?> ?ErrorCodes { get; set; }
		public string? ActionToResponse { get; set; }		
	}
}
