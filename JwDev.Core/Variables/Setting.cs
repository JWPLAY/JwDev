using System;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using JwDev.Base.Constants;
using JwDev.Base.Logging;
using JwDev.Base.Utils;
using JwDev.Base.Variables;
using JwDev.Base.WasHandler;
using JwDev.Core.Messages;
using JwDev.Core.Utils;
using JwDev.Model.Map;

namespace JwDev.Core.Variables
{
	public static class Setting
	{
		public static void Init()
		{
			try
			{
				Logger.Debug("Setting Start!!");

				/* ******************************************************************************************
				 * Set Default Font Name & Size
				* ******************************************************************************************/
				if (FontFamily.Families.Where(x => x.Name == SkinConsts.FONT_NAME).Any())
				{
					GlobalVar.Skin.FontName = SkinConsts.FONT_NAME;
				}
				else
				{
					GlobalVar.Skin.FontName = SystemFonts.DefaultFont.Name;
				}
				AppearanceObject.DefaultFont = new Font(GlobalVar.Skin.FontName, GlobalVar.Skin.FontSize);

				SplashUtils.ShowWait("리소스 데이터를 생성하는 중입니다... 잠시만...");

				/* ******************************************************************************************
				 * Set Default Domain
				* ******************************************************************************************/
				DomainUtils.Init();

				/* ******************************************************************************************
				 * Get System Setting
				* ******************************************************************************************/
				try
				{
					DataTable data = (DataTable)WasHelper.GetData("Auth", "GetSettings", "Setting", new DataMap()).Requests[0].Data;
					if (data != null && data.Rows.Count > 0)
					{
						foreach (DataRow row in data.Rows)
						{
							GlobalVar.Settings.SetValue(row["CODE"].ToString(), row["VALUE"]);
						}
					}
				}
				catch (Exception ex)
				{
					MsgBox.Show(ex);
				}

				/* ******************************************************************************************
				 * Get Domain
				* ******************************************************************************************/
				try
				{
					DomainUtils.SetData();
				}
				catch (Exception ex)
				{
					MsgBox.Show(ex);
				}

				/* ******************************************************************************************
				 * Set LookAndFeel Font
				* ******************************************************************************************/
				if (GlobalVar.Skin.MainSkin.IsNullOrEmpty() == false)
				{
					UserLookAndFeel.Default.UseDefaultLookAndFeel = true;
					if (GlobalVar.Skin.MainSkin.ToStringNullToEmpty() != SkinConsts.MAIN_SKIN)
					{
						UserLookAndFeel.Default.SetSkinStyle(GlobalVar.Skin.MainSkin);
					}
				}
				else
				{
					UserLookAndFeel.Default.UseDefaultLookAndFeel = false;
				}

				if (GlobalVar.Skin.FontName != SkinConsts.FONT_NAME ||
					GlobalVar.Skin.FontSize != SkinConsts.FONT_SIZE)
				{
					AppearanceObject.DefaultFont = new Font(GlobalVar.Skin.FontName, GlobalVar.Skin.FontSize);
				}

				SplashUtils.CloseWait();
				Logger.Debug("Setting End!!");
			}
			catch (Exception ex)
			{
				MsgBox.Show(ex);
			}
		}
	}
}
