using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroViewer
{
    public partial class utils : Form
    {


        public utils()
        {
            InitializeComponent();
        }

        private void MakeResult()
        {
            string strUrl = tbURL.Text;
            string strTmp = tbTEXT.Text;
            if (strTmp == "") strTmp = tbURL.Text;
            tbResult.Text = "<a href=\"" + strUrl + "\" target=\"_blank\">" + strTmp + "</a>";
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

        private void btnShowBrowser_Click(object sender, EventArgs e)
        {
            string strTemp = tbResult.Text;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }
    }
}