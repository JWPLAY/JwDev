using System.Windows.Forms;
using JwDev.Base.Constants;

namespace JwDev.Base.Models
{
	public class SkinInfo
	{
		public bool UseSkin { get; set; }
		public string MainSkin { get; set; }
		public string FormSkin { get; set; }
		public string GridSkin { get; set; }
		public string FontName { get; set; }
		public float FontSize { get; set; }
		public bool GridEvenAndOdd { get; set; }
		public bool VisibleToolbarName { get; set; }
		public FormWindowState MainWindowState { get; set; }

		public SkinInfo()
		{
			UseSkin = SkinConsts.USE_SKIN;
			MainSkin = SkinConsts.MAIN_SKIN;
			FormSkin = SkinConsts.FORM_SKIN;
			GridSkin = SkinConsts.GRID_SKIN;
			FontName = SkinConsts.FONT_NAME;
			FontSize = SkinConsts.FONT_SIZE;
			GridEvenAndOdd = SkinConsts.GRID_EVEN_AND_ODD;
			VisibleToolbarName = true;
			MainWindowState = FormWindowState.Maximized;
		}
	}
}
