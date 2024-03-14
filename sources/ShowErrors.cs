using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroViewer
{
    public partial class ShowErrors : Form
    {
        private string[] sErrors;   // length 30
        private int[] iErrors;      // where in sError the non-blank error message and length 30
        private int[] BodyFromRow;  // could be as small as 1 or as large as 30 but holds 30
        private string[] AllBody;   // length 30
        private string sLoc = "";

        //i is the row selected
        private void MakeBodyAvailable(int i)
        {
            if (i < 0) return;
            int j = BodyFromRow[i];
            //File.WriteAllText(sLoc + "/MyHtml.html", Utils.XMLprefix + AllBody[j] + Utils.XMLsuffix);
            File.WriteAllText(sLoc + "/MyHtml.txt", AllBody[j]);
        }

        public ShowErrors(ref string[] mName, ref string[] mErrors, ref string[] rAllBody)
        {
            InitializeComponent();
            sLoc = Utils.WhereExe;
            AllBody = rAllBody;
            lbMacNames.Items.Clear();
            int i = 0, j = 0;
            sErrors = new string[mErrors.Length];
            iErrors = new int[mErrors.Length]; 
            BodyFromRow = new int[mErrors.Length];
            foreach (string s in mName)
            {
                if (mErrors[i] == null) break;
                if (mErrors[i] != "")
                {
                    lbMacNames.Items.Add(s);
                    sErrors[j] = mErrors[i];
                    BodyFromRow[j] = i;
                    j++;
                }
                i++;
            }
            btnFindErr.Text = lbMacNames.Items[0].ToString();
            MakeBodyAvailable(0);
            lbMacNames.SelectedIndex = 0;
        }

        private void lbMacNames_MouseClick(object sender, MouseEventArgs e)
        {
            int index = lbMacNames.IndexFromPoint(e.Location);
            lbMacNames.SelectedIndex = index;
        }

        private void lbMacNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int r = lbMacNames.SelectedIndex;
            if (r < 0) return;
            tbError.Text = sErrors[r];
            btnFindErr.Text = lbMacNames.Items[r].ToString();
        }

        private void btnFindErr_Click(object sender, EventArgs e)
        {
            string strLoc = sLoc + "\\MyHtml.txt";
            MakeBodyAvailable(lbMacNames.SelectedIndex);
            Utils.NotepadViewer(strLoc);
        }
    }
}
