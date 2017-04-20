﻿using System;
using System.Collections.Generic;
using System.Data;
using JwDev.Base.DBTran.Model;
using JwDev.Base.Map;
using JwDev.Base.Utils;
using JwDev.Model.System;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class DatabaseService
	{
		public static RequestDataSet GetTableList(RequestDataSet reqset)
		{
			try
			{
				if (reqset.Requests == null || reqset.Requests.Length == 0)
					throw new Exception("처리요청이 없습니다.");

				foreach (RequestData req in reqset.Requests)
				{
					req.Parameter.SetValue("INS_USER", reqset.UserId);
					req.Data = DaoFactory.Instance.QueryForList<TableListModel>(req.SqlId, req.Parameter);
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

		public static RequestDataSet GetColumnList(RequestDataSet reqset)
		{
			try
			{
				if (reqset.Requests == null || reqset.Requests.Length == 0)
					throw new Exception("처리요청이 없습니다.");

				foreach (RequestData req in reqset.Requests)
				{
					req.Parameter.SetValue("INS_USER", reqset.UserId);
					req.Data = DaoFactory.Instance.QueryForList<ColumnListModel>(req.SqlId, req.Parameter);
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
					object table_id = null;
					object column_id = null;

					//테이블
					if (req.Requests.Length > 0)
					{
						if (req.Requests[0].Data == null)
							throw new Exception("저장할 데이터가 존재하지 않습니다.");

						DataMap data = req.Requests[0].Data as DataMap;
						data.SetValue("INS_USER", req.UserId);

						if (data.GetValue("ID").ToStringNullToEmpty() == "" || data.GetValue("ID").ToStringNullToEmpty() == "0")
							data.SetValue("ROWSTATE", "INSERT"); 

						if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
						{
							table_id = DaoFactory.Instance.Insert("InsertTables", data);
						}
						else if (data.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
						{
							DaoFactory.Instance.Update("UpdateTables", data);
							table_id = data.GetValue("ID");
						}
						req.Requests[0].ErrorNumber = 0;
						req.Requests[0].ErrorMessage = "SUCCESS";
						req.Requests[0].ReturnValue = table_id;
					}

					//컬럼
					if (req.Requests.Length > 1)
					{
						if (req.Requests[1].Data != null && (req.Requests[1].Data as DataTable).Rows.Count > 0)
						{
							IList<DataMap> list = (req.Requests[1].Data as DataTable).ToDataMapList();
							foreach (DataMap map in list)
							{
								map.SetValue("INS_USER", req.UserId);

								if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "" || map.GetValue("ROWSTATE").ToStringNullToEmpty() == "0")
									map.SetValue("ROWSTATE", "INSERT");

								if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "INSERT")
								{
									column_id = DaoFactory.Instance.Insert("InsertColumns", map);
								}
								else if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "UPDATE")
								{
									column_id = map.GetValue("ID");

									DaoFactory.Instance.Update("UpdateColumns", map);
								}
								else if (map.GetValue("ROWSTATE").ToStringNullToEmpty() == "DELETE")
								{
									column_id = map.GetValue("ID");

									DaoFactory.Instance.Update("DeleteColumns", map);
								}
							}
							req.Requests[1].ErrorNumber = 0;
							req.Requests[1].ErrorMessage = "SUCCESS";
							req.Requests[1].ReturnValue = column_id;
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
