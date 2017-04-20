using JwDev.Core.Enumerations;
using JwDev.Core.Models;

namespace JwDev.Core.Base.Interface
{
	public interface IEditForm
	{
		ToolbarButtons ToolbarButtons { get; set; }
		EditModeEnum EditMode { get; set; }
		bool VisibleToolbar { get; set; }
		bool VisibleStatusbar { get; set; }
		bool IsLoadingRefresh { get; set; }
		ElapseTime LoadingElapseTime { get; set; }
		bool IsLoaded { get; }

		bool IsDataEdit { get; set; }
		bool IsRequests { get; set; }
		FormTypeEnum FormType { get; set; }

		void SetToolbarButtons(ToolbarButtons toolbarButtons);
	}
}
