#define SPECIAL1

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace MacroViewer
{
    public partial class utils : Form
    {
        private string sLoc;
        private bool bDoingExample = false;
        private string sColor = ""; // used with SPECIAL1
        int r30, c30;   // number of rows and columns in that small data grid table
        private int RowsExpected = 0;
        private int ColsExpected = 0;
        private int nGathered = 0;
        private string[] sEach;
        private List<string[]> sMyTable = new List<string[]>();
        public utils()
        {
            InitializeComponent();
            sLoc = Utils.WhereExe;
#if SPECIAL1
            cbUseDelims.Checked = true;
            cbUseBorder.Checked = true;
#endif
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
            if (rbIsImg.Checked)
                tbResult.Text = Utils.AssembleIMG(strUrl);
            else tbResult.Text = Utils.FormUrl(strUrl, strTmp);
            if (bDoingExample)
            {  //<table border="1" width="50%"><tr><td>atest</td></tr></table>
                string s = "Your <b>example</b> follows below this horizontal line<hr>";
                s += tbResult.Text;
                s += "<hr>Your example is above.  ";
                s += "<font color='#006400'>I am DarkGreen and <b>bold</b></font><br>";
                s += "Put me in a <table border='1' width='%50'><tr><td>box if you are <b>BOLD</b></td></tr></table>";
                s += "<br>Click the adjacent 'Show In Browser' to see it";
                tbScratch.Text = s;
            }
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
            bDoingExample = true;
            string Samp1 = "Click to see how to find the release date of an hp laptop";
            string Samp2 = "https://www.lifewire.com/how-to-find-the-serial-number-of-an-hp-laptop-5189844#:~:text=You%20can%20determine%20your%20laptop's,week%20of%20the%20year%202020.";
            if (rbLnkToImg.Checked)
            {
                Samp1 = "Click to see an Expert Icon";
                Samp2 = "https://h30434.www3.hp.com/html/@029DD5B9D9DE7F854E52DDBD91AAAAD9/rank_icons/expert.png";
            }
            if (rbIsImg.Checked)
            {
                Samp1 = "This field is ignored";
                Samp2 = "https://h30434.www3.hp.com/html/@029DD5B9D9DE7F854E52DDBD91AAAAD9/rank_icons/expert.png";
            }
            tbTEXT.Text = Samp1;
            tbURL.Text = Samp2;
            MakeResult();
            bDoingExample = false;
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


        private int RefreshTable()
        {
            int i = cbUseBorder.Checked ? 1 : 0;
            try
            {
                r30 = Convert.ToInt32(tbRows.Text);
                c30 = Convert.ToInt32(tbCols.Text);
            }
            catch
            {
                ClearDGV();
                return 1;
            }
            int n = r30 * c30;
            tbScratch.Text = Utils.FormTable(r30, c30, true, i);
            return n;
        }

        private void btnApplyTab_Click(object sender, EventArgs e)
        {
            int n = RefreshTable();
            if (cbPreFill.Checked && (n <= 30))
            {
                dgv.Rows.Clear();
                for (int i = 0; i < n; i++)
                {
                    //DataGridViewRow row = new DataGridViewRow();
                    dgv.Rows.Add(Utils.strFillSubscript(r30, c30, i), "    ");
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
            RefreshTable();
            string sBig = tbScratch.Text;
            string s1, s2;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (null == row.Cells[0].Value) break;
                s1 = row.Cells[0].Value.ToString();
                if (null == row.Cells[1].Value) break;
                s2 = row.Cells[1].Value.ToString();
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
            if (RowsExpected == 0) RowsExpected = nGathered;
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
                List<string[]> list = new List<string[]>();
                foreach (string s in sEach)
                {
                    sFirst = s.Trim().Split(new char[] { ' ', '\t', ',' },
                        StringSplitOptions.RemoveEmptyEntries);
                    if (n != sFirst.Length)
                    {
                        MessageBox.Show("missing delimiter in your paste");
                        return;
                    }
                    list.Add(sFirst);
                }
                string[] sOne = new string[list.Count];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < list.Count; j++)
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
            string sSt = cbUseBorder.Checked ? "<table border='1'>" : "<table>";
            string sSe = "</table>";
            string sOut = sSt;
            if (RowsExpected > 0)
            {
                int cE = sMyTable.Count;
                int rE = RowsExpected;
                for (int r = 0; r < RowsExpected; r++)
                {
                    int i = 0;
                    sOut += sStrCol;
#if SPECIAL1
                    sColor = sMyTable[cE - 1][r];
#endif
                    for (int c = 0; c < cE; c++)
                    {
                        string s = sMyTable[c][r];
#if SPECIAL1
                        s = FormSpecial(i, s);
                        i++;
#endif
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

        private void ClearDGV()
        {
            dgv.Rows.Clear();
            tbCols.Text = "1";
            tbRows.Text = "1";
        }

        private void btnCleardgv_Click(object sender, EventArgs e)
        {
            ClearDGV();
        }

        private void utils_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Utils.WordpadHelp("UTILS");
        }

        private void btnApplyList_Click(object sender, EventArgs e)
        {
            CopyListToScratch();
        }

        private void CopyListToScratch()
        {
            string sOut = rbBullet.Checked ? "<ul>" : "<ol>";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (null == row.Cells[1].Value) break;
                string s = row.Cells[1].Value.ToString();
                sOut += "<li>" + s + "</li>";
            }
            sOut += rbBullet.Checked ? "</ul>" : "</ol>";
            //<table border='1'><tr><td>R0C0_AAAA</td></tr></table>
            if (cbFrameList.Checked)
            {
                sOut = "<table border='1'><tr><td>" + sOut + "</td></tr></table>";
            }
            tbScratch.Text = sOut;
        }

        private void btnClrList_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
        }

        private void bltnShowList_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbScratch.Text);
        }

        private void btnFillList_Click(object sender, EventArgs e)
        {
            nGathered = GatherClipboardText();
            dgv.Rows.Clear();
            foreach (string s in sEach)
            {
                dgv.Rows.Add(dgv.Rows.Count + 1, s);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                dgv.Rows.Clear();
                dgv.Visible = true;
                dgv.AllowUserToAddRows = true;
                dgv.AllowUserToDeleteRows = true;
            }
            else
            {
                dgv.Rows.Clear();
                dgv.Visible = cbPreFill.Checked;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
            }
            if(tabControl1.SelectedIndex == 2)
            {
                tbColorResult.ForeColor = tbTextToColor.ForeColor;
                tbColorResult.BackColor = tbTextToColor.BackColor;
            }
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbScratch, 1);
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbScratch, 2);
        }

        private void btnInvertNL_Click(object sender, EventArgs e)
        {
            Utils.SwapNL(ref tbScratch);
        }

        private void cbUseBorder_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbUseBorder.Checked)
            {
                tbScratch.Text = tbScratch.Text.Replace("<table>", "table border='1'>");
            }
            else
            {
                tbScratch.Text = tbScratch.Text.Replace("<table border='1'>", "<table>");
            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            //string htmlColor = ColorTranslator.ToHtml(selectedColor);
        }

        private void btnShowB_Click(object sender, EventArgs e)
        {
            ShowBrowser(tbColorResult.Text);
        }

        private void btnBackGround_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    tbColorResult.BackColor = selectedColor;
                }
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    tbColorResult.ForeColor = selectedColor;
                }
            }
        }

        private void btnSetFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    Font selectedFont = fontDialog.Font;
                    tbColorResult.Font = selectedFont;
                }
            }
            tbColorResult.Text = tbTextToColor.Text;
        }

        private void btnApplyCol_Click(object sender, EventArgs e)
        {
            tbColorResult.Text = tbTextToColor.Text;
            HTMlColorPicker cp = new HTMlColorPicker(tbColorResult.Font);
            cp.ShowDialog();
            if(cp.WantedColors != null)
            {
                tbColorResult.ForeColor = cp.WantedColors.ForeColor;
                tbColorResult.BackColor = cp.WantedColors.BackColor;
            }
            cp.Dispose();
        }

        private void btnShowCode_Click(object sender, EventArgs e)
        {
            
            string s = Utils.ApplyColors1(ref tbColorResult);
            tbScratch.Text = s;
        }

        private void btnClrCol_Click(object sender, EventArgs e)
        {
            tbTextToColor.Text = "";
            tbTextToColor.Font = gbTxtCol.Font;
        }



#if SPECIAL1
        private string FormSpecial(int i, string s)
        {
            string r = s;
            // expecting Cyan' ForeGround ForeGroundBold Background BackgroundBold '#00FFFF
            // from ChatGPT HTML palette list replacing ":" with the above words in the quote '
            if (i == 0) return s;
            if(i == 5) // note that HP forum no longer allows background colors
            {
                //return "&lt;span style='background-color: " + sColor + "; color: white;'&gt;" + "&lt;b&gt;ForeGroundBold&lt;b&gt;" + "&lt;/span&gt;"; //  used with white
                return "&lt;font color='" + sColor + "'&gt;" +"ForeGround" + "&lt;/font&gt;"; // used with 'black'
            }

            switch (i)
            {
                case 1: r = "<font color='" + sColor + "'>" + s + "</font>";
                    break;
                case 2: r  = "<font color='" + sColor + "'><b>" + s + "</b></font>";
                    break;
                    // below white was black (or viceversa) for first run of SPECIAL1 and
                case 3: r = "<span style='background-color: " + sColor + "; color: black;'>" + s + "</span>";
                    break;   
                case 4: r = "<span style='background-color: " + sColor + "; color: black;'><b>" + s + "</b></span>";
                    break;
            }
            return r;
        }
#endif
    }
}
/* the below was used with 'SPECIAL1'
Black: #000000
White: #FFFFFF
Red: #FF0000
Green: #008000
Blue: #0000FF
Yellow: #FFFF00
Cyan: #00FFFF
Magenta: #FF00FF
Silver: #C0C0C0
Gray: #808080
Maroon: #800000
Olive: #808000
Purple: #800080
Teal: #008080
Navy: #000080
Orange: #FFA500
Aquamarine: #7FFFD4
Turquoise: #40E0D0
Lime: #00FF00
Fuchsia: #FF00FF
Indigo: #4B0082
Violet: #EE82EE
Pink: #FFC0CB
Peach: #FFDAB9
Beige: #F5F5DC
 */