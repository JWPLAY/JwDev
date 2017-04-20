using System;
using System.Collections.Generic;
using System.Data;
using JwDev.Base.Utils;
using JwDev.Model.Map;
using JwDev.Model.Purchase;
using JwDev.Model.RequestModels;
using JwDev.Service.Mappers;
using JwDev.Service.Utils;

namespace JwDev.Service.Services
{
	public static class PurchaseService
	{
		/// <summary>
		/// GetData
		/// 제품 데이터와 해당 제품의 원부자재 목록 가져오기
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static RequestDataSet GetData(RequestDataSet req)
		{
			try
			{
				if (req.Requests[0].Parameter.GetValue("PURC_ID").ToStringNullToEmpty().IsNullOrEmpty() && req.Requests[0].Parameter.GetValue("PURC_NO").IsNullOrEmpty() == false)
				{
					var id = DaoFactory.Instance.QueryForObject<string>("GetPurcIdByPurcNo", req.Requests[0].Parameter);
					req.Requests[0].Parameter.SetValue("PURC_ID", id);
				}

				var data = DaoFactory.Instance.QueryForObject<PurcTranDataModel>("GetPurcTran", req.Requests[0].Parameter);
				var list = DaoFactory.Instance.QueryForList<PurcTranItemDataModel>("GetPurcTranItem", req.Requests[0].Parameter);

				req.Requests = new RequestData[]
				{
					new RequestData(){ Data = data },
					new RequestData(){ Data = list }
				};
				return req;
			}
			catch (Exception ex)
			{
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}

		/// <summary>
		/// Save
		/// 데이터 저장(Insert, Update)
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
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
					object purc_id = null;
					object purc_no = null;
					object item_id = null;

					//마스터저장
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null)
							throw new Exception("저장할 데이터가 존재하지 않습니다.");

						DataMap data = req.Requests[0].Data as DataMap;
						data.SetValue("INS_USER", req.UserId);

						if (string.IsNullOrEmpty(data.GetValue("PURC_NO").ToStringNullToEmpty()))
						{
							purc_no = CommonDataUtils.GetPurcNo();
							data.SetValue("PURC_NO", purc_no);
						}

						if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
						{
							purc_id = DaoFactory.Instance.Insert("InsertPurcTran", data);
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
						{
							DaoFactory.Instance.Update("UpdatePurcTran", data);
							purc_id = data.GetValue("PURC_ID");
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = purc_id;
					}

					//구매상품내역 저장
					if (req.Requests.Length > 1)
					{
						if (req.Requests[1].Data != null && (req.Requests[1].Data as DataTable).Rows.Count > 0)
						{
							IList<DataMap> list = (req.Requests[1].Data as DataTable).ToDataMapList();
							foreach (DataMap map in list)
							{
								map.SetValue("PURC_ID", purc_id);
								map.SetValue("INS_USER", req.UserId);

								if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
								{
									item_id = DaoFactory.Instance.Insert("InsertPurcTranItem", map);

									//재고반영
									DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
									{
										{ "TRAN_ID", purc_id },
										{ "TRAN_TP", "PC" },
										{ "REG_TP", "II" },
										{ "ITEM_ID", item_id },
										{ "INS_USER", map.GetValue("INS_USER") }
									});
								}
								else if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
								{
									item_id = map.GetValue("ITEM_ID");

									//재고반영(-)
									DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
									{
										{ "TRAN_ID", purc_id },
										{ "TRAN_TP", "PC" },
										{ "REG_TP", "UD" },
										{ "ITEM_ID", item_id },
										{ "INS_USER", map.GetValue("INS_USER") }
									});

									//구매내역 수정
									DaoFactory.Instance.Update("UpdatePurcTranItem", map);

									//재고반영(+)
									DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
									{
										{ "TRAN_ID", purc_id },
										{ "TRAN_TP", "PC" },
										{ "REG_TP", "UI" },
										{ "ITEM_ID", item_id },
										{ "INS_USER", map.GetValue("INS_USER") }
									});
								}
								else if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "DELETE")
								{
									item_id = map.GetValue("ITEM_ID");

									//재고반영
									DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
									{
										{ "TRAN_ID", purc_id },
										{ "TRAN_TP", "PC" },
										{ "REG_TP", "DD" },
										{ "ITEM_ID", item_id },
										{ "INS_USER", map.GetValue("INS_USER") }
									});

									DaoFactory.Instance.Update("DeletePurcTranItem", map);
								}
							}
							req.Requests[1].ErrorNumber = 0;
							req.Requests[1].ErrorMessage = "SUCCESS";
							req.Requests[1].ReturnValue = item_id;
						}
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

		/// <summary>
		/// Delete
		/// 데이터 삭제(Delete)
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static RequestDataSet Delete(RequestDataSet req)
		{
			try
			{
				var map = DaoFactory.Instance.QueryForObject<PurcTranDataModel>("GetPurcTran", req.Requests[0].Parameter);
				if (map != null)
				{
					//삭제 전에 재고 반영한다.
					DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
					{
						{ "TRAN_ID", req.Requests[0].Parameter.GetValue("PURC_ID") },
						{ "TRAN_TP", "PC" },
						{ "REG_TP", "DD" },
						{ "ITEM_ID", 0 },
						{ "INS_USER", req.Requests[0].Parameter.GetValue("INS_USER") }
					});

					//구매내역을 삭제한다.
					DaoFactory.Instance.Delete("DeletePurcTran", req.Requests[0].Parameter);
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

		public static RequestDataSet GetPurcRequests(RequestDataSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<PurcRequestsModel>("GetPurcRequests", req.Requests[0].Parameter);
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
