using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace MacroViewer
{
    public partial class main : Form
    {
        string aPage;
        const int NumMacros = 30;
        int[] StartMac = new int[NumMacros];
        int[] StopMac = new int[NumMacros];
        int[] MacBody = new int[NumMacros];
        string[] Body = new string[NumMacros];


        OpenFileDialog ofd;
        bool UseWebView = false;
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


        public main()
        {
            InitializeComponent();
            UseWebView = WebViewIsInstalled();
        }




        private static string GetDownloadsPath()
        {
            if (Environment.OSVersion.Version.Major < 6) throw new NotSupportedException();
            IntPtr pathPtr = IntPtr.Zero;
            try
            {
                SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }


        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        private string GetLastFolder()
        {
            string LastFolder = Properties.Settings.Default.LastFolder;
            if (LastFolder == null || LastFolder == "")
            {
                LastFolder = GetDownloadsPath();
                if(!Directory.Exists(LastFolder))
                    LastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return LastFolder;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {

            ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.html";
            ofd.InitialDirectory = GetLastFolder();
        }


        private bool FindBody()
        {

            string strFind; //<span class="html-attribute-value">profilemacro_2</span>"&gt;</span>
            string strEnd = "<span class=\"html-tag\">";
            int j, k, n;
            for (int i = 0; i < NumMacros; i++)
            {
                strFind = "<span class=\"html-attribute-value\">profilemacro_" + (i + 1).ToString() + "</span>\"&gt;</span>";
                j = aPage.Substring(MacBody[i]).IndexOf(strFind);
                if (j < 0) return false;
                n = MacBody[i] + j + strFind.Length;
                k = aPage.Substring(n).IndexOf(strEnd);
                if (k < 0) return false;
                string strBody = aPage.Substring(n, k).Replace("lt;", "<");
                Body[i] = strBody.Replace("&amp;", "").Replace("gt;",">");
            }
            return true;
        }

        private bool FindNames()
        {
            int j, k, n;
            string strFind = "<span class=\"html-attribute-name\">value</span>=\"<span class=\"html-attribute-value\">";
            lbName.Rows.Clear();
            for (int i = 0; i < NumMacros; i++)
            {
                j = aPage.Substring(StartMac[i]).IndexOf(strFind);
                if (j < 0) return false;
                n = StartMac[i] + j + strFind.Length;
                k = aPage.Substring(n).IndexOf("</span>");
                if(k < 0) return false;
                string strName = aPage.Substring(n, k);
                MacBody[i] = n+k+1;
                lbName.Rows.Add(i+1,strName);
            }
            return true;
        }



        private bool FindMacros()
        {
            int j, k;
            for(int i = 0; i < NumMacros; i++)
            {
                string strFind = "Macro " + (i+1).ToString();
                j = aPage.IndexOf(strFind);
                if (j < 0) return false;
                StartMac[i] = j;
                j += strFind.Length;
                k = aPage.Substring(j).IndexOf(strFind);
                if(k < 0) return false;
                StopMac[i] = k+j;
            }   
            return true;
        }

        private void ParsePage()
        {
            
            FindMacros();
            FindNames();
            FindBody();
        }

        private void ReadMacroHTML()
        {
            int nLength;
            string LastFolder = "";
            ofd.ShowDialog();
            string strFileName = ofd.FileName;
            if (!File.Exists(strFileName)) return;
            LastFolder = Path.GetDirectoryName(ofd.FileName);
            Properties.Settings.Default.LastFolder = LastFolder;
            aPage = File.ReadAllText(strFileName);
            nLength = aPage.Length;
            ParsePage();
        }



        private void btnGo_Click(object sender, EventArgs e)
        {
            string strTemp = "<!DOCTYPE html>\r\n<html>\r\n<head>" + tbBody.Text + "\r\n</body>\r\n</html>";
            if (UseWebView)
            {
                ShowPage MyShowPage = new ShowPage(strTemp);
                MyShowPage.Show();
            }
        }

        private void lbName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            tbBody.Text = Body[i];
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadMacroHTML();
        }

        private void utilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utils MyUtils = new utils();
            MyUtils.ShowDialog();
            MyUtils.Dispose();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help MyHelp = new help();
            MyHelp.ShowDialog();
            MyHelp.Dispose();
        }

        private void btnClearEM_Click(object sender, EventArgs e)
        {
            tbBody.Clear();
        }

        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbBody.Text);
        }

        private void btnCopyFrom_Click(object sender, EventArgs e)
        {
            tbBody.Text = Clipboard.GetText();
        }
    }
}
