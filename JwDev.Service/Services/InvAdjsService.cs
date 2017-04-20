using System;
using JwDev.Base.Utils;
using JwDev.Model.Inventory;
using JwDev.Model.Map;
using JwDev.Model.RequestModels;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class InvAdjsService
	{
		public static RequestDataSet GetList(RequestDataSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<InvAdjsListModel>("GetInvAdjsList", req.Requests[0].Parameter);
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

		public static RequestDataSet GetData(RequestDataSet req)
		{
			try
			{
				var data = DaoFactory.Instance.QueryForObject<InvAdjsDataModel>("GetInvAdjsData", req.Requests[0].Parameter);
				req.Requests[0].Data = data;
				return req;
			}
			catch (Exception ex)
			{
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}

		public static RequestDataSet Save(RequestDataSet req)
		{
			bool isTran = false;

			try
			{
				if (req == null)
					throw new Exception("처리할 요청이 정확하지 않습니다.");

				if (req.Requests == null || req.Requests.Length == 0)
					throw new Exception("처리할 데이터가 없습니다.");

				DaoFactory.Instance.BeginTransaction();
				isTran = true;

				try
				{
					object reg_id = null;
					
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null)
							throw new Exception("저장할 데이터가 존재하지 않습니다.");

						DataMap data = req.Requests[0].Data as DataMap;
						data.SetValue("INS_USER", req.UserId);

						if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
						{
							//생산트랜 저장
							reg_id = DaoFactory.Instance.Insert("InsertInvAdjs", data);

							//재고반영
							DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
							{
								{ "TRAN_ID", reg_id },
								{ "TRAN_TP", "IV" },
								{ "REG_TP", "II" },
								{ "INS_USER", data.GetValue("INS_USER") }
							});
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
						{
							reg_id = data.GetValue("REG_ID");

							//수정 전 재고반영(-)
							DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
							{
								{ "TRAN_ID", reg_id },
								{ "TRAN_TP", "IV" },
								{ "REG_TP", "UD" },
								{ "INS_USER", data.GetValue("INS_USER") }
							});

							//생산수정
							DaoFactory.Instance.Update("UpdateInvAdjs", data);

							//수정 후 재고반영(+)
							DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
							{
								{ "TRAN_ID", reg_id },
								{ "TRAN_TP", "IV" },
								{ "REG_TP", "UI" },
								{ "INS_USER", data.GetValue("INS_USER") }
							});
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "DELETE")
						{
							reg_id = data.GetValue("REG_ID");

							//재고반영
							DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
							{
								{ "TRAN_ID", reg_id },
								{ "TRAN_TP", "IV" },
								{ "REG_TP", "DD" },
								{ "INS_USER", data.GetValue("INS_USER") }
							});

							//생산트랜삭제
							DaoFactory.Instance.Update("DeleteInvAdjs", data);
							
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = reg_id;
					}
					
					if (isTran)
						DaoFactory.Instance.CommitTransaction();
				}
				catch(Exception ex)
				{
					if (isTran)
						DaoFactory.Instance.RollBackTransaction();

					throw new Exception(ex.Message);
				}
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
