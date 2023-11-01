using Newtonsoft.Json;

namespace BTCTestnetCoins.Models
{
	public class CaptchaResponse
	{
		public Guid? CaptchaResponseId { get; set; }
		[JsonProperty("hostname")]
		public string? Ip { get; set; }

		[JsonProperty("success")]
		public bool? Success { get; set; }

		[JsonProperty("challenge_ts")]
		public string? ChallengeTimeStamp { get; set; }

		[JsonProperty("score")]
		public double? CaptchaScore { get; set; }

		[JsonProperty("error-codes")]
		public IEnumerable<string?> ?ErrorCodes { get; set; }

		[JsonProperty("action")]
		public string? ActionToResponse { get; set; }
		
		public CaptchaResponse()
		{
			CaptchaResponseId = Guid.NewGuid();
		}
	}
}
