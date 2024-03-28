using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MacroViewer.Utils;

namespace MacroViewer
{
    public partial class RemoveImages : Form
    {

        private List<CLocalFiles> LocalImageFiles;
        private string strTempFilePath = "";
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
            foreach (CLocalFiles cf in LocalImageFiles)
            {
                if (cf.NotUsed)
                {
                    string sFile = Utils.WhereExe + "\\" + cf.Name;
                    try
                    {
                        File.Delete(sFile);
                        NumDeleted++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex.Message, "Unable to delete", MessageBoxButtons.OKCancel);
                    }
                }
            }
            if (NumDeleted > 0)
            {
                LocalImageFiles.Clear();
                FillLocalImageTable();
            }
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
            strTempFilePath = dgvUsedImages.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (strTempFilePath == "") return;
            strTempFilePath = Utils.WhereExe + "\\" + strTempFilePath;
            pbImage.Image = Image.FromFile(strTempFilePath);
        }

    }
}
