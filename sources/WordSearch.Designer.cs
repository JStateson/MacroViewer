namespace MacroViewer
{
    partial class WordSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordSearch));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbHPKB = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnHPKB = new System.Windows.Forms.Button();
            this.btnExitToMac = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbIgnCase = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.rbAnyMatch = new System.Windows.Forms.RadioButton();
            this.rbExactMatch = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbNumMatches = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbKeyFound = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSearched = new System.Windows.Forms.DataGridView();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearched)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbHPKB);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dgvSearched);
            this.groupBox1.Controls.Add(this.tbKeywords);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 651);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyword match";
            // 
            // cbHPKB
            // 
            this.cbHPKB.AutoSize = true;
            this.cbHPKB.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbHPKB.Location = new System.Drawing.Point(148, 41);
            this.cbHPKB.Name = "cbHPKB";
            this.cbHPKB.Size = new System.Drawing.Size(199, 24);
            this.cbHPKB.TabIndex = 8;
            this.cbHPKB.Text = "Include HP KB in search";
            this.cbHPKB.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnHPKB);
            this.groupBox3.Controls.Add(this.btnExitToMac);
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Controls.Add(this.cbIgnCase);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.rbAnyMatch);
            this.groupBox3.Controls.Add(this.rbExactMatch);
            this.groupBox3.Location = new System.Drawing.Point(497, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 259);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Keyword parameters";
            // 
            // btnHPKB
            // 
            this.btnHPKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHPKB.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnHPKB.Location = new System.Drawing.Point(200, 127);
            this.btnHPKB.Name = "btnHPKB";
            this.btnHPKB.Size = new System.Drawing.Size(140, 34);
            this.btnHPKB.TabIndex = 10;
            this.btnHPKB.Text = "Search HP KB";
            this.btnHPKB.UseVisualStyleBackColor = true;
            this.btnHPKB.Click += new System.EventHandler(this.btnHPKB_Click);
            // 
            // btnExitToMac
            // 
            this.btnExitToMac.ForeColor = System.Drawing.Color.Red;
            this.btnExitToMac.Location = new System.Drawing.Point(30, 212);
            this.btnExitToMac.Name = "btnExitToMac";
            this.btnExitToMac.Size = new System.Drawing.Size(192, 31);
            this.btnExitToMac.TabIndex = 9;
            this.btnExitToMac.Text = "EXIT and show macro";
            this.toolTip1.SetToolTip(this.btnExitToMac, "Bring up the selected macro after the form closes\r\nThis does not work if you have" +
        " unfinised edits");
            this.btnExitToMac.UseVisualStyleBackColor = true;
            this.btnExitToMac.Click += new System.EventHandler(this.btnExitToMac_Click);
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(248, 212);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 31);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbIgnCase
            // 
            this.cbIgnCase.AutoSize = true;
            this.cbIgnCase.Checked = true;
            this.cbIgnCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnCase.Location = new System.Drawing.Point(47, 129);
            this.cbIgnCase.Name = "cbIgnCase";
            this.cbIgnCase.Size = new System.Drawing.Size(112, 24);
            this.cbIgnCase.TabIndex = 2;
            this.cbIgnCase.Text = "Ignore case";
            this.cbIgnCase.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSearch.Location = new System.Drawing.Point(200, 52);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 34);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rbAnyMatch
            // 
            this.rbAnyMatch.AutoSize = true;
            this.rbAnyMatch.Location = new System.Drawing.Point(47, 81);
            this.rbAnyMatch.Name = "rbAnyMatch";
            this.rbAnyMatch.Size = new System.Drawing.Size(102, 24);
            this.rbAnyMatch.TabIndex = 1;
            this.rbAnyMatch.Text = "Any match";
            this.rbAnyMatch.UseVisualStyleBackColor = true;
            // 
            // rbExactMatch
            // 
            this.rbExactMatch.AutoSize = true;
            this.rbExactMatch.Checked = true;
            this.rbExactMatch.Location = new System.Drawing.Point(47, 35);
            this.rbExactMatch.Name = "rbExactMatch";
            this.rbExactMatch.Size = new System.Drawing.Size(105, 24);
            this.rbExactMatch.TabIndex = 0;
            this.rbExactMatch.TabStop = true;
            this.rbExactMatch.Text = "Exact word";
            this.rbExactMatch.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbNumMatches);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbKeyFound);
            this.groupBox2.Location = new System.Drawing.Point(497, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 330);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keywords found";
            // 
            // tbNumMatches
            // 
            this.tbNumMatches.Location = new System.Drawing.Point(222, 43);
            this.tbNumMatches.Name = "tbNumMatches";
            this.tbNumMatches.ReadOnly = true;
            this.tbNumMatches.Size = new System.Drawing.Size(60, 26);
            this.tbNumMatches.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total Matches";
            // 
            // lbKeyFound
            // 
            this.lbKeyFound.FormattingEnabled = true;
            this.lbKeyFound.HorizontalScrollbar = true;
            this.lbKeyFound.ItemHeight = 20;
            this.lbKeyFound.Location = new System.Drawing.Point(30, 109);
            this.lbKeyFound.Name = "lbKeyFound";
            this.lbKeyFound.Size = new System.Drawing.Size(291, 204);
            this.lbKeyFound.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(127, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Double click row to view macro";
            // 
            // dgvSearched
            // 
            this.dgvSearched.AllowUserToAddRows = false;
            this.dgvSearched.AllowUserToDeleteRows = false;
            this.dgvSearched.AllowUserToResizeColumns = false;
            this.dgvSearched.AllowUserToResizeRows = false;
            this.dgvSearched.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearched.Location = new System.Drawing.Point(28, 258);
            this.dgvSearched.MultiSelect = false;
            this.dgvSearched.Name = "dgvSearched";
            this.dgvSearched.ReadOnly = true;
            this.dgvSearched.RowHeadersVisible = false;
            this.dgvSearched.Size = new System.Drawing.Size(431, 384);
            this.dgvSearched.TabIndex = 3;
            this.dgvSearched.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_RowEnter);
            this.dgvSearched.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_CellDoubleClick);
            this.dgvSearched.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_RowEnter);
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(28, 152);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(431, 26);
            this.tbKeywords.TabIndex = 2;
            this.tbKeywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKeywords_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(24, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Keywords (press enter to search)";
            // 
            // WordSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 675);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WordSearch";
            this.Text = "WordSearch";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.WordSearch_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WordSearch_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearched)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSearched;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbIgnCase;
        private System.Windows.Forms.RadioButton rbAnyMatch;
        private System.Windows.Forms.RadioButton rbExactMatch;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbNumMatches;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbKeyFound;
        private System.Windows.Forms.Button btnExitToMac;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbHPKB;
        private System.Windows.Forms.Button btnHPKB;
    }
}