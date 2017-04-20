﻿using System;
using System.Collections.Generic;
using DevExpress.Data;
using DevExpress.Utils;
using JwDev.Base.WasHandler;
using JwDev.Model.Map;
using JwDev.Base.Utils;
using JwDev.Core.Base.Forms;
using JwDev.Core.Controls.Grid;
using JwDev.Core.Utils;

namespace JwDev.Core.Forms.Sales
{
	public partial class SaleStatListForm : EditForm
	{
		public SaleStatListForm()
		{
			InitializeComponent();
		}

		protected override void InitButtons()

		{
			SetToolbarButtons(new Models.ToolbarButtons() { Refresh = true });
		}
		protected override void InitControls()
		{
			base.InitControls();

			SetFieldNames();

			datSaleDate.Init();
			InitGrid();
		}
		void InitGrid()
		{
			gridSaleProduct.Init();
			gridSaleProduct.ShowFooter = true;
			gridSaleProduct.AddGridColumns(new XGridColumn[]
			{
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "PRODUCT_ID", HorzAlignment = HorzAlignment.Center, Width = 80, Visible = false },
				new XGridColumn() { FieldName = "PRODUCT_CODE", HorzAlignment = HorzAlignment.Center, Width = 80 },
				new XGridColumn() { FieldName = "PRODUCT_NAME", Width = 200 },
				new XGridColumn() { FieldName = "SALE_QTY", Width = 100, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum },
				new XGridColumn() { FieldName = "SALE_AMT", Width = 120, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum }
			});
			gridSaleProduct.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridSaleProduct.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridSaleProduct.ColumnFix("ROW_NO");

			gridSaleCustomer.Init();
			gridSaleCustomer.ShowFooter = true;
			gridSaleCustomer.AddGridColumns(new XGridColumn[]
			{
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "CUSTOMER_ID", HorzAlignment = HorzAlignment.Center, Width = 80, Visible = false },
				new XGridColumn() { FieldName = "CUSTOMER_NAME", Width = 200 },
				new XGridColumn() { FieldName = "SALE_QTY", Width = 100, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum },
				new XGridColumn() { FieldName = "SALE_AMT", Width = 120, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum }
			});
			gridSaleCustomer.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridSaleCustomer.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridSaleCustomer.ColumnFix("ROW_NO");

			gridSaleCategory.Init();
			gridSaleCategory.ShowFooter = true;
			gridSaleCategory.AddGridColumns(new XGridColumn[]
			{
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "CATEGORY", Width = 200 },
				new XGridColumn() { FieldName = "SALE_QTY", Width = 100, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum },
				new XGridColumn() { FieldName = "SALE_AMT", Width = 120, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum }
			});
			gridSaleCategory.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridSaleCategory.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridSaleCategory.ColumnFix("ROW_NO");

			gridSalePayType.Init();
			gridSalePayType.ShowFooter = true;
			gridSalePayType.AddGridColumns(new XGridColumn[]
			{
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "PAY_TYPE", Width = 100 },
				new XGridColumn() { FieldName = "SALE_QTY", Width = 100, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum },
				new XGridColumn() { FieldName = "SALE_AMT", Width = 120, HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", IsSummary = true, SummaryItemType = SummaryItemType.Sum }
			});
			gridSalePayType.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridSalePayType.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridSalePayType.ColumnFix("ROW_NO");

		}

		protected override void DataLoad(object param = null)
		{
			try
			{
				DataMap p = new DataMap();
				p.SetValue("ST_SALE_DATE", datSaleDate.DateFrEdit.GetDateChar8());
				p.SetValue("ED_SALE_DATE", datSaleDate.DateToEdit.GetDateChar8());

				var res = WasHelper.GetData("Sales", "GetSaleStat", p);

				if (res.Requests.Length == 0)
					throw new Exception("조회 데이터가 없습니다.");

				if (res.Requests.Length > 0)
					gridSaleProduct.DataSource = (res.Requests[0].Data as List<DataMap>).DataMapListToDataTable("Data1");

				if (res.Requests.Length > 1)
					gridSaleCustomer.DataSource = (res.Requests[1].Data as List<DataMap>).DataMapListToDataTable("Data2");

				if (res.Requests.Length > 2)
					gridSaleCategory.DataSource = (res.Requests[2].Data as List<DataMap>).DataMapListToDataTable("Data3");

				if (res.Requests.Length > 3)
					gridSalePayType.DataSource = (res.Requests[3].Data as List<DataMap>).DataMapListToDataTable("Data4");

			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}
	}
}