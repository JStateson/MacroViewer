using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Windows.Documents;

namespace MacroViewer.sources
{

    /*
    <a jsname="UWckNb" href="https://pranx.com/bios/"
        data-ved="2ahUKEwjP7f_qp9uHAxU5HkQIHd_BN6cQFnoECBUQAQ"
        ping="/url?sa=t&amp;source=web&amp;rct=j&amp;opi=89978449&amp;
        url=https://pranx.com/bios/&amp;ved=2ahUKEwjP7f_qp9uHAxU5HkQIHd_BN6cQFnoECBUQAQ" >
        <h3 >Phoenix BIOS Setup Utility Simulator</h3></a>
    */

    //<a href="http://h10032.www1.hp.com/ctg/Manual/c06534544.pdf" target="_blank">stest</a>

    public partial class BiosEmuSim : Form
    {
        public class cBSFview
        {
            public string sTEXT { get; set; }
            public string sHREF { get; set; }
        }
        private List<cBSFview> BSFsources = new List<cBSFview>();
        private string srcData = "BiosSimulators.txt";
        private string LsrcData = "";
        private int nCurrentRow;
        private string sCurrentRowURL; 
        private BindingSource bs = new BindingSource();
        private bool bChanged = false;
        private List<string> sRawText = new List<string>();
        private List<string> sRawHTML = new List<string>(); 
        private List<int> iInxText = new List<int>();
        Color Blue;
        Color Red = Color.Red;
        public BiosEmuSim()
        {
            InitializeComponent();
            Blue = btnSave.ForeColor;
            LsrcData = Utils.WhereExe + "/" + srcData;
            LoadSimFiles();
        }

        private void LoadSimFiles()
        {
            string sDefault = "<a href=\"https://pranx.com/bios\" target=\"_blank\">Phoenix CMOS Setup Utility</a>";
            int i = 0;

            string s ="", sH="", sT="";
            BSFsources.Clear();
            sRawText.Clear();
            sRawHTML.Clear();
            dgvBIOS.DataSource = null;
            dgvBIOS.Refresh();


            if (!File.Exists(LsrcData))
            {
                File.WriteAllText(LsrcData, sDefault);
            }
            StreamReader sr = new StreamReader(LsrcData);
            s = sr.ReadLine();
            while (s != null)
            {
                int r = nExtractHT(s, ref sH, ref sT);
                if(r==0)
                {

                    sRawHTML.Add(sH);
                    sRawText.Add(sT);
                    i++;
                }
                else
                {
                    tbError.Text += "error reading data:" + r.ToString();
                    break;
                }
                s = sr.ReadLine();
            }
            MySort(sRawText);
            for(i = 0; i<sRawHTML.Count; i++)
            {
                int j = iInxText[i];
                cBSFview cBSF = new cBSFview();
                cBSF.sTEXT = sRawText[j];
                cBSF.sHREF = sRawHTML[j];
                BSFsources.Add(cBSF);
            }
            sr.Close();
            bs.DataSource = BSFsources;
            dgvBIOS.DataSource = bs;
            dgvBIOS.Columns[1].Visible = false;
            dgvBIOS.Columns[0].ReadOnly = true;
            dgvBIOS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvBIOS.Columns[0].HeaderText = "BIOS simulator / emulator";
            dgvBIOS.Refresh();
        }

        private void MySort(List<string> list)
        {
            int i = 0;
            var sortedIndices = list
     .Select((value, index) => new { Value = value, Index = index })
     .OrderBy(item => item.Value)
     .Select(item => item.Index)
     .ToList();
            foreach(var index in sortedIndices)
            {
                iInxText.Add(index);
            }
        }

        // note all have that h3
        private int nGoogleExtractHT(ref string sHREF, ref string sTEXT)
        {
            string s = Utils.GetHPclipboard();
            string t = s.ToLower();
            int i = s.IndexOf("href=");
            if(i == -1)return 1;
            i += 5; // should be a quote or double quote
            string c = s.Substring(i, 1);
            if (!(c == "'" || c == "\"")) return 2;
            i++;
            int j = s.IndexOf(c, i);
            if(j == -1) return 3;
            sHREF = s.Substring(i, j-i);
            i = s.LastIndexOf("</h3>");
            if (i == -1)
                i = s.LastIndexOf("</a>");
            if(i == -1) return 4;
            s = s.Substring(0, i);
            j = s.LastIndexOf(">");
            if (j == -1) return 5;
            j++;
            s = s.Substring(j, i - j).Replace("...", "");
            sTEXT = s.Trim();
            return 0;
        }

        private int nExtractHT(string s, ref string sHREF, ref string sTEXT)
        {
            string ab = "_blank\">";
            string t = s.ToLower();
            int i = s.IndexOf("href=");
            if (i == -1) return 1;
            i += 5; // should be a quote or double quote
            string c = s.Substring(i, 1);
            if (!(c == "'" || c == "\"")) return 2;
            i++;
            int j = s.IndexOf(c, i);
            if (j == -1) return 3;
            sHREF = s.Substring(i, j - i);
            i = s.LastIndexOf("</a>");
            if (i == -1) return 4;
            j = s.LastIndexOf(ab);
            if (j == -1) return 5;
            j += ab.Length;
            sTEXT = s.Substring(j,i-j);
            return 0;
        }

        private void btnClrIL_Click(object sender, EventArgs e)
        {
            tbError.Text = "";
            tbHref.Text = "";
            tbText.Text = "";
            tbURL.Text = "";
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            string sH = "";
            string sT = "";
            int r = nGoogleExtractHT(ref sH, ref sT);
            if(r == 0)
            {
                tbHref.Text = sH;
                tbText.Text = sT;
                tbURL.Text = Utils.FormUrl(tbHref.Text, tbText.Text);
            }
            else
            {
                tbError.Text = "error code " + r.ToString();
            }
        }
        
        private int iHasItem()
        {
            int i = 0;
            string s = tbText.Text;
            foreach(cBSFview cb in BSFsources)
            {
                if (cb.sTEXT == s) return i;
                i++;
            }
            return -1;
        }

        private void MustSave(bool b)
        {
            bChanged = b;
            btnSave.ForeColor = b ? Red : Blue;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int i = iHasItem() + 1;
            if(iHasItem() > 0)
            {
                tbError.Text = "Already in table";
                dgvBIOS.Rows[i-1].Selected = true;
            }
            else
            {
                cBSFview cBSF = new cBSFview();
                cBSF.sTEXT = tbText.Text;
                cBSF.sHREF = tbHref.Text;
                BSFsources.Add(cBSF);
                bs.ResetBindings(false);
                MustSave(true);
            }
        }

        private void dgvBIOS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            nCurrentRow = e.RowIndex;
            tbTextB.Text = BSFsources[nCurrentRow].sTEXT;
            tbHrefB.Text = BSFsources[nCurrentRow].sHREF;
            btnUpdateURL.Enabled = false;
            sCurrentRowURL = Utils.FormUrl(tbHrefB.Text, tbTextB.Text);
        }

        private void dgvBIOS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string s = BSFsources[nCurrentRow].sHREF;
            Utils.LocalBrowser(s);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strErr = Utils.BBCparse(tbURL.Text);
            if (strErr == "")
            {
                Utils.LocalBrowser(tbHref.Text);
            }
            else tbError.Text = strErr;
        }

        private void btnMakeURL_Click(object sender, EventArgs e)
        {
            tbURL.Text = Utils.FormUrl(tbHref.Text, tbText.Text);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void SaveChanges()
        {
            using (StreamWriter writer = new StreamWriter(LsrcData, false))
            {
                foreach (cBSFview cBSF in BSFsources)
                {
                    string s = Utils.FormUrl(cBSF.sHREF, cBSF.sTEXT);
                    writer.WriteLine(s);
                }
                writer.Close();
            }
            MustSave(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnUpdateURL_Click(object sender, EventArgs e)
        {
            BSFsources[nCurrentRow].sHREF = tbHrefB.Text;
            BSFsources[nCurrentRow].sTEXT = tbTextB.Text;
            bs.ResetBindings(false);
        }

        private void btnTestB_Click(object sender, EventArgs e)
        {
            Utils.LocalBrowser(tbHrefB.Text);
            string s = Utils.FormUrl(tbHrefB.Text, tbTextB.Text);
            bChanged = (s != sCurrentRowURL);
            btnUpdateURL.Enabled = bChanged;
        }

        private void BiosEmuSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(bChanged)
            {
                DialogResult dr = MessageBox.Show("You have not saved changes." + Environment.NewLine +
                    "Click OK to save",
                    "Click Cancel to return", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    SaveChanges();
                    e.Cancel = false;
                }

                else
                    e.Cancel = true;
            }
        }



        private void btnCLRatbHREF_Click(object sender, EventArgs e)
        {
            tbHref.Text = "";
        }

        private void btnCLRatbaTEXT_Click(object sender, EventArgs e)
        {
            tbText.Text = "";
        }

        private void dgvBIOS_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MustSave(true);
        }

        private void btnCLRhb_Click(object sender, EventArgs e)
        {
            tbHrefB.Text = "";
        }

        private void btnCLRtb_Click(object sender, EventArgs e)
        {
            tbTextB.Text = "";
        }

        private void btnCanChg_Click(object sender, EventArgs e)
        {
            LoadSimFiles();
            MustSave(false);
        }

        private void dgvBIOS_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
    }
}
