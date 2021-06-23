﻿
namespace ExcelExporter {
    partial class ExcelExporter {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelExporter));
            this.CSVListView = new System.Windows.Forms.ListView();
            this.BtnSVNRevert = new System.Windows.Forms.Button();
            this.BtnSVNUpdate = new System.Windows.Forms.Button();
            this.BtnSVNCommit = new System.Windows.Forms.Button();
            this.ListVScrollBar = new System.Windows.Forms.VScrollBar();
            this.OutPutText = new System.Windows.Forms.TextBox();
            this.FilterText = new System.Windows.Forms.TextBox();
            this.LabelSearch = new System.Windows.Forms.Label();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.BtnClientPath = new System.Windows.Forms.Button();
            this.BtnServerPath = new System.Windows.Forms.Button();
            this.ClientOutputPath = new System.Windows.Forms.TextBox();
            this.ServerOutputPath = new System.Windows.Forms.TextBox();
            this.Client = new System.Windows.Forms.Label();
            this.Server = new System.Windows.Forms.Label();
            this.ClientCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ServerCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BtnRefreshList = new System.Windows.Forms.Button();
            this.BtnExcelPath = new System.Windows.Forms.Button();
            this.TextExcelPath = new System.Windows.Forms.TextBox();
            this.BtnExport = new System.Windows.Forms.Button();
            this.SelectedSheetList = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressText = new System.Windows.Forms.Label();
            this.GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CSVListView
            // 
            this.CSVListView.HideSelection = false;
            this.CSVListView.Location = new System.Drawing.Point(5, 94);
            this.CSVListView.Name = "CSVListView";
            this.CSVListView.Size = new System.Drawing.Size(464, 489);
            this.CSVListView.TabIndex = 0;
            this.CSVListView.UseCompatibleStateImageBehavior = false;
            // 
            // BtnSVNRevert
            // 
            this.BtnSVNRevert.Location = new System.Drawing.Point(511, 536);
            this.BtnSVNRevert.Name = "BtnSVNRevert";
            this.BtnSVNRevert.Size = new System.Drawing.Size(90, 30);
            this.BtnSVNRevert.TabIndex = 1;
            this.BtnSVNRevert.Text = "SVNRevert";
            this.BtnSVNRevert.UseVisualStyleBackColor = true;
            this.BtnSVNRevert.Click += new System.EventHandler(this.SVNRevert_Click);
            // 
            // BtnSVNUpdate
            // 
            this.BtnSVNUpdate.Location = new System.Drawing.Point(623, 536);
            this.BtnSVNUpdate.Name = "BtnSVNUpdate";
            this.BtnSVNUpdate.Size = new System.Drawing.Size(90, 30);
            this.BtnSVNUpdate.TabIndex = 2;
            this.BtnSVNUpdate.Text = "SVNUpdate";
            this.BtnSVNUpdate.UseVisualStyleBackColor = true;
            this.BtnSVNUpdate.Click += new System.EventHandler(this.SVNUpdate_Click);
            // 
            // BtnSVNCommit
            // 
            this.BtnSVNCommit.Location = new System.Drawing.Point(733, 536);
            this.BtnSVNCommit.Name = "BtnSVNCommit";
            this.BtnSVNCommit.Size = new System.Drawing.Size(90, 30);
            this.BtnSVNCommit.TabIndex = 3;
            this.BtnSVNCommit.Text = "SVNCommit";
            this.BtnSVNCommit.UseVisualStyleBackColor = true;
            this.BtnSVNCommit.Click += new System.EventHandler(this.SVNCommit_Click);
            // 
            // ListVScrollBar
            // 
            this.ListVScrollBar.Location = new System.Drawing.Point(466, 94);
            this.ListVScrollBar.Name = "ListVScrollBar";
            this.ListVScrollBar.Size = new System.Drawing.Size(17, 489);
            this.ListVScrollBar.TabIndex = 4;
            // 
            // OutPutText
            // 
            this.OutPutText.BackColor = System.Drawing.SystemColors.WindowText;
            this.OutPutText.ForeColor = System.Drawing.Color.Lime;
            this.OutPutText.Location = new System.Drawing.Point(733, 0);
            this.OutPutText.Multiline = true;
            this.OutPutText.Name = "OutPutText";
            this.OutPutText.ReadOnly = true;
            this.OutPutText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutPutText.Size = new System.Drawing.Size(352, 294);
            this.OutPutText.TabIndex = 5;
            // 
            // FilterText
            // 
            this.FilterText.Location = new System.Drawing.Point(54, 12);
            this.FilterText.Name = "FilterText";
            this.FilterText.Size = new System.Drawing.Size(328, 23);
            this.FilterText.TabIndex = 7;
            this.FilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterText_KeyDown);
            // 
            // LabelSearch
            // 
            this.LabelSearch.AutoSize = true;
            this.LabelSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelSearch.Location = new System.Drawing.Point(5, 13);
            this.LabelSearch.Name = "LabelSearch";
            this.LabelSearch.Size = new System.Drawing.Size(43, 19);
            this.LabelSearch.TabIndex = 8;
            this.LabelSearch.Text = "搜索:";
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.BtnClientPath);
            this.GroupBox.Controls.Add(this.BtnServerPath);
            this.GroupBox.Controls.Add(this.ClientOutputPath);
            this.GroupBox.Controls.Add(this.ServerOutputPath);
            this.GroupBox.Controls.Add(this.Client);
            this.GroupBox.Controls.Add(this.Server);
            this.GroupBox.Controls.Add(this.ClientCheckedListBox);
            this.GroupBox.Controls.Add(this.ServerCheckedListBox);
            this.GroupBox.Location = new System.Drawing.Point(489, 329);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(584, 193);
            this.GroupBox.TabIndex = 9;
            this.GroupBox.TabStop = false;
            // 
            // BtnClientPath
            // 
            this.BtnClientPath.Location = new System.Drawing.Point(6, 153);
            this.BtnClientPath.Name = "BtnClientPath";
            this.BtnClientPath.Size = new System.Drawing.Size(80, 25);
            this.BtnClientPath.TabIndex = 11;
            this.BtnClientPath.Text = "路径设置";
            this.BtnClientPath.UseVisualStyleBackColor = true;
            this.BtnClientPath.Click += new System.EventHandler(this.BtnClientPath_Click);
            // 
            // BtnServerPath
            // 
            this.BtnServerPath.Location = new System.Drawing.Point(6, 69);
            this.BtnServerPath.Name = "BtnServerPath";
            this.BtnServerPath.Size = new System.Drawing.Size(80, 25);
            this.BtnServerPath.TabIndex = 10;
            this.BtnServerPath.Text = "路径设置";
            this.BtnServerPath.UseVisualStyleBackColor = true;
            this.BtnServerPath.Click += new System.EventHandler(this.BtnServerPath_Click);
            // 
            // ClientOutputPath
            // 
            this.ClientOutputPath.Location = new System.Drawing.Point(96, 153);
            this.ClientOutputPath.Name = "ClientOutputPath";
            this.ClientOutputPath.Size = new System.Drawing.Size(482, 23);
            this.ClientOutputPath.TabIndex = 5;
            this.ClientOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientOutputPath_KeyDown);
            // 
            // ServerOutputPath
            // 
            this.ServerOutputPath.AccessibleDescription = "";
            this.ServerOutputPath.Location = new System.Drawing.Point(96, 69);
            this.ServerOutputPath.Name = "ServerOutputPath";
            this.ServerOutputPath.Size = new System.Drawing.Size(482, 23);
            this.ServerOutputPath.TabIndex = 4;
            this.ServerOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServerOutputPath_KeyDown);
            // 
            // Client
            // 
            this.Client.AutoSize = true;
            this.Client.Location = new System.Drawing.Point(22, 119);
            this.Client.Name = "Client";
            this.Client.Size = new System.Drawing.Size(47, 17);
            this.Client.TabIndex = 3;
            this.Client.Text = "客户端:";
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(22, 35);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(47, 17);
            this.Server.TabIndex = 2;
            this.Server.Text = "服务器:";
            // 
            // ClientCheckedListBox
            // 
            this.ClientCheckedListBox.CheckOnClick = true;
            this.ClientCheckedListBox.FormattingEnabled = true;
            this.ClientCheckedListBox.HorizontalScrollbar = true;
            this.ClientCheckedListBox.Items.AddRange(new object[] {
            "Lua",
            "Json",
            "Sqlite",
            "Bytes",
            "C#",
            "C++"});
            this.ClientCheckedListBox.Location = new System.Drawing.Point(96, 107);
            this.ClientCheckedListBox.MultiColumn = true;
            this.ClientCheckedListBox.Name = "ClientCheckedListBox";
            this.ClientCheckedListBox.Size = new System.Drawing.Size(482, 40);
            this.ClientCheckedListBox.TabIndex = 1;
            // 
            // ServerCheckedListBox
            // 
            this.ServerCheckedListBox.CheckOnClick = true;
            this.ServerCheckedListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServerCheckedListBox.FormattingEnabled = true;
            this.ServerCheckedListBox.HorizontalScrollbar = true;
            this.ServerCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ServerCheckedListBox.Items.AddRange(new object[] {
            "Lua",
            "Json",
            "Sqlite",
            "Bytes",
            "C#",
            "C++"});
            this.ServerCheckedListBox.Location = new System.Drawing.Point(96, 22);
            this.ServerCheckedListBox.MultiColumn = true;
            this.ServerCheckedListBox.Name = "ServerCheckedListBox";
            this.ServerCheckedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ServerCheckedListBox.Size = new System.Drawing.Size(482, 40);
            this.ServerCheckedListBox.TabIndex = 0;
            // 
            // BtnRefreshList
            // 
            this.BtnRefreshList.Location = new System.Drawing.Point(399, 12);
            this.BtnRefreshList.Name = "BtnRefreshList";
            this.BtnRefreshList.Size = new System.Drawing.Size(70, 25);
            this.BtnRefreshList.TabIndex = 12;
            this.BtnRefreshList.Text = "刷新列表";
            this.BtnRefreshList.UseVisualStyleBackColor = true;
            this.BtnRefreshList.Click += new System.EventHandler(this.BtnRefreshList_Click);
            // 
            // BtnExcelPath
            // 
            this.BtnExcelPath.Location = new System.Drawing.Point(388, 49);
            this.BtnExcelPath.Name = "BtnExcelPath";
            this.BtnExcelPath.Size = new System.Drawing.Size(81, 25);
            this.BtnExcelPath.TabIndex = 13;
            this.BtnExcelPath.Text = "Excel路径";
            this.BtnExcelPath.UseVisualStyleBackColor = true;
            this.BtnExcelPath.Click += new System.EventHandler(this.BtnExcelPath_Click);
            // 
            // TextExcelPath
            // 
            this.TextExcelPath.AccessibleDescription = "";
            this.TextExcelPath.Location = new System.Drawing.Point(12, 51);
            this.TextExcelPath.Name = "TextExcelPath";
            this.TextExcelPath.Size = new System.Drawing.Size(370, 23);
            this.TextExcelPath.TabIndex = 12;
            this.TextExcelPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextExcelPath_KeyDown);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.Black;
            this.BtnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExport.BackgroundImage")));
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BtnExport.Location = new System.Drawing.Point(926, 528);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(125, 55);
            this.BtnExport.TabIndex = 14;
            this.BtnExport.Text = "导出";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // SelectedSheetList
            // 
            this.SelectedSheetList.BackColor = System.Drawing.SystemColors.WindowText;
            this.SelectedSheetList.ForeColor = System.Drawing.Color.Lime;
            this.SelectedSheetList.Location = new System.Drawing.Point(495, 0);
            this.SelectedSheetList.Multiline = true;
            this.SelectedSheetList.Name = "SelectedSheetList";
            this.SelectedSheetList.ReadOnly = true;
            this.SelectedSheetList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SelectedSheetList.Size = new System.Drawing.Size(232, 294);
            this.SelectedSheetList.TabIndex = 15;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(495, 304);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(572, 22);
            this.ProgressBar.TabIndex = 16;
            // 
            // ProgressText
            // 
            this.ProgressText.AutoSize = true;
            this.ProgressText.Location = new System.Drawing.Point(997, 307);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.Size = new System.Drawing.Size(0, 17);
            this.ProgressText.TabIndex = 17;
            this.ProgressText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ExcelExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 585);
            this.Controls.Add(this.ProgressText);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SelectedSheetList);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.BtnExcelPath);
            this.Controls.Add(this.TextExcelPath);
            this.Controls.Add(this.BtnRefreshList);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.LabelSearch);
            this.Controls.Add(this.FilterText);
            this.Controls.Add(this.OutPutText);
            this.Controls.Add(this.ListVScrollBar);
            this.Controls.Add(this.BtnSVNCommit);
            this.Controls.Add(this.BtnSVNUpdate);
            this.Controls.Add(this.BtnSVNRevert);
            this.Controls.Add(this.CSVListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ExcelExporter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "导表工具";
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView CSVListView;
        private System.Windows.Forms.Button BtnSVNRevert;
        private System.Windows.Forms.Button BtnSVNUpdate;
        private System.Windows.Forms.Button BtnSVNCommit;
        private System.Windows.Forms.VScrollBar ListVScrollBar;
        private System.Windows.Forms.TextBox OutPutText;
        private System.Windows.Forms.TextBox FilterText;
        private System.Windows.Forms.Label LabelSearch;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.CheckedListBox ServerCheckedListBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label Server;
        private System.Windows.Forms.CheckedListBox ClientCheckedListBox;
        private System.Windows.Forms.Label Client;
        private System.Windows.Forms.Button BtnServerPath;
        private System.Windows.Forms.TextBox ClientOutputPath;
        private System.Windows.Forms.TextBox ServerOutputPath;
        private System.Windows.Forms.Button BtnClientPath;
        private System.Windows.Forms.Button BtnRefreshList;
        private System.Windows.Forms.Button BtnExcelPath;
        private System.Windows.Forms.TextBox TextExcelPath;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.TextBox SelectedSheetList;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressText;
    }
}

