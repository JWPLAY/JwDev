using System;
using JwDev.Base.DBTran.Controller;
using JwDev.Base.Map;
using JwDev.Base.Utils;

namespace JwDev.Core.Utils
{
	public static class CommonRequest
	{
		public static int SaveAddress(DataMap map)
		{
			try
			{
				var res = RequestHelper.Execute("Base", "Save", "Address", map, "ADDRESS_ID");
				if (res.ErrorNumber != 0)
					throw new Exception(res.ErrorMessage);

				if (res.Requests[0].ReturnValue == null)
					throw new Exception("반환값이 정확하지 않습니다.");

				return res.Requests[0].ReturnValue.ToIntegerNullToZero();
			}
			catch
			{
				throw;
			}
		}
	}
}
