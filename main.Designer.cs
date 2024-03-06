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
            this.tbNumMac = new System.Windows.Forms.TextBox();
            this.tbMacName = new System.Windows.Forms.TextBox();
            this.btnChangeUrls = new System.Windows.Forms.Button();
            this.btnCopyTo = new System.Windows.Forms.Button();
            this.cbLaunchPage = new System.Windows.Forms.CheckBox();
            this.tbBody = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPrinterMacsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePrinterMacsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSignatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpMainEdit = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.gbManageImages = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAppendMac = new System.Windows.Forms.Button();
            this.btnSetObj = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbNotM = new System.Windows.Forms.Label();
            this.lbM = new System.Windows.Forms.Label();
            this.btnNoMark = new System.Windows.Forms.Button();
            this.btnToMark = new System.Windows.Forms.Button();
            this.btnToNotepad = new System.Windows.Forms.Button();
            this.btnCopyFrom = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCLrUrl = new System.Windows.Forms.Button();
            this.btnAddURL = new System.Windows.Forms.Button();
            this.tbImgUrl = new System.Windows.Forms.TextBox();
            this.btnAddImg = new System.Windows.Forms.Button();
            this.gbSupp = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveM = new System.Windows.Forms.Button();
            this.btnDelM = new System.Windows.Forms.Button();
            this.btnClearEM = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.downloadMacrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.lbName)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.gpMainEdit.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.gbManageImages.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.lbName.Size = new System.Drawing.Size(360, 639);
            this.lbName.TabIndex = 4;
            this.toolTip1.SetToolTip(this.lbName, "you must double click a row");
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
            this.btnGo.Location = new System.Drawing.Point(353, 61);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(121, 31);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Show As Page";
            this.toolTip1.SetToolTip(this.btnGo, "Pops up form using Edge or Chrome");
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnAdd1New
            // 
            this.btnAdd1New.Location = new System.Drawing.Point(70, 133);
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
            this.btnAdd2New.Location = new System.Drawing.Point(53, 174);
            this.btnAdd2New.Name = "btnAdd2New";
            this.btnAdd2New.Size = new System.Drawing.Size(108, 23);
            this.btnAdd2New.TabIndex = 18;
            this.btnAdd2New.Text = "Add 2 newlines";
            this.toolTip1.SetToolTip(this.btnAdd2New, "inserts <br>");
            this.btnAdd2New.UseVisualStyleBackColor = true;
            this.btnAdd2New.Click += new System.EventHandler(this.btnAdd2New_Click);
            // 
            // tbNumMac
            // 
            this.tbNumMac.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbNumMac.Location = new System.Drawing.Point(21, 30);
            this.tbNumMac.Name = "tbNumMac";
            this.tbNumMac.ReadOnly = true;
            this.tbNumMac.Size = new System.Drawing.Size(54, 22);
            this.tbNumMac.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tbNumMac, "total number of macros");
            // 
            // tbMacName
            // 
            this.tbMacName.Location = new System.Drawing.Point(18, 53);
            this.tbMacName.Name = "tbMacName";
            this.tbMacName.Size = new System.Drawing.Size(190, 22);
            this.tbMacName.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbMacName, "Name the the macro you are adding or editing");
            // 
            // btnChangeUrls
            // 
            this.btnChangeUrls.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeUrls.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnChangeUrls.Location = new System.Drawing.Point(253, 88);
            this.btnChangeUrls.Name = "btnChangeUrls";
            this.btnChangeUrls.Size = new System.Drawing.Size(138, 25);
            this.btnChangeUrls.TabIndex = 9;
            this.btnChangeUrls.Text = "Change URLS";
            this.toolTip1.SetToolTip(this.btnChangeUrls, "You must upload images to your\r\nHP community picture folder first.");
            this.btnChangeUrls.UseVisualStyleBackColor = true;
            this.btnChangeUrls.Click += new System.EventHandler(this.btnChangeUrls_Click);
            // 
            // btnCopyTo
            // 
            this.btnCopyTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyTo.Location = new System.Drawing.Point(235, 35);
            this.btnCopyTo.Name = "btnCopyTo";
            this.btnCopyTo.Size = new System.Drawing.Size(161, 29);
            this.btnCopyTo.TabIndex = 9;
            this.btnCopyTo.Text = "Copy to clipboard";
            this.toolTip1.SetToolTip(this.btnCopyTo, "Change to markup and copy to clipboard then\r\nlog into your HP Community page, sel" +
        "ect your\r\nmacro list, edit the macro and past into the\r\nmacro too update it.");
            this.btnCopyTo.UseVisualStyleBackColor = true;
            this.btnCopyTo.Click += new System.EventHandler(this.btnCopyTo_Click);
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
            this.toolTip1.SetToolTip(this.cbLaunchPage, "If checked then a web page is \r\nlaunched when a row id double clicked");
            this.cbLaunchPage.UseVisualStyleBackColor = true;
            // 
            // tbBody
            // 
            this.tbBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBody.Location = new System.Drawing.Point(18, 33);
            this.tbBody.Multiline = true;
            this.tbBody.Name = "tbBody";
            this.tbBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbBody.Size = new System.Drawing.Size(397, 441);
            this.tbBody.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.utilsToolStripMenuItem,
            this.testSignatureToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1387, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readHTMLToolStripMenuItem,
            this.loadFromXMLToolStripMenuItem,
            this.loadPrinterMacsToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToXMLToolStripMenuItem,
            this.savePrinterMacsToolStripMenuItem,
            this.downloadMacrosToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // readHTMLToolStripMenuItem
            // 
            this.readHTMLToolStripMenuItem.Name = "readHTMLToolStripMenuItem";
            this.readHTMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.readHTMLToolStripMenuItem.Text = "Read HTML file";
            this.readHTMLToolStripMenuItem.Click += new System.EventHandler(this.readHTMLToolStripMenuItem_Click);
            // 
            // loadFromXMLToolStripMenuItem
            // 
            this.loadFromXMLToolStripMenuItem.Name = "loadFromXMLToolStripMenuItem";
            this.loadFromXMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadFromXMLToolStripMenuItem.Text = "Load PC macro";
            this.loadFromXMLToolStripMenuItem.Click += new System.EventHandler(this.loadFromXMLToolStripMenuItem_Click);
            // 
            // loadPrinterMacsToolStripMenuItem
            // 
            this.loadPrinterMacsToolStripMenuItem.Name = "loadPrinterMacsToolStripMenuItem";
            this.loadPrinterMacsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadPrinterMacsToolStripMenuItem.Text = "Load Printer macs";
            this.loadPrinterMacsToolStripMenuItem.Click += new System.EventHandler(this.loadPrinterMacsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // saveToXMLToolStripMenuItem
            // 
            this.saveToXMLToolStripMenuItem.Name = "saveToXMLToolStripMenuItem";
            this.saveToXMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToXMLToolStripMenuItem.Text = "Save PC macro";
            this.saveToXMLToolStripMenuItem.Click += new System.EventHandler(this.saveToXMLToolStripMenuItem_Click);
            // 
            // savePrinterMacsToolStripMenuItem
            // 
            this.savePrinterMacsToolStripMenuItem.Name = "savePrinterMacsToolStripMenuItem";
            this.savePrinterMacsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.savePrinterMacsToolStripMenuItem.Text = "Save Printer macs";
            this.savePrinterMacsToolStripMenuItem.Click += new System.EventHandler(this.savePrinterMacsToolStripMenuItem_Click);
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
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gpMainEdit
            // 
            this.gpMainEdit.Controls.Add(this.btnNew);
            this.gpMainEdit.Controls.Add(this.groupBox6);
            this.gpMainEdit.Controls.Add(this.groupBox5);
            this.gpMainEdit.Controls.Add(this.groupBox4);
            this.gpMainEdit.Controls.Add(this.gbSupp);
            this.gpMainEdit.Controls.Add(this.btnClearEM);
            this.gpMainEdit.Controls.Add(this.btnGo);
            this.gpMainEdit.Enabled = false;
            this.gpMainEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpMainEdit.Location = new System.Drawing.Point(434, 43);
            this.gpMainEdit.Name = "gpMainEdit";
            this.gpMainEdit.Size = new System.Drawing.Size(927, 763);
            this.gpMainEdit.TabIndex = 8;
            this.gpMainEdit.TabStop = false;
            this.gpMainEdit.Text = "Edit Macro";
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnNew.Location = new System.Drawing.Point(399, 171);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 32);
            this.btnNew.TabIndex = 18;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox6.Controls.Add(this.gbManageImages);
            this.groupBox6.Controls.Add(this.btnSetObj);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.tbBody);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(484, 21);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(437, 709);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Enter text or html and click to Show As Page";
            // 
            // gbManageImages
            // 
            this.gbManageImages.Controls.Add(this.label4);
            this.gbManageImages.Controls.Add(this.btnChangeUrls);
            this.gbManageImages.Controls.Add(this.label3);
            this.gbManageImages.Controls.Add(this.btnAppendMac);
            this.gbManageImages.Location = new System.Drawing.Point(18, 563);
            this.gbManageImages.Name = "gbManageImages";
            this.gbManageImages.Size = new System.Drawing.Size(397, 131);
            this.gbManageImages.TabIndex = 8;
            this.gbManageImages.TabStop = false;
            this.gbManageImages.Text = "Manage Images";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Location = new System.Drawing.Point(6, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "This changes image path to\r\nyour HP picture folder URLs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "This can create an entire macro\r\none image at a time";
            // 
            // btnAppendMac
            // 
            this.btnAppendMac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppendMac.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAppendMac.Location = new System.Drawing.Point(253, 28);
            this.btnAppendMac.Name = "btnAppendMac";
            this.btnAppendMac.Size = new System.Drawing.Size(138, 25);
            this.btnAppendMac.TabIndex = 7;
            this.btnAppendMac.Text = "Append Image";
            this.btnAppendMac.UseVisualStyleBackColor = true;
            this.btnAppendMac.Click += new System.EventHandler(this.btnAppendMac_Click);
            // 
            // btnSetObj
            // 
            this.btnSetObj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetObj.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSetObj.Location = new System.Drawing.Point(293, 510);
            this.btnSetObj.Name = "btnSetObj";
            this.btnSetObj.Size = new System.Drawing.Size(138, 25);
            this.btnSetObj.TabIndex = 5;
            this.btnSetObj.Text = "EDIT LINK";
            this.btnSetObj.UseVisualStyleBackColor = true;
            this.btnSetObj.Click += new System.EventHandler(this.btnSetObj_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(15, 497);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 48);
            this.label1.TabIndex = 4;
            this.label1.Text = "Position cursor at any location, or\r\nhighlight (select) any text or URL and\r\nclic" +
    "k EDIT LINK to create an object.";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbNotM);
            this.groupBox5.Controls.Add(this.btnAdd2New);
            this.groupBox5.Controls.Add(this.lbM);
            this.groupBox5.Controls.Add(this.btnNoMark);
            this.groupBox5.Controls.Add(this.btnAdd1New);
            this.groupBox5.Controls.Add(this.btnToMark);
            this.groupBox5.Controls.Add(this.btnToNotepad);
            this.groupBox5.Controls.Add(this.btnCopyTo);
            this.groupBox5.Controls.Add(this.btnCopyFrom);
            this.groupBox5.Location = new System.Drawing.Point(24, 276);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(425, 219);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Markup Editing (enable for images)";
            // 
            // lbNotM
            // 
            this.lbNotM.AutoEllipsis = true;
            this.lbNotM.AutoSize = true;
            this.lbNotM.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbNotM.ForeColor = System.Drawing.Color.Green;
            this.lbNotM.Location = new System.Drawing.Point(167, 90);
            this.lbNotM.Name = "lbNotM";
            this.lbNotM.Size = new System.Drawing.Size(21, 17);
            this.lbNotM.TabIndex = 16;
            this.lbNotM.Text = "";
            // 
            // lbM
            // 
            this.lbM.AutoEllipsis = true;
            this.lbM.AutoSize = true;
            this.lbM.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbM.ForeColor = System.Drawing.Color.Green;
            this.lbM.Location = new System.Drawing.Point(167, 43);
            this.lbM.Name = "lbM";
            this.lbM.Size = new System.Drawing.Size(21, 17);
            this.lbM.TabIndex = 15;
            this.lbM.Text = "";
            // 
            // btnNoMark
            // 
            this.btnNoMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNoMark.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNoMark.Location = new System.Drawing.Point(6, 84);
            this.btnNoMark.Name = "btnNoMark";
            this.btnNoMark.Size = new System.Drawing.Size(155, 29);
            this.btnNoMark.TabIndex = 14;
            this.btnNoMark.Text = "Remove markup";
            this.btnNoMark.UseVisualStyleBackColor = true;
            this.btnNoMark.Click += new System.EventHandler(this.btnNoMark_Click);
            // 
            // btnToMark
            // 
            this.btnToMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToMark.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnToMark.Location = new System.Drawing.Point(6, 36);
            this.btnToMark.Name = "btnToMark";
            this.btnToMark.Size = new System.Drawing.Size(155, 29);
            this.btnToMark.TabIndex = 13;
            this.btnToMark.Text = "Switch to markup";
            this.btnToMark.UseVisualStyleBackColor = true;
            this.btnToMark.Click += new System.EventHandler(this.btnToMark_Click);
            // 
            // btnToNotepad
            // 
            this.btnToNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToNotepad.Location = new System.Drawing.Point(235, 127);
            this.btnToNotepad.Name = "btnToNotepad";
            this.btnToNotepad.Size = new System.Drawing.Size(161, 29);
            this.btnToNotepad.TabIndex = 12;
            this.btnToNotepad.Text = "Copy to notepad";
            this.btnToNotepad.UseVisualStyleBackColor = true;
            this.btnToNotepad.Click += new System.EventHandler(this.btnToNotepad_Click);
            // 
            // btnCopyFrom
            // 
            this.btnCopyFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyFrom.Location = new System.Drawing.Point(235, 84);
            this.btnCopyFrom.Name = "btnCopyFrom";
            this.btnCopyFrom.Size = new System.Drawing.Size(161, 27);
            this.btnCopyFrom.TabIndex = 10;
            this.btnCopyFrom.Text = "Copy from clipboard";
            this.btnCopyFrom.UseVisualStyleBackColor = true;
            this.btnCopyFrom.Click += new System.EventHandler(this.btnCopyFrom_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCLrUrl);
            this.groupBox4.Controls.Add(this.btnAddURL);
            this.groupBox4.Controls.Add(this.tbImgUrl);
            this.groupBox4.Controls.Add(this.btnAddImg);
            this.groupBox4.Location = new System.Drawing.Point(24, 531);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(419, 119);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add image or newlines. For URLs use the Utils menu item";
            // 
            // btnCLrUrl
            // 
            this.btnCLrUrl.Location = new System.Drawing.Point(272, 36);
            this.btnCLrUrl.Name = "btnCLrUrl";
            this.btnCLrUrl.Size = new System.Drawing.Size(70, 23);
            this.btnCLrUrl.TabIndex = 19;
            this.btnCLrUrl.Text = "Clear";
            this.btnCLrUrl.UseVisualStyleBackColor = true;
            this.btnCLrUrl.Click += new System.EventHandler(this.btnCLrUrl_Click);
            // 
            // btnAddURL
            // 
            this.btnAddURL.Location = new System.Drawing.Point(153, 36);
            this.btnAddURL.Name = "btnAddURL";
            this.btnAddURL.Size = new System.Drawing.Size(91, 23);
            this.btnAddURL.TabIndex = 17;
            this.btnAddURL.Text = "Paste URL";
            this.btnAddURL.UseVisualStyleBackColor = true;
            this.btnAddURL.Click += new System.EventHandler(this.btnAddURL_Click);
            // 
            // tbImgUrl
            // 
            this.tbImgUrl.Location = new System.Drawing.Point(6, 78);
            this.tbImgUrl.Name = "tbImgUrl";
            this.tbImgUrl.Size = new System.Drawing.Size(378, 22);
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
            this.gbSupp.Controls.Add(this.label2);
            this.gbSupp.Controls.Add(this.groupBox3);
            this.gbSupp.Controls.Add(this.tbMacName);
            this.gbSupp.Controls.Add(this.btnSaveM);
            this.gbSupp.Controls.Add(this.btnDelM);
            this.gbSupp.Location = new System.Drawing.Point(24, 50);
            this.gbSupp.Name = "gbSupp";
            this.gbSupp.Size = new System.Drawing.Size(250, 201);
            this.gbSupp.TabIndex = 13;
            this.gbSupp.TabStop = false;
            this.gbSupp.Text = "Supplemental Table";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Macro Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbNumMac);
            this.groupBox3.Location = new System.Drawing.Point(18, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(104, 72);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Count";
            // 
            // btnSaveM
            // 
            this.btnSaveM.Enabled = false;
            this.btnSaveM.Location = new System.Drawing.Point(137, 112);
            this.btnSaveM.Name = "btnSaveM";
            this.btnSaveM.Size = new System.Drawing.Size(91, 23);
            this.btnSaveM.TabIndex = 2;
            this.btnSaveM.Text = "Save Macro";
            this.btnSaveM.UseVisualStyleBackColor = true;
            this.btnSaveM.Click += new System.EventHandler(this.btnSaveM_Click);
            // 
            // btnDelM
            // 
            this.btnDelM.Enabled = false;
            this.btnDelM.Location = new System.Drawing.Point(137, 150);
            this.btnDelM.Name = "btnDelM";
            this.btnDelM.Size = new System.Drawing.Size(91, 23);
            this.btnDelM.TabIndex = 1;
            this.btnDelM.Text = "Delete";
            this.btnDelM.UseVisualStyleBackColor = true;
            this.btnDelM.Click += new System.EventHandler(this.btnDelM_Click);
            // 
            // btnClearEM
            // 
            this.btnClearEM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearEM.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnClearEM.Location = new System.Drawing.Point(399, 117);
            this.btnClearEM.Name = "btnClearEM";
            this.btnClearEM.Size = new System.Drawing.Size(75, 32);
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
            this.groupBox2.Size = new System.Drawing.Size(404, 730);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Double click any row to transfer to editor";
            // 
            // downloadMacrosToolStripMenuItem
            // 
            this.downloadMacrosToolStripMenuItem.Name = "downloadMacrosToolStripMenuItem";
            this.downloadMacrosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.downloadMacrosToolStripMenuItem.Text = "Download Macros";
            this.downloadMacrosToolStripMenuItem.Click += new System.EventHandler(this.downloadMacrosToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 785);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gpMainEdit);
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
            this.gpMainEdit.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.gbManageImages.ResumeLayout(false);
            this.gbManageImages.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.GroupBox gpMainEdit;
        private System.Windows.Forms.Button btnClearEM;
        private System.Windows.Forms.Button btnCopyFrom;
        private System.Windows.Forms.Button btnCopyTo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inx;
        private System.Windows.Forms.DataGridViewTextBoxColumn MacName;
        private System.Windows.Forms.ToolStripMenuItem testSignatureToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbLaunchPage;
        private System.Windows.Forms.Button btnToNotepad;
        private System.Windows.Forms.ToolStripMenuItem readHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToXMLToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbSupp;
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
        private System.Windows.Forms.Button btnCLrUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnNoMark;
        private System.Windows.Forms.Button btnToMark;
        private System.Windows.Forms.ToolStripMenuItem loadPrinterMacsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePrinterMacsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetObj;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lbM;
        private System.Windows.Forms.Label lbNotM;
        private System.Windows.Forms.Button btnAppendMac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbManageImages;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnChangeUrls;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadMacrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

