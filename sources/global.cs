﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Security;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Text;
using System.Drawing;
using System.Windows.Ink;
using System.Collections.Generic;

namespace MacroViewer
{
    public static class Utils
    {
        public static string XMLprefix = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-type\" content=\"text/html;charset=UTF-8\" /></head><body>";
        public static string XMLsuffix = "</body></html>";
        //public static string XMLdtd = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        public static string WhereExe = "";
        public static string UnNamedMacro = "Change Me";
        public static string[] LocalMacroPrefix = {"PC","PRN", "HP"};
        public static void ShowParseLocationErrors(string strText)
        {
            string strLoc = WhereExe +  "\\MyHtml.txts";
            File.WriteAllText(strLoc, strText);
            Utils.NotepadViewer(strLoc);
        }

        public static string RemoveNL(string text)
        {
            string strRtn = text.Replace(Environment.NewLine, "<br>");
            return strRtn.Replace("\n", "<br>").Trim();
        }
        public static void NotepadViewer(string strFile)
        {
            if (strFile == "") return;
            Process.Start("C:\\Windows\\Notepad.exe", strFile);
        }
        // BBCODE parse for bad tags
        public static string BBCparse(string strIn)
        {
            string strRtn = "";
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strIn + " ");  // seems needed to catch trailing open tag
            foreach(var strErr in htmlDoc.ParseErrors)
            {
                string strLine = " line:" + strErr.Line.ToString() + ", char:" + strErr.LinePosition.ToString();
                strRtn += strErr.Reason + strLine + Environment.NewLine;
            }
            return strRtn;
        }

        public static string FormUrl(string strUrl, string strIn)
        {
            if (strIn == "") strIn = strUrl;
            return "<a href=\"" + strUrl + "\" target=\"_blank\">" + strIn + "</a>";
        }

        public static string FixImg(string strImg)
        {

            return strImg;
        }

        public const string AssignedImageName = "LOCALIMAGEFILE"; // PRN and PC suffix 
        public enum eBrowserType
        {
            eEdge = 0,
            eChrome = 1,
            eFirefox = 2
        }

        public static string AssembleIMG(string strURL)
        {
            return "<img src=\"" + strURL.Trim() + "\" border=\"2\">";
        }
        public static eBrowserType BrowserWanted = eBrowserType.eEdge;
        public static string VolunteerUserID = "";
        public static void LocalBrowser(string strUrl)
        {
            switch (BrowserWanted)
            {
                case Utils.eBrowserType.eFirefox:
                    Process.Start("firefox.exe", "-new-window " + strUrl);
                    break;
                case Utils.eBrowserType.eEdge:
                    Process.Start("microsoft-edge:" + strUrl);
                    break;
                case Utils.eBrowserType.eChrome:
                    Process.Start("chrome.exe", strUrl);
                    break;
            }
        }

        public static string Form1CellTable(string strIn)
        {
            return "<table border=\"1\" width=\"100%\"><tr><td width=\"100%\"><p>" + strIn + "</p></td></tr></table>";
        }
        public static void PurgeLocalImages(string strType,  string WhereExe)
        {
            var dir = new DirectoryInfo(WhereExe);
            foreach (var file in dir.EnumerateFiles(AssignedImageName + "-" + strType +"-*.png"))
            {
                file.Delete();
            }
        }

        public static int CountImages()
        {
            string[] files = Directory.GetFiles(WhereExe, "*.png");
            return files.Length;
        }
        public class CLocalFiles
        {
            public bool NotUsed { get; set; }
            public string Name { get; set; }
        }
        public static List<CLocalFiles> NumLocalImageFiles() 
        {
            List<CLocalFiles> MyImages = new List<CLocalFiles>();
            string[] files = Directory.GetFiles(WhereExe, "LOCALIMAGEFILE-*.png");
            foreach(string s in files)
            {
                CLocalFiles cf = new CLocalFiles();
                cf.Name = Path.GetFileName(s);
                cf.NotUsed = true;
                MyImages.Add(cf);
            }
            return MyImages;
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

        public static string strFill(int i, int n)
        {
            string strOut = "";
            string sAlpha1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string sAlpha = sAlpha1 + sAlpha1.ToLower();
            int k = i % 52;
            for (int j = 0; j < n; j++)
                strOut += sAlpha.Substring(k, 1); 
            return strOut;
        }
        public static string FormTable(int rows, int cols)
        {
            if (rows == 0 && cols == 0) return "";
            if (rows == 0) rows = 1;
            if (cols == 0) cols = 1;
            int jChar = 0;

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<table border='1'>");

            for (int i = 0; i < rows; i++)
            {
                htmlBuilder.Append("<tr>");
                for (int j = 0; j < cols; j++)
                {
                    htmlBuilder.Append("<td>");
                    htmlBuilder.Append(strFill(jChar,4));
                    htmlBuilder.Append("</td>");
                    jChar++;
                }

                htmlBuilder.Append("</tr>");
            }
            htmlBuilder.Append("</table>");
            return htmlBuilder.ToString();
        }
    }

    internal class CShowBrowser
    {
        private bool bUseWebView  = true;
        private string strPrefix = Utils.XMLprefix;  //"<!DOCTYPE html><body><html><head><meta http-equiv=\"Content-type\" content=\"text/html;charset=UTF-8\" />";
        private string strSuffix = Utils.XMLsuffix;  //"</body></html>";
        
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

        // had to add block as BlinkTimer was not working !?!?!? todo to do todo
        public void ShowInBrowser(string strIn, bool bBlock)
        {
            string strTemp = strPrefix + strIn + strSuffix;
            if (bUseWebView)
            {
                ShowPage MyShowPage = new ShowPage(Utils.WhereExe, strTemp);    // this is WebView2 stuff
                if(bBlock)
                {
                    MyShowPage.ShowDialog();
                    MyShowPage.Dispose();
                    return;
                }
                MyShowPage.Show();
            }
            else
            {
                WebBrowserPage MyShowPage = new WebBrowserPage(strTemp);    // ie11 old browser
                if (bBlock)
                {
                    MyShowPage.ShowDialog();
                    MyShowPage.Dispose();
                    return;
                }
                MyShowPage.Show();
            }
        }

        public void ShowInBrowser(string strIn)
        {
            ShowInBrowser(strIn, false);
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
            Thread.Sleep(2000);
            Clipboard.SetText(strText);
            IntPtr hWnd = process.Handle;
            BringWindowToTop(hWnd);
            SendKeys.SendWait("^V");
        }
        public void PasteToNotepad(string strText, string strFile)
        {
            if (strText == "") return;
            // Let's start Notepad
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\Notepad.exe";
            process.StartInfo.Arguments = strFile;
            process.Start();
            Thread.Sleep(2000);
            Clipboard.SetText(strText);
            IntPtr hWnd = process.Handle;
            BringWindowToTop(hWnd);
            SendKeys.SendWait("^{end}");
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait("^V");
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
