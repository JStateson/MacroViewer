using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
/*
 * webbrowser did not work with some HP sites so I read this
 * https://stackoverflow.com/questions/790542/replacing-net-webbrowser-control-with-a-better-browser-like-chrome
 * https://stackoverflow.com/questions/72993370/how-to-access-document-property-in-webview2-control
 * <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
 */


namespace MacroViewer
{
    public partial class ShowPage : Form
    {
        WebView2 webview;
        private int fHeight, fWidth;
        private int x = 20, y = 20;
        public async void ShowStuff(string sLoc, string strIn)
        {
  
            webview = new WebView2
            {
                // CreationProperties = new CoreWebView2CreationProperties(),
                Location = new Point(x, y),
                Size = new Size(fWidth, fHeight)
            };

            var promise = webview.EnsureCoreWebView2Async().GetAwaiter();
            promise.OnCompleted(() =>
            {
                try
                {
                    promise.GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
                webview.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    hostName: "myprivateapp", folderPath: sLoc,
                    accessKind: CoreWebView2HostResourceAccessKind.Allow);
                webview.CoreWebView2.Navigate("https://myprivateapp/MyHtml.html");
            });

            this.Controls.Add(webview);
        }

        public ShowPage(string sLoc, string ShowString)
        {
            InitializeComponent();
            /*
            if(ShowPage.ActiveForm != null) { }
            {
                fHeight = ShowPage.ActiveForm.Height - 2 * y;
                fWidth = ShowPage.ActiveForm.Width - 2 * x;
            }
            */
            fWidth = 825;
            fHeight = 600;
            File.WriteAllText(sLoc + "/MyHtml.html", ShowString);
            ShowStuff(sLoc, ShowString);
        }

    }
}
