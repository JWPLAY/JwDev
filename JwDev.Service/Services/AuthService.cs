﻿using System;
using System.Collections.Generic;
using JwDev.Model.WasModels;
using JwDev.Model.Map;
using JwDev.Base.Utils;
using JwDev.Model.Auth;
using JwDev.Service.Mappers;

namespace JwDev.Service.Services
{
	public static class AuthService
	{
		/// <summary>
		/// CheckLoginUser
		/// 로그인 체크
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet CheckLoginUser(WasRequestSet req)
		{
			try
			{
				req.Requests[0].Parameter.SetValue("USER_ID", null);
				var data = DaoFactory.Instance.QueryForObject<LoginUserDataModel>("GetLoginUser", req.Requests[0].Parameter);

				if (data == null)
				{
					req.ErrorNumber = -1;
					req.ErrorMessage = "해당 아이디로 사용자를 찾을 수 없습니다.";
					return req;
				}

				if (data.USE_YN != "Y")
				{
					req.ErrorNumber = -2;
					req.ErrorMessage = "사용 가능한 아이디가 아닙니다. 확인 후 다시 시도하세요!!!";
					return req;
				}

				if (data.IS_PW_CHECK != 1)
				{
					req.ErrorNumber = -3;
					req.ErrorMessage = "비밀번호가 정확하지 않습니다. 확인 후 다시 시도하세요!!!";
					return req;
				}

				req.Requests[0].Parameter.SetValue("USER_ID", data.USER_ID);

				try
				{
					DaoFactory.Instance.Insert("InsertLoginLog", req.Requests[0].Parameter);
				}
				catch (Exception ex)
				{
					throw new Exception("로그인로그 저장 중 오류가 발생하였습니다.\r\n" + ErrorUtils.GetMessage(ex));
				}

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

		/// <summary>
		/// Logout
		/// 로그아웃
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet Logout(WasRequestSet req)
		{
			try
			{
				try
				{
					DaoFactory.Instance.Update("UpdateLogout", req.Requests[0].Parameter);
				}
				catch (Exception ex)
				{
					throw new Exception("로그아웃 정보 저장 중 오류가 발생하였습니다.\r\n" + ErrorUtils.GetMessage(ex));
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
		/// GetUserMenus
		/// 사용자별 메뉴구성
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet GetMainMenus(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<MainMenuDataModel>("GetMainMenus", req.Requests[0].Parameter);
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

		/// <summary>
		/// GetFormData
		/// 화면정보 가져오기
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet GetFormData(WasRequestSet req)
		{
			try
			{
				var data = DaoFactory.Instance.QueryForObject<DataMap>("GetViewData", req.Requests[0].Parameter);
				List<DataMap> list = new List<DataMap>();
				if (data != null)
				{
					list.Add(data);
				}
				req.Requests[0].Data = ConvertUtils.DataMapListToDataTable(list, "GetFormData");
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
		/// SaveBookmark
		/// 북마크 저장
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet SaveBookmark(WasRequestSet req)
		{
			try
			{
				var map = DaoFactory.Instance.QueryForObject<DataMap>("SelectBookmark", req.Requests[0].Parameter);
				if (map == null || map.GetValue("USER_ID") == null)
				{
					DaoFactory.Instance.Insert("InsertBookmark", req.Requests[0].Parameter);
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
		/// GetDomains
		/// 도메인 가져오기
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet GetDictionaries(WasRequestSet req)
		{
			try
			{
				IList<DataMap> list = DaoFactory.Instance.QueryForList<DataMap>("GetDictionaries", req.Requests[0].Parameter);
				req.Requests[0].Data = ConvertUtils.DataMapListToDataTable(list, req.Requests[0].SqlId);
				return req;
			}
			catch (Exception ex)
			{
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}

		public static WasRequestSet GetMessages(WasRequestSet req)
		{
			try
			{
				IList<DataMap> list = DaoFactory.Instance.QueryForList<DataMap>("GetMessages", req.Requests[0].Parameter);
				req.Requests[0].Data = ConvertUtils.DataMapListToDataTable(list, req.Requests[0].SqlId);
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
		/// GetSettings
		/// 시스템설정 가져오기
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet GetSettings(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<DataMap>("GetSettings", req.Requests[0].Parameter);
				req.Requests[0].Data = ConvertUtils.DataMapListToDataTable(list, req.Requests[0].SqlId);
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
		/// GetHelpContent
		/// 도움말 컨텐츠
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet GetHelpContent(WasRequestSet req)
		{
			try
			{
				var data = DaoFactory.Instance.QueryForObject<DataMap>("GetHelpContent", req.Requests[0].Parameter);
				List<DataMap> list = new List<DataMap>();
				if (data != null)
					list.Add(data);

				req.Requests[0].Data = ConvertUtils.DataMapListToDataTable(list, req.Requests[0].SqlId);
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
		/// GetStyles
		/// 스타일 정보
		/// </summary>
		/// <param name="req">WasRequest</param>
		/// <returns>WasRequest</returns>
		public static WasRequestSet GetStyles(WasRequestSet req)
		{
			try
			{
				var list = DaoFactory.Instance.QueryForList<DataMap>("GetStyles", req.Requests[0].Parameter);
				req.Requests[0].Data = ConvertUtils.DataMapListToDataTable(list, req.Requests[0].SqlId);
				return req;
			}
			catch (Exception ex)
			{
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}

		public static WasRequestSet ClearPassword(WasRequestSet req)
		{
			try
			{
				DataMap data = req.Requests[0].Data as DataMap;
				data.SetValue("INS_USER", req.UserId);

				DaoFactory.Instance.Update("ClearPassword", data);
				req.ErrorNumber = 0;
				req.ErrorMessage = "SUCCESS";
				return req;
			}
			catch (Exception ex)
			{
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}

		public static WasRequestSet ChangePassword(WasRequestSet req)
		{
			try
			{
				DataMap map = req.Requests[0].Data as DataMap;
				map.SetValue("INS_USER", req.UserId);

				var data = DaoFactory.Instance.QueryForObject<LoginUserDataModel>("GetLoginUser", map);

				if (data == null)
				{
					req.ErrorNumber = -1;
					req.ErrorMessage = "해당 아이디로 사용자를 찾을 수 없습니다.";
					return req;
				}

				if (data.USE_YN != "Y")
				{
					req.ErrorNumber = -2;
					req.ErrorMessage = "사용 가능한 아이디가 아닙니다. 확인 후 다시 시도하세요!!!";
					return req;
				}

				if (data.IS_PW_CHECK != 1)
				{
					req.ErrorNumber = -3;
					req.ErrorMessage = "비밀번호가 정확하지 않습니다. 확인 후 다시 시도하세요!!!";
					return req;
				}

				map.SetValue("USER_ID", data.USER_ID);
				map.SetValue("LOGIN_PW", map.GetValue("CHG_LOGIN_PW"));

				DaoFactory.Instance.Update("ChangePassword", map);
				req.ErrorNumber = 0;
				req.ErrorMessage = "SUCCESS";
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
