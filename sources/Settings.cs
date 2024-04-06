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
using static System.Net.Mime.MediaTypeNames;
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
            tbSpecialWord.Text = Properties.Settings.Default.SpecialWord;
            cbSaveUNK.Checked = Properties.Settings.Default.SaveUnkUrls;
            cbRefOnly.Checked = !Properties.Settings.Default.UseMarkers;
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
            cbListFN.Items.Add("ALL");
            foreach (string s in Utils.LocalMacroPrefix)
                cbListFN.Items.Add(s);
            cbListFN.SelectedIndex = 0;
            lbMarker.Text = lbMarker.Text.Replace("pre", Utils.SupSigPrefix).Replace("suf", Utils.SupSigSuffix);        }

     
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
                btnShowURL.Enabled = true;
                btnDelURL.Enabled = true;
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
            Properties.Settings.Default.SpecialWord = tbSpecialWord.Text;
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
                    Color selectedColor = colorDialog.Color;
                    tbSupSig.ForeColor = selectedColor;
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


        private void btnCpyEdit_Click(object sender, EventArgs e)
        {
            string sOut = "";
            if (clbImages.GetItemCheckState(0) == CheckState.Checked)
                sOut = Utils.ApplyColors1(ref tbSupSig) + "<br>";
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


        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbEdit, 1);
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            Utils.AddNL(ref tbEdit, 2);
        }

        private void btnClearEB_Click(object sender, EventArgs e)
        {
            tbEdit.Text = "";
            Properties.Settings.Default.ChangeSig = "";
            PerformSave();
            btnAddSupSig.Enabled = false;
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
            Properties.Settings.Default.UseMarkers = !cbRefOnly.Checked;
            UpdateSupSignature();
        }

        private void UpdateSupSignature()
        {
            Properties.Settings.Default.ChangeSig = cbListFN.SelectedItem.ToString();
            PerformSave();
            DialogResult dr =   MessageBox.Show("The program must restarted to update the supplemental signature",
                    "Click OK to restart now",MessageBoxButtons.OKCancel);
            bWantsExit = (dr == DialogResult.OK);
            if (bWantsExit) this.Close();
        }

        private void SetRef(int n)
        {
            if (!cbRefOnly.Checked) return;
            if (n > 0)
            {
                tbEdit.Text = Utils.LocalMacroRefs[n-1];

            }
            else
            {
                tbEdit.Text = "";
            }
            tbSupSig.Text = tbEdit.Text;
        }

        private void cbListFN_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = cbListFN.SelectedIndex;
            if(cbRefOnly.Checked)
                SetRef(n);
        }

        private void LockRef(bool bRefOnly)
        {
            if (bRefOnly)
            {
                SetRef(cbListFN.SelectedIndex);
            }
            bRefOnly = !bRefOnly;
            btnColor.Enabled = bRefOnly;
            btnCpyEdit.Enabled = bRefOnly;
            btnFont.Enabled = bRefOnly;
            btnTest.Enabled = bRefOnly;
            clbImages.Enabled = bRefOnly;

        }

        private void cbRefOnly_CheckStateChanged(object sender, EventArgs e)
        {
            LockRef(cbRefOnly.Checked);
        }
    }
}
