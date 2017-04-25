using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JwDev.Base.Variables;
using JwDev.Model.WasModels;

namespace JwDev.Base.WasHandler
{
	public class WasClient
	{
		const string api_url = @"api/Was";

		#region Init
		public HttpClient Init()
		{
			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Connection.Clear();
				client.DefaultRequestHeaders.ConnectionClose = false;
				client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
				//client.DefaultRequestHeaders.Add("Connection", "keep-alive");
				//client.DefaultRequestHeaders.Add("Keep-Alive", "timeout=6000");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.ExpectContinue = false;
				client.Timeout = TimeSpan.FromMinutes(60);
				client.BaseAddress = new Uri(GlobalVar.Server.WasURL);
				return client;
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region Execute
		public WasRequestSet Execute(WasRequestSet request)
		{
			try
			{
				//-------------------------------------------------------------
				// 글로벌 변수로 선언된 값을 변경한다.
				//-------------------------------------------------------------
				request.CompanyId = GlobalVar.CompanyId;
				request.UserId = GlobalVar.UserId;

				//HttpContent content = new ObjectContent<DataRequest>(request, jsonFormatter);
				//var resp = Init().PostAsync(webapiUri, content).Result;
				var response = Init().PostAsJsonAsync(api_url, request).Result;
				if (response.IsSuccessStatusCode)
				{
					return response.Content.ReadAsAsync<WasRequestSet>().Result;
				}
				else
				{
					throw new Exception(string.Format("{0}{1}{2}{3}{4}", response.StatusCode,
																		Environment.NewLine,
																		response.RequestMessage,
																		Environment.NewLine,
																		response.ReasonPhrase));
				}
			}
			catch (AggregateException ex)
			{
				//하나 이상의 오류 발생한 경우
				string message = string.Empty;
				for (int i = 0; i < ex.InnerExceptions.Count; i++)
					message = string.Concat(message,
												ex.InnerExceptions[i].Message,
												Environment.NewLine,
												ex.InnerExceptions[i].StackTrace,
												Environment.NewLine);
				throw new Exception(message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		#endregion
	}
}
