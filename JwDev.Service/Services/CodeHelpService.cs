using System;
using System.Collections.Generic;
using JwDev.Base.Map;
using JwDev.Base.Utils;
using JwDev.Base.DBTran.Model;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class CodeHelpService
	{
		public static RequestDataSet GetCodeHelpLookup(RequestDataSet reqset)
		{
			try
			{
				if (reqset.Requests == null || reqset.Requests.Length == 0)
					throw new Exception("처리요청이 정확하지 않습니다.");

				foreach (RequestData req in reqset.Requests)
				{
					IList<DataMap> list;
					var parentCode = req.Parameter.GetValue("PARENT_CODE").ToStringNullToEmpty().ToCamelCase();
					switch (parentCode)
					{
						case "Module":
						case "View":
						case "Menu":
						case "Group":
						case "Role":
						case "Help":
						case "Code":
						case "CodeGroup":
						case "Customer":
						case "Product":
						case "AllProduct":
						case "ProdProduct":
						case "Material":
						case "Purcitem":
						case "Saleitem":
						case "Department":
						case "Employee":
						case "Project":
						case "Work":
						case "Task":
						case "MasterPlanResource":
						case "MasterPlan":
						case "System":
							list = DaoFactory.Instance.QueryForList<DataMap>(string.Format("GetCodeHelp{0}List", parentCode), req.Parameter);
							break;
						default:
							list = DaoFactory.Instance.QueryForList<DataMap>("GetCodeHelp", req.Parameter);
							break;
					}
					req.Data = ConvertUtils.DataMapListToDataTable(list, req.SqlId);
				}

				return reqset;
			}
			catch (Exception ex)
			{
				reqset.ErrorNumber = ex.HResult;
				reqset.ErrorMessage = ex.Message;
				return reqset;
			}
		}
	}
}
