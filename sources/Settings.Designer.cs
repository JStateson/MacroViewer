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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSpecialWord = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbTB = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbRefOnly = new System.Windows.Forms.CheckBox();
            this.lbMarker = new System.Windows.Forms.Label();
            this.cbListFN = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbAttached = new System.Windows.Forms.Label();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnClearEB = new System.Windows.Forms.Button();
            this.btnCopy2Clip = new System.Windows.Forms.Button();
            this.btnAdd2New = new System.Windows.Forms.Button();
            this.btnAdd1New = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSavEdits = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.clbImages = new System.Windows.Forms.CheckedListBox();
            this.tbEdit = new System.Windows.Forms.TextBox();
            this.btnCpyEdit = new System.Windows.Forms.Button();
            this.btnFont = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnAddSupSig = new System.Windows.Forms.Button();
            this.tbSupSig = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnShowURL = new System.Windows.Forms.Button();
            this.btnDelURL = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTestPP = new System.Windows.Forms.Button();
            this.tbPP = new System.Windows.Forms.TextBox();
            this.tbLongAllowed = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbSaveLoc = new System.Windows.Forms.Label();
            this.cbSaveUNK = new System.Windows.Forms.CheckBox();
            this.tbURLcnt = new System.Windows.Forms.TextBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbTB.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.label2.Size = new System.Drawing.Size(137, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dialog boxes use WebView\r\nwhich is Edge.  Browsers \r\nuse the selection above.";
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
            // gbTB
            // 
            this.gbTB.Controls.Add(this.label9);
            this.gbTB.Controls.Add(this.cbRefOnly);
            this.gbTB.Controls.Add(this.lbMarker);
            this.gbTB.Controls.Add(this.cbListFN);
            this.gbTB.Controls.Add(this.groupBox3);
            this.gbTB.Controls.Add(this.btnRemoveAll);
            this.gbTB.Controls.Add(this.btnClearEB);
            this.gbTB.Controls.Add(this.btnCopy2Clip);
            this.gbTB.Controls.Add(this.btnAdd2New);
            this.gbTB.Controls.Add(this.btnAdd1New);
            this.gbTB.Controls.Add(this.label3);
            this.gbTB.Controls.Add(this.btnSavEdits);
            this.gbTB.Controls.Add(this.btnTest);
            this.gbTB.Controls.Add(this.label4);
            this.gbTB.Controls.Add(this.clbImages);
            this.gbTB.Controls.Add(this.tbEdit);
            this.gbTB.Controls.Add(this.btnCpyEdit);
            this.gbTB.Controls.Add(this.btnFont);
            this.gbTB.Controls.Add(this.btnColor);
            this.gbTB.Controls.Add(this.btnAddSupSig);
            this.gbTB.Controls.Add(this.tbSupSig);
            this.gbTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTB.Location = new System.Drawing.Point(457, 191);
            this.gbTB.Name = "gbTB";
            this.gbTB.Size = new System.Drawing.Size(826, 526);
            this.gbTB.TabIndex = 5;
            this.gbTB.TabStop = false;
            this.gbTB.Text = "Add at end of every macro";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Info;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(589, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(203, 65);
            this.label9.TabIndex = 28;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // cbRefOnly
            // 
            this.cbRefOnly.AutoSize = true;
            this.cbRefOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRefOnly.ForeColor = System.Drawing.Color.Red;
            this.cbRefOnly.Location = new System.Drawing.Point(378, 105);
            this.cbRefOnly.Name = "cbRefOnly";
            this.cbRefOnly.Size = new System.Drawing.Size(156, 20);
            this.cbRefOnly.TabIndex = 27;
            this.cbRefOnly.Text = "Do not use  marker";
            this.toolTip1.SetToolTip(this.cbRefOnly, "If you are just adding a\r\nreference at the bottom\r\nof each macro then no\r\nneed to" +
        " use markers");
            this.cbRefOnly.UseVisualStyleBackColor = true;
            this.cbRefOnly.CheckStateChanged += new System.EventHandler(this.cbRefOnly_CheckStateChanged);
            // 
            // lbMarker
            // 
            this.lbMarker.AutoSize = true;
            this.lbMarker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMarker.ForeColor = System.Drawing.Color.Red;
            this.lbMarker.Location = new System.Drawing.Point(375, 72);
            this.lbMarker.Name = "lbMarker";
            this.lbMarker.Size = new System.Drawing.Size(134, 16);
            this.lbMarker.TabIndex = 26;
            this.lbMarker.Text = "Markers [pre] [suf]";
            this.toolTip1.SetToolTip(this.lbMarker, "These are used to mark the\r\nlocation of the supplemental\r\nsignature. If not used " +
        "then those\r\nsignatures must be manually\r\nremoved if unwanted.");
            // 
            // cbListFN
            // 
            this.cbListFN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbListFN.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbListFN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbListFN.FormattingEnabled = true;
            this.cbListFN.Location = new System.Drawing.Point(154, 52);
            this.cbListFN.Name = "cbListFN";
            this.cbListFN.Size = new System.Drawing.Size(63, 22);
            this.cbListFN.TabIndex = 25;
            this.cbListFN.SelectedIndexChanged += new System.EventHandler(this.cbListFN_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbAttached);
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(16, 387);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(143, 51);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Number Attached";
            this.toolTip1.SetToolTip(this.groupBox3, "Number of macros having \r\nsupplemental signatures");
            // 
            // lbAttached
            // 
            this.lbAttached.AutoSize = true;
            this.lbAttached.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAttached.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbAttached.Location = new System.Drawing.Point(49, 26);
            this.lbAttached.Name = "lbAttached";
            this.lbAttached.Size = new System.Drawing.Size(15, 16);
            this.lbAttached.TabIndex = 0;
            this.lbAttached.Text = "0";
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAll.ForeColor = System.Drawing.Color.Red;
            this.btnRemoveAll.Location = new System.Drawing.Point(12, 72);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(118, 23);
            this.btnRemoveAll.TabIndex = 23;
            this.btnRemoveAll.Text = "Remove from:";
            this.toolTip1.SetToolTip(this.btnRemoveAll, "This will replace any existing\r\nsupplemental signature");
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnClearEB
            // 
            this.btnClearEB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearEB.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClearEB.Location = new System.Drawing.Point(233, 441);
            this.btnClearEB.Name = "btnClearEB";
            this.btnClearEB.Size = new System.Drawing.Size(120, 33);
            this.btnClearEB.TabIndex = 22;
            this.btnClearEB.Text = "Clear all";
            this.btnClearEB.UseVisualStyleBackColor = true;
            this.btnClearEB.Click += new System.EventHandler(this.btnClearEB_Click);
            // 
            // btnCopy2Clip
            // 
            this.btnCopy2Clip.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCopy2Clip.Location = new System.Drawing.Point(233, 397);
            this.btnCopy2Clip.Name = "btnCopy2Clip";
            this.btnCopy2Clip.Size = new System.Drawing.Size(120, 23);
            this.btnCopy2Clip.TabIndex = 21;
            this.btnCopy2Clip.Text = "Copy to clipboard";
            this.toolTip1.SetToolTip(this.btnCopy2Clip, "inserts <br />");
            this.btnCopy2Clip.UseVisualStyleBackColor = true;
            this.btnCopy2Clip.Click += new System.EventHandler(this.btnCopy2Clip_Click);
            // 
            // btnAdd2New
            // 
            this.btnAdd2New.Location = new System.Drawing.Point(233, 346);
            this.btnAdd2New.Name = "btnAdd2New";
            this.btnAdd2New.Size = new System.Drawing.Size(120, 23);
            this.btnAdd2New.TabIndex = 20;
            this.btnAdd2New.Text = "Add 2 newlines";
            this.toolTip1.SetToolTip(this.btnAdd2New, "inserts <br />");
            this.btnAdd2New.UseVisualStyleBackColor = true;
            this.btnAdd2New.Click += new System.EventHandler(this.btnAdd2New_Click);
            // 
            // btnAdd1New
            // 
            this.btnAdd1New.Location = new System.Drawing.Point(233, 298);
            this.btnAdd1New.Name = "btnAdd1New";
            this.btnAdd1New.Size = new System.Drawing.Size(120, 23);
            this.btnAdd1New.TabIndex = 19;
            this.btnAdd1New.Text = "Add a NewLine";
            this.toolTip1.SetToolTip(this.btnAdd1New, "inserts <br />");
            this.btnAdd1New.UseVisualStyleBackColor = true;
            this.btnAdd1New.Click += new System.EventHandler(this.btnAdd1New_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message";
            // 
            // btnSavEdits
            // 
            this.btnSavEdits.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSavEdits.Location = new System.Drawing.Point(16, 333);
            this.btnSavEdits.Name = "btnSavEdits";
            this.btnSavEdits.Size = new System.Drawing.Size(75, 23);
            this.btnSavEdits.TabIndex = 9;
            this.btnSavEdits.Text = " Save";
            this.btnSavEdits.UseVisualStyleBackColor = true;
            this.btnSavEdits.Click += new System.EventHandler(this.btnSavEdits_Click);
            // 
            // btnTest
            // 
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(16, 282);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(114, 23);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "Show on page";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Add the above items";
            // 
            // clbImages
            // 
            this.clbImages.FormattingEnabled = true;
            this.clbImages.Location = new System.Drawing.Point(233, 153);
            this.clbImages.Name = "clbImages";
            this.clbImages.Size = new System.Drawing.Size(120, 109);
            this.clbImages.TabIndex = 6;
            // 
            // tbEdit
            // 
            this.tbEdit.Location = new System.Drawing.Point(378, 155);
            this.tbEdit.Multiline = true;
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Size = new System.Drawing.Size(414, 342);
            this.tbEdit.TabIndex = 5;
            // 
            // btnCpyEdit
            // 
            this.btnCpyEdit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCpyEdit.Location = new System.Drawing.Point(16, 232);
            this.btnCpyEdit.Name = "btnCpyEdit";
            this.btnCpyEdit.Size = new System.Drawing.Size(114, 23);
            this.btnCpyEdit.TabIndex = 4;
            this.btnCpyEdit.Text = "Copy to edit box";
            this.toolTip1.SetToolTip(this.btnCpyEdit, "Click this to create the\r\nsupplemental signature");
            this.btnCpyEdit.UseVisualStyleBackColor = true;
            this.btnCpyEdit.Click += new System.EventHandler(this.btnCpyEdit_Click);
            // 
            // btnFont
            // 
            this.btnFont.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFont.Location = new System.Drawing.Point(16, 180);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 3;
            this.btnFont.Text = "Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnColor
            // 
            this.btnColor.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnColor.Location = new System.Drawing.Point(12, 128);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(114, 23);
            this.btnColor.TabIndex = 2;
            this.btnColor.Text = "Color-foreground";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnAddSupSig
            // 
            this.btnAddSupSig.Enabled = false;
            this.btnAddSupSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSupSig.ForeColor = System.Drawing.Color.Red;
            this.btnAddSupSig.Location = new System.Drawing.Point(12, 30);
            this.btnAddSupSig.Name = "btnAddSupSig";
            this.btnAddSupSig.Size = new System.Drawing.Size(114, 23);
            this.btnAddSupSig.TabIndex = 1;
            this.btnAddSupSig.Text = "Apply to macros:";
            this.toolTip1.SetToolTip(this.btnAddSupSig, "This will replace any existing\r\nsupplemental signature");
            this.btnAddSupSig.UseVisualStyleBackColor = true;
            this.btnAddSupSig.Click += new System.EventHandler(this.btnAddSupSig_Click);
            // 
            // tbSupSig
            // 
            this.tbSupSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSupSig.Location = new System.Drawing.Point(378, 32);
            this.tbSupSig.Name = "tbSupSig";
            this.tbSupSig.Size = new System.Drawing.Size(424, 20);
            this.tbSupSig.TabIndex = 0;
            this.tbSupSig.Text = "Pleae let me know if this works";
            // 
            // btnShowURL
            // 
            this.btnShowURL.Enabled = false;
            this.btnShowURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowURL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnShowURL.Location = new System.Drawing.Point(276, 25);
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
            this.btnDelURL.Location = new System.Drawing.Point(276, 64);
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
            this.groupBox4.Location = new System.Drawing.Point(359, 28);
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
    "LC=true\" target=\"_blank\">here for network setup</a> in your language";
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
            this.groupBox5.Location = new System.Drawing.Point(773, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(510, 129);
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
            this.tbURLcnt.Location = new System.Drawing.Point(428, 25);
            this.tbURLcnt.Name = "tbURLcnt";
            this.tbURLcnt.ReadOnly = true;
            this.tbURLcnt.Size = new System.Drawing.Size(48, 20);
            this.tbURLcnt.TabIndex = 0;
            this.tbURLcnt.Text = "test";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Info;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(188, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 65);
            this.label8.TabIndex = 8;
            this.label8.Text = "Browser and User Info\r\nneed to be saved.  Be \r\nsure to click Apply.  No\r\nother it" +
    "ems in this dialog\r\nbox need to be saved";
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
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 729);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.gbTB);
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
            this.gbTB.ResumeLayout(false);
            this.gbTB.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbTB;
        private System.Windows.Forms.Button btnAddSupSig;
        private System.Windows.Forms.TextBox tbSupSig;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnShowURL;
        private System.Windows.Forms.TextBox tbURLcnt;
        private System.Windows.Forms.Label lbSaveLoc;
        private System.Windows.Forms.CheckBox cbSaveUNK;
        private System.Windows.Forms.Button btnDelURL;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.TextBox tbEdit;
        private System.Windows.Forms.Button btnCpyEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clbImages;
        private System.Windows.Forms.Button btnSavEdits;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd2New;
        private System.Windows.Forms.Button btnAdd1New;
        private System.Windows.Forms.Button btnCopy2Clip;
        private System.Windows.Forms.Button btnClearEB;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbAttached;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSpecialWord;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbListFN;
        private System.Windows.Forms.Label lbMarker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbRefOnly;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbPP;
        private System.Windows.Forms.Button btnTestPP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbLongAllowed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbEmail;
    }
}