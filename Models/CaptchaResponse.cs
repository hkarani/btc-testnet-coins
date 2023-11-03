using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
namespace BTCTestnetCoins.Models

{
	public class CaptchaResponse
	{
		[NotMapped]
		private string? errorCodes;

		public Guid? CaptchaResponseId { get; set; }
		[JsonProperty("hostname")]
		public string? Ip { get; set; }

		[JsonProperty("success")]
		public bool? Success { get; set; }

		[JsonProperty("challenge_ts")]
		public string? ChallengeTimeStamp { get; set; }

		[JsonProperty("score")]
		public double? CaptchaScore { get; set; }

		[NotMapped]
		[JsonProperty("error-codes")]
		public IEnumerable<string?> ?ErrorCodesList { get; set; }

		public string? ErrorCodes
		{
			get
			{
				return System.Text.Json.JsonSerializer.Serialize(ErrorCodesList);
			}
			set
			{
				string serializedErrorList = System.Text.Json.JsonSerializer.Serialize(ErrorCodesList);
				errorCodes = serializedErrorList;				
			}
		}

		[JsonProperty("action")]
		public string? ActionToResponse { get; set; }
		
		public CaptchaResponse()
		{
			CaptchaResponseId = Guid.NewGuid();
		}
	}
}
