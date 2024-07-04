using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static MacroViewer.Utils;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroViewer
{

    public partial class Settings : Form
    {
        public eBrowserType eBrowser {  get; set; }
        public bool bWantsExit { get; set; }
        public string userid { get; set; }
        private cMacroChanges xMC;
        private List<string> stDataSource = new List<string>();
        private List<long> stDateTime = new List<long>();
        private DateTime ThisDT;
        private List<int> SrtInx;
        public Settings(eBrowserType reBrowser, string ruserid, int NumAttached, ref cMacroChanges RxMC)
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
            string sPP = Properties.Settings.Default.sPPrefix;
            if (sPP != "init") tbPP.Text = sPP;
            tbLongAllowed.Text = Properties.Settings.Default.LongestExpectedURL.ToString();
            tbSpecialWord.Text = Properties.Settings.Default.SpecialWord;
            tbEmail.Text = Properties.Settings.Default.sEmail;
            cbSaveUNK.Checked = Properties.Settings.Default.SaveUnkUrls;
            cbDisableVPaste.Checked = Properties.Settings.Default.Vdisable;
            cbRepeatSearch.Checked = Properties.Settings.Default.WSrepeat;
            lbSaveLoc.Text = Utils.WhereExe + "\\UrlDebug.txt";
            CountUnkUrls();
            bWantsExit = false;
            xMC = RxMC;
            cbFileN.DataSource = xMC.GetFNChanges();
            xMC.nSelectedRowIndex = -1;
            if (cbFileN.Items.Count > 0 )
            {
                //string s = cbFileN.Items[0].ToString();
                //SrtInx = xMC.GetMNChanges(s, ref stDataSource, ref stDateTime);
                gbChanged.Visible = true;
            }

            xMC.sGoTo = "";
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
            Properties.Settings.Default.sPPrefix = tbPP.Text;
            Properties.Settings.Default.sMSuffix = tbMSuffix.Text;
            Properties.Settings.Default.LongestExpectedURL = Convert.ToInt32(tbLongAllowed.Text);
            Properties.Settings.Default.Vdisable = cbDisableVPaste.Checked;
            Properties.Settings.Default.WSrepeat = cbRepeatSearch.Checked;
            Utils.nLongestExpectedURL = Properties.Settings.Default.LongestExpectedURL;
            Utils.BrowserWanted = eBrowser;
            if (tbUserID.Text != "")
            { 
                userid = tbUserID.Text;
                Properties.Settings.Default.UserID = userid;
            }
            Utils.VolunteerUserID = userid;
            Properties.Settings.Default.SaveUnkUrls = cbSaveUNK.Checked;
            Properties.Settings.Default.SpecialWord = tbSpecialWord.Text;
            Properties.Settings.Default.sEmail = tbEmail.Text;
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



        private void ShowText(string strTemp)
        {
            if (strTemp == "") return;
            Utils.ShowPageInBrowser("", strTemp);
        }


        private void btnTestPP_Click(object sender, EventArgs e)
        {
            ShowText(tbPP.Text);
        }

        private void btnClrM_Click(object sender, EventArgs e)
        {
            xMC.ClearChanges();
            lbEdited.Items.Clear();
            gbChanged.Visible = false;
        }

        private void cbFileN_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = cbFileN.SelectedIndex;
            string s = cbFileN.Items[n].ToString();
            List<string> xstDataSource = new List<string>();
            List<long> xstDateTime = new List<long>();
            lbEdited.DataSource = null;
            lbEdited.Invalidate();
            SrtInx = xMC.GetMNChanges(s, ref xstDataSource, ref xstDateTime);
            stDataSource.Clear();
            stDateTime.Clear();
            foreach (int i in SrtInx)
            {
                stDataSource.Add(xstDataSource[i]);
                stDateTime.Add(xstDateTime[i]); 
            }
            lbEdited.DataSource = stDataSource;
            lbEdited.Refresh();
            xMC.nSelectedRowIndex = 0;
        }


        private void btnExitSelect_Click(object sender, EventArgs e)
        {
            int n = xMC.nSelectedRowIndex;
            if(n < 0 || n >= lbEdited.Items.Count) { return; }
            string rowContent = lbEdited.Items[n].ToString();
            string sFN = cbFileN.Text;
            xMC.sGoTo = sFN + ":" + rowContent;
            this.Close();
        }

        private void lbEdited_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string formattedDate = date.ToString("MMMM dd yyyy, hh:mm tt");
            //  July 04 2024, 01:45 PM
            xMC.nSelectedRowIndex = lbEdited.SelectedIndex;
            int n = xMC.nSelectedRowIndex;
            if (n < 0)
            {
                return;
            }
            ThisDT = new DateTime(stDateTime[n]);
            tbDateChg.Text = ThisDT.ToString("MMMM dd yyyy, hh:mm tt");
        }
    }
}
