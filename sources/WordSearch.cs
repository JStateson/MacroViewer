using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Diagnostics.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Security;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;

namespace MacroViewer
{
    public partial class WordSearch : Form
    {
        private int SelectedRow = -1;
        private List<CFound> cFound;
        private List<CFound> cSorted;
        private List<CBody> cAll;
        private string[] keywords;
        private bool[] KeyPresent;
        private int[] KeyCount;
        private int TotalMatches = 0;
        public int LastViewed { get; set; }
        int nUseLastViewed = -1;
        private bool TriedFailed = false;
        public WordSearch(ref List<CBody> Rcb)
        {
            InitializeComponent();
            cFound = new List<CFound>();
            cSorted = new List<CFound>();
            cAll = Rcb;
            LastViewed = -1;
            cbHPKB.Checked = Properties.Settings.Default.IncludeHPKB;
            cbOfferAlt.Checked = Properties.Settings.Default.OfferAltSearch;
        }



        private void dgvSearched_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = cSorted[SelectedRow].WhereFound;
            string strTemp = cAll[n].sBody;
            if (strTemp == "") return;
            nUseLastViewed = n;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }

        private void dgvSearched_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (SelectedRow < 0) return;
            int n = cSorted[SelectedRow].WhereFound;
            nUseLastViewed = n;
            string[] sEach = cAll[n].fKeys.Trim().Split(new char[] {' '});
            lbKeyFound.Items.Clear();
            foreach (string s in sEach)
            {
                lbKeyFound.Items.Add(s);
            }
        }

        private string PerformSearch(string text)
        {
            string strRtn = "";
            string sTmp = "";
            string sPattern = "";
            int i, n = keywords.Length;
            Regex regex;
            MatchCollection allMatches;
            for (i = 0; i < n; i++) KeyCount[i] = 0;
            i = 0;
            foreach (string keyword in keywords)
            {
                if(rbExactMatch.Checked)
                {
                    regex = new Regex("\\b" + Regex.Escape(keyword) + "\\b",
                        cbIgnCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None);
                }
                else
                {
                    sPattern = "\\b\\w*" + keyword + "\\w*\\b";
                    regex = new Regex(sPattern, cbIgnCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None);
                }
                allMatches = regex.Matches(text);
                TotalMatches += allMatches.Count;
                foreach (Match m in allMatches)
                {
                    sTmp = m.Value;
                    if (strRtn.Contains(sTmp)) continue;
                    KeyCount[i]++;
                    strRtn += sTmp + " ";
                }
                i++;
            }
            return strRtn;
        }

        private int CountWords(string s)
        {
            string[] sS = s.Trim().Split(new char[] { ' ', '\t' });
            return sS.Length;
        }

        private void RunSort(int n, ref int[] Unsorted, ref int[] SortInx)
        {
            int a,b;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    a = SortInx[j];
                    b = SortInx[j + 1];
                    if (Unsorted[a] < Unsorted[b])
                    {
                        SortInx[j] = SortInx[j + 1];
                        SortInx[j + 1] = a;
                    }
                }
            }
        }

        private string FormBetter(string s)
        {
            string t = s.ToLower();
            if (t.Contains("wi-fi"))
            {
                return s + "wifi";
            }
            if (t.Contains("wifi"))
            {
                return s + "wi-fi";
            }
            return s;
        }

        private void RunSearch()
        {
            cFound.Clear();
            cSorted.Clear();
            TotalMatches = 0;
            TriedFailed = true;
            lbKeyFound.Items.Clear();
            tbNumMatches.Text = "";
            string sBetter = FormBetter(tbKeywords.Text.Trim());
            keywords = sBetter.Split(new char[] { ' ', '\t' });
            int n = keywords.Length;
            int j,i = 0;
            KeyPresent = new bool[n];
            KeyCount = new int[n];
            int[] cWidth = new int[4] { 48, 64, 64, 316 };
            n = cAll.Count;
            int[] Unsorted = new int[n];
            int[] SortInx = new int[n];
            int CFcnt = 0;
            if(cbHPKB.Checked)
                HP_KB_find();

            foreach(CBody cb in cAll)
            { 
                string sKeys = PerformSearch(cb.Name + " " + cb.sBody);
                if (sKeys != "")
                {
                    n = 0;
                    CFound cf = new CFound();
                    foreach (int m in KeyCount)
                    {
                        n += m;
                    }
                    cAll[i].fKeys = sKeys;
                    SortInx[CFcnt] = CFcnt;
                    Unsorted[CFcnt] = CountWords(sKeys);
                    cAll[i].nWdsfKey = Unsorted[CFcnt];
                    CFcnt++;
                    cf.Name = cb.Name;
                    cf.Number = cb.Number;
                    cf.File = cb.File;
                    cf.Found = n.ToString();
                    cf.WhereFound = i;
                    cFound.Add(cf);
                }
                else
                {
                    cAll[i].fKeys = "";
                }
                i++;
            }
            RunSort(CFcnt, ref Unsorted, ref SortInx);
            n  = cFound.Count;
            for(i = 0; i < n; i++)
            {
                j = SortInx[i];
                cSorted.Add(cFound[j]);
            }
            dgvSearched.DataSource = cSorted.ToArray();
            dgvSearched.Columns[1].HeaderText = "Mac#";
            j = 0;
            foreach (int k in cWidth)
                dgvSearched.Columns[j++].Width = k;
            if (TotalMatches > 0)
            {
                tbNumMatches.Text = TotalMatches.ToString();
                TriedFailed = false;
            }
            else
            {
                tbNumMatches.Text = "";
                TriedFailed = true;
            }
            gbAlltSearch.Visible = TriedFailed && cbOfferAlt.Checked;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RunSearch();
        }

        private void tbKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RunSearch();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnS1_Click(object sender, EventArgs e)
        {
            RunSearch();
        }

        private void btnExitToMac_Click(object sender, EventArgs e)
        {
            LastViewed = nUseLastViewed;
            this.Close();
        }

        private void WordSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IncludeHPKB = cbHPKB.Checked;
            Properties.Settings.Default.OfferAltSearch = cbOfferAlt.Checked;
            Properties.Settings.Default.Save();
        }

        private void HP_KB_find()
        {
            string s = tbKeywords.Text.Trim();
            if (s == "") return;
            string strUrl = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=";
            Utils.LocalBrowser(strUrl + s);
        }


        private void WordSearch_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Utils.WordpadHelp("SEARCH");
        }

        private void cbOfferAlt_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbOfferAlt.Checked)
            {
                gbAlltSearch.Visible = TriedFailed;
            }
            else gbAlltSearch.Visible = false;
        }


        private string PlusConCat(string t, string[] ss)
        {
            string sRtn = t;
            foreach(string s in ss)
            {
                sRtn += s + "+";
            }
            return sRtn.Substring(0, sRtn.Length - 1); ;
        }

        private void RunB(string s, string t)
        {
            Utils.LocalBrowser(s + PlusConCat(t, keywords));
        }

        //https://www.ebay.com/sch/i.html?_nkw=hp+cm1415&_sacat=58058
        private void AltSearch(string sKey)
        {
            //string[] sBtn = { "PC", "PRN", "EBA", "GOO", "MAN", "DRV"};
            string s = "";
            string t = "";
            switch(sKey)
            { 
                case "PC":
                    RunB("https://www.google.com/search?q=", "DRIVERS+HP+");
                    RunB("https://www.google.com/search?q=", "DISASSEMBLE+HP+");
                    break;
                case "PRN":
                    RunB("https://www.google.com/search?q=", "PRINTER+DRIVERS+HP+");
                    RunB("https://www.google.com/search?q=", "PRINTER+MANUAL+HP+");
                    RunB("https://www.google.com/search?q=", "FACTORY+RESET+HP+");
                    RunB("https://www.google.com/search?q=", "YOUTUBE+NETWORK+CONNECT+HP+");
                    break;
                case "EBA":
                    t = PlusConCat("HP ",keywords);
                    s = "https://www.ebay.com/sch/i.html?_nkw=" + t + "&_sacat=58058";
                    Utils.LocalBrowser(s);
                    break;
                case "GOO":
                    RunB("https://www.google.com/search?q=", "HP+");
                    break;
                case "MAN":
                    RunB("https://www.google.com/search?q=", "HP+MANUAL+");
                    break;
                case "DRV":
                    RunB("https://www.google.com/search?q=", "HP+DRIVERS+");
                    break;
                case "HPKB":
                    HP_KB_find();
                    break;
            }

        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            AltSearch("PC");
        }

        private void btnPrn_Click(object sender, EventArgs e)
        {
            AltSearch("PRN");
        }

        private void btnEbay_Click(object sender, EventArgs e)
        {
            AltSearch("EBA");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            AltSearch("GOO");
        }

        private void btnMan_Click(object sender, EventArgs e)
        {
            AltSearch("MAN");
        }

        private void btnDrv_Click(object sender, EventArgs e)
        {
            AltSearch("DRV");
        }

        private void btnKbSearch_Click(object sender, EventArgs e)
        {
            AltSearch("HPKB");
        }
    }
}
