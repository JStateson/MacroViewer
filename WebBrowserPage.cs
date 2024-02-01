using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
