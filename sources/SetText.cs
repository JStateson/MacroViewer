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
            strResultOut = bBoxed ? strBoxed : tbResult.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }

        private string FormObject()
        {
            string strOut = "";
            string strTmp = "";
            string strUC = tbRawUrl.Text.ToUpper();
            if (strUC == "") return "";
            bool bHaveHTML = strUC.Contains("HTTPS:") || strUC.Contains("HTTP:");
            if (!bHaveHTML) return "";
            strTmp = tbPrefix.Text.Trim();
            if (strTmp != "")
            {
                strOut = strTmp + " ";
            }
            strOut += Utils.FormUrl(cbCleanUrl.Checked ? Utils.dRef(tbRawUrl.Text) : tbRawUrl.Text,
                tbSelectedItem.Text.Trim());
            strTmp = tbSuffix.Text.Trim();
            if (strTmp != "")
            {
                strOut += " " + strTmp;
            }
            strResultOut = strOut.Trim();
            tbResult.Text = strResultOut;
            return strResultOut;
        }

        private void btnApplyText_Click(object sender, EventArgs e)
        {
            FormObject();
            StopTimer();
        }

        private void RunBrowser()
        {
            string strTemp = tbResult.Text;
            if (strTemp == "" || bBoxed) strTemp = strBoxed;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp, true);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            RunBrowser();
        }

        private void TimerControl(bool bEnable)
        {
            bBoxed = bEnable;
            bBlinking = bEnable;
            BlinkTimer.Enabled = bEnable;
            if (bEnable) btnBoxIT.Text = "Remove from box";
            else
            {
                btnBoxIT.Text = "Put in box";
                strBoxed = "";
            }
        }

        private void StopTimer()
        {
            BlinkTimer.Enabled = false;
            Application.DoEvents();
            bBlinking = false;
            bBoxed = false;
            btnBoxIT.Text = "Put in box";
            strBoxed = "";
        }

        private void StartTimer()
        {
            bBlinking = true;
            bBoxed = true;
            btnBoxIT.Text = "Remove from box";
            BlinkTimer.Enabled = true;
            Application.DoEvents();
        }


        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            lbBoxed.Visible = bBlinking;
            bBlinking = !bBlinking;
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
            strResultOut = Utils.FormTable(r, c, cbPreFill.Checked, 1);
            this.Close();
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

        private void btnSqueeze_Click(object sender, EventArgs e)
        {
            PutInBox(true);
        }

        private void btnBoxIT_Click(object sender, EventArgs e)
        {
            PutInBox(false);
        }

        private void PutInBox(bool bSqueeze)
        {
            string strUnBoxed = tbResult.Text.Trim();
            if (strUnBoxed == "") strUnBoxed = FormObject();
            if (strUnBoxed == "")
            {
                strUnBoxed = tbSelectedItem.Text.Trim();
            }
            if (strUnBoxed == "") return;
            strBoxed = bSqueeze ? Utils.Form1CellTable(strUnBoxed) : Utils.Form1CellTableP(strUnBoxed); ;
            if (bBoxed) StopTimer();
            else StartTimer();
        }
    }
}
