using System;
using System.Collections.Generic;
using System.Data;
using JwDev.Base.Utils;
using JwDev.Model.Map;
using JwDev.Model.Sales;
using JwDev.Model.WasModels;
using JwDev.Service.Mappers;
using JwDev.Service.Utils;

namespace JwDev.Service.Services
{
	public static class SalesService
	{
		public static WasRequestSet GetList(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<SaleRequestsModel>("GetSaleRequests", req.Requests[0].Parameter);
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

		public static WasRequestSet GetData(WasRequestSet req)
		{
			try
			{
				var saletran = DaoFactory.Instance.QueryForObject<SaleTranDataModel>("GetSaleTran", req.Requests[0].Parameter);
				var saleitem = DaoFactory.Instance.QueryForList<SaleTranItemDataModel>("GetSaleTranItem", req.Requests[0].Parameter);

				req.Requests = new WasRequest[]
				{
					new WasRequest() { Data = saletran },
					new WasRequest() { Data = saleitem }
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

		public static WasRequestSet Save(WasRequestSet req)
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
					object sale_id = null;
					object sale_no = null;
					object item_id = null;

					//마스터저장
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null)
							throw new Exception("저장할 데이터가 존재하지 않습니다.");

						DataMap data = (req.Requests[0].Data as DataTable).ToDataMapList()[0];
						data.SetValue("INS_USER", req.UserId);

						if (string.IsNullOrEmpty(data.GetValue("SALE_NO").ToStringNullToEmpty()))
						{
							sale_no = CommonDataUtils.GetSaleNo();
							data.SetValue("SALE_NO", sale_no);
						}

						if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
						{
							sale_id = DaoFactory.Instance.Insert("InsertSaleTran", data);
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
						{
							DaoFactory.Instance.Update("UpdateSaleTran", data);
							sale_id = data.GetValue("SALE_ID");
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = sale_id;
					}

					//품목 저장
					if (req.Requests.Length > 1)
					{
						if (req.Requests[1].Data != null && (req.Requests[1].Data as DataTable).Rows.Count > 0)
						{
							IList<DataMap> list = (req.Requests[1].Data as DataTable).ToDataMapList();
							foreach (DataMap map in list)
							{
								map.SetValue("SALE_ID", sale_id);
								map.SetValue("INS_USER", req.UserId);

								if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
								{
									item_id = DaoFactory.Instance.Insert("InsertSaleTranItem", map);
									DaoFactory.Instance.Update("UpdateSaleTranSum", map);

									//재고반영
									DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
									{
										{ "TRAN_ID", sale_id },
										{ "TRAN_TP", "SL" },
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
										{ "TRAN_ID", sale_id },
										{ "TRAN_TP", "SL" },
										{ "REG_TP", "UD" },
										{ "ITEM_ID", item_id },
										{ "INS_USER", map.GetValue("INS_USER") }
									});

									//구매내역 수정
									DaoFactory.Instance.Update("UpdateSaleTranItem", map);
									DaoFactory.Instance.Update("UpdateSaleTranSum", map);

									//재고반영(+)
									DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
									{
										{ "TRAN_ID", sale_id },
										{ "TRAN_TP", "SL" },
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
										{ "TRAN_ID", sale_id },
										{ "TRAN_TP", "SL" },
										{ "REG_TP", "DD" },
										{ "ITEM_ID", item_id },
										{ "INS_USER", map.GetValue("INS_USER") }
									});

									DaoFactory.Instance.Update("DeleteSaleTranItem", map);
									DaoFactory.Instance.Update("UpdateSaleTranSum", map);
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
				catch (Exception ex)
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
		public static WasRequestSet Delete(WasRequestSet req)
		{
			try
			{
				var map = DaoFactory.Instance.QueryForObject<SaleTranDataModel>("GetSaleTran", req.Requests[0].Parameter);
				if (map != null)
				{
					//삭제 전에 재고 반영한다.
					DaoFactory.Instance.QueryForObject<int>("BatchInventory", new DataMap()
					{
						{ "TRAN_ID", req.Requests[0].Parameter.GetValue("SALE_ID") },
						{ "TRAN_TP", "SL" },
						{ "REG_TP", "DD" },
						{ "ITEM_ID", 0 },
						{ "INS_USER", req.UserId }
					});
					
					DaoFactory.Instance.Delete("DeleteSaleTran", req.Requests[0].Parameter);
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

		public static WasRequestSet GetCategory(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<SaleCategoryListModel>("GetSaleCategory", req.Requests[0].Parameter);
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
		public static WasRequestSet GetProducts(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<SaleProductsListModel>("GetSaleProducts", req.Requests[0].Parameter);
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

		public static WasRequestSet GetSaleSumData(WasRequestSet req)
		{
			try
			{
				var data = DaoFactory.Instance.QueryForObject<SaleSumDataModel>("GetSaleSumData", req.Requests[0].Parameter);
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

		public static WasRequestSet GetSaleStat(WasRequestSet req)
		{
			try
			{
				var data1 = DaoFactory.Instance.QueryForList<DataMap>("GetSaleProductList", req.Requests[0].Parameter);
				var data2 = DaoFactory.Instance.QueryForList<DataMap>("GetSaleCustomerList", req.Requests[0].Parameter);
				var data3 = DaoFactory.Instance.QueryForList<DataMap>("GetSaleCategoryList", req.Requests[0].Parameter);
				var data4 = DaoFactory.Instance.QueryForList<DataMap>("GetSalePayTypeList", req.Requests[0].Parameter);

				req.Requests = new WasRequest[]
				{
					new WasRequest() { Data = data1 },
					new WasRequest() { Data = data2 },
					new WasRequest() { Data = data3 },
					new WasRequest() { Data = data4 }
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

		public static WasRequestSet GetSaleDashboard(WasRequestSet req)
		{
			try
			{
				var data1 = DaoFactory.Instance.QueryForList<DataMap>("GetSaleDaily", req.Requests[0].Parameter);
				var data2 = DaoFactory.Instance.QueryForList<DataMap>("GetSaleMonthly", req.Requests[0].Parameter);

				req.Requests = new WasRequest[]
				{
					new WasRequest() { Data = data1 },
					new WasRequest() { Data = data2 }
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
	}
}
