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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbKeysFound = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSearched = new System.Windows.Forms.DataGridView();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearched)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dgvSearched);
            this.groupBox1.Controls.Add(this.tbKeywords);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(657, 539);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyword match";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbKeysFound);
            this.groupBox2.Location = new System.Drawing.Point(344, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keywords found";
            // 
            // tbKeysFound
            // 
            this.tbKeysFound.Location = new System.Drawing.Point(58, 33);
            this.tbKeysFound.Name = "tbKeysFound";
            this.tbKeysFound.ReadOnly = true;
            this.tbKeysFound.Size = new System.Drawing.Size(167, 22);
            this.tbKeysFound.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSearch.Location = new System.Drawing.Point(148, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(33, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 16);
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
            this.dgvSearched.Location = new System.Drawing.Point(27, 141);
            this.dgvSearched.MultiSelect = false;
            this.dgvSearched.Name = "dgvSearched";
            this.dgvSearched.ReadOnly = true;
            this.dgvSearched.RowHeadersVisible = false;
            this.dgvSearched.Size = new System.Drawing.Size(599, 381);
            this.dgvSearched.TabIndex = 3;
            this.dgvSearched.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_CellDoubleClick);
            this.dgvSearched.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearched_RowEnter);
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(27, 70);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(262, 22);
            this.tbKeywords.TabIndex = 2;
            this.tbKeywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKeywords_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(34, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Keywords";
            // 
            // WordSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 563);
            this.Controls.Add(this.groupBox1);
            this.Name = "WordSearch";
            this.Text = "WordSearch";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox tbKeysFound;
        private System.Windows.Forms.Button btnSearch;
    }
}