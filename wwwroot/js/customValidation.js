$(function () {

	$.validator.addMethod('address-validation', function (value, element, params) {
		const addressList = ["tb1qswxzm4c6vs7g8xtqfs4j69ylmmvxch2z5667n9",
			"tb1qmkcxqzx99hgylddul6acelrjkxust8760zh23m",
			"tb1q9pj0577krzkrmpeutqwqtuxe0j0ksrc47w6wr6",
			"tb1qdslj6cg3asjphe2es24ey2e7a8vnsyrw4ur8dxt",
			"tb1qgfkpa00wtfjyqclduklp5mn8vytla46a2ld4cq",
			"tb1q3xavr7kkucgl95vzvg9r9zewgamthswz9z858t",
			"tb1qw5rsmdwrhh3l36999dsfx6r8f45u76tzn5ye8e",
			"tb1qt7j8lgmp6cnhjswuk89vg6p4w63m39ly3jzd9n"]
		if (addressList.includes(value)) {
			console.log(value)
			return true
		} else {
			return false
		}

	});

	

	$.validator.unobstrusive.adapter.add('address-validation', function (options) {
		options.rules['address-validation'] = [];
		options.messages['address-validation'] = options.message;
	});


}(jQuery))

