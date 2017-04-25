namespace JwDev.Base.Models
{
	public class UserInfo
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }

		public UserInfo()
		{
			CompanyId = 1000;
			CompanyName = "JW-PLAY";
			UserId = 1000;
			UserName = "TEST";
		}
	}
}
