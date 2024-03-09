using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MacroViewer
{
    public partial class help : Form
    {
        private static string[] strForms = { "FILE", "SIG", "EDIT", "MANAGE", "UTILS", "EDITLINK", "XMLERRORS" };
        private void MakeVisible(string sHelp)
        {
            rFILE.Visible = (sHelp == "FILE");
            rUTIL.Visible = (sHelp == "UTILS");
            rEDIT.Visible = (sHelp == "EDIT");
            rSIG.Visible = (sHelp == "SIG");
            rXMLERR.Visible = (sHelp == "XMLERRORS");
            rEDITLINK.Visible = (sHelp == "EDITLINK");
            rMANAGE.Visible = (sHelp == "MANAGE");
        }

        private int nLongest = 0;

        private int ReduceSize(int nPixels, double Pct)
        {
            double x = Convert.ToDouble(nPixels) * Pct;
            return Convert.ToInt32(x);
        }

        // assume font to be 9 points and guessing 7 across and 9 high
        // 80 chars across is 560 pixels
        private void SetPosition(RichTextBox rBOX)
        {
            double x = rBOX.Text.Length;
            x = 0.9 *  x / nLongest;
            int Width = Math.Max(560, ReduceSize(Size.Width, x));
            int Height = Math.Max(180, ReduceSize(Size.Height, x));
            rBOX.Location = new System.Drawing.Point(20, 20);
            rBOX.Size = new System.Drawing.Size(Width, Height);
            rBOX.BackColor = Color.White;
        }

        private void ShowHelp(string sHelp)
        {
            MakeVisible(sHelp);
            switch (sHelp)
            {
                case "FILE":
                    SetPosition(rFILE);
                    break;
                case "UTILS":
                    SetPosition(rUTIL);
                    break;
                case "EDIT":
                    SetPosition(rEDIT);
                    break;
                case "SIG":
                    SetPosition(rSIG);
                    break;
                case "EDITLINK":
                    SetPosition(rEDITLINK);
                    break;
                case "MANAGE":
                    SetPosition(rMANAGE);
                    break;
                case "XMLERRORS":
                    SetPosition(rXMLERR);
                    break;
            }
        }

        public help(string sHelp)
        {
            InitializeComponent();
            nLongest= 0;
            foreach(RichTextBox rtb in this.Controls)
            {
                nLongest = Math.Max(rtb.Text.Length, nLongest);
            }
            ShowHelp(sHelp);
        }
    }
}
