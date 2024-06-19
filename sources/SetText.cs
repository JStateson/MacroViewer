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
        private string strBoxed = "";
        private int CurrentDemo = 0, TotalDemos = 4;
        //0: box url, 1: list, 2:image url, 3: image
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

        private void SetNextDemo()
        {
            CurrentDemo++;
            if (CurrentDemo == TotalDemos) CurrentDemo = 0;
            btnDemo.Text = "Click for demo" + (CurrentDemo + 1).ToString();
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
                if (cbMakeIMG.Checked)
                {
                    strOut += Utils.AssembleIMG(tbRawUrl.Text);
                }
                else
                {
                    strOut += Utils.FormUrl(cbCleanUrl.Checked ? Utils.dRef(tbRawUrl.Text) : tbRawUrl.Text, tbSelectedItem.Text.Trim());

                }
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

        private int WhichDemo()
        {
            int i = 0;
            string[] sDemos = {"1", "2", "3", "4" };
            for(i = 0; i < sDemos.Length; i++)
            {
                if(btnDemo.Text.Contains(sDemos[i]))
                {
                    break;
                }
            }
            SetNextDemo();
            return i;
        }


        private void RunDemo()
        {
            int i = WhichDemo();
            switch (i)
            {
                case 0:
                    tbPrefix.Text = "You can ";
                    tbSuffix.Text = " to visit google";
                    tbSelectedItem.Text = "CLICK HERE";
                    tbRawUrl.Text = "https://www.google.com";
                    rbFitBox.Checked = true;
                    cbMakeIMG.Checked = false;
                break;
                case 1:
                    tbPrefix.Text = "";
                    tbSuffix.Text = "";
                    tbSelectedItem.Text = "Some country codes:\r\n#A2M - Iceland - English\r\n#A2N - Saudi Arabia - Arabic'English";
                    tbRawUrl.Text = "";
                    rbSqueeze.Checked = true;
                    cbMakeIMG.Checked = false;
                break;
                case 2:
                    tbPrefix.Text = "You can ";
                    tbSuffix.Text = " to see what to do with old Dell systems";
                    tbSelectedItem.Text = "Click Here";
                    tbRawUrl.Text = "https://h30434.www3.hp.com/t5/image/serverpage/image-id/368919i27FD57F55C2EC433";
                    //tbRawUrl.Text = "https://h30434.www3.hp.com/t5/image/serverpage/image-id/372002iFA9364C9FF20F330";
                    rbSqueeze.Checked = true;
                    cbMakeIMG.Checked = false;
                break;
                case 3:
                    tbPrefix.Text = "This is what linux thinks of Windows XP<br>";
                    tbSuffix.Text = "<br><br>Yes, Linux does suck!";
                    tbSelectedItem.Text = "";
                    //tbRawUrl.Text = "https://h30434.www3.hp.com/t5/image/serverpage/image-id/368919i27FD57F55C2EC433";
                    tbRawUrl.Text = "https://h30434.www3.hp.com/t5/image/serverpage/image-id/372002iFA9364C9FF20F330";
                    rbNoBox.Checked = true;
                    cbMakeIMG.Checked = true;
                    break;
            }

        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            RunDemo();
            FormObject();
        }

        private void btnClrDemo_Click(object sender, EventArgs e)
        {
            tbPrefix.Text = "";
            tbSuffix.Text = "";
            tbSelectedItem.Text = "";
            tbRawUrl.Text = "";
            cbMakeIMG.Checked = false;
        }
    }
}
