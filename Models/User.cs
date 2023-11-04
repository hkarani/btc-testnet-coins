namespace BTCTestnetCoins.Models
{
	public class User
	{
		public bool eligible;

		public Guid? Id { get; set; }
		public string? IpAddress { get; set; }
		public DateTime? FirstTimeAccesed { get; set; }
		public DateTime LastAccesed { get; set; }		
		public int? NumberOfTimesAccessed { get; set; }
		public bool? IsBlocked { get; set; } = false;
		public bool? IsEligible
		{
			get;
			set;
		}

		public User()
		{
			Id = Guid.NewGuid();
			IsEligible = DateTime.Now.Subtract(LastAccesed).TotalDays > 3;
		}

	}
}
