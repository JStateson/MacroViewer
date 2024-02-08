﻿namespace MacroViewer
{
    partial class CSignature
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbBody = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveEdits = new System.Windows.Forms.Button();
            this.btnDelSig = new System.Windows.Forms.Button();
            this.blnAdd = new System.Windows.Forms.Button();
            this.dgvSigList = new System.Windows.Forms.DataGridView();
            this.gbEditSig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowBrowser = new System.Windows.Forms.Button();
            this.bltnSaveBack = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnToNote = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSigList)).BeginInit();
            this.gbEditSig.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1122, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.fileToolStripMenuItem.Text = "ReOpen";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // tbBody
            // 
            this.tbBody.Location = new System.Drawing.Point(43, 100);
            this.tbBody.Multiline = true;
            this.tbBody.Name = "tbBody";
            this.tbBody.Size = new System.Drawing.Size(504, 322);
            this.tbBody.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSaveEdits);
            this.groupBox1.Controls.Add(this.btnDelSig);
            this.groupBox1.Controls.Add(this.blnAdd);
            this.groupBox1.Controls.Add(this.dgvSigList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(28, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 459);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sig List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(18, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 48);
            this.label1.TabIndex = 14;
            this.label1.Text = "Be sure to save\r\nedits if you added\r\nor remove a sig";
            // 
            // btnSaveEdits
            // 
            this.btnSaveEdits.Location = new System.Drawing.Point(21, 271);
            this.btnSaveEdits.Name = "btnSaveEdits";
            this.btnSaveEdits.Size = new System.Drawing.Size(90, 23);
            this.btnSaveEdits.TabIndex = 13;
            this.btnSaveEdits.Text = "Save Edits";
            this.btnSaveEdits.UseVisualStyleBackColor = true;
            this.btnSaveEdits.Click += new System.EventHandler(this.btnSaveEdits_Click);
            // 
            // btnDelSig
            // 
            this.btnDelSig.Location = new System.Drawing.Point(21, 214);
            this.btnDelSig.Name = "btnDelSig";
            this.btnDelSig.Size = new System.Drawing.Size(90, 23);
            this.btnDelSig.TabIndex = 2;
            this.btnDelSig.Text = "Delete Sig";
            this.btnDelSig.UseVisualStyleBackColor = true;
            this.btnDelSig.Click += new System.EventHandler(this.btnDelSig_Click);
            // 
            // blnAdd
            // 
            this.blnAdd.Location = new System.Drawing.Point(21, 157);
            this.blnAdd.Name = "blnAdd";
            this.blnAdd.Size = new System.Drawing.Size(90, 23);
            this.blnAdd.TabIndex = 1;
            this.blnAdd.Text = "Add Sig";
            this.blnAdd.UseVisualStyleBackColor = true;
            this.blnAdd.Click += new System.EventHandler(this.blnAdd_Click);
            // 
            // dgvSigList
            // 
            this.dgvSigList.AllowUserToResizeColumns = false;
            this.dgvSigList.AllowUserToResizeRows = false;
            this.dgvSigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSigList.Location = new System.Drawing.Point(151, 38);
            this.dgvSigList.MultiSelect = false;
            this.dgvSigList.Name = "dgvSigList";
            this.dgvSigList.Size = new System.Drawing.Size(240, 384);
            this.dgvSigList.TabIndex = 0;
            this.dgvSigList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSigList_CellClick);
            // 
            // gbEditSig
            // 
            this.gbEditSig.Controls.Add(this.btnToNote);
            this.gbEditSig.Controls.Add(this.label2);
            this.gbEditSig.Controls.Add(this.btnShowBrowser);
            this.gbEditSig.Controls.Add(this.bltnSaveBack);
            this.gbEditSig.Controls.Add(this.tbBody);
            this.gbEditSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEditSig.Location = new System.Drawing.Point(491, 40);
            this.gbEditSig.Name = "gbEditSig";
            this.gbEditSig.Size = new System.Drawing.Size(582, 459);
            this.gbEditSig.TabIndex = 3;
            this.gbEditSig.TabStop = false;
            this.gbEditSig.Text = "Sig Edit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(202, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 48);
            this.label2.TabIndex = 15;
            this.label2.Text = "Be sure to copy changes\r\nback to Sig List and be sure\r\nto save changes.";
            // 
            // btnShowBrowser
            // 
            this.btnShowBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowBrowser.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnShowBrowser.Location = new System.Drawing.Point(406, 21);
            this.btnShowBrowser.Name = "btnShowBrowser";
            this.btnShowBrowser.Size = new System.Drawing.Size(141, 23);
            this.btnShowBrowser.TabIndex = 3;
            this.btnShowBrowser.Text = "Show in Browser";
            this.btnShowBrowser.UseVisualStyleBackColor = true;
            this.btnShowBrowser.Click += new System.EventHandler(this.btnShowBrowser_Click);
            // 
            // bltnSaveBack
            // 
            this.bltnSaveBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bltnSaveBack.ForeColor = System.Drawing.SystemColors.Highlight;
            this.bltnSaveBack.Location = new System.Drawing.Point(43, 41);
            this.bltnSaveBack.Name = "bltnSaveBack";
            this.bltnSaveBack.Size = new System.Drawing.Size(113, 23);
            this.bltnSaveBack.TabIndex = 2;
            this.bltnSaveBack.Text = "Copy back";
            this.toolTip1.SetToolTip(this.bltnSaveBack, "Copy the signature back to the list table");
            this.bltnSaveBack.UseVisualStyleBackColor = true;
            this.bltnSaveBack.Click += new System.EventHandler(this.bltnSaveBack_Click);
            // 
            // btnToNote
            // 
            this.btnToNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToNote.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnToNote.Location = new System.Drawing.Point(406, 61);
            this.btnToNote.Name = "btnToNote";
            this.btnToNote.Size = new System.Drawing.Size(141, 23);
            this.btnToNote.TabIndex = 16;
            this.btnToNote.Text = "Copy to notepad";
            this.toolTip1.SetToolTip(this.btnToNote, "Copy the signatur to notepad");
            this.btnToNote.UseVisualStyleBackColor = true;
            this.btnToNote.Click += new System.EventHandler(this.btnToNote_Click);
            // 
            // CSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 540);
            this.Controls.Add(this.gbEditSig);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CSignature";
            this.Text = "Signature";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSigList)).EndInit();
            this.gbEditSig.ResumeLayout(false);
            this.gbEditSig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TextBox tbBody;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelSig;
        private System.Windows.Forms.Button blnAdd;
        private System.Windows.Forms.DataGridView dgvSigList;
        private System.Windows.Forms.GroupBox gbEditSig;
        private System.Windows.Forms.Button bltnSaveBack;
        private System.Windows.Forms.Button btnShowBrowser;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSaveEdits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnToNote;
    }
}