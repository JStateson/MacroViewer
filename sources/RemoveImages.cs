using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MacroViewer.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroViewer
{
    public partial class RemoveImages : Form
    {

        private List<CLocalFiles> LocalImageFiles;
        private string strTempFilePath = "";
        private string strTempFilename = "";
        private int iWidth, iHeight;
        private List<string> DelList;

        public RemoveImages()
        {
            InitializeComponent();
            FillLocalImageTable();
        }

        private List<CLocalFiles> NumLocalImageFiles()
        {
            List<CLocalFiles> MyImages = new List<CLocalFiles>();
            string[] files = Directory.GetFiles(WhereExe, "LOCALIMAGEFILE-*.png");
            foreach (string s in files)
            {
                CLocalFiles cf = new CLocalFiles();
                cf.Name = Path.GetFileName(s);
                cf.NotUsed = true;
                MyImages.Add(cf);
            }
            return MyImages;
        }

        private void btnDelUnused_Click(object sender, EventArgs e)
        {
            int NumDeleted = 0;
            DelList = new List<string>();
            foreach (CLocalFiles cf in LocalImageFiles)
            {
                if (cf.NotUsed)
                {
                    string sFile = Utils.WhereExe + "\\" + cf.Name;
                    DelList.Add(sFile);
                    NumDeleted++;
                }
            }

            if(NumDeleted > 0)
            {
                LocalImageFiles.Clear();
                dgvUsedImages.DataSource = null;
                ClearPB();
            }
            foreach(string s in DelList)
            {
                try
                {
                    File.Delete(s);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to delete file " + s);
                }
            }
            FillLocalImageTable();
        }

        private void FillLocalImageTable()
        {
            string strpath = "";
            string strTotal = "";
            int i = 0;
            int r = dgvUsedImages.Size.Width;
            LocalImageFiles = NumLocalImageFiles();
            foreach (string s in Utils.LocalMacroPrefix)
            {
                strpath = Utils.FNtoPath(s);
                if (!File.Exists(strpath)) continue;
                strTotal += File.ReadAllText(strpath);
            }
            foreach (CLocalFiles cf in LocalImageFiles)
            {
                if (strTotal.Contains(cf.Name))
                {
                    LocalImageFiles[i].NotUsed = false;
                }
                i++;
            }
            dgvUsedImages.DataSource = LocalImageFiles;
            dgvUsedImages.Columns[0].Width = 64;
            dgvUsedImages.Columns[1].Width = r - dgvUsedImages.Columns[0].Width - 36;
        }

        private void dgvUsedImages_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            strTempFilename = dgvUsedImages.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (strTempFilename == "") return;
            strTempFilePath = Utils.WhereExe + "\\" + strTempFilename;
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }
            pbImage.Image = Image.FromFile(strTempFilePath);
        }

        private void RunBrowser(string strTemp)
        {
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }

        private void ClearPB()
        {
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }
        }

        private void RemoveImages_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearPB();
        }

        private void dgvUsedImages_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool bFound = Utils.GetPixSize(strTempFilename, ref iHeight, ref iWidth);
            if (bFound)
            {
                string strImg = Utils.AssembleImage(strTempFilename, iHeight, iWidth);
                RunBrowser(strImg);
            }
        }
    }
}
