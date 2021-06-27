using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ExcelExporter {
    public partial class ExcelExporterUI : Form {
        public static readonly string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        public string ExcelFullPath { get; private set; } = null;
        public string ExcelListFullPath {
            get {
                return string.Format("{0}/ToExportTableList.json", ExcelFullPath);
            }
        }
        public string ServerOutputFullPath { get; private set; } = null;
        public string ClientOutputFullPath { get; private set; } = null;

        public ExcelExporterUI() {
            this.InitializeComponent();
            this.OnInited();
        }

        private void OnInited() {
            OutPutText.Text = Environment.CurrentDirectory + "\n        " +
                Directory.GetCurrentDirectory() + "\n       " +
                Application.StartupPath + "\n       " +
                Application.ExecutablePath;
        }

        private void SVNRevert_Click(object sender, EventArgs e) {
            SVNProcess.SVNHelper.Revert("");
        }

        private void SVNUpdate_Click(object sender, EventArgs e) {
            SVNProcess.SVNHelper.Update("");
        }

        private void SVNCommit_Click(object sender, EventArgs e) {
            SVNProcess.SVNHelper.Commit("");
        }

        private void BtnExport_Click(object sender, EventArgs e) {

        }

        private void BtnRefreshList_Click(object sender, EventArgs e) {

        }

        private void BtnExcelPath_Click(object sender, EventArgs e) {
            //using (OpenFileDialog ofd = new OpenFileDialog()) {
            //    ofd.Title = "选择文件";
            //    ofd.InitialDirectory = Desktop + "\\combats\\modals";
            //    ofd.Filter = fileFliter;
            //    ofd.AddExtension = true;//自动添加后缀名
            //    ofd.CheckFileExists = true;//如果是用户手写文件名，那么如果不存在需要警告

            //    if (ofd.ShowDialog() == DialogResult.OK)//如果就是说手写的文件名不存在，就会自动退出到（ ofd.ShowDialog() == DialogResult.CANCEL）
            //    {
            //        g_dt = readExcel(ofd.FileName, true);
            //        if (g_dt == null) {
            //            return;
            //        }
            //        showInForm();
            //    }
            //}
        }

        private void FilterText_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
            }
        }

        private void TextExcelPath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
            }
        }

        private void ServerClassOutputPath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
            }
        }

        private void ServerDataOutputPath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
            }
        }

        private void ClientClassOutputPath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
            }
        }

        private void ClientDataOutputPath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
            }
        }

        private void BtnServerClassPath_Click(object sender, EventArgs e) {

        }

        private void BtnServerDataPath_Click(object sender, EventArgs e) {

        }

        private void BtnClientClassPath_Click(object sender, EventArgs e) {

        }

        private void BtnClientDataPath_Click(object sender, EventArgs e) {

        }
    }
}
