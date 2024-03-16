using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MacroViewer
{
    public partial class SetText : Form
    {
        private string sLoc;
        private bool bBoxing = false;
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
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            strResultOut = bBoxing ? strBoxed : tbResult.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }



        private void btnApplyText_Click(object sender, EventArgs e)
        {
            string strOut = "";
            string strTmp = "";
            string strUC = tbRawUrl.Text.ToUpper();
            if (strUC == "") return;
            bool bHaveHTML = strUC.Contains("HTTPS:") || strUC.Contains("HTTP:");
            if (!bHaveHTML) return;
            strTmp = tbPrefix.Text.Trim();
            if (strTmp != "")
            {
                strOut = strTmp + " ";
            }
            strOut += Utils.FormUrl(Utils.dRef(tbRawUrl.Text), tbSelectedItem.Text.Trim());
            strTmp = tbSuffix.Text.Trim();
            if(strTmp !="")
            {
                strOut += " " + strTmp;
            }
            strResultOut = strOut;
            tbResult.Text = strResultOut;
    }

        private void RunBrowser(string sLoc)
        {
            string strTemp = tbResult.Text;
            if (strTemp == "") strTemp = strBoxed;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp, true);
        }

        private void btnTest_Click(object sender, EventArgs e) 
        {
            RunBrowser(sLoc); 
        }

        private void TimerControl(bool bEnable)
        {
            bBoxing = bEnable;
            bBlinking = bEnable;
            BlinkTimer.Enabled = bEnable;
            if (bEnable) btnBoxIT.Text = "Remove from box";
            else
            {
                btnBoxIT.Text = "Put in box";
                strBoxed = "";
            }
        }

        private void btnBoxIT_Click(object sender, EventArgs e)
        {
            string strUnBoxed = tbResult.Text.Trim();
            if (strUnBoxed == "")
            {
                strUnBoxed = tbSelectedItem.Text.Trim();
            }
            if (strUnBoxed == "") return;
            strBoxed = Utils.Form1CellTable(strUnBoxed);
            if (bBoxing) TimerControl(false);
            else TimerControl(true);
        }


        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            lbBoxed.Visible = bBoxing;
            bBoxing = !bBoxing;
        }

        private void SetText_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnApplyTab_Click(object sender, EventArgs e)
        {
            int r = Convert.ToInt32(tbRows.Text);
            int c = Convert.ToInt32(tbCols.Text);
            strResultOut = Utils.FormTable(r, c);
            this.Close();
        }
    }
}
