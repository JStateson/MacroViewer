using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace MacroViewer
{
    public partial class utils : Form
    {
        private string sLoc;
        private int RowsExpected = 0;
        private int ColsExpected = 0;
        private int nGathered = 0;
        private string[] sEach;
        private List<string[]> sMyTable = new List<string[]>();
        public utils()
        {
            InitializeComponent();
            sLoc = Utils.WhereExe;
        }

        private void EnableRC()
        {
            RowsExpected = 0;
            ColsExpected = 0;
            nGathered = 0;
            sMyTable = new List<string[]>();
            btnFillRow.Enabled = true;
            btnFillCol.Enabled = true;
            tbScratch.Text = "";
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
            EnableRC();
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
            if (dgv.Rows.Count == 0) return;
            string sBig = tbScratch.Text;
            foreach (DataGridViewRow row in dgv.Rows)
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

        private void btnFillRow_Click(object sender, EventArgs e)
        {
            btnFillCol.Enabled = false;
            nGathered = GatherClipboardText();
            if (ColsExpected == 0) ColsExpected = nGathered;
            FillTable();
        }

        private void btnFillCol_Click(object sender, EventArgs e)
        {
            btnFillRow.Enabled = false;
            nGathered = GatherClipboardText();
            if(RowsExpected == 0) RowsExpected = nGathered;
            FillTable();
        }

        private void AddTable(string[] sOne)
        {
            int i = 0, n = RowsExpected;
            if (n == 0) n = ColsExpected;
            if (n != nGathered)
            {
                MessageBox.Show("wrong number of items for row or column");
                return;
            }
            string[] sNew = new string[n];
            foreach (string s in sOne)
            {
                sNew[i] = s.Trim();
                i++;
            }
            sMyTable.Add(sNew);
            FormTable();
        }


        private void FillTable()
        {
            if (cbUseDelims.Checked)
            { 
                string[] sFirst = sEach[0].Trim().Split(new char[] { ' ', '\t', ',' },
                        StringSplitOptions.RemoveEmptyEntries);
                int n = sFirst.Length;
                List < string[]> list = new List<string[]>();   
                foreach(string s in sEach)
                {
                    sFirst = s.Trim().Split(new char[] { ' ', '\t', ',' },
                        StringSplitOptions.RemoveEmptyEntries);
                    if(n != sFirst.Length)
                    {
                        MessageBox.Show("missing delimiter in your paste");
                        return;
                    }
                    list.Add(sFirst);
                }
                string[] sOne = new string[list.Count];
                for(int i = 0; i < n; i++)
                {                    
                    for(int j = 0; j < list.Count;j++)
                    {
                        sOne[j] = list[j][i];
                    }
                    AddTable(sOne);
                }
            }
            else AddTable(sEach);
        }

        private void FormTable()
        {
            string sPre = "<td>";
            string sSuf = "</td>";
            string sStrCol = "<tr>";
            string sEndCol = "</tr>";
            string sSt = "<table border='1'>";
            string sSe = "</table.";
            string sOut = sSt;
            if (RowsExpected > 0)
            {
                int cE = sMyTable.Count;
                int rE = RowsExpected;
                for (int r = 0; r < RowsExpected; r++)
                {
                    sOut += sStrCol;
                    for (int c = 0; c < cE; c++)
                    {
                        string s = sMyTable[c][r];
                        sOut += sPre + s + sSuf;
                    }
                    sOut += sEndCol;
                }
                sOut += sSe;
            }
            else  // columns
            {
                int rE = sMyTable.Count;
                int cE = ColsExpected;
                for (int r = 0; r < rE; r++)
                {
                    sOut += sStrCol;
                    for (int c = 0; c < cE; c++)
                    {
                        string s = sMyTable[r][c];
                        sOut += sPre + s + sSuf;
                    }
                    sOut += sEndCol;
                }
                sOut += sSe;
            }
            tbScratch.Text = sOut;
        }

        private int GatherClipboardText()
        {
            string sIn = Clipboard.GetText();
            // remove emptys is needed because \r\n is newline
            sEach = sIn.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return sEach.Length;
        }

        private void btnClearTab_Click(object sender, EventArgs e)
        {
            EnableRC();
        }

        private void btnShowTab_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }
    }
}