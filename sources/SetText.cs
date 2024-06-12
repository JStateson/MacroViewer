using HtmlAgilityPack;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MacroViewer
{
    public partial class SetText : Form
    {
        private string sLoc;
        private bool bBoxed = false;
        private bool bBlinking = false;
        string strBoxed = "";
        public string strResultOut { get; set; }
        public SetText(string strIn)
        {
            InitializeComponent();
            tbPrefix.Text = "";
            tbSuffix.Text = "";
            tbSelectedItem.Text = strIn;
            tbResult.Text = "";
            tbRawUrl.Text = "";
            gpTable.Enabled = (strIn == "");
            sLoc = Utils.WhereExe;
            cbPreFill.Checked = Properties.Settings.Default.FillAlpha;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            strResultOut = tbResult.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }

        private void FormObject()
        {
            string strOut = "";
            string strTmp = "";
            string strUC = tbRawUrl.Text.ToUpper();
            bool bHaveHTML = strUC.Contains("HTTPS:") || strUC.Contains("HTTP:");
            string strResult = "";
            tbResult.Text = "";

            strTmp = tbPrefix.Text.Trim();
            if (strTmp != "")
            {
                strOut = strTmp + " ";
            }
            if ((bHaveHTML))
            {
                strOut += Utils.FormUrl(cbCleanUrl.Checked ? Utils.dRef(tbRawUrl.Text) : tbRawUrl.Text, tbSelectedItem.Text.Trim());
                strTmp = tbSuffix.Text.Trim();
                if (strTmp != "")
                {
                    strOut += " " + strTmp;
                }
            }
            else strOut = tbSelectedItem.Text.Trim();

            strResult = strOut.Trim();
            if(rbNoBox.Checked) tbResult.Text = strResult;
            else
            {
                tbResult.Text = rbSqueeze.Checked ? Utils.Form1CellTable(strResult) : Utils.Form1CellTableP(strResult);
            }
        }

        private void btnApplyText_Click(object sender, EventArgs e)
        {
            FormObject();
        }

        private void RunBrowser()
        {
            string strTemp = tbResult.Text;
            if (strTemp == "" || bBoxed) strTemp = strBoxed;
            if (strTemp == "") return;
            Utils.ShowPageInBrowser("", strTemp);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            RunBrowser();
        }


        private void btnApplyTab_Click(object sender, EventArgs e)
        {
            int r, c;
            try
            {
                r = Convert.ToInt32(tbRows.Text);
                c = Convert.ToInt32(tbCols.Text);
            }
            catch
            {
                tbRows.Text = "1";
                tbCols.Text = "1";
                return;
            }
            tbResult.Text = Utils.FormTable(r, c, cbPreFill.Checked, 1);
        }

        private void SetText_Shown(object sender, EventArgs e)
        {
            tbRawUrl.Focus();
        }

        private void SetText_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FillAlpha = cbPreFill.Checked;
            Properties.Settings.Default.Save();
        }


        private void PutInBox(bool bSqueeze)
        {
            string strUnBoxed = tbResult.Text.Trim();
            if (strUnBoxed == "") return;
            strBoxed = rbSqueeze.Checked ? Utils.Form1CellTable(strUnBoxed) : Utils.Form1CellTableP(strUnBoxed);
            tbResult.Text = strBoxed;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbResult.Text = "";
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            tbPrefix.Text = "You can ";
            tbSuffix.Text = " to visit google";
            tbSelectedItem.Text = "CLICK HERE";
            tbRawUrl.Text = "https://www.google.com";
            rbFitBox.Checked = true;
            FormObject();
        }

        private void btnClrDemo_Click(object sender, EventArgs e)
        {
            tbPrefix.Text = "";
            tbSuffix.Text = "";
            tbSelectedItem.Text = "";
            tbRawUrl.Text = "";
        }
    }
}
