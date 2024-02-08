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
            this.tbBody = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSignatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopyFrom = new System.Windows.Forms.Button();
            this.btnCopyTo = new System.Windows.Forms.Button();
            this.btnClearEM = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbLaunchPage = new System.Windows.Forms.CheckBox();
            this.btnToNotepad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lbName)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.lbName.Size = new System.Drawing.Size(360, 388);
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
            this.btnGo.Location = new System.Drawing.Point(57, 62);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(121, 31);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Show as page";
            this.toolTip1.SetToolTip(this.btnGo, "Pops up form using Edge or Chrome");
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tbBody
            // 
            this.tbBody.Location = new System.Drawing.Point(210, 62);
            this.tbBody.Multiline = true;
            this.tbBody.Name = "tbBody";
            this.tbBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbBody.Size = new System.Drawing.Size(397, 409);
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
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
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
            this.groupBox1.Controls.Add(this.btnToNotepad);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCopyFrom);
            this.groupBox1.Controls.Add(this.btnCopyTo);
            this.groupBox1.Controls.Add(this.btnClearEM);
            this.groupBox1.Controls.Add(this.tbBody);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(477, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 496);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Macro";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(207, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "You may enter HTML below and click to see it in a web page";
            // 
            // btnCopyFrom
            // 
            this.btnCopyFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyFrom.Location = new System.Drawing.Point(17, 294);
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
            this.btnCopyTo.Location = new System.Drawing.Point(17, 234);
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
            this.btnClearEM.Location = new System.Drawing.Point(103, 170);
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
            this.groupBox2.Size = new System.Drawing.Size(418, 496);
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
            // btnToNotepad
            // 
            this.btnToNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToNotepad.Location = new System.Drawing.Point(17, 387);
            this.btnToNotepad.Name = "btnToNotepad";
            this.btnToNotepad.Size = new System.Drawing.Size(161, 29);
            this.btnToNotepad.TabIndex = 12;
            this.btnToNotepad.Text = "Copy to notepad";
            this.btnToNotepad.UseVisualStyleBackColor = true;
            this.btnToNotepad.Click += new System.EventHandler(this.btnToNotepad_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 565);
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
    }
}

