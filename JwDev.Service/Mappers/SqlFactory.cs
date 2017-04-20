using System.Data.SqlClient;
using JwDev.Service.Config;
using Microsoft.ApplicationBlocks.Data;

namespace JwDev.Service.Mappers
{
	public class SqlFactory
	{
		public static SqlHelper Instance;
		public static SqlConnection Connection
		{
			get
			{
				return new SqlConnection(DatabaseConfig.ConnectionString);
			}
		}
	}
}
