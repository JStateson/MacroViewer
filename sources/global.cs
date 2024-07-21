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
using System.Net.NetworkInformation;
using System.Linq.Expressions;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.AxHost;

namespace MacroViewer
{
    public class CMoveSpace
    {/// <summary>
     /// "PC", "AIO", "LJ", "DJ", "OJ", "IN", "OS", "NET", "HW", "RF", "NO", "TR", "HP"  DO NOT CHANGE ORDER OF BELOW ITEMS
     /// </summary>

        public string[] MacroIDs;
        public int[] nMacsInFile;
        public int[] nMacsAllowed;
        public void Init()
        {
            int i = 0;
            MacroIDs = Utils.LocalMacroPrefix;
            nMacsInFile = new int[MacroIDs.Length];
            nMacsAllowed = new int[MacroIDs.Length];
            for (i = 0; i < MacroIDs.Length - 1; i++)
                nMacsAllowed[i] = Utils.NumMacros;
            nMacsAllowed[MacroIDs.Length - 1] = Utils.HPmaxNumber; // only 30 macros in HP forum original list
            // this item has to be "HP" and stay at end of any string list of files
            CountEmpties();
        }
        public void SetMacCount(string ID, int n)
        {
            int i = 0;
            foreach(string s in MacroIDs)
            {
                if(s == ID)
                {
                    nMacsInFile[i] = n;
                    return;
                }
                i++;
            }
        }
        public int GetMacCount(string ID)
        {
            int i = 0;
            foreach (string s in MacroIDs)
            {
                if (s == ID)
                {
                    return nMacsInFile[i];
                }
                i++;
            }
            return -1;
        }
        public int GetMacCountAvailable(string ID)
        {
            int i = 0;
            int n = 0;
            foreach (string s in MacroIDs)
            {
                if (s == ID)
                {
                    return nMacsAllowed[i] - nMacsInFile[i];
                }
                i++;
            }
            return n;
        }
        public void UpdateCount()
        {
            int n = GetMacCount(strDes);
            n += nChecked;
            SetMacCount(strDes, n);
            n = GetMacCount(strType);   //removing checked from source file
            n -= nChecked;
            SetMacCount(strType, n);
        }

        private int CountItems(string s)
        {
            if (Utils.NoFileThere(s)) return 0;
            string[] sAll = File.ReadAllLines(Utils.FNtoPath(s));
            if (s == "HP")
            {
                int j = 0;
                for (int i = 0; i < sAll.Length; i += 2)
                {
                    if (sAll[i].Length != 0) j++;
                }
                return j;
            }
            return sAll.Length / 2;
        }

        public void CountEmpties()
        {
            foreach (string s in Utils.LocalMacroPrefix)
            {
                SetMacCount(s, CountItems(s));
            }
        }

        public int nChecked;    // this many checked
        public bool bRun;       // if true then perform move
        public bool bCopy;      // do not delete
        public string strType;    // name of the "from" file ie: source
        public string strDes;   // destination
        public bool bDelete;    // if true then just delete the item from the source, no move required
    }

    public class cMacroChanges
    {
        private List<string> mChanges;
        private string ChangeList = "";
        public string sGoTo = "";
        public int nSelectedRowIndex;
        private int iPadSize = 6;   // 5 integers including a space
        private int x16 = 17;   // 16 hex chars plus a space
        private int x4 = 5;     // 4 hex chars plus a space
        private int ReadLinesIntoList()
        {
            mChanges = new List<string>();

            if (File.Exists(ChangeList))
            {
                mChanges.AddRange(File.ReadAllLines(ChangeList));
            }
            else
            {
                return 0;
            }
            return mChanges.Count;
        }

        public int Init(string sFileName)
        {
            ChangeList = Utils.WhereExe + "\\" + sFileName;
            return ReadLinesIntoList();
        }

        public int GetZ000(string sHEX)
        {
            string s = sHEX.Substring(sHEX.Length - 3);
            return Convert.ToInt32(s,16);
        }
        private string SetZ000(string sHex, int n)
        {
            string sRtn = "";
            if(n == 0)
            {
                sRtn = sHex.Substring(0, 13) + "000";
            }
            else // add one
            {
                n = GetZ000(sHex);
                n++;
                sRtn = Convert.ToString(n,16);
                sRtn =sHex.Substring(0,13) + sRtn.PadLeft(3, '0');
            }
            return sRtn;
        }

        private void IncChange(int i)
        {
            string s = mChanges[i];
            int j = s.IndexOf(":");
            string t = SetZ000(s.Substring(0,j), 1);
            t += s.Substring(j);
            mChanges[i] = t;
        }

        public string TicksToHex(long ticks)
        {
            return ticks.ToString("X16");
        }

        // filename and macro name
        public void AddChange(string sFN, string sMN)
        {

            DateTime date = DateTime.Now;
            //string formattedDate = date.ToString("MMMM dd yyyy, hh:mm tt");
            //  July 04 2024, 01:45 PM
            string hexString = TicksToHex(date.Ticks);
            string sFnMn = sFN + ":" + sMN;
            //if (mChanges.Any(s => s.Contains(sFnMn))) return;
            Predicate<string> matchPredicate = s => s.Contains(sFnMn);
            int i = mChanges.FindIndex(matchPredicate);
            if(i == -1)
                mChanges.Add(SetZ000(hexString,0) + ":" + sFnMn);
            else
            {
                IncChange(i);
            }
        }
        public void AddView(string sFN, string sMN)
        {

            DateTime date = DateTime.Now;
            //string formattedDate = date.ToString("MMMM dd yyyy, hh:mm tt");
            //  July 04 2024, 01:45 PM
            long ticks = date.Ticks;
            string hexString = "0001";
            string sFnMn = sFN + ":" + sMN;
            Predicate<string> matchPredicate = s => s.Contains(sFnMn);
            int i = mChanges.FindIndex(matchPredicate);
            if(i >= 0)
            {
                string s = mChanges[i].Substring(0, 4);
                int n = Convert.ToInt32(s, 16);
                n++;
                hexString = n.ToString("X4");
                mChanges.RemoveAt(i);
            }
            mChanges.Add(hexString + ":" + sFnMn);
        }

        public bool bIsEmpty()
        {
            return mChanges.Count <= 0;
        }

        public void RemView(string sFN, string t)
        {
            string sFnMn = sFN + ":" + t.Substring(iPadSize);
            Predicate<string> matchPredicate = s => s.Contains(sFnMn);
            int i = mChanges.FindIndex(matchPredicate);
            if (i >= 0)
            {
                mChanges.RemoveAt(i);
            }
        }

        public bool GoToMacro(ref string sFN, ref string sMN)
        {
            if (sGoTo == "") return false;
            sFN = sGoTo.Substring(0, 2);
            sMN = sGoTo.Substring(3);
            return true;
        }

        public void ClearChanges()
        {
            File.Delete(ChangeList);
            mChanges.Clear();
        }

        public void SaveChanges()
        {
            if (mChanges.Count > 0)
            {
                File.WriteAllLines(ChangeList, mChanges);
            }
            else File.Delete(ChangeList);
        }

        // sMN is the 2 or 3 character Macro Name
        public void TryRemove(string sFN, string sMN)
        {
            string sFnMn = sFN + ":" + sMN;
            mChanges.RemoveAll(line => line == sFnMn);
        }

        public List<string> GetFN(int i)
        {
            List<string> st = new List<string>();
            foreach (string s in mChanges)
            {
                string t = sGetFN(s.Substring(i));
                if (!st.Contains(t))
                {
                    st.Add(t);
                }
            }
            return st;
        }

        public List<string> GetFNChanges()
        {
            return GetFN(x16);
        }
        public List<string> GetFNViews()
        {
            return GetFN(x4);
        }

        public string sGetFN(string s)
        {
            int i = s.IndexOf(":");
            return s.Substring(0, i);
        }

        public List<int> GetMNViews(string sFN, ref List<string> stEdit, ref List<int> nViewed)
        {
            stEdit.Clear();   // the edit data source
            nViewed.Clear();     // the edit date
            int n;
            foreach (string s in mChanges)
            {
                string iView = s.Substring(0, 4);    // times viewed in hex
                n = Convert.ToInt32(iView, 16);
                string sn = n.ToString().PadLeft(iPadSize-1) + " ";
                string t = sGetFN(s.Substring(x4));
                if (t == sFN)
                {
                    stEdit.Add(sn + s.Substring(iPadSize + t.Length)); // there is no :
                    nViewed.Add((int)Convert.ToInt32(iView, 16));
                }
            }
            // Create a list of tuples where each tuple contains an integer and its original index
            var SrtInx = nViewed.Select((number, index) => new { Number = number, Index = index }).ToList();

            // Sort the list of tuples based on the integer values
            var sortedSrtInx = SrtInx.OrderByDescending(x => x.Number).ToList(); //Descending
            List<int> SrtOut = new List<int>();
            foreach (var item in sortedSrtInx)
            {
                SrtOut.Add(item.Index);
            }
            return SrtOut;
        }
        //1234567890123456:FileID:Macroname
        //---hex----------:xx or xxx: 
        public List<int> GetMNChanges(string sFN, ref List<string> stEdit, ref List<long> lDate)
        {
            stEdit.Clear();   // the edit data source
            lDate.Clear();     // the edit date
            int n = 0;
            foreach (string s in mChanges)
            {
                string dST = s.Substring(0, 16);    // date string
                string t = sGetFN(s.Substring(x16));
                if (t == sFN)
                {
                    stEdit.Add(s.Substring(x16 + t.Length + 1));
                    lDate.Add((long) Convert.ToInt64(dST,16));
                    n++;
                }
            }
            // Create a list of tuples where each tuple contains an integer and its original index
            var SrtInx = lDate.Select((number, index) => new { Number = number, Index = index }).ToList();

            // Sort the list of tuples based on the integer values
            var sortedSrtInx = SrtInx.OrderByDescending(x => x.Number).ToList(); //Descending
            List<int> SrtOut = new List<int>();
            foreach(var item in sortedSrtInx)
            {
                SrtOut.Add(item.Index);
            }
            return SrtOut;
        }

        public int CalculateChecksum(string input)
        {
            int checksum = 0;
            foreach (char character in input)
            {
                checksum += character;
            }
            return checksum;
        }

        public void isMacroChanged(int chk, string sFN, string sMN, string sBody)
        {
            if (chk != CalculateChecksum(sBody))
            {
                AddChange(sFN, sMN);
            }
        }
    }

        // to add additional macro pages you need to mod the above cms to add an neXX and the below
        // and add a specific file opening if desired to have it in the menu dropdown
    public static class Utils
    {
        private const int iNMacros = 13;
        public const int NumMacros = 999;
        public static int HPmaxNumber = 40;  // some users may have more!
        public static int nLongestExpectedURL = 256;
        public static int TotalNumberMacros = 0;
        public static string[] nUse ={ // these must match the button names in WordSearch
            //"PC","Printer","Drivers","EBay","Google","Manuals","YouTube","HP KB"
              "PC","PRN",    "DRV",    "EBA", "GOO",   "MAN",    "HPYT",   "HPKB"
        };

        public static bool IsNewPRN (string sT)
        {
            if (sT == "") return false;
            return (sPrinterTypes.Contains(" " + sT + " "));
        }

        public static string nBR(int n)
        {
            string s = "";
            for (int i = 0; i < n; i ++)
            {
                s += "<br>";
            }
            return s;
        }

        public static string nNL(int n)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += Environment.NewLine;
            }
            return s;
        }

        private static string[] sUse = // possible new macros
        {
            "PC AIO HW",            //PC
            "LJ DJ OJ IN HW",          //PRN
            "PC AIO LJ DJ OJ IN",      //DRV
            "NET OS HW",            //EBY
            "NET OS HW RF NO",      //GOO
            "PC AIO LJ DJ OJ IN",      //MAN
            "LJ DJ OJ IN NET",         //Youtube
            "PC AIO LJ DJ OJ IN OS"    //HP KB
        };
        public static string sFindUses(string s)
        {
            int i = 0;
            foreach(string t in nUse )
            {
                if (s == t) return sUse[i];
                i++;
            }
            return "";
        }
        // do not change the order of below items and HP must be last!
        public static string sPrinterTypes = " LJ DJ OJ IN ";    // must have a space and match below
        public static string[] LocalMacroPrefix = new string[iNMacros]  { "PC", "AIO", "LJ", "DJ", "OJ", "IN", "OS", "NET", "HW", "RF", "NO", "TR", "HP" };
        public static string[] LocalMacroFullname = new string[iNMacros] { "Desktop(PC)", "AIO or Laptop", "LaserJet(LJ)",
                "DeskJet(DJ)", "OfficeJet(OJ)", "Tank-Inkjet(IN)", "OS related", "Network related", "Hardware", "Reference", "Notes", "Transfer", "HP from HTML" };
        public static string[] LocalMacroRefs = new string[iNMacros] {"PC Reference","PC Reference","LaserJet Reference",
                "DeskJet Reference","OfficeJet Reference","Tank-Ink Reference", "", "", "", "","","",""};

        // there is an "SI" type which is used for SIgnature images.
        public static string XMLprefix = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-type\" content=\"text/html;charset=UTF-8\" /></head><body style=\"width: 800px; auto;\">";
        public static string XMLsuffix = "</body></html>";
        public static string sIsAlbum = "/image/serverpage/image-id";
        public static string sHasSize = "/image-size/";
        public static string sDifSiz = "tiny,thumb,small,medium,large";
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
        public static string AllAlphas = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxya";
        public static string[] sPossibleLanguageOption = { "-16\" target=", "-16?openCLC=true\" target=" };
        public static string AddLanguageOption(string sIN)
        {
            if (sIN.IndexOf(sPossibleLanguageOption[0] ) != -1)
            {
                sIN = sIN.Replace(sPossibleLanguageOption[0], sPossibleLanguageOption[1]);
            }
            return sIN;
        }


        // adjust i and j so that they do not contains any leading or trailing spaces
        // i is start position, k is length of string
        public static string AdjustNoTrim(ref int i, ref int k, ref string s)
        {
            if (s == "" || k == 0) return "";
            if (i >= s.Length) return "";
            int j = i + k - 1;
            char c = s[i];
            while(c == ' ')
            {
                i++;
                if(i >= s.Length) break;
                c = s[i];
            }
            c = s[j];
            while(c == ' ')
            {
                j--;
                c = s[j];
            }
            k = j - i + 1;
            if (k > 0)
            {
                return s.Substring(i, k);
            }
            k = 0;
            return "";
        }
        public static void ShellHTML(string s, bool IsFilename)
        {
            string sTemp = WhereExe + "\\";
            if(IsFilename)
            {
                sTemp += s;
                if (!File.Exists(sTemp)) return;
            }
            else
            {
                sTemp += "MyHtml.html";
                File.WriteAllText(sTemp, s);
            }
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "explorer.exe", // The application to run
                    Arguments = sTemp,           // Any arguments to pass to the application
                    UseShellExecute = true,   // Whether to use the operating system shell to start the process
                    RedirectStandardOutput = false, // Whether to redirect the output (for console applications)
                    RedirectStandardError = false,  // Whether to redirect the error output (for console applications)
                    CreateNoWindow = false    // Whether to create a window for the process
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        internal static class ClipboardFormats
        {
            static readonly string HEADER =
                "Version:0.9\r\n" +
                "StartHTML:{0:0000000000}\r\n" +
                "EndHTML:{1:0000000000}\r\n" +
                "StartFragment:{2:0000000000}\r\n" +
                "EndFragment:{3:0000000000}\r\n";

            static readonly string HTML_START =
                "<html>\r\n" +
                "<body>\r\n" +
                "<!--StartFragment-->";

            static readonly string HTML_END =
                "<!--EndFragment-->\r\n" +
                "</body>\r\n" +
                "</html>";

            public static string ConvertHtmlToClipboardData(string html)
            {
                var encoding = new System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
                var data = Array.Empty<byte>();

                var header = encoding.GetBytes(String.Format(HEADER, 0, 1, 2, 3));
                data = data.Concat(header).ToArray();

                var startHtml = data.Length;
                data = data.Concat(encoding.GetBytes(HTML_START)).ToArray();

                var startFragment = data.Length;
                data = data.Concat(encoding.GetBytes(html)).ToArray();

                var endFragment = data.Length;
                data = data.Concat(encoding.GetBytes(HTML_END)).ToArray();

                var endHtml = data.Length;

                var newHeader = encoding.GetBytes(
                    String.Format(HEADER, startHtml, endHtml, startFragment, endFragment));
                if (newHeader.Length != startHtml)
                {
                    throw new InvalidOperationException(nameof(ConvertHtmlToClipboardData));
                }

                Array.Copy(newHeader, data, length: startHtml);
                return encoding.GetString(data);
            }
        }

        public static void CopyHTML(string html)
        {
            var dataObject = new DataObject();
            string s = ClipboardFormats.ConvertHtmlToClipboardData(html);
            Clipboard.SetData(DataFormats.Html, s);
        }

        public static string ClipboardGetText()
        {
            string t = Clipboard.GetText();
            if (t == null) t = "";
            return t.Trim();
        }
        public static void ShowPageInBrowser(string strType, string strTemp)
        {
            string sPP = Properties.Settings.Default.sPPrefix;
            strTemp = strTemp.Replace(Environment.NewLine, "<br>");
            if (sPP != "init" && strType != "" && Utils.sPrinterTypes.Contains(strType + " "))
            {
                strTemp = "<br>" + Properties.Settings.Default.sPPrefix + "<br><br>" + strTemp +
                    "<br><br>" + Properties.Settings.Default.sMSuffix;
            }
            else 
            if(strType != "")
                strTemp += "<br><br>" + Properties.Settings.Default.NotPrnSuffix;
            ShellHTML(strTemp, false);
            CopyHTML(strTemp);

        }

        public static bool SyntaxTest(string s)
        {
            string strErr = Utils.BBCparse(s);
            if (strErr == "") return false;
            DialogResult Res1 = MessageBox.Show(strErr, "Click OK to see where errors are", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (Res1 == DialogResult.OK)
            {
                ShowParseLocationErrors(s);
                MessageBox.Show(strErr, "Errors are near locations listed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        public static string[] RequiredMacrosRF = { "PC AIO LAPTOP support documents", "Printer support documents" };//, "HP-KB-WIKI"};

        public static void WordpadHelp(string sHelp)
        {
            string[] strForms = { "FILE", "SIG", "EDIT",  "EDITLINK",
                "MANAGE","XMLERRORS", "UTILS", "SEARCH", "WEB" };
            string[] strFiles = { "mnu-file.rtf","mnu-imag-sig.rtf","mnu-main-edit.rtf", "mnu-edit-link.rtf",
                "mnu-manage-img.rtf", "mnu-paste-sig.rtf", "mnu-util.rtf", "mnu-word-search.rtf" , "mnu-web-search.rtf"};
            int n = 0;
            foreach (string s in strForms)
            {
                if (s == sHelp)
                {
                    Process.Start("wordpad.exe", WhereExe + "/" + strFiles[n]);
                    return;
                }
                n++;
            }
        }

        public static int HasSupSig(ref string s, ref int i, ref int j)
        {
            if (s == null) return 0;
            if (s == "") return 0;
            i = s.IndexOf(SupSigPrefix);
            j = s.IndexOf(SupSigSuffix);
            if (i > 0 && j > 0 && i < j) return 1;
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
            return WhereExe + "\\" + strFN + ((strFN == "SI") ? "gnatures.txt" : "macros.txt");
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
            if (strIn == null) return "";
            htmlDoc.LoadHtml(strIn + " ");  // seems needed to catch trailing open tag
            foreach(var strErr in htmlDoc.ParseErrors)
            {
                string strLine = " line:" + strErr.Line.ToString() + ", char:" + strErr.LinePosition.ToString();
                strRtn += strErr.Reason + strLine + Environment.NewLine;
            }
            int i = strIn.IndexOf("...");
            if(i >= 0)
            {
                strRtn += "possible '...' problem at " + i.ToString() + Environment.NewLine;
            }
            return strRtn;
        }

        public static string FormUrl(string strUrl, string strIn)
        {
            if (strIn == "") strIn = strUrl;
            return "<a href=\"" + strUrl + "\" target=\"_blank\">" + strIn + "</a>";
        }


        public static int FirstDifferenceIndex(string str1, string str2)
        {
            if (str1 == null) return 0;
            if (str2 == null) return 0;
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

        public static bool GetPixSize(string strName, ref int iHeight, ref int iWidth)
        {
            string strPath = WhereExe + "\\" + strName;
            try
            {
                using (var image = new Bitmap(strPath))
                {
                    iWidth = image.Width;
                    iHeight = image.Height;
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string AssembleImage(string strUrl, int Height, int Width)
        {
            if (strUrl.Contains("image-id"))
            {
                return Utils.AssembleIMG(strUrl);
            }
//return "<img src=\"" + strUrl + "\" border=\"2\"  height=\"" + Height.ToString() + "\" width=\"" + Width.ToString() + "\">";

            return "<img src=\"" + strUrl + "\"  height=\"" + Height.ToString() + "\" width=\"" + Width.ToString() + "\">";
        }

        public static string AssembleIMG(string strURL)
        {
//return "<img src=\"" + strURL.Trim() + "\" border=\"2\">";
            return "<img src=\"" + strURL.Trim() + "\">";
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
                    //Process.Start("chrome.exe", "--allow-running-insecure-content  " + strUrl);
                    Process.Start("chrome.exe", strUrl);
                    break;
            }
        }
        public static int CountSetBits(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count += n & 1;
                n >>= 1;
            }
            return count;
        }
        private static string ColorToHtml(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        //<span style='background-color: #000000; color: white;'>ForeGroundBold</span>
        public static string ApplyColors1(ref TextBox tb)
        {
            Graphics graphics = tb.CreateGraphics();
            int px = (int)Math.Round(tb.Font.SizeInPoints * graphics.DpiY / 72);
            string s = "<span style=\"color: " + tb.ForeColor.Name + "; background-color: " + tb.BackColor.Name + "; ";

            int i = (int)tb.Font.Style;   // enums 1 and 2 are now 3 for bold italix
            if(i == 3)
            {
                s += "font-style: italic; ";
                s += "font-weight: bold; ";
            }
            if (tb.Font.Style == FontStyle.Italic) s += "font-style: italic; ";
            if (tb.Font.Style == FontStyle.Bold) s += "font-weight: bold; ";
            s += "font-size: " + px.ToString() + "px; font-family: '" + tb.Font.Name + "'\"; >";
            s += tb.Text + "</span>";
            return s;
        }
        public static string ApplyColors(ref TextBox tb)
        {
            string sNBI = "";
            string s = tb.Text;
            string sCol = "";
            string sFs = "";
            string sFe = "";
            string sS, sE;
            string sFONTs = "";
            float fontSize = tb.Font.Size - 5;  // html seems to need -5 to scale good
            int iSize = (int)Math.Ceiling(fontSize);
            sFONTs = fontSize.ToString("0.00");
            if (tb.Font.Style == FontStyle.Bold) sNBI = "b";
            if (tb.Font.Style == FontStyle.Italic) sNBI = "i";

            int i = (int)tb.Font.Style;   // enums 1 and 2 are now 3 for bold italix
            Color color = Color.FromArgb(tb.ForeColor.ToArgb());
            sCol = ColorToHtml(color);
            if (i == 3)
            {
                sS = "<b><i>";
                sE = "</b></i>";
            }
            else
            {
                sS = (sNBI == "") ? "" : "<" + sNBI + ">";
                sE = (sNBI == "") ? "" : "</" + sNBI + ">";
            }
            if (sCol != "#000000")
            {
                sFs = "<font color=\"" + sCol + "\" size=\"" + sFONTs + "\">";
                sFe = "</font>";
            }
            else
            {
                sFs = "<font size=\"" + sFONTs + "\">";
                sFe = "</font>";
            }
            return sS + sFs + s + sFe + sE;
        }

        public static string Form1CellTable(string strIn, string sWidth)
        {
            if (sWidth == "")
            {
                return "<table border=\"1\"><tr><td>" + strIn + "</td></tr></table>";
            }
            return "<table border=\"1\" width=\"" + sWidth + "%\"><tr><td>" + strIn + "</td></tr></table>";
        }

        // this puts a newline in the table to make it easier to read the text and copy it
        // the <p> does not work at the HP forum and a double newline is needed
        public static string Form1CellTableP(string strIn, string sWidth)
        {
            if (sWidth == "")
            {
                return "<table border=\"1\"><tr><td><br>&nbsp;" + strIn + "&nbsp;<br><br></td></tr></table>";
            }
            return "<table border=\"1\" width=\"" +sWidth+ "%\"><tr><td><br>&nbsp;" + strIn + "&nbsp;<br><br></td></tr></table>";
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
            int k = i % 52;
            for (int j = 0; j < n; j++)
                strOut += AllAlphas.Substring(k, 1); 
            return strOut;
        }

        // scripting into array r is row c is column sizes
        public static string strFillSubscript(int r, int c, int n)
        {
            int row = n / c;
            int col = n % c;
            int k = n % 52;
            string s ="R"+row.ToString();
            s += "C" + col.ToString() + "_";
            for (int j = 0; j < 4; j++)
                s += AllAlphas.Substring(k, 1);
            return s;
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

        public static void SwapNL(ref TextBox tb)
        {
            if(tb.Text.Length > 0)
            {
                if (tb.Text.Contains("<br>"))
                    tb.Text = tb.Text.Replace("<br>", Environment.NewLine);
                else tb.Text = tb.Text.Replace(Environment.NewLine, "<br>");                   
            }
        }

        //insert a horizontal line
        public static void InsertHR(ref TextBox tbEdit)
        {
            string s1, s2;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j != 0) return;
            int n = tbEdit.Text.Length;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i);
            s1 += "<br><hr><br>" + s2;
            tbEdit.Text = s1;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = 0;
            tbEdit.Focus();
        }

        // add or remove bold text
        public static void AddBold(ref TextBox tbEdit)
        {
            string s1, s2, s3;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j == 0) return;
            int n = tbEdit.Text.Length;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i, j);
            s3 = tbEdit.Text.Substring(i + j);
            // user may have selected several bold so let them all be undone
            if (s2.Contains("<b>"))
            {
                s2 = s2.Replace("<b>", "");
                s2 = s2.Replace("</b>", "");
                tbEdit.Text = s1 + s2 + s3;
                tbEdit.SelectionStart = i;
                tbEdit.SelectionLength = j - (n - tbEdit.Text.Length);
            }
            else
            {
                s1 += "<b>";
                s2 += "</b>";
                tbEdit.Text = s1 + s2 + s3;
                tbEdit.SelectionStart = i;
                tbEdit.SelectionLength = j + 7;
            }
            tbEdit.Focus();
        }

        // add or remove bold text
        public static void RemoveSelectedNL(ref TextBox tbEdit)
        {
            string s1, s2, s3;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j == 0) return;
            int n = tbEdit.Text.Length;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i, j);
            s3 = tbEdit.Text.Substring(i + j);
            // user may have selected several bold so let them all be undone
            s2 = s2.Replace(Environment.NewLine, "");
            tbEdit.Text = s1 + s2 + s3;
            tbEdit.Focus();
        }

        public static void AddColor(ref TextBox tbEdit, string sColor)
        {
            string s1, s2, s3;
            int i = tbEdit.SelectionStart;
            int j = tbEdit.SelectionLength;
            if (j == 0) return;
            s1 = tbEdit.Text.Substring(0, i);
            s2 = tbEdit.Text.Substring(i, j);
            s3 = tbEdit.Text.Substring(i + j);
            if (s2.Contains("<")) return; // not going to restore any previous FUs
            string s = "<font color=\"" + sColor + "\">";
            int n = s.Length;
            s1 += s;
            s = "</font>";
            n += s.Length;
            s2 += s;
            tbEdit.Text = s1 + s2 + s3;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = j + n;
            tbEdit.Focus();
        }

        public static bool IsValidHtmlColor(string color)
        {
            string pattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            return Regex.IsMatch(color, pattern);
        }

        //insert newlines into textbox
        public static void AddNL(ref TextBox tbEdit, int n)
        {
            int i = tbEdit.SelectionStart;
            int j = tbEdit.Text.Length;
            if (i == 0)
            {
                if (j > 0)
                {
                    if (!tbEdit.Focused)
                    {
                        i = j;
                        tbEdit.Focus();
                    }
                }
            }
            tbEdit.Text = tbEdit.Text.Insert(i, "<br><br>".Substring(0, n * 4)); ;
            i += 4 * n;
            tbEdit.SelectionStart = i;
            tbEdit.SelectionLength = 0;
            tbEdit.Focus();
        }

        public static string FormTable(int rows, int cols, bool bFill, int iSize)
        {
            if (rows == 0 && cols == 0) return "";
            if (rows == 0) rows = 1;
            if (cols == 0) cols = 1;
            int jChar = 0;
            string r = rows > 9 ? "00" : "0";
            string c = cols > 9 ? "00" : "0";
            string UseBorder = iSize == 0 ? "<table>" : "<table border='" + iSize.ToString() + "' width=\"50%\">";


            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(UseBorder);

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
            bool bWritten = false;
            string s = eRef(sUrl, ref bWritten);
            int n = s.Length;
            if (!bWritten && n > nLongestExpectedURL)
            {
                RecordLongUrl(sUrl);
            }
            return s;
        }

        private static void RecordLongUrl(string sUrl)
        {
            if (System.Diagnostics.Debugger.IsAttached || bRecordUnscrubbedURLs)
            {
                // keep track of which urls cannot be untracked of de-referenced
                using (StreamWriter writer = File.AppendText(WhereExe + "\\UrlDebug.txt"))
                {
                    writer.WriteLine(sUrl); // we are allowing newlines here
                }
            }
        }

        private static string eRef(string sUrl, ref bool bWritten)
        {
            int i,j;
            sUrl = dStr(sUrl,"/ref");
            string surl = sUrl.ToLower();
            if (surl.Contains(".youtube") || surl.Contains("support.hp.com"))return sUrl;

            i = surl.IndexOf("search?");
            if(i > 0)
            {
                j = sUrl.IndexOf("q=", i + 7);
                if (j < 0) return sUrl;
                i = sUrl.IndexOf('&', j);
                if(i < 0) return sUrl;
                return sUrl.Substring(0, i);
            }

            if(surl.Contains(".aliexpress") || surl.Contains(".ebay"))
            {
                i = sUrl.IndexOf('?');
                return (i < 0) ? sUrl : sUrl.Substring(0, i);
            }

            i = surl.IndexOf("#:~:text=");
            if (i > 0) return sUrl.Substring(0, i);


            i = surl.IndexOf("?utm_source=");  // gets bing and google
            if (i > 0) return sUrl.Substring(0, i);

            i = surl.IndexOf("?gclid=");  // gets crucial
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

            RecordLongUrl(sUrl);
            return sUrl;        
        }

        // could have copied XML instead of HTML
        public static string ChangeBRtoNL(string s)
        {
            string sNL = Environment.NewLine;
            s = s.Replace("<br>", sNL);
            //s = s.Replace("</br>", sNL); // these is error
            s = s.Replace("<br/>", sNL);    // this is error but shows up in some HTML
            s = s.Replace("<br />", sNL);
            return s;
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
                writer.Write(strGEnd);
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

        public static int LengthURL(ref string sBody, int iStart)
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
                if (c == '<') return n;
            }
            return s.Length;
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


        public bool FindUrl(int nLooked, ref string s)
        {

            int iHTTP = 0, iLen = 0;
            string sTMP = s.ToLower();
            iHTTP = sTMP.IndexOf("http");
            if (iHTTP < 0) return false;
            iLen = Utils.LengthURL(ref s, iHTTP);
            string strFound = s.Substring(iHTTP, iLen);
            cReplace(nLooked, ref s, iHTTP, iLen);
            return true;
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

    public class dgvStruct
    {
        public int Inx { get; set; }
        public int HP_HTML_DIF_LOC { get; set; }
        public bool MoveM { get; set; }
        public bool HPerr { get; set; }
        public bool HP_HTML_NO_DIFF { get; set; }
        public string MacName { get; set; }
        public string sErr { get; set; }
        public string sBody { get; set; }
    }
    public class CFound
    {
        public string File { get; set; }
        public string Number { get; set; }
        public string Found { get; set; }    //number of keywords found
        public string Name { get; set; }
        public int WhereFound;
        public int WhichMatch; // bit 0 set = first match, bit 1 set = second, etc
        public bool MayHaveLanguage;
        public bool bWanted; // just want to see this file name one
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
