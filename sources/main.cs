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


namespace MacroViewer
{
    public partial class main : Form
    {
        private const int NumMacros = 50;   // only 30 for the HTML file
        private int NumInBody = 0;  // probably should have used a List<string> instead of [NumMacros]
        bool bHaveHTMLasLOCAL = false;      // if true then we just read in html 
        private bool bShowingError = false; // if true then highlighting errors between HP and local
        private bool bShowingDiff = false;  // if true then highlighting differences instead
        private bool bHaveBothHP = false; // have read both the HTML and the HPmacros for convenience uploading
        private bool bHaveBothHPerr = false; // as above but found errors
        private bool bHaveBothDIFF = false;  // HP and local have differences
        // if true then at least one of the HTML had an error and the corresponding HPmacros is fixed
        private string[] MacroErrors = new string[NumMacros];
        private string[] MacroNames = new string[NumMacros];
        private bool[] HTMLerr = new bool[NumMacros];   // if set then error at macro
        private bool[] HPerr = new bool[NumMacros];     // if set then error at macro
        private bool[] HP_HTML_NO_DIFF = new bool[NumMacros];   // if set then difference between them
        private int[] HP_HTML_DIF_LOC = new int[NumMacros]; // -1 is no dif. 0 is long and 1..x is first difference
        private int[] OriginalColor = new int[NumMacros];  //0:was OK to start with, 1:red, 2:blue
        // if HTMLerr set but HPerr is not set then can copy to clipboard with right click
        private bool[] bHPcorrected = new bool[NumMacros];
        private string aPage;
        private int[] StartMac = new int[NumMacros];
        private int[] StopMac = new int[NumMacros];
        private int[] MacBody = new int[NumMacros];
        private string[] Body = new string[NumMacros];
        private string[] Body_HP_HTML = new string[NumMacros]; //local copy of ORIGINAL HP macros
        private string[] Name_HP_HTML = new string[NumMacros]; //local copy of ORIGINAL HP macro names
        private string strType = "";    // either PRN or PC for printer or pc macros or HP 
        private string TXTName = "";
        private int CurrentRowSelected = -1;
        private OpenFileDialog ofd;
        private bool bMacroErrors;
        private bool bInitialLoad = false;  // this is used with the bad ending to look for
        private List<string> strBadEnding = new List<string>();
        private bool bHaveHTML = false; // html macro was read in. this cannot be edited
        private int NumSupplementalSignatures = 0;

        //CSendCloud SendToCloud = new CSendCloud();

        public List<CBody> cBodies;  // this is only updated when the program starts

        public main()
        {
            InitializeComponent();
            Utils.WhereExe = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            EnableMacEdits(false);
            SwitchToMarkup(true);
            //SendToCloud.Init();
            gbManageImages.Enabled = true;// System.Diagnostics.Debugger.IsAttached;
            int iBrowser = Properties.Settings.Default.BrowserID;
            if (iBrowser < 0) Utils.BrowserWanted = Utils.eBrowserType.eEdge;
            else Utils.BrowserWanted = (Utils.eBrowserType)Properties.Settings.Default.BrowserID;
            Utils.VolunteerUserID = Properties.Settings.Default.UserID;
            string strFilename = Properties.Settings.Default.HTTP_HP;
            this.Text = " HP Macro Editor";
            settingsToolStripMenuItem.ForeColor = (Utils.CountImages() > 20) ? Color.Red : Color.Black;
            LoadAllFiles();
            Utils.bRecordUnscrubbedURLs = Properties.Settings.Default.SaveUnkUrls;
            SpecialUsed(Properties.Settings.Default.SpecialWord != "");
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
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
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

        private void HaveSelected(bool bVal)
        {
            btnSaveM.Enabled = bVal;
            btnDelM.Enabled = bVal;
            btnDelChecked.Enabled = bVal;
        }

        private void ShowUneditedRow(int e)
        {
            CurrentRowSelected = e;
            if (lbName.Rows.Count == 0)
            {
                tbBody.Text = "";
                tbMacName.Text = "";
                return;
            }
            HaveSelected(true);
            tbBody.Text = Body[CurrentRowSelected];
            tbMacName.Text = lbName.Rows[CurrentRowSelected].Cells[2].Value.ToString();
            lbName.ClearSelection();
            lbName.Rows[CurrentRowSelected].Selected = true;
            if (lbNotM.Visible) SwitchToMarkup(false);
            if (cbLaunchPage.Checked)
                RunBrowser();
        }

        private void ShowSelectedRow(int e)
        {
            if (bPageSaved())
            {
                ShowUneditedRow(e);
            }
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

        private void SwitchToMarkup(bool bEnable)
        {
            string strOut = "";
            if (bEnable)
            {
                strOut = tbBody.Text.Replace(Environment.NewLine, "<br>");
            }
            else
            {
                strOut = tbBody.Text.Replace("<br>", Environment.NewLine);
            }
            tbBody.Text = strOut;
            lbM.Visible = bEnable;
            lbNotM.Visible = !bEnable;
            UsingMarkup(bEnable);
        }

        private void CopyBodyToClipboard()
        {
            if (tbBody.Text == "") return;
            Clipboard.SetText(tbBody.Text);
        }
        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            CopyBodyToClipboard();
        }

        // below is for testing but I never got it to work
        // trying to get BBCode from an HTML page
        //https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/aa767917(v=vs.85)?redirectedfrom=MSDN
        private void RecoverBBCfromHTML()
        {
            RichTextBox rtb = new RichTextBox();
            if (Clipboard.ContainsText(TextDataFormat.Html))
            {
                string returnHtmlText = Clipboard.GetText(TextDataFormat.Html);
                //Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
                rtb.Text = (string)Clipboard.GetData(DataFormats.Html);
                rtb.Text = "";
                rtb.Paste();
                string strTemp = (string)Clipboard.GetData(DataFormats.Html);
            }
        }

        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            string strTemp = Clipboard.GetText();
            if (!strTemp.Contains(Environment.NewLine))
            {
                strTemp = strTemp.Replace("\n", Environment.NewLine);
            }
            if (lbM.Visible)strTemp = strTemp.Replace(Environment.NewLine, "<br>");
            tbBody.Text = strTemp;
            //RecoverBBCfromHTML();
        }

        // notice to anyone reading this: Feel free to copy the signature and change it
        //https://h30434.www3.hp.com/t5/image/serverpage/image-id/362592i68E07BAE29119CCB/image-size/tiny/is-moderation-mode/true?v=v2&px=100
        // can be tiny thumb small large 
        private void testSignatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSignature MySigTest = new CSignature();
            MySigTest.ShowDialog();
            MySigTest.Dispose();
        }


        const uint WM_PASTE = 0x302;
        const int WM_SETTEXT = 0x000C;


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
            //SendToCloud.PasteToCloud("7L4H9UA#ABA");
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
                gpMainEdit.Enabled = true;
                gbSupp.Enabled = false;
                strType = "";
                bHaveHTML = true;
                ShowUneditedRow(0);
                btnNew.Enabled = false;
                tbMacName.Enabled = false;
                lbName.Columns[2].HeaderText = "Name HTML";
            }
        }

        private void readHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHTMLfile();
        }

        // HP and HTML can have blank macro names and body but NOT any others
        private int LoadFromTXT(string strFN)
        {
            int i = 0;
            bool bNoEmpty = !(strFN == "HP" || strFN == "" || strFN == "HTML");
            tbMacName.Enabled = bNoEmpty;
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
                    sBody = sr.ReadLine();
                    Body[i] = sBody; // not needed ?? .Replace("<br />", "<br>");
                    sBody = Utils.BBCparse(sBody);
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
            btnNew.Enabled = lbName.RowCount < NumMacros;
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
            lbName.Rows.RemoveAt(lbName.Rows.Count - 1);
            HaveSelected(lbName.RowCount > 0);
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
            string strErr = Utils.BBCparse(tbBody.Text);
            if (strErr == "") return false;
            strErr = "Macro " + (CurrentRowSelected + 1).ToString() + " has HTML errors";
            DialogResult Res1 = MessageBox.Show(strErr, "Errors needs to be fixed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

        private void SaveCurrentMacros()
        {
            bool bChanged = false;
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
            SaveCurrentMacros();
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
            if (lbName.Rows.Count == NumMacros)
            {
                MessageBox.Show("Can only hold " + NumMacros.ToString() + " macros");
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
            Body[CurrentRowSelected] = RemoveNewLine(ref bChanged, strBody);
            tbBody.Text = strBody;
            ReSaveAsTXT(TXTName);
            HaveSelected(true);
            lbName.Rows[CurrentRowSelected].Selected = true;
            tbMacName.Text = strNewName;
            tbNumMac.Text = lbName.Rows.Count.ToString();
            EnableMacEdits(true);
            return true;
        }

        private void btnAddM_Click(object sender, EventArgs e)
        {
            string strNewName = tbMacName.Text.Trim();
            AddNew(tbMacName.Text.Trim(), tbBody.Text.Trim());
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            if (tbImgUrl.Text == "") return;
            string strImgUrl = Utils.AssembleIMG(tbImgUrl.Text);
            tbBody.Text = tbBody.Text.Insert(i, strImgUrl);
            tbBody.SelectionStart = i + strImgUrl.Length;
            tbBody.SelectionLength = 0;
            tbBody.Focus();
        }



        private void btnAddURL_Click(object sender, EventArgs e)
        {
            if (tbImgUrl.Text == "") return;
            string strOver = "";
            string strUrl = "<a href=\"" + tbImgUrl.Text.Trim() + " target=\"_blank\">";
            int i = tbBody.SelectionStart;
            int n = tbBody.SelectionLength;
            if (n > 0)
                strOver = tbBody.Text.Substring(i, n).Trim();
            if (strOver == "") n = 0;
            if (n == 0)
            {
                strUrl += "</a>";
                tbBody.Text = tbBody.Text.Insert(i, strUrl);
            }
            else
            {
                tbBody.Text = tbBody.Text.Remove(i, n);
                strUrl += strOver + "</a>";
                tbBody.Text = tbBody.Text.Insert(i, strUrl);
            }
            tbBody.SelectionStart = i + strUrl.Length;
            tbBody.SelectionLength = 0;
            tbBody.Focus();
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            tbBody.Text = tbBody.Text.Insert(i, "<br>");
            i += 4;
            tbBody.SelectionStart = i;
            tbBody.SelectionLength = 0;
            tbBody.Focus();
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            tbBody.Text = tbBody.Text.Insert(i, "<br><br>");
            i += 8;
            tbBody.SelectionStart = i;
            tbBody.SelectionLength = 0;
            tbBody.Focus();
        }

        private void btnCLrUrl_Click(object sender, EventArgs e)
        {
            tbImgUrl.Text = "";
        }

        private void UsingMarkup(bool bEnable)
        {
            btnAdd1New.Enabled = bEnable;
            btnAdd2New.Enabled = bEnable;
        }

        private void btnToMark_Click(object sender, EventArgs e)
        {
            SwitchToMarkup(true);
        }

        private void btnNoMark_Click(object sender, EventArgs e)
        {
            SwitchToMarkup(false);
        }

        private void loadPrinterMacsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ReloadHP(int r)
        {
            mShowDiff.Visible = false;
            lbRCcopy.Visible = false;
            bHaveHTMLasLOCAL = ReadLastHTTP();
            ShowUneditedRow(r);
            strType = "HP";
            LoadFromTXT(strType);
            if (bHaveHTMLasLOCAL)
            {
                LookForHTMLfix();
                bHaveBothDIFF = AnyHPdiff();
                mShowDiff.Visible = bHaveBothDIFF;
                lbRCcopy.Visible = bHaveBothDIFF;
            }
            ShowUneditedRow(r);
            EnableMacEdits(true);
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
            string strRaw = tbBody.SelectedText;
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


        private void btnNew_Click(object sender, EventArgs e)
        {
            SwitchToMarkup(true);
            AddNew(Utils.UnNamedMacro, "");
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        private string tbBodyMarked()
        {
            return tbBody.Text.Replace(Environment.NewLine, "<br>");
        }

        private bool bNothingToSave()
        {
            if (CurrentRowSelected < 0 || strType == "" || NumInBody == 0)
            {
                return true; // nothing to save 
            }
            if (Body[CurrentRowSelected] == null) return true; // a leftover "Change Me" has empty body
            bool bEdited = (tbBodyMarked() != Body[CurrentRowSelected]);
            return !bEdited;
        }

        private bool bPageSaved()
        {
            if (bNothingToSave()) return true;

            DialogResult Res1 = MessageBox.Show("Macro was not saved", "Click OK to save this macro", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (Res1 == DialogResult.OK)
            {
                SaveCurrentMacros();
                return true;
            }
            return false;
        }

        private void btnChangeUrls_Click(object sender, EventArgs e)
        {
            ManageMacros MyManageMac = new ManageMacros(strType, NumMacros, ref Body);
            MyManageMac.ShowDialog();
            tbBody.Text = MyManageMac.AllBody[CurrentRowSelected];
            SaveCurrentMacros();
            MyManageMac.Dispose();
        }

        private void btnAppendMac_Click(object sender, EventArgs e)
        {
            CreateMacro MyCM = new CreateMacro(strType);
            MyCM.ShowDialog();
            string strReturn = MyCM.strResultOut;
            if (strReturn == null) return;
            if (strReturn == "") return;
            tbBody.Text += strReturn;
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
            help MyHelp = new help(sHelp);
            MyHelp.Show();
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
                    /*
                    Application.DoEvents();
                    MessageBox.Show("Original macro is on notepad",
                            "Macro " + (r+1).ToString(),MessageBoxButtons.OKCancel);
                    */
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strErr = Utils.BBCparse(tbBody.Text);
            if (strErr == "") return;
            DialogResult Res1 = MessageBox.Show(strErr,"Click OK to see where errors are",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            if(Res1 == DialogResult.OK)
            {
                Utils.ShowParseLocationErrors(tbBody.Text);
                MessageBox.Show(strErr, "Errors are near locations listed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbName_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ShowSelectedRow(e.RowIndex);
        }


        private void btnCancelEdits_Click(object sender, EventArgs e)
        {
            tbBody.Text = Body[CurrentRowSelected];
        }



        private void btnLinkAll_Click(object sender, EventArgs e)
        {
            string sBody = tbBody.Text.ToLower();
            bool bHasHyper = sBody.Contains("<a ") || sBody.Contains("<img ");
            if (bHasHyper) return; // probably need to explain why
            SwitchToMarkup(false);
            sBody = tbBody.Text;
            Utils.ReplaceUrls(ref sBody, true);
            tbBody.Text = sBody;
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
            SelectFileItem(Utils.LocalMacroPrefix[i]);
        }

        // find next file PC->PRN->HP and repeat back to PC
        private void btnNextTable_Click(object sender, EventArgs e)
        {
            PositionNext(1);
        }

        private int LoadFileItem(string sPrefix)
        {
            int n = LoadFromTXT(sPrefix);
            if(sPrefix != "HP")
            {
                SetVisDiffErr(false);
            }
            return n;
        }

        private void ShowEmpty(string sWanted)
        {
            lbName.Rows.Clear();
            for (int i = 0; i < NumMacros; i++) Body[i] = "";
            tbBody.Text = "";
            tbMacName.Text = "";
            strType = sWanted;
            this.Text = " HP Macro Editor";
            lbName.Columns[2].HeaderText = "Name " + sWanted;
        }

        private void SelectFileItem(string sPrefix)
        {
            if (Utils.NoFileThere(sPrefix))
            {
                ShowEmpty(sPrefix);
                return;
            }
            if(strType != sPrefix)
            {
                if(!bPageSaved())
                {
                    return; // user failed to save edits
                }
            }
            lbRCcopy.Visible = false;
            mMoveMacro.Visible = true;
            lbName.ReadOnly = false;
            strType = sPrefix;
            if(sPrefix == "HP")
            {
                ReloadHP(0);
            }
            else
            {
                AccessDiffBoth(false);
                LoadFromTXT(strType);
                EnableMacEdits(true);
                ShowUneditedRow(0);
                SetVisDiffErr(false);
            }
            AllowMacroMove(true);
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
            string sDirty = Clipboard.GetText();
            Utils.ReplaceUrls(ref sDirty,false);
            Clipboard.SetText(sDirty);
        }


        private void LoadAllFiles()
        {
            bInitialLoad = true;
            NumSupplementalSignatures = 0;
            bool bMustChange = Properties.Settings.Default.ChangeSig;
            string nSig = Properties.Settings.Default.EditedSig;    // the new supplemental signature
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
                        NumSupplementalSignatures += Utils.HasSupSig(ref Body[i]);
                        if (bMustChange)
                        {
                            cb.sBody = Utils.ReplaceSupSig(nSig, ref Body[i]);
                            Body[i] = cb.sBody;
                        }
                        else cb.sBody = Body[i];
                        cb.fKeys = "";
                        cBodies.Add(cb);
                    }
                    if(bMustChange && n > 0)
                    {
                        SaveAsTXT(s);
                    }
                    if (n != 0) strType = s;
                }
            }
            Properties.Settings.Default.ChangeSig = false;
            Properties.Settings.Default.Save();

            bInitialLoad = false;
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
            WordSearch ws = new WordSearch(ref cBodies);
            ws.ShowDialog();
            int i, n = ws.LastViewed;
            ws.Dispose();
            if(n >= 0 && bNothingToSave())
            {
                CBody cb = cBodies[n];
                LoadFromTXT(cb.File);
                i = Convert.ToInt32(cb.Number);
                ShowUneditedRow(i - 1);
            }
        }

        private void WordSearch_Click(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        private int CountItems(string s)
        {
            if (Utils.NoFileThere(s)) return 0;
            string[] sAll = File.ReadAllLines(Utils.FNtoPath(s));
            if (s == "HP")
            {
                int j = 0;
                for(int i = 0; i < sAll.Length;i+= 2)
                {
                    if (sAll[i].Length == 0) j++;
                }
                return j;
            }
            return sAll.Length / 2;
        }

        private void CountEmpties(ref CMoveSpace cms)
        {
            cms.neHP = CountItems("HP");
            cms.nePC = NumMacros - CountItems("PC");
            cms.neAIO = NumMacros - CountItems("AIO");
            cms.neLJ = NumMacros - CountItems("LJ");
            cms.neDJ = NumMacros - CountItems("DJ");
            cms.neOS = NumMacros - CountItems("OS");
        }
        private int CountChecks()
        {
            int n = 0;
            foreach (DataGridViewRow row in lbName.Rows)
            {
                if((bool)row.Cells[1].EditedFormattedValue) n++ ;
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
            CMoveSpace cms = new CMoveSpace();
            cms.strType = strType;
            cms.bRun = false;
            cms.bDelete = false;
            CountEmpties(ref cms);
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
        }

        private void btnSpecialWord_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Properties.Settings.Default.SpecialWord);
            SpecialUsed(false);
        }

        private void main_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            help MyHelp = new help("FILE");
            MyHelp.Show();
        }
    }
}