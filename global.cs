using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using static System.Drawing.ImageConverter;
using System.Drawing;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System.Text;

namespace MacroViewer
{    
    public static class Utils
    {
        public const string AssignedImageName = "LOCALIMAGEFILE"; // PRN and PC suffix 
        public enum eBrowserType
        {
            eEdge = 0,
            eChrome = 1,
            eFirefox = 2
        }

        public static eBrowserType BrowserWanted = eBrowserType.eEdge;
        public static string VolunteerUserID="";

        public static void PurgeLocalImages(string strType,  string WhereExe)
        {
            var dir = new DirectoryInfo(WhereExe);
            foreach (var file in dir.EnumerateFiles(AssignedImageName + "-" + strType +"-*.png"))
            {
                file.Delete();
            }
        }
        public static string GetNextImageFile(string strType, string strExe)
        {
            int i = 0;
            string strName, strPath;
            while(true)
            {
                strName = AssignedImageName + "-" + strType + "-" + i.ToString() + ".png";
                strPath = strExe + "/" + strName;
                if(File.Exists(strPath))
                {
                    i++;
                    if(i > 90)
                    {
                        MessageBox.Show("ERROR: over 90 images in " + strExe + "\r\nSave what you can as I am purging");
                        Process.Start(strExe);
                        PurgeLocalImages(strType, strExe);
                    }
                    continue;
                }
                return strName;
            }
        }

        // paths returned from file explorer have quotes
        public static string RemoveOuterQuotes(string strIn)
        {
            string strTmp = strIn.Trim();
            if (strTmp.Substring(0, 1) == "\"")
            {
                strTmp = strTmp.Replace("\"", "");
            }
            return strTmp.Trim();
        }

        /*
    // ext = "*.bmp;*.dib;*.rle"           descr = BMP
    // ext = "*.jpg;*.jpeg;*.jpe;*.jfif"   descr = JPEG
    // ext = "*.gif"                       descr = GIF
    // ext = "*.tif;*.tiff"                descr = TIFF
    // ext = "*.png"                       descr = PNG
    */
        public static bool IsUrlImage(string aLCase)
        {
            string[] ImgExt = new string[11]
            {".bmp",".dib",".rle",".jpg",".jpeg",".jpe",".jfif",".gif",".tif",".tiff",".png" };
            if (aLCase.Contains("image/serverpage"))
                return true; // must be from HP server
            foreach (string aImg in ImgExt)
            {
                if (aLCase.Contains(aImg)) return true;
            }
            return false;
        }

        public static bool bIsHTTP(string strIN)
        {
            string strUC = strIN.ToUpper();
            return (strUC.Contains("HTTPS:") || strUC.Contains("HTTP:"));
        }

    }
    internal class CShowBrowser
    {
        private bool bUseWebView  = true;
        private string strPrefix = "<!DOCTYPE html><html><head>";
        private string strSuffix = "</body></html>";
        private bool WebViewIsInstalled()
        {
            string regKey = @"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients";
            using (RegistryKey edgeKey = Registry.LocalMachine.OpenSubKey(regKey))
            {
                if (edgeKey != null)
                {
                    string[] productKeys = edgeKey.GetSubKeyNames();
                    if (productKeys.Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void Init()
        {
            bUseWebView = WebViewIsInstalled();
        }


        public void ShowInBrowser(string sLoc, string strIn)
        {
            string strTemp = strPrefix + strIn + strSuffix;
            if (bUseWebView)
            {
                ShowPage MyShowPage = new ShowPage(sLoc, strTemp);    // this is WebView2 stuff
                MyShowPage.Show();
            }
            else
            {
                WebBrowserPage MyShowPage = new WebBrowserPage(strTemp);    // ie11 old browser
                MyShowPage.Show();
            }
        }

    }
    internal class CSendNotepad
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);
        public void PasteToNotepad(string strText)
        {
            if (strText == "") return;
            // Let's start Notepad
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\Notepad.exe";
            process.Start();


            // Give the process some time to startup
            Thread.Sleep(2000);

            // Copy the text in the datafield to Clipboard
            Clipboard.SetText(strText);

            // Get the Notepad Handle
            IntPtr hWnd = process.Handle;

            // Activate the Notepad Window
            BringWindowToTop(hWnd);

            // Use SendKeys to Paste
            SendKeys.Send("^V");
        }
    }

    internal class CSendCloud
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);
        private Process HPprocess;
        public void Init()
        {
            HPprocess = new Process();
            HPprocess.StartInfo.Verb = "runas";
            HPprocess.StartInfo.FileName ="C:\\Program Files\\WindowsApps\\AD2F1837.HPCloudRecoveryTool_2.7.8.0_x64__v10z8vjag6ke6\\CloudRecovery\\CloudRecovery.exe";
            HPprocess.Start();
        }
        public void PasteToCloud(string strText)
        {

            // Copy the text in the datafield to Clipboard
            Clipboard.SetText(strText);

            // Get the HP cloud Handle
            IntPtr hWnd = HPprocess.Handle;
            

            // Activate the Notepad Window
            BringWindowToTop(hWnd);

            // Use SendKeys to Paste
            SendKeys.Send("^V");
        }
    }
}
