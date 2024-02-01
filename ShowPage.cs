using Microsoft.Win32;
using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Microsoft.Web.WebView2.Core;
/*
 * webbrowser did not work with some HP sites so I read this
 * https://stackoverflow.com/questions/790542/replacing-net-webbrowser-control-with-a-better-browser-like-chrome
 * https://stackoverflow.com/questions/72993370/how-to-access-document-property-in-webview2-control
 */


namespace MacroViewer
{
    public partial class ShowPage : Form
    {

        async void ShowStuff(string strIn)
        {
            await webView.EnsureCoreWebView2Async();
            webView.NavigateToString(strIn);
        }

        public ShowPage(string ShowString)
        {
            InitializeComponent();
            ShowStuff(ShowString);
        }

    }
}
