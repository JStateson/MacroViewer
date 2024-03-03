using System.Windows.Forms;

namespace MacroViewer
{
    public partial class WebBrowserPage : Form
    {
        public WebBrowserPage(string strIn)
        {
            InitializeComponent();
            webBrowser1.DocumentText = strIn;
        }
    }
}
