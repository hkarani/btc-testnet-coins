using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BTCTestnetCoins.Models
{
	public class CustomAddressValidation: Attribute, IModelValidator
	{
		public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
		{
			
			string testnetRegexPattern = @"\b(tb(0([ac-hj-np-z02-9]{39}|[ac-hj-np-z02-9]{59})|1[ac-hj-np-z02-9]{8,87})|[mn2][a-km-zA-HJ-NP-Z1-9]{25,39})\b";
			Regex regex = new Regex(testnetRegexPattern);
			bool isMatch = regex.IsMatch(context.Model as string);
			List<string> usedAddressList = new List<string>
			{
				"tb1qswxzm4c6vs7g8xtqfs4j69ylmmvxch2z5667n9",
				"tb1qmkcxqzx99hgylddul6acelrjkxust8760zh23m",
				"tb1q9pj0577krzkrmpeutqwqtuxe0j0ksrc47w6wr6",
				"tb1qdslj6cg3asjphe2es24ey2e7a8vnyrw4ur8dxt",
				"tb1qgfkpa00wtfjyqclduklp5mn8vytla46a2ld4cq",
				"tb1q3xavr7kkucgl95vzvg9r9zewgamthswz9z858t",
				"tb1qw5rsmdwrhh3l36999dsfx6r8f45u76tzn5ye8e",
				"tb1qt7j8lgmp6cnhjswuk89vg6p4w63m39ly3jzd9n"
			};

			if (!isMatch)
			{
				return new List<ModelValidationResult> { 
					new ModelValidationResult("", "Invalid testnet address")
				};
			}else if(usedAddressList.Contains(context.Model as string))
			{
				Console.WriteLine("This is a used address");
				return new List<ModelValidationResult> {
					new ModelValidationResult("", "This is address has already been used. Enter a fresh address")
				};
			}else
			{
				Console.WriteLine("Empty validation strin returned");
				return Enumerable.Empty<ModelValidationResult>();	
				
			}

			
		}
	}
}
