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

        public main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.html";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
            int nLength, n;
            ofd.ShowDialog();
            string strFileName = ofd.FileName;
            aPage = File.ReadAllText(strFileName);
            nLength = aPage.Length;
            ParsePage();
        }



        private void btnGo_Click(object sender, EventArgs e)
        {
            string strTemp = "<!DOCTYPE html>\r\n<html>\r\n<head>" + tbBody.Text + "\r\n</body>\r\n</html>";
            webBrowser1.DocumentText = strTemp;
        }

        private void lbName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            tbBody.Text = Body[i];
            string strTemp = "<!DOCTYPE html>\r\n<html>\r\n<head>" + tbBody.Text + "\r\n</body>\r\n</html>";
            webBrowser1.DocumentText = strTemp;
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
    }
}
