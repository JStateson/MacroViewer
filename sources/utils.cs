using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace MacroViewer
{
    public partial class utils : Form
    {
        private string sLoc;
        public utils()
        {
            InitializeComponent();
            sLoc = Utils.WhereExe;
        }

        private void MakeResult()
        {
            string strUrl = Utils.dRef(tbURL.Text);
            string strTmp = tbTEXT.Text;
            if (strTmp == "") strTmp = strUrl;
            tbResult.Text = Utils.FormUrl(strUrl, strTmp);
        }

        private void btnCvt_Click(object sender, EventArgs e)
        {
            MakeResult();
        }



        private void btnClip_Click(object sender, EventArgs e)
        {
            if (tbResult.Text == "") return;
            Clipboard.SetText(tbResult.Text);
        }

        private void btnClrHREF_Click(object sender, EventArgs e)
        {
            tbTEXT.Text = "";
        }

        private void btnClearURL_Click(object sender, EventArgs e)
        {
            tbURL.Text = "";
        }

        private void btnClrScratch_Click(object sender, EventArgs e)
        {
            tbScratch.Text = "";
        }

        private void showExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Samp1 = "Click to see how to find the release date of an hp laptop";
            string Samp2 = "https://www.lifewire.com/how-to-find-the-serial-number-of-an-hp-laptop-5189844#:~:text=You%20can%20determine%20your%20laptop's,week%20of%20the%20year%202020.";
            tbTEXT.Text = Samp1;
            tbURL.Text = Samp2;
            MakeResult();
        }

        private void ShowBrowser(string s)
        {
            if (s == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(s);
        }

        private void btnShowBrowser_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbResult.Text);
        }

        private void btnApplyTab_Click(object sender, EventArgs e)
        {
            int r = Convert.ToInt32(tbRows.Text);
            int c = Convert.ToInt32(tbCols.Text);
            int n = r * c;
            int i;
            tbScratch.Text = Utils.FormTable(r, c, true);
            btnShow.Enabled = (tbScratch.Text != "");
            if(cbPreFill.Checked && (n <= 30))
            {
                dgv.Rows.Clear();
                for(i = 0; i < n; i++)
                {
                    //DataGridViewRow row = new DataGridViewRow();
                    dgv.Rows.Add(Utils.strFillSubscript(r,c,i),"    ");
                }

            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }

        private void cbPreFill_CheckStateChanged(object sender, EventArgs e)
        {
            dgv.Visible = cbPreFill.Checked;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string sBig = tbScratch.Text;
            foreach(DataGridViewRow row in dgv.Rows)
            {
                string s1 = row.Cells[0].Value.ToString();
                string s2 = row.Cells[1].Value.ToString();
                sBig = sBig.Replace(s1, s2);
            }
            tbScratch.Text = sBig;
        }

        private void btnCopyScratch_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbScratch.Text);
        }

        private void btnShowScratch_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }
    }
}