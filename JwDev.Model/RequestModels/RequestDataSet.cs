using System;
using System.Runtime.Serialization;

namespace JwDev.Model.RequestModels
{
	[DataContract]
	public class RequestDataSet : IDisposable
	{
		[DataMember]
		public string ServiceId { get; set; }

		[DataMember]
		public string ProcessId { get; set; }

		[DataMember]
		public string DatabaseId { get; set; }

		[DataMember]
		public bool IsTransaction { get; set; }

		[DataMember]
		public RequestData[] Requests { get; set; }

		[DataMember]
		public int UserId { get; set; }

		[DataMember]
		public DateTime? StartTime { get; set; }

		[DataMember]
		public DateTime? EndTime { get; set; }

		[DataMember]
		public int ErrorNumber { get; set; }

		[DataMember]
		public string ErrorMessage { get; set; }

		public RequestDataSet()
		{
			IsTransaction = true;
			Requests = null;
			ErrorNumber = 0;
			ErrorMessage = "SUCCESS";
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);	
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Requests != null && Requests.Length > 0)
				{
					foreach (RequestData data in Requests)
					{
						if (data != null)
						{
							data.Dispose();
						}
					}
					Requests = null;
				}
			}
		}
	}
}
