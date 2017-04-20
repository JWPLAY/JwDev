﻿namespace JwDev.Core.Messages
{
    partial class ErrorMessageBox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageBox));
			this.lc = new DevExpress.XtraLayout.LayoutControl();
			this.memStackTrace = new DevExpress.XtraEditors.MemoEdit();
			this.btnOk = new DevExpress.XtraEditors.SimpleButton();
			this.memMessage = new DevExpress.XtraEditors.MemoEdit();
			this.lcGroupBase = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lcTabGroup = new DevExpress.XtraLayout.TabbedControlGroup();
			this.lcTabGroupMessage = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lcItemMessage = new DevExpress.XtraLayout.LayoutControlItem();
			this.lcTabGroupStackTrace = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lcItemStackTrace = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.lc)).BeginInit();
			this.lc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memStackTrace.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memMessage.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupBase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroup)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupMessage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemMessage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupStackTrace)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemStackTrace)).BeginInit();
			this.SuspendLayout();
			// 
			// lc
			// 
			this.lc.AllowCustomization = false;
			this.lc.Controls.Add(this.memStackTrace);
			this.lc.Controls.Add(this.btnOk);
			this.lc.Controls.Add(this.memMessage);
			this.lc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lc.Location = new System.Drawing.Point(0, 0);
			this.lc.Name = "lc";
			this.lc.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(792, 231, 569, 350);
			this.lc.Root = this.lcGroupBase;
			this.lc.Size = new System.Drawing.Size(598, 368);
			this.lc.TabIndex = 5;
			this.lc.Text = "xLayout1";
			// 
			// memStackTrace
			// 
			this.memStackTrace.Location = new System.Drawing.Point(17, 17);
			this.memStackTrace.Name = "memStackTrace";
			this.memStackTrace.Size = new System.Drawing.Size(564, 260);
			this.memStackTrace.StyleController = this.lc;
			this.memStackTrace.TabIndex = 5;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
			this.btnOk.Appearance.Options.UseFont = true;
			this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
			this.btnOk.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.btnOk.Location = new System.Drawing.Point(242, 334);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(114, 22);
			this.btnOk.StyleController = this.lc;
			this.btnOk.TabIndex = 0;
			this.btnOk.TabStop = false;
			this.btnOk.Text = "확인";
			// 
			// memMessage
			// 
			this.memMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memMessage.Location = new System.Drawing.Point(17, 17);
			this.memMessage.Name = "memMessage";
			this.memMessage.Size = new System.Drawing.Size(564, 260);
			this.memMessage.StyleController = this.lc;
			this.memMessage.TabIndex = 4;
			// 
			// lcGroupBase
			// 
			this.lcGroupBase.BackgroundImage = global::JwDev.Core.Properties.Resources.back_gray;
			this.lcGroupBase.BackgroundImageVisible = true;
			this.lcGroupBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.lcGroupBase.GroupBordersVisible = false;
			this.lcGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.emptySpaceItem4,
            this.emptySpaceItem5,
            this.layoutControlItem2,
            this.lcTabGroup});
			this.lcGroupBase.Location = new System.Drawing.Point(0, 0);
			this.lcGroupBase.Name = "Root";
			this.lcGroupBase.Size = new System.Drawing.Size(598, 368);
			this.lcGroupBase.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 298);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 24);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 24);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(578, 24);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(0, 322);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(230, 26);
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem5
			// 
			this.emptySpaceItem5.AllowHotTrack = false;
			this.emptySpaceItem5.Location = new System.Drawing.Point(348, 322);
			this.emptySpaceItem5.Name = "emptySpaceItem5";
			this.emptySpaceItem5.Size = new System.Drawing.Size(230, 26);
			this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.btnOk;
			this.layoutControlItem2.Location = new System.Drawing.Point(230, 322);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(118, 26);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(118, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(118, 26);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// lcTabGroup
			// 
			this.lcTabGroup.Location = new System.Drawing.Point(0, 0);
			this.lcTabGroup.Name = "lcTabGroup";
			this.lcTabGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.lcTabGroup.SelectedTabPage = this.lcTabGroupMessage;
			this.lcTabGroup.SelectedTabPageIndex = 0;
			this.lcTabGroup.Size = new System.Drawing.Size(578, 298);
			this.lcTabGroup.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcTabGroupMessage,
            this.lcTabGroupStackTrace});
			this.lcTabGroup.TextLocation = DevExpress.Utils.Locations.Bottom;
			// 
			// lcTabGroupMessage
			// 
			this.lcTabGroupMessage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemMessage});
			this.lcTabGroupMessage.Location = new System.Drawing.Point(0, 0);
			this.lcTabGroupMessage.Name = "lcTabGroupMessage";
			this.lcTabGroupMessage.Size = new System.Drawing.Size(568, 264);
			this.lcTabGroupMessage.Text = "메시지";
			// 
			// lcItemMessage
			// 
			this.lcItemMessage.Control = this.memMessage;
			this.lcItemMessage.Location = new System.Drawing.Point(0, 0);
			this.lcItemMessage.Name = "lcItemMessage";
			this.lcItemMessage.Size = new System.Drawing.Size(568, 264);
			this.lcItemMessage.TextSize = new System.Drawing.Size(0, 0);
			this.lcItemMessage.TextVisible = false;
			// 
			// lcTabGroupStackTrace
			// 
			this.lcTabGroupStackTrace.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemStackTrace});
			this.lcTabGroupStackTrace.Location = new System.Drawing.Point(0, 0);
			this.lcTabGroupStackTrace.Name = "lcTabGroupStackTrace";
			this.lcTabGroupStackTrace.Size = new System.Drawing.Size(568, 264);
			this.lcTabGroupStackTrace.Text = "오류위치";
			// 
			// lcItemStackTrace
			// 
			this.lcItemStackTrace.Control = this.memStackTrace;
			this.lcItemStackTrace.Location = new System.Drawing.Point(0, 0);
			this.lcItemStackTrace.Name = "lcItemStackTrace";
			this.lcItemStackTrace.Size = new System.Drawing.Size(568, 264);
			this.lcItemStackTrace.TextSize = new System.Drawing.Size(0, 0);
			this.lcItemStackTrace.TextVisible = false;
			// 
			// ErrorMessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 368);
			this.Controls.Add(this.lc);
			this.LookAndFeel.SkinName = "DevExpress Dark Style";
			this.Name = "ErrorMessageBox";
			this.Text = "ErrorMessageBox";
			((System.ComponentModel.ISupportInitialize)(this.lc)).EndInit();
			this.lc.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memStackTrace.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memMessage.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcGroupBase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroup)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupMessage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemMessage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcTabGroupStackTrace)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lcItemStackTrace)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.MemoEdit memMessage;
        private  DevExpress.XtraEditors.SimpleButton btnOk;
		private DevExpress.XtraLayout.LayoutControl lc;
		private DevExpress.XtraLayout.LayoutControlGroup lcGroupBase;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.TabbedControlGroup lcTabGroup;
		private DevExpress.XtraLayout.LayoutControlGroup lcTabGroupMessage;
		private DevExpress.XtraLayout.LayoutControlItem lcItemMessage;
		private DevExpress.XtraLayout.LayoutControlGroup lcTabGroupStackTrace;
		private DevExpress.XtraEditors.MemoEdit memStackTrace;
		private DevExpress.XtraLayout.LayoutControlItem lcItemStackTrace;
	}
}