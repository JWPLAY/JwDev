﻿namespace JwDev.Core.Forms.Purchase
{
	partial class PurcTranForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lcGroupEdit1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lcItemCustomer = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtCustomerId = new JwDev.Core.Controls.Common.XSearch();
			this.lcItemPurcDate = new DevExpress.XtraLayout.LayoutControlItem();
			this.datPurcDate = new DevExpress.XtraEditors.DateEdit();
			this.lcItemPurcId = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtPurcId = new DevExpress.XtraEditors.TextEdit();
			this.lcItemPurcNo = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtPurcNo = new DevExpress.XtraEditors.TextEdit();
			this.lcItemPurcType = new DevExpress.XtraLayout.LayoutControlItem();
			this.lupPurcType = new JwDev.Core.Controls.Common.XLookup();
			this.lcItemRemarks = new DevExpress.XtraLayout.LayoutControlItem();
			this.memRemarks = new DevExpress.XtraEditors.MemoEdit();
			this.gridItem = new JwDev.Core.Controls.Grid.XGrid();
			this.lcTab = new DevExpress.XtraLayout.TabbedControlGroup();
			this.lcTabGroupItem = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lcGridList = new DevExpress.XtraLayout.LayoutControlItem();
			this.lcTabGroupItemButtons = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lcButtonItemAdd = new DevExpress.XtraLayout.LayoutControlItem();
			this.btnItemAdd = new DevExpress.XtraEditors.SimpleButton();
			this.lcButtonItemDel = new DevExpress.XtraLayout.LayoutControlItem();
			this.btnItemDel = new DevExpress.XtraEditors.SimpleButton();
			this.lcGroupInfoReg = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lcItemInsTime = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtInsTime = new DevExpress.XtraEditors.TextEdit();
			this.lcItemUpdTime = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtUpdTime = new DevExpress.XtraEditors.TextEdit();
			this.lcItemInsUserName = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtInsUserName = new DevExpress.XtraEditors.TextEdit();
			this.lcItemUpdUserName = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtUpdUserName = new DevExpress.XtraEditors.TextEdit();
			this.lcGroupEdit2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lcGroupEdit = new DevExpress.XtraLayout.LayoutControlGroup();
			((System.ComponentModel.ISupportInitialize)(this.lc)).BeginInit();
			this.lc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupBase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemCustomer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.datPurcDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.datPurcDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcId)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPurcId.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcNo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPurcNo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lupPurcType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemRemarks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memRemarks.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTab)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGridList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupItemButtons)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcButtonItemAdd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcButtonItemDel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupInfoReg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemInsTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInsTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemUpdTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpdTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemInsUserName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInsUserName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemUpdUserName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpdUserName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// lc
			// 
			this.lc.Controls.Add(this.txtUpdUserName);
			this.lc.Controls.Add(this.txtInsUserName);
			this.lc.Controls.Add(this.txtUpdTime);
			this.lc.Controls.Add(this.txtInsTime);
			this.lc.Controls.Add(this.memRemarks);
			this.lc.Controls.Add(this.txtPurcNo);
			this.lc.Controls.Add(this.txtPurcId);
			this.lc.Controls.Add(this.lupPurcType);
			this.lc.Controls.Add(this.datPurcDate);
			this.lc.Controls.Add(this.btnItemDel);
			this.lc.Controls.Add(this.btnItemAdd);
			this.lc.Controls.Add(this.txtCustomerId);
			this.lc.Controls.Add(this.gridItem);
			this.lc.Location = new System.Drawing.Point(0, 44);
			this.lc.Margin = new System.Windows.Forms.Padding(0);
			this.lc.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(922, 345, 450, 400);
			this.lc.Padding = new System.Windows.Forms.Padding(2);
			this.lc.Size = new System.Drawing.Size(998, 502);
			// 
			// lcGroupBase
			// 
			this.lcGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcTab,
            this.lcGroupInfoReg,
            this.lcGroupEdit});
			this.lcGroupBase.Name = "Root";
			this.lcGroupBase.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.lcGroupBase.Size = new System.Drawing.Size(998, 502);
			// 
			// lcGroupEdit1
			// 
			this.lcGroupEdit1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemCustomer,
            this.lcItemPurcDate,
            this.lcItemPurcId,
            this.lcItemPurcNo,
            this.lcItemPurcType});
			this.lcGroupEdit1.Location = new System.Drawing.Point(0, 0);
			this.lcGroupEdit1.Name = "lcGroupEdit1";
			this.lcGroupEdit1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
			this.lcGroupEdit1.Size = new System.Drawing.Size(507, 86);
			this.lcGroupEdit1.TextVisible = false;
			// 
			// lcItemCustomer
			// 
			this.lcItemCustomer.Control = this.txtCustomerId;
			this.lcItemCustomer.Location = new System.Drawing.Point(0, 48);
			this.lcItemCustomer.Name = "lcItemCustomer";
			this.lcItemCustomer.Size = new System.Drawing.Size(493, 24);
			this.lcItemCustomer.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtCustomerId
			// 
			this.txtCustomerId.CodeField = "CODE";
			this.txtCustomerId.CodeGroup = "CUSTOMER";
			this.txtCustomerId.CodeWidth = 100;
			this.txtCustomerId.DisplayFields = new string[] {
        "CodeId",
        "CodeName"};
			this.txtCustomerId.EditText = null;
			this.txtCustomerId.EditValue = null;
			this.txtCustomerId.Location = new System.Drawing.Point(126, 59);
			this.txtCustomerId.MaximumSize = new System.Drawing.Size(0, 20);
			this.txtCustomerId.MinimumSize = new System.Drawing.Size(0, 20);
			this.txtCustomerId.Name = "txtCustomerId";
			this.txtCustomerId.NameField = "NAME";
			this.txtCustomerId.Parameters = null;
			this.txtCustomerId.Size = new System.Drawing.Size(374, 20);
			this.txtCustomerId.TabIndex = 6;
			// 
			// lcItemPurcDate
			// 
			this.lcItemPurcDate.Control = this.datPurcDate;
			this.lcItemPurcDate.Location = new System.Drawing.Point(0, 24);
			this.lcItemPurcDate.Name = "lcItemPurcDate";
			this.lcItemPurcDate.Size = new System.Drawing.Size(246, 24);
			this.lcItemPurcDate.TextSize = new System.Drawing.Size(111, 14);
			// 
			// datPurcDate
			// 
			this.datPurcDate.EditValue = null;
			this.datPurcDate.Location = new System.Drawing.Point(126, 35);
			this.datPurcDate.Name = "datPurcDate";
			this.datPurcDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.datPurcDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.datPurcDate.Size = new System.Drawing.Size(127, 20);
			this.datPurcDate.StyleController = this.lc;
			this.datPurcDate.TabIndex = 10;
			// 
			// lcItemPurcId
			// 
			this.lcItemPurcId.Control = this.txtPurcId;
			this.lcItemPurcId.Location = new System.Drawing.Point(0, 0);
			this.lcItemPurcId.Name = "lcItemPurcId";
			this.lcItemPurcId.Size = new System.Drawing.Size(246, 24);
			this.lcItemPurcId.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtPurcId
			// 
			this.txtPurcId.Location = new System.Drawing.Point(126, 11);
			this.txtPurcId.Name = "txtPurcId";
			this.txtPurcId.Size = new System.Drawing.Size(127, 20);
			this.txtPurcId.StyleController = this.lc;
			this.txtPurcId.TabIndex = 12;
			// 
			// lcItemPurcNo
			// 
			this.lcItemPurcNo.Control = this.txtPurcNo;
			this.lcItemPurcNo.Location = new System.Drawing.Point(246, 0);
			this.lcItemPurcNo.Name = "lcItemPurcNo";
			this.lcItemPurcNo.Size = new System.Drawing.Size(247, 24);
			this.lcItemPurcNo.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtPurcNo
			// 
			this.txtPurcNo.Location = new System.Drawing.Point(372, 11);
			this.txtPurcNo.Name = "txtPurcNo";
			this.txtPurcNo.Size = new System.Drawing.Size(128, 20);
			this.txtPurcNo.StyleController = this.lc;
			this.txtPurcNo.TabIndex = 13;
			// 
			// lcItemPurcType
			// 
			this.lcItemPurcType.Control = this.lupPurcType;
			this.lcItemPurcType.Location = new System.Drawing.Point(246, 24);
			this.lcItemPurcType.Name = "lcItemPurcType";
			this.lcItemPurcType.Size = new System.Drawing.Size(247, 24);
			this.lcItemPurcType.TextSize = new System.Drawing.Size(111, 14);
			// 
			// lupPurcType
			// 
			this.lupPurcType.DataSource = null;
			this.lupPurcType.DisplayMember = "";
			this.lupPurcType.GroupCode = null;
			this.lupPurcType.ListMember = "LIST_NAME";
			this.lupPurcType.Location = new System.Drawing.Point(372, 35);
			this.lupPurcType.Name = "lupPurcType";
			this.lupPurcType.NullText = "[EditValue is null]";
			this.lupPurcType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lupPurcType.SelectedIndex = -1;
			this.lupPurcType.Size = new System.Drawing.Size(128, 20);
			this.lupPurcType.StyleController = this.lc;
			this.lupPurcType.TabIndex = 11;
			this.lupPurcType.ValueMember = "";
			// 
			// lcItemRemarks
			// 
			this.lcItemRemarks.Control = this.memRemarks;
			this.lcItemRemarks.Location = new System.Drawing.Point(0, 0);
			this.lcItemRemarks.Name = "lcItemRemarks";
			this.lcItemRemarks.Size = new System.Drawing.Size(473, 72);
			this.lcItemRemarks.TextSize = new System.Drawing.Size(111, 14);
			// 
			// memRemarks
			// 
			this.memRemarks.Location = new System.Drawing.Point(633, 11);
			this.memRemarks.Name = "memRemarks";
			this.memRemarks.Size = new System.Drawing.Size(354, 68);
			this.memRemarks.StyleController = this.lc;
			this.memRemarks.TabIndex = 14;
			// 
			// gridItem
			// 
			this.gridItem.Compress = false;
			this.gridItem.DataSource = null;
			this.gridItem.Editable = true;
			this.gridItem.FocusedRowHandle = -2147483648;
			this.gridItem.GridViewType = JwDev.Core.Controls.Grid.GridViewType.GridView;
			this.gridItem.Location = new System.Drawing.Point(9, 155);
			this.gridItem.Name = "gridItem";
			this.gridItem.PageFooterCenter = null;
			this.gridItem.PageFooterLeft = null;
			this.gridItem.PageFooterRight = null;
			this.gridItem.PageHeaderCenter = null;
			this.gridItem.PageHeaderLeft = null;
			this.gridItem.PageHeaderRight = null;
			this.gridItem.Pager = null;
			this.gridItem.PrintFooter = null;
			this.gridItem.PrintHeader = null;
			this.gridItem.ReadOnly = false;
			this.gridItem.ShowFooter = false;
			this.gridItem.ShowGroupPanel = false;
			this.gridItem.Size = new System.Drawing.Size(980, 276);
			this.gridItem.TabIndex = 4;
			// 
			// lcTab
			// 
			this.lcTab.Location = new System.Drawing.Point(0, 86);
			this.lcTab.Name = "lcTab";
			this.lcTab.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.lcTab.SelectedTabPage = this.lcTabGroupItem;
			this.lcTab.SelectedTabPageIndex = 0;
			this.lcTab.Size = new System.Drawing.Size(994, 350);
			this.lcTab.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcTabGroupItem});
			// 
			// lcTabGroupItem
			// 
			this.lcTabGroupItem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGridList,
            this.lcTabGroupItemButtons});
			this.lcTabGroupItem.Location = new System.Drawing.Point(0, 0);
			this.lcTabGroupItem.Name = "lcTabGroupItem";
			this.lcTabGroupItem.Size = new System.Drawing.Size(984, 316);
			this.lcTabGroupItem.Text = "품목등록";
			// 
			// lcGridList
			// 
			this.lcGridList.Control = this.gridItem;
			this.lcGridList.Location = new System.Drawing.Point(0, 36);
			this.lcGridList.Name = "lcGridList";
			this.lcGridList.Size = new System.Drawing.Size(984, 280);
			this.lcGridList.TextSize = new System.Drawing.Size(0, 0);
			this.lcGridList.TextVisible = false;
			// 
			// lcTabGroupItemButtons
			// 
			this.lcTabGroupItemButtons.BackgroundImage = global::JwDev.Core.Properties.Resources.back_gray;
			this.lcTabGroupItemButtons.BackgroundImageVisible = true;
			this.lcTabGroupItemButtons.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem4,
            this.lcButtonItemAdd,
            this.lcButtonItemDel});
			this.lcTabGroupItemButtons.Location = new System.Drawing.Point(0, 0);
			this.lcTabGroupItemButtons.Name = "lcTabGroupItemButtons";
			this.lcTabGroupItemButtons.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.lcTabGroupItemButtons.Size = new System.Drawing.Size(984, 36);
			this.lcTabGroupItemButtons.TextVisible = false;
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(200, 0);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(774, 26);
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// lcButtonItemAdd
			// 
			this.lcButtonItemAdd.Control = this.btnItemAdd;
			this.lcButtonItemAdd.Location = new System.Drawing.Point(0, 0);
			this.lcButtonItemAdd.Name = "lcButtonItemAdd";
			this.lcButtonItemAdd.Size = new System.Drawing.Size(100, 26);
			this.lcButtonItemAdd.TextSize = new System.Drawing.Size(0, 0);
			this.lcButtonItemAdd.TextVisible = false;
			// 
			// btnItemAdd
			// 
			this.btnItemAdd.Location = new System.Drawing.Point(14, 124);
			this.btnItemAdd.Name = "btnItemAdd";
			this.btnItemAdd.Size = new System.Drawing.Size(96, 22);
			this.btnItemAdd.StyleController = this.lc;
			this.btnItemAdd.TabIndex = 8;
			this.btnItemAdd.Text = "추가";
			// 
			// lcButtonItemDel
			// 
			this.lcButtonItemDel.Control = this.btnItemDel;
			this.lcButtonItemDel.Location = new System.Drawing.Point(100, 0);
			this.lcButtonItemDel.Name = "lcButtonItemDel";
			this.lcButtonItemDel.Size = new System.Drawing.Size(100, 26);
			this.lcButtonItemDel.TextSize = new System.Drawing.Size(0, 0);
			this.lcButtonItemDel.TextVisible = false;
			// 
			// btnItemDel
			// 
			this.btnItemDel.Location = new System.Drawing.Point(114, 124);
			this.btnItemDel.Name = "btnItemDel";
			this.btnItemDel.Size = new System.Drawing.Size(96, 22);
			this.btnItemDel.StyleController = this.lc;
			this.btnItemDel.TabIndex = 9;
			this.btnItemDel.Text = "삭제";
			// 
			// lcGroupInfoReg
			// 
			this.lcGroupInfoReg.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemInsTime,
            this.lcItemUpdTime,
            this.lcItemInsUserName,
            this.lcItemUpdUserName});
			this.lcGroupInfoReg.Location = new System.Drawing.Point(0, 436);
			this.lcGroupInfoReg.Name = "lcGroupInfoReg";
			this.lcGroupInfoReg.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
			this.lcGroupInfoReg.Size = new System.Drawing.Size(994, 62);
			this.lcGroupInfoReg.TextVisible = false;
			// 
			// lcItemInsTime
			// 
			this.lcItemInsTime.Control = this.txtInsTime;
			this.lcItemInsTime.Location = new System.Drawing.Point(0, 0);
			this.lcItemInsTime.Name = "lcItemInsTime";
			this.lcItemInsTime.Size = new System.Drawing.Size(490, 24);
			this.lcItemInsTime.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtInsTime
			// 
			this.txtInsTime.Location = new System.Drawing.Point(126, 447);
			this.txtInsTime.Name = "txtInsTime";
			this.txtInsTime.Size = new System.Drawing.Size(371, 20);
			this.txtInsTime.StyleController = this.lc;
			this.txtInsTime.TabIndex = 15;
			// 
			// lcItemUpdTime
			// 
			this.lcItemUpdTime.Control = this.txtUpdTime;
			this.lcItemUpdTime.Location = new System.Drawing.Point(490, 0);
			this.lcItemUpdTime.Name = "lcItemUpdTime";
			this.lcItemUpdTime.Size = new System.Drawing.Size(490, 24);
			this.lcItemUpdTime.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtUpdTime
			// 
			this.txtUpdTime.Location = new System.Drawing.Point(616, 447);
			this.txtUpdTime.Name = "txtUpdTime";
			this.txtUpdTime.Size = new System.Drawing.Size(371, 20);
			this.txtUpdTime.StyleController = this.lc;
			this.txtUpdTime.TabIndex = 16;
			// 
			// lcItemInsUserName
			// 
			this.lcItemInsUserName.Control = this.txtInsUserName;
			this.lcItemInsUserName.Location = new System.Drawing.Point(0, 24);
			this.lcItemInsUserName.Name = "lcItemInsUserName";
			this.lcItemInsUserName.Size = new System.Drawing.Size(490, 24);
			this.lcItemInsUserName.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtInsUserName
			// 
			this.txtInsUserName.Location = new System.Drawing.Point(126, 471);
			this.txtInsUserName.Name = "txtInsUserName";
			this.txtInsUserName.Size = new System.Drawing.Size(371, 20);
			this.txtInsUserName.StyleController = this.lc;
			this.txtInsUserName.TabIndex = 17;
			// 
			// lcItemUpdUserName
			// 
			this.lcItemUpdUserName.Control = this.txtUpdUserName;
			this.lcItemUpdUserName.Location = new System.Drawing.Point(490, 24);
			this.lcItemUpdUserName.Name = "lcItemUpdUserName";
			this.lcItemUpdUserName.Size = new System.Drawing.Size(490, 24);
			this.lcItemUpdUserName.TextSize = new System.Drawing.Size(111, 14);
			// 
			// txtUpdUserName
			// 
			this.txtUpdUserName.Location = new System.Drawing.Point(616, 471);
			this.txtUpdUserName.Name = "txtUpdUserName";
			this.txtUpdUserName.Size = new System.Drawing.Size(371, 20);
			this.txtUpdUserName.StyleController = this.lc;
			this.txtUpdUserName.TabIndex = 18;
			// 
			// lcGroupEdit2
			// 
			this.lcGroupEdit2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemRemarks});
			this.lcGroupEdit2.Location = new System.Drawing.Point(507, 0);
			this.lcGroupEdit2.Name = "lcGroupEdit2";
			this.lcGroupEdit2.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
			this.lcGroupEdit2.Size = new System.Drawing.Size(487, 86);
			this.lcGroupEdit2.TextVisible = false;
			// 
			// lcGroupEdit
			// 
			this.lcGroupEdit.GroupBordersVisible = false;
			this.lcGroupEdit.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGroupEdit1,
            this.lcGroupEdit2});
			this.lcGroupEdit.Location = new System.Drawing.Point(0, 0);
			this.lcGroupEdit.Name = "lcGroupEdit";
			this.lcGroupEdit.Size = new System.Drawing.Size(994, 86);
			this.lcGroupEdit.TextVisible = false;
			// 
			// PurcTranForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(998, 568);
			this.Name = "PurcTranForm";
			this.Text = "PurcTranForm";
			((System.ComponentModel.ISupportInitialize)(this.lc)).EndInit();
			this.lc.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lcGroupBase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemCustomer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.datPurcDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.datPurcDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcId)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPurcId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcNo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPurcNo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemPurcType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lupPurcType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemRemarks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memRemarks.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTab)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGridList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupItemButtons)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcButtonItemAdd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcButtonItemDel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupInfoReg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemInsTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInsTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemUpdTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpdTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemInsUserName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtInsUserName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemUpdUserName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUpdUserName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupEdit)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private DevExpress.XtraLayout.LayoutControlGroup lcGroupEdit1;
		private Controls.Grid.XGrid gridItem;
		private Controls.Common.XSearch txtCustomerId;
		private DevExpress.XtraLayout.LayoutControlItem lcItemCustomer;
		private DevExpress.XtraLayout.TabbedControlGroup lcTab;
		private DevExpress.XtraLayout.LayoutControlGroup lcTabGroupItem;
		private DevExpress.XtraLayout.LayoutControlItem lcGridList;
		private DevExpress.XtraLayout.LayoutControlGroup lcTabGroupItemButtons;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraEditors.SimpleButton btnItemDel;
		private DevExpress.XtraEditors.SimpleButton btnItemAdd;
		private DevExpress.XtraLayout.LayoutControlItem lcButtonItemAdd;
		private DevExpress.XtraLayout.LayoutControlItem lcButtonItemDel;
		private DevExpress.XtraEditors.DateEdit datPurcDate;
		private DevExpress.XtraLayout.LayoutControlItem lcItemPurcDate;
		private Controls.Common.XLookup lupPurcType;
		private DevExpress.XtraLayout.LayoutControlItem lcItemPurcType;
		private DevExpress.XtraEditors.TextEdit txtPurcNo;
		private DevExpress.XtraEditors.TextEdit txtPurcId;
		private DevExpress.XtraLayout.LayoutControlItem lcItemPurcId;
		private DevExpress.XtraLayout.LayoutControlItem lcItemPurcNo;
		private DevExpress.XtraEditors.MemoEdit memRemarks;
		private DevExpress.XtraLayout.LayoutControlItem lcItemRemarks;
		private DevExpress.XtraLayout.LayoutControlGroup lcGroupInfoReg;
		private DevExpress.XtraEditors.TextEdit txtUpdUserName;
		private DevExpress.XtraEditors.TextEdit txtInsUserName;
		private DevExpress.XtraEditors.TextEdit txtUpdTime;
		private DevExpress.XtraEditors.TextEdit txtInsTime;
		private DevExpress.XtraLayout.LayoutControlItem lcItemInsTime;
		private DevExpress.XtraLayout.LayoutControlItem lcItemUpdTime;
		private DevExpress.XtraLayout.LayoutControlItem lcItemInsUserName;
		private DevExpress.XtraLayout.LayoutControlItem lcItemUpdUserName;
		private DevExpress.XtraLayout.LayoutControlGroup lcGroupEdit2;
		private DevExpress.XtraLayout.LayoutControlGroup lcGroupEdit;
	}
}