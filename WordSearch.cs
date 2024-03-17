using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Diagnostics.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroViewer
{
    public partial class WordSearch : Form
    {
        private int SelectedRow = -1;
        private List<CFound> cFound;
        private List<CBody> cAll;
        private string[] keywords;
        private bool[] KeyPresent;

        public WordSearch(ref List<CBody> Rcb)
        {
            InitializeComponent();
            cFound = new List<CFound>();
            cAll = Rcb;
        }



        private void dgvSearched_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = cFound[SelectedRow].WhereFound;
            string strTemp = cAll[n].sBody;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }

        private void dgvSearched_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            int n = cFound[SelectedRow].WhereFound;
            tbKeysFound.Text = cAll[n].fKeys;
        }


        private string DoSearch(string text)
        {
            string strRtn = "";
            int i, n = keywords.Length;
            for (i = 0; i < n; i++) KeyPresent[i] = false;
            i = 0;
            foreach (string keyword in keywords)
            {
                Regex regex = new Regex("\\b" + Regex.Escape(keyword) + "\\b", RegexOptions.IgnoreCase);
                Match match = regex.Match(text);
                if (match.Success)
                {
                    KeyPresent[i] = true;
                    strRtn+= match.Value + " ";   
                    //Console.WriteLine($"Found keyword '{keyword}' at index {match.Index}");
                }
                i++;
            }
            return strRtn;
        }

        private void RunSearch()
        {
            cFound.Clear();
            keywords = tbKeywords.Text.Split(new char[] { ' ', '\t' });
            int n = keywords.Length;
            int i = 0;
            KeyPresent = new bool[n];
            int[] cWidth = new int[4] { 32, 64, 64, 432 };
            foreach (CBody cb in cAll)
            {
                string sKeys = DoSearch(cb.sBody);
                if (sKeys != "")
                {
                    n = 0;
                    CFound cf = new CFound();
                    foreach (bool b in KeyPresent)
                    { if (b) { n++; } }
                    cAll[i].fKeys = sKeys;
                    cf.Name = cb.Name;
                    cf.Number = cb.Number;
                    cf.File = cb.File;
                    cf.Found = n.ToString();
                    cf.WhereFound = i;
                    cFound.Add(cf);
                }
                else
                {
                    cAll[i].fKeys = "";
                }
                i++;
            }
            dgvSearched.DataSource = cFound.ToArray();
            int j = 0;
            foreach (int k in cWidth)
                dgvSearched.Columns[j++].Width = k;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RunSearch();
        }

        private void tbKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RunSearch();
            }
        }
    }
}
