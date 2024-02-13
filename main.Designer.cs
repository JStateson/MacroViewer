namespace MacroViewer
{
    partial class main
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbName = new System.Windows.Forms.DataGridView();
            this.Inx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MacName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGo = new System.Windows.Forms.Button();
            this.btnAdd1New = new System.Windows.Forms.Button();
            this.btnAdd2New = new System.Windows.Forms.Button();
            this.tbBody = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSignatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAddURL = new System.Windows.Forms.Button();
            this.tbImgUrl = new System.Windows.Forms.TextBox();
            this.btnAddImg = new System.Windows.Forms.Button();
            this.gbSupp = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbNumMac = new System.Windows.Forms.TextBox();
            this.tbMacName = new System.Windows.Forms.TextBox();
            this.btnSaveM = new System.Windows.Forms.Button();
            this.btnDelM = new System.Windows.Forms.Button();
            this.btnAddM = new System.Windows.Forms.Button();
            this.btnToNotepad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopyFrom = new System.Windows.Forms.Button();
            this.btnCopyTo = new System.Windows.Forms.Button();
            this.btnClearEM = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbLaunchPage = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.lbName)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbSupp.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(203, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(9, 8);
            this.panel2.TabIndex = 1;
            // 
            // lbName
            // 
            this.lbName.AllowUserToAddRows = false;
            this.lbName.AllowUserToDeleteRows = false;
            this.lbName.AllowUserToOrderColumns = true;
            this.lbName.AllowUserToResizeRows = false;
            this.lbName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lbName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Inx,
            this.MacName});
            this.lbName.Location = new System.Drawing.Point(16, 76);
            this.lbName.MultiSelect = false;
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(360, 509);
            this.lbName.TabIndex = 4;
            this.lbName.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lbName_CellDoubleClick);
            // 
            // Inx
            // 
            this.Inx.HeaderText = "Macro";
            this.Inx.Name = "Inx";
            this.Inx.ReadOnly = true;
            this.Inx.Width = 48;
            // 
            // MacName
            // 
            this.MacName.HeaderText = "Name";
            this.MacName.Name = "MacName";
            this.MacName.Width = 260;
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnGo.Location = new System.Drawing.Point(33, 28);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(121, 31);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Show as page";
            this.toolTip1.SetToolTip(this.btnGo, "Pops up form using Edge or Chrome");
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnAdd1New
            // 
            this.btnAdd1New.Location = new System.Drawing.Point(328, 77);
            this.btnAdd1New.Name = "btnAdd1New";
            this.btnAdd1New.Size = new System.Drawing.Size(91, 23);
            this.btnAdd1New.TabIndex = 16;
            this.btnAdd1New.Text = "Add newline";
            this.toolTip1.SetToolTip(this.btnAdd1New, "inserts <br>");
            this.btnAdd1New.UseVisualStyleBackColor = true;
            this.btnAdd1New.Click += new System.EventHandler(this.btnAdd1New_Click);
            // 
            // btnAdd2New
            // 
            this.btnAdd2New.Location = new System.Drawing.Point(443, 77);
            this.btnAdd2New.Name = "btnAdd2New";
            this.btnAdd2New.Size = new System.Drawing.Size(108, 23);
            this.btnAdd2New.TabIndex = 18;
            this.btnAdd2New.Text = "Add 2 newlines";
            this.toolTip1.SetToolTip(this.btnAdd2New, "inserts <br>");
            this.btnAdd2New.UseVisualStyleBackColor = true;
            this.btnAdd2New.Click += new System.EventHandler(this.btnAdd2New_Click);
            // 
            // tbBody
            // 
            this.tbBody.Location = new System.Drawing.Point(289, 65);
            this.tbBody.Multiline = true;
            this.tbBody.Name = "tbBody";
            this.tbBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbBody.Size = new System.Drawing.Size(397, 399);
            this.tbBody.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.utilsToolStripMenuItem,
            this.testSignatureToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1151, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readHTMLToolStripMenuItem,
            this.loadFromXMLToolStripMenuItem,
            this.saveToXMLToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // readHTMLToolStripMenuItem
            // 
            this.readHTMLToolStripMenuItem.Name = "readHTMLToolStripMenuItem";
            this.readHTMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.readHTMLToolStripMenuItem.Text = "Read HTML";
            this.readHTMLToolStripMenuItem.Click += new System.EventHandler(this.readHTMLToolStripMenuItem_Click);
            // 
            // loadFromXMLToolStripMenuItem
            // 
            this.loadFromXMLToolStripMenuItem.Name = "loadFromXMLToolStripMenuItem";
            this.loadFromXMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadFromXMLToolStripMenuItem.Text = "Load from text";
            this.loadFromXMLToolStripMenuItem.Click += new System.EventHandler(this.loadFromXMLToolStripMenuItem_Click);
            // 
            // saveToXMLToolStripMenuItem
            // 
            this.saveToXMLToolStripMenuItem.Name = "saveToXMLToolStripMenuItem";
            this.saveToXMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToXMLToolStripMenuItem.Text = "Save to text";
            this.saveToXMLToolStripMenuItem.Click += new System.EventHandler(this.saveToXMLToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // utilsToolStripMenuItem
            // 
            this.utilsToolStripMenuItem.Name = "utilsToolStripMenuItem";
            this.utilsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.utilsToolStripMenuItem.Text = "Utils";
            this.utilsToolStripMenuItem.Click += new System.EventHandler(this.utilsToolStripMenuItem_Click);
            // 
            // testSignatureToolStripMenuItem
            // 
            this.testSignatureToolStripMenuItem.Name = "testSignatureToolStripMenuItem";
            this.testSignatureToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.testSignatureToolStripMenuItem.Text = "Test Signature";
            this.testSignatureToolStripMenuItem.Click += new System.EventHandler(this.testSignatureToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.gbSupp);
            this.groupBox1.Controls.Add(this.btnToNotepad);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCopyFrom);
            this.groupBox1.Controls.Add(this.btnCopyTo);
            this.groupBox1.Controls.Add(this.btnClearEM);
            this.groupBox1.Controls.Add(this.tbBody);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(434, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 630);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Macro";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnAdd2New);
            this.groupBox4.Controls.Add(this.btnAddURL);
            this.groupBox4.Controls.Add(this.btnAdd1New);
            this.groupBox4.Controls.Add(this.tbImgUrl);
            this.groupBox4.Controls.Add(this.btnAddImg);
            this.groupBox4.Location = new System.Drawing.Point(54, 485);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(619, 119);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add image, url or newlines";
            // 
            // btnAddURL
            // 
            this.btnAddURL.Location = new System.Drawing.Point(30, 77);
            this.btnAddURL.Name = "btnAddURL";
            this.btnAddURL.Size = new System.Drawing.Size(91, 23);
            this.btnAddURL.TabIndex = 17;
            this.btnAddURL.Text = "Paste URL";
            this.btnAddURL.UseVisualStyleBackColor = true;
            this.btnAddURL.Click += new System.EventHandler(this.btnAddURL_Click);
            // 
            // tbImgUrl
            // 
            this.tbImgUrl.Location = new System.Drawing.Point(144, 37);
            this.tbImgUrl.Name = "tbImgUrl";
            this.tbImgUrl.Size = new System.Drawing.Size(452, 22);
            this.tbImgUrl.TabIndex = 15;
            this.tbImgUrl.Text = "enter URL here then click add or paste";
            // 
            // btnAddImg
            // 
            this.btnAddImg.Location = new System.Drawing.Point(30, 36);
            this.btnAddImg.Name = "btnAddImg";
            this.btnAddImg.Size = new System.Drawing.Size(91, 23);
            this.btnAddImg.TabIndex = 14;
            this.btnAddImg.Text = "Add image";
            this.btnAddImg.UseVisualStyleBackColor = true;
            this.btnAddImg.Click += new System.EventHandler(this.btnAddImg_Click);
            // 
            // gbSupp
            // 
            this.gbSupp.Controls.Add(this.groupBox3);
            this.gbSupp.Controls.Add(this.tbMacName);
            this.gbSupp.Controls.Add(this.btnSaveM);
            this.gbSupp.Controls.Add(this.btnDelM);
            this.gbSupp.Controls.Add(this.btnAddM);
            this.gbSupp.Location = new System.Drawing.Point(15, 76);
            this.gbSupp.Name = "gbSupp";
            this.gbSupp.Size = new System.Drawing.Size(250, 198);
            this.gbSupp.TabIndex = 13;
            this.gbSupp.TabStop = false;
            this.gbSupp.Text = "Supplemental Table";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbNumMac);
            this.groupBox3.Location = new System.Drawing.Point(18, 64);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(104, 72);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Count";
            // 
            // tbNumMac
            // 
            this.tbNumMac.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbNumMac.Location = new System.Drawing.Point(21, 30);
            this.tbNumMac.Name = "tbNumMac";
            this.tbNumMac.ReadOnly = true;
            this.tbNumMac.Size = new System.Drawing.Size(54, 22);
            this.tbNumMac.TabIndex = 5;
            // 
            // tbMacName
            // 
            this.tbMacName.Location = new System.Drawing.Point(18, 21);
            this.tbMacName.Name = "tbMacName";
            this.tbMacName.Size = new System.Drawing.Size(190, 22);
            this.tbMacName.TabIndex = 3;
            // 
            // btnSaveM
            // 
            this.btnSaveM.Location = new System.Drawing.Point(137, 157);
            this.btnSaveM.Name = "btnSaveM";
            this.btnSaveM.Size = new System.Drawing.Size(91, 23);
            this.btnSaveM.TabIndex = 2;
            this.btnSaveM.Text = "Save Macro";
            this.btnSaveM.UseVisualStyleBackColor = true;
            this.btnSaveM.Click += new System.EventHandler(this.btnSaveM_Click);
            // 
            // btnDelM
            // 
            this.btnDelM.Location = new System.Drawing.Point(137, 128);
            this.btnDelM.Name = "btnDelM";
            this.btnDelM.Size = new System.Drawing.Size(91, 23);
            this.btnDelM.TabIndex = 1;
            this.btnDelM.Text = "Delete";
            this.btnDelM.UseVisualStyleBackColor = true;
            this.btnDelM.Click += new System.EventHandler(this.btnDelM_Click);
            // 
            // btnAddM
            // 
            this.btnAddM.Location = new System.Drawing.Point(137, 99);
            this.btnAddM.Name = "btnAddM";
            this.btnAddM.Size = new System.Drawing.Size(91, 23);
            this.btnAddM.TabIndex = 0;
            this.btnAddM.Text = "Add Macro";
            this.btnAddM.UseVisualStyleBackColor = true;
            this.btnAddM.Click += new System.EventHandler(this.btnAddM_Click);
            // 
            // btnToNotepad
            // 
            this.btnToNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToNotepad.Location = new System.Drawing.Point(112, 435);
            this.btnToNotepad.Name = "btnToNotepad";
            this.btnToNotepad.Size = new System.Drawing.Size(161, 29);
            this.btnToNotepad.TabIndex = 12;
            this.btnToNotepad.Text = "Copy to notepad";
            this.btnToNotepad.UseVisualStyleBackColor = true;
            this.btnToNotepad.Click += new System.EventHandler(this.btnToNotepad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(286, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "You may enter HTML below and click to see it in a web page";
            // 
            // btnCopyFrom
            // 
            this.btnCopyFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyFrom.Location = new System.Drawing.Point(112, 392);
            this.btnCopyFrom.Name = "btnCopyFrom";
            this.btnCopyFrom.Size = new System.Drawing.Size(161, 27);
            this.btnCopyFrom.TabIndex = 10;
            this.btnCopyFrom.Text = "Copy from clipboard";
            this.btnCopyFrom.UseVisualStyleBackColor = true;
            this.btnCopyFrom.Click += new System.EventHandler(this.btnCopyFrom_Click);
            // 
            // btnCopyTo
            // 
            this.btnCopyTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyTo.Location = new System.Drawing.Point(112, 343);
            this.btnCopyTo.Name = "btnCopyTo";
            this.btnCopyTo.Size = new System.Drawing.Size(161, 29);
            this.btnCopyTo.TabIndex = 9;
            this.btnCopyTo.Text = "Copy to clipboard";
            this.btnCopyTo.UseVisualStyleBackColor = true;
            this.btnCopyTo.Click += new System.EventHandler(this.btnCopyTo_Click);
            // 
            // btnClearEM
            // 
            this.btnClearEM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearEM.Location = new System.Drawing.Point(198, 295);
            this.btnClearEM.Name = "btnClearEM";
            this.btnClearEM.Size = new System.Drawing.Size(75, 28);
            this.btnClearEM.TabIndex = 8;
            this.btnClearEM.Text = "Clear";
            this.btnClearEM.UseVisualStyleBackColor = true;
            this.btnClearEM.Click += new System.EventHandler(this.btnClearEM_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbLaunchPage);
            this.groupBox2.Controls.Add(this.lbName);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 630);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Double click any row to transfer to editor";
            // 
            // cbLaunchPage
            // 
            this.cbLaunchPage.AutoSize = true;
            this.cbLaunchPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLaunchPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbLaunchPage.Location = new System.Drawing.Point(63, 28);
            this.cbLaunchPage.Name = "cbLaunchPage";
            this.cbLaunchPage.Size = new System.Drawing.Size(122, 24);
            this.cbLaunchPage.TabIndex = 5;
            this.cbLaunchPage.Text = "Launch Page";
            this.cbLaunchPage.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 685);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "main";
            this.Text = " HP Macro Editor";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lbName)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbSupp.ResumeLayout(false);
            this.gbSupp.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbBody;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DataGridView lbName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearEM;
        private System.Windows.Forms.Button btnCopyFrom;
        private System.Windows.Forms.Button btnCopyTo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inx;
        private System.Windows.Forms.DataGridViewTextBoxColumn MacName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem testSignatureToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbLaunchPage;
        private System.Windows.Forms.Button btnToNotepad;
        private System.Windows.Forms.ToolStripMenuItem readHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToXMLToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbSupp;
        private System.Windows.Forms.Button btnAddM;
        private System.Windows.Forms.Button btnSaveM;
        private System.Windows.Forms.Button btnDelM;
        private System.Windows.Forms.TextBox tbMacName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbNumMac;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAdd1New;
        private System.Windows.Forms.TextBox tbImgUrl;
        private System.Windows.Forms.Button btnAddURL;
        private System.Windows.Forms.Button btnAddImg;
        private System.Windows.Forms.Button btnAdd2New;
    }
}

