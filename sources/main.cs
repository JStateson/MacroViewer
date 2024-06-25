//#define SPECIAL2
//#define SPECIAL3
//#define SPECIAL4
using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Windows.Ink;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Windows.Media.Animation;
using System.Dynamic;
using System.Configuration;
using System.Security.Policy;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;


namespace MacroViewer
{
    public partial class main : Form
    {
        private bool bForceClose = false;
        private int NumInBody = 0;  // probably should have used a List<string> instead of [Utils.NumMacros]
        bool bHaveHTMLasLOCAL = false;      // if true then we just read in html 
        private bool bShowingError = false; // if true then highlighting errors between HP and local
        private bool bShowingDiff = false;  // if true then highlighting differences instead
        private bool bHaveBothHP = false; // have read both the HTML and the HPmacros for convenience uploading
        private bool bHaveBothHPerr = false; // as above but found errors
        private bool bHaveBothDIFF = false;  // HP and local have differences
        // if true then at least one of the HTML had an error and the corresponding HPmacros is fixed
        private string[] MacroErrors = new string[Utils.NumMacros];
        private string[] MacroNames = new string[Utils.NumMacros];
        private bool[] HTMLerr = new bool[Utils.NumMacros];   // if set then error at macro
        private bool[] HPerr = new bool[Utils.NumMacros];     // if set then error at macro
        private bool[] HP_HTML_NO_DIFF = new bool[Utils.NumMacros];   // if set then difference between them
        private int[] HP_HTML_DIF_LOC = new int[Utils.NumMacros]; // -1 is no dif. 0 is long and 1..x is first difference
        private int[] OriginalColor = new int[Utils.NumMacros];  //0:was OK to start with, 1:red, 2:blue
        // if HTMLerr set but HPerr is not set then can copy to clipboard with right click
        private bool[] bHPcorrected = new bool[Utils.NumMacros];
        private string aPage;
        private int[] StartMac = new int[Utils.NumMacros];
        private int[] StopMac = new int[Utils.NumMacros];
        private int[] MacBody = new int[Utils.NumMacros];
        private string[] Body = new string[Utils.NumMacros];
        private string[] Body_HP_HTML = new string[Utils.NumMacros]; //local copy of ORIGINAL HP macros
        private string[] Name_HP_HTML = new string[Utils.NumMacros]; //local copy of ORIGINAL HP macro names
        private string strType = "";    // either PRN or PC for printer or pc macros or HP 
        private string TXTName = "";
        private int CurrentRowSelected = -1;
        private OpenFileDialog ofd;
        private bool bMacroErrors;
        private bool bInitialLoad = false;  // this is used with the bad ending to look for
        private List<string> strBadEnding = new List<string>();
        private bool bHaveHTML = false; // html macro was read in. this cannot be edited
        private int NumSupplementalSignatures = 0;
        private Color NormalEditColor;
        public CMoveSpace cms;
        public List<CBody> cBodies;  // this is only updated when the program starts
        private string sBadMacroName;
        private string TextFromClipboardMNUs = "";
        private bool bTextFromClipboardMNUs; // can text be used as an argument to a search

        public main()
        {
            InitializeComponent();
            Utils.WhereExe = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            EnableMacEdits(false);
            gbManageImages.Enabled = true;// System.Diagnostics.Debugger.IsAttached;
            int iBrowser = Properties.Settings.Default.BrowserID;
            if (iBrowser < 0) Utils.BrowserWanted = Utils.eBrowserType.eEdge;
            else Utils.BrowserWanted = (Utils.eBrowserType)Properties.Settings.Default.BrowserID;
            Utils.VolunteerUserID = Properties.Settings.Default.UserID;
            Utils.nLongestExpectedURL = Properties.Settings.Default.LongestExpectedURL;
            string strFilename = Properties.Settings.Default.HTTP_HP;
            this.Text = " HP Macro Editor";
            settingsToolStripMenuItem.ForeColor = (Utils.CountImages() > 20) ? Color.Red : Color.Black;
            LoadAllFiles();
            cms = new CMoveSpace();
            Utils.bRecordUnscrubbedURLs = Properties.Settings.Default.SaveUnkUrls;
            SpecialUsed(Properties.Settings.Default.SpecialWord != "");
            NormalEditColor = btnCancelEdits.ForeColor;
            SetFGcolor("#FF6600");
            if (bForceClose)
            {
                timer1.Interval = 500;
            }
        }

        private string IgnoreSupSig(string s)
        {
            string strRtn = s;
            int i = s.IndexOf(Utils.SupSigPrefix);
            if(i>0)
            {
                strRtn =Utils.NoTrailingNL( s.Substring(0, i));
            }
            return strRtn.Trim();
        }

        private bool AnyHPdiff()
        {
            bool b = true;
            for (int i = 0; i < NumInBody; i++)
            {
                if (bHaveHTMLasLOCAL)
                {
                    HP_HTML_DIF_LOC[i] = Utils.FirstDifferenceIndex(IgnoreSupSig(Body[i]),Body_HP_HTML[i]);
                    HP_HTML_NO_DIFF[i] = (HP_HTML_DIF_LOC[i] == -1);    //(IgnoreSupSig(Body[i]) == Body_HP_HTML[i]);
                    HPerr[i] |= HP_HTML_NO_DIFF[i]; // jys todo TODO to do clean this up
                    b = b && HP_HTML_NO_DIFF[i];
                }
            }
            return !b;
        }
        private void LookForHTMLfix()
        {
            bool b = false;
            bool c = false;
            for (int i = 0; i < NumInBody; i++)
            {
                OriginalColor[i] = 0;
                bHPcorrected[i] = false;
                if (HTMLerr[i]) // there is an HTML error
                {
                    OriginalColor[i] = 1;
                    if (!HPerr[i])  // it was fixed here
                    {
                        b = true;
                        bHPcorrected[i] = true;
                        SetErrorBlue(i);
                        OriginalColor[i] = 2;
                    }
                }
                HTMLerr[i] |= HPerr[i];
                c |= HTMLerr[i];
                if (HTMLerr[i])
                {
                    if (OriginalColor[i] != 2)  //todo TODO to do need to clean this up
                        OriginalColor[i] = 1;
                }
            }
            bHaveBothHPerr = c;
            bShowingError = c;
            bShowingDiff = false;
            lbRCcopy.Visible = b;
            bHaveBothHP = true;
        }

        private void BackupHP_HTML()
        {
            for (int i = 0; i < NumInBody; i++)
            {
                Body_HP_HTML[i] = Body[i];
                Name_HP_HTML[i] = MacroNames[i];
            }
        }

        private void ShowHighlights()
        {
            if (bHaveBothHP)
            {
                if (bShowingError && bHaveBothHPerr)
                {
                    mShowDiff.Text = "Show Errors";
                    bShowingError = false;
                    bShowingDiff = true;
                    HighlightDIF();
                    return;
                }
                if (bShowingDiff && bHaveBothDIFF)
                {
                    mShowDiff.Text = "Show Diff";
                    bShowingError = true;
                    bShowingDiff = false;
                    //LookForHTMLfix();   // highlights errors TODO to do todo
                    RestoreColors();
                    return;
                }
                // there are no errors or no differences
                bShowingError = false;
                bShowingDiff = true;
                HighlightDIF();
            }
        }

        private void HighlightDIF()
        {
            for (int i = 0; i < NumInBody; i++)
            {
                if (HP_HTML_NO_DIFF[i])
                    SetDefaultCellColor(i);
                else SetErrorRed(i);
            }
        }

        private void RestoreColors()
        {
            for (int i = 0; i < NumInBody; i++)
            {
                switch (OriginalColor[i])
                {
                    case 0: SetDefaultCellColor(i);
                        break;
                    case 1: SetErrorRed(i);
                        break;
                    case 2: SetErrorBlue(i);
                        break;
                }

            }
        }

        private static string GetDownloadsPath()
        {
            if (Environment.OSVersion.Version.Major < 6) throw new NotSupportedException();
            IntPtr pathPtr = IntPtr.Zero;
            try
            {
                SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }


        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        private string GetLastFolder()
        {
            string LastFolder = Properties.Settings.Default.LastFolder;
            if (LastFolder == null || LastFolder == "")
            {
                LastFolder = GetDownloadsPath();
                if (!Directory.Exists(LastFolder))
                    LastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return LastFolder;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.html";
            ofd.InitialDirectory = GetLastFolder();
        }

        private void SetErrorRed(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Bold);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.Red;
        }

        private void SetErrorBlue(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Bold);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.Blue;
        }

        private void SetDefaultCellColor(int r)
        {
            lbName.Rows[r].Cells[0].Style.Font = new Font("Arial", 10, FontStyle.Regular);
            lbName.Rows[r].Cells[0].Style.ForeColor = Color.Black;
        }

        private bool FindBody()
        {

            string strFind; //<span class="html-attribute-value">profilemacro_2</span>"&gt;</span>
            string strEnd = "<span class=\"html-tag\">";
            int j, k, n;
            bMacroErrors = false;
            for (int i = 0; i < 30; i++)
            {
                strFind = "<span class=\"html-attribute-value\">profilemacro_" + (i + 1).ToString() + "</span>\"&gt;</span>";
                j = aPage.Substring(MacBody[i]).IndexOf(strFind);
                if (j < 0) return false;
                if (MacBody[i] == 0) continue;  // empty body is ok 
                n = MacBody[i] + j + strFind.Length;
                k = aPage.Substring(n).IndexOf(strEnd);
                if (k < 0) return false;
                string strBody = aPage.Substring(n, k);
                while (strBody.Contains("&amp;"))
                {
                    strBody = strBody.Replace("&amp;", "&");
                }

                strBody = strBody.Replace("&lt;", "<").Replace("&gt;", ">");
                strBody = Utils.RemoveNL(strBody.Replace("&nbsp;", " "));
                Body[i] = strBody;
                strFind = Utils.BBCparse(strBody);
                MacroErrors[i] = strFind;
                HTMLerr[i] = (strFind != "");
                if (HTMLerr[i])
                {
                    bMacroErrors = true;
                    SetErrorRed(i);
                }
            }
            mShowErr.Visible = bMacroErrors;
            bShowingError = bMacroErrors;
            return true;
        }

        private bool FindNames()
        {
            int j, k, n;
            int NumUsed = 0;
            string strName = "";
            string strFind = "<span class=\"html-attribute-name\">value</span>=\"<span class=\"html-attribute-value\">";
            lbName.Rows.Clear();
            tbNumMac.Text = "0";
            for (int i = 0; i < 30; i++)
            {
                j = aPage.Substring(StartMac[i]).IndexOf(strFind);
                if (j < 0) return false;
                n = StartMac[i] + j + strFind.Length;
                if (n > StopMac[i]) // must be empty
                {
                    strName = "";
                    MacBody[i] = 0;
                }
                else
                {
                    k = aPage.Substring(n).IndexOf("</span>");
                    if (k < 0) return false;
                    strName = aPage.Substring(n, k);
                    MacBody[i] = n + k + 1;
                    NumUsed++;
                }
                lbName.Rows.Add(i + 1, false, strName);
                MacroNames[i] = strName;
            }
            tbNumMac.Text = NumUsed.ToString();
            return true;
        }



        private bool FindMacros()
        {
            int j, k;
            NumInBody = 0;
            for (int i = 0; i < 30; i++)
            {
                string strFind = "Macro " + (i + 1).ToString();
                j = aPage.IndexOf(strFind);
                if (j < 0) return false;
                StartMac[i] = j;
                j += strFind.Length;
                k = aPage.Substring(j).IndexOf(strFind);
                if (k < 0) return false;
                StopMac[i] = k + j;
            }
            NumInBody = 30;
            return true;
        }

        private void ParsePage()
        {
            lbName.RowEnter -= lbName_RowEnter;
            FindMacros();
            FindNames();
            FindBody();
            lbName.RowEnter += lbName_RowEnter;
        }

        private bool ReadMacroHTML()
        {
            int nLength;
            string LastFolder = "";
            ofd.Filter = "HTML Files|macros.html";
            ofd.ShowDialog();
            string strFileName = ofd.FileName;
            if (!File.Exists(strFileName)) return false;
            LastFolder = Path.GetDirectoryName(ofd.FileName);
            Properties.Settings.Default.LastFolder = LastFolder;
            Properties.Settings.Default.HTTP_HP = strFileName;
            Properties.Settings.Default.Save();
            aPage = File.ReadAllText(strFileName);
            this.Text = " HP Macro Editor: " + strFileName;
            nLength = aPage.Length;
            ParsePage();
            return true;
        }

        private bool ReadLastHTTP()
        {
            string strFilename = Properties.Settings.Default.HTTP_HP;
            if (strFilename == null) return false;
            if (strFilename == "") return false;
            if(!File.Exists(strFilename))
            {
                MessageBox.Show("file " + strFilename + " cannot be found");
                return false;
            }
            aPage = File.ReadAllText(strFilename);
            if (aPage == null) return false;
            if (aPage.Length == 0) return false;
            ParsePage();
            BackupHP_HTML();
            return true;
        }

        private void RunBrowser()
        {
            string strTemp = tbBody.Text;
            if (strTemp == "") return;
            if(cbShowLang.Checked)
            {
                strTemp = Utils.AddLanguageOption(strTemp);
            }
            Utils.ShowPageInBrowser(strType, strTemp);
        }


        private void btnGo_Click(object sender, EventArgs e)
        {
            RunBrowser();
        }

        private void mHPload_Click(object sender, EventArgs e)
        {
            SelectFileItem("HP");
        }

        private void mAIOload_Click(object sender, EventArgs e)
        {
            SelectFileItem("AIO");
        }

        private void mDJload_Click(object sender, EventArgs e)
        {
            SelectFileItem("DJ");
        }

        private void mLJload_Click(object sender, EventArgs e)
        {
            SelectFileItem("LJ");
        }

        private void mPCload_Click(object sender, EventArgs e)
        {
            SelectFileItem("PC");
        }

        private void mOSload_Click(object sender, EventArgs e)
        {
            SelectFileItem("OS");
        }

        private void mnuNet_Click(object sender, EventArgs e)
        {
            SelectFileItem("NET");
        }


        /*
         * kb notebook forum (no search)
         * https://h30434.www3.hp.com/t5/Notebooks-Knowledge-Base/tkb-p/notebooks-knowledge-base
         */
        private void FormQueryKB(string sS)
        {
            if(!bTextFromClipboardMNUs)
            {
                FormGoToKB(sS);
                return;
            }
            string s = sS.Substring(0,1).ToLower();
            string w = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message";
            string f = w + "?filter=location&q=" + TextFromClipboardMNUs;
            string p = f + "&location=tkb-board:";
            string[] KBl = { "printers-knowledge-base", "desktop-knowledge-base",
                    "notebooks-knowledge-base","gaming-knowledge-base" };
            string sQ = "";
            switch (s)
            {
                case "p": sQ = p + KBl[0]; break;
                case "d": sQ = p + KBl[1]; break;
                case "n": sQ = p + KBl[2]; break;
                case "g": sQ = p + KBl[3]; break;
                case "a": sQ = w + "?filter=includeTkbs&include_tkbs=true&q=" + TextFromClipboardMNUs;
                break;
            }
            if(sQ != "")
                Utils.LocalBrowser(sQ);
        }

        private void mnuKnow(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                FormQueryKB(menuItem.Text);
            }
        }

        private void hPYouTubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sObj = Utils.ClipboardGetText().Replace(" ", "%20");
            Utils.LocalBrowser("https://www.youtube.com/@HPSupport/search?query=" + sObj);
        }

        // this was to look at the clipboard and see if there were keywords
        private string ClipExtractSearchID()
        {
            string str = TextFromClipboardMNUs;
            int n = str.Length;
            if (n > 20) return "";  // was not a keyword probably a bunch of junk or url
            string  str0 = "";
            char res;

            //remove any parens anywhere
            str0 = str.Replace("(", "").Replace(")", "");
            while (str0 != str)
            {
                str = str0;
                str0 = str.Replace("(", "").Replace(")", "");
            }

            //remove trailing periods or commas
            n = str.Length - 1;
            res = str[n];
            if (res == '.' || res == ',') str0 = str.Substring(0, n);

            while (str0 != str)
            {
                str = str0;
                n = str.Length - 1;
                res = str[n];
                if (res == '.' || res == ',') str0 = str.Substring(0, n);
            }
            str = str.Replace("HP", "");
            str = str.Replace("hp", "");
            return str;
        }

        private string GetSearchKeyword(string s)
        {
            if (!bTextFromClipboardMNUs) return "";
            string sObj = ClipExtractSearchID();
            if (s == "" || sObj == "") return "";
            return s + " " + sObj;
        }

        private void mnuDrvGoog_Click(object sender, EventArgs e)
        {
            string sObj = GetSearchKeyword("support.hp.com us-en drivers model series");
            if (sObj == "") return;
            sObj = sObj.Replace(" ", "+");
            Utils.LocalBrowser("https://google.com/search?q=" + sObj);
        }

        private void mnuDevCol_Click(object sender, EventArgs e)
        {
            string sObj = GetSearchKeyword("HP");
            if (sObj == "") return;
            Utils.LocalBrowser("https://us.driverscollection.com/Search/" + sObj.Replace(" ","%20"));
        }

        //https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=ipmmb-fm
        private void mnuSearchComm_Click(object sender, EventArgs e)
        {
            string s = "https://h30434.www3.hp.com/t5/forums/searchpage/tab/message?advanced=false&allow_punctuation=false&q=";
            if (!bTextFromClipboardMNUs)
                Utils.LocalBrowser("https://h30434.www3.hp.com");
            else
            {
                s += TextFromClipboardMNUs.Replace(" ","%20");
            }
            Utils.LocalBrowser(s);
        }

        private string sExtract(string s, string sPat)
        {
            int n = s.IndexOf(sPat);
            int m = sPat.Length;
            if (n < 0) return "";
            n += m;
            int i = s.IndexOf("&", n);
            if (i < 0) i = s.IndexOf("\\",n);
            if (i < 0) return "";
            if ((i - n) != 4) return "";
            return s.Substring(n, 4);
        }

        // "USB\\VID_2EF4&PID_5842&MI_00\\8&1AD5FA5&0&0000";
        // "PCI\VEN_1B21&DEV_2142&SUBSYS_87561043&REV_00\4&299AAA38&0&00E4"

        // devicehunt.com/search/type/usb/vendor/2EF4/device/5842
        private void mnuHuntDev_Click(object sender, EventArgs e)
        {
            string s = Utils.ClipboardGetText();
            string sType = "";
            if (s.Contains("USB")) sType = "/usb";
            if (s.Contains("PCI")) sType = "/pci";
            if (sType == "") return;
            string sVid = sExtract(s, "\\VID_");
            if (sVid == "") sVid = sExtract(s, "\\VEN_");
            if (sVid == "") return;
            string sPid = sExtract(s, "&PID_");
            if (sPid == "") sPid = sExtract(s, "&DEV_");
            if (sPid == "") return;
            s = "https://devicehunt.com/view/type/" + sType + "/vendor/" + sVid + "/device/" + sPid;
            Utils.LocalBrowser(s);
        }

        private void mnRecDis_Click(object sender, EventArgs e)
        {
            Utils.LocalBrowser("https://h30434.www3.hp.com/t5/custom/page/page-id/RecentDiscussions");
        }
        private void HaveSelected(bool bVal)
        {
            btnSaveM.Enabled = bVal;
            btnDelM.Enabled = bVal;
            btnDelChecked.Enabled = bVal;
        }

        private void MakeSticky(string s)
        {
            bool b = true;
            switch(s)
            {
                case "RF":
                    b = (CurrentRowSelected >= Utils.RequiredMacrosRF.Length);
                    break;
            }
            btnDelM.Enabled = b;
            btnDelChecked.Enabled = b;
        }

        private void CheckForLanguageOption(bool bRowChanged)
        {
            cbShowLang.Visible = tbBody.Text.Contains(Utils.sPossibleLanguageOption[0]);
            cbShowLang.Checked = !bRowChanged;
        }

        private void ShowUneditedRow(int e)
        {
            bool bChanged = (CurrentRowSelected != e);
            CurrentRowSelected = e;
            if (lbName.Rows.Count == 0 || e >= lbName.Rows.Count)
            {
                tbBody.Text = "";
                tbMacName.Text = "";
                return;
            }
            if(strType == "RF")
            {
                MakeSticky(strType);
            }
            else HaveSelected(true);
            ShowBodyFromSelected();
            tbMacName.Text = lbName.Rows[CurrentRowSelected].Cells[2].Value.ToString();
            lbName.ClearSelection();
            lbName.Rows[CurrentRowSelected].Selected = true;
            CheckForLanguageOption(bChanged);
            if (cbLaunchPage.Checked)
                RunBrowser();
        }

        private void ShowBodyFromSelected()
        {
            if (Body[CurrentRowSelected] == null)
                Body[CurrentRowSelected] = "";
            tbBody.Text = Body[CurrentRowSelected].Replace("<br>", Environment.NewLine);
        }

        private void ShowSelectedRow(int e)
        {
            bool bIgnore = false;
            if (bPageSaved(ref bIgnore))
            {
                ShowUneditedRow(e);
            }
            else lbName.Rows[CurrentRowSelected].Selected = true;
        }

        private void lbName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            RunBrowser();
        }



        private void btnClearEM_Click(object sender, EventArgs e)
        {
            tbBody.Clear();
        }

        private void CopyBodyToClipboard()
        {
            if (tbBody.Text == "") return;
            Clipboard.SetText(tbBody.Text.Replace(Environment.NewLine,"<br>")); 
        }

        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            CopyBodyToClipboard();
        }

        private string RemoveHeaders(string s)
        {
            string t = s.Replace("<!--StartFragment-->", "");
            int i = t.IndexOf("<body>");
            int j = t.IndexOf("</body>");
            if(i < 0 || j < 0)return "";
            i += 6;
            return t.Substring(i, j - i);
        }

        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            string strTemp = "";
            if (Clipboard.ContainsText(TextDataFormat.Html))
            {
                strTemp = RemoveHeaders(Clipboard.GetText(TextDataFormat.Html));
            }
            else if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                strTemp = Clipboard.GetText(TextDataFormat.Text);
                if (!strTemp.Contains(Environment.NewLine))
                {
                    strTemp = strTemp.Replace("\n", Environment.NewLine);
                }
            }
            tbBody.Text = strTemp.Replace("<br>", Environment.NewLine);
        }



        private void PutOnNotepad(string strIn)
        {
            CSendNotepad SendNotepad = new CSendNotepad();
            string npTitle = strIn;
            SendNotepad.PasteToNotepad(strIn);
        }

        private void btnToNotepad_Click(object sender, EventArgs e)
        {
            string s = tbMacName.Text + Environment.NewLine;
            PutOnNotepad(s + tbBody.Text);
        }

        // the below is never set true?: TODO to do todo
        private void AccessDiffBoth(bool b)
        {
            bHaveBothHP = b;
            bHaveHTMLasLOCAL = b;
        }

        // this is always called with false TODO 
        // HP and HTTP can show errors and differences but no other macros have that option
        private void SetVisDiffErr(bool b)
        {
            mShowDiff.Visible = b;
            mShowErr.Visible = b;
        }

        // can move macros from all files except the HTML one
        private void AllowMacroMove(bool b)
        {
            mMoveMacro.Visible = b;
            lbName.ReadOnly = !b;
        }

        private void LoadHTMLfile()
        {
            AccessDiffBoth(false);
            SetVisDiffErr(false);
            AllowMacroMove(false);
            lbRCcopy.Visible = false;
            if (ReadMacroHTML())
            {
                EnableMacEdits(false);
                strType = "";
                bHaveHTML = true;
                saveToXMLToolStripMenuItem.Enabled = true;
                ShowUneditedRow(0);
                AllowChanges(false);
                lbName.Columns[2].HeaderText = "Name HTML";
            }
            else AllowChanges(true);
        }

        private void AllowChanges(bool f)
        {
            btnNew.Enabled = f;
            tbMacName.Enabled = f;
            //gpMainEdit.Enabled = f;
            gbSupp.Enabled = f;
        }

        private void readHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHTMLfile();
        }

#if SPECIAL4
// this needs to check for a table followed by a width followed by NO image before the end of the table
        private string Has50noPic(string strIn)
        {
            string strRtn="";
            int i;
            i = strIn.IndexOf("width=\"50%\"");
            if (i >= 0)
            {
                strRtn += "possible '%50' problem at " + i.ToString() + Environment.NewLine;
            }
            return strRtn;
        }
#endif


        // HP and HTML can have blank macro names and body but NOT any others
        private int LoadFromTXT(string strFN)
        {
            int i = 0;
            bool bNoEmpty = !(strFN == "HP" || strFN == "" || strFN == "HTML");
            // 
            bMacroErrors = false;
            mShowErr.Visible = false;
            TXTName = strFN;
            strType = strFN;     //TODO todo to do this needs to be cleaned up
            gpMainEdit.Enabled = true;
            gbSupp.Enabled = true;
            string TXTmacName = Utils.FNtoPath(strFN);
            this.Text = " HP Macro Editor: " + TXTmacName;
            lbName.Rows.Clear();
            NumInBody = 0;
            if (File.Exists(TXTmacName))
            {
                StreamReader sr = new StreamReader(TXTmacName);
                string line = sr.ReadLine();
                string sBody;
                bHaveHTML = false;
                lbName.RowEnter -= lbName_RowEnter;
                while (line != null)
                {
                    lbName.Rows.Add(i + 1, false, line);   // this triggers row enter callback
                    MacroNames[i] = line;
                    if(bInitialLoad)
                    {
                        if(line.Contains(Utils.UnNamedMacro))
                        {
                            sBadMacroName +=
                                "Macro " + (i + 1).ToString() + " in " + strFN + " is un-named\r\n";
                        }
                    }
                    sBody = sr.ReadLine();
                    Body[i] = sBody;
                    sBody = Utils.BBCparse(sBody);
#if SPECIAL4
#endif
                    MacroErrors[i] = sBody;
                    HPerr[i] = (sBody != "");
                    if (HPerr[i])
                    {
                        bMacroErrors = true;
                        SetErrorRed(i);
                    }
                    mShowErr.Visible = bMacroErrors;
                    i++;
                    NumInBody++;
                    line = sr.ReadLine();
                    if(line == null)
                    {
                        break;
                    }
                    if(line == "")
                    {
                        // file  HP can have an empty macro unlike any others empty
                    }
                    if (line == "" & bNoEmpty)
                    {
                        if(bInitialLoad)
                        {
                            strBadEnding.Add(strFN);
                        }
                        break;  // if stop here then file has a trailing newline !!!
                    }
                }
                lbName.RowEnter += lbName_RowEnter;
                lbName.Columns[2].HeaderText = "Name: " + Utils.FNtoHeader(strFN);
                sr.Close();
            }
            tbNumMac.Text = i.ToString();
            btnNew.Enabled = lbName.RowCount < Utils.NumMacros;
            if (strFN == "HP") btnNew.Enabled = false;
            return i;
        }

        private void SaveAsTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            strType = strFN;    // TODO need to clean this up todo to do
            string TXTmacName = Utils.FNtoPath(strFN);
            foreach (DataGridViewRow row in lbName.Rows)
            {
                string strName = row.Cells[2].Value.ToString();
                string strBody = Body[i];
                strOut += strName + Environment.NewLine + strBody + Environment.NewLine;
                i++;
            }
            if(i > 0)Utils.WriteAllText(TXTmacName, strOut);
            else File.Delete(TXTmacName);
            NumInBody = i;
        }

        private void saveToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!bHaveHTML) return;
            DialogResult Res1 = MessageBox.Show("This will overwrite HPmacros",
                "Possible loss of macros", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Res1 == DialogResult.Yes)
            {
                SaveAsTXT("HP");
            }
        }


        private int RemoveMacro()
        {
            int j = CurrentRowSelected;
            if (j >= lbName.Rows.Count - 1) j = CurrentRowSelected - 1;
            if (j < 0) j = 0;
            for (int i = 0; i < lbName.Rows.Count - 1; i++)
            {
                if (i >= CurrentRowSelected)
                {
                    lbName.Rows[i].Cells[2].Value = lbName.Rows[i + 1].Cells[2].Value;
                    Body[i] = Body[i + 1];
                }
            }
            NumInBody--;
            lbName.Rows.RemoveAt(lbName.Rows.Count - 1);    // side effect causes a select which causes another warning
            HaveSelected(lbName.RowCount > 0);
            //NumInBody--; cannot go here as the test will fail
            return j;
        }

        private void EnableMacEdits(bool enable)
        {
            btnDelM.Enabled = enable && CurrentRowSelected >= 0;
            btnSaveM.Enabled = enable && CurrentRowSelected >= 0;
            btnDelChecked.Enabled = enable && CurrentRowSelected >= 0;
        }


        private void btnDelM_Click(object sender, EventArgs e)
        {
            string strName = tbMacName.Text;
            int i = CurrentRowSelected + 1;
            string strN = "";
            if (strType != "HP")
            {
                strN = (strName == "") ? "You are deleting an unnamed macro" :
                    "You are deleting the macro named " + strName;
                DialogResult Res1 = MessageBox.Show(strN, "Deleting  macro " + i.ToString(),
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
                CurrentRowSelected = RemoveMacro();
            }
            else
            {
                strN = (strName == "") ? "You are removing the contents of an unnamed macro" :
                    "You are removing contents of the macro named " + strName;
                DialogResult Res1 = MessageBox.Show(strN, "Deleting  macro " + i.ToString(),
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
                Body[CurrentRowSelected] = "";
                lbName.Rows[CurrentRowSelected].Cells[2].Value = "";
                tbBody.Text = "";
            }
            MustFinishEdit(true);
            ReSaveAsTXT(TXTName);
            ShowUneditedRow(CurrentRowSelected);
        }

        private void ReSaveAsTXT(string TXTName)
        {
            SaveAsTXT(TXTName);
            LoadFromTXT(TXTName);
        }

        private bool FailsHTMLparse()
        {
            if (HasBadUrl()) return true;
            if (strType == "NO" || strType == "RF") return false;
            return SyntaxTest();
        }

        private bool NoEmptyMacros()
        {
            int i = 0;
            tbMacName.Text = tbMacName.Text.Trim();
            if (tbMacName.Text == "")
            {
                tbMacName.Text = Utils.UnNamedMacro;
                i++;
            }
            tbBody.Text = tbBody.Text.Trim();
            if (tbBody.Text == "")
            {
                tbBody.Text = Utils.UnNamedMacro;
                i++;
            }
            return i == 2;  // both items blank
        }

        private void SaveCurrentMacros()
        {
            bool bChanged = false;
            NoEmptyMacros();
            string strName = tbMacName.Text;
            string strOld = "";
            if (lbName.RowCount == 0)
            {
                return; // must have wanted to add a row: sorry
            }
            if (FailsHTMLparse())
            {
                return;
            }
            strOld = lbName.Rows[CurrentRowSelected].Cells[2].Value.ToString();
            if (strName != strOld && (Utils.UnNamedMacro != strOld))
            {
                DialogResult Res1 = MessageBox.Show("This will overwrite " + strOld + " with " + strName,
        "Replaceing a macro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
            }
            lbName.Rows[CurrentRowSelected].Cells[2].Value = strName;
            Body[CurrentRowSelected] = RemoveNewLine(ref bChanged, Utils.NoTrailingNL(tbBody.Text).Trim());
            if (TXTName == "HP")
            {
                SaveAsTXT(TXTName);
                ReloadHP(CurrentRowSelected);
            }
            else
            {
                ReSaveAsTXT(TXTName);
            }
            ShowUneditedRow(CurrentRowSelected);
        }

        private void btnSaveM_Click(object sender, EventArgs e)
        {
            if(NoEmptyMacros())
            {
                MessageBox.Show("You cannot save an empty macro");
                return;
            }
            SaveCurrentMacros();
            MustFinishEdit(true);
        }

        private string RemoveNewLine(ref bool bChanged, string strIn)
        {
            string strOut = Utils.RemoveNL(strIn);
            bChanged = (strOut.Length != strIn.Length);
            return strOut;
        }



        private bool AddNew(string strNewName, string strBody)
        {
            bool bChanged = false;
            if (lbName.Rows.Count == Utils.NumMacros)
            {
                MessageBox.Show("Can only hold " + Utils.NumMacros.ToString() + " macros");
                return false;
            }
            if (strNewName == "")
            {
                MessageBox.Show("Must have a name for the macro");
                return false;
            }
            for (int i = 0; i < lbName.Rows.Count; i++)
            {
                if (lbName.Rows[i].Cells[2].Value.ToString() == strNewName)
                {
                    MessageBox.Show("Macro " + (i + 1).ToString() + " name must be unique!");
                    return false;
                }
            }
            if (CurrentRowSelected >= 0 && CurrentRowSelected < lbName.Rows.Count)
                lbName.Rows[CurrentRowSelected].Selected = false;
            CurrentRowSelected = lbName.Rows.Count;
            lbName.Rows.Add(CurrentRowSelected + 1, false, strNewName);
            if(Utils.IsNewPRN(TXTName))
            {
                string sNB =
                    "You may need to reset the printer: video here" + Utils.nBR(3) +
                    "WiFi setup to router:  video here" + Utils.nBR(3) +
                    "For WiFi direct setup click here.<br>This is useful if there is no modem " + Utils.nBR(3) +
                    "Simple push button or WPS setup is described on page xx of Users Manual" + Utils.nBR(3) +
                    "Full feature software DEVICE MONTH YEAR" + Utils.nBR(3) +
                    "Printer Reference";
                strBody = sNB;
            }
            Body[CurrentRowSelected] = RemoveNewLine(ref bChanged, strBody);
            tbBody.Text = strBody.Replace("<br>",Environment.NewLine);
            ReSaveAsTXT(TXTName);
            HaveSelected(true);
            lbName.Rows[CurrentRowSelected].Selected = true;
            tbMacName.Text = strNewName;
            tbNumMac.Text = lbName.Rows.Count.ToString();
            EnableMacEdits(true);
            return true;
        }



        private int ReloadHP(int r)
        {
            int iCnt = 0;
            mShowDiff.Visible = false;
            lbRCcopy.Visible = false;
            bHaveHTMLasLOCAL = ReadLastHTTP();
            ShowUneditedRow(r);
            strType = "HP";
            iCnt = LoadFromTXT(strType);
            if (bHaveHTMLasLOCAL)
            {
                LookForHTMLfix();
                bHaveBothDIFF = AnyHPdiff();
                mShowDiff.Visible = bHaveBothDIFF;
                lbRCcopy.Visible = bHaveBothDIFF;
            }
            ShowUneditedRow(r);
            EnableMacEdits(true);
            return iCnt;
        }


        private void ReplaceText(int iStart, int iLen, string strText)
        {
            string sPrefix = tbBody.Text.Substring(0, iStart);
            string sSuffix = tbBody.Text.Substring(iStart + iLen);
            tbBody.Text = sPrefix + strText + sSuffix;
        }

        private void btnSetObj_Click(object sender, EventArgs e)
        {
            string strReturn = "";
            bool bHaveHTML = false;
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            string sTemp = tbBody.Text;
            string strRaw = Utils.AdjustNoTrim(ref i, ref j, ref sTemp);
            string strUC = strRaw.ToUpper();
            bHaveHTML = strUC.Contains("HTTPS:") || strUC.Contains("HTTP:");
            if (bHaveHTML)
            {
                if (j < 12) return; // http://a.com is smallest
                LinkObject MyLO = new LinkObject(strRaw);
                MyLO.ShowDialog();
                strReturn = MyLO.strResultOut;
                if (strReturn == null) return;
                if (strReturn == "") return;
                ReplaceText(i, j, strReturn);
                MyLO.Dispose();
            }
            else
            {
                SetText MySET = new SetText(strRaw);
                MySET.ShowDialog();
                strReturn = MySET.strResultOut;
                if (strReturn == null) return;
                if (strReturn == "") return;
                if (j > 0)
                {
                    ReplaceText(i, j, strReturn);
                }
                else
                {
                    tbBody.Text = tbBody.Text.Insert(i, strReturn);
                }
                MySET.Dispose();
            }
        }

        private string AppendDash(string s, int n)
        {
            int i = n - s.Length;
            return s + string.Concat(Enumerable.Repeat('-', i));
        }

        private string GetReference()
        {
            string sRtn = "";
            int i = 0;
            foreach(string s in Utils.LocalMacroPrefix)
            {
                if(s == strType)
                {
                    string t = Utils.LocalMacroRefs[i];
                    if (t == "") break;
                    return string.Concat(Enumerable.Repeat(Environment.NewLine, 8)) + t;
                }
                i++;
            }
            return sRtn;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bool bIgnore = false;
            if (bPageSaved(ref bIgnore))
            {
                AddNew(Utils.UnNamedMacro, GetReference());
            }
        }

        private void MustFinishEdit(bool bFinished)
        {
            if(bFinished)
            {
                btnCancelEdits.ForeColor = NormalEditColor;
                btnSaveM.ForeColor = NormalEditColor;
                btnDelM.ForeColor = NormalEditColor;
            }
            else
            {
                btnCancelEdits.ForeColor = Color.Red;
                btnSaveM.ForeColor = Color.Red;
                btnDelM.ForeColor = Color.Red;  
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        //need a copy of the edit box contents in full markup
        private string tbBodyMarked()
       {
            //string sBody = tbBody.Text.Trim().Replace(Environment.NewLine, "<br>");
            string sBody = tbBody.Text.Replace(Environment.NewLine, "<br>");
            return sBody;
        }


        //?? if NumInBody is equal to CurrentRowSelected then the we must have deleted the row??
        // this side effect happened
        private bool bNothingToSave()
        {
            if (CurrentRowSelected < 0 || strType == "" || NumInBody == 0 || NumInBody == CurrentRowSelected)
            {
                // if row count is 0 then a new macro file and user should have saved: sorry
                return true; // nothing to save 
            }
            if (Body[CurrentRowSelected] == null)
            {
                if (tbBody.Text.Trim().Length > 0) return false;
                return true; // a leftover "Change Me" has empty body
            }
            bool bEdited = (tbBodyMarked() != Body[CurrentRowSelected]);
            return !bEdited;
        }

        private bool bPageSaved(ref bool bIgnore)
        {
            string sMsg = "Macro was not saved\r\nEither save, cancel edits or ignore";
            bIgnore = false;
            if(tbMacName.Text.Trim() == "")
            {
                NoEmptyMacros();
                sMsg = "Un-named macro not saved, using " + Utils.UnNamedMacro;
            }
            if (bNothingToSave()) return true;
            MustFinishEdit(false);
            DialogResult Res1 = MessageBox.Show(sMsg, "Click NO to ignore", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (Res1 == DialogResult.No) // problem with clearing dgv when saving current macro
            {   // error was "operation cannot be performed in this event handler"
                //SaveCurrentMacros();  // cannot do this as it was called from a row change
                //return true;
                bIgnore = true;
                return false; // need to fix the problem of leading newlines
            }
            return false;
        }

        private void btnChangeUrls_Click(object sender, EventArgs e)
        {
            ManageMacros MyManageMac = new ManageMacros(strType, Utils.NumMacros, ref Body);
            MyManageMac.ShowDialog();
            tbBody.Text = MyManageMac.AllBody[CurrentRowSelected];
            SaveCurrentMacros();
            MyManageMac.Dispose();
        }

        private void btnAppendMac_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            CreateMacro MyCM = new CreateMacro(strType);
            MyCM.ShowDialog();
            string strReturn = MyCM.strResultOut;
            if (strReturn == null) return;
            if (strReturn == "") return;
            string s1 = tbBody.Text.Substring(0, i) + "<br>";
            string s2 = "<br>" + tbBody.Text.Substring(i);
            tbBody.Text = s1 + strReturn + s2;
        }

        private void downloadMacrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //https://h30434.www3.hp.com/t5/user/myprofilepage/tab/user-macros
            string UserMacs = "https://h30434.www3.hp.com/t5/user/myprofilepage/tab/user-macros";
            Utils.LocalBrowser(UserMacs);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bMustExit = false;
            Settings MySettings = new Settings(Utils.BrowserWanted, Utils.VolunteerUserID, NumSupplementalSignatures);
            MySettings.ShowDialog();
            Utils.BrowserWanted = MySettings.eBrowser;
            Utils.VolunteerUserID = MySettings.userid;
            bMustExit = MySettings.bWantsExit;
            MySettings.Dispose();
            if (bMustExit) this.Close();
        }


        private void helpWithUtilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("UTILS");
        }

        private void helpWithWebSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("WEB");
        }

        private void managingImagesHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("MANAGE");
        }

        private void helpWithFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("FILE");
        }

        private void helpWithSignaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("SIG");
        }

        private void helpWithSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("SEARCH");
        }

        private void ShowHelp(string sHelp)
        {
            Utils.WordpadHelp(sHelp);
        }

        private void helpWithEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("EDIT");
        }

        private void EDITLINKHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("EDITLINK");
        }

        private void helpWithErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp("XMLERRORS");
        }


        private void lbName_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (strType != "HP") return;
                int r = e.RowIndex;
                if (r == -1) return;
                if (!HP_HTML_NO_DIFF[r])
                {
                    string sWhereErr = "";
                    if (HP_HTML_DIF_LOC[r] == 0)
                    {
                        sWhereErr = " Diff is at end (or empty)";
                    }
                    else if (HP_HTML_DIF_LOC[r] > 0)
                    {
                        sWhereErr = " Diff at char " + HP_HTML_DIF_LOC[r].ToString();
                    }
                    string s ="Original Macro " + (r+1).ToString() + ": '" + Name_HP_HTML[r] + "'" + sWhereErr + Environment.NewLine + Body_HP_HTML[r];
                    PutOnNotepad(s);
                }
            }
        }

        //copy and pasting from a reply can sometimes get a ... instead of the full url
        private bool HasBadUrl()
        {
            if (tbBody.Text.Contains("..."))
            {
                DialogResult Res1 = MessageBox.Show("There is a '...' in the URL.  Click OK to ignore", "Possibly bad URL", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Res1 == DialogResult.OK)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        private bool SyntaxTest()
        {
            string strErr = Utils.BBCparse(tbBody.Text);
            if (strErr == "") return false;
            DialogResult Res1 = MessageBox.Show(strErr, "Click OK to see where errors are", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (Res1 == DialogResult.OK)
            {
                Utils.ShowParseLocationErrors(tbBody.Text);
                MessageBox.Show(strErr, "Errors are near locations listed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }
      

        private void lbName_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ShowSelectedRow(e.RowIndex);
        }


        private void btnCancelEdits_Click(object sender, EventArgs e)
        {
            ShowBodyFromSelected();
            SetFGcolor("#FF6600");
            tbColorCode.ForeColor = ColorTranslator.FromHtml(tbColorCode.Text.ToString());
            MustFinishEdit(true);
        }

        private void btnLinkAll_Click(object sender, EventArgs e)
        {
            string sBody = tbBody.Text.ToLower();
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            string strRaw = Utils.AdjustNoTrim(ref i, ref j, ref sBody);
            bool bHasHyper = sBody.Contains("<a ") || sBody.Contains("<img ");
            if (!bHasHyper) // do not want to unlink urls
            {
                if(j == 0)
                {
                    sBody = tbBody.Text;
                    Utils.ReplaceUrls(ref sBody, true);
                    tbBody.Text = sBody;
                    return;
                }
            }
            // see if there is a range to link

            if (j < 12) return; // http://a.com is smallest
            sBody = strRaw.ToLower();
            bHasHyper = sBody.Contains("<a ") || sBody.Contains("<img ");
            if (bHasHyper) return;  // do not want to link anything twice
            Utils.ReplaceUrls(ref strRaw, true);
            ReplaceText(i, j, strRaw);
        }

        // This code will run when Ctrl+V is pressed.  if url selected then option to make a hyperlink
        private void tbBody_KeyDown(object sender, KeyEventArgs e)
        {

            int i, j, k;
            string sBody, sFromCB, sL;
            string sText, sS, sE;
            if (e.Control && e.KeyCode == Keys.V)
            {
                sBody = tbBody.Text.ToLower();
                i = tbBody.SelectionStart;
                j = tbBody.SelectionLength;
                if (j == 0) return;
                sFromCB = Clipboard.GetText();
                sL = sFromCB.ToLower();
                k = sL.IndexOf("http");
                if (k != 0) return;
                sText = Utils.AdjustNoTrim(ref i, ref j, ref sBody);
                string sOut = Utils.FormUrl(sFromCB, sText);
                e.Handled = true;
                e.SuppressKeyPress = true;
                /* the following DID NOT WORK!!!!

                 DialogResult dr = MessageBox.Show("Make text into hyperlink? click Yes else Cancel", sText, MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                {
                    e.Handled = false;
                    e.SuppressKeyPress = false;
                    return;
                }
                */
                sS = sBody.Substring(0, i);
                sE = sBody.Substring(i + j);
                tbBody.Text = sS + sOut + sE;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            PositionNext(-1);
        }

        private void PositionNext(int iDir)
        {
            int n = Utils.LocalMacroPrefix.Length;
            int i = 0;
            if (strType == "")
            {
                SelectFileItem("PC");
                return;
            }
            i = Array.IndexOf(Utils.LocalMacroPrefix, strType);
            i += iDir; ;
            if (i < 0) i = n - 1;
            if (i == n) i = 0;
            n = SelectFileItem(Utils.LocalMacroPrefix[i]);
            if (n > 0) lbName.Focus();
        }

        // find next file PC->PRN->HP and repeat back to PC
        private void btnNextTable_Click(object sender, EventArgs e)
        {
            PositionNext(1);
        }

        private int WarnMissing(string sFN)
        {
            string sOut = "";
            string TXTmacName = Utils.FNtoPath(sFN);
            DialogResult dr = MessageBox.Show("Reference Macro RFmacros.txt is missing\r\nClick YES to create placeholders or NO to quit",
                "WARNING",MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                bForceClose = true;
                return -1;
            }
            foreach (string s in Utils.RequiredMacrosRF)
            {
                sOut += s + Environment.NewLine + "Content is missing" + Environment.NewLine;
            }
            Utils.WriteAllText(TXTmacName, sOut);
            return Utils.RequiredMacrosRF.Length;
        }

        private int LoadFileItem(string sPrefix)
        {
            int n = LoadFromTXT(sPrefix);
            if(sPrefix != "HP")
            {
                SetVisDiffErr(false);
            }
            if(n == 0 && sPrefix == "RF")
            {
                n = WarnMissing(sPrefix);
                if(n > 0) return LoadFromTXT(sPrefix);
                return 0;
            }
            return n;
        }

        private void ShowEmpty(string sWanted)
        {
            lbName.Rows.Clear();
            for (int i = 0; i < Utils.NumMacros; i++) Body[i] = "";
            tbBody.Text = "";
            tbMacName.Text = "";
            strType = sWanted;
            this.Text = " HP Macro Editor";
            lbName.Columns[2].HeaderText = "Name: " + Utils.FNtoHeader(sWanted);
            NumInBody = 0;
            btnSaveM.Enabled = false;
            btnNew.Enabled = true; // allow to be created
        }

        private int SelectFileItem(string sPrefix)
        {
            bool bIgnore = false;
            int iCnt = 0;
            if (strType != sPrefix)
            {
                if (!bPageSaved(ref bIgnore))
                {
                    return 0; // user failed to save edits
                }
            }
            strType = sPrefix;
            TXTName = sPrefix;
            if (Utils.NoFileThere(sPrefix))
            {
                ShowEmpty(sPrefix);
                return 0;
            }
            btnSaveM.Enabled = true;
            lbRCcopy.Visible = false;
            mMoveMacro.Visible = true;
            lbName.ReadOnly = false;

            if(sPrefix == "HP")
            {
                iCnt = ReloadHP(0);
            }
            else
            {
                AccessDiffBoth(false);
                iCnt = LoadFromTXT(strType);
                EnableMacEdits(true);
                ShowUneditedRow(0);
                SetVisDiffErr(false);
            }
            AllowMacroMove(true);
            return iCnt;
        }

        private void mShowDiff_Click(object sender, EventArgs e)
        {
            if (bHaveBothHP)
            {
                ShowHighlights();
            }
        }

        private void mShowErr_Click(object sender, EventArgs e)
        {
            ShowErrors MySE = new ShowErrors(ref MacroNames, ref MacroErrors, ref Body);
            MySE.Show();
        }

        private void btnCleanUrl_Click(object sender, EventArgs e)
        {
            string sDirty = Utils.ClipboardGetText();
            if (sDirty == "") return;
            Utils.ReplaceUrls(ref sDirty,false);
            Clipboard.SetText(sDirty);
        }


        private void LoadAllFiles()
        {
            bInitialLoad = true;
            sBadMacroName = "";
            if (cBodies == null)
            {
                bHaveHTMLasLOCAL = ReadLastHTTP();
                cBodies = new List<CBody>();

                foreach (string s in Utils.LocalMacroPrefix)
                {
                    int i, n = LoadFileItem(s);
                    for (i = 0; i < n; i++)
                    {
                        CBody cb = new CBody();
                        cb.File = s;
                        cb.Number = (i + 1).ToString();
                        cb.Name = lbName.Rows[i].Cells[2].Value.ToString();
#if SPECIAL2
                        bDebug |= RunBorderFix(s, i+1, cb.Name, ref Body[i]);
#endif
#if SPECIAL3
                        bDebug |= RunLookMissingTR(s, i + 1, cb.Name, ref Body[i]);
#endif

                        cb.sBody = Body[i];
                        cb.fKeys = "";
                        cBodies.Add(cb);
                    }
                    if (n != 0) strType = s;
                }
            }

            bInitialLoad = false;
            if(sBadMacroName != "")
            {
                MessageBox.Show(sBadMacroName, "Bad macro names");
            }
            if(strBadEnding.Count > 0)
            {
                string strNames = "";
                foreach(string s in strBadEnding)
                {
                    strNames += (s + " ");
                }
                MessageBox.Show("One or more files have trailing newline and will be re-written\r\nIf this happens repeatedly please create an issue",
                    strNames, MessageBoxButtons.OK);
                ReWriteBadFiles();
            }
            if (strType != "") lbName.ReadOnly = false;                
        }

        private void ReWriteBadFiles()
        {
            foreach(string strFN in strBadEnding)
            {
                string strOut = "";
                bool bFound = false;
                foreach(CBody cb in cBodies)
                {
                    if(cb.File == strFN)
                    {
                        if (bFound) strOut += Environment.NewLine;
                        strOut += cb.Name + Environment.NewLine;
                        strOut += cb.sBody;
                        bFound = true;
                    }
                }
                if(strOut != "") // probably should assert this
                {
                    File.WriteAllText(Utils.FNtoPath(strFN), strOut);
                }
            }
        }

        private void RaiseSearch()
        {
            bool bFinishedEdits = bNothingToSave();
            WordSearch ws = new WordSearch(ref cBodies, bFinishedEdits);            
            ws.ShowDialog();
            int i, n = ws.LastViewed;
            string NewID = ws.NewItemID;
            string NewName = ws.NewItemName;
            ws.Dispose();
            if(n >= 0 && bFinishedEdits)
            {
                CBody cb = cBodies[n];
                LoadFromTXT(cb.File);
                i = Convert.ToInt32(cb.Number);
                ShowUneditedRow(i - 1);
            }
            if(NewID != "" &&bFinishedEdits)
            {
                n = LoadFromTXT(NewID);
                if (n < Utils.NumMacros)
                {
                    AddNew(NewName, GetReference());
                }
            }
        }

        private void WordSearch_Click(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        private int CountChecks()
        {
            int n = 0;
            int i = 0;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                row.Cells[1].Value = row.Cells[1].EditedFormattedValue;
                if (strType == "RF")
                {
                    if (i < Utils.RequiredMacrosRF.Length)
                    {
                        row.Cells[1].Value = false;                        
                    }
                    else
                    {
                        if ((bool)row.Cells[1].Value)
                        {
                            n++;
                        }
                    }
                    i++;
                }
                else if((bool)row.Cells[1].Value) n++ ;
            }
            return n;
        }


        private void AppendTheseRows(CMoveSpace cms)
        {
            string strAdded = "";
            int i = -1;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                bool bWantSelect = ((bool)row.Cells[1].Value) || ((bool)row.Cells[1].EditedFormattedValue);
                i++;
                if (bWantSelect)
                {
                    strAdded += row.Cells[2].Value.ToString() + Environment.NewLine;
                    strAdded += Body[i] + Environment.NewLine;
                    row.Cells[1].Value = true;
                }
            }
            Utils.FileAppendText(cms.strDes, strAdded);  // has a pair of newlines at end
        }

        private void InsertTheseRows(CMoveSpace cms)
        {
            List<CNewMac> cb = new List<CNewMac>();
            string[] HPsaved;
            string strOut = "";
            string strLoc = "";
            int i = -1;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                bool bWantSelect = ((bool)row.Cells[1].Value) || ((bool)row.Cells[1].EditedFormattedValue);
                i++;
                if (bWantSelect)
                {
                    CNewMac newMac = new CNewMac();
                    newMac.AddNB(row.Cells[2].Value.ToString(), Body[i]);   // no newlines as added later
                    row.Cells[1].Value = true;
                    cb.Add(newMac);
                }
            }
            // look through the HP file for a place to put them
            strLoc = Utils.FNtoPath("HP");
            HPsaved = File.ReadAllLines(strLoc);
            i = -1;
            foreach(string s in HPsaved)
            {
                i++;
                if(s == "")
                {
                    if (cb.Count != 0)
                    {
                        HPsaved[i] = cb[0].sName;
                        string t = cb[0].sBody;
                        HPsaved[i + 1] = (t == "") ? "Body Missing" : t;
                        cb.RemoveAt(0);
                    }
                }
                strOut += HPsaved[i] + Environment.NewLine;
            }
            Utils.WriteAllText(strLoc,Utils.NoTrailingNL(strOut));
        }

        private void PerformMove(CMoveSpace cms)
        {
            int i = -1;

            if(!cms.bDelete)        // need to move them, not just delete them
           {
                if(cms.strDes != "HP")
                {
                    AppendTheseRows(cms);
                }
                else
                {
                    InsertTheseRows(cms);
                }
            }

            //handle the ones left over.  if HP then just blank out the body and save
            //else replace the disk file and reload
            if (cms.strType == "HP")
            {
                foreach (DataGridViewRow row in lbName.Rows)
                {
                    i++;
                    if ((bool)row.Cells[1].EditedFormattedValue)
                    {
                        Body[i] = "";
                        row.Cells[2].Value = "";
                        row.Cells[1].Value = false;
                    }
                }
                SaveAsTXT(cms.strType);
                return;
            }
            SaveWantedMacros(cms.strType);
        }

        // the ones unchecked are to be saved
        private void SaveWantedMacros(string strType)
        {
            string strAdded = "";
            string strPath = Utils.FNtoPath(strType);
            int i = -1;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                i++;
                bool bWantDelete = (bool)row.Cells[1].Value; // || ((bool)row.Cells[1].EditedFormattedValue);
                if (!bWantDelete) 
                {
                    strAdded += row.Cells[2].Value.ToString() + Environment.NewLine;
                    strAdded += Body[i] + Environment.NewLine;
                }
                else
                {
                    row.Cells[1].Value = false;
                }
            }
            if (strAdded != "")
            {
                Utils.WriteAllText(strPath, strAdded);
                LoadFromTXT(strType);
                NumInBody = i+1;
            }
            else
            {
                File.Delete(strPath);
                lbName.Rows.Clear();
                NumInBody = 0;
            }
            ShowUneditedRow(0);
        }


        private void mMoveMacro_Click(object sender, EventArgs e)
        {
            cms.Init(); 
            cms.strType = strType;
            cms.bRun = false;
            cms.bDelete = false;

            cms.nChecked = CountChecks();
            MoveMacro mm = new MoveMacro(ref cms);
            mm.ShowDialog();
            mm.Dispose();
            if (cms.bRun && cms.nChecked > 0)
            {
                PerformMove(cms);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        private void btnDelChecked_Click(object sender, EventArgs e)
        {
            if(CountChecks() == 0)return;
            CMoveSpace cms = new CMoveSpace();
            cms.strType = strType;
            cms.bDelete = true;
            PerformMove(cms);
        }

        private void mnuLCnT_Click(object sender, EventArgs e)
        {
            utils MyUtils = new utils();
            MyUtils.Show();
        }

        private void mnuRemoveLocalImgs_Click(object sender, EventArgs e)
        {
            RemoveImages ri = new RemoveImages();
            ri.ShowDialog();
            ri.Dispose();
        }

        private void SpecialUsed(bool b)
        {
            btnSpecialWord.Visible = b;
            timer1.Enabled = b;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            SpecialUsed(false);
            if (bForceClose)
            {
                this.Close();
            }

        }

        private void btnSpecialWord_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Properties.Settings.Default.SpecialWord);
            SpecialUsed(false);
        }

        private void main_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Utils.WordpadHelp("FILE");
        }


        private void btnBold_Click(object sender, EventArgs e)
        {
            Utils.AddBold(ref tbBody);
        }

        private void btnClipToUpper_Click(object sender, EventArgs e)
        {
            string s = Utils.ClipboardGetText().ToUpper();
            Clipboard.SetText(s);
        }

        private void btnToLower_Click(object sender, EventArgs e)
        {
            string s = Utils.ClipboardGetText().ToLower();
            Clipboard.SetText(s);
        }

        // this shows the difference if url is cleaned
        private void btnCleanPaste_Click(object sender, EventArgs e)
        {
            int i, j;
            string sOut = "";
            string sDirty = Utils.ClipboardGetText();
            if (sDirty == null) return;
            string s = sDirty.ToUpper();
            if (!(s.Contains("HTTPS:") || s.Contains("HTTP:"))) return;
            i = sDirty.Length;
            sOut = AppendDash("Before cleaning",40) + Environment.NewLine + sDirty + Environment.NewLine;   
            Utils.ReplaceUrls(ref sDirty, false);
            j = sDirty.Length;
            sOut += AppendDash("After cleaning",40) + Environment.NewLine + sDirty + Environment.NewLine;
            sOut += (i == j) ? "No difference" : "\r\nShortened " + (i - j).ToString() + " characters";
            PutOnNotepad(sOut);
        }

// some <tr> are missing the required <tr><td> and cannot be used in forum macro
// this seems to be a requirement to HP forums and is not an HTML requirement 
// the program fixes it so that any macros can be used at in the HP forum
// this would not have to be done if I knew about the problem before I coded anything
#if SPECIAL3
        private bool RunLookMissingTR(string sType, int i, string mName, ref string sIn)
        {
            int n = sIn.IndexOf("<td></td>");
            int m = 0;
            if(n > 0)
            {
                m = 1;
                sIn = sIn.Replace("<td></td>", "<td>&nbsp;</td>");
            }
            string s = sIn;
            sIn = s;
            n = m + LookMissing(ref sIn, 0);
            if (n > 0)
            {
                using (StreamWriter writer = File.AppendText(Utils.WhereExe + "\\LookedMissing.txt"))
                {
                    writer.WriteLine(sType + " " + i.ToString() + " " + mName + " " + n.ToString());
                }
            }
            return n > 0;
        }

        private int LookMissing(ref string sIn, int n)
        {
            string s1, s2, s3;
            int k;
            if (n >= sIn.Length) return 0;
            int i = sIn.IndexOf("<table", n); //6
            if (i < 0) return 0;
            int j = sIn.IndexOf("</table>", i); //8
            if (j < 0) return 0;
            string s = sIn.Substring(i, 8 + j - i);
            n += (j + 8);
            if (s.Contains("<tr>"))return LookMissing(ref sIn, n);
            k = sIn.IndexOf("<td>", i);
            Debug.Assert((i < k) && (k < j));
            s1 = sIn.Substring(0, i);
            k = s.IndexOf("<td>");  // should be first one
            s3 = sIn.Substring(j + 8);
            s2 = s.Substring(0, k) + "<tr>" + s.Substring(k); // 4
            s2 = s2.Replace("</table>", "</tr></table>"); //5
            sIn = s1 + s2 + s3;
            n += 9;// (9 + s2.Length());
            return 1 + LookMissing(ref sIn, n);
        }
#endif

#if SPECIAL2
        private bool RunBorderFix(string sType, int i, string mName, ref string sIn)
        {
            int n = BorderFix(ref sIn);
            if(n > 0)
            {
                using (StreamWriter writer = File.AppendText(Utils.WhereExe + "\\FixedBorder.txt"))
                {
                    writer.WriteLine(sType + " " + i.ToString() + " " + mName + " " + n.ToString());
                }
            }
            return n > 0;
        }

        /*
         * replace
         * <img src= border="2">
         * with
         * <table border="1" width="50%"><td><img src=></td></table>
         * 
        */
        private int BorderFix(ref string s)
        {
            string t,u;
            int i,j;
            j = s.LastIndexOf("border=\"2\">");
            if (j < 0) return 0;
            i = s.LastIndexOf("<img", j);
            if (i < 0) return 0;
            t = s.Substring(i, j - i) + ">";
            u = Utils.Form1CellTable(t);
            t = s.Substring(i, 11 + j - i);
            s = s.Replace(t, u);
            return 1 + BorderFix(ref s);
        }
#endif
        //red is #FF6600
        private void btnRed_Click(object sender, EventArgs e)
        {
            if (SetFGcolor(tbColorCode.Text))
            {
                string s = tbColorCode.Text;    // may have changed
                Utils.AddColor(ref tbBody, s);
            }
            else BeepError();
        }
        private bool SetFGcolor(string sColor)
        { 
            string s = sColor.Trim().ToUpper();
            bool bRtn = true;   // assume all is ok
            if (Utils.IsValidHtmlColor(s))
            {
                tbColorCode.Text = s;
            }
            else
            {
                s = "#FF6600";
                tbColorCode.Text = s;
                bRtn = false;
            }
            tbColorCode.ForeColor = ColorTranslator.FromHtml(s);
            return bRtn;
        }
        private void btnColors_Click(object sender, EventArgs e)
        {
            string sHTMLcolors = "<table border='1'><tr><td>Black</td><td><font color='#000000'>ForeGround</font></td><td><font color='#000000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #000000; color: black;'>Background</span></td><td><span style='background-color: #000000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#000000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>White</td><td><font color='#FFFFFF'>ForeGround</font></td><td><font color='#FFFFFF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFFFFF; color: black;'>Background</span></td><td><span style='background-color: #FFFFFF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFFFFF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Red</td><td><font color='#FF0000'>ForeGround</font></td><td><font color='#FF0000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FF0000; color: black;'>Background</span></td><td><span style='background-color: #FF0000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FF0000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Green</td><td><font color='#008000'>ForeGround</font></td><td><font color='#008000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #008000; color: black;'>Background</span></td><td><span style='background-color: #008000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#008000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Blue</td><td><font color='#0000FF'>ForeGround</font></td><td><font color='#0000FF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #0000FF; color: black;'>Background</span></td><td><span style='background-color: #0000FF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#0000FF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Yellow</td><td><font color='#FFFF00'>ForeGround</font></td><td><font color='#FFFF00'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFFF00; color: black;'>Background</span></td><td><span style='background-color: #FFFF00; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFFF00'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Cyan</td><td><font color='#00FFFF'>ForeGround</font></td><td><font color='#00FFFF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #00FFFF; color: black;'>Background</span></td><td><span style='background-color: #00FFFF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#00FFFF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Magenta</td><td><font color='#FF00FF'>ForeGround</font></td><td><font color='#FF00FF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FF00FF; color: black;'>Background</span></td><td><span style='background-color: #FF00FF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FF00FF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Silver</td><td><font color='#C0C0C0'>ForeGround</font></td><td><font color='#C0C0C0'><b>ForeGroundBold</b></font></td><td><span style='background-color: #C0C0C0; color: black;'>Background</span></td><td><span style='background-color: #C0C0C0; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#C0C0C0'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Gray</td><td><font color='#808080'>ForeGround</font></td><td><font color='#808080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #808080; color: black;'>Background</span></td><td><span style='background-color: #808080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#808080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Maroon</td><td><font color='#800000'>ForeGround</font></td><td><font color='#800000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #800000; color: black;'>Background</span></td><td><span style='background-color: #800000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#800000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Olive</td><td><font color='#808000'>ForeGround</font></td><td><font color='#808000'><b>ForeGroundBold</b></font></td><td><span style='background-color: #808000; color: black;'>Background</span></td><td><span style='background-color: #808000; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#808000'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Purple</td><td><font color='#800080'>ForeGround</font></td><td><font color='#800080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #800080; color: black;'>Background</span></td><td><span style='background-color: #800080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#800080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Teal</td><td><font color='#008080'>ForeGround</font></td><td><font color='#008080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #008080; color: black;'>Background</span></td><td><span style='background-color: #008080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#008080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Navy</td><td><font color='#000080'>ForeGround</font></td><td><font color='#000080'><b>ForeGroundBold</b></font></td><td><span style='background-color: #000080; color: black;'>Background</span></td><td><span style='background-color: #000080; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#000080'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Orange</td><td><font color='#FFA500'>ForeGround</font></td><td><font color='#FFA500'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFA500; color: black;'>Background</span></td><td><span style='background-color: #FFA500; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFA500'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Aquamarine</td><td><font color='#7FFFD4'>ForeGround</font></td><td><font color='#7FFFD4'><b>ForeGroundBold</b></font></td><td><span style='background-color: #7FFFD4; color: black;'>Background</span></td><td><span style='background-color: #7FFFD4; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#7FFFD4'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Turquoise</td><td><font color='#40E0D0'>ForeGround</font></td><td><font color='#40E0D0'><b>ForeGroundBold</b></font></td><td><span style='background-color: #40E0D0; color: black;'>Background</span></td><td><span style='background-color: #40E0D0; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#40E0D0'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Lime</td><td><font color='#00FF00'>ForeGround</font></td><td><font color='#00FF00'><b>ForeGroundBold</b></font></td><td><span style='background-color: #00FF00; color: black;'>Background</span></td><td><span style='background-color: #00FF00; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#00FF00'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Fuchsia</td><td><font color='#FF00FF'>ForeGround</font></td><td><font color='#FF00FF'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FF00FF; color: black;'>Background</span></td><td><span style='background-color: #FF00FF; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FF00FF'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Indigo</td><td><font color='#4B0082'>ForeGround</font></td><td><font color='#4B0082'><b>ForeGroundBold</b></font></td><td><span style='background-color: #4B0082; color: black;'>Background</span></td><td><span style='background-color: #4B0082; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#4B0082'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Violet</td><td><font color='#EE82EE'>ForeGround</font></td><td><font color='#EE82EE'><b>ForeGroundBold</b></font></td><td><span style='background-color: #EE82EE; color: black;'>Background</span></td><td><span style='background-color: #EE82EE; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#EE82EE'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Pink</td><td><font color='#FFC0CB'>ForeGround</font></td><td><font color='#FFC0CB'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFC0CB; color: black;'>Background</span></td><td><span style='background-color: #FFC0CB; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFC0CB'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Peach</td><td><font color='#FFDAB9'>ForeGround</font></td><td><font color='#FFDAB9'><b>ForeGroundBold</b></font></td><td><span style='background-color: #FFDAB9; color: black;'>Background</span></td><td><span style='background-color: #FFDAB9; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#FFDAB9'&gt;ForeGround&lt;/font&gt;</td></tr><tr><td>Beige</td><td><font color='#F5F5DC'>ForeGround</font></td><td><font color='#F5F5DC'><b>ForeGroundBold</b></font></td><td><span style='background-color: #F5F5DC; color: black;'>Background</span></td><td><span style='background-color: #F5F5DC; color: black;'><b>BackgroundBold</b></span></td><td>&lt;font color='#F5F5DC'&gt;ForeGround&lt;/font&gt;</td></tr></table>";
            Utils.ShowPageInBrowser("", sHTMLcolors);
        }

        private void BeepError()
        {
            int frequency = 1000;
            int duration = 500;
            Console.Beep(frequency, duration);
        }

        private void FormGoToKB(string sS)
        {
            string s = sS.Substring(0, 1).ToLower();
            string w = "https://h30434.www3.hp.com/t5/";

            string[] KBl = 
            {
                "Printers-Knowledge-Base/tkb-p/printers-knowledge-base",
                "Desktop-Knowledge-Base/tkb-p/desktop-knowledge-base",
                "Notebooks-Knowledge-Base/tkb-p/notebooks-knowledge-base",
                "Gaming-Knowledge-Base/tkb-p/gaming-knowledge-base"
            };
            string sQ = "";
            switch (s)
            {
                case "p": sQ = w + KBl[0]; break;
                case "d": sQ = w + KBl[1]; break;
                case "n": sQ = w + KBl[2]; break;
                case "g": sQ = w + KBl[3]; break;
                case "a":
                    sQ = "https://h30434.www3.hp.com/t5/custom/page/page-id/RecentDiscussions";
                    break;
                case "s":
                    if (Properties.Settings.Default.SpecialWord == null) return;
                    Clipboard.SetText(Properties.Settings.Default.SpecialWord);
                    SpecialUsed(false);
                    return;
                case "e": Clipboard.SetText(Properties.Settings.Default.sEmail);
                    return;
            }
            if (sQ != "")
                Utils.LocalBrowser(sQ);
        }

        private void HPWS_click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                FormGoToKB(menuItem.Text);
            }
        }

        private void bltnHR_Click(object sender, EventArgs e)
        {
            Utils.InsertHR(ref tbBody);
        }

        private void mnuAskQ_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML("SiteMap.html", true);
        }

        private void loadHardwareMacsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFileItem("HW");
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool bIgnore = false;
            if(!bForceClose)
            {
                if (!bPageSaved(ref bIgnore))
                {
                    e.Cancel = !bIgnore;
                }
            }

        }

        private void mnuRef_Click(object sender, EventArgs e)
        {
            SelectFileItem("RF");
        }

        private void mnuNote_Click(object sender, EventArgs e)
        {
            SelectFileItem("NO");
        }

        private void btnSwapBR_Click(object sender, EventArgs e)
        {
            if(btnSwapBR.Text == "Show <BR>")
            {
                btnSwapBR.Text = "Hide <BR>";
                tbBody.Text = tbBody.Text.Replace(Environment.NewLine, "<br>");
            }
            else
            {
                btnSwapBR.Text = "Show <BR>";
                tbBody.Text = tbBody.Text.Replace("<br>",Environment.NewLine);
            }
        }

        private void btnHTest_Click(object sender, EventArgs e)
        {
            SyntaxTest();
        }

        private void btnNoNL_Click(object sender, EventArgs e)
        {
            Utils.RemoveSelectedNL(ref tbBody);
        }



        private void SigImages()
        {
            CSignature MySigTest = new CSignature();
            MySigTest.ShowDialog();
            MySigTest.Dispose();
        }
        private void mnuEmoji_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML(Properties.Resources.emoji, false);
        }

        private void mnuImgSig_Click(object sender, EventArgs e)
        {
            SigImages();
        }

        private void mnuCCodes_Click(object sender, EventArgs e)
        {
            Utils.ShellHTML(Properties.Resources.HP_CountryCodes, false);
        }

        private void EnableAllWeb(bool b)
        {
            mnuSearchComm.Enabled = b;
            mnuDrvGoog.Enabled = b;
            mnuHuntDev.Enabled = b;

        }

        private void mnRecDis_Click_1(object sender, EventArgs e)
        {
            TextFromClipboardMNUs = Utils.ClipboardGetText();
            string sL = TextFromClipboardMNUs.ToLower();
            int n = TextFromClipboardMNUs.Length;
            bTextFromClipboardMNUs = !(n <= 1 || n > 128);
            mnuDrvGoog.Enabled = bTextFromClipboardMNUs;
            mnuDevCol.Enabled = bTextFromClipboardMNUs;
            hPYouTubeToolStripMenuItem.Enabled = bTextFromClipboardMNUs;
            mnuHuntDev.Enabled = sL.Contains("dev") && sL.Contains("ven");
        }
    }
    
}