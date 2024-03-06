using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;


namespace MacroViewer
{
    public partial class main : Form
    {
        string aPage;
        const int NumMacros = 30;
        int[] StartMac = new int[NumMacros];
        int[] StopMac = new int[NumMacros];
        int[] MacBody = new int[NumMacros];
        string[] Body = new string[NumMacros];
        string strType = "";    // eithe PRN or PC for printer or pc macros
        string TXTmacs;
        string TXTName = "";
        int CurrentRowSelected = -1;
        OpenFileDialog ofd;


        //CSendCloud SendToCloud = new CSendCloud();

        public main()
        {
            InitializeComponent();
            TXTmacs = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            EnableMacEdits(false);
            SwitchToMarkup(true);
            //SendToCloud.Init();
            gbManageImages.Enabled = true;// System.Diagnostics.Debugger.IsAttached;
            Utils.BrowserWanted = (Utils.eBrowserType)Properties.Settings.Default.BrowserID;
            Utils.VolunteerUserID = Properties.Settings.Default.UserID;
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


        private bool FindBody()
        {

            string strFind; //<span class="html-attribute-value">profilemacro_2</span>"&gt;</span>
            string strEnd = "<span class=\"html-tag\">";
            int j, k, n;
            for (int i = 0; i < NumMacros; i++)
            {
                strFind = "<span class=\"html-attribute-value\">profilemacro_" + (i + 1).ToString() + "</span>\"&gt;</span>";
                j = aPage.Substring(MacBody[i]).IndexOf(strFind);
                if (j < 0) return false;
                if (MacBody[i] == 0) continue;  // empty body
                n = MacBody[i] + j + strFind.Length;
                k = aPage.Substring(n).IndexOf(strEnd);
                if (k < 0) return false;
                string strBody = aPage.Substring(n, k).Replace("lt;", "<");
                strBody = strBody.Replace("&nbsp;", " ");
                Body[i] = strBody.Replace("&amp;", "").Replace("gt;", ">");  // cannot use "'"
            }
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
            for (int i = 0; i < NumMacros; i++)
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
            }
            tbNumMac.Text = NumUsed.ToString();
            return true;
        }



        private bool FindMacros()
        {
            int j, k;
            for (int i = 0; i < NumMacros; i++)
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
            aPage = File.ReadAllText(strFileName);
            nLength = aPage.Length;
            ParsePage();
            return true;
        }

        private void RunBrowser()
        {
            string strTemp = tbBody.Text;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(TXTmacs, strTemp);
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

        private void ShowSelectedRow(int e)
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
            if (cbLaunchPage.Checked)
                RunBrowser();
        }

        private void lbName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ShowSelectedRow(e.RowIndex);
        }

        private void utilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utils MyUtils = new utils();
            MyUtils.Show();
            //MyUtils.ShowDialog();
            //MyUtils.Dispose();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            string strOut = "";
            if (tbBody.Text == "") return;
            Clipboard.SetText(tbBody.Text);
        }

        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            tbBody.Text = Clipboard.GetText();
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




        private void btnToNotepad_Click(object sender, EventArgs e)
        {
            CSendNotepad SendNotepad = new CSendNotepad();
            SendNotepad.PasteToNotepad(tbBody.Text);

            //SendToCloud.PasteToCloud("7L4H9UA#ABA");
        }

        private void readHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ReadMacroHTML())
            {
                EnableMacEdits(false);
                gpMainEdit.Enabled = true;
                gbSupp.Enabled = false;
                strType = "";
            }            
        }

        private void LoadFromTXT(string strFN)
        {
            int i = 0;
            TXTName = strFN;
            gpMainEdit.Enabled = true;
            gbSupp.Enabled = true;
            string TXTmacName = TXTmacs + "\\" + strFN + ".txt";
            lbName.Rows.Clear();
            if (File.Exists(TXTmacName))
            {
                StreamReader sr = new StreamReader(TXTmacName);
                string line = sr.ReadLine();
                string sBody;
                while (line != null)
                {
                    lbName.Rows.Add(i + 1, line);
                    sBody = sr.ReadLine();
                    Body[i] = sBody;
                    i++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            tbNumMac.Text = i.ToString();
            btnNew.Enabled = lbName.RowCount < 30;
        }

        private void loadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromTXT("PCmacros");
            strType = "PC";
            ShowSelectedRow(0);
            EnableMacEdits(true);
        }

        private void SaveAsTXT(string strFN)
        {
            int i = 0;
            string strOut = "";
            TXTName = strFN;
            string TXTmacName = TXTmacs + "\\" + strFN + ".txt";
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
            DialogResult Res1 = MessageBox.Show("This will overwrite PCmacros",
                    "Possible loss of macros", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Res1 == DialogResult.Yes)
            {
                SaveAsTXT("PCmacros");
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
            DialogResult Res1 = MessageBox.Show("You are deleting the macro named " + strName,
"Deleting  macro " + i.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Res1 != DialogResult.Yes)
            {
                return;
            }
            CurrentRowSelected = RemoveMacro();
            SaveAsTXT(TXTName);
            LoadFromTXT(TXTName);
            ShowSelectedRow(CurrentRowSelected);
        }

        private void btnSaveM_Click(object sender, EventArgs e)
        {
            bool bChanged = false;
            string strName = tbMacName.Text;
            string strOld = "";
            if (lbName.RowCount == 0)
            {
                // must have wanted to add a row
                btnAddM_Click(sender, e);
                return;
            }
            strOld = lbName.Rows[CurrentRowSelected].Cells[1].Value.ToString();
            if (strName != strOld)
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
            SaveAsTXT(TXTName);
        }

        private string RemoveNewLine(ref bool bChanged, string strIn)
        {
            string strOut = strIn.Replace(Environment.NewLine, "<br>").Trim();
            // the above does not work for html as it has a newline
            strOut = strOut.Replace("\n", "<br>");
            bChanged = (strOut.Length != strIn.Length);
            return strOut;
        }


        private bool AddNew(string strNewName, string strBody)
        {
            bool bChanged = false;
            if (lbName.Rows.Count == 30)
            {
                MessageBox.Show("Can only hold 30 macros");
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
                    MessageBox.Show("Macro name must be unique: " + (i + 1).ToString());
                    return false;
                }
            }
            if (CurrentRowSelected >= 0 && CurrentRowSelected < lbName.Rows.Count)
                lbName.Rows[CurrentRowSelected].Selected = false;
            CurrentRowSelected = lbName.Rows.Count;
            lbName.Rows.Add(CurrentRowSelected+1, strNewName);
            Body[CurrentRowSelected] = RemoveNewLine(ref bChanged, strBody);
            tbBody.Text = strBody;
            SaveAsTXT(TXTName);
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
            LoadFromTXT("PRNmacros");
            strType = "PRN";
            ShowSelectedRow(0);
            EnableMacEdits(true);
        }

        private void savePrinterMacsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Res1 = MessageBox.Show("This will overwrite PRNmacros ",
        "Possible loss of macros", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (Res1 == DialogResult.Yes)
            {
                SaveAsTXT("PRNmacros");
            }
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
            AddNew("Need Name","");
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        private void btnChangeUrls_Click(object sender, EventArgs e)
        {
            ManageMacros MyManageMac = new ManageMacros(strType, ref Body);
            MyManageMac.ShowDialog();
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
            Settings MySettings = new Settings(ref Utils.BrowserWanted, ref Utils.VolunteerUserID);
            MySettings.ShowDialog();
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
    }
}
