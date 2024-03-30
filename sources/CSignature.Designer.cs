namespace MacroViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSignature));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbBody = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveEdits = new System.Windows.Forms.Button();
            this.btnDelSig = new System.Windows.Forms.Button();
            this.blnAdd = new System.Windows.Forms.Button();
            this.dgvSigList = new System.Windows.Forms.DataGridView();
            this.sName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbEditSig = new System.Windows.Forms.GroupBox();
            this.btnPasteImg = new System.Windows.Forms.Button();
            this.btnAdd2New = new System.Windows.Forms.Button();
            this.btnAdd1New = new System.Windows.Forms.Button();
            this.btnTestXML = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnToNote = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowBrowser = new System.Windows.Forms.Button();
            this.bltnSaveBack = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNLtoNotepad = new System.Windows.Forms.Button();
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
            this.menuStrip1.Size = new System.Drawing.Size(1168, 24);
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
            this.tbBody.Location = new System.Drawing.Point(43, 235);
            this.tbBody.Multiline = true;
            this.tbBody.Name = "tbBody";
            this.tbBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbBody.Size = new System.Drawing.Size(504, 301);
            this.tbBody.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSaveEdits);
            this.groupBox1.Controls.Add(this.btnDelSig);
            this.groupBox1.Controls.Add(this.blnAdd);
            this.groupBox1.Controls.Add(this.dgvSigList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(28, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 552);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sig and Image List";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(41, 443);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(370, 80);
            this.label3.TabIndex = 16;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(11, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 48);
            this.label1.TabIndex = 14;
            this.label1.Text = "Be sure to save\r\nedits if you added\r\nor remove a sig";
            // 
            // btnSaveEdits
            // 
            this.btnSaveEdits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveEdits.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSaveEdits.Location = new System.Drawing.Point(14, 271);
            this.btnSaveEdits.Name = "btnSaveEdits";
            this.btnSaveEdits.Size = new System.Drawing.Size(152, 29);
            this.btnSaveEdits.TabIndex = 13;
            this.btnSaveEdits.Text = "Save all in list";
            this.btnSaveEdits.UseVisualStyleBackColor = true;
            this.btnSaveEdits.Click += new System.EventHandler(this.btnSaveEdits_Click);
            // 
            // btnDelSig
            // 
            this.btnDelSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelSig.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnDelSig.Location = new System.Drawing.Point(14, 214);
            this.btnDelSig.Name = "btnDelSig";
            this.btnDelSig.Size = new System.Drawing.Size(152, 29);
            this.btnDelSig.TabIndex = 2;
            this.btnDelSig.Text = "Delete Selected";
            this.btnDelSig.UseVisualStyleBackColor = true;
            this.btnDelSig.Click += new System.EventHandler(this.btnDelSig_Click);
            // 
            // blnAdd
            // 
            this.blnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blnAdd.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.blnAdd.Location = new System.Drawing.Point(14, 157);
            this.blnAdd.Name = "blnAdd";
            this.blnAdd.Size = new System.Drawing.Size(152, 29);
            this.blnAdd.TabIndex = 1;
            this.blnAdd.Text = "Add Sig or Image";
            this.blnAdd.UseVisualStyleBackColor = true;
            this.blnAdd.Click += new System.EventHandler(this.blnAdd_Click);
            // 
            // dgvSigList
            // 
            this.dgvSigList.AllowUserToAddRows = false;
            this.dgvSigList.AllowUserToResizeColumns = false;
            this.dgvSigList.AllowUserToResizeRows = false;
            this.dgvSigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSigList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sName});
            this.dgvSigList.Location = new System.Drawing.Point(191, 38);
            this.dgvSigList.MultiSelect = false;
            this.dgvSigList.Name = "dgvSigList";
            this.dgvSigList.RowHeadersVisible = false;
            this.dgvSigList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSigList.Size = new System.Drawing.Size(243, 384);
            this.dgvSigList.TabIndex = 0;
            this.dgvSigList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSigList_CellClick);
            this.dgvSigList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSigList_CellContentDoubleClick);
            // 
            // sName
            // 
            this.sName.HeaderText = "Name";
            this.sName.Name = "sName";
            this.sName.Width = 240;
            // 
            // gbEditSig
            // 
            this.gbEditSig.Controls.Add(this.btnNLtoNotepad);
            this.gbEditSig.Controls.Add(this.btnPasteImg);
            this.gbEditSig.Controls.Add(this.btnAdd2New);
            this.gbEditSig.Controls.Add(this.btnAdd1New);
            this.gbEditSig.Controls.Add(this.btnTestXML);
            this.gbEditSig.Controls.Add(this.btnClear);
            this.gbEditSig.Controls.Add(this.btnToNote);
            this.gbEditSig.Controls.Add(this.label2);
            this.gbEditSig.Controls.Add(this.btnShowBrowser);
            this.gbEditSig.Controls.Add(this.bltnSaveBack);
            this.gbEditSig.Controls.Add(this.tbBody);
            this.gbEditSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEditSig.Location = new System.Drawing.Point(518, 40);
            this.gbEditSig.Name = "gbEditSig";
            this.gbEditSig.Size = new System.Drawing.Size(617, 552);
            this.gbEditSig.TabIndex = 3;
            this.gbEditSig.TabStop = false;
            this.gbEditSig.Text = "Sig Edit";
            // 
            // btnPasteImg
            // 
            this.btnPasteImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasteImg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPasteImg.Location = new System.Drawing.Point(43, 173);
            this.btnPasteImg.Name = "btnPasteImg";
            this.btnPasteImg.Size = new System.Drawing.Size(161, 34);
            this.btnPasteImg.TabIndex = 21;
            this.btnPasteImg.Text = "Paste as image";
            this.toolTip1.SetToolTip(this.btnPasteImg, "Copy the signature back to the list table");
            this.btnPasteImg.UseVisualStyleBackColor = true;
            this.btnPasteImg.Click += new System.EventHandler(this.btnPasteImg_Click);
            // 
            // btnAdd2New
            // 
            this.btnAdd2New.Location = new System.Drawing.Point(261, 179);
            this.btnAdd2New.Name = "btnAdd2New";
            this.btnAdd2New.Size = new System.Drawing.Size(108, 23);
            this.btnAdd2New.TabIndex = 20;
            this.btnAdd2New.Text = "Add 2 newlines";
            this.toolTip1.SetToolTip(this.btnAdd2New, "inserts <br />");
            this.btnAdd2New.UseVisualStyleBackColor = true;
            this.btnAdd2New.Click += new System.EventHandler(this.btnAdd2New_Click);
            // 
            // btnAdd1New
            // 
            this.btnAdd1New.Location = new System.Drawing.Point(278, 145);
            this.btnAdd1New.Name = "btnAdd1New";
            this.btnAdd1New.Size = new System.Drawing.Size(91, 23);
            this.btnAdd1New.TabIndex = 19;
            this.btnAdd1New.Text = "Add newline";
            this.toolTip1.SetToolTip(this.btnAdd1New, "inserts <br>");
            this.btnAdd1New.UseVisualStyleBackColor = true;
            this.btnAdd1New.Click += new System.EventHandler(this.btnAdd1New_Click);
            // 
            // btnTestXML
            // 
            this.btnTestXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestXML.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTestXML.Location = new System.Drawing.Point(43, 112);
            this.btnTestXML.Name = "btnTestXML";
            this.btnTestXML.Size = new System.Drawing.Size(113, 34);
            this.btnTestXML.TabIndex = 18;
            this.btnTestXML.Text = "Test";
            this.toolTip1.SetToolTip(this.btnTestXML, "Copy the signature back to the list table");
            this.btnTestXML.UseVisualStyleBackColor = true;
            this.btnTestXML.Click += new System.EventHandler(this.btnTestXML_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.Location = new System.Drawing.Point(43, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(113, 34);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.btnClear, "Copy the signature back to the list table");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnToNote
            // 
            this.btnToNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToNote.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnToNote.Location = new System.Drawing.Point(406, 125);
            this.btnToNote.Name = "btnToNote";
            this.btnToNote.Size = new System.Drawing.Size(173, 34);
            this.btnToNote.TabIndex = 16;
            this.btnToNote.Text = "Copy as is to notepad";
            this.toolTip1.SetToolTip(this.btnToNote, "Copy the signatur to notepad including linebreak markup");
            this.btnToNote.UseVisualStyleBackColor = true;
            this.btnToNote.Click += new System.EventHandler(this.btnToNote_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(185, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 96);
            this.label2.TabIndex = 15;
            this.label2.Text = "Be sure to copy changes back\r\n(Apply) to Sig List and Save.\r\n\r\nUse the PASTE AS I" +
    "MAGE\r\nto paste the address of an\r\nimage from the Clipboard";
            // 
            // btnShowBrowser
            // 
            this.btnShowBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowBrowser.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnShowBrowser.Location = new System.Drawing.Point(406, 21);
            this.btnShowBrowser.Name = "btnShowBrowser";
            this.btnShowBrowser.Size = new System.Drawing.Size(141, 34);
            this.btnShowBrowser.TabIndex = 3;
            this.btnShowBrowser.Text = "Show in Browser";
            this.btnShowBrowser.UseVisualStyleBackColor = true;
            this.btnShowBrowser.Click += new System.EventHandler(this.btnShowBrowser_Click);
            // 
            // bltnSaveBack
            // 
            this.bltnSaveBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bltnSaveBack.ForeColor = System.Drawing.SystemColors.Highlight;
            this.bltnSaveBack.Location = new System.Drawing.Point(43, 21);
            this.bltnSaveBack.Name = "bltnSaveBack";
            this.bltnSaveBack.Size = new System.Drawing.Size(113, 34);
            this.bltnSaveBack.TabIndex = 2;
            this.bltnSaveBack.Text = "Apply";
            this.toolTip1.SetToolTip(this.bltnSaveBack, "Copy the signature back to the list table");
            this.bltnSaveBack.UseVisualStyleBackColor = true;
            this.bltnSaveBack.Click += new System.EventHandler(this.bltnSaveBack_Click);
            // 
            // btnNLtoNotepad
            // 
            this.btnNLtoNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNLtoNotepad.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNLtoNotepad.Location = new System.Drawing.Point(406, 74);
            this.btnNLtoNotepad.Name = "btnNLtoNotepad";
            this.btnNLtoNotepad.Size = new System.Drawing.Size(141, 34);
            this.btnNLtoNotepad.TabIndex = 22;
            this.btnNLtoNotepad.Text = "Copy to notepad";
            this.toolTip1.SetToolTip(this.btnNLtoNotepad, "Copy the signature using newlines");
            this.btnNLtoNotepad.UseVisualStyleBackColor = true;
            this.btnNLtoNotepad.Click += new System.EventHandler(this.btnNLtoNotepad_Click);
            // 
            // CSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 622);
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
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnTestXML;
        private System.Windows.Forms.Button btnAdd2New;
        private System.Windows.Forms.Button btnAdd1New;
        private System.Windows.Forms.DataGridViewTextBoxColumn sName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPasteImg;
        private System.Windows.Forms.Button btnNLtoNotepad;
    }
}