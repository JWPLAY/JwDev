using System;
using System.Collections.Generic;
using System.Data;
using JwDev.Base.Utils;
using JwDev.Model.Codes;
using JwDev.Model.Map;
using JwDev.Model.WasModels;
using JwDev.Service.Mappers;
using JwDev.Service.Utils;

namespace JwDev.Service.Services
{
	public static class ProductService
	{
		public static WasRequestSet GetList(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<ProductListModel>("SelectProducts", req.Requests[0].Parameter);
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
				var product = DaoFactory.Instance.QueryForObject<ProductDataModel>("SelectProduct", req.Requests[0].Parameter);
				var materials = DaoFactory.Instance.QueryForList<DataMap>("SelectProductMaterials", req.Requests[0].Parameter);

				req.Requests = new WasRequest[]
				{
					new WasRequest() { Data = product },
					new WasRequest() { Data = materials }
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
					object product_id = null;
					object reg_id = null;
					string product_code = string.Empty;

					//상품정보 저장
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null)
							throw new Exception("상품정보를 저장할 데이터가 존재하지 않습니다.");

						DataMap data = req.Requests[0].Data as DataMap;
						data.SetValue("INS_USER", req.UserId);

						if (string.IsNullOrEmpty(data.GetValue("PRODUCT_CODE").ToStringNullToEmpty()))
						{
							product_code = CommonDataUtils.GetProductCode(data.GetValue("PRODUCT_TYPE").ToString());
							data.SetValue("PRODUCT_CODE", product_code);
						}

						if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
						{
							product_id = DaoFactory.Instance.Insert("InsertProduct", data);
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
						{
							DaoFactory.Instance.Update("UpdateProduct", data);
							product_id = data.GetValue("PRODUCT_ID");
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "DELETE")
						{
							DaoFactory.Instance.Update("DeleteProduct", data);
							product_id = data.GetValue("PRODUCT_ID");
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = product_id;
					}

					//원부자재정보 저장
					if (req.Requests.Length > 1)
					{
						if (req.Requests[1].Data != null && (req.Requests[1].Data as DataTable).Rows.Count > 0)
						{
							IList<DataMap> list = (req.Requests[1].Data as DataTable).ToDataMapList();
							foreach (DataMap map in list)
							{
								map.SetValue("PRODUCT_ID", product_id);
								map.SetValue("INS_USER", req.UserId);

								if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
								{
									reg_id = DaoFactory.Instance.Insert("InsertProductMaterial", map);
								}
								else if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
								{
									DaoFactory.Instance.Update("UpdateProductMaterial", map);
									reg_id = map.GetValue("REG_ID");
								}
								else if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "DELETE")
								{
									DaoFactory.Instance.Update("DeleteProductMaterial", map);
									reg_id = map.GetValue("REG_ID");
								}
							}
							req.Requests[1].ErrorNumber = 0;
							req.Requests[1].ErrorMessage = "SUCCESS";
							req.Requests[1].ReturnValue = product_id;
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

		public static WasRequestSet Delete(WasRequestSet req)
		{
			try
			{
				var map = DaoFactory.Instance.QueryForObject<DataMap>("SelectProduct", req.Requests[0].Parameter);
				if (map != null)
				{
					DaoFactory.Instance.Insert("DeleteProduct", req.Requests[0].Parameter);
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

		public static WasRequestSet GetSalesPriceList(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<SalesPriceListModel>("GetSalesPriceList", req.Requests[0].Parameter);
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

		public static WasRequestSet GetSalesPriceData(WasRequestSet req)
		{
			try
			{
				var data = DaoFactory.Instance.QueryForObject<SalesPriceDataModel>("GetSalesPriceData", req.Requests[0].Parameter);
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

		public static WasRequestSet SaveSalesPrice(WasRequestSet req)
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
					object product_id = null;
					object reg_id = null;
					
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null)
							throw new Exception("상품정보를 저장할 데이터가 존재하지 않습니다.");

						DataMap data = req.Requests[0].Data as DataMap;
						data.SetValue("INS_USER", req.UserId);

						product_id = data.GetValue("PRODUCT_ID");

						if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
						{
							reg_id = DaoFactory.Instance.Insert("InsertSalesPrice", data);
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
						{
							reg_id = data.GetValue("REG_ID");
							DaoFactory.Instance.Update("UpdateSalesPrice", data);
							
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "DELETE")
						{
							reg_id = data.GetValue("REG_ID");
							DaoFactory.Instance.Update("DeleteSalesPrice", data);
							
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = reg_id;
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

		public static WasRequestSet GetDataByBarcode(WasRequestSet req)
		{
			try
			{
				var data = DaoFactory.Instance.QueryForObject<DataMap>("GetDataByBarcode", req.Requests[0].Parameter);
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
	}
}
