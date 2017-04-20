using DevExpress.XtraSplashScreen;
using JwDev.Core.Splash;

namespace JwDev.Core.Utils
{
	public static class SplashUtils
	{
		public static void ShowWait(string msg = "")
		{
			if (IsWaitFormVisible() == false)
			{
				SplashScreenManager.ShowForm(typeof(WaitFormEx));
			}
			SplashScreenManager.Default.SendCommand(WaitFormEx.WaitFormCommand.LoadingImage, true);
			if (!string.IsNullOrEmpty(msg))
			{
				SplashScreenManager.Default.SetWaitFormDescription(msg);
			}
		}
		public static void CloseWait()
		{
			if (IsWaitFormVisible() == true)
			{
				SplashScreenManager.CloseForm(false);
			}
		}
		public static bool IsWaitFormVisible()
		{
			if (SplashScreenManager.Default == null || SplashScreenManager.Default.IsSplashFormVisible == false)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
