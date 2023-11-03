namespace BTCTestnetCoins.Models
{
	public class User
	{
		public bool eligible;
		public Guid? Id { get; set; }
		public string? IpAddress { get; set; }
		public DateTime? FirstTimeAccesed { get; set; }
		public DateTime LastAccesed { get; private set; }
		
		public int NumberOfTimesAccessed { get; set; }
		public bool IsBlocked { get; set; }
		public bool IsEligible
		{
			get { return eligible; }
			set
			{
				TimeSpan timeDifference = DateTime.Now.Subtract(LastAccesed);
				if (timeDifference.TotalDays > 3)
				{
					eligible = true;
				}
			}
		}

		public User()
		{
			Id = Guid.NewGuid();
		}

	}
}
