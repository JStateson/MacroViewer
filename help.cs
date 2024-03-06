using System.Windows.Forms;

namespace MacroViewer
{
    public partial class help : Form
    {
        private static string[] strForms = { "FILE","SIG","EDIT","MANAGE", "UTILS", "EDITLINK" };
        private void MakeVisible(string sHelp)
        {
            rFILE.Visible = (sHelp == "FILE");
            rUTIL.Visible = (sHelp == "UTILS");
            rEDIT.Visible = (sHelp == "EDIT");
            rSIG.Visible = (sHelp == "SIG");
            rEDITLINK.Visible = (sHelp == "EDITLINK");
            rMANAGE.Visible = (sHelp == "MANAGE");
        }

        private void SetPosition(RichTextBox rBOX)
        {
            rBOX.Location = new System.Drawing.Point(20, 20);
            rBOX.Size = new System.Drawing.Size(600, 400);
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

            }
        }

        public help(string sHelp)
        {
            InitializeComponent();
            ShowHelp(sHelp);
        }
    }
}
