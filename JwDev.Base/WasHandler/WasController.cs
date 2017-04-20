﻿using System;
using JwDev.Base.Logging;
using JwDev.Base.Utils;
using JwDev.Model.RequestModels;

namespace JwDev.Base.WasHandler
{
	public class WasController
	{
		private const string assemblyName = @"JwDev.Service";

		public RequestDataSet Execute(RequestDataSet req)
		{
			var namespaceName = string.Format("JwDev.Service.Services.{0}Service", req.ServiceId);
			var methodName = req.ProcessId;

			try
			{
				Logger.Info(string.Format("WasController=> {0}.{1}", namespaceName, methodName));
				var result = (RequestDataSet)TypeUtils.InvokeMethodByParam(assemblyName, namespaceName, methodName, req);
				return result;
			}
			catch (Exception ex)
			{
				Logger.Error("WasController=> " + ex.Message);
				req.ErrorNumber = ex.HResult;
				req.ErrorMessage = ex.Message;
				return req;
			}
		}
	}
}
