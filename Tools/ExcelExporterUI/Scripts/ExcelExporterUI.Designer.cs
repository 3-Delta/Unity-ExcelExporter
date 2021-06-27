
namespace ExcelExporter {
    partial class ExcelExporterUI {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelExporterUI));
            this.CSVListView = new System.Windows.Forms.ListView();
            this.BtnSVNRevert = new System.Windows.Forms.Button();
            this.BtnSVNUpdate = new System.Windows.Forms.Button();
            this.BtnSVNCommit = new System.Windows.Forms.Button();
            this.ListVScrollBar = new System.Windows.Forms.VScrollBar();
            this.OutPutText = new System.Windows.Forms.TextBox();
            this.FilterText = new System.Windows.Forms.TextBox();
            this.LabelSearch = new System.Windows.Forms.Label();
            this.ClientGroup = new System.Windows.Forms.GroupBox();
            this.ClientDataOutputPath = new System.Windows.Forms.TextBox();
            this.BtnClientDataPath = new System.Windows.Forms.Button();
            this.ClientDataIni = new System.Windows.Forms.Label();
            this.ClientDataCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ClientClassOutputPath = new System.Windows.Forms.TextBox();
            this.BtnClientClassPath = new System.Windows.Forms.Button();
            this.ClientClassCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ClientClassIni = new System.Windows.Forms.Label();
            this.ServerGroup = new System.Windows.Forms.GroupBox();
            this.ServerDataOutputPath = new System.Windows.Forms.TextBox();
            this.BtnServerDataPath = new System.Windows.Forms.Button();
            this.ServerDataIni = new System.Windows.Forms.Label();
            this.ServerDataCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ServerClassOutputPath = new System.Windows.Forms.TextBox();
            this.BtnServerClassPath = new System.Windows.Forms.Button();
            this.ServerClassCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ServerClassIni = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BtnRefreshList = new System.Windows.Forms.Button();
            this.BtnExcelPath = new System.Windows.Forms.Button();
            this.TextExcelPath = new System.Windows.Forms.TextBox();
            this.BtnExport = new System.Windows.Forms.Button();
            this.SelectedSheetList = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressText = new System.Windows.Forms.Label();
            this.ClientGroup.SuspendLayout();
            this.ServerGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // CSVListView
            // 
            this.CSVListView.HideSelection = false;
            this.CSVListView.Location = new System.Drawing.Point(5, 74);
            this.CSVListView.Name = "CSVListView";
            this.CSVListView.Size = new System.Drawing.Size(464, 665);
            this.CSVListView.TabIndex = 0;
            this.CSVListView.UseCompatibleStateImageBehavior = false;
            // 
            // BtnSVNRevert
            // 
            this.BtnSVNRevert.Location = new System.Drawing.Point(495, 692);
            this.BtnSVNRevert.Name = "BtnSVNRevert";
            this.BtnSVNRevert.Size = new System.Drawing.Size(90, 30);
            this.BtnSVNRevert.TabIndex = 1;
            this.BtnSVNRevert.Text = "SVNRevert";
            this.BtnSVNRevert.UseVisualStyleBackColor = true;
            this.BtnSVNRevert.Click += new System.EventHandler(this.SVNRevert_Click);
            // 
            // BtnSVNUpdate
            // 
            this.BtnSVNUpdate.Location = new System.Drawing.Point(615, 692);
            this.BtnSVNUpdate.Name = "BtnSVNUpdate";
            this.BtnSVNUpdate.Size = new System.Drawing.Size(90, 30);
            this.BtnSVNUpdate.TabIndex = 2;
            this.BtnSVNUpdate.Text = "SVNUpdate";
            this.BtnSVNUpdate.UseVisualStyleBackColor = true;
            this.BtnSVNUpdate.Click += new System.EventHandler(this.SVNUpdate_Click);
            // 
            // BtnSVNCommit
            // 
            this.BtnSVNCommit.Location = new System.Drawing.Point(733, 692);
            this.BtnSVNCommit.Name = "BtnSVNCommit";
            this.BtnSVNCommit.Size = new System.Drawing.Size(90, 30);
            this.BtnSVNCommit.TabIndex = 3;
            this.BtnSVNCommit.Text = "SVNCommit";
            this.BtnSVNCommit.UseVisualStyleBackColor = true;
            this.BtnSVNCommit.Click += new System.EventHandler(this.SVNCommit_Click);
            // 
            // ListVScrollBar
            // 
            this.ListVScrollBar.Location = new System.Drawing.Point(469, 74);
            this.ListVScrollBar.Name = "ListVScrollBar";
            this.ListVScrollBar.Size = new System.Drawing.Size(17, 665);
            this.ListVScrollBar.TabIndex = 4;
            // 
            // OutPutText
            // 
            this.OutPutText.BackColor = System.Drawing.SystemColors.WindowText;
            this.OutPutText.ForeColor = System.Drawing.Color.Lime;
            this.OutPutText.Location = new System.Drawing.Point(776, 0);
            this.OutPutText.Multiline = true;
            this.OutPutText.Name = "OutPutText";
            this.OutPutText.ReadOnly = true;
            this.OutPutText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutPutText.Size = new System.Drawing.Size(309, 294);
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
            // ClientGroup
            // 
            this.ClientGroup.Controls.Add(this.ClientDataOutputPath);
            this.ClientGroup.Controls.Add(this.BtnClientDataPath);
            this.ClientGroup.Controls.Add(this.ClientDataIni);
            this.ClientGroup.Controls.Add(this.ClientDataCheckedListBox);
            this.ClientGroup.Controls.Add(this.ClientClassOutputPath);
            this.ClientGroup.Controls.Add(this.BtnClientClassPath);
            this.ClientGroup.Controls.Add(this.ClientClassCheckedListBox);
            this.ClientGroup.Controls.Add(this.ClientClassIni);
            this.ClientGroup.Location = new System.Drawing.Point(495, 515);
            this.ClientGroup.Name = "ClientGroup";
            this.ClientGroup.Size = new System.Drawing.Size(578, 154);
            this.ClientGroup.TabIndex = 19;
            this.ClientGroup.TabStop = false;
            this.ClientGroup.Text = "客户端配置";
            // 
            // ClientDataOutputPath
            // 
            this.ClientDataOutputPath.AccessibleDescription = "";
            this.ClientDataOutputPath.Location = new System.Drawing.Point(90, 123);
            this.ClientDataOutputPath.Name = "ClientDataOutputPath";
            this.ClientDataOutputPath.Size = new System.Drawing.Size(482, 23);
            this.ClientDataOutputPath.TabIndex = 13;
            this.ClientDataOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientDataOutputPath_KeyDown);
            // 
            // BtnClientDataPath
            // 
            this.BtnClientDataPath.Location = new System.Drawing.Point(4, 121);
            this.BtnClientDataPath.Name = "BtnClientDataPath";
            this.BtnClientDataPath.Size = new System.Drawing.Size(80, 25);
            this.BtnClientDataPath.TabIndex = 14;
            this.BtnClientDataPath.Text = "路径设置";
            this.BtnClientDataPath.UseVisualStyleBackColor = true;
            this.BtnClientDataPath.Click += new System.EventHandler(this.BtnClientDataPath_Click);
            // 
            // ClientDataIni
            // 
            this.ClientDataIni.AutoSize = true;
            this.ClientDataIni.Location = new System.Drawing.Point(26, 97);
            this.ClientDataIni.Name = "ClientDataIni";
            this.ClientDataIni.Size = new System.Drawing.Size(35, 17);
            this.ClientDataIni.TabIndex = 12;
            this.ClientDataIni.Text = "数据:";
            // 
            // ClientDataCheckedListBox
            // 
            this.ClientDataCheckedListBox.CheckOnClick = true;
            this.ClientDataCheckedListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ClientDataCheckedListBox.FormattingEnabled = true;
            this.ClientDataCheckedListBox.HorizontalScrollbar = true;
            this.ClientDataCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ClientDataCheckedListBox.Items.AddRange(new object[] {
            "Binary",
            "Json",
            "Lua",
            "Sqlite"});
            this.ClientDataCheckedListBox.Location = new System.Drawing.Point(90, 79);
            this.ClientDataCheckedListBox.MultiColumn = true;
            this.ClientDataCheckedListBox.Name = "ClientDataCheckedListBox";
            this.ClientDataCheckedListBox.Size = new System.Drawing.Size(482, 40);
            this.ClientDataCheckedListBox.TabIndex = 11;
            // 
            // ClientClassOutputPath
            // 
            this.ClientClassOutputPath.AccessibleDescription = "";
            this.ClientClassOutputPath.Location = new System.Drawing.Point(90, 48);
            this.ClientClassOutputPath.Name = "ClientClassOutputPath";
            this.ClientClassOutputPath.Size = new System.Drawing.Size(482, 23);
            this.ClientClassOutputPath.TabIndex = 4;
            this.ClientClassOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientClassOutputPath_KeyDown);
            // 
            // BtnClientClassPath
            // 
            this.BtnClientClassPath.Location = new System.Drawing.Point(6, 46);
            this.BtnClientClassPath.Name = "BtnClientClassPath";
            this.BtnClientClassPath.Size = new System.Drawing.Size(80, 25);
            this.BtnClientClassPath.TabIndex = 10;
            this.BtnClientClassPath.Text = "路径设置";
            this.BtnClientClassPath.UseVisualStyleBackColor = true;
            this.BtnClientClassPath.Click += new System.EventHandler(this.BtnClientClassPath_Click);
            // 
            // ClientClassCheckedListBox
            // 
            this.ClientClassCheckedListBox.CheckOnClick = true;
            this.ClientClassCheckedListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ClientClassCheckedListBox.FormattingEnabled = true;
            this.ClientClassCheckedListBox.HorizontalScrollbar = true;
            this.ClientClassCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ClientClassCheckedListBox.Items.AddRange(new object[] {
            "C#"});
            this.ClientClassCheckedListBox.Location = new System.Drawing.Point(90, 22);
            this.ClientClassCheckedListBox.MultiColumn = true;
            this.ClientClassCheckedListBox.Name = "ClientClassCheckedListBox";
            this.ClientClassCheckedListBox.Size = new System.Drawing.Size(482, 22);
            this.ClientClassCheckedListBox.TabIndex = 0;
            // 
            // ClientClassIni
            // 
            this.ClientClassIni.AutoSize = true;
            this.ClientClassIni.Location = new System.Drawing.Point(33, 22);
            this.ClientClassIni.Name = "ClientClassIni";
            this.ClientClassIni.Size = new System.Drawing.Size(23, 17);
            this.ClientClassIni.TabIndex = 2;
            this.ClientClassIni.Text = "类:";
            // 
            // ServerGroup
            // 
            this.ServerGroup.Controls.Add(this.ServerDataOutputPath);
            this.ServerGroup.Controls.Add(this.BtnServerDataPath);
            this.ServerGroup.Controls.Add(this.ServerDataIni);
            this.ServerGroup.Controls.Add(this.ServerDataCheckedListBox);
            this.ServerGroup.Controls.Add(this.ServerClassOutputPath);
            this.ServerGroup.Controls.Add(this.BtnServerClassPath);
            this.ServerGroup.Controls.Add(this.ServerClassCheckedListBox);
            this.ServerGroup.Controls.Add(this.ServerClassIni);
            this.ServerGroup.Location = new System.Drawing.Point(493, 353);
            this.ServerGroup.Name = "ServerGroup";
            this.ServerGroup.Size = new System.Drawing.Size(578, 154);
            this.ServerGroup.TabIndex = 18;
            this.ServerGroup.TabStop = false;
            this.ServerGroup.Text = "服务器配置";
            // 
            // ServerDataOutputPath
            // 
            this.ServerDataOutputPath.AccessibleDescription = "";
            this.ServerDataOutputPath.Location = new System.Drawing.Point(90, 123);
            this.ServerDataOutputPath.Name = "ServerDataOutputPath";
            this.ServerDataOutputPath.Size = new System.Drawing.Size(482, 23);
            this.ServerDataOutputPath.TabIndex = 13;
            this.ServerDataOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServerDataOutputPath_KeyDown);
            // 
            // BtnServerDataPath
            // 
            this.BtnServerDataPath.Location = new System.Drawing.Point(4, 121);
            this.BtnServerDataPath.Name = "BtnServerDataPath";
            this.BtnServerDataPath.Size = new System.Drawing.Size(80, 25);
            this.BtnServerDataPath.TabIndex = 14;
            this.BtnServerDataPath.Text = "路径设置";
            this.BtnServerDataPath.UseVisualStyleBackColor = true;
            this.BtnServerDataPath.Click += new System.EventHandler(this.BtnServerDataPath_Click);
            // 
            // ServerDataIni
            // 
            this.ServerDataIni.AutoSize = true;
            this.ServerDataIni.Location = new System.Drawing.Point(26, 97);
            this.ServerDataIni.Name = "ServerDataIni";
            this.ServerDataIni.Size = new System.Drawing.Size(35, 17);
            this.ServerDataIni.TabIndex = 12;
            this.ServerDataIni.Text = "数据:";
            // 
            // ServerDataCheckedListBox
            // 
            this.ServerDataCheckedListBox.CheckOnClick = true;
            this.ServerDataCheckedListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServerDataCheckedListBox.FormattingEnabled = true;
            this.ServerDataCheckedListBox.HorizontalScrollbar = true;
            this.ServerDataCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ServerDataCheckedListBox.Items.AddRange(new object[] {
            "Binary",
            "Json"});
            this.ServerDataCheckedListBox.Location = new System.Drawing.Point(90, 96);
            this.ServerDataCheckedListBox.MultiColumn = true;
            this.ServerDataCheckedListBox.Name = "ServerDataCheckedListBox";
            this.ServerDataCheckedListBox.Size = new System.Drawing.Size(482, 22);
            this.ServerDataCheckedListBox.TabIndex = 11;
            // 
            // ServerClassOutputPath
            // 
            this.ServerClassOutputPath.AccessibleDescription = "";
            this.ServerClassOutputPath.Location = new System.Drawing.Point(90, 48);
            this.ServerClassOutputPath.Name = "ServerClassOutputPath";
            this.ServerClassOutputPath.Size = new System.Drawing.Size(482, 23);
            this.ServerClassOutputPath.TabIndex = 4;
            this.ServerClassOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServerClassOutputPath_KeyDown);
            // 
            // BtnServerClassPath
            // 
            this.BtnServerClassPath.Location = new System.Drawing.Point(6, 46);
            this.BtnServerClassPath.Name = "BtnServerClassPath";
            this.BtnServerClassPath.Size = new System.Drawing.Size(80, 25);
            this.BtnServerClassPath.TabIndex = 10;
            this.BtnServerClassPath.Text = "路径设置";
            this.BtnServerClassPath.UseVisualStyleBackColor = true;
            this.BtnServerClassPath.Click += new System.EventHandler(this.BtnServerClassPath_Click);
            // 
            // ServerClassCheckedListBox
            // 
            this.ServerClassCheckedListBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ServerClassCheckedListBox.CheckOnClick = true;
            this.ServerClassCheckedListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServerClassCheckedListBox.FormattingEnabled = true;
            this.ServerClassCheckedListBox.HorizontalScrollbar = true;
            this.ServerClassCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ServerClassCheckedListBox.Items.AddRange(new object[] {
            "C++"});
            this.ServerClassCheckedListBox.Location = new System.Drawing.Point(90, 22);
            this.ServerClassCheckedListBox.MultiColumn = true;
            this.ServerClassCheckedListBox.Name = "ServerClassCheckedListBox";
            this.ServerClassCheckedListBox.Size = new System.Drawing.Size(482, 22);
            this.ServerClassCheckedListBox.TabIndex = 0;
            // 
            // ServerClassIni
            // 
            this.ServerClassIni.AutoSize = true;
            this.ServerClassIni.Location = new System.Drawing.Point(33, 22);
            this.ServerClassIni.Name = "ServerClassIni";
            this.ServerClassIni.Size = new System.Drawing.Size(23, 17);
            this.ServerClassIni.TabIndex = 2;
            this.ServerClassIni.Text = "类:";
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
            this.BtnExcelPath.Location = new System.Drawing.Point(388, 43);
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
            this.TextExcelPath.Location = new System.Drawing.Point(12, 45);
            this.TextExcelPath.Name = "TextExcelPath";
            this.TextExcelPath.Size = new System.Drawing.Size(370, 23);
            this.TextExcelPath.TabIndex = 12;
            this.TextExcelPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextExcelPath_KeyDown);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.Lime;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BtnExport.ForeColor = System.Drawing.Color.Red;
            this.BtnExport.Location = new System.Drawing.Point(937, 682);
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
            this.SelectedSheetList.Size = new System.Drawing.Size(275, 294);
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
            // ExcelExporterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 744);
            this.Controls.Add(this.ClientGroup);
            this.Controls.Add(this.ServerGroup);
            this.Controls.Add(this.ProgressText);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SelectedSheetList);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.BtnExcelPath);
            this.Controls.Add(this.TextExcelPath);
            this.Controls.Add(this.BtnRefreshList);
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
            this.Name = "ExcelExporterUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "导表工具";
            this.ClientGroup.ResumeLayout(false);
            this.ClientGroup.PerformLayout();
            this.ServerGroup.ResumeLayout(false);
            this.ServerGroup.PerformLayout();
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
        private System.Windows.Forms.CheckedListBox ServerClassCheckedListBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label ServerClassIni;
        private System.Windows.Forms.Button BtnServerClassPath;
        private System.Windows.Forms.TextBox ServerClassOutputPath;
        private System.Windows.Forms.Button BtnRefreshList;
        private System.Windows.Forms.Button BtnExcelPath;
        private System.Windows.Forms.TextBox TextExcelPath;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.TextBox SelectedSheetList;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressText;
        private System.Windows.Forms.GroupBox ServerGroup;
        private System.Windows.Forms.CheckedListBox ServerDataCheckedListBox;
        private System.Windows.Forms.TextBox ServerDataOutputPath;
        private System.Windows.Forms.Button BtnServerDataPath;
        private System.Windows.Forms.Label ServerDataIni;
        private System.Windows.Forms.GroupBox ClientGroup;
        private System.Windows.Forms.TextBox ClientDataOutputPath;
        private System.Windows.Forms.Button BtnClientDataPath;
        private System.Windows.Forms.Label ClientDataIni;
        private System.Windows.Forms.CheckedListBox ClientDataCheckedListBox;
        private System.Windows.Forms.TextBox ClientClassOutputPath;
        private System.Windows.Forms.Button BtnClientClassPath;
        private System.Windows.Forms.CheckedListBox ClientClassCheckedListBox;
        private System.Windows.Forms.Label ClientClassIni;
    }
}

