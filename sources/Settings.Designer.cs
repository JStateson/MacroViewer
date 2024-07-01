namespace MacroViewer
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbFirefox = new System.Windows.Forms.RadioButton();
            this.rbChrome = new System.Windows.Forms.RadioButton();
            this.rbEdge = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSpecialWord = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnShowURL = new System.Windows.Forms.Button();
            this.btnDelURL = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTestPP = new System.Windows.Forms.Button();
            this.tbPP = new System.Windows.Forms.TextBox();
            this.tbLongAllowed = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbMSuffix = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbSaveLoc = new System.Windows.Forms.Label();
            this.cbSaveUNK = new System.Windows.Forms.CheckBox();
            this.tbURLcnt = new System.Windows.Forms.TextBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbRepeatSearch = new System.Windows.Forms.CheckBox();
            this.cbDisableVPaste = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbFirefox);
            this.groupBox1.Controls.Add(this.rbChrome);
            this.groupBox1.Controls.Add(this.rbEdge);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Browser to use";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Browsers \r\nuse the selection above.";
            // 
            // rbFirefox
            // 
            this.rbFirefox.AutoSize = true;
            this.rbFirefox.Location = new System.Drawing.Point(26, 94);
            this.rbFirefox.Name = "rbFirefox";
            this.rbFirefox.Size = new System.Drawing.Size(56, 17);
            this.rbFirefox.TabIndex = 2;
            this.rbFirefox.Text = "Firefox";
            this.rbFirefox.UseVisualStyleBackColor = true;
            // 
            // rbChrome
            // 
            this.rbChrome.AutoSize = true;
            this.rbChrome.Checked = true;
            this.rbChrome.Location = new System.Drawing.Point(26, 62);
            this.rbChrome.Name = "rbChrome";
            this.rbChrome.Size = new System.Drawing.Size(61, 17);
            this.rbChrome.TabIndex = 1;
            this.rbChrome.TabStop = true;
            this.rbChrome.Text = "Chrome";
            this.rbChrome.UseVisualStyleBackColor = true;
            // 
            // rbEdge
            // 
            this.rbEdge.AutoSize = true;
            this.rbEdge.Location = new System.Drawing.Point(26, 33);
            this.rbEdge.Name = "rbEdge";
            this.rbEdge.Size = new System.Drawing.Size(96, 17);
            this.rbEdge.TabIndex = 0;
            this.rbEdge.Text = "Microsoft Edge";
            this.rbEdge.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tbEmail);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbSpecialWord);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbUserID);
            this.groupBox2.Location = new System.Drawing.Point(12, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 422);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User Info";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(16, 361);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "Email";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(74, 358);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(158, 20);
            this.tbEmail.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Info;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(222, 52);
            this.label7.TabIndex = 7;
            this.label7.Text = "The Special Word can be anything you want.\r\nThe main mage has a button that copie" +
    "s the\r\nSpecial Word into the Windows Clipboard\r\nfor any use that you want.  Be s" +
    "ure to \"Apply\"";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(16, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Special Word";
            // 
            // tbSpecialWord
            // 
            this.tbSpecialWord.Location = new System.Drawing.Point(124, 249);
            this.tbSpecialWord.Name = "tbSpecialWord";
            this.tbSpecialWord.Size = new System.Drawing.Size(108, 20);
            this.tbSpecialWord.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(16, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Your HP user ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 91);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // tbUserID
            // 
            this.tbUserID.Location = new System.Drawing.Point(132, 38);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(100, 20);
            this.tbUserID.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.Location = new System.Drawing.Point(207, 45);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Apply and exit";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(207, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShowURL
            // 
            this.btnShowURL.Enabled = false;
            this.btnShowURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowURL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnShowURL.Location = new System.Drawing.Point(247, 19);
            this.btnShowURL.Name = "btnShowURL";
            this.btnShowURL.Size = new System.Drawing.Size(135, 23);
            this.btnShowURL.TabIndex = 1;
            this.btnShowURL.Text = "Click to see them";
            this.toolTip1.SetToolTip(this.btnShowURL, "This will replace any existing\r\nsupplemental signature");
            this.btnShowURL.UseVisualStyleBackColor = true;
            this.btnShowURL.Click += new System.EventHandler(this.btnShowURL_Click);
            // 
            // btnDelURL
            // 
            this.btnDelURL.Enabled = false;
            this.btnDelURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelURL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnDelURL.Location = new System.Drawing.Point(247, 56);
            this.btnDelURL.Name = "btnDelURL";
            this.btnDelURL.Size = new System.Drawing.Size(135, 23);
            this.btnDelURL.TabIndex = 4;
            this.btnDelURL.Text = "Click to delete them";
            this.toolTip1.SetToolTip(this.btnDelURL, "This will replace any existing\r\nsupplemental signature");
            this.btnDelURL.UseVisualStyleBackColor = true;
            this.btnDelURL.Click += new System.EventHandler(this.btnDelURL_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTestPP);
            this.groupBox4.Controls.Add(this.tbPP);
            this.groupBox4.Location = new System.Drawing.Point(653, 45);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(362, 129);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Printer Prefix";
            this.toolTip1.SetToolTip(this.groupBox4, "Is put in first line of\r\neach printer macro");
            // 
            // btnTestPP
            // 
            this.btnTestPP.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTestPP.Location = new System.Drawing.Point(15, 53);
            this.btnTestPP.Name = "btnTestPP";
            this.btnTestPP.Size = new System.Drawing.Size(51, 23);
            this.btnTestPP.TabIndex = 4;
            this.btnTestPP.Text = "Test";
            this.btnTestPP.UseVisualStyleBackColor = true;
            this.btnTestPP.Click += new System.EventHandler(this.btnTestPP_Click);
            // 
            // tbPP
            // 
            this.tbPP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbPP.Location = new System.Drawing.Point(89, 23);
            this.tbPP.Multiline = true;
            this.tbPP.Name = "tbPP";
            this.tbPP.Size = new System.Drawing.Size(244, 88);
            this.tbPP.TabIndex = 1;
            this.tbPP.Text = "Click <a href=\"https://support.hp.com/us-en/document/ish_1716406-1413451-16?openC" +
    "LC=true\" target=\"_blank\">here for network setup</a> using HP Smart but in a diff" +
    "erent language";
            // 
            // tbLongAllowed
            // 
            this.tbLongAllowed.BackColor = System.Drawing.SystemColors.Window;
            this.tbLongAllowed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLongAllowed.Location = new System.Drawing.Point(170, 61);
            this.tbLongAllowed.Name = "tbLongAllowed";
            this.tbLongAllowed.Size = new System.Drawing.Size(48, 20);
            this.tbLongAllowed.TabIndex = 5;
            this.tbLongAllowed.Text = "256";
            this.toolTip1.SetToolTip(this.tbLongAllowed, "Click APPLY to save");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(19, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "Longest Allowed URL";
            this.toolTip1.SetToolTip(this.label10, "After scrubbing, any url with length\r\nexceeding this number is recorded\r\nas an un" +
        "scrubbed url.");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.tbMSuffix);
            this.groupBox3.Location = new System.Drawing.Point(653, 232);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 129);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Macro Suffix";
            this.toolTip1.SetToolTip(this.groupBox3, "Is put in first line of\r\neach printer macro");
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button1.Location = new System.Drawing.Point(15, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tbMSuffix
            // 
            this.tbMSuffix.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbMSuffix.Location = new System.Drawing.Point(89, 19);
            this.tbMSuffix.Multiline = true;
            this.tbMSuffix.Name = "tbMSuffix";
            this.tbMSuffix.Size = new System.Drawing.Size(244, 88);
            this.tbMSuffix.TabIndex = 1;
            this.tbMSuffix.Text = "Please let me know if this works";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.tbLongAllowed);
            this.groupBox5.Controls.Add(this.btnDelURL);
            this.groupBox5.Controls.Add(this.lbSaveLoc);
            this.groupBox5.Controls.Add(this.cbSaveUNK);
            this.groupBox5.Controls.Add(this.btnShowURL);
            this.groupBox5.Controls.Add(this.tbURLcnt);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(603, 412);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(412, 129);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Unscrubbed URL list";
            // 
            // lbSaveLoc
            // 
            this.lbSaveLoc.AutoSize = true;
            this.lbSaveLoc.BackColor = System.Drawing.SystemColors.Window;
            this.lbSaveLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSaveLoc.Location = new System.Drawing.Point(19, 99);
            this.lbSaveLoc.Name = "lbSaveLoc";
            this.lbSaveLoc.Size = new System.Drawing.Size(80, 15);
            this.lbSaveLoc.TabIndex = 3;
            this.lbSaveLoc.Text = "UrlDebug.txt";
            // 
            // cbSaveUNK
            // 
            this.cbSaveUNK.AutoSize = true;
            this.cbSaveUNK.Location = new System.Drawing.Point(19, 31);
            this.cbSaveUNK.Name = "cbSaveUNK";
            this.cbSaveUNK.Size = new System.Drawing.Size(96, 17);
            this.cbSaveUNK.TabIndex = 2;
            this.cbSaveUNK.Text = " Save URLS";
            this.cbSaveUNK.UseVisualStyleBackColor = true;
            // 
            // tbURLcnt
            // 
            this.tbURLcnt.BackColor = System.Drawing.SystemColors.Window;
            this.tbURLcnt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbURLcnt.Location = new System.Drawing.Point(247, 96);
            this.tbURLcnt.Name = "tbURLcnt";
            this.tbURLcnt.ReadOnly = true;
            this.tbURLcnt.Size = new System.Drawing.Size(135, 20);
            this.tbURLcnt.TabIndex = 0;
            this.tbURLcnt.Text = "test";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Info;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(340, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 65);
            this.label8.TabIndex = 8;
            this.label8.Text = "Browser and User Info\r\nneed to be saved.  Be \r\nsure to click Apply.  No\r\nother it" +
    "ems in this dialog\r\nbox need to be saved";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(204, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 52);
            this.label3.TabIndex = 11;
            this.label3.Text = "You need to prevent Edge from switching\r\nhttp to https as many HP documents fail." +
    "\r\nType \'edge://flags/#edge-automatic-https \'\r\ninto the Edge browser and enable t" +
    "he option\r\n";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbDisableVPaste);
            this.groupBox6.Controls.Add(this.cbRepeatSearch);
            this.groupBox6.Location = new System.Drawing.Point(320, 243);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(249, 422);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Edit and Search Overrides";
            this.toolTip1.SetToolTip(this.groupBox6, "Display the previous search settings\r\nand values when bring up the word\r\nsearch. " +
        " Make any change to disable.");
            // 
            // cbRepeatSearch
            // 
            this.cbRepeatSearch.AutoSize = true;
            this.cbRepeatSearch.Location = new System.Drawing.Point(23, 38);
            this.cbRepeatSearch.Name = "cbRepeatSearch";
            this.cbRepeatSearch.Size = new System.Drawing.Size(122, 17);
            this.cbRepeatSearch.TabIndex = 0;
            this.cbRepeatSearch.Text = "Repeat word search";
            this.cbRepeatSearch.UseVisualStyleBackColor = true;
            // 
            // cbDisableVPaste
            // 
            this.cbDisableVPaste.AutoSize = true;
            this.cbDisableVPaste.Location = new System.Drawing.Point(23, 79);
            this.cbDisableVPaste.Name = "cbDisableVPaste";
            this.cbDisableVPaste.Size = new System.Drawing.Size(164, 17);
            this.cbDisableVPaste.TabIndex = 1;
            this.cbDisableVPaste.Text = "Disable CTRL-V HTML paste";
            this.toolTip1.SetToolTip(this.cbDisableVPaste, "If user highlights a phrase in the\r\nmain edit box and press CTRL-V\r\nit will be ig" +
        "nored.  HTML must be\r\ncreated using a dialog box brought\r\nup by clicking a butto" +
        "n.");
            this.cbDisableVPaste.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 694);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFirefox;
        private System.Windows.Forms.RadioButton rbChrome;
        private System.Windows.Forms.RadioButton rbEdge;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserID;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnShowURL;
        private System.Windows.Forms.TextBox tbURLcnt;
        private System.Windows.Forms.Label lbSaveLoc;
        private System.Windows.Forms.CheckBox cbSaveUNK;
        private System.Windows.Forms.Button btnDelURL;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSpecialWord;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbPP;
        private System.Windows.Forms.Button btnTestPP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbLongAllowed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbMSuffix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cbDisableVPaste;
        private System.Windows.Forms.CheckBox cbRepeatSearch;
    }
}