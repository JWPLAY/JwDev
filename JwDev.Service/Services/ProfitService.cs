using System;
using JwDev.Model.Profit;
using JwDev.Model.RequestModels;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class ProfitService
	{
		public static RequestDataSet GetList(RequestDataSet req)
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
