﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using JwDev.Base.DBTran.Controller;
using JwDev.Base.DBTran.Model;
using JwDev.Base.Map;
using JwDev.Base.Utils;
using JwDev.Core.Base.Forms;
using JwDev.Core.Controls.Grid;
using JwDev.Core.Enumerations;
using JwDev.Core.Helper;
using JwDev.Core.Models;
using JwDev.Core.Utils;
using JwDev.Model.Codes;

namespace JwDev.Core.Forms.Code
{
	public partial class ProductsForm : EditForm
	{
		public ProductsForm()
		{
			InitializeComponent();

			lupProductType.EditValueChanged += delegate (object sender, EventArgs e)
			{
				if (this.IsLoaded)
					onProductTypeChanged();
			};
		}

		void onProductTypeChanged()
		{
			if (lupProductType.EditValue.ToStringNullToEmpty() == "M")
			{
				btnLineAdd.Enabled =
					btnLineDel.Enabled =
					btnSave.Enabled = false;
			}
			else
			{
				if (this.EditMode == EditModeEnum.New)
				{
					btnLineAdd.Enabled =
						btnLineDel.Enabled = true;
					btnSave.Enabled = false;
				}
				else
				{
					btnLineAdd.Enabled =
						btnLineDel.Enabled =
						btnSave.Enabled = true;
				}
			}
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
			btnLineAdd.Enabled = btnLineDel.Enabled = btnSave.Enabled = this.IsDataEdit;

			btnLineAdd.Click += delegate (object sender, EventArgs e)
			{
				int rowindex = gridMaterials.AddNewRow();
				gridMaterials.SetFocus(rowindex, "MATERIAL_NAME");
			};
			btnLineDel.Click += delegate (object sender, EventArgs e)
			{
				if (gridMaterials.MainView.FocusedRowHandle < 0)
					return;
				gridMaterials.MainView.DeleteRow(gridMaterials.MainView.FocusedRowHandle);
			};
			btnSave.Click += delegate (object sender, EventArgs e)
			{
				DataSaveMaterials();
			};
		}

		protected override void InitControls()
		{
			base.InitControls();

			lcItemProductName.Tag = true;

			SetFieldNames();
			
			txtProductId.SetEnable(false);
			txtProductCode.SetEnable(false);
			txtInsTime.SetEnable(false);
			txtInsUserName.SetEnable(false);
			txtUpdTime.SetEnable(false);
			txtUpdUserName.SetEnable(false);

			InitCombo();
			InitGrid();

			lcTabGroup.SelectedTabPage = lcTabGroupMaterials;
		}

		void InitCombo()
		{
			lupSchProductType.BindData("PRODUCT_TYPE", null, "ALL", true);
			lupProductType.BindData("PRODUCT_TYPE", null, null, true);
			lupCategory.BindData("CATEGORY", null, null, true);
			lupUnitType.BindData("UNIT_TYPE", null, null, true);
		}

		void InitGrid()
		{
			#region 조회리스트
			gridList.Init();
			gridList.AddGridColumns(
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "PRODUCT_ID", HorzAlignment = HorzAlignment.Center, Width = 60, Visible = false },
				new XGridColumn() { FieldName = "PRODUCT_CODE", HorzAlignment = HorzAlignment.Center, Width = 80 },
				new XGridColumn() { FieldName = "PRODUCT_NAME", Width = 200 },
				new XGridColumn() { FieldName = "PRODUCT_TYPE", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "CATEGORY", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "USE_YN", HorzAlignment = HorzAlignment.Center, RepositoryItem = gridList.GetRepositoryItemCheckEdit(), Width = 80 }
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
					if (e.Button == MouseButtons.Left && e.Clicks == 1)
					{
						GridView view = sender as GridView;
						DetailDataLoad(view.GetRowCellValue(e.RowHandle, "PRODUCT_ID"));
					}
				}
				catch(Exception ex)
				{
					ShowErrBox(ex);
				}
			};
			#endregion

			#region 원부자재목록
			gridMaterials.Init();

			#region Grid Add Columns
			gridMaterials.AddGridColumns(
				new XGridColumn() { FieldName = "ROW_NO" },
				new XGridColumn() { FieldName = "REG_ID", HorzAlignment = HorzAlignment.Center, Width = 40, Visible = false },
				new XGridColumn() { FieldName = "PRODUCT_ID", HorzAlignment = HorzAlignment.Center, Width = 50, Visible = false },
				new XGridColumn() { FieldName = "MATERIAL_NAME", Width = 200 },
				new XGridColumn() { FieldName = "MATERIAL_ID", HorzAlignment = HorzAlignment.Center, Width = 100 },
				new XGridColumn() { FieldName = "INPUT_QTY", HorzAlignment = HorzAlignment.Far, FormatType = FormatType.Numeric, FormatString = "N0", Width = 80 },
				new XGridColumn() { FieldName = "UNIT_TYPE", HorzAlignment = HorzAlignment.Center, Width = 80 },
				new XGridColumn() { FieldName = "INS_TIME" },
				new XGridColumn() { FieldName = "INS_USER_NAME" },
				new XGridColumn() { FieldName = "UPD_TIME" },
				new XGridColumn() { FieldName = "UPD_USER_NAME" }
				);
			#endregion

			gridMaterials.SetRepositoryItemButtonEdit("MATERIAL_NAME");
			gridMaterials.SetEditable("MATERIAL_NAME", "INPUT_QTY");

			gridMaterials.SetColumnBackColor(SkinUtils.ForeColor, "ROW_NO");
			gridMaterials.SetColumnForeColor(SkinUtils.BackColor, "ROW_NO");
			gridMaterials.ColumnFix("ROW_NO");

			#region Grid Events
			(gridMaterials.MainView.Columns["MATERIAL_NAME"].ColumnEdit as RepositoryItemButtonEdit).KeyDown += delegate (object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Enter)
				{
					doSearchMaterial();
				}
			};
			(gridMaterials.MainView.Columns["MATERIAL_NAME"].ColumnEdit as RepositoryItemButtonEdit).EditValueChanged += delegate (object sender, EventArgs e)
			{
				if (gridMaterials.GetValue(gridMaterials.MainView.FocusedRowHandle, "MATERIAL_NAME").ToStringNullToEmpty() == "")
				{
					gridMaterials.SetValue(gridMaterials.MainView.FocusedRowHandle, "MATERIAL_NAME", null);
					gridMaterials.SetValue(gridMaterials.MainView.FocusedRowHandle, "MATERIAL_ID", null);
				}
			};
			(gridMaterials.MainView.Columns["MATERIAL_NAME"].ColumnEdit as RepositoryItemButtonEdit).ButtonClick += delegate (object sender, ButtonPressedEventArgs e)
			{
				doSearchMaterial();
			};
			#endregion

			#endregion
		}

		void doSearchMaterial()
		{
			try
			{
				gridMaterials.PostEditor();
				gridMaterials.UpdateCurrentRow();

				int rowIndex = gridMaterials.MainView.FocusedRowHandle;
				object findtext = gridMaterials.GetValue(rowIndex, "MATERIAL_NAME");
				DataMap map = CodeHelper.ShowForm("MATERIAL", new DataMap() { { "FIND_TEXT", findtext } });
				if (map != null && map.GetType() == typeof(DataMap) && map.GetValue("MATERIAL_ID").ToStringNullToEmpty() != "")
				{
					gridMaterials.SetValue(rowIndex, "MATERIAL_NAME", map.GetValue("MATERIAL_NAME"));
					gridMaterials.SetValue(rowIndex, "MATERIAL_ID", map.GetValue("MATERIAL_ID"));
					gridMaterials.SetValue(rowIndex, "UNIT_TYPE", map.GetValue("UNIT_TYPE"));
					gridMaterials.SetFocus(rowIndex, "INPUT_QTY");
				}
			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
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
				txtProductId.Clear();
				txtProductCode.Clear();
				txtProductName.Clear();
				txtBarcode.Clear();
				chkUseYn.Checked = true;
				memRemarks.Clear();

				txtInsTime.Clear();
				txtInsUserName.Clear();
				txtUpdTime.Clear();
				txtUpdUserName.Clear();

				gridMaterials.DataSource = null;
				gridMaterials.EmptyDataTableBinding();

				onProductTypeChanged();

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
				gridList.BindData("Product", "GetList", null, new DataMap() { { "PRODUCT_TYPE", lupSchProductType.EditValue }, { "FIND_TEXT", txtFindText.EditValue } });

				if (param != null)
					DetailDataLoad(param);
				else
					DataInit();
			}
			catch(Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		void DetailDataLoad(object id)
		{
			try
			{
				var res = RequestHelper.GetData("Product", new DataMap() { { "PRODUCT_ID", id } });
				if (res.Requests.Length > 0)
				{
					if (res.Requests[0].Data == null)
						throw new Exception("조회 데이터가 없습니다.");

					ProductDataModel model = (ProductDataModel)res.Requests[0].Data;

					txtProductId.EditValue = model.PRODUCT_ID;
					txtProductCode.EditValue = model.PRODUCT_CODE;
					txtProductName.EditValue = model.PRODUCT_NAME;
					txtBarcode.EditValue = model.BARCODE;
					lupProductType.EditValue = model.PRODUCT_TYPE;
					lupCategory.EditValue = model.CATEGORY;
					lupUnitType.EditValue = model.UNIT_TYPE;
					chkUseYn.EditValue = model.USE_YN;
					memRemarks.EditValue = model.REMARKS;

					txtInsTime.EditValue = model.INS_TIME;
					txtInsUserName.EditValue = model.INS_USER_NAME;
					txtUpdTime.EditValue = model.UPD_TIME;
					txtUpdUserName.EditValue = model.UPD_USER_NAME;				
				}

				if (res.Requests.Length > 1)
				{
					gridMaterials.DataSource = (res.Requests[1].Data as List<DataMap>).DataMapListToDataTable();
				}

				onProductTypeChanged();

				SetToolbarButtons(new ToolbarButtons() { New = true, Refresh = true, Save = true, SaveAndNew = true, Delete = true });
				this.EditMode = EditModeEnum.Modify;
				txtProductName.Focus();
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
				DataMap map = lcGroupEdit.ItemToDataMap();
				map.SetValue("ROWSTATE", (this.EditMode == EditModeEnum.New) ? "INSERT" : "UPDATE");

				var res = RequestHelper.Execute(new RequestDataSet()
				{
					ServiceId = "Product",
					ProcessId = "Save",
					Requests = new RequestData[]
					{
						new RequestData() { Data = map },
						new RequestData() { Data = GetMaterialData() }
					}
				});
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
					{ "PRODUCT_ID", txtProductId.EditValue },
					{ "ROWSTATE", "DELETE" }
				};

				var res = RequestHelper.Execute("Base", "Save", "Product", data, null);
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

		void DataLoadMaterials()
		{
			try
			{
				gridMaterials.BindData("Base", "GetList", "SelectProductMaterials", new DataMap() { { "PRODUCT_ID", txtProductId.EditValue } });
			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}
		void DataSaveMaterials()
		{
			try
			{
				DataTable dt = GetMaterialData();

				if (dt == null || dt.Rows.Count == 0)
					throw new Exception("저장할 건이 없습니다.");

				var res = RequestHelper.Execute(new RequestDataSet()
				{
					ServiceId = "Base",
					ProcessId = "Save",
					Requests = new RequestData[] 
					{
						new RequestData()
						{
							SqlId = "ProductMaterial",
							Data = dt
						}
					}
				});
				if (res.ErrorNumber != 0)
					throw new Exception(res.ErrorMessage);

				ShowMsgBox("저장하였습니다.");
				DataLoadMaterials();
			}
			catch (Exception ex)
			{
				ShowErrBox(ex);
			}
		}

		private DataTable GetMaterialData()
		{
			try
			{
				DataTable dt = new DataTable();
				dt.Columns.AddRange(new DataColumn[]
				{
					new DataColumn("REG_ID", typeof(int)),
					new DataColumn("PRODUCT_ID", typeof(int)),
					new DataColumn("MATERIAL_ID", typeof(int)),
					new DataColumn("INPUT_QTY", typeof(int)),
					new DataColumn("ROWSTATE", typeof(string))
				});

				if (gridMaterials.MainView.RowCount > 0)
				{
					gridMaterials.PostEditor();
					gridMaterials.UpdateCurrentRow();

					foreach (DataRow row in gridMaterials.GetDataTable().GetChangedData().Rows)
					{
						string rowstate = row["ROWSTATE"].ToString();

						dt.Rows.Add(
							row["REG_ID"],
							txtProductId.EditValue,
							row["MATERIAL_ID"],
							row["INPUT_QTY"],
							rowstate
							);
					}
				}

				return dt;
			}
			catch
			{
				throw;
			}
		}
	}
}