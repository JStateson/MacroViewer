using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroViewer.sources
{


    public partial class EditOldUrls : Form
    {
        public class cMyUrls
        {
            private int n = 0;
            public class cUrls
            {
                public string sOrigHref;
                public string sOrigText;
                public string sProposedH;
                public string sProposedT;
                public string sOrigResult;
                public string sChangedResult;
                public bool bIsImage;
            }
            public List<cUrls> UrlInfo;
            public string sText;
            public string oText;
            public bool GetHT(string s, ref string sH, ref string sT)
            {
                string sLC = s.ToLower();
                int i = sLC.IndexOf("href=");
                if (i < 0) return false;
                i += 5;
                string c = s.Substring(i, 1);
                i++;
                int j = sLC.IndexOf(c, i);
                if (j < 0) return false;
                sH = s.Substring(i, j - i);
                i = s.IndexOf(">", j);
                if (i < 0) return false;
                i++;
                j = s.LastIndexOf("</a>");
                if (j < 0) return false;
                sT = s.Substring(i, j - i);
                return true;
            }
            
            public string GetUpdated()
            {
                n = 0;
                foreach(cUrls cu in UrlInfo)
                {
                    sText = sText.Replace(Utils.strFill(n,8),cu.sChangedResult);
                    n++;
                }
                return sText;
            }

            //<a is done here
            // use 1111, 2222, etc to use as pattern to replace else might get duplicate replacments
            private void ProcessA(string sLC)
            {
                int i = 0, j, k;
                string sH = "";
                string sT = "";
                while (true)
                {
                    j = sLC.IndexOf("<a", i);
                    if (j < 0) break;
                    i = j + 2;
                    k = sLC.IndexOf("</a>", i);
                    Debug.Assert(k > 0);
                    k += 4;
                    string t = sText.Substring(j, k - j);
                    bool b = GetHT(t, ref sH, ref sT);
                    if (b)
                    {
                        cUrls cu = new cUrls();
                        cu.sOrigResult = t;
                        cu.sChangedResult = t;
                        i = sText.IndexOf(t);
                        sText = Utils.ReplaceStringAtLoc(sText, n, i, t.Length);
                        n++;
                        cu.sProposedT = sT;
                        cu.sOrigText = sT;
                        cu.sProposedH = sH;
                        cu.sOrigHref = sH;
                        cu.bIsImage = false;
                        UrlInfo.Add(cu);
                    }
                    sLC = sText.ToLower();
                    i = 0;
                }
            }


            //<img src="https://h30434.www3.hp.com/t5/image/serverpage/image-id/362710iC75893BC32089485">
            private bool GetSRC(string s, ref string sSRC)
            {
                string sLC = s.ToLower();
                int i, j;
                i = s.IndexOf("src=");
                if (i < 0) return false;
                i += 4;
                string c = s.Substring(i, 1);
                i++;
                j = s.IndexOf(c, i);
                if(j < 0) return false;
                sSRC = s.Substring(i, j - i);
                return true;
            }
            private void ProcessI(string sLC)
            {
                int i = 0, j, k;
                string sH = "";
                string sT = "";
                while (true)
                {
                    j = sLC.IndexOf("<img ", i);
                    if (j < 0) break;
                    i = j + 5;
                    k = sLC.IndexOf(">", i);
                    Debug.Assert(k > 0);
                    k++;
                    string t = sText.Substring(j, k - j);
                    bool b = GetSRC(t, ref sH);
                    if (b)
                    {
                        cUrls cu = new cUrls();
                        cu.sOrigResult = t;
                        cu.sChangedResult = t;
                        i = sText.IndexOf(t);
                        sText = Utils.ReplaceStringAtLoc(sText, n, i, t.Length);
                        n++;
                        cu.sProposedT = sT;
                        cu.sOrigText = sT;
                        cu.sProposedH = sH;
                        cu.sOrigHref = sH;
                        cu.bIsImage = true;
                        UrlInfo.Add(cu);
                    }
                    sLC = sText.ToLower();
                    i = 0;
                }
            }
            public void Init(string s)
            {
                UrlInfo = new List<cUrls>();
                sText = s;
                oText = s;
                ProcessA(s.ToLower());
                ProcessI(s.ToLower());
            }
        }

        public string strResultOut { get; set; }
        public cMyUrls mU;
        public int nSelectedM = -1;
        string sLBatext;
        Color lbFC;
        string sLBimage = "Image Options";
        string[] sImgOpt;
        public bool bIsImage = false;
        public EditOldUrls(string rText)
        {
            InitializeComponent();

            sImgOpt = Utils.sDifSiz.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < sImgOpt.Length; i++)
            {
                sImgOpt[i] = Utils.sHasSize + sImgOpt[i];
            }
            mU = new cMyUrls();
            mU.Init(rText);
            cbMacroList.Items.Clear();
            for(int i = 0; i < mU.UrlInfo.Count; i++)
            {
                cbMacroList.Items.Add("Macro " + (i + 1).ToString());
            }
            cbMacroList.SelectedIndex = mU.UrlInfo.Count > 0 ? 0 : -1;
            nSelectedM = cbMacroList.SelectedIndex;
            sLBatext = gbText.Text;
            lbFC = lbChanged.ForeColor;
        }

        private void ShowImgTip()
        {
            gbText.Text = sLBatext;
            bIsImage = false;
            if (mU.UrlInfo[nSelectedM].bIsImage)
            {
                if (mU.UrlInfo[nSelectedM].sOrigHref.Contains(Utils.sIsAlbum))
                {
                    string s = "Put just before the quote \" delimiter below" + Environment.NewLine;
                    for (int i = 0; i < sImgOpt.Length; i++)
                    {
                        s+= sImgOpt[i] + Environment.NewLine;
                    }
                    tbT.Text = s;
                    gbText.Text = "Image options:";
                }
                else tbT.Text = "No HP options this image";
                bIsImage = true;
            }
        }

        private void cbMacroList_SelectedIndexChanged(object sender, EventArgs e)
        {
            nSelectedM = cbMacroList.SelectedIndex;
            if (nSelectedM >= 0)
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sProposedT;
                tbH.Text = mU.UrlInfo[nSelectedM].sProposedH;
                tbResult.Text = mU.UrlInfo[nSelectedM].sChangedResult;
                tbT.ReadOnly = mU.UrlInfo[nSelectedM].bIsImage;
                if(mU.UrlInfo[nSelectedM].bIsImage)
                {
                    tbT.ReadOnly = true;
                }
                ShowChange();
                ShowImgTip();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Utils.ShowPageInBrowser("",tbResult.Text);
        }

        private void ShowChange()
        {
            string sC = mU.UrlInfo[nSelectedM].sChangedResult;
            string sO = mU.UrlInfo[nSelectedM].sOrigResult;
            if (tbResult.Text == sO)
            {
                lbChanged.Text = "URL not changed";
                lbChanged.ForeColor = btnCancelExit.ForeColor;

            }
            else
            { 
                if(tbResult.Text == sC) lbChanged.Text = "Changed URL saved";
                else lbChanged.Text = "URL not saved";
                lbChanged.ForeColor = lbFC;
            }
        }

        private void btnCancelChg_Click(object sender, EventArgs e)
        {
            if(nSelectedM >= 0)
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sOrigText;
                tbH.Text = mU.UrlInfo[nSelectedM].sOrigHref;
                tbResult.Text = mU.UrlInfo[nSelectedM].sOrigResult;
                mU.UrlInfo[nSelectedM].sChangedResult = tbResult.Text;
                ShowChange();
            }

        }

        private void FormChange()
        {
            if (nSelectedM >= 0)
            {
                string s = "";
                if (bIsImage)
                {
                    s = Utils.AssembleIMG(tbH.Text);
                }
                else
                {
                    s = Utils.FormUrl(tbH.Text, tbT.Text);
                }
                tbResult.Text = s;
            }
            ShowChange();
        }

        private void SaveChange()
        {
            mU.UrlInfo[nSelectedM].sChangedResult = tbResult.Text;
            ShowChange();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChange();
        }

        private void btnApplyExit(object sender, EventArgs e)
        {
            strResultOut = mU.GetUpdated();
            this.Close();
        }

        private void btnCancelExit_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }


        private void btnClrH_Click(object sender, EventArgs e)
        {
            tbH.Text = "";
        }

        private void btnCanH_Click(object sender, EventArgs e)
        {
            tbH.Text = mU.UrlInfo[nSelectedM].sOrigHref;          
        }

        private void btnClrT_Click(object sender, EventArgs e)
        {
            tbT.Text = "";
        }

        private void btnCanT_Click(object sender, EventArgs e)
        {
            tbT.Text = mU.UrlInfo[nSelectedM].sOrigText;
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            FormChange();
        }
    }
}
