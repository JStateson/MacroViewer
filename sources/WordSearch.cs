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
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Diagnostics;

namespace MacroViewer
{
    public partial class WordSearch : Form
    {
        public class cRefURLs
        {
            public string PageOut="";
            public string sFile="", sMacN="", nMac = "";
            public void init(string ssFile, string ssMacN, string sMac)
            {
                sFile = ssFile;
                sMacN = ssMacN;
                nMac = sMac;
                PageOut = "";
            }
            // only look after the _blank>
            public bool LookForUrl(int inx, ref string s)
            {
                // <a href ...blank..whatever.....</a>
                string sBlank = "=\"_blank\">";
                int Left_a, Right_a;
                Right_a = s.IndexOf("</a>", inx);
                if (Right_a == -1) return true;
                Left_a = s.LastIndexOf("<a href", inx);
                if(Left_a == -1) return true;
                string t = s.Substring(Left_a, Right_a - Left_a) + "<br><br>";
                int i = t.IndexOf(sBlank);
                inx -= Left_a;
                if (inx < i) return false;
                if (PageOut.Contains(t)) return true;
                PageOut += t;
                return true;
            }
        }

        private int SelectedRow = -1;
        private List<cRefURLs> RefUrls = new List<cRefURLs>();
        private List<CFound> cFound = new List<CFound>();
        private List<CFound> cSorted = new List<CFound>();
        private List<CFound> aSorted = new List<CFound>();
        private List<CBody> cAll;
        List<string> keywords;
        private bool[] KeyPresent;
        private int[] KeyCount;
        private int TotalMatches = 0;
        public int LastViewed { get; set; }
        public string NewItemName { get; set; }
        public string NewItemID { get; set; }
        int nUseLastViewed = -1;
        private bool TriedFailed = false;
        private int[] cWidth = new int[3] { 48, 64, 64 }; // last was 316 but now using fill
        private int[] Unsorted;
        private int[] SortInx;
        private int[] aSort;
        private int CFcnt = 0;
        private Font Reg12;
        private Font Reg10;
        private string SelectedFile = "";
        private string HasFiles = "";
        private bool [] ColSortDirection = new bool[4] {true,true,true,true}; // true is descending false is ascending
        private Color RestoreColor;
        private string[] CountryCodes;
        private string CountryCodeResults;

        public WordSearch(ref List<CBody> Rcb, bool bAllowChangeExit)
        {
            InitializeComponent();
            cAll = Rcb;
            LastViewed = -1;
            cbHPKB.Checked = Properties.Settings.Default.IncludeHPKB;
            cbOfferAlt.Checked = Properties.Settings.Default.OfferAltSearch;
            btnExitToMac.Enabled = bAllowChangeExit;
            NewItemID = "";
            NewItemName = "";
            Reg12 = cbHPKB.Font;
            Reg10 = gbAlltSearch.Font;
            RestoreColor = lbTMinfo.ForeColor;
            CountryCodes = Properties.Resources.Sorted_Raw_List.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void CountryLookup(string sLine)
        {
            CountryCodeResults = "";
            int i = 5; //'#abc "
            string[] sWords = sLine.Split(new char[] { ' ', '\t', ',' },StringSplitOptions.RemoveEmptyEntries);
            foreach(string sWord in sWords)
            {
                foreach (string s in CountryCodes)
                {
                    if (s.StartsWith("#"))
                    {
                        if (s.Substring(i).ToLower().Contains(sWord))
                        {
                            CountryCodeResults += s + Environment.NewLine;
                        }
                    }
                }
            }
        }
        private void AddSelButtons()
        {
            int i = 0;
            int x = 2, y = 18;
            gbSelect.Controls.Clear();
            foreach (string s in Utils.LocalMacroPrefix)
            {
                Button btn = new Button();
                btn.Text = s;
                btn.Width = 50;
                btn.Height = 30;
                btn.Location = new System.Drawing.Point(x + i*(btn.Width + 10), y);
                btn.Enabled = FileFound(s);
                gbSelect.Controls.Add(btn);
                btn.Click += Btn_Click;
                i++;
            }
        }


        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                SelectedFile = button.Text;
                dgvSearched.RowEnter -= dgvSearched_RowEnter;
                SortTable(0);
                dgvSearched.RowEnter += dgvSearched_RowEnter;
            }
        }

        private void FormCandidateMacros(string sBtnName)
        {
            CMoveSpace cms = new CMoveSpace();
            cms.Init();
            if (lbNewItems.DataSource != null)
                lbNewItems.DataSource = null;
            string s = Utils.sFindUses(sBtnName).Trim();
            string[] sOut = s.Split(new char[] {' '});
            int i = 0;
            foreach(string sID in sOut)
            {
                int n = cms.GetMacCountAvailable(sID);
                if (n == 0) sOut[i] = "";
                i++;
            }
            lbNewItems.Items.Clear();
            lbNewItems.DataSource = sOut;
            gbMakeNew.Visible = true;       
        }

        private string GetRefUrl(string sMacName)
        {
            int i = -1;
            string strRtn = "";
            foreach(cRefURLs cr in RefUrls)
            {
                if(sMacName == cr.sMacN)
                {
                    i = Convert.ToInt32(cr.nMac) ;
                    strRtn = cr.PageOut;
                    break;
                }
            }
            Debug.Assert(i > 0, "RF macro number not found!");
            return strRtn;
        }

        private void dgvSearched_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || SelectedRow < 0) return;
            List<CFound> bSorted = (List<CFound>)dgvSearched.DataSource as List<CFound>;
            int n = bSorted[SelectedRow].WhereFound;
            string strType = bSorted[SelectedRow].File;
            string strTemp = cAll[n].sBody;
            if (strTemp == "") return;
            nUseLastViewed = n;
            if (dgvSearched.Rows[e.RowIndex].Cells[0].Value.ToString() == "RF")
            {
                string MacName = dgvSearched.Rows[e.RowIndex].Cells[3].Value.ToString();
                strTemp = GetRefUrl(MacName);
                if (strTemp == "") return;
            }
            Utils.ShowPageInBrowser(strType, strTemp);
        }

        private void SortTable(int column)
        {
            bool b;
            int n=0;
            if(column == 2)
            {
                b = !ColSortDirection[2];
                ColSortDirection[2] = b;
                RunMacSort(CFcnt, b);
            }
            if(column == 0) // sort by file
            {
                b = !ColSortDirection[2];
                ColSortDirection[2] = b;
                if(SelectedFile == "")
                {
                    AlphaExtractFile(b);
                    n = aSort.Length;
                }
                else
                {
                    n = JustExtract(SelectedFile);
                }
                aSorted.Clear();
                for(int i = 0; i < n; i++)
                {
                    int j = aSort[i];
                    aSorted.Add(cSorted[j]);
                }
                dgvSearched.DataSource = null;
                dgvSearched.Invalidate();
                dgvSearched.DataSource = aSorted;
                SetDGVwidth();
                dgvSearched.Refresh();
            }
        }

        private void dgvSearched_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (SelectedRow < 0)
            {
                SortTable(e.ColumnIndex);
                dgvSearched.Refresh();
                return;
            }
            int n = cSorted[SelectedRow].WhereFound;
            nUseLastViewed = n;
            string[] sEach = cAll[n].fKeys.Trim().Split(new[] {"\n"}, StringSplitOptions.None);
            lbKeyFound.Items.Clear();
            foreach (string s in sEach)
            {
                lbKeyFound.Items.Add(s);
            }
        }

        private string PerformSearch(string text, string sMacID)
        {
            string strRtn = "";
            string sTmp = "";
            string sPattern = "";
            int i, n = keywords.Count;
            Regex regex;
            MatchCollection allMatches;
            for (i = 0; i < n; i++) KeyCount[i] = 0;
            i = 0;
            foreach (string keyword in keywords)
            {
                if (keyword.Length == 1) continue;

                if(keyword.Contains(" ") || rbEPhrase.Checked)
                {
                    sPattern = $@"\b{Regex.Escape(keyword)}\b";
                }
                else
                {
                    if(rbExactMatch.Checked)
                    {
                        sPattern = "\\b" + Regex.Escape(keyword) + "\\b";
                    }
                    else
                    {
                        sPattern = "\\b\\w*" + keyword + "\\w*\\b";
                    }
                }
                regex = new Regex(sPattern, cbIgnCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None);

                allMatches = regex.Matches(text);
                TotalMatches += allMatches.Count;
                foreach (Match m in allMatches)
                {
                    sTmp = m.Value.ToLower();
                    if(sMacID == "RF")
                    {
                        cRefURLs cr = RefUrls.Last();
                        bool b = cr.LookForUrl(m.Index, ref text);
                        if(!b)
                        {
                            // was NOT a match as was part of the url
                            KeyCount[i]--;
                        }
                    }
                    KeyCount[i]++;
                    if (strRtn.Contains(sTmp)) continue;
                    strRtn += sTmp + "\n";
                }
                i++;
            }
            return strRtn;
        }

        private void CountFile(string s)
        {
            if (!HasFiles.Contains(s))
                HasFiles += s + " ";
        }

        private bool FileFound(string s)
        {
            return HasFiles.Contains(s) ;
        }

        private void RunSort(int n, bool Descending)
        {
            int a,b;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    a = SortInx[j];
                    b = SortInx[j + 1];
                    if(Descending)
                    {
                        if (Unsorted[a] < Unsorted[b])
                        {
                            SortInx[j] = SortInx[j + 1];
                            SortInx[j + 1] = a;
                        }
                    }
                    else
                    {
                        if (Unsorted[a] > Unsorted[b])
                        {
                            SortInx[j] = SortInx[j + 1];
                            SortInx[j + 1] = a;
                        }
                    }
                }
            }
        }


        private string FormBetter(string s)
        {
            string t = s.ToLower();
            if (t.Contains("wi-fi"))
            {
                return s + " wifi";
            }
            if (t.Contains("wifi"))
            {
                return s + " wi-fi";
            }
            return s;
        }

        private void SetFont(Font f)
        {
            cbHPKB.Font = f;
            cbOfferAlt.Font = f;
        }

        private void TestForCountryCode(string sLine)
        {
            int i = sLine.IndexOf("country ");
            bool b = ((i >= 0) && (sLine.Length > 10));
            btnShowCC.Visible = b;
        }



        private void SplitStringWithQuotedPhrases(string input)
        {
            // Regex pattern to match words and quoted phrases
            string pattern = @"(""(?:\\.|[^""])*""|\S+)";
            keywords = new List<string>();
            if (rbEPhrase.Checked)
            {
                keywords.Add(input);
                return;
            }
            foreach (Match match in Regex.Matches(input, pattern))
            {
                keywords.Add(match.Value.Replace("\"",""));
            }
            return;
        }

        private void RunSearch()
        {
            string sBetter = "";
            cFound.Clear();
            cSorted.Clear();
            RefUrls.Clear();
            aSorted.Clear();
            TotalMatches = 0;
            TriedFailed = true;
            lbKeyFound.Items.Clear();
            tbNumMatches.Text = "";
            HasFiles = "";
            SelectedFile = "";
            dgvSearched.DataSource = null;
            dgvSearched.Rows.Clear();

            TestForCountryCode(tbKeywords.Text.ToLower());

            sBetter = FormBetter(tbKeywords.Text.Trim());
            if (rbEPhrase.Checked)
                sBetter = tbKeywords.Text.Trim().Replace("\"", "");
            else sBetter = FormBetter(tbKeywords.Text.Trim());
            tbKeywords.Text = sBetter;

            SplitStringWithQuotedPhrases(sBetter);


            int n = keywords.Count;
            int j,i = 0;
            KeyPresent = new bool[n];
            KeyCount = new int[n];
            CFcnt = 0;
            n = cAll.Count;
            Unsorted = new int[n];
            SortInx = new int[n];
            if (cbHPKB.Checked)
                HP_KB_find();
            lbDropped.Text = "";
            foreach(string s in keywords)
            {
                if(s.Length == 1)
                {
                    if(!lbDropped.Text.Contains(s))
                        lbDropped.Text += s + " ";
                }
            }
            if(lbDropped.Text != "")lbDropped.Text = "Dropped from search: " + lbDropped.Text;
            lbDropped.Visible = rbExactMatch.Checked || rbAnyMatch.Checked;
            foreach (CBody cb in cAll)
            {
                string sPrN = cb.Name + " ";
                if (cb.File == "RF")
                {
                    cRefURLs cr = new cRefURLs();
                    cr.init(cb.File, cb.Name, cb.Number);
                    RefUrls.Add(cr);
                    sPrN = "";  // RF does not need to have the name searched unlike all other macro information.
                }
                string sKeys = PerformSearch(sPrN + cb.sBody,cb.File);  // eg: do not include "support" for RF
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
                    Unsorted[CFcnt] = n;
                    cAll[i].nWdsfKey = Unsorted[CFcnt];
                    CFcnt++;
                    cf.Name = cb.Name;
                    cf.Number = cb.Number;
                    cf.File = cb.File;
                    CountFile(cb.File);
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
            NotifyFinding(CFcnt);
            RunMacSort(CFcnt, true);
        }

        private void NotifyFinding(int cnt)
        {
            if(cnt == 0)
            {
                lbTMinfo.Text = "Nothing found";
                lbTMinfo.ForeColor = Color.Red;
                tbNumMatches.Visible = false;
            }
            else
            {
                lbTMinfo.Text = "Total Matches";
                lbTMinfo.ForeColor = RestoreColor;
                tbNumMatches.Visible = true;
            }
            gbAlltSearch.Visible = cnt==0 && cbOfferAlt.Checked;
            if (gbAlltSearch.Visible) SetFont(Reg10);
            else SetFont(Reg12);
            gbMakeNew.Visible = false;
        }

        private int JustExtract(string w)
        {
            aSort = new int[cFound.Count];
            int i = 0;
            foreach (string s in Utils.LocalMacroPrefix)
            {
                if (s != w) continue;
                for (int j = 0; j < cFound.Count; j++)
                {
                    if (s == cSorted[j].File)
                    {
                        aSort[i] = j;
                        i++;
                    }
                }
            }
            return i;
        }

        // get alphabet sort order for file "AIO" "LJ" etc.
        private void AlphaExtractFile(bool b)
        {
            aSort = new int[cFound.Count];
            int i = 0;
            if(b)
            {
                foreach (string s in Utils.LocalMacroPrefix)
                {
                    for (int j = 0; j < cFound.Count; j++)
                    {
                        if (s == cSorted[j].File)
                        {
                            aSort[i] = j;
                            i++;
                        }
                    }
                }
            }
            else
            {
                foreach (string s in Utils.LocalMacroPrefix.AsEnumerable().Reverse())
                {
                    for (int j = 0; j < cFound.Count; j++)
                    {
                        if (s == cSorted[j].File)
                        {
                            aSort[i] = j;
                            i++;
                        }
                    }
                }
            }
        }

        private void SetDGVwidth()
        {
            int j = 0;
            foreach (int k in cWidth)
                dgvSearched.Columns[j++].Width = k;
            dgvSearched.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RunMacSort(int CFcnt, bool Descending)
        {
            int i, j, n;
            if (CFcnt == 0) return;
            dgvSearched.RowEnter -= dgvSearched_RowEnter;
            cSorted.Clear();
            RunSort(CFcnt, Descending);
            n = cFound.Count;
            SelectedFile = "";
            for (i = 0; i < n; i++)
            {
                j = SortInx[i];
                cSorted.Add(cFound[j]);
            }
            dgvSearched.DataSource = cSorted;
            dgvSearched.Columns[1].HeaderText = "Mac#";
            SetDGVwidth();
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
            gbSelect.Visible = TotalMatches > 0;
            if(TotalMatches > 0)
            {
                AddSelButtons();
            }
            dgvSearched.RowEnter += dgvSearched_RowEnter;
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

        //https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?filter=includeTkbs&q=cm1415&include_tkbs=true&collapse_discussion=true
        private void HP_KB_find()
        {
            string s = tbKeywords.Text.Trim();
            if (s == "") return;
            //string strUrl = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=";
            string strUrl = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?filter=includeTkbs&include_tkbs=true&q=";            
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
            if (gbAlltSearch.Visible) SetFont(Reg10);
            else SetFont(Reg12);
        }


        private string PlusConCat(string t, List<string> ss)
        {
            string sRtn = t;
            int i = t.IndexOf('&');
            string sAppend = "";
            if(i != -1)
            {
                sAppend = t.Substring(i);
                sRtn = t.Substring(0,i);
            }
            foreach(string s in ss)
            {
                sRtn += s + "+";
            }
            t = sRtn.Substring(0, sRtn.Length - 1) + sAppend;
            return t;
        }

        private void RunB(string s, string t)
        {
            Utils.LocalBrowser(s + PlusConCat(t, keywords));
        }

        //https://www.ebay.com/sch/i.html?_nkw=hp+cm1415&_sacat=58058
        private void AltSearch(string sKey)
        {
            string s = "";
            string t = "";
            FormCandidateMacros(sKey);
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
                    RunB("https://www.youtube.com/@HPSupport/search?query=", "");
                    RunB("https://support.hp.com/us-en/deviceSearch?q=", "&origin=pdp");
                    break;
                case "EBA":
                    RunB("https://www.ebay.com/sch/i.html?_nkw=", "HP &_sacat=58058");
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
                case "HPYT":
                    RunB("https://www.youtube.com/@HPSupport/search?query=", "");
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

        private void btnHpYTsup_Click(object sender, EventArgs e)
        {
            AltSearch("HPYT");
        }

        private void btnMakeNew_Click(object sender, EventArgs e)
        {
            int r = lbNewItems.SelectedIndex;
            if (r < 0) return;
            string s = lbNewItems.Items[r].ToString();
            if (s == "") return;    // no space left
            NewItemID = s;
            NewItemName = tbKeywords.Text.Trim();
            this.Close();
        }

        private void btnShowCC_Click(object sender, EventArgs e)
        {
            string sLine = tbKeywords.Text.ToLower();
            int i = sLine.IndexOf("country ");
            bool b = ((i >= 0) && (sLine.Length > 10));
            btnShowCC.Visible = b;
            if (btnShowCC.Visible)
            {
                CountryLookup(sLine.Substring(i + 8));
            }
            lbKeyFound.Items.Clear();
            lbKeyFound.Items.Add("Double click to capture code");
            lbKeyFound.Items.AddRange(CountryCodeResults.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void lbKeyFound_DoubleClick(object sender, EventArgs e)
        {
            if(btnShowCC.Visible)
            {
                int index = lbKeyFound.SelectedIndex;
                if (index != ListBox.NoMatches)
                {
                    string sCode = lbKeyFound.Items[index].ToString();
                    Clipboard.SetText(sCode.Substring(0, 4));
                }
            }
        }
    }
}
