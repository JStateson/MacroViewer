﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MacroViewer
{
    public partial class MoveMacro : Form
    {
        int x = 30, y = 30;
        CMoveSpace cms;
        CheckBox cbFROMbox;

        public MoveMacro(ref CMoveSpace rcms)
        {
            InitializeComponent();
            cms = rcms;
            FillFrom(ref gbFrom);
            FillTo(ref gbTo);
            tbNumMoving.Text = cms.nChecked.ToString();
        }

        private void FillFrom(ref GroupBox gb)
        {
            int n = Utils.LocalMacroPrefix.Length;
            CheckBox rb = new CheckBox();
            for (int i = 0; i < n; i++)
            {
                string s = Utils.LocalMacroPrefix[i];
                rb = new CheckBox();
                rb.Text = s + "macro";
                rb.Name = s + i.ToString();
                rb.Location = new System.Drawing.Point(x, y + (i) * 30);
                rb.Enabled = (cms.strType == s);
                rb.CheckedChanged += CheckBox_CheckedChanged;
                gb.Controls.Add(rb);
            }
            foreach(CheckBox cb in gbFrom.Controls)
            {
                if(cb.Enabled)
                {
                    cb.Checked = cms.nChecked > 0;
                    if (!cb.Checked)
                        cb.Enabled = false; // do not let use check the box if none were checked originally
                    cbFROMbox = cb;
                    return;
                }
            }
        }



        private void FillTo(ref GroupBox gb)
        {
            int n = Utils.LocalMacroPrefix.Length;  //number of files
            RadioButton rb;
            for (int i = 0; i < n; i++)
            {
                string s = Utils.LocalMacroPrefix[i];
                rb = new RadioButton();
                rb.AutoSize = true;
                rb.Text = s + "macro - " + Utils.LocalMacroFullname[i];
                rb.Name = s;
                rb.Location = new System.Drawing.Point(x, y + (i) * 30);
                rb.Enabled = (cms.strType != s);
                rb.CheckedChanged += RadioButton_CheckedChanged;
                gb.Controls.Add(rb);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            int n = -1;
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                cms.strDes = rb.Name;
                n = cms.GetMacCountAvailable(cms.strDes);
                tbNFree.Text = n.ToString();
                btnMove.Enabled = false;
                if (n >= cms.nChecked)
                {
                    if (cbFROMbox != null)
                    {
                        if(cbFROMbox.Checked)
                            btnMove.Enabled = true;
                    }
                }
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cbFROMbox = (CheckBox)sender;
            if (!cbFROMbox.Checked)
                btnMove.Enabled = false;
            else
            {
                if (tbNFree.Text == null) return;
                if (tbNFree.Text == "") return;
                int n = Convert.ToInt32(tbNFree.Text);
                btnMove.Enabled = (n > cms.nChecked);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            cms.bRun = true;
            cms.UpdateCount();
            this.Close();
        }

    }
}
