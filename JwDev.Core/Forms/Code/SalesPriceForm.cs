﻿using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using JwDev.Base.DBTran.Controller;
using JwDev.Base.Map;
using JwDev.Core.Base.Forms;
using JwDev.Core.Controls.Grid;
using JwDev.Core.Enumerations;
using JwDev.Core.Models;
using JwDev.Core.Utils;
using JwDev.Model.Codes;

namespace JwDev.Core.Forms.Code
{
	public partial class SalesPriceForm : EditForm
	{
		public SalesPriceForm()
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

			lcItemProductName.Tag = true;
			lcItemBegDate.Tag = true;

			SetFieldNames();

			txtRegId.SetEnable(false);
			txtProductId.SetEnable(false);
			txtProductCode.SetEnable(false);
			txtProductName.SetEnable(false);
			datEndDate.SetEnable(false);
			txtInsTime.SetEnable(false);
			txtInsUserName.SetEnable(false);
			txtUpdTime.SetEnable(false);
			txtUpdUserName.SetEnable(false);

			datBegDate.Init();
			datEndDate.Init();
			spnSalePrice.SetFormat("N0", false);
			
			InitGrid();
			
		}

		void InitGrid()
		{
			#region 조회리스트
			gridList.Init();
			gridList.AddGridColumns(new XGridColumn[]
			{
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "PRODUCT_ID", HorzAlignment = HorzAlignment.Center, Width = 60, Visible = false },
				new XGridColumn() { FieldName = "PRODUCT_CODE", HorzAlignment = HorzAlignment.Center, Width = 80 },
				new XGridColumn() { FieldName = "PRODUCT_NAME", Width = 200 },
				new XGridColumn() { FieldName = "SALE_PRICE", HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", Width = 100 },
				new XGridColumn() { FieldName = "COST_PRICE", HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", Width = 100 },
				new XGridColumn() { FieldName = "PRODUCT_TYPE", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "CATEGORY", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "USE_YN", HorzAlignment = HorzAlignment.Center, RepositoryItem = gridList.GetRepositoryItemCheckEdit(), Width = 80 }
			});

			gridList.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridList.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridList.ColumnFix("ROW_NO");

			gridList.RowCellClick += delegate (object sender, RowCellClickEventArgs e)
			{
				if (e.RowHandle < 0)
					return;

				try
				{
					if (e.Button == MouseButtons.Left && e.Clicks == 1)
					{
						GridView view = sender as GridView;

						DataInit();
						DetailListLoad(view.GetRowCellValue(e.RowHandle, "PRODUCT_ID"));
						txtProductId.EditValue = view.GetRowCellValue(e.RowHandle, "PRODUCT_ID");
						txtProductCode.EditValue = view.GetRowCellValue(e.RowHandle, "PRODUCT_CODE");
						txtProductName.EditValue = view.GetRowCellValue(e.RowHandle, "PRODUCT_NAME");
						datBegDate.Focus();
					}
				}
				catch(Exception ex)
				{
					ShowErrBox(ex);
				}
			};
			#endregion

			#region 변경이력
			gridHistList.Init();

			#region Grid Add Columns
			gridHistList.AddGridColumns(
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "REG_ID", HorzAlignment = HorzAlignment.Center, Width = 60, Visible = false },
				new XGridColumn() { FieldName = "PRODUCT_ID", HorzAlignment = HorzAlignment.Center, Width = 80, Visible = false },
				new XGridColumn() { FieldName = "BEG_DATE" },
				new XGridColumn() { FieldName = "END_DATE" },
				new XGridColumn() { FieldName = "SALE_PRICE", HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", Width = 100 },
				new XGridColumn() { FieldName = "REMARKS", Width = 200 },
				new XGridColumn() { FieldName = "INS_TIME" },
				new XGridColumn() { FieldName = "INS_USER_NAME" },
				new XGridColumn() { FieldName = "UPD_TIME" },
				new XGridColumn() { FieldName = "UPD_USER_NAME" }
				);
			#endregion

			gridHistList.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridHistList.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridHistList.ColumnFix("ROW_NO");

			gridHistList.RowCellClick += delegate (object sender, RowCellClickEventArgs e)
			{
				if (e.RowHandle < 0)
					return;

				try
				{
					if (e.Button == MouseButtons.Left && e.Clicks == 1)
					{
						GridView view = sender as GridView;
						DetailDataLoad(view.GetRowCellValue(e.RowHandle, "REG_ID"));
					}
				}
				catch (Exception ex)
				{
					ShowErrBox(ex);
				}
			};

			#endregion
		}

		protected override void LoadForm()
		{
			base.LoadForm();
			DataLoad();
		}

		protected override void DataInit()
		{
			try
			{
				txtRegId.Clear();
				txtProductId.Clear();
				txtProductName.Clear();
				datEndDate.Clear();
				spnSalePrice.Clear();
				memRemarks.Clear();

				txtInsTime.Clear();
				txtInsUserName.Clear();
				txtUpdTime.Clear();
				txtUpdUserName.Clear();

				gridHistList.DataSource = null;
				gridHistList.EmptyDataTableBinding();

				SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true });
				this.EditMode = EditModeEnum.New;
				txtProductName.Focus();
			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		protected override void DataLoad(object param = null)
		{
			try
			{
				gridList.BindData("Product", "GetList", null, new DataMap()
				{
					{ "FIND_TEXT", txtFindText.EditValue },
					{ "VIEW_TYPE", "1" }
				});

				if (txtProductId.EditValue == null)
				{
					gridHistList.Clear();
					DataInit();
				}
				else
				{
					DetailListLoad(txtProductId.EditValue);
					if (param != null)
						DetailDataLoad(param);
					else
						DataInit();
				}
			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		void DetailListLoad(object product_id)
		{
			try
			{
				gridHistList.BindData("Product", "GetSalesPriceList", null, new DataMap()
				{
					{ "PRODUCT_ID", product_id }
				});
			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		void DetailDataLoad(object reg_id)
		{
			try
			{
				var data = RequestHelper.GetData<SalesPriceDataModel>("Product", "GetSalesPriceData", new DataMap()
				{
					{ "REG_ID", reg_id }
				});

				txtRegId.EditValue = data.REG_ID;
				txtProductId.EditValue = data.PRODUCT_ID;
				txtProductCode.EditValue = data.PRODUCT_CODE;
				txtProductName.EditValue = data.PRODUCT_NAME;
				datBegDate.SetDateChar8(data.BEG_DATE);
				datEndDate.SetDateChar8(data.END_DATE);
				spnSalePrice.EditValue = data.SALE_PRICE;
				memRemarks.EditValue = data.REMARKS;

				txtInsTime.EditValue = data.INS_TIME;
				txtInsUserName.EditValue = data.INS_USER_NAME;
				txtUpdTime.EditValue = data.UPD_TIME;
				txtUpdUserName.EditValue = data.UPD_USER_NAME;

				SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true, Delete = true });
				this.EditMode = EditModeEnum.Modify;
				spnSalePrice.Focus();

			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		protected override void DataSave(object arg, SaveCallback callback)
		{
			try
			{
				DataMap data = lcGroupEdit1.ItemToDataMap();
				data.SetValue("ROWSTATE", (this.EditMode == EditModeEnum.New) ? "INSERT" : "UPDATE");

				var res = RequestHelper.Execute("Product", "SaveSalesPrice", data, "REG_ID");
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
					{ "REG_ID", txtRegId.EditValue },
					{ "ROWSTATE", "DELETE" }
				};

				var res = RequestHelper.Execute("Product", "SaveSalesPrice", data, null);
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