namespace MacroViewer
{
    partial class utils
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(utils));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCvt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowScratch = new System.Windows.Forms.Button();
            this.btnCopyScratch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClrScratch = new System.Windows.Forms.Button();
            this.tbScratch = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnShowBrowser = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnClip = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.btnClearURL = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbTEXT = new System.Windows.Forms.TextBox();
            this.btnClrHREF = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.showExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpTable = new System.Windows.Forms.GroupBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnApplyTab = new System.Windows.Forms.Button();
            this.tbCols = new System.Windows.Forms.TextBox();
            this.tbRows = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnFillRow = new System.Windows.Forms.Button();
            this.btnFillCol = new System.Windows.Forms.Button();
            this.cbUseDelims = new System.Windows.Forms.CheckBox();
            this.tbDelims = new System.Windows.Forms.TextBox();
            this.btnClearTab = new System.Windows.Forms.Button();
            this.cbPreFill = new System.Windows.Forms.CheckBox();
            this.btnShowTab = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gpTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCvt
            // 
            this.btnCvt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCvt.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCvt.Location = new System.Drawing.Point(137, 125);
            this.btnCvt.Name = "btnCvt";
            this.btnCvt.Size = new System.Drawing.Size(127, 32);
            this.btnCvt.TabIndex = 1;
            this.btnCvt.Text = "GO - create result";
            this.btnCvt.UseVisualStyleBackColor = true;
            this.btnCvt.Click += new System.EventHandler(this.btnCvt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowScratch);
            this.groupBox1.Controls.Add(this.btnCopyScratch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClrScratch);
            this.groupBox1.Controls.Add(this.tbScratch);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnCvt);
            this.groupBox1.Location = new System.Drawing.Point(31, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 544);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Form URL";
            // 
            // btnShowScratch
            // 
            this.btnShowScratch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowScratch.Location = new System.Drawing.Point(29, 419);
            this.btnShowScratch.Name = "btnShowScratch";
            this.btnShowScratch.Size = new System.Drawing.Size(111, 24);
            this.btnShowScratch.TabIndex = 11;
            this.btnShowScratch.Text = "Show in browser";
            this.btnShowScratch.UseVisualStyleBackColor = true;
            this.btnShowScratch.Click += new System.EventHandler(this.btnShowScratch_Click);
            // 
            // btnCopyScratch
            // 
            this.btnCopyScratch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyScratch.Location = new System.Drawing.Point(29, 369);
            this.btnCopyScratch.Name = "btnCopyScratch";
            this.btnCopyScratch.Size = new System.Drawing.Size(111, 24);
            this.btnCopyScratch.TabIndex = 10;
            this.btnCopyScratch.Text = "Copy to Clipboard";
            this.btnCopyScratch.UseVisualStyleBackColor = true;
            this.btnCopyScratch.Click += new System.EventHandler(this.btnCopyScratch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(26, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 65);
            this.label1.TabIndex = 9;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnClrScratch
            // 
            this.btnClrScratch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClrScratch.Location = new System.Drawing.Point(92, 324);
            this.btnClrScratch.Name = "btnClrScratch";
            this.btnClrScratch.Size = new System.Drawing.Size(48, 23);
            this.btnClrScratch.TabIndex = 8;
            this.btnClrScratch.Text = "Clear";
            this.btnClrScratch.UseVisualStyleBackColor = true;
            this.btnClrScratch.Click += new System.EventHandler(this.btnClrScratch_Click);
            // 
            // tbScratch
            // 
            this.tbScratch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScratch.Location = new System.Drawing.Point(161, 301);
            this.tbScratch.Multiline = true;
            this.tbScratch.Name = "tbScratch";
            this.tbScratch.Size = new System.Drawing.Size(554, 222);
            this.tbScratch.TabIndex = 7;
            this.tbScratch.Text = resources.GetString("tbScratch.Text");
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnShowBrowser);
            this.groupBox4.Controls.Add(this.tbResult);
            this.groupBox4.Controls.Add(this.btnClip);
            this.groupBox4.Location = new System.Drawing.Point(137, 187);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(578, 85);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Result";
            // 
            // btnShowBrowser
            // 
            this.btnShowBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowBrowser.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnShowBrowser.Location = new System.Drawing.Point(335, 47);
            this.btnShowBrowser.Name = "btnShowBrowser";
            this.btnShowBrowser.Size = new System.Drawing.Size(135, 24);
            this.btnShowBrowser.TabIndex = 3;
            this.btnShowBrowser.Text = "Show in browser";
            this.btnShowBrowser.UseVisualStyleBackColor = true;
            this.btnShowBrowser.Click += new System.EventHandler(this.btnShowBrowser_Click);
            // 
            // tbResult
            // 
            this.tbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResult.Location = new System.Drawing.Point(24, 19);
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.Size = new System.Drawing.Size(529, 22);
            this.tbResult.TabIndex = 0;
            // 
            // btnClip
            // 
            this.btnClip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClip.Location = new System.Drawing.Point(24, 47);
            this.btnClip.Name = "btnClip";
            this.btnClip.Size = new System.Drawing.Size(135, 24);
            this.btnClip.TabIndex = 2;
            this.btnClip.Text = "Copy to Clipboard";
            this.btnClip.UseVisualStyleBackColor = true;
            this.btnClip.Click += new System.EventHandler(this.btnClip_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbURL);
            this.groupBox3.Controls.Add(this.btnClearURL);
            this.groupBox3.Location = new System.Drawing.Point(289, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(426, 61);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "URL";
            // 
            // tbURL
            // 
            this.tbURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbURL.Location = new System.Drawing.Point(91, 21);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(317, 22);
            this.tbURL.TabIndex = 0;
            // 
            // btnClearURL
            // 
            this.btnClearURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearURL.Location = new System.Drawing.Point(6, 20);
            this.btnClearURL.Name = "btnClearURL";
            this.btnClearURL.Size = new System.Drawing.Size(48, 23);
            this.btnClearURL.TabIndex = 5;
            this.btnClearURL.Text = "Clear";
            this.btnClearURL.UseVisualStyleBackColor = true;
            this.btnClearURL.Click += new System.EventHandler(this.btnClearURL_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbTEXT);
            this.groupBox2.Controls.Add(this.btnClrHREF);
            this.groupBox2.Location = new System.Drawing.Point(289, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 61);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HREF";
            // 
            // tbTEXT
            // 
            this.tbTEXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTEXT.Location = new System.Drawing.Point(91, 19);
            this.tbTEXT.Name = "tbTEXT";
            this.tbTEXT.Size = new System.Drawing.Size(317, 22);
            this.tbTEXT.TabIndex = 0;
            // 
            // btnClrHREF
            // 
            this.btnClrHREF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClrHREF.Location = new System.Drawing.Point(6, 19);
            this.btnClrHREF.Name = "btnClrHREF";
            this.btnClrHREF.Size = new System.Drawing.Size(48, 23);
            this.btnClrHREF.TabIndex = 6;
            this.btnClrHREF.Text = "Clear";
            this.btnClrHREF.UseVisualStyleBackColor = true;
            this.btnClrHREF.Click += new System.EventHandler(this.btnClrHREF_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showExampleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1329, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // showExampleToolStripMenuItem
            // 
            this.showExampleToolStripMenuItem.Name = "showExampleToolStripMenuItem";
            this.showExampleToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.showExampleToolStripMenuItem.Text = "Run Example";
            this.showExampleToolStripMenuItem.Click += new System.EventHandler(this.showExampleToolStripMenuItem_Click);
            // 
            // gpTable
            // 
            this.gpTable.Controls.Add(this.groupBox5);
            this.gpTable.Controls.Add(this.btnTransfer);
            this.gpTable.Controls.Add(this.dgv);
            this.gpTable.Controls.Add(this.btnShow);
            this.gpTable.Controls.Add(this.btnApplyTab);
            this.gpTable.Controls.Add(this.cbPreFill);
            this.gpTable.Controls.Add(this.tbCols);
            this.gpTable.Controls.Add(this.tbRows);
            this.gpTable.Controls.Add(this.label2);
            this.gpTable.Controls.Add(this.label3);
            this.gpTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpTable.Location = new System.Drawing.Point(784, 59);
            this.gpTable.Name = "gpTable";
            this.gpTable.Size = new System.Drawing.Size(509, 544);
            this.gpTable.TabIndex = 9;
            this.gpTable.TabStop = false;
            this.gpTable.Text = "Create a table";
            // 
            // btnTransfer
            // 
            this.btnTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTransfer.Location = new System.Drawing.Point(330, 58);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(100, 22);
            this.btnTransfer.TabIndex = 8;
            this.btnTransfer.Text = "Update Table";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgv.Location = new System.Drawing.Point(23, 247);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(417, 276);
            this.dgv.TabIndex = 7;
            this.dgv.Visible = false;
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Position";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Contents";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 312;
            // 
            // btnShow
            // 
            this.btnShow.Enabled = false;
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnShow.Location = new System.Drawing.Point(172, 58);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(118, 22);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "Show in browser";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnApplyTab
            // 
            this.btnApplyTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyTab.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApplyTab.Location = new System.Drawing.Point(172, 23);
            this.btnApplyTab.Name = "btnApplyTab";
            this.btnApplyTab.Size = new System.Drawing.Size(57, 22);
            this.btnApplyTab.TabIndex = 4;
            this.btnApplyTab.Text = "Apply";
            this.btnApplyTab.UseVisualStyleBackColor = true;
            this.btnApplyTab.Click += new System.EventHandler(this.btnApplyTab_Click);
            // 
            // tbCols
            // 
            this.tbCols.Location = new System.Drawing.Point(85, 58);
            this.tbCols.Name = "tbCols";
            this.tbCols.Size = new System.Drawing.Size(57, 22);
            this.tbCols.TabIndex = 3;
            this.tbCols.Text = "1";
            // 
            // tbRows
            // 
            this.tbRows.Location = new System.Drawing.Point(85, 23);
            this.tbRows.Name = "tbRows";
            this.tbRows.Size = new System.Drawing.Size(57, 22);
            this.tbRows.TabIndex = 2;
            this.tbRows.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Columns";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rows";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnShowTab);
            this.groupBox5.Controls.Add(this.btnClearTab);
            this.groupBox5.Controls.Add(this.tbDelims);
            this.groupBox5.Controls.Add(this.cbUseDelims);
            this.groupBox5.Controls.Add(this.btnFillCol);
            this.groupBox5.Controls.Add(this.btnFillRow);
            this.groupBox5.Location = new System.Drawing.Point(32, 105);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(408, 123);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "How to fill it in (do not use above)";
            // 
            // btnFillRow
            // 
            this.btnFillRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillRow.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFillRow.Location = new System.Drawing.Point(15, 25);
            this.btnFillRow.Name = "btnFillRow";
            this.btnFillRow.Size = new System.Drawing.Size(155, 23);
            this.btnFillRow.TabIndex = 6;
            this.btnFillRow.Text = "Fill ROW from clipboard";
            this.btnFillRow.UseVisualStyleBackColor = true;
            this.btnFillRow.Click += new System.EventHandler(this.btnFillRow_Click);
            // 
            // btnFillCol
            // 
            this.btnFillCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillCol.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFillCol.Location = new System.Drawing.Point(15, 54);
            this.btnFillCol.Name = "btnFillCol";
            this.btnFillCol.Size = new System.Drawing.Size(176, 23);
            this.btnFillCol.TabIndex = 7;
            this.btnFillCol.Text = "Fill COLUMN from clipboard";
            this.btnFillCol.UseVisualStyleBackColor = true;
            this.btnFillCol.Click += new System.EventHandler(this.btnFillCol_Click);
            // 
            // cbUseDelims
            // 
            this.cbUseDelims.AutoSize = true;
            this.cbUseDelims.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUseDelims.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbUseDelims.Location = new System.Drawing.Point(230, 55);
            this.cbUseDelims.Name = "cbUseDelims";
            this.cbUseDelims.Size = new System.Drawing.Size(152, 20);
            this.cbUseDelims.TabIndex = 8;
            this.cbUseDelims.Text = "Split on Delimiters";
            this.cbUseDelims.UseVisualStyleBackColor = true;
            // 
            // tbDelims
            // 
            this.tbDelims.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDelims.Location = new System.Drawing.Point(264, 85);
            this.tbDelims.Name = "tbDelims";
            this.tbDelims.ReadOnly = true;
            this.tbDelims.Size = new System.Drawing.Size(118, 20);
            this.tbDelims.TabIndex = 9;
            this.tbDelims.Text = "Comma, Whitespace";
            // 
            // btnClearTab
            // 
            this.btnClearTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearTab.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClearTab.Location = new System.Drawing.Point(283, 20);
            this.btnClearTab.Name = "btnClearTab";
            this.btnClearTab.Size = new System.Drawing.Size(48, 23);
            this.btnClearTab.TabIndex = 10;
            this.btnClearTab.Text = "Clear";
            this.btnClearTab.UseVisualStyleBackColor = true;
            this.btnClearTab.Click += new System.EventHandler(this.btnClearTab_Click);
            // 
            // cbPreFill
            // 
            this.cbPreFill.AutoSize = true;
            this.cbPreFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPreFill.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbPreFill.Location = new System.Drawing.Point(262, 28);
            this.cbPreFill.Name = "cbPreFill";
            this.cbPreFill.Size = new System.Drawing.Size(223, 20);
            this.cbPreFill.TabIndex = 5;
            this.cbPreFill.Text = "I want to fill it in now (limit 30)";
            this.cbPreFill.UseVisualStyleBackColor = true;
            this.cbPreFill.CheckStateChanged += new System.EventHandler(this.cbPreFill_CheckStateChanged);
            // 
            // btnShowTab
            // 
            this.btnShowTab.Enabled = false;
            this.btnShowTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowTab.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnShowTab.Location = new System.Drawing.Point(15, 85);
            this.btnShowTab.Name = "btnShowTab";
            this.btnShowTab.Size = new System.Drawing.Size(118, 22);
            this.btnShowTab.TabIndex = 11;
            this.btnShowTab.Text = "Show in browser";
            this.btnShowTab.UseVisualStyleBackColor = true;
            this.btnShowTab.Click += new System.EventHandler(this.btnShowTab_Click);
            // 
            // utils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 615);
            this.Controls.Add(this.gpTable);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "utils";
            this.Text = "utils";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gpTable.ResumeLayout(false);
            this.gpTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCvt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbTEXT;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnClip;
        private System.Windows.Forms.Button btnClrHREF;
        private System.Windows.Forms.Button btnClearURL;
        private System.Windows.Forms.TextBox tbScratch;
        private System.Windows.Forms.Button btnClrScratch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showExampleToolStripMenuItem;
        private System.Windows.Forms.Button btnShowBrowser;
        private System.Windows.Forms.GroupBox gpTable;
        private System.Windows.Forms.Button btnApplyTab;
        private System.Windows.Forms.TextBox tbCols;
        private System.Windows.Forms.TextBox tbRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnCopyScratch;
        private System.Windows.Forms.Button btnShowScratch;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnFillRow;
        private System.Windows.Forms.Button btnFillCol;
        private System.Windows.Forms.TextBox tbDelims;
        private System.Windows.Forms.CheckBox cbUseDelims;
        private System.Windows.Forms.Button btnClearTab;
        private System.Windows.Forms.CheckBox cbPreFill;
        private System.Windows.Forms.Button btnShowTab;
    }
}