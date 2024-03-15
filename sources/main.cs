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
        private int [] OriginalColor = new int[NumMacros];  //0:was OK to start with, 1:red, 2:blue
        // if HTMLerr set but HPerr is not set then can copy to clipboard with right click
        private bool[] bHPcorrected = new bool[NumMacros];
        private string aPage;
        private int[] StartMac = new int[NumMacros];
        private int[] StopMac = new int[NumMacros];
        int[] MacBody = new int[NumMacros];
        private string[] Body = new string[NumMacros];
        private string[] Body_HP_HTML = new string[NumMacros]; //local copy of ORIGINAL HP macros
        private string strType = "";    // either PRN or PC for printer or pc macros or HP 
        private string TXTName = "";
        private int CurrentRowSelected = -1;
        private OpenFileDialog ofd;
        private bool bMacroErrors;
        private bool bHaveHTML = false; // html macro was read in. this cannot be edited

        //CSendCloud SendToCloud = new CSendCloud();

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
        }

        private bool AnyHPdiff()
        {
            bool b = true;
            for (int i = 0; i < NumInBody; i++)
            {
                if (bHaveHTMLasLOCAL)
                {
                    HP_HTML_NO_DIFF[i] = (Body[i] == Body_HP_HTML[i]);
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
            for(int i = 0; i < NumInBody; i++)
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
                if(HTMLerr[i])
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
            for(int i = 0; i< NumInBody; i++)
            {
                Body_HP_HTML[i] = Body[i];
            }
        }

        private void ShowHighlights()
        {
            if (bHaveBothHP)
            {
                if (bShowingError && bHaveBothHPerr)
                {
                    mShowDiff.Text = "Show Errors";
                    bShowingError=false;
                    bShowingDiff = true;
                    HighlightDIF();
                    return;
                }
                if(bShowingDiff && bHaveBothDIFF)
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
                switch(OriginalColor[i])
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
                strBody = strBody.Replace("&nbsp;", " ");
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
                lbName.Rows.Add(i + 1, strName);
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
            aPage = File.ReadAllText(strFilename);
            if(aPage == null) return false;
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


        private void HaveSelected(bool bVal)
        {
            btnSaveM.Enabled = bVal;
            btnDelM.Enabled = bVal;
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
            tbMacName.Text = lbName.Rows[CurrentRowSelected].Cells[1].Value.ToString();
            lbName.ClearSelection();
            lbName.Rows[CurrentRowSelected].Selected = true;
            if (lbNotM.Visible) SwitchToMarkup(false);
            if (cbLaunchPage.Checked)
                RunBrowser();
        }

        private void ShowSelectedRow(int e)
        {
            if(bPageSaved())
            {
                ShowUneditedRow(e);
            }
        }

        private void lbName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            RunBrowser();
        }

        private void utilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utils MyUtils = new utils();
            MyUtils.Show();
            //MyUtils.ShowDialog();
            //MyUtils.Dispose();
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
            RichTextBox rtb= new RichTextBox();
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
            tbBody.Text = Clipboard.GetText();
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
            string npTitle =  strIn;
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

        private void readHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessDiffBoth(false);
            mShowDiff.Visible = false;
            mShowErr.Visible = false;
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
            }
        }

        private void LoadFromTXT(string strFN)
        {
            int i = 0;
            bMacroErrors = false;
            mShowErr.Visible = false;
            TXTName = strFN;
            gpMainEdit.Enabled = true;
            gbSupp.Enabled = true;
            string TXTmacName = Utils.WhereExe + "\\" + strFN + ".txt";
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
                    lbName.Rows.Add(i + 1, line);   // this triggers row enter callback
                    MacroNames[i] = line;
                    sBody = sr.ReadLine();
                    Body[i] = sBody.Replace("<br />","<br>");
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
                    line = sr.ReadLine();
                    NumInBody++;
                }
                lbName.RowEnter += lbName_RowEnter;
                sr.Close();
            }
            tbNumMac.Text = i.ToString();
            btnNew.Enabled = lbName.RowCount < NumMacros;
            if (strFN == "HPmacros") btnNew.Enabled = false;
        }

        private void loadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFileItem("PC");
        }

        private void SaveAsTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            string TXTmacName = Utils.WhereExe + "\\" + strFN + ".txt";
            foreach (DataGridViewRow row in lbName.Rows)
            {
                string strName = row.Cells[1].Value.ToString();
                if (strName == "") continue;
                string strBody = Body[i];
                strOut += strName + Environment.NewLine + strBody + Environment.NewLine;
                i++;
            }
            File.WriteAllText(TXTmacName, strOut);
        }

        private void saveToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!bHaveHTML) return;
                DialogResult Res1 = MessageBox.Show("This will overwrite HPmacros",
                    "Possible loss of macros", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Res1 == DialogResult.Yes)
            {
                SaveAsTXT("HPmacros");
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
                    lbName.Rows[i].Cells[1].Value = lbName.Rows[i + 1].Cells[1].Value;
                    Body[i] = Body[i + 1];
                }
            }
            lbName.Rows.RemoveAt(lbName.Rows.Count - 1);
            HaveSelected(lbName.RowCount > 0);
            return j;
        }

        private void EnableMacEdits(bool enable)
        {
            tbMacName.Enabled = enable;
            tbNumMac.Enabled = enable;
            btnDelM.Enabled = enable && CurrentRowSelected >= 0;
            btnSaveM.Enabled = enable && CurrentRowSelected >= 0;
        }


        private void btnDelM_Click(object sender, EventArgs e)
        {
            string strName = tbMacName.Text;
            int i = CurrentRowSelected + 1;
            if(strType != "HP")
            {
                DialogResult Res1 = MessageBox.Show("You are deleting the macro named " + strName,
"Deleting  macro " + i.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
                CurrentRowSelected = RemoveMacro();
            }
            else
            {
                DialogResult Res1 = MessageBox.Show("You are removing contents of the macro named " + strName,
"Deleting  macro " + i.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
                Body[CurrentRowSelected] = "";
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

        private void SaveCurrentMacros()
        {
            bool bChanged = false;
            string strName = tbMacName.Text;
            string strOld = "";
            if (lbName.RowCount == 0)
            {
                return; // must have wanted to add a row: sorry
            }
            strOld = lbName.Rows[CurrentRowSelected].Cells[1].Value.ToString();
            if (strName != strOld && (Utils.UnNamedMacro != strOld))
            {
                DialogResult Res1 = MessageBox.Show("This will overwrite " + strOld + " with " + strName,
        "Replaceing a macro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Res1 != DialogResult.Yes)
                {
                    return;
                }
            }
            lbName.Rows[CurrentRowSelected].Cells[1].Value = strName;
            Body[CurrentRowSelected] = RemoveNewLine(ref bChanged, tbBody.Text);
            if (TXTName == "HPmacros")
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
                if (lbName.Rows[i].Cells[1].Value.ToString() == strNewName)
                {
                    MessageBox.Show("Macro " + (i + 1).ToString() + " name must be unique!");
                    return false;
                }
            }
            if (CurrentRowSelected >= 0 && CurrentRowSelected < lbName.Rows.Count)
                lbName.Rows[CurrentRowSelected].Selected = false;
            CurrentRowSelected = lbName.Rows.Count;
            lbName.Rows.Add(CurrentRowSelected+1, strNewName);
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
            AddNew(tbMacName.Text.Trim(),tbBody.Text.Trim());
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            //<img src="https://h30434.www3.hp.com/t5/image/serverpage/image-id/362710iC75893BC32089485" border="2">
            string strTmp = tbImgUrl.Text.Trim().Replace("\"", "");
            string strImgUrl = "<img src=\"" + strTmp + "\" border=\"2\">";
            tbBody.Text = tbBody.Text.Insert(tbBody.SelectionStart, strImgUrl);
        }



        private void btnAddURL_Click(object sender, EventArgs e)
        {
            //<a href="h ttps://h30434.www3.hp.com/t5/image/serverpage/image-id/363370i5BE16BA39E85139E/image-size/large/is-moderation-mode/true?v=v2&px=999" target="_blank">
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
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            tbBody.Text = tbBody.Text.Insert(i, "<br>");
            i += 4;
            tbBody.SelectionStart = i;
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            tbBody.Text = tbBody.Text.Insert(i, "<br><br>");
            i += 8;
            tbBody.SelectionStart = i;
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
            SelectFileItem("PRN");
        }

        private void ReloadHP(int r)
        {
            mShowDiff.Visible = false;
            lbRCcopy.Visible = false;
            bHaveHTMLasLOCAL = ReadLastHTTP();
            ShowUneditedRow(r);
            LoadFromTXT("HPmacros");
            strType = "HP";
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
        private void savePrinterMacsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFileItem("HP");
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
            AddNew(Utils.UnNamedMacro,"");
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

        private bool bPageSaved()
        {
            if (CurrentRowSelected < 0 || strType == "") return true; // nothing to save 
            bool bEdited = (tbBodyMarked() != Body[CurrentRowSelected]);
            if (bEdited)
            {
                DialogResult Res1 = MessageBox.Show("Macro was not saved", "Click OK to save this macro", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Res1 == DialogResult.OK)
                {
                    SaveCurrentMacros();
                    return true;
                }
                return false;
            }
            return true;
        }

        private void btnChangeUrls_Click(object sender, EventArgs e)
        {
            ManageMacros MyManageMac = new ManageMacros(strType, ref Body);
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
            Settings MySettings = new Settings(Utils.BrowserWanted, Utils.VolunteerUserID);
            MySettings.ShowDialog();
            Utils.BrowserWanted = MySettings.eBrowser;
            Utils.VolunteerUserID = MySettings.userid;
            MySettings.Dispose();
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
                    //CopyBodyToClipboard();
                    string s = tbMacName.Text + Environment.NewLine;
                    PutOnNotepad(s+Body_HP_HTML[r]);
                    Application.DoEvents();
                    MessageBox.Show("Original macro is on notepad",
                            "Macro " + (r+1).ToString(),MessageBoxButtons.OKCancel);
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


        private class CMarkup
        {
            public class cFiller
            {
                public string sFiller;
                public string OldUrl;
                public string NewUrl;
            }
            public List<cFiller> cFillerList;
            private void cReplace(int n, ref string sBody, int iLoc, int iLen)
            {
                cFiller cf = new cFiller();
                cf.OldUrl = sBody.Substring(iLoc, iLen);
                cf.sFiller = Utils.strFill(n, iLen);
                sBody = sBody.Replace(cf.OldUrl, cf.sFiller);
                cf.NewUrl = Utils.FormUrl(cf.OldUrl, "");
                cFillerList.Add(cf);
            }
            
            public void Init()
            {
                cFillerList = new List<cFiller>();  
            }

            private int LengthURL(ref string sBody, int iStart)
            {
                int n = -1;
                string s = sBody.Substring(iStart);
                foreach(char c in s)
                {
                    n++;
                    if(c == ' ') return n;
                    if(c == '\n') return n;
                    if(c == '\r') return n;
                    if(c == '\t') return n;
                }
                return s.Length;
            }

            public bool FindUrl(int nLooked, ref string s)
            {

                int iHTTP = 0, iLen = 0;
                string sTMP = s.ToLower();
                iHTTP = sTMP.IndexOf("http");
                if (iHTTP < 0) return false;
                iLen = LengthURL(ref s, iHTTP);
                string strFound = s.Substring(iHTTP, iLen);
                cReplace(nLooked, ref s, iHTTP, iLen);
                return true;
            }
        }

        private void btnLinkAll_Click(object sender, EventArgs e)
        {
            string sBody = tbBody.Text.ToLower();
            bool bHasHyper = sBody.Contains("<a ") || sBody.Contains("<img ");
            if (bHasHyper) return; // probably need to explain why
            SwitchToMarkup(false);
            sBody = tbBody.Text;
            int n = 0;
            CMarkup MyMarkup = new CMarkup();
            MyMarkup.Init();
            while(true)
            {
                bHasHyper = MyMarkup.FindUrl(n, ref sBody);
                if (!bHasHyper) break;
                n++;
            }
            while(n > 0)
            {
                n--;
                CMarkup.cFiller cf = MyMarkup.cFillerList[n];
                sBody = sBody.Replace(cf.sFiller,cf.NewUrl);
            }
            tbBody.Text = sBody;
        }

        // find next file PC->PRN->HP and repeat back to PC
        private void btnNextTable_Click(object sender, EventArgs e)
        {
            int n = Utils.LocalMacroPrefix.Length;
            int i = 0;
            foreach (string s in Utils.LocalMacroPrefix)
            {
                if(strType.Contains(s))
                {
                    i++;
                    if (i == n) i = 0;
                    SelectFileItem(Utils.LocalMacroPrefix[i]);
                    return;
                }
                i++;
            }
            SelectFileItem("PC");
        }

        private void SelectFileItem(string sPrefix)
        {
            if(strType != sPrefix)
            {
                if(!bPageSaved())
                {
                    return; // user failed to save edits
                }
            }
            lbRCcopy.Visible = false;
            switch (sPrefix)
            {
                case "PC":
                    AccessDiffBoth(false);
                    LoadFromTXT("PCmacros");
                    strType = "PC";
                    EnableMacEdits(true);
                    ShowUneditedRow(0);
                    break;
                case "PRN":
                    AccessDiffBoth(false);
                    LoadFromTXT("PRNmacros");
                    strType = "PRN";
                    EnableMacEdits(true);
                    ShowUneditedRow(0);
                    break;
                case "HP":
                    ReloadHP(0);
                    break;
            }
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
    }
}
