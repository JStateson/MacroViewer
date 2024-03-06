using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace MacroViewer
{
    public partial class CSignature : Form
    {
        private string SigLoc = "";
        private string SigFilename = "";
        private XmlDocument xmlDoc = new XmlDocument();
        private string DefaultSig = "<hr /><blockquote><div><font face=\"hpsimplified, arial, sans-serif\" size=\"2.5\">I am a community volunteer.<br /><strong><font color=\"#000000\" size=\"2.75\">If you found the answer helpful and/or you want to say “thanks”? </font></strong>Click the <strong><font color=\"#0059d6\" size=\"2.75\">“ Yes ” box below </font></strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71238i8585EF0CF97FB353/image-dimensions/50x27?v&#61;v2\" />Did I help solve the problem?<strong><font color=\"#f80000\" size=\"2.75\"> don´t forget to </font></strong>click <strong><font color=\" green \" size=\"2.75\">“ Accept as a solution”</font> </strong><img src=\"https://h30467.www3.hp.com/t5/image/serverpage/image-id/71236i432711946C879F03/image-dimensions/129x32?v&#61;v2\" />, someone who has the same query may find this solution and be helped by it.</font></div></blockquote><hr />";
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
            SigFilename = SigLoc + "\\signatures.xml";
            ReadXML();
        }

        private string FormNewSig(string sName, string sBody)
        {
            string strDefault = "<hp_signature>" + Environment.NewLine;
            strDefault+= "<name>" + sName + "</name>" + Environment.NewLine;
            strDefault += "<body>" + Environment.NewLine;
            strDefault += sBody + Environment.NewLine;
            strDefault += "</body>" + Environment.NewLine;
            strDefault += "</hp_signature>" + Environment.NewLine;
            return strDefault;
        }



        private void ReadXML()
        {
            xmlDoc = new XmlDocument();
            if (!File.Exists(SigFilename))
            {
                string strDefault = "<xml>" + Environment.NewLine;
                strDefault += FormNewSig("Default", DefaultSig);
                strDefault += "</xml>";
                File.WriteAllText(SigFilename, strDefault);
            }
            xmlDoc.Load(SigFilename);
            FillTable();
        }

        private void FillTable()
        {
            int i = 0;
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                string sName = node.ChildNodes[0].InnerText;
                string sBody = node.ChildNodes[1].InnerXml;
                sSigListBody.Add(sBody);
                CStringValue sNew = new CStringValue(sName);
                sSigListName.Add(sNew);
                i++;
            }
            dgvSigList.DataSource = sSigListName.ToArray();
            dgvSigList.Columns[0].Name = "Name";
            dgvSigList.Columns["Name"].HeaderText = "Name";
            dgvSigList.Columns[0].Width = dgvSigList.Width;
            ShowSig(0);
        }

        private void dgvSigList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

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
        }

        private void btnDelSig_Click(object sender, EventArgs e)
        {
            int i = dgvSigList.CurrentRow.Index;
        }

        private void btnSaveEdits_Click(object sender, EventArgs e)
        {
            int i = 0;
            string strOut = "<xml>" + Environment.NewLine;
            foreach (DataGridViewRow row in dgvSigList.Rows)
            {
                string Name = row.Cells["Name"].Value.ToString().Trim();
                sSigListName[i].Name = Name;
                string strBody = sSigListBody[i].ToString().Trim();
                strOut += FormNewSig(Name, strBody);
                i++;
            }
            strOut += "</xml>";
            File.WriteAllText(SigFilename, strOut);
        }

        private void bltnSaveBack_Click(object sender, EventArgs e)
        {
            sSigListBody[CurrentRowSelected] = tbBody.Text;
        }

        private void dgvSigList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            ShowSig(i);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadXML();
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
    }
}
