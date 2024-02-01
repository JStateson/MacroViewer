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

        private void btnCvt_Click(object sender, EventArgs e)
        {
            string strUrl = tbURL.Text;
            string strTmp = tbTEXT.Text;
            if(strTmp == "")strTmp = tbURL.Text;
            tbResult.Text = "<a href=\"" + strUrl + "\" target=\"_blank\">" + strTmp + "</a>";
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
    }
}
