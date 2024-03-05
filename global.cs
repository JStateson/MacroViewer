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

namespace MacroViewer
{
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
        public void ShowInBrowser(string strIn)
        {
            string strTemp = strPrefix + strIn + strSuffix;
            if (bUseWebView)
            {
                ShowPage MyShowPage = new ShowPage(strTemp);    // this is WebView2 stuff
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

    public class CMacroImage
    {
        public bool bIsRemote;         // if true then the url exists else the file is local only
        public string sRemoteUrl;      // where file is located else it is folder with executable  
        public int ImgNum;             // 0..number of images etc
    }

    public class CMacImages
    {
        public int MacNum;
        public int NextAvailableImgIndex;
        public string strFileLocation;  // path to the file but missing the number and extension
        public string sMacName;
        public List<CMacroImage> MacroImageL;
        public void Init(string strMacname)
        {
            MacroImageL = new List<CMacroImage>();
            sMacName = strMacname;
            NextAvailableImgIndex = 0;
        }
        private string GetImageID(int iNum) // iNum is always 0..count-1
        {
            return strFileLocation + "-" + MacNum.ToString() + "-" + MacroImageL[iNum].ImgNum.ToString() + ".id";
        }
        private string GetFilenameID(int iNum) // iNum is always 0..count-1
        {
            return strFileLocation + "-" + MacNum.ToString() + "-" + iNum.ToString(); // + ".id";
        }
        public string GetImageLocation(int iNum)
        {
            return MacroImageL[iNum].sRemoteUrl;
        }

        public void SetNextIndex()
        {
            NextAvailableImgIndex = 0;
            if(MacroImageL.Count == 0)
            {
                return;
            }
            foreach (CMacroImage img1 in MacroImageL)
            {
                NextAvailableImgIndex = Math.Max(NextAvailableImgIndex, img1.ImgNum);
            }
            NextAvailableImgIndex++;
        }

        public void ReadMacroImages()
        {
            int i = 0;
            NextAvailableImgIndex = -1;
            while (true)
            {
                string strFileN = GetFilenameID(i) + ".id";
                if (File.Exists(strFileN))
                {
                    CMacroImage mi = new CMacroImage();
                    string[] IDs = File.ReadAllLines(strFileN);
                    mi.bIsRemote = IDs[0].Contains("REMOTE");
                    mi.sRemoteUrl = IDs[1];
                    mi.ImgNum = i;
                    NextAvailableImgIndex = Math.Max(NextAvailableImgIndex, i);
                    MacroImageL.Add(mi);
                }
                i++;
                if (i > 20) break;
            }
            NextAvailableImgIndex++;
        }

        public string AssembleImage()
        {
            int n = NextAvailableImgIndex - 1;
            return "<img src=\"" + MacroImageL[n].sRemoteUrl + "\" border=\"2\"  height=\"200\" width=\"200\">";
        }

        public string GetNextFilename()
        {
            return GetFilenameID(NextAvailableImgIndex);
        }

        public void RemoveAll()
        {
            int i = 0;
            while (true)
            {
                string strFileN = GetFilenameID(i) + ".id";
                if (File.Exists(strFileN))
                {
                    File.Delete(strFileN);
                }
                strFileN = GetFilenameID(i) + ".png";
                if (File.Exists(strFileN))
                {
                    File.Delete(strFileN);
                }
                i++;
                if (i > 20) break;
            }
            NextAvailableImgIndex = 0;
            MacroImageL = null;
        }

        public void AddRemoteImage(string strUrl)
        {
            string strFN = GetFilenameID(NextAvailableImgIndex);
            CMacroImage mi = new CMacroImage();
            mi.sRemoteUrl = strUrl;
            mi.bIsRemote = true;
            mi.ImgNum = NextAvailableImgIndex;
            string strOut = "REMOTE" + Environment.NewLine + strUrl;
            File.WriteAllText(strFN + ".id", strOut);
            NextAvailableImgIndex++;
            MacroImageL.Add(mi);
        }

        public void AddLocalImage(ref PictureBox pb)
        {
            string strFN = GetFilenameID(NextAvailableImgIndex);
            ImageConverter converter = new ImageConverter();
            byte[] MyByteArray = (byte[])converter.ConvertTo(pb.Image, typeof(byte[]));
            File.WriteAllBytes(strFN + ".png", MyByteArray);
            string strOut = "LOCAL" + Environment.NewLine + strFN + ".png";
            File.WriteAllText(strFN + ".id", strOut);
            CMacroImage mi = new CMacroImage();
            mi.sRemoteUrl = strFN + ".png";
            mi.bIsRemote = false;
            mi.ImgNum = NextAvailableImgIndex;
            MacroImageL.Add(mi);
            NextAvailableImgIndex++;
        }
    }
}
