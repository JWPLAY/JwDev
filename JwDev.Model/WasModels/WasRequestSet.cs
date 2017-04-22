using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace JwDev.Model.WasModels
{
	[DataContract]
	public class WasRequestSet : IDisposable
	{
		[DataMember]
		[Description("서비스ID")]
		public string ServiceId { get; set; }

		[DataMember]
		[Description("프로세스ID")]
		public string ProcessId { get; set; }

		[DataMember]
		[Description("데이터베이스ID")]
		public string DatabaseId { get; set; }

		[DataMember]
		[Description("트랜잭션여부")]
		public bool IsTransaction { get; set; }

		[DataMember]
		[Description("요청목록")]
		public WasRequest[] Requests { get; set; }

		[DataMember]
		[Description("회사ID")]
		public int CompanyId { get; set; }

		[DataMember]
		[Description("사용자ID")]
		public int UserId { get; set; }

		[DataMember]
		[Description("처리시작시간")]
		public DateTime? StartTime { get; set; }

		[DataMember]
		[Description("처리종료시간")]
		public DateTime? EndTime { get; set; }

		[DataMember]
		[Description("오류번호")]
		public int ErrorNumber { get; set; }

		[DataMember]
		[Description("오류메시지")]
		public string ErrorMessage { get; set; }

		public WasRequestSet()
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
					foreach (WasRequest req in Requests)
					{
						if (req != null)
						{
							req.Dispose();
						}
					}
					Requests = null;
				}
			}
		}
	}
}
