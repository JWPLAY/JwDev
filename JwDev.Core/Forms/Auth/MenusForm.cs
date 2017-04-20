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
	public partial class MenusForm : EditForm
	{
		public MenusForm()
		{
			InitializeComponent();
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

			lcItemMenuName.Tag = true;

			SetFieldNames();

			txtMenuId.SetEnable(false);
			txtInsTime.SetEnable(false);
			txtInsUserName.SetEnable(false);
			txtUpdTime.SetEnable(false);
			txtUpdUserName.SetEnable(false);

			numSortSeq.SetFormat("D", false);

			InitCombo();
			InitGrid();
		}

		void InitCombo()
		{
			lupFormType.BindData("FORM_TYPE", null, null, true);
		}

		void InitGrid()
		{
			gridList.Init();
			gridList.AddGridColumns(
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "HIER_ID", Visible = false },
				new XGridColumn() { FieldName = "HIER_NAME", CaptionCode = "MENU_NAME", Width = 300 },
				new XGridColumn() { FieldName = "MENU_ID", HorzAlignment = HorzAlignment.Center, Width = 80 },
				new XGridColumn() { FieldName = "SORT_SEQ", HorzAlignment = HorzAlignment.Center, Width = 80 },
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
						DetailDataLoad(view.GetRowCellValue(e.RowHandle, "MENU_ID"));
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
			txtMenuId.Clear();
			txtMenuName.Clear();
			txtParentId.Clear();
			numSortSeq.Clear();
			txtAssembly.Clear();
			txtNamespace.Clear();
			txtInstance.Clear();
			chkUseYn.Checked = true;
			memRemarks.Clear();

			txtInsTime.Clear();
			txtInsUserName.Clear();
			txtUpdTime.Clear();
			txtUpdUserName.Clear();

			SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true });
			EditMode = EditModeEnum.New;
			txtMenuName.Focus();
		}

		protected override void DataLoad(object param = null)
		{
			gridList.BindData("Base", "GetList", "SelectMenus", new DataMap() { { "FIND_TEXT", txtFindText.EditValue } });

			if (param != null)
				DetailDataLoad(param);
			else
				DataInit();
		}

		void DetailDataLoad(object id)
		{
			try
			{
				DataMap data = (DataMap)(WasHelper.GetData("Base", "GetData", "SelectMenu", new DataMap() { { "MENU_ID", id } }).Requests[0].Data);
				if (data == null)
					throw new Exception("조회할 데이터가 없습니다.");

				txtMenuId.EditValue = data.GetValue("MENU_ID");
				txtMenuName.EditValue = data.GetValue("MENU_NAME");
				txtParentId.EditValue = data.GetValue("PARENT_ID");
				numSortSeq.EditValue = data.GetValue("SORT_SEQ");
				txtAssembly.EditValue = data.GetValue("ASSEMBLY");
				txtNamespace.EditValue = data.GetValue("NAMESPACE");
				txtInstance.EditValue = data.GetValue("INSTANCE");
				lupFormType.EditValue = data.GetValue("FORM_TYPE");
				chkUseYn.EditValue = data.GetValue("USE_YN");
				memRemarks.EditValue = data.GetValue("REMARKS");

				txtInsTime.EditValue = data.GetValue("INS_TIME");
				txtInsUserName.EditValue = data.GetValue("INS_USER_NAME");
				txtUpdTime.EditValue = data.GetValue("UPD_TIME");
				txtUpdUserName.EditValue = data.GetValue("UPD_USER_NAME");

				SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true, Delete = true });
				EditMode = EditModeEnum.Modify;
				txtMenuName.Focus();

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
				DataMap map = lc.ItemToDataMap();
				map.SetValue("ROWSTATE", (this.EditMode == EditModeEnum.New) ? "INSERT" : "UPDATE");

				var res = WasHelper.Execute("Base", "Save", "Menu", map, "MENU_ID");
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
				DataMap map = new DataMap()
				{
					{ "MENU_ID", txtMenuId.EditValue },
					{ "ROWSTATE", "DELETE" }
				};

				var res = WasHelper.Execute("Base", "Save", "Menu", map, "MENU_ID");
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
	}
}