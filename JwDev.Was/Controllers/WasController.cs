using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JwDev.Base.Utils;
using JwDev.Model.WasModels;

namespace JwDev.Was.Controllers
{
	public class WasController : ApiController
    {
		private const string assemblyName = @"JwDev.Service";

		public HttpResponseMessage Post(WasRequestSet reqset)
		{
			string namespaceName = string.Format("{0}.Services.{1}Service", assemblyName, reqset.ServiceId);
			string methodName = reqset.ProcessId;

			try
			{
				WasRequestSet result = (WasRequestSet)TypeUtils.InvokeMethodByParam(assemblyName, namespaceName, methodName, reqset);
				return Request.CreateResponse(HttpStatusCode.Created, result);
			}
			catch (Exception ex)
			{
				reqset.ErrorNumber = ex.HResult;
				reqset.ErrorMessage = ex.Message;
				return Request.CreateResponse(HttpStatusCode.Created, reqset);
			}
		}
	}
}
