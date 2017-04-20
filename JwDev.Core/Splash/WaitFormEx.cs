﻿using System;
using DevExpress.XtraWaitForm;

namespace JwDev.Core.Splash
{
	public partial class WaitFormEx : WaitForm
	{
		public WaitFormEx()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			progressPanel1.AutoHeight = false;
		}

		public override void SetCaption(string caption)
		{
			base.SetCaption(caption);
			progressPanel1.Caption = caption;
		}
		public override void SetDescription(string description)
		{
			base.SetDescription(description);
			progressPanel1.Description = description;
		}
		public override void ProcessCommand(Enum cmd, object arg)
		{
			switch ((WaitFormCommand)cmd)
			{
				case WaitFormCommand.LoadingImage:




					break;
				default:
					break;
			}
			base.ProcessCommand(cmd, arg);
		}



		public enum WaitFormCommand
		{
			LoadingImage
		}
	}
}
