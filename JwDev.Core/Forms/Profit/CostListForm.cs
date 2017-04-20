﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using JwDev.Base.DBTran.Controller;
using JwDev.Base.Map;
using JwDev.Base.Utils;
using JwDev.Core.Base.Forms;
using JwDev.Core.Controls.Grid;
using JwDev.Core.Messages;
using JwDev.Core.Utils;

namespace JwDev.Core.Forms.Profit
{
	public partial class CostListForm : EditForm
	{
		public CostListForm()
		{
			InitializeComponent();

			gridList.RowCellStyle += delegate (object sender, RowCellStyleEventArgs e)
			{
				if (e.RowHandle < 0)
					return;

				try
				{
					if (e.Column.FieldName.EndsWith("_QTY") || e.Column.FieldName.EndsWith("_AMT"))
					{
						if (e.CellValue.ToDecimalNullToZero() < 0)
							e.Appearance.ForeColor = Color.Red;
					}
				}
				catch(Exception ex)
				{
					ShowErrBox(ex);
				}
			};

			btnClosing.Click += delegate (object sender, EventArgs e) { doCreate(); };
		}

		protected override void InitButtons()
		{
			SetToolbarButtons(new Models.ToolbarButtons()
			{
				Refresh = true,
				Export = true
			});
			btnClosing.Enabled = this.IsDataEdit;
		}
		protected override void InitControls()
		{
			base.InitControls();

			SetFieldNames();

			datClosingYm.Init(Enumerations.CalendarViewType.MonthView);
			txtProduct.Init("ALL_PRODUCT", "PRODUCT_ID", "PRODUCT_NAME", null, null);

			InitCombo();
			InitGrid();
		}
		void InitCombo()
		{
			lupProductType.BindData("PRODUCT_TYPE", null, "ALL", true);
			lupCategory.BindData("CATEGORY", null, "ALL", true);
		}
		void InitGrid()
		{
			gridList.Init();
			gridList.ShowFooter = true;
			gridList.AddGridColumns(
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "PRODUCT_TYPE", HorzAlignment = HorzAlignment.Center, Width = 80 },
				new XGridColumn() { FieldName = "CATEGORY", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "PRODUCT_ID", HorzAlignment = HorzAlignment.Center, Width = 100, Visible = false },
				new XGridColumn() { FieldName = "PRODUCT_CODE", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "PRODUCT_NAME", Width = 200 },
				new XGridColumn() { FieldName = "BASE_QTY", Caption = "기초재고수량", HorzAlignment = HorzAlignment.Far, Width = 90, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "BASE_AMT", Caption = "기초재고금액", HorzAlignment = HorzAlignment.Far, Width = 110, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "PURC_QTY", Caption = "당월구매수량", HorzAlignment = HorzAlignment.Far, Width = 90, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "PURC_AMT", Caption = "당월구매금액", HorzAlignment = HorzAlignment.Far, Width = 110, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "PROD_QTY", Caption = "당월생산수량", HorzAlignment = HorzAlignment.Far, Width = 90, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "PROD_AMT", Caption = "당월생산금액", HorzAlignment = HorzAlignment.Far, Width = 110, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "TSUM_QTY", Caption = "원가계산수량", HorzAlignment = HorzAlignment.Far, Width = 90, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "TSUM_AMT", Caption = "원가계산금액", HorzAlignment = HorzAlignment.Far, Width = 110, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "COST_PRICE", HorzAlignment = HorzAlignment.Far, Width = 100, FormatType = FormatType.Numeric, FormatString = "N0" },
				new XGridColumn() { FieldName = "REMARKS", Width = 200 },
				new XGridColumn() { FieldName = "INS_TIME" },
				new XGridColumn() { FieldName = "INS_USER", Visible = false },
				new XGridColumn() { FieldName = "INS_USER_NAME" },
				new XGridColumn() { FieldName = "UPD_TIME" },
				new XGridColumn() { FieldName = "UPD_USER", Visible = false },
				new XGridColumn() { FieldName = "UPD_USER_NAME" }
				);

			gridList.SetMerge("PRODUCT_TYPE", "CATEGORY");
			
			gridList.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridList.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
		}

		protected override void DataLoad(object param = null)
		{
			gridList.BindData("CostPrice", "GetList", null, new DataMap() {
				{ "CLOSING_YM", datClosingYm.GetDateChar6() },
				{ "PRODUCT_ID", txtProduct.EditValue },
				{ "PRODUCT_TYPE", lupProductType.EditValue },
				{ "CATEGORY", lupCategory.EditValue }
			});
		}

		void doCreate()
		{
			if (MsgBox.Show("원가마감하겠습니까?", "확인!!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			try
			{
				var res = RequestHelper.ProcedureCall("CreateCostPrice", new DataMap() { { "CLOSING_YM", datClosingYm.GetDateChar6() } });
				if (res.ErrorNumber != 0)
					throw new Exception(res.ErrorMessage);

				ShowMsgBox("생성하였습니다.");
				DataLoad(null);
			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}
	}
}