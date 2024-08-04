//#define SPECIAL2
//#define SPECIAL3
/*
 * zoom is ctrl shift dot  or comma to set font in visual studio
 * load file is a comment for when a macro is read to be displayed in the datagridview
 * show macro is a command for ShowUneditedRow where a macro is displayed from the datagridview
*/
#define SPECIAL4
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
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Globalization;
using System.Data;
using MacroViewer.sources;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Linq.Expressions;


namespace MacroViewer
{
    public partial class main : Form
    {
        private bool bForceClose = false;
        private int NumInBody = 0;  // probably should have used a List<string> instead of [Utils.NumMacros]
        private bool bHaveHTMLasLOCAL = false;      // if true then we just read in html 
        private bool bShowingError = false; // if true then highlighting errors between HP and local
        private bool bHaveBothHP = false; // have read both the HTML and the HPmacros for convenience uploading
        private string aPage;
        private int[] StartMac = new int[Utils.HPmaxNumber];
        private int[] StopMac  = new int[Utils.HPmaxNumber];
        private int[] MacBody  = new int[Utils.HPmaxNumber];
        private string strType = "";    // either PRN or PC for printer or pc macros or HP 
        private string TXTName = "";
        private int CurrentRowSelected = -1;
        private OpenFileDialog ofd;
        private bool bMacroErrors;
        private bool bInitialLoad = false;  // this is used with the bad ending to look for
        private List<string> strBadEnding = new List<string>();
        private bool bHaveHTML = false; // html macro was read in. this cannot be edited
        private bool bHaveHP = false;   // have an HPMacro.txt file
        private int NumSupplementalSignatures = 0;
        private Color NormalEditColor;
        public CMoveSpace cms;
        public List<CBody> cBodies;  // this is only updated when the program starts
        private string sBadMacroName;
        private string TextFromClipboardMNUs = "";
        private bool bTextFromClipboardMNUs; // can text be used as an argument to a search
        private cMacroChanges xMacroChanges;
        private cMacroChanges xMacroViews;
        private int tbBodyChecksumN = 0;
        private bool tbBodyChecksumB  = false;
        private int NumCheckedMacros = 0;
        private string LastViewedFN = "";   // last macro file prefix
        private string UnfinishedFN = "";   // this file has "Change Me" or empty body
        private string UnfinishedMN = "";   // this macro is the unfinished one
        private int UnfinishedIndex = -1;
        private string vWarning = "";
        public List<dgvStruct> DataTable;   // from reading supplemental files
        public List<dgvStruct> HPDataTable; // from reading the HP HTTP file
        public BindingSource bs = new BindingSource();
        private bool bHasLocal = false;  // set to true if a local image file path is present in the macro
        public cDupHTTP DupHTTP;

        public main()
        {
            InitializeComponent();
            lbName.AutoGenerateColumns = false;
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
            xMacroChanges = new cMacroChanges();
            xMacroChanges.Init("MacroChanges.txt");
            xMacroViews = new cMacroChanges();
            xMacroViews.Init("MacroViews.txt");
            DataTable = new List<dgvStruct>();
            HPDataTable = new List<dgvStruct>();
            cms = new CMoveSpace();
            NormalEditColor = btnCancelEdits.ForeColor;
            SetFGcolor("#FF6600");
            if (bForceClose)
            {
                timer1.Interval = 500;
            }

            vWarning = lblVurl.Text;
            if (Properties.Settings.Default.Vdisable)
            {
                lblVurl.Text = "CTRL-V behavies as usual" + Environment.NewLine + "No hyperlink shortcut";
            }
            else
            {
                lblVurl.Text = vWarning;
            }
            this.Shown += LoadInitialFiles;
        }

        private void CheckPWE()
        {
            btnSpecialWord.Enabled = (Properties.Settings.Default.SpecialWord != "");
            btnCopyEmail.Enabled = (Properties.Settings.Default.sEmail != "");
        }

        private  void LoadInitialFiles(object sender, EventArgs e)
        {
            Utils.TotalNumberMacros = LoadAllFiles();
            CheckPWE();
            if (Properties.Settings.Default.cSplash == Properties.Settings.Default.sSplash) return;
            splash MySplash = new splash();
            MySplash.Show();
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

#if aint
// bMacroError might be used here
        private void LookForHTMLfix()
        {
            bool b = false;
            bool c = false;
            for (int i = 0; i < Utils.NumMacros; i++) // was NIB
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

#endif

        private void HighlightDIF()
        {
            int i = 0;
            foreach(dgvStruct row in DataTable)
            {
                if (row.HP_HTML_NO_DIFF)
                    SetDefaultCellColor(i);
                else SetErrorRed(i);
                i++;
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
            for (int i = 0; i < HPDataTable.Count; i++)
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
                //Body[i] = strBody;
                HPDataTable[i].sBody = strBody;
                strFind = Utils.BBCparse(strBody);
                HPDataTable[i].sErr = strFind;
                HPDataTable[i].HPerr = (strFind != "");
                if (HPDataTable[i].HPerr)
                {
                    bMacroErrors = true;
                   // SetErrorRed(i);
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
            tbNumMac.Text = "0";
            for (int i = 0; i < HPDataTable.Count; i++)
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
                HPDataTable[i].MoveM = false;
                HPDataTable[i].MacName = strName;
            }
            tbNumMac.Text = NumUsed.ToString();
            return true;
        }



        private bool FindMacros()
        {
            int j, k;
            NumInBody = 0;
            for (int i = 0; i < Utils.HPmaxNumber; i++)
            {
                string strFind = "Macro " + (i + 1).ToString();
                j = aPage.IndexOf(strFind);
                if (j < 0)
                {
                    Utils.HPmaxNumber = i;
                    return false;
                }
                dgvStruct dgv = new dgvStruct();
                dgv.Inx = i + 1;
                HPDataTable.Add(dgv);
                StartMac[i] = j;
                j += strFind.Length;
                k = aPage.Substring(j).IndexOf(strFind);
                if (k < 0)
                {
                    Utils.HPmaxNumber = i;
                    return false;
                }
                StopMac[i] = k + j;
            }
            NumInBody = HPDataTable.Count;
            Utils.HPmaxNumber = NumInBody;
            return true;
        }

        private void ParsePage()
        {
            HPDataTable.Clear();
            FindMacros();
            FindNames();
            FindBody();
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
            return true;
        }

        //jys
        private void RunBrowser()
        {
            string strTemp = tbBody.Text;
            if (strTemp == "" || !tbBody.Enabled) return;
            string sMacroName = tbMacName.Text; //lbName.Rows[CurrentRowSelected].Cells[2].Value.ToString();
            if (cbShowLang.Checked)
            {
                strTemp = Utils.AddLanguageOption(strTemp);
            }
            Utils.ShowPageInBrowser(strType, strTemp);
            xMacroViews.AddView(strType, sMacroName);
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

        private void mINload(object sender, EventArgs e)
        {
            SelectFileItem("IN");
        }

        private void mOJload(object sender, EventArgs e)
        {
            SelectFileItem("OJ");
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

        //HID\VID_187C&PID_0526\7&167D68C8&1&0000
        private string sExtract(string s, string sPat)
        {
            int e = 4;  // length of hex string
            int n = s.IndexOf(sPat);
            int m = sPat.Length;
            if (n < 0) return "";
            n += m;
            return s.Substring(n, e);
        }

        // "USB\\VID_2EF4&PID_5842&MI_00\\8&1AD5FA5&0&0000";
        // "PCI\VEN_1B21&DEV_2142&SUBSYS_87561043&REV_00\4&299AAA38&0&00E4"

        // devicehunt.com/search/type/usb/vendor/2EF4/device/5842
        private void mnuHuntDev_Click(object sender, EventArgs e)
        {
            string s = Utils.ClipboardGetText().ToUpper();
            string sType = "";
            if (s.Contains("USB")) sType = "/USB";
            if (s.Contains("PCI")) sType = "/PCI";
            //if (s.Contains("HID")) sType = "/HID";
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
        }

        private void MakeSticky(string s)
        {
            bool b = true;
            if(Properties.Settings.Default.AllowSTICKYedits == false)
            {
                switch (s)
                {
                    case "RF":
                        b = (CurrentRowSelected >= Utils.RequiredMacrosRF.Length);
                        break;
                }
            }
            btnDelM.Enabled = b;
            btnSaveM.Enabled = b;
            btnDelChecked.Enabled = b;
        }

        private void CheckForLanguageOption(bool bRowChanged)
        {
            cbShowLang.Visible = tbBody.Text.Contains(Utils.sPossibleLanguageOption[0]);
            cbShowLang.Checked = !bRowChanged;
        }

        private void AllowTBbody(bool bAccess)
        {
            tbBody.Enabled = bAccess;
            tbBody.ReadOnly = !bAccess;
            if(!bAccess)
            {
                tbNumMac.Text = "";
                tbMNum.Text = "";
            }
        }

        private void AllowTBbody()
        {
            AllowTBbody(lbName.Rows.Count > 0);
        }


        private bool AnyHyper(string s)
        {
            if (s.Contains("href=")) return true;
            if (s.Contains("<img ")) return true;
            return false;
        }

        // show macro here
        private void ShowUneditedRow(int e)
        {
            bool bChanged = (CurrentRowSelected != e);
            CurrentRowSelected = e;
            if (lbName.Rows.Count == 0 || e >= lbName.Rows.Count)
            {
                tbBody.Text = "Please create a new macro by clicking 'NEW'";
                tbMacName.Text = "";
                AllowTBbody(false);
                return;
            }
            if(strType != LastViewedFN)
            {
                NumCheckedMacros = 0;
                LastViewedFN = strType;
            }
            //AllowTBbody(true);
            tbMNum.Text = (1 + e).ToString();
            if (strType == "RF")
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
            btnShowURLs.Enabled = AnyHyper(tbBody.Text.ToLower());
        }

        private void ShowBodyFromSelected()
        {
            if (DataTable[CurrentRowSelected].sBody == null)
            {
                DataTable[CurrentRowSelected].sBody = ""; // have named macro but not body so create empty one
                tbBody.Text = "";
            }
            else tbBody.Text = DataTable[CurrentRowSelected].sBody.Replace("<br>", Environment.NewLine);
            tbBodyChecksumN = xMacroChanges.CalculateChecksum(tbBody.Text);
            tbBodyChecksumB = true;
            lbName.Rows[CurrentRowSelected].Selected = true;
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



        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            TbodyInsert(Utils.GetHPclipboard());
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
        }



        // can move macros from all files except the HTML one
        private void AllowMacroMove(bool b)
        {
            mMoveMacro.Visible = b;
            lbName.ReadOnly = !b;
        }

        private void AllowMacroMove()
        {
            AllowMacroMove(DataTable.Count > 0);
        }

        private void LoadHTMLfile()
        {
            AccessDiffBoth(false);
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
            if(bHaveHTML)
            {
                if (bHaveHP) return;
                SaveHTTPasTXT("HP");
                LoadFromTXT("HP");
                ShowUneditedRow(0);
                AllowMacroMove(true);
            }
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

        private void TryDif(ref string str1, ref string str2)
        {
            int k = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    k++;
                }
            }
        }

      
        // load files here
        // HP and HTML can have blank macro names and body but NOT any others
        private int LoadFromTXT(string strFN)
        {
            int i = 0;
            bool bNoEmpty = !(strFN == "HP" || strFN == "" || strFN == "HTML");
            string sErr = "";
            bHasLocal = false;
            bMacroErrors = false;
            bool bMacroDiff = false;
            mShowErr.Visible = false;
            TXTName = strFN;
            strType = strFN;     //TODO todo to do this needs to be cleaned up
            gpMainEdit.Enabled = true;
            gbSupp.Enabled = true;
            string TXTmacName = Utils.FNtoPath(strFN);
            this.Text = " HP Macro Editor: " + TXTmacName;
            NumInBody = 0;
            DataTable.Clear();
            lbName.DataSource = null;
            lbName.Invalidate();
            if (File.Exists(TXTmacName))
            {
                StreamReader sr = new StreamReader(TXTmacName);
                string line = sr.ReadLine();
                string sBody, s;
                bHaveHTML = false;
                bool bAnyHPdiff = (strFN == "HP") && bHaveHTMLasLOCAL;
                lbName.RowEnter -= lbName_RowEnter;
                while (line != null)
                {
                    dgvStruct dgv = new dgvStruct();
                    dgv.Inx = i + 1;
                    dgv.MacName = line;
                    dgv.MoveM = false;
                    sBody = sr.ReadLine();
                    if (sBody == null) sBody = "";
                    dgv.sBody = sBody;
                    if (sBody.Contains(Utils.AssignedImageName))
                        bHasLocal = true;
                    sErr = "";
                    int j = sBody.ToLower().IndexOf("http:");
                    if(j >= 0)
                    {
                        sErr += " http: found(" + (j+1).ToString() + ") ";
                    }                   
                    sErr += Utils.BBCparse(sBody);
                    dgv.HPerr = (sErr != "");
                    dgv.sErr = sErr;
                    if (bAnyHPdiff)
                    {
                        if (HPDataTable[i].sBody == null) s = "";
                        else s = HPDataTable[i].sBody;
                        dgv.HP_HTML_DIF_LOC = Utils.FirstDifferenceIndex(sBody, s);
                        dgv.HP_HTML_NO_DIFF = (dgv.HP_HTML_DIF_LOC == -1);
                        if (!dgv.HP_HTML_NO_DIFF)
                            bMacroDiff = true;
                        //dgv.HPerr |= dgv.HP_HTML_NO_DIFF;
                    }
                    else dgv.HP_HTML_NO_DIFF = true;
                    if (dgv.HPerr)
                        bMacroErrors = true;
                    DataTable.Add(dgv);
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
                NumInBody = DataTable.Count;
                bs.DataSource = DataTable;
                lbName.DataSource = bs;
                bs.ResetBindings(false);
                CreateLB(strFN);
                lbName.Columns[2].ReadOnly = true;
                lbName.RowEnter += lbName_RowEnter;
                sr.Close();
                if (DataTable.Count > 0 && strFN == "HP")
                    bHaveBothHP = bHaveHTMLasLOCAL;
            }
            else CreateLB(strFN);
            tbNumMac.Text = i.ToString();
            for(i = 0; i < lbName.Rows.Count; i++)
            {
                if (DataTable[i].HPerr)
                {
                    bMacroErrors = true;
                    SetErrorRed(i);
                }
            }
            btnNew.Enabled = lbName.RowCount < Utils.NumMacros;
            if (strFN == "HP")
            {
                btnNew.Enabled = false;
                for (i = 0; i < lbName.Rows.Count; i++)
                {
                    if (!DataTable[i].HP_HTML_NO_DIFF)
                    {
                        SetErrorRed(i);
                    }
                }
            }
            mShowErr.Visible = bMacroErrors;
            lbRCcopy.Visible = bMacroDiff;
            AllowTBbody(i>0);
            return i;
        }

        private void CreateLB(string s)
        {
            lbName.Columns.Clear();

            lbName.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Inx",
                HeaderText = "N",
                ValueType = typeof(int),
                Width = 36
            });
            lbName.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "MoveM",
                HeaderText = "Move",
                Width = 50
            });
            lbName.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MacName",
                HeaderText = "Name: " + Utils.FNtoHeader(s),
                Width = 280
            });
        }

        private void SaveAsTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            strType = strFN;    // TODO need to clean this up todo to do
            string TXTmacName = Utils.FNtoPath(strFN);
            foreach (dgvStruct row in DataTable)
            {
                string strName = row.MacName;
                string strBody = row.sBody;
                strOut += strName + Environment.NewLine + strBody + Environment.NewLine;
                i++;
            }
            if (i > 0)Utils.WriteAllText(TXTmacName, strOut);
            else File.Delete(TXTmacName);
            NumInBody = i;
        }

        private void SaveHTTPasTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            strType = strFN;    // TODO need to clean this up todo to do
            string TXTmacName = Utils.FNtoPath(strFN);
            foreach (dgvStruct row in HPDataTable)
            {
                string strName = row.MacName;
                string strBody = row.sBody;
                strOut += strName + Environment.NewLine + strBody + Environment.NewLine;
                i++;
            }
            if (i > 0) Utils.WriteAllText(TXTmacName, strOut);
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
                SaveHTTPasTXT("HP");
            }
        }


        private int RemoveMacro()
        {
           int j = CurrentRowSelected;
            lbName.RowEnter -= lbName_RowEnter;
            bs.Remove(bs.Current);
            bs.ResetBindings(false);
            lbName.RowEnter += lbName_RowEnter;
            if (j == lbName.RowCount)
                j--;
            if (j < 0) j = 0;
            return j;
        }

        private void EnableMacEdits(bool enable)
        {
            btnDelM.Enabled = enable && CurrentRowSelected >= 0;
            btnSaveM.Enabled = enable && CurrentRowSelected >= 0;
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
                DataTable[CurrentRowSelected].sBody = "";
                DataTable[CurrentRowSelected].MacName = "";
 //               lbName.Rows[CurrentRowSelected].Cells[2].Value = "";
                tbBody.Text = "";
            }
            xMacroChanges.TryRemove(strType, tbMacName.Text);
            MustFinishEdit(true);
            ReSaveAsTXT(TXTName);
            ShowUneditedRow(CurrentRowSelected);
        }

        private void ReSaveAsTXT(string TXTName)
        {
            SaveAsTXT(TXTName);
            LoadFromTXT(TXTName);
        }

        private int FailsHTMLparse()
        {
            if (HasBadUrl()) return 1;
            if (strType == "NO" || strType == "RF") return 0;
            return Utils.SyntaxTest(tbBody.Text);
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
            Debug.Assert(tbBody.Text != null, "Edit body should not be null");
            tbBody.Text = tbBody.Text.Trim();
            if (tbBody.Text == "")
            {
                tbBody.Text = Utils.UnNamedMacro;
                i++;
            }
            return i == 2;  // both items blank
        }

        // if UpdateSelected then the macro name and body changed
        // return code 0:ok to save; 1:cannot save;  3: cannot save as do not want to overwrite
        // code of 2: html error needs to be corrected but could be ignored
        // 
        private int SaveCurrentMacros(bool UpdateSelected)
        {
            bool bChanged = false;
            NoEmptyMacros();
            string strName = tbMacName.Text;
            string strOld = "";
            if (lbName.RowCount == 0)
            {
                return 1; // must have wanted to add a row: sorry
            }
            int r = FailsHTMLparse();
            if (r > 0)
            {
                switch (r)
                {
                    case 1:
                        return 2;
                    case 2:
                        break;
                }
            }

            if(UpdateSelected)
            {
                strOld = lbName.Rows[CurrentRowSelected].Cells[2].Value.ToString();
                if (strName != strOld && (Utils.UnNamedMacro != strOld))
                {
                    DialogResult Res1 = MessageBox.Show("This will overwrite " + strOld + " with " + strName,
            "Replaceing a macro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Res1 != DialogResult.Yes)
                    {
                        return 3;
                    }
                }
                lbName.Rows[CurrentRowSelected].Cells[2].Value = strName;
                DataTable[CurrentRowSelected].sBody = RemoveNewLine(ref bChanged, Utils.NoTrailingNL(tbBody.Text).Trim());
                if (tbBodyChecksumB)
                    xMacroChanges.isMacroChanged(tbBodyChecksumN, TXTName, strName, DataTable[CurrentRowSelected].sBody);
            }

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
            return 0;
        }

        private void RemoveHTML(ref string s)
        {
            bool b = true;
            int i, j;
            while (b)
            {
                i = s.IndexOf("<a href");
                b = i >= 0;
                if(b)
                {
                    j = s.IndexOf("</a>", i);
                    j += 4;
                    s = s.Remove(i, j - i);
                }
            }
            b = true;
            while(b)
            {
                i = s.IndexOf("<img ");
                b = i >= 0;
                if (b)
                {
                    j = s.IndexOf(">", i);
                    j++;
                    s = s.Remove(i, j - i);
                }
            }
        }
        private bool RefsOnly()
        {
            if (strType != "RF") return false;
            string s = tbBody.Text.ToLower().Replace(Environment.NewLine, "") ;
            RemoveHTML(ref s);
            s = s.Trim();
            return s != "";
        }
         
        private void btnSaveM_Click(object sender, EventArgs e)
        {
            if (RefsOnly())
            {
                MessageBox.Show("Macros must contain URLs only, no text");
                btnCancelEdits.ForeColor = Color.Red;
                return;
            }
            if (NoEmptyMacros())
            {
                MessageBox.Show("You cannot save an empty macro");
                return;
            }
            int RtnCode = SaveCurrentMacros(true);
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
            dgvStruct dgv = new dgvStruct();
            dgv.Inx = lbName.Rows.Count;
            dgv.MacName = strNewName;
            dgv.HPerr = false;
            dgv.sBody = "";
            dgv.MoveM = false;
            dgv.sErr = "";
            if (Utils.IsNewPRN(TXTName))
            {
                string sNB =
                    "You may need to reset the printer: video here" + Utils.nBR(3) +
                    "WiFi setup to router:  video here" + Utils.nBR(3) +
                    "For WiFi direct setup see page xx click here.<br>This is useful if there is no modem or you do not want to give someone access to your modem but you want to let them print something." + Utils.nBR(3) +
                    "Simple push button or WPS setup is described on page xx of Users Manual" + Utils.nBR(3) +
                    "Full feature software DEVICE MONTH YEAR" + Utils.nBR(3) +
                    "Printer Reference ID1 ID2";
                strBody = sNB;
            }
            dgv.sBody = RemoveNewLine(ref bChanged, strBody);
            DataTable.Add(dgv);
            bs.ResetBindings(false);
            tbBody.Text = strBody.Replace("<br>",Environment.NewLine);
            ReSaveAsTXT(TXTName);
            HaveSelected(true);
            lbName.Invalidate();
            lbName.Refresh();
            CurrentRowSelected = lbName.Rows.Count-1;
            lbName.Rows[CurrentRowSelected].Selected = true;
            tbMacName.Text = strNewName;
            tbNumMac.Text = lbName.Rows.Count.ToString();
            EnableMacEdits(true);
            return true;
        }



        private int ReloadHP(int r)
        {
            int iCnt = 0;
            lbRCcopy.Visible = false;
            if(!bHaveHTMLasLOCAL)
                bHaveHTMLasLOCAL = ReadLastHTTP();
            ShowUneditedRow(r);
            strType = "HP";
            iCnt = LoadFromTXT(strType);
            ShowUneditedRow(r);
            EnableMacEdits(true);
            return iCnt;
        }


        private void ReplaceText(int iStart, int iLen, string strText)
        {
            string sPrefix = tbBody.Text.Substring(0, iStart);
            string sSuffix = tbBody.Text.Substring(iStart + iLen);
            tbBody.Text = sPrefix + strText + sSuffix;
            ScrollToCaretPosition(tbBody, iStart + strText.Length);
        }

        private void TbodyInsert(string sClip)
        {
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            ReplaceText(i, j, sClip);
        }

        private void btnSetObj_Click(object sender, EventArgs e)
        {
            string strReturn = "";
            bool bHaveHTML = false;
            bool bHaveImg = false;
            int i = tbBody.SelectionStart;
            int j = tbBody.SelectionLength;
            string sTemp = tbBody.Text;
            string strRaw = Utils.AdjustNoTrim(ref i, ref j, ref sTemp);
            string strLC = strRaw.ToLower();
            bHaveHTML = strLC.Contains("https:") || strLC.Contains("http:");
            bHaveImg = Utils.IsUrlImage(strLC);

            if (bHaveHTML || bHaveImg)
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
                tbBodyChecksumB = false;
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
            return sBody.Trim();
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
             if (DataTable[CurrentRowSelected].sBody == null)
            {
                if (tbBody.Text.Trim().Length > 0) return false;
                return true; // a leftover "Change Me" has empty body
            }
            string s = tbBodyMarked();
            bool bEdited = (s != DataTable[CurrentRowSelected].sBody);
            return !bEdited;
        }

        private bool bPageSaved(ref bool bIgnore)
        {
            string sMsg = "Macro was not saved\r\nEither save, cancel edits or delete";
            bIgnore = false;
            if(tbMacName.Text.Trim() == "" && strType != "HP")
            {
                NoEmptyMacros();
                sMsg = "Un-named macro not saved, using " + Utils.UnNamedMacro;
            }
            if (bNothingToSave()) return true;
            MustFinishEdit(false);
            DialogResult Res1 = MessageBox.Show(sMsg, "Cannot be ignored", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // cannot ignore as this procedure was called by an attempted change to a different macro file
            // and I cannot handle saving during the procedure call. Wish I knew more about these calls as the
            // error message from the internal handler was strange and was always trap when I try to save.
            return false;
        }

        private void btnChangeUrls_Click(object sender, EventArgs e)
        {
            bool bIgnore = false;
            if (bPageSaved(ref bIgnore))
            {
                ManageMacros MyManageMac = new ManageMacros(strType, ref DataTable);
                MyManageMac.ShowDialog();
                if (MyManageMac.nAnyChanges > 0)
                {
                    SaveCurrentMacros(false);
                }
                MyManageMac.Dispose();
            }
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
            bool bIgnore = false;
            int i = 0;
            Settings MySettings = new Settings(Utils.BrowserWanted, Utils.VolunteerUserID, NumSupplementalSignatures,
                ref xMacroChanges, ref xMacroViews);
            MySettings.ShowDialog();
            Utils.BrowserWanted = MySettings.eBrowser;
            Utils.VolunteerUserID = MySettings.userid;
            bMustExit = MySettings.bWantsExit;
            MySettings.Dispose();
            if (bMustExit) this.Close();
            CheckPWE();
            if(xMacroChanges.sGoTo != "")
            {
                if (!bPageSaved(ref bIgnore)) return;
                string sFN = "";
                string sMN = "";
                xMacroChanges.GoToMacro(ref sFN, ref sMN);
                if(sFN != strType)
                    LoadFromTXT(sFN);
                foreach(dgvStruct dgv in DataTable)
                {
                    if (dgv.MacName == sMN) break;
                    i++;
                    if (i == DataTable.Count)
                    {
                        i = 0;
                        break;
                    }
                }
                ShowUneditedRow(i);
            }
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
                if (HPDataTable.Count == 0)
                {
                    MessageBox.Show("Please open your original HP macro file.");
                    return;
                }
                if (!DataTable[r].HP_HTML_NO_DIFF) //(!HP_HTML_NO_DIFF[r])
                {
                    string sWhereErr = "";
                    if (DataTable[r].HP_HTML_DIF_LOC == 0)
                    {
                        sWhereErr = " Diff is at end (or empty)";
                    }
                    else if (DataTable[r].HP_HTML_DIF_LOC > 0)
                    {
                        sWhereErr = " Diff at char " + DataTable[r].HP_HTML_DIF_LOC.ToString();
                    }
                    string s ="Original Macro " + (r+1).ToString() + ": '" + HPDataTable[r].MacName + "'" + sWhereErr + Environment.NewLine + HPDataTable[r].sBody;
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
                if(j == 0 || strRaw.Trim().Length == 0)
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
            string sOut = "";
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Properties.Settings.Default.Vdisable) return;
                string tBody = tbBody.Text;
                sBody = tBody.ToLower();
                i = tbBody.SelectionStart;
                j = tbBody.SelectionLength;
                if (j == 0) return;
                sFromCB = Clipboard.GetText();
                sL = sFromCB.ToLower();
                if (sL.Trim().Length == 0) return;
                k = sL.IndexOf("http");
                if (k != 0) return;
                sText = Utils.AdjustNoTrim(ref i, ref j, ref tBody);
                if(Utils.IsUrlImage(sL))
                {
                    sOut = Utils.AssembleIMG(sText);
                }
                else sOut = Utils.FormUrl(sFromCB, sText);
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
                sS = tBody.Substring(0, i);
                sE = tBody.Substring(i + j);
                tbBody.Text = sS + sOut + sE;
                ScrollToCaretPosition(tbBody, i + sOut.Length);
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
            i += iDir;
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
            DialogResult dr = MessageBox.Show("Reference Macro RFmacros.txt is missing\r\nSelect YES to install Placeholders or NO to quit",
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

        private void ShowEmpty(string sWanted)
        {
            DataTable.Clear();
            bs.ResetBindings(false);
            tbBody.Text = "";
            tbMacName.Text = "";
            strType = sWanted;
            this.Text = " HP Macro Editor";
            NumInBody = 0;
            btnSaveM.Enabled = false;
            btnNew.Enabled = true; // allow to be created
            lbName.Columns[2].HeaderText = "Name: " + Utils.FNtoHeader(sWanted);
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
                AllowTBbody(false);
                AllowMacroMove(false);
                tbBody.Text = "Please create a new macro by clicking 'NEW'";
                return 0;
            }
            if (strType != LastViewedFN)
            {
                NumCheckedMacros = 0;
                LastViewedFN = strType;
            }
            //AllowTBbody(true);
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
            }
            AllowMacroMove(true);
            btnChangeUrls.Enabled = bHasLocal;
            return iCnt;
        }

        private void mShowDiff_Click(object sender, EventArgs e)
        {
            if (bHaveBothHP)
            {
                //ShowHighlights();
            }
        }

        private void mShowErr_Click(object sender, EventArgs e)
        {
            ShowErrors MySE = new ShowErrors(ref DataTable);
            MySE.Show();
        }

        private void btnCleanUrl_Click(object sender, EventArgs e)
        {
            string sDirty = Utils.ClipboardGetText();
            if (sDirty == "") return;
            Utils.ReplaceUrls(ref sDirty,false);
            Clipboard.SetText(sDirty);
        }


        private int LoadAllFiles()
        {
            int nMacroCnt = 0;
            bool bNoEmpty;
            sBadMacroName = "";
            if (cBodies == null)
            {
                bHaveHTMLasLOCAL = ReadLastHTTP();
                cBodies = new List<CBody>();
                if(!File.Exists(Utils.FNtoPath("RF")))
                {
                    int n = WarnMissing("RF");
                    if (n > 0) return LoadFromTXT("RF");
                    return 0;
                }
                foreach (string strFN in Utils.LocalMacroPrefix)
                {
                    int i=0;
                    bNoEmpty = !(strFN == "HP" || strFN == "" || strFN == "HTML");
                    string FNpath = Utils.FNtoPath(strFN);

                    if (File.Exists(FNpath))
                    {
                        StreamReader sr = new StreamReader(FNpath);
                        string strMN = sr.ReadLine();
                        string sBody;
                        bHaveHTML = false;
                        while (strMN != null)
                        {
                            if (strMN.Contains(Utils.UnNamedMacro))
                            {
                                sBadMacroName +=
                                    "Macro " + (i + 1).ToString() + " in " + strFN + " is un-named\r\n";
                                UnfinishedFN = strFN;
                                UnfinishedMN = strMN;
                                UnfinishedIndex = i;
                            }
                            sBody = sr.ReadLine();
                            CBody cb = new CBody();
                            cb.File = strFN;
                            cb.Number = (i + 1).ToString();
                            cb.Name = strMN;
#if SPECIAL2
                        bDebug |= RunBorderFix(strFN, i+1, cb.Name, ref sBody);
#endif
#if SPECIAL3
                        bDebug |= RunLookMissingTR(strFN, i + 1, cb.Name, ref sBody);
#endif

                            cb.sBody = (sBody == null) ? "" : sBody; ;
                            cb.fKeys = "";
                            cBodies.Add(cb);
                            sBody = Utils.BBCparse(sBody);
#if SPECIAL4
#endif
                            i++;
                            nMacroCnt++;
                            strMN = sr.ReadLine();
                            if (strMN == null)
                            {
                                break;
                            }
                            if (strMN == "")
                            {
                                // file  HP can have an empty macro unlike any others empty
                            }
                            if (strMN == "" & bNoEmpty)
                            {
                                strBadEnding.Add(strFN);
                                break;  // if stop here then file has a trailing newline !!!
                            }
                        }
                        sr.Close();
                        if(strFN == "HP" && i > 0)
                            bHaveHP = true;
                    }
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
            
            if(UnfinishedFN == "")
            {
                LoadFromTXT("HP");
                ShowUneditedRow(0);
            }
            else
            {
                LoadFromTXT(UnfinishedFN);
                ShowUneditedRow(UnfinishedIndex);
            }
            AllowTBbody();
            return nMacroCnt;
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
            string sFN = strType;
            bool bFinishedEdits = bNothingToSave();
            WordSearch ws = new WordSearch(ref cBodies, bFinishedEdits, ref xMacroViews);            
            ws.ShowDialog();
            int i, n = ws.LastViewed;
            string NewID = ws.NewItemID;
            string NewName = ws.NewItemName;
            ws.Dispose();
            LastViewedFN = "";  // also sets checked macro count to 0 
            LastViewedFN = "";  // also sets checked macro count to 0 
            if(n >= 0 && bFinishedEdits)
            {
                CBody cb = cBodies[n];
                LoadFromTXT(cb.File);
                sFN = cb.File;
                i = Convert.ToInt32(cb.Number);
                ShowUneditedRow(i - 1);
            }
            if(NewID != "" &&bFinishedEdits)
            {
                n = LoadFromTXT(NewID);
                sFN = NewID;
                if (n < Utils.NumMacros)
                {
                    AddNew(NewName, GetReference());
                }
            }
            if(strType != "HP")
            {
                AccessDiffBoth(false);
                EnableMacEdits(true);
            }
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
                    strAdded += DataTable[i].sBody + Environment.NewLine;
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
                    newMac.AddNB(row.Cells[2].Value.ToString(), DataTable[i].sBody);   // no newlines as added later
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
            if (cms.bCopy) return;
            //handle the ones left over.  if HP then just blank out the body and save
            //else replace the disk file and reload
            if (cms.strType == "HP")
            {
                foreach (DataGridViewRow row in lbName.Rows)
                {
                    i++;
                    if ((bool)row.Cells[1].EditedFormattedValue)
                    {
                        DataTable[i].sBody = "";
                        DataTable[i].MacName = "";
                        row.Cells[2].Value = "";
                        row.Cells[1].Value = false;
                        if (i == CurrentRowSelected) tbBody.Text = "";
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
                    strAdded += DataTable[i].sBody + Environment.NewLine;
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
            RemoveImages ri = new RemoveImages(ref cBodies);
            ri.ShowDialog();
            ri.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            if (bForceClose)
            {
                this.Close();
            }

        }

        private void btnCopyEmail_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.sEmail == "") return;
            Clipboard.SetText(Properties.Settings.Default.sEmail);
        }

        private void btnSpecialWord_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SpecialWord == "") return;
            Clipboard.SetText(Properties.Settings.Default.SpecialWord);
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
            xMacroChanges.SaveChanges();
            xMacroViews.SaveChanges();
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
                tbBody.Text = tbBody.Text.Replace("<br />", "<br>");
                tbBody.Text = tbBody.Text.Replace("<br>",Environment.NewLine);
            }
        }

        private void btnHTest_Click(object sender, EventArgs e)
        {
            Utils.SyntaxTest(tbBody.Text);
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
            mnuHuntDev.Enabled |= sL.Contains("vid") && sL.Contains("pid");
        }

        private string GetParagraph(ref string strIn)
        {
            string p = "";
            int i = strIn.IndexOf("<p>");
            if (i < 0) return "";
            int j = strIn.IndexOf("</p>", i + 3);
            if (j < 0) return "";
            int n = j - i;
            p = strIn.Substring(i + 3, j - i - 3);
            strIn = strIn.Remove(i, n).Replace("<p> </p>"," ");
            return p + "<br>" + GetParagraph(ref strIn);
        }

        private string GetSpans(ref string strIn)
        {
            string p = "";
            int i = strIn.IndexOf("<span>");
            if (i < 0)
            {
                if (strIn.Length == 0) return "";
                string sLast = strIn;
                strIn = "";
                return sLast;
            }
            if(i > 0)
            {
                p += strIn.Substring(0, i);
                strIn = strIn.Substring(i);
                return p + GetSpans(ref strIn);
            }
            int j = strIn.IndexOf("</span>", i + 6);
            if (j < 0) return "";
            int n = j - i + 7;
            p = strIn.Substring(i + 6, j - i - 6);
            strIn = strIn.Remove(i, n);
            return p + GetSpans(ref strIn);
        }

        private string sExtractInfo(string sP)
        {
            string sout = "";
            int i = sP.IndexOf("<a href=\"");
            int j = sP.IndexOf("</a>", i + 9);
            string s = sP.Substring(i + 9, j - i - 9);
            return sout;
        }

        private int RemoveStylesClasses(string sKey, ref string sIn)
        {
            int i = sIn.IndexOf(sKey);
            if (i < 0) return 0;
            int j = sIn.IndexOf('"', i + sKey.Length);
            if(j < 0) return 0;
            int n = j - i + 1;
            string s = sIn.Substring(i, n);
            sIn = sIn.Remove(i, n);
            return 1 + RemoveStylesClasses(sKey, ref sIn);
        }

        //s = s.Replace(" rel=\"nofollow noopener noreferrer\"", ""); or any combination
        // the HP site will add its referrals as needed
        private string sRemoveREL(ref string s)
        {
            int i = s.IndexOf(" rel=\"");
            if (i < 0) return s;
            int j = s.IndexOf('"', i + 6);
            j++;
            string t = s.Substring(0,i) + s.Substring(j);
            return sRemoveREL(ref t);
        }

        private void CleanSB(ref string s)
        {
            s = sRemoveREL(ref s);
            s = s.Replace("<span> </span>", " ");
            s = s.Replace("<br >", "<br>");
            s = s.Replace("<strong>", "<b>");
            s = s.Replace("</strong>", "</b>");
            s = s.Replace("<!--EndFragment-->", "");
            s = s.Replace("<br />", "<br>");
            s = s.Replace("&nbsp;", " ");
            //s = s.Replace(" target=\"_self\"", "");
            //s = s.Replace(" target=\"_blank\"", "");
            s = s.Replace(" data-unlink=\"true\"", "");
            s = Regex.Replace(s, @"\s+", " ");  // replace 1 or more white space with space
        }

        private void btnFromHP_Click(object sender, EventArgs e)
        {
            string s = Utils.GetHPclipboard().Trim();
            CleanSB(ref s);
            int n = RemoveStylesClasses("style=\"",ref s);
            n += RemoveStylesClasses("class=\"", ref s);
            n += RemoveStylesClasses("image-alt=\"", ref s);
            n += RemoveStylesClasses("role=\"", ref s);
            n += RemoveStylesClasses("title=\"", ref s);
            n += RemoveStylesClasses("alt=\"", ref s);
            n += RemoveStylesClasses("li-bindable=\"", ref s);
            n += RemoveStylesClasses("li-message-uid=\"", ref s);
            n += RemoveStylesClasses("li-image-url=\"", ref s);
            n += RemoveStylesClasses("li-image-display-id=\"", ref s);
            n += RemoveStylesClasses("li-bypass-lightbox-when-linked=\"", ref s);
            n += RemoveStylesClasses("tabindex=\"", ref s);
            n += RemoveStylesClasses("li-use-hover-links=\"", ref s);
            n += RemoveStylesClasses("li-compiled=\"", ref s);
            string sOut = "";
            // first check for paragraphs
            s = Regex.Replace(s, @"\s+", " ");
            s = s.Replace("<span >", "<span>");
            s = s.Replace("<p >", "<p>");
            s = s.Replace("<strong >", "<b>"); // unaccountably these are needed again ???
            s = s.Replace("</strong>", "</b>");
            s = s.Replace("<br >", "<br>"); // fixes <br span ----
            while (true)
            {
                string sPara = GetParagraph(ref s);
                if(sPara.Length == 0) break;
                sOut += sPara + "<br>"; // sExtractInfo(sPara);
            }
            // if no paragraphs the try spans
            if(sOut != "")
            {
                s = sOut;
                sOut = "";
            }
            
            while (true)
            {
                string sSpan = GetSpans(ref s);
                if (sSpan == "")
                {
                    if(sOut.Contains("<span>"))
                    {
                        s = sOut;
                        sOut = "";
                        continue;
                    }
                    break;
                }
                sOut += sSpan + "<br>";
            }
            TbodyInsert(sOut.Trim());
        }

        private void lbName_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;

            // Check if the click is on a checkbox cell
            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                // Get the current state of the checkbox
                bool isChecked = (bool)dataGridView[e.ColumnIndex, e.RowIndex].Value;

                // Toggle the checkbox state
                dataGridView[e.ColumnIndex, e.RowIndex].Value = !isChecked;

                NumCheckedMacros += isChecked ? -1 : 1;
                btnDelChecked.Enabled = NumCheckedMacros > 0;
            }
        }

        private void ScrollToCaretPosition(System.Windows.Forms.TextBox textBox, int characterPosition)
        {
            textBox.Focus();
            textBox.SelectionStart = characterPosition;
            textBox.SelectionLength = 0;
            textBox.ScrollToCaret();
        }

        private void btnShowURLs_Click(object sender, EventArgs e)
        {
            EditOldUrls UDurl = new EditOldUrls(tbBody.Text);
            UDurl.ShowDialog();
            string strReturn = UDurl.strResultOut;
            if (strReturn == null) return;
            if (strReturn == "") return;
            tbBody.Text = strReturn;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //ShowHighlights();
            HighlightDIF();
            timer2.Enabled = false;
            
        }

        private void mnPhAlbum_Click(object sender, EventArgs e)
        {
            Utils.ShowMyPhotoAlbum();
        }

        private void mnuLShowDups_Click(object sender, EventArgs e)
        {
            cDupHTTP DupHTTP = new cDupHTTP();
            cBodies = null;
            Utils.TotalNumberMacros = LoadAllFiles();
            foreach (CBody cb in cBodies)
            {
                int n = DupHTTP.AddN(cb.File, cb.Number, cb.sBody);
                int i = Utils.LocalMacroIndexOf(cb.File);
                DupHTTP.nHyper[i] += n;
            }
            ShowDups sd = new ShowDups(ref DupHTTP, ref cBodies);
            sd.ShowDialog();
            if(sd.strFN != "")
            {
                LoadFromTXT(sd.strFN);
                ShowUneditedRow(sd.iMN);
            }
            sd.Dispose();
        }

        private void lbName_SelectionChanged(object sender, EventArgs e)
        {
            if (lbName.SelectedRows.Count > 0)
            {
                int selectedRowIndex = lbName.SelectedRows[0].Index;
                if (selectedRowIndex >= 0 && selectedRowIndex < bs.Count)
                {
                    bs.Position = selectedRowIndex;
                }
            }
        }

        /*

        <a jsname="UWckNb" href="http://h10032.www1.hp.com/ctg/Manual/c06534544.pdf" data-ved="2ahUKEwiOmrfOmduHAxWkN0QIHdlDB-gQFnoECBgQAQ" ping="/url?sa=t&amp;source=web&amp;rct=j&amp;opi=89978449&amp;url=http://h10032.www1.hp.com/ctg/Manual/c06534544.pdf&amp;ved=2ahUKEwiOmrfOmduHAxWkN0QIHdlDB-gQFnoECBgQAQ" ><h3 >Interactive BIOS simulator HP ProBook 450G6</h3></a>


        Let me know if you need more help
        */

        private string ConvertToHTML(string sClip)
        {
            string strBody = sClip;
            string[] sHave = {"&amp;","&lt;", "&gt;", "&nbsp;", "%3A", "%2F","%3F", "%3D","<P>","</P>" };
            string[] sWant = {"&","<", ">", " ", ":", "/", "?","=", "<p>", "</p>" };
            if (strBody == "") return "";
            string hCase, wCase;
            for(int i = 0; i < sHave.Length; i++)
            {
                hCase = sHave[i].ToLower();
                wCase = sWant[i];
                while (strBody.Contains(hCase))
                {
                    strBody = strBody.Replace(hCase, wCase);
                }
                hCase = sHave[i].ToUpper();
                while (strBody.Contains(hCase))
                {
                    strBody = strBody.Replace(hCase, wCase);
                }
            }
            return strBody;
        }

        private void btnPSource_Click(object sender, EventArgs e)
        {
            string sClip = Clipboard.GetText();
            string sHTML = ConvertToHTML(sClip);
            int r = Utils.SyntaxTest(sHTML);
            TbodyInsert(sHTML);
        }

        private void mnuBIOSemu_Click(object sender, EventArgs e)
        {
            BiosEmuSim bes = new  BiosEmuSim();
            bes.ShowDialog();
            bes.Dispose();
        }
    }
    
}