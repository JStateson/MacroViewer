using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static MacroViewer.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroViewer
{
    public partial class Settings : Form
    {
        public eBrowserType eBrowser {  get; set; }
        public bool bWantsExit { get; set; }
        public string userid { get; set; }

        private string strEditedSave = "";

        public Settings(eBrowserType reBrowser, string ruserid, int NumAttached)
        {
            InitializeComponent();
            userid = ruserid;
            tbUserID.Text = userid;
            eBrowser = reBrowser;
            switch (eBrowser)
            {
                case eBrowserType.eChrome: rbChrome.Checked = true; break;
                case eBrowserType.eFirefox: rbFirefox.Checked = true; break;
                case eBrowserType.eEdge: rbEdge.Checked = true; break;  
            }

            tbSupSig.Text = Properties.Settings.Default.SupSig;
            cbSaveUNK.Checked = Properties.Settings.Default.SaveUnkUrls;
            strEditedSave = Properties.Settings.Default.EditedSig;
            strEditedSave = strEditedSave.Replace(SupSigPrefix, "");
            strEditedSave = strEditedSave.Replace(SupSigSuffix, "");
            tbEdit.Text = strEditedSave;
            lbSaveLoc.Text = Utils.WhereExe + "\\UrlDebug.txt";
            CountUnkUrls();
            clbImages.Items.Clear();
            clbImages.Items.Add("Message", true);
            clbImages.Items.Add("Yes image", false);   // TODO to do todo this should come from the
            clbImages.Items.Add("Solution", false);     // test signature form and not be hard coded
            bWantsExit = false;
            lbAttached.Text = NumAttached.ToString();
        }

        private void CountUnkUrls()
        {
            if (File.Exists(lbSaveLoc.Text))
            {
                string AllUnkUrls = File.ReadAllText(lbSaveLoc.Text);
                int count = 0;
                foreach (char c in AllUnkUrls)
                {
                    if (c == '\n')
                    {
                        count++;
                    }
                }
                tbURLcnt.Text = "Total unknown " + count.ToString();
            }
            else tbURLcnt.Text = "No saved urls";
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rbChrome.Checked) eBrowser = eBrowserType.eChrome;
            if (rbEdge.Checked) eBrowser = eBrowserType.eEdge;
            if (rbFirefox.Checked) eBrowser = eBrowserType.eFirefox;
            Properties.Settings.Default.BrowserID = (int)eBrowser;
            Utils.BrowserWanted = eBrowser;
            if (tbUserID.Text != "")
            { 
                userid = tbUserID.Text;
                Properties.Settings.Default.UserID = userid;
            }
            Utils.VolunteerUserID = userid;
            Properties.Settings.Default.SaveUnkUrls = cbSaveUNK.Checked;
            Properties.Settings.Default.SupSig = tbSupSig.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }



        private void btnDelURL_Click(object sender, EventArgs e)
        {
            File.Delete(lbSaveLoc.Text);
        }

        private void btnShowURL_Click(object sender, EventArgs e)
        {
            Utils.NotepadViewer(lbSaveLoc.Text);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected color
                    Color selectedColor = colorDialog.Color;

                    // Use the selected color
                    tbSupSig.ForeColor = selectedColor;
                    //BackColor = selectedColor;
                    //tbSupSig.Text = $"Selected Color: {selectedColor.Name}";
                }
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected font
                    Font selectedFont = fontDialog.Font;

                    // Use the selected font
                    tbSupSig.Font = selectedFont;
                }
            }
        }

        /*
            public static string SupSigPrefix = "<b><font color=\"#f80000\">====";
            public static string SupSigSuffix = "====</font></b>";
         */
        private string ColorToHtml(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        private void btnCpyEdit_Click(object sender, EventArgs e)
        {
            string sNBI = "";
            string sSiz = "";
            string sCol = "";
            string sFs = "";
            string sFe = "";
            string sS, sE;
            string sOut = "";
            string sFONTs = "";
            float fontSize = tbSupSig.Font.Size - 5;  // html seems to need -5 to scale good
            int iSize = (int)Math.Ceiling(fontSize);
            sFONTs = fontSize.ToString("0.00");
            if (tbEdit.Font.Style == FontStyle.Bold) sNBI = "b";
            if (tbEdit.Font.Style == FontStyle.Italic) sNBI = "i";
            Color color = Color.FromArgb( tbSupSig.ForeColor.ToArgb());
            sCol = ColorToHtml(color);
            sS = (sNBI == "") ? "" : "<" + sNBI + ">";
            sE = (sNBI == "") ? "" : "</" + sNBI + ">";
            if(sCol != "#000000")
            {
                sFs = "<font color =\"" + sCol + "\" size=\"" + sFONTs + "\">";
                sFe = "</font>";
            }
            if (clbImages.GetItemCheckState(0) == CheckState.Checked) sOut =
                    sS + sFs + tbSupSig.Text + sFe + sE + "<br>";
            // TODO the following YES and SOLUTION probably need to come from the signature form
            if (clbImages.GetItemCheckState(1) == CheckState.Checked) sOut += Utils.YesButton + "  ";
            if (clbImages.GetItemCheckState(2) == CheckState.Checked) sOut += Utils.SolButton + "<br>";
            tbEdit.Text = sOut;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strTemp = tbEdit.Text;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }

        private void btnSavEdits_Click(object sender, EventArgs e)
        {
            if (tbEdit.Text == "") return;
            btnAddSupSig.Enabled = true;
            PerformSave();
        }

        private void PerformSave()
        {
            string strTemp = NoTrailingNL(tbEdit.Text.Replace(Environment.NewLine, "<br>"));
            Properties.Settings.Default.EditedSig = strTemp;
            Properties.Settings.Default.SupSig = tbSupSig.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void AddNL(int n)
        {
            int i = tbEdit.SelectionStart;
            int j = tbEdit.Text.Length;
            if(i == 0)
            {
                if (j > 0)
                {
                    if (!tbEdit.Focused)
                    {
                        i = j;
                        tbEdit.Focus();
                    }
                }
            }
            tbEdit.Text = tbEdit.Text.Insert(i, "<br><br>".Substring(0, n * 4)); ;
            i += 4*n;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = 0;
            tbEdit.Focus();
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            AddNL(1);
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            AddNL(2);
        }

        private void btnClearEB_Click(object sender, EventArgs e)
        {
            tbEdit.Text = "";
        }

        private void btnCopy2Clip_Click(object sender, EventArgs e)
        {
            if (tbEdit.Text == "") return;
            Clipboard.SetText(tbEdit.Text);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            tbEdit.Text = "";
            UpdateSupSignature();
        }
         
        private void btnAddSupSig_Click(object sender, EventArgs e)
        {
            UpdateSupSignature();
        }

        private void UpdateSupSignature()
        {
            Properties.Settings.Default.ChangeSig = true;
            PerformSave();
            DialogResult dr =   MessageBox.Show("The program must restarted to update the supplemental signature",
                    "Click OK to restart now",MessageBoxButtons.OKCancel);
            bWantsExit = (dr == DialogResult.OK);
            if (bWantsExit) this.Close();
        }
    }
}
