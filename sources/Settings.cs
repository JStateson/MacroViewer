using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MacroViewer.Utils;

namespace MacroViewer
{
    public partial class Settings : Form
    {
        public eBrowserType eBrowser {  get; set; }
        public string userid { get; set; }
        public List<CLocalFiles> LocalImageFiles;
        public Settings(eBrowserType reBrowser, string ruserid)
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
            FillLocalImageTable();
            tbSupSig.Text = Properties.Settings.Default.SupSig;
            cbSaveUNK.Checked = Properties.Settings.Default.SaveUnkUrls;
            lbSaveLoc.Text = Utils.WhereExe + "\\UrlDebug.txt";
            CountUnkUrls();
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

        private void FillLocalImageTable()
        {
            string strpath = "";
            string strTotal = "";
            int i = 0;
            int r = dgvUsedImages.Size.Width;
            LocalImageFiles = Utils.NumLocalImageFiles();
            foreach(string s in Utils.LocalMacroPrefix)
            {
                strpath = Utils.FNtoPath(s);
                if (!File.Exists(strpath)) continue;
                strTotal += File.ReadAllText(strpath);
            }
            foreach(CLocalFiles cf in LocalImageFiles)
            {
                if(strTotal.Contains(cf.Name))
                {
                    LocalImageFiles[i].NotUsed = false;
                }
                i++;
            }
            dgvUsedImages.DataSource = LocalImageFiles;
            dgvUsedImages.Columns[0].Width = 64;
            dgvUsedImages.Columns[1].Width = r - dgvUsedImages.Columns[0].Width - 36;
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
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnDelUnused_Click(object sender, EventArgs e)
        {
            int NumDeleted = 0;
            foreach(CLocalFiles cf in LocalImageFiles)
            {
                if(cf.NotUsed)
                {
                    string sFile = Utils.WhereExe + "\\" + cf.Name;
                    try
                    {
                        File.Delete(sFile);
                        NumDeleted++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex.Message, "Unable to delete", MessageBoxButtons.OKCancel);
                    }
                }
            }
            if (NumDeleted > 0)
            {
                LocalImageFiles.Clear();
                FillLocalImageTable();
            }
        }

        private void btnAddSupSig_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SupSig = tbSupSig.Text.ToString();
            Properties.Settings.Default.ChangeSig = true;
            Properties.Settings.Default.Save();
            MessageBox.Show("The program must restarted to update the supplemental signature");
        }

        private void btnDelURL_Click(object sender, EventArgs e)
        {
            File.Delete(lbSaveLoc.Text);
        }

        private void btnShowURL_Click(object sender, EventArgs e)
        {
            Utils.NotepadViewer(lbSaveLoc.Text);
        }
    }
}
