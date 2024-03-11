using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace MacroViewer
{
    public partial class CSignature : Form
    {
        private string SigLoc = "";
        private string SigFilename = "";
        private string DefaultSig = "<div><font face=\"hpsimplified, arial, sans-serif\" size=\"2.5\">I am a community volunteer.<br/><strong><font color=\"#000000\" size=\"2.75\">If you found the answer helpful and/or you want to say “thanks”? </font></strong>Click the <strong><font color=\"#0059d6\" size=\"2.75\">“ Yes ” box below </font></strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71238i8585EF0CF97FB353/image-dimensions/50x27?v&#61;v2\" />Did I help solve the problem?<strong><font color=\"#f80000\" size=\"2.75\"> don´t forget to </font></strong>click <strong><font color=\" green \" size=\"2.75\">“ Accept as a solution”</font> </strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71236i432711946C879F03/image-dimensions/129x32?v&#61;v2\">, someone who has the same query may find this solution and be helped by it.</font></div>";
        public class CStringValue
        {
            public CStringValue(string s)
            {
                _value = s;
            }
            public string Name { get { return _value; } set { _value = value; } }
            string _value;
        }
        private List<string> sSigListBody = new List<string>();
        private List<CStringValue> sSigListName = new List<CStringValue>();
        private int CurrentRowSelected = 0; // this corresponds to the body of the text and the name selected

        private void ShowSig(int iRow)
        {
            if (iRow < 0) return;
            CurrentRowSelected = iRow;
            tbBody.Text = sSigListBody[CurrentRowSelected].ToString();
        }

        public CSignature()
        {
            InitializeComponent();
            SigLoc = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            SigFilename = SigLoc + "\\signatures.txt";
            ReadSIG();
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
                CStringValue sNew = new CStringValue(sName);
                sSigListName.Add(sNew);
                while(true)
                {
                    i++;
                    sName = sr.ReadLine();
                    if (sName == null) break;
                    sBody = sr.ReadLine();
                    sSigListBody.Add(sBody);
                    sNew = new CStringValue(sName);
                    sSigListName.Add(sNew);
                }
            }
            dgvSigList.DataSource = sSigListName.ToArray();
            dgvSigList.Columns[0].Name = "Name";
            dgvSigList.Columns["Name"].HeaderText = "Name";
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
            MyBrowser.ShowInBrowser(sLoc, strTemp);
        }

        private void btnShowBrowser_Click(object sender, EventArgs e)
        {
            RunBrowser(SigLoc);
        }

        private void blnAdd_Click(object sender, EventArgs e)
        {
            int n = dgvSigList.RowCount;
            sSigListBody.Add("ChangeMe");
            tbBody.Text = "ChangeMe";
            CStringValue sNew = new  CStringValue("ChangeName");
            sSigListName.Add(sNew);
            dgvSigList.DataSource = sSigListName.ToArray();
            btnSaveEdits.Enabled = false;
        }

        private void btnDelSig_Click(object sender, EventArgs e)
        {
            int i = dgvSigList.CurrentRow.Index;
        }

        private void btnSaveEdits_Click(object sender, EventArgs e)
        {
            int i = 0;
            string strOut = "";
            foreach (DataGridViewRow row in dgvSigList.Rows)
            {
                string Name = row.Cells["Name"].Value.ToString().Trim();
                sSigListName[i].Name = Name;
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
            SendNotepad.PasteToNotepad(tbBody.Text);
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
            btnSaveEdits.Enabled = false;
            string strTemp = Utils.RemoveNL(tbBody.Text);
            string strErr = Utils.BBCparse(strTemp);
            if (strErr == "")
            {
                btnSaveEdits.Enabled = true;
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
