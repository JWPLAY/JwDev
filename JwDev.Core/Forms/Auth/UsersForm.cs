﻿using System;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using JwDev.Base.WasHandler;
using JwDev.Model.Map;
using JwDev.Core.Base.Forms;
using JwDev.Core.Controls.Grid;
using JwDev.Core.Enumerations;
using JwDev.Core.Models;
using JwDev.Core.Utils;

namespace JwDev.Core.Forms.Auth
{
	public partial class UsersForm : EditForm
	{
		public UsersForm()
		{
			InitializeComponent();

			btnPasswordClear.Click += delegate (object sender, EventArgs e) { ClearPassword(); };
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			txtFindText.Focus();
		}

		protected override void InitButtons()
		{
			base.InitButtons();
			SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true });
		}
		protected override void InitControls()
		{
			base.InitControls();

			lcItemUserName.Tag = true;

			SetFieldNames();

			txtUserId.SetEnable(false);
			txtInsTime.SetEnable(false);
			txtInsUser.SetEnable(false);
			txtUpdTime.SetEnable(false);
			txtUpdUser.SetEnable(false);

			InitCombo();
			InitGrid();
		}

		void InitCombo()
		{
			lupUserType.BindData("USER_TYPE", null, null, true);
		}

		void InitGrid()
		{
			gridList.Init();
			gridList.AddGridColumns(
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "USER_ID", HorzAlignment = HorzAlignment.Center, Visible = false },
				new XGridColumn() { FieldName = "USER_NAME", Width = 100 },
				new XGridColumn() { FieldName = "LOGIN_ID", Width = 100 },
				new XGridColumn() { FieldName = "USE_YN", HorzAlignment = HorzAlignment.Center, Width = 80, RepositoryItem = gridList.GetRepositoryItemCheckEdit() }
			);
			gridList.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridList.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridList.ColumnFix("ROW_NO");

			gridList.RowCellClick += delegate (object sender, RowCellClickEventArgs e)
			{
				if (e.RowHandle < 0)
					return;

				try
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
					{
						GridView view = sender as GridView;
						DetailDataLoad(view.GetRowCellValue(e.RowHandle, "USER_ID"));
					}
				}
				catch(Exception ex)
				{
					ShowErrBox(ex);
				}
			};
		}
		protected override void LoadForm()
		{
			base.LoadForm();
			DataLoad();
		}
		protected override void DataInit()
		{
			txtUserId.Clear();
			txtUserName.Clear();
			txtLoginId.Clear();
			chkUseYn.Checked = true;
			memeRemarks.Clear();

			txtInsTime.Clear();
			txtInsUser.Clear();
			txtUpdTime.Clear();
			txtUpdUser.Clear();

			SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true });
			this.EditMode = EditModeEnum.New;
			txtUserName.Focus();
		}

		protected override void DataLoad(object param = null)
		{
			gridList.BindData("Base", "GetList", "SelectUsers", new DataMap() { { "FIND_TEXT", txtFindText.EditValue } });

			if (param != null)
				DetailDataLoad(param);
			else
				DataInit();
		}

		void DetailDataLoad(object id)
		{
			try
			{
				DataMap data = (DataMap)WasHelper.GetData("Base", "GetData", "SelectUsers", new DataMap() { { "USER_ID", id } }).Requests[0].Data;
				if (data == null)
					throw new Exception("조회할 데이터가 없습니다.");

				txtUserId.EditValue = data.GetValue("USER_ID");
				txtUserName.EditValue = data.GetValue("USER_NAME");
				lupUserType.EditValue = data.GetValue("USER_TYPE");
				txtLoginId.EditValue = data.GetValue("LOGIN_ID");
				chkUseYn.EditValue = data.GetValue("USE_YN");
				memeRemarks.EditValue = data.GetValue("REMARKS");

				txtInsTime.EditValue = data.GetValue("INS_TIME");
				txtInsUser.EditValue = data.GetValue("INS_USER_NAME");
				txtUpdTime.EditValue = data.GetValue("UPD_TIME");
				txtUpdUser.EditValue = data.GetValue("UPD_USER_NAME");

				SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true, Delete = true });
				this.EditMode = EditModeEnum.Modify;
				txtUserName.Focus();

			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		protected override void DataSave(object arg, SaveCallback callback)
		{
			try
			{
				DataMap data = new DataMap()
				{
					{ "USER_ID", txtUserId.EditValue },
					{ "USER_NAME", txtUserName.EditValue },
					{ "USER_TYPE", lupUserType.EditValue },
					{ "LOGIN_ID", txtLoginId.EditValue },
					{ "USE_YN", chkUseYn.EditValue },
					{ "REMARKS", memeRemarks.EditValue },
					{ "ROWSTATE", (this.EditMode== EditModeEnum.New)?"INSERT":"UPDATE" }
				};

				var res = WasHelper.Execute("Base", "Save", "User", data, "USER_ID");
				if (res.ErrorNumber != 0)
					throw new Exception(res.ErrorMessage);

				ShowMsgBox("저장하였습니다.");
				callback(arg, res.Requests[0].ReturnValue);

			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		protected override void DataDelete(object arg, DeleteCallback callback)
		{
			try
			{
				DataMap data = new DataMap()
				{
					{ "USER_ID", txtUserId.EditValue },
					{ "ROWSTATE", "DELETE" }
				};

				var res = WasHelper.Execute("Base", "Save", "User", data, null);
				if (res.ErrorNumber != 0)
					throw new Exception(res.ErrorMessage);

				ShowMsgBox("삭제하였습니다.");
				callback(arg, null);

			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		private void ClearPassword()
		{
			try
			{
				DataMap data = new DataMap()
				{
					{ "USER_ID", txtUserId.EditValue }
				};

				var res = WasHelper.Execute("Auth", "ClearPassword", null, data, null);
				if (res.ErrorNumber != 0)
					throw new Exception(res.ErrorMessage);

				ShowMsgBox("비밀번호를 초기화 하였습니다.");

			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}
	}
}