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
            public class cUrls
            {
                public string sHref;
                public string sText;
                public string sOrig;
                public string sChanged;
                public bool bChanged;
                public bool bIsImage;
            }
            public List<cUrls> UrlInfo;
            public string sText;
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

            public int UpdatePage()
            {
                int i = 0;
                foreach(cUrls cu in UrlInfo)
                {
                    if(cu.bChanged)
                    {
                        sText = sText.Replace(cu.sOrig, cu.sChanged);
                        i++;
                    }
                }
                return i;
            }

            public bool AnyChange(int n)
            {
                bool b = !(UrlInfo[n].sChanged == UrlInfo[n].sOrig);
                UrlInfo[n].bChanged = b;
                return b;
            } 

            //<a is done here
            private void ProcessA(string sLC)
            {
                int i = 0, j, k;

                while (true)
                {
                    j = sLC.IndexOf("<a", i);
                    if (j < 0) break;
                    i = j + 2;
                    k = sLC.IndexOf("</a>", i);
                    Debug.Assert(k > 0);
                    k += 4;
                    cUrls cu = new cUrls();
                    string t = sText.Substring(j, k - j);
                    bool b = GetHT(t, ref cu.sHref, ref cu.sText);
                    if (b)
                    {
                        cu.sOrig = t;
                        cu.sChanged = t;
                        cu.bChanged = false;
                        cu.bIsImage = false;
                        UrlInfo.Add(cu);
                    }
                    i = k;
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

                while (true)
                {
                    j = sLC.IndexOf("<img ", i);
                    if (j < 0) break;
                    i = j + 5;
                    k = sLC.IndexOf(">", i);
                    Debug.Assert(k > 0);
                    k++;
                    cUrls cu = new cUrls();
                    string t = sText.Substring(j, k - j);
                    cu.sText = "";
                    bool b = GetSRC(t, ref cu.sHref);
                    if (b)
                    {
                        cu.sOrig = t;
                        cu.sChanged = t;
                        cu.bChanged = false;
                        cu.bIsImage = true;
                        UrlInfo.Add(cu);
                    }
                    i = k;
                }
            }
            public void Init(string s)
            {
                UrlInfo = new List<cUrls>();
                sText = s;
                ProcessA(s.ToLower());
                ProcessI(s.ToLower());
            }
        }

        public string strResultOut { get; set; }
        public cMyUrls mU;
        public int nSelectedM = -1;
        string sLBatext;
        string sLBimage;
        string[] sImgOpt;
        public EditOldUrls(string rText)
        {
            InitializeComponent();
            sLBatext = lbText.Text;
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
        }

        private void AnyChange()
        {
            lbChanged.Visible = mU.AnyChange(nSelectedM);
        }

        private void ShowImgTip()
        {
            lbText.Text = sLBatext;
            if (mU.UrlInfo[nSelectedM].bIsImage)
            {
                if (mU.UrlInfo[nSelectedM].sHref.Contains(Utils.sIsAlbum))
                {
                    string s = "Put just before the quote \" delimiter below" + Environment.NewLine;
                    for (int i = 0; i < sImgOpt.Length; i++)
                    {
                        s+= sImgOpt[i] + Environment.NewLine;
                    }
                    tbT.Text = s;
                    lbText.Text = "Image options:";
                }
            }
        }

        private void cbMacroList_SelectedIndexChanged(object sender, EventArgs e)
        {
            nSelectedM = cbMacroList.SelectedIndex;
            if (nSelectedM >= 0)
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sText;
                tbH.Text = mU.UrlInfo[nSelectedM].sHref;
                tbResult.Text = mU.UrlInfo[nSelectedM].sChanged;
                tbT.ReadOnly = mU.UrlInfo[nSelectedM].bIsImage;
                if(mU.UrlInfo[nSelectedM].bIsImage)
                {
                    tbT.ReadOnly = true;
                }
                AnyChange();
                ShowImgTip();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Utils.ShowPageInBrowser("",tbResult.Text);
        }

        private void btnCancelChg_Click(object sender, EventArgs e)
        {
            if(nSelectedM >= 0)
            {
                tbT.Text = mU.UrlInfo[nSelectedM].sText;
                tbH.Text = mU.UrlInfo[nSelectedM].sHref;
                tbResult.Text = mU.UrlInfo[nSelectedM].sOrig;
                mU.UrlInfo[nSelectedM].sChanged = mU.UrlInfo[nSelectedM].sOrig;
                lbChanged.Visible = false;
            }

        }

        private void btnFormChg_Click(object sender, EventArgs e)
        {
            if(nSelectedM >= 0)
            {
                string s = mU.UrlInfo[nSelectedM].sOrig;
                s = s.Replace(mU.UrlInfo[nSelectedM].sHref, tbH.Text);
                if (!mU.UrlInfo[nSelectedM].bIsImage)
                    s = s.Replace(mU.UrlInfo[nSelectedM].sText, tbT.Text);
                tbResult.Text = s;
                Utils.SyntaxTest(s);
                AnyChange();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(nSelectedM >= 0)
            {
                mU.UrlInfo[nSelectedM].sChanged = tbResult.Text;
            }
            AnyChange();
        }

        private void btnApplyExit(object sender, EventArgs e)
        {
            if(mU.UpdatePage() > 0)
            {
                strResultOut = mU.sText;
            }
            this.Close();
        }

        private void btnCancelExit_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }
    }
}
