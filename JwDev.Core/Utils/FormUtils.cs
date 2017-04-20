﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using JwDev.Base.WasHandler;
using JwDev.Model.Map;
using JwDev.Base.Utils;
using JwDev.Core.Base.Forms;

namespace JwDev.Core.Utils
{
	public static class FormUtils
	{
		public static IEnumerable<Control> GetAllControls(Control parent)
		{
			var controls = new List<Control>();
			parent.Controls.Cast<Control>().ToList().ForEach(x => controls.AddRange(GetAllControls(x))
			);
			controls.Add(parent);
			return controls;
		}

		public static bool IsExistsForm(string formName)
		{
			return Application.OpenForms.OfType<Form>().Where(x => x.Name == formName).Any();
		}

		public static Form GetForm(string formName)
		{
			if (IsExistsForm(formName))
			{
				return Application.OpenForms[formName];
			}
			else
			{
				return null;
			}
		}

		public static Assembly GetAssembly(string assemblyName)
		{
			try
			{
				Assembly assembly = null;
				if (Assembly.GetExecutingAssembly().Equals(Assembly.LoadFrom(string.Empty + assemblyName)))
				{
					assembly = Assembly.GetExecutingAssembly();
				}
				else
				{
					assembly = Assembly.LoadFrom(string.Format(@"{0}\\{1}", Application.StartupPath, assemblyName));
				}
				return assembly;
			}
			catch
			{
				throw;
			}
		}

		public static void ShowHelp(object helpId, object menuId = null)
		{
			if (helpId == null && menuId == null)
			{
				return;
			}
			try
			{
				string textValue;
				string rtfValue;
				string helpName;

				DataMap data = (DataMap)WasHelper.GetData("Auth", "GetHelpContent", null, new DataMap() { { "HELP_ID", helpId }, { "MENU_ID", menuId } }).Requests[0].Data;
				if (data != null)
				{
					helpId = data.GetValue("ID");
					helpName = data.GetValue("NAME").ToStringNullToEmpty();
					rtfValue = data.GetValue("CONTENT_BY_RTE").ToStringNullToEmpty();
					textValue = data.GetValue("CONTENT").ToStringNullToEmpty();

					if (rtfValue.IsNullOrEmpty() == false || textValue.IsNullOrEmpty() == false)
					{
						HelpForm form = new HelpForm() { Name = string.Format("HELP_{0}", helpId), Text = "[도움말]", Subject = helpName };
						if (rtfValue.IsNullOrEmpty())
						{
							form.Content = textValue;
						}
						else
						{
							form.ContentByRte = rtfValue;
						}
						form.InsertDtime = data.GetValue("INS_TIME").ToStringNullToEmpty();
						form.InsertUserName = data.GetValue("INS_USER_NAME").ToStringNullToEmpty();
						form.UpdateDtime = data.GetValue("UPD_TIME").ToStringNullToEmpty();
						form.UpdateUserName = data.GetValue("UPD_USER_ID").ToStringNullToEmpty();
						form.StartPosition = FormStartPosition.CenterScreen;
						form.TopMost = true;
						form.Show();
					}
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
