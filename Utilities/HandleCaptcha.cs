﻿using BTCTestnetCoins.Data;
using BTCTestnetCoins.Models;
using Newtonsoft.Json;

namespace BTCTestnetCoins.Utilities
{
	public class HandleCaptcha
	{
		public static async Task<CaptchaResponse> IsCaptchaValid(string response, string userIpAddress)
		{
			BTCTestnetCoinsDbContext dbCtx = new();
			try
			{
				var secretKey = Environment.GetEnvironmentVariable("GOOGLE_RECAPTCHA_SECRET_KEY");
				using var client = new HttpClient();
				var payload = new Dictionary<string, string>
				{
					{"secret", secretKey },
					{"response", response },
					{"remoteip", userIpAddress }
				};

				var content = new FormUrlEncodedContent(payload);
				var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
				var captchaRepsoneJson = await verify.Content.ReadAsStringAsync();
				var captchaResult = JsonConvert.DeserializeObject<CaptchaResponse>(captchaRepsoneJson);

				if(captchaResult == null)
				{
					return new CaptchaResponse { };
				}
				return captchaResult;			

			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
