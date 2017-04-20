﻿using System;
using System.Data;
using JwDev.Base.DBTran.Model;
using JwDev.Base.Map;
using JwDev.Base.Utils;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class CustomerService
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
				var customerData = DaoFactory.Instance.QueryForObject<DataMap>("SelectCustomer", req.Requests[0].Parameter);
				var phonesData = DaoFactory.Instance.QueryForList<DataMap>("SelectCustomerPhones", req.Requests[0].Parameter);
				var addressData = DaoFactory.Instance.QueryForList<DataMap>("SelectCustomerAddress", req.Requests[0].Parameter);

				req.Requests = new RequestData[]
				{
					new RequestData() { Data = customerData },
					new RequestData() { Data = phonesData },
					new RequestData() { Data = addressData }
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
					string rowState = string.Empty;
					object customer_id = null;
					object biz_reg_id = null;
					object biz_address_id = null;

					//거래처정보 저장
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null || (req.Requests[0].Data as DataTable).Rows.Count == 0)
							throw new Exception("거래처정보를 저장할 데이터가 존재하지 않습니다.");

						DataMap data = (req.Requests[0].Data as DataTable).ToDataMapList()[0];
						data.SetValue("INS_USER", req.UserId);

						rowState = data.GetValue("ROWSTATE").ToStringNullToEmpty();

						//주소등록
						if (rowState == "INSERT" || rowState == "UPDATE")
						{
							var addr = DaoFactory.Instance.QueryForObject("GetAddressId", new DataMap() { { "ADDRESS_ID", data.GetValue("ADDRESS_ID") } });
							if (addr.ToStringNullToEmpty().IsNullOrEmpty())
							{
								if (!data.GetValue("POST_NO").ToStringNullToEmpty().IsNullOrEmpty() ||
									!data.GetValue("ZONE_NO").ToStringNullToEmpty().IsNullOrEmpty() ||
									!data.GetValue("ADDRESS1").ToStringNullToEmpty().IsNullOrEmpty() ||
									!data.GetValue("ADDRESS2").ToStringNullToEmpty().IsNullOrEmpty())
								{
									biz_address_id = DaoFactory.Instance.Insert("InsertAddress", data);

									if (biz_address_id.ToStringNullToEmpty().IsNullOrEmpty())
										throw new Exception("사업자정보의 주소를 저장하는 중 오류가 발생하였습니다.");

									data.SetValue("ADDRESS_ID", biz_address_id);
								}
							}
							else
							{
								DaoFactory.Instance.Update("UpdateAddress", data);
							}

							var bizId = DaoFactory.Instance.QueryForObject("GetBizRegId", new DataMap() { { "BIZ_REG_ID", data.GetValue("BIZ_REG_ID") } });
							if (bizId.ToStringNullToEmpty().IsNullOrEmpty())
							{
								if (!data.GetValue("BIZ_REG_NO").ToStringNullToEmpty().IsNullOrEmpty())
								{
									biz_reg_id = DaoFactory.Instance.Insert("InsertBizRegCe", data);

									if (biz_reg_id.ToStringNullToEmpty().IsNullOrEmpty())
										throw new Exception("사업자정보를 저장하지 못했습니다.");

									data.SetValue("BIZ_REG_ID", biz_reg_id);
								}
							}
							else
							{
								DaoFactory.Instance.Update("UpdateBizRegCe", data);
							}
						}

						if (rowState == "INSERT")
						{
							customer_id = DaoFactory.Instance.Insert("InsertCustomer", data);

							if (customer_id.ToStringNullToEmpty().IsNullOrEmpty())
								throw new Exception("거래처정보를 저장하지 못했습니다.");
						}
						else if (rowState == "UPDATE")
						{
							DaoFactory.Instance.Update("UpdateCustomer", data);
							customer_id = data.GetValue("CUSTOMER_ID");
						}
						else if (rowState == "DELETE")
						{
							DaoFactory.Instance.Update("DeleteCustomer", data);
							customer_id = data.GetValue("CUSTOMER_ID");
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = customer_id;
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
				var map = DaoFactory.Instance.QueryForObject<DataMap>("SelectCustomer", req.Requests[0].Parameter);
				if (map != null)
				{
					DaoFactory.Instance.Insert("DeleteCustomer", req.Requests[0].Parameter);
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

		public static RequestDataSet SaveCustomerAddress(RequestDataSet req)
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
					string rowState = string.Empty;
					object reg_id = null;
					object address_id = null;

					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null || (req.Requests[0].Data as DataTable).Rows.Count == 0)
							throw new Exception("저장할 데이터가 존재하지 않습니다.");

						foreach (DataRow row in (req.Requests[0].Data as DataTable).Rows)
						{
							DataMap map = row.ToDataMap();
							if (map == null || map.Count == 0)
								continue;

							map.SetValue("INS_USER", req.UserId);
							rowState = map.GetValue("ROWSTATE").ToString();

							if (rowState == "INSERT" || rowState == "UPDATE")
							{
								var addr = DaoFactory.Instance.QueryForObject("GetAddressId", new DataMap() { { "ADDRESS_ID", map.GetValue("ADDRESS_ID") } });
								if (addr.ToStringNullToEmpty().IsNullOrEmpty())
								{
									if (!map.GetValue("POST_NO").ToStringNullToEmpty().IsNullOrEmpty() ||
										!map.GetValue("ZONE_NO").ToStringNullToEmpty().IsNullOrEmpty() ||
										!map.GetValue("ADDRESS1").ToStringNullToEmpty().IsNullOrEmpty() ||
										!map.GetValue("ADDRESS2").ToStringNullToEmpty().IsNullOrEmpty())
									{
										address_id = DaoFactory.Instance.Insert("InsertAddress", map);

										if (address_id.ToStringNullToEmpty().IsNullOrEmpty())
											throw new Exception("주소를 저장하는 중 오류가 발생하였습니다.");

										map.SetValue("ADDRESS_ID", address_id);
									}
								}
								else
								{
									DaoFactory.Instance.Update("UpdateAddress", map);
								}
							}

							if (rowState == "INSERT")
							{
								reg_id = DaoFactory.Instance.Insert("InsertCustomerAddress", map);

								if (reg_id.ToStringNullToEmpty().IsNullOrEmpty())
									throw new Exception("거래처 주소정보를 저장하지 못했습니다.");
							}
							else if (rowState == "UPDATE")
							{
								DaoFactory.Instance.Update("UpdateCustomerAddress", map);
								reg_id = map.GetValue("REG_ID");
							}
							else if (rowState == "DELETE")
							{
								DaoFactory.Instance.Update("DeleteCustomer", map);
								reg_id = map.GetValue("REG_ID");
							}
							req.Requests[0].ErrorNumber = 0;
							req.Requests[0].ErrorMessage = "SUCCESS";
							req.Requests[0].ReturnValue = reg_id;
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
	}
}
