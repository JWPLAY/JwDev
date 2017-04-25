using JwDev.Base.Constants;

namespace JwDev.Base.Models
{
	public class ServerInfo
	{
		public string WasMode { get; set; }
		public string WasURL { get; set; }
		public string DatabaseId { get; set; }

		public ServerInfo()
		{
			WasMode = ServerConsts.MODE;
			WasURL = ServerConsts.REAL;
			DatabaseId = "REAL";
		}
	}
}
