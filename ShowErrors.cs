using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace MacroViewer
{
    public partial class ShowErrors : Form
    {
        private string[] sErrors;
        private int[] iErrors;
        public ShowErrors(ref string[] mName, ref string[] mErrors)
        {
            InitializeComponent();
            lbMacNames.Items.Clear();
            int i = 0, j = 0;
            sErrors = new string[mErrors.Length];
            iErrors = new int[mErrors.Length]; 
            foreach (string s in mName)
            {
                if (mErrors[i] == null) break;
                if (mErrors[i] != "")
                {
                    lbMacNames.Items.Add(s);
                    sErrors[j] = mErrors[i];
                    j++;
                }
                i++;
            }
        }

        private void lbMacNames_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void lbMacNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int r = lbMacNames.SelectedIndex;
            tbError.Text = sErrors[r];
        }
    }
}
