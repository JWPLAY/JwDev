using JwDev.Base.Models;
using JwDev.Model.Map;

namespace JwDev.Base.Variables
{
	public static class GlobalVar
	{
		static GlobalVar()
		{
			Server = new ServerInfo();
			Skin = new SkinInfo();
			Settings = new DataMap();
		}

		public static string Version { get; set; }
		public static int CompanyId { get; set; }
		public static int UserId { get; set; }
		public static ServerInfo Server { get; set; }
		public static SkinInfo Skin { get; set; }
		public static DataMap Settings { get; set; }
	}
}
