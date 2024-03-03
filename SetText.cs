using System;
using System.Windows.Forms;

namespace MacroViewer
{
    public partial class SetText : Form
    {
        public string strResultOut { get; set; }
        public SetText(string strIn)
        {
            InitializeComponent();
            tbPrefix.Text = "";
            tbSuffix.Text = "";
            tbSelectedItem.Text = strIn;
            tbResult.Text = "";
            tbRawUrl.Text = "";
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

        private string FormUrl(string strUrl,  string strIn)
        {
            if (strIn == "") strIn = strUrl;
            return  "<a href=\"" + strUrl + "\" target=\"_blank\">" + strIn + "</a>";
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
            strOut += FormUrl(tbRawUrl.Text.Trim(), tbSelectedItem.Text.Trim());
            strTmp = tbSuffix.Text.Trim();
            if(strTmp !="")
            {
                strOut += " " + strTmp;
            }
            strResultOut = strOut;
            tbResult.Text = strResultOut;
    }

        private void RunBrowser()
        {
            string strTemp = tbResult.Text;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            RunBrowser(); 
        }
    }
}
