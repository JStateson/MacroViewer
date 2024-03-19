using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MacroViewer
{
    public partial class CSignature : Form
    {
        private string SigLoc = "";
        private string SigFilename = "";
        private string DefaultSig = "<div><font face=\"hpsimplified, arial, sans-serif\" size=\"2.5\">I am a community volunteer.<br/><strong><font color=\"#000000\" size=\"2.75\">If you found the answer helpful and/or you want to say “thanks”? </font></strong>Click the <strong><font color=\"#0059d6\" size=\"2.75\">“ Yes ” box below </font></strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71238i8585EF0CF97FB353/image-dimensions/50x27?v&#61;v2\" />Did I help solve the problem?<strong><font color=\"#f80000\" size=\"2.75\"> don´t forget to </font></strong>click <strong><font color=\" green \" size=\"2.75\">“ Accept as a solution”</font> </strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71236i432711946C879F03/image-dimensions/129x32?v&#61;v2\">, someone who has the same query may find this solution and be helped by it.</font></div>";


        public List<string> sSigListBody = new List<string>();
        public int CurrentRowSelected = 0; // this corresponds to the body of the text and the name selected

        private void ShowSig(int iRow)
        {
            if (iRow < 0) return;
            CurrentRowSelected = iRow;
            tbBody.Text = sSigListBody[CurrentRowSelected].ToString();
        }

        public CSignature()
        {
            InitializeComponent();
            SigLoc = Utils.WhereExe;
            SigFilename = SigLoc + "\\signatures.txt";
            ReadSIG();
            foreach (DataGridViewColumn column in dgvSigList.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private string FormNewSig(string sName, string sBody)
        {
            string strDefault = sName + Environment.NewLine;
            strDefault += sBody + Environment.NewLine;
            return strDefault;
        }


        private void CreateSigFromDefault()
        {
            string strDefault = FormNewSig("jys1", DefaultSig);
            File.WriteAllText(SigFilename, strDefault);
        }

        private void ReadSIG()
        {
            if (!File.Exists(SigFilename))
            {
                CreateSigFromDefault();
            }
            dgvSigList.Rows.Clear();
            sSigListBody.Clear();
            FillTable();
        }

        private int FillTable()
        {
            int i = 0;
            if (File.Exists(SigFilename))
            {
                StreamReader sr = new StreamReader(SigFilename);
                string sName = sr.ReadLine();
                string sBody = sr.ReadLine();
                sSigListBody.Add(sBody);
                //DataGridViewRow row = new DataGridViewRow();
                dgvSigList.Rows.Add(sName);
                while(true)
                {
                    i++;
                    sName = sr.ReadLine();
                    if (sName == null) break;
                    if (sName == "") break;
                    sBody = sr.ReadLine();
                    if(sBody == null) break;
                    if (sBody == "") break;
                    dgvSigList.Rows.Add(sName);
                    sSigListBody.Add(sBody);
                }
                sr.Close();
            }
            dgvSigList.Columns[0].Width = dgvSigList.Width;
            ShowSig(0);
            return i;
        }

        private void dgvSigList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            RunBrowser(SigLoc);
            ShowSig(i);
        }
        private void RunBrowser(string sLoc)
        {
            string strTemp = tbBody.Text;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(strTemp);
        }

        private void btnShowBrowser_Click(object sender, EventArgs e)
        {
            RunBrowser(SigLoc);
        }

        private void blnAdd_Click(object sender, EventArgs e)
        {
            dgvSigList.Rows.Add("ChangeMe");
            tbBody.Text = "ChangeMe";
            sSigListBody.Add(tbBody.Text);
        }

        private void btnDelSig_Click(object sender, EventArgs e)
        {
            int i = dgvSigList.CurrentRow.Index;
//            sSigListName.RemoveAt(i);
            dgvSigList.Rows.RemoveAt(i);
            //dgvSigList.DataSource = sSigListName;
        }

        private void btnSaveEdits_Click(object sender, EventArgs e)
        {
            int i = 0;
            string strOut = "";
            foreach (DataGridViewRow row in dgvSigList.Rows)
            {
                string Name = row.Cells[0].Value.ToString().Trim();
                string strBody = sSigListBody[i].ToString().Trim();
                strOut += FormNewSig(Name, strBody);
                i++;
            }
            File.WriteAllText(SigFilename, strOut);
        }

        private void bltnSaveBack_Click(object sender, EventArgs e)
        {
            tbBody.Text = Utils.RemoveNL(tbBody.Text);
            sSigListBody[CurrentRowSelected] = tbBody.Text;
        }

        private void dgvSigList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            ShowSig(i);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadSIG();
        }

        private void btnToNote_Click(object sender, EventArgs e)
        {
            CSendNotepad SendNotepad = new CSendNotepad();
            string npTitle = dgvSigList.CurrentRow.Cells[0].Value.ToString() + Environment.NewLine + tbBody.Text;
            SendNotepad.PasteToNotepad(npTitle);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbBody.Text = "";
        }

        private void btnAdd1New_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            tbBody.Text = tbBody.Text.Insert(i, "<br>");
            i += 4;
            tbBody.SelectionStart = i;
        }

        private void btnAdd2New_Click(object sender, EventArgs e)
        {
            int i = tbBody.SelectionStart;
            tbBody.Text = tbBody.Text.Insert(i, "<br><br>");
            i += 8;
            tbBody.SelectionStart = i;
        }
        
        private void TestBBC()
        {
            //btnSaveEdits.Enabled = false;
            string strTemp = Utils.RemoveNL(tbBody.Text);
            string strErr = Utils.BBCparse(strTemp);
            if (strErr == "")
            {
                //btnSaveEdits.Enabled = true;
                return;
            }
            DialogResult Res1 = MessageBox.Show(strErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void btnTestXML_Click(object sender, EventArgs e)
        {
            TestBBC();
        }
    }
}
