using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BTCTestnetCoins.Models
{
	public class AddressValidationAttribute: ValidationAttribute, IClientModelValidator
	{
		public override bool IsValid(object? value)
		{
			List<string> usedAddressList = new ()
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

			if (usedAddressList.Contains(value)) {
				return true;
			}

			return false;

		}

		public override string FormatErrorMessage(string name)
		{
			return $"This address is used";
		}

		
		public void AddValidation(ClientModelValidationContext context)
		{
			var fieldName = context.Attributes["name"];
			context.Attributes.TryAdd("data-val", "true");
			context.Attributes.TryAdd("data-val-address-validation", FormatErrorMessage(fieldName));
			Console.WriteLine("This function got called");
		}
	}
}
