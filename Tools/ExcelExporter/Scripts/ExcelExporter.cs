using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelExporter {
    public partial class ExcelExporter : Form {
        public ExcelExporter() {
            InitializeComponent();
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

        private void BtnRefreshList_Click(object sender, EventArgs e) {

        }

        private void BtnServerPath_Click(object sender, EventArgs e) {

        }

        private void BtnClientPath_Click(object sender, EventArgs e) {

        }

        private void FilterText_KeyDown(object sender, KeyEventArgs e) {

        }

        private void ServerOutputPath_KeyDown(object sender, KeyEventArgs e) {

        }

        private void ClientOutputPath_KeyDown(object sender, KeyEventArgs e) {

        }

        private void TextExcelPath_KeyDown(object sender, KeyEventArgs e) {

        }

        private void BtnExcelPath_Click(object sender, EventArgs e) {

        }
    }
}
