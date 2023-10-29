namespace BTCTestnetCoins.Models
{
	public class User
	{
		public int Id { get; set; }	
		public string? IpAddress { get; set; }
		public DateTime? FirstTimeAccesed { get; set; }
		public DateTime? LastAccesed { get; private set; }
		public bool Eligible { get; set; }
		public int NumberOfTimesAccessed { get; set; }
		public bool IsBlocked { get; set; }
	}
}
