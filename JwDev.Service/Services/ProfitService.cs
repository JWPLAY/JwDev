using System;
using JwDev.Model.Profit;
using JwDev.Model.WasModels;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class ProfitService
	{
		public static WasRequestSet GetList(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<ProfitListModel>("GetProfitList", req.Requests[0].Parameter);
				req.Requests[0].Data = list;
				return req;
			}
			catch (Exception ex)
			{
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}
	}
}
