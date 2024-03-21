using Microsoft.Win32;
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
using System.Security.Policy;
using System.Windows.Automation;

namespace MacroViewer
{
    public class CMoveSpace
    {
        public int nChecked;    // this many checked
        public int nePC;        // number of empty slots for macros
        public int neAIO;       // AIO or laptop as disassembly is different from PC
        public int neLJ;        // laserjst
        public int neDJ;       //   deskjst
        public int neHP;
        public int neOS;
        public bool bRun;       // if true then perform move
        public string strType;    // name of the "from" file
        public string strDes;   // destination
    }

    // to add additional macro pages you need to mod the above cms to add an neXX and the below
    // and add a file opening
    public static class Utils
    {
        public static string[] LocalMacroPrefix = { "PC", "AIO", "LJ", "DJ", "OS", "HP" };
        public static string XMLprefix = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-type\" content=\"text/html;charset=UTF-8\" /></head><body style=\"width: 800px; auto;\">";
        public static string XMLsuffix = "</body></html>";
        //public static string XMLdtd = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        public static int HPforumWidth = 825;   // seems like any response "box" never exceeds 825 pixels
        //    <div style="width: 825px; background-color: lightblue; padding: 10px;">
        //<body style="width: 800px; margin: 0 auto;">
        public static string WhereExe = "";
        public static string UnNamedMacro = "Change Me";


        public static string FNtoHeader(string strFN)
        {
            string[] LocalMacroFullname = { "Desktop(PC)", "AIO or Laptop", "LaserJet(LJ)", "DeskJet(DJ)", "OS related", "HP from HTML" };
            int i = 0;
            foreach (string s in LocalMacroPrefix)
            {
                if(s ==  strFN)
                {
                    return LocalMacroFullname[i];
                }
                i++;
            }
            return "HTML (uneditable)";
        }
        public static void ShowParseLocationErrors(string strText)
        {
            string strLoc = WhereExe +  "\\MyHtmlErr.txt";
            File.WriteAllText(strLoc, strText);
            Utils.NotepadViewer(strLoc);
        }

        public static string FNtoPath(string strFN)
        {
            return WhereExe + "\\" + strFN + "macros.txt";
        }

        public static bool NoFileThere(string strFN)
        {
            string strPath = FNtoPath(strFN);
            if (File.Exists(strPath)) return false;
            return true;
        }

        public static string RemoveNL(string text)
        {
            string strRtn = text.Replace(Environment.NewLine, "<br>");
            return strRtn.Replace("\n", "<br>").Trim();
        }

        public static void WriteAllText(string strLoc, string strData)
        {
            File.WriteAllText(strLoc, NoTrailingNL(strData));
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
        /*
         * https://www.google.com/search?q=hp+928511-001&sca_e  https://www.bing.com/search?q=sn570&qs=n
         * https://www.amazon.com/s?k=sn570&crid=3U  https://www.aliexpress.us/w/wholesale-sn570.html?spm=a2g0o.home.search.0
         * https://www.google.com/search?client=firefox-b-1-d&q=hp+50334-601
         * https://www.amazon.com/Ediloca-Internal-Compatible-Ultrabooks-Computers/dp/B0B7QYZF9X/ref=sr_1
         * 
         */
        private static string dStr(string strIn, string strRef)
        {
            int i = strIn.IndexOf(strRef);
            return (i < 0) ? strIn.Trim() : strIn.Substring(0,i);
        }
        public static string dRef(string sUrl)
        {
            int i,j;
            sUrl = dStr(sUrl,"/ref");
            if (sUrl.Contains("youtube"))return sUrl;
            string surl = sUrl.ToLower();
            i = sUrl.IndexOf("search?");
            if(i > 0)
            {
                j = sUrl.IndexOf("q=", i + 7);
                if (j < 0) return sUrl;
                i = sUrl.IndexOf('&', j);
                if(i < 0) return sUrl;
                return sUrl.Substring(0, i);
            }
            if (surl.Contains("amazon") || surl.Contains("newegg"))
            {
                i = sUrl.IndexOf('&');
                return (i < 0) ? sUrl: sUrl.Substring(0, i);
            }
            if(surl.Contains("aliexpress") || surl.Contains("ebay"))
            {
                i = sUrl.IndexOf('?');
                return (i < 0) ? sUrl : sUrl.Substring(0, i);
            }

            i = sUrl.IndexOf("#:~:text=");
            if (i > 0) return sUrl.Substring(0, i);


            i = sUrl.IndexOf("?utm_source=");  // gets bing and google
            if (i > 0) return sUrl.Substring(0, i);

            if (System.Diagnostics.Debugger.IsAttached)
            { 
                // keep track of which urls cannot be untracked of de-referenced
                using (StreamWriter writer = File.AppendText(WhereExe + "\\UrlDebug.txt"))
                {
                    writer.WriteLine(sUrl);
                }
            }
            return sUrl;        
        }

        public static string NoTrailingNL(string s)
        {
            int i = s.Length;
            if (i < 2) return s;
            string t = s.Substring(i - 2);
            if (Environment.NewLine == t)
            {
                t = s.Substring(0, i-2);
                return NoTrailingNL(t);
            }
            return s;
        }

        //If file does not exist then no need for newline on the append
        public static void FileAppendText(string strFN, string text)
        {
            string sFilePath = FNtoPath(strFN);
            bool b = NoFileThere(strFN);
            string strGEnd = (b ? "" : Environment.NewLine) + NoTrailingNL(text);
            using (StreamWriter writer = File.AppendText(sFilePath))
            {
                writer.WriteLine(strGEnd);
                writer.Close();
            }
        }

        public static void ReplaceUrls(ref string sBody, bool MakeHyper)
        {
            int n = 0;
            bool b;
            CMarkup MyMarkup = new CMarkup();
            MyMarkup.Init(MakeHyper);
            while (true)
            {
                b = MyMarkup.FindUrl(n, ref sBody);
                if (!b) break;
                n++;
            }
            while (n > 0)
            {
                n--;
                CMarkup.cFiller cf = MyMarkup.cFillerList[n];
                sBody = sBody.Replace(cf.sFiller, cf.NewUrl);
            }
        }
    }


    public class CMarkup
    {
        private bool MakeHyper;
        public class cFiller
        {
            public string sFiller;
            public string OldUrl;
            public string NewUrl;
        }
        public List<cFiller> cFillerList;
        private void cReplace(int n, ref string sBody, int iLoc, int iLen)
        {
            cFiller cf = new cFiller();
            cf.OldUrl = sBody.Substring(iLoc, iLen);
            cf.sFiller = Utils.strFill(n, iLen);
            sBody = sBody.Replace(cf.OldUrl, cf.sFiller);
            string strClean = Utils.dRef(cf.OldUrl);
            cf.NewUrl = MakeHyper ? Utils.FormUrl(strClean, "") : strClean;
            cFillerList.Add(cf);
        }

        public void Init(bool bMakeHyper)
        {
            cFillerList = new List<cFiller>();
            MakeHyper = bMakeHyper;
        }

        private int LengthURL(ref string sBody, int iStart)
        {
            int n = -1;
            string s = sBody.Substring(iStart);
            foreach (char c in s)
            {
                n++;
                if (c == ' ') return n;
                if (c == '\n') return n;
                if (c == '\r') return n;
                if (c == '\t') return n;
            }
            return s.Length;
        }

        public bool FindUrl(int nLooked, ref string s)
        {

            int iHTTP = 0, iLen = 0;
            string sTMP = s.ToLower();
            iHTTP = sTMP.IndexOf("http");
            if (iHTTP < 0) return false;
            iLen = LengthURL(ref s, iHTTP);
            string strFound = s.Substring(iHTTP, iLen);
            cReplace(nLooked, ref s, iHTTP, iLen);
            return true;
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
        //<body style="width: 800px; margin: 0 auto;">
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

    public class CBody
    {
        public string File;     //PC, PRN, HP  
        public string Number;   //macro number 1..30 or more
        public string Name;     //macro name
        public string sBody;    //body of marco
        public string fKeys;    //keywords found separated by a space
        public int nWdsfKey;    //number of words in fKeys is the number of unique hits
    }
    public class CFound
    {
        public string File { get; set; }
        public string Number { get; set; }
        public string Found { get; set; }    //number of keywords found
        public string Name { get; set; }
        public int WhereFound;
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
