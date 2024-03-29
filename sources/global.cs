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
using System.Security.Policy;
using System.Windows.Automation;
using System.Net.NetworkInformation;

namespace MacroViewer
{
    public class CMoveSpace
    {
        public int nChecked;    // this many checked
        public int nePC;        // number of empty slots for macros
        public int neAIO;       // AIO or laptop as disassembly is different from PC
        public int neLJ;        // laserjst
        public int neDJ;        // deskjet
        public int neOS;        // any general OS related help not specific to HP
        public int neHP;        // local copy of the HP macros
        public bool bRun;       // if true then perform move
        public string strType;    // name of the "from" file ie: source
        public string strDes;   // destination
        public bool bDelete;    // if true then just delete the item from the source, no move required
    }

    // to add additional macro pages you need to mod the above cms to add an neXX and the below
    // and add a specific file opening if desired to have it in the menu dropdown
    public static class Utils
    {
        public static string[] LocalMacroPrefix = { "PC", "AIO", "LJ", "DJ", "OS", "HP" };
        private static string[] LocalMacroFullname = { "Desktop(PC)", "AIO or Laptop", "LaserJet(LJ)", "DeskJet(DJ)", "OS related", "HP from HTML" };

        public static string XMLprefix = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-type\" content=\"text/html;charset=UTF-8\" /></head><body style=\"width: 800px; auto;\">";
        public static string XMLsuffix = "</body></html>";
        //public static string XMLdtd = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        public static int HPforumWidth = 825;   // seems like any response "box" never exceeds 825 pixels
        //    <div style="width: 825px; background-color: lightblue; padding: 10px;">
        //<body style="width: 800px; margin: 0 auto;">
        public static string WhereExe = "";
        public static string UnNamedMacro = "Change Me";
        public static string SupSigPrefix = "=+-=";   // these are used to identify the macro addition to make it eacy
        public static string SupSigSuffix = "=-+=";   // to delete or to change.
        public static bool bRecordUnscrubbedURLs = false;
        public static string YesButton = "<img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71238i8585EF0CF97FB353/image-dimensions/50x27?v=v2\">";
        public static string SolButton = "<img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71236i432711946C879F03/image-dimensions/129x32?v=v2\">";
        public static int HasSupSig(ref string s)
        {
            if (s.Contains(SupSigPrefix) && s.Contains(SupSigSuffix)) return 1;
            return 0;
        }

        public static string FNtoHeader(string strFN)
        {
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

        public static string ReplaceSupSig(string sSup, ref string sBody)
        {
            int i, j;
            if (sBody == null) return "";
            if (sBody == "") return "";
            string sAdded, sRtn = "";
            if(sSup == "")
            {
                i = sBody.IndexOf(SupSigPrefix);
                if (i > 0) sRtn = sBody.Substring(0, i);
                else sRtn = sBody;
                sRtn = NoTrailingNL(sRtn).Trim();
            }
            else
            {
                sAdded = SupSigPrefix + sSup + SupSigSuffix;
                i = sBody.IndexOf(SupSigPrefix);
                if (i < 0)
                {
                    sRtn = NoTrailingNL(sBody).Trim() + "<br><br>" + sAdded;
                }
                else
                {
                    j = sBody.IndexOf(SupSigSuffix);    // probably should assert j > 0
                    sRtn = sBody.Substring(0, i) + sAdded;
                }
            }
            return sRtn;
        }

        public static int FirstDifferenceIndex(string str1, string str2)
        {
            int minLength = Math.Min(str1.Length, str2.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (str1[i] != str2[i])
                {
                    return i + 1;
                }
            }

            // If all characters up to the length of the shorter string are the same
            if (str1.Length != str2.Length)
            {
                return 0;
            }

            // If the strings are identical, return -1
            return -1;
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

        /*
         *
      x  https://www.amazon.com/s?k=wd+blue+sn570&hvadid=604496098449&hvdev=c&hvlocphy=9028097&hvnetw=g&hvqmt=e&hvrand=16170472703128286580&hvtargid=kwd-1440214831857&hydadcr=24329_13517663&tag=googhydr-20&ref=pd_sl_9paam7inoz_e
        https://www.amazon.com/Western-Digital-SN580-Internal-Solid/dp/B0C8XMH264/ref=asc_df_B0C8XMH264?tag=bingshoppinga-20&linkCode=df0&hvadid=80401975313450&hvnetw=o&hvqmt=e&hvbmt=be&hvdev=c&hvlocint=&hvlocphy=&hvtargid=pla-4584001439821208&th=1
        https://www.officedepot.com/a/products/6789792/Western-Digital-BLUE-SN570-Internal-NVMe/?mediacampaignid=71700000100485049_370762392&gclid=138b98ec20b212f5975afeedf0922222&gclsrc=3p.ds&msclkid=138b98ec20b212f5975afeedf0922222
        https://www.bhphotovideo.com/c/product/1673383-REG/seagate_st4000dm004_barracuda_4tb_3_5_5400.html?ap=y&smp=y&msclkid=c6b55c6469861e7e5dfd3a65b9749523
      x  https://www.seagate.com/products/nas-drives/ironwolf-pro-hard-drive/?sku=ST10000NTZ01&utm_campaign=nas-2024-shopping-global&utm_medium=sem&utm_source=google-shopping&utm_product=ironwolf-nas&utm_use_case=general&prodSrc=ironwolf-nas&use_case=general&gad_source=1&gclid=Cj0KCQjwwYSwBhDcARIsAOyL0fjBfMIzPXARUADRZgnhTNWFs4SOeDyf_vEmpeg1pimtKttTI1JuE_0aAqQ_EALw_wcB
        https://www.newegg.com/crucial-2tb-t500/p/20-156-389?Item=20-156-389
        https://www.westerndigital.com/products/internal-drives/wd-blue-sn580-nvme-ssd?cjdata=MXxOfDB8WXww&cjevent=e9592a2deadf11ee806c35760a1cb827&utm_medium=afl1&utm_source=cj&utm_content=Western+Digital+Clearance,+Canada&cp1=100357191&utm_campaign=ca-clearance&utm_term=02-03-2022&cp2=Microsoft+Shopping+(Bing+Rebates,+Coupons,+etc.)&sku=WDS250G3B0E
         */

        // can stop at ?
        private static string[] QListVendors = {
            ".westerndigital.",
            ".officedepot.",
            ".bhphotovideo.",
            ".amazon.", // but  not s? so put after the ?& test
            ".newegg."
        };
        private static string QVendor(string sUrl)
        {
            foreach(string s in QListVendors)
            {
                int i = sUrl.IndexOf(s);
                if (i > 0)
                {
                    int j = s.IndexOf('?');
                    if (j < 0) return sUrl;
                    return sUrl.Substring(0, j);    
                }
            }
            return "";
        }

        public static string FormTable(int rows, int cols, bool bFill)
        {
            if (rows == 0 && cols == 0) return "";
            if (rows == 0) rows = 1;
            if (cols == 0) cols = 1;
            int jChar = 0;
            string r = rows > 9 ? "00" : "0";
            string c = cols > 9 ? "00" : "0";

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<table border='1'>");

            for (int i = 0; i < rows; i++)
            {
                htmlBuilder.Append("<tr>");
                for (int j = 0; j < cols; j++)
                {
                    string s = "R" + i.ToString(r) + "C" + j.ToString(c) + "_" + strFill(jChar, 4);
                    htmlBuilder.Append("<td>");
                    if (bFill) htmlBuilder.Append(s);
                    else htmlBuilder.Append("    ");
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
         * https://www.google.com/search?client=firefox-b-1-d&q=hp+50334-601  https://www.newegg.com/p/pl?d=wd+blue+sn570
         * https://www.amazon.com/Ediloca-Internal-Compatible-Ultrabooks-Computers/dp/B0B7QYZF9X/ref=sr_1
         * ? before & is used by amazon, wd, seagate, newegg but not google with that firefox
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
            string surl = sUrl.ToLower();
            if (surl.Contains("youtube"))return sUrl;

            i = surl.IndexOf("search?");
            if(i > 0)
            {
                j = sUrl.IndexOf("q=", i + 7);
                if (j < 0) return sUrl;
                i = sUrl.IndexOf('&', j);
                if(i < 0) return sUrl;
                return sUrl.Substring(0, i);
            }

            if(surl.Contains("aliexpress") || surl.Contains("ebay"))
            {
                i = sUrl.IndexOf('?');
                return (i < 0) ? sUrl : sUrl.Substring(0, i);
            }

            i = surl.IndexOf("#:~:text=");
            if (i > 0) return sUrl.Substring(0, i);


            i = surl.IndexOf("?utm_source=");  // gets bing and google
            if (i > 0) return sUrl.Substring(0, i);

            //if (surl.Contains("amazon") || surl.Contains("newegg") || surl.Contains("westerndigital"))
            {
                i = sUrl.IndexOf('&');
                j = sUrl.IndexOf('?');
                //return (i < 0) ? sUrl: sUrl.Substring(0, i);
                if (j < i) return sUrl.Substring(0, i);
                if (i < 0 && j < 0) return sUrl;    //nothing complicated so just return
            }

            surl = QVendor(sUrl);
            if (surl != "") return surl;

            if (surl.Contains("https://parts.hp.com/hpparts/Default.aspx"))
                return "https://parts.hp.com/hpparts";

            if (System.Diagnostics.Debugger.IsAttached || bRecordUnscrubbedURLs)
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
            t = s.Substring(i - 4);
            if (t == "<br>")
            {
                t = s.Substring(0, i - 4);
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

    public class CNewMac
    {
        public string sName;
        public string sBody;
        public void AddNB (string sn, string sb)
        {
            sName = sn;
            sBody = sb;
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
