namespace MacroViewer
{
    partial class CreateMacro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateMacro));
            this.btnCencel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseImg = new System.Windows.Forms.Button();
            this.btnFormRemote = new System.Windows.Forms.Button();
            this.tbUrlText = new System.Windows.Forms.TextBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.cbFormBorder = new System.Windows.Forms.CheckBox();
            this.gbPCTbw = new System.Windows.Forms.GroupBox();
            this.rb0pct = new System.Windows.Forms.RadioButton();
            this.rb50 = new System.Windows.Forms.RadioButton();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.btnTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbPCTbw.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCencel
            // 
            this.btnCencel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCencel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCencel.Location = new System.Drawing.Point(24, 498);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(122, 36);
            this.btnCencel.TabIndex = 9;
            this.btnCencel.Text = "Cancel and exit";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(24, 431);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(122, 36);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(474, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(565, 473);
            this.pbImage.TabIndex = 7;
            this.pbImage.TabStop = false;
            this.pbImage.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbImage_LoadCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowseImg);
            this.groupBox1.Controls.Add(this.btnFormRemote);
            this.groupBox1.Controls.Add(this.tbUrlText);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 198);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Location";
            // 
            // btnBrowseImg
            // 
            this.btnBrowseImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseImg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnBrowseImg.Location = new System.Drawing.Point(15, 76);
            this.btnBrowseImg.Name = "btnBrowseImg";
            this.btnBrowseImg.Size = new System.Drawing.Size(163, 36);
            this.btnBrowseImg.TabIndex = 5;
            this.btnBrowseImg.Text = "Browse for image";
            this.btnBrowseImg.UseVisualStyleBackColor = true;
            this.btnBrowseImg.Click += new System.EventHandler(this.btnBrowseImg_Click);
            // 
            // btnFormRemote
            // 
            this.btnFormRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormRemote.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFormRemote.Location = new System.Drawing.Point(15, 130);
            this.btnFormRemote.Name = "btnFormRemote";
            this.btnFormRemote.Size = new System.Drawing.Size(163, 36);
            this.btnFormRemote.TabIndex = 4;
            this.btnFormRemote.Text = "Use Above Image";
            this.btnFormRemote.UseVisualStyleBackColor = true;
            this.btnFormRemote.Click += new System.EventHandler(this.btnFormRemote_Click);
            // 
            // tbUrlText
            // 
            this.tbUrlText.Location = new System.Drawing.Point(15, 39);
            this.tbUrlText.Name = "tbUrlText";
            this.tbUrlText.Size = new System.Drawing.Size(343, 22);
            this.tbUrlText.TabIndex = 0;
            this.tbUrlText.TextChanged += new System.EventHandler(this.tbUrlText_TextChanged);
            // 
            // btnPaste
            // 
            this.btnPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPaste.Location = new System.Drawing.Point(165, 226);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(227, 36);
            this.btnPaste.TabIndex = 5;
            this.btnPaste.Text = "Paste Image From Clipboard";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.Location = new System.Drawing.Point(24, 343);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(122, 36);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // cbFormBorder
            // 
            this.cbFormBorder.AutoSize = true;
            this.cbFormBorder.Checked = true;
            this.cbFormBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFormBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFormBorder.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbFormBorder.Location = new System.Drawing.Point(226, 371);
            this.cbFormBorder.Name = "cbFormBorder";
            this.cbFormBorder.Size = new System.Drawing.Size(91, 20);
            this.cbFormBorder.TabIndex = 11;
            this.cbFormBorder.Text = "Form Box";
            this.cbFormBorder.UseVisualStyleBackColor = true;
            // 
            // gbPCTbw
            // 
            this.gbPCTbw.Controls.Add(this.rb0pct);
            this.gbPCTbw.Controls.Add(this.rb50);
            this.gbPCTbw.Controls.Add(this.rb100);
            this.gbPCTbw.Location = new System.Drawing.Point(226, 422);
            this.gbPCTbw.Name = "gbPCTbw";
            this.gbPCTbw.Size = new System.Drawing.Size(165, 113);
            this.gbPCTbw.TabIndex = 14;
            this.gbPCTbw.TabStop = false;
            this.gbPCTbw.Text = "Box width (%)";
            // 
            // rb0pct
            // 
            this.rb0pct.AutoSize = true;
            this.rb0pct.Checked = true;
            this.rb0pct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb0pct.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rb0pct.Location = new System.Drawing.Point(38, 84);
            this.rb0pct.Name = "rb0pct";
            this.rb0pct.Size = new System.Drawing.Size(74, 20);
            this.rb0pct.TabIndex = 13;
            this.rb0pct.TabStop = true;
            this.rb0pct.Text = "Default";
            this.rb0pct.UseVisualStyleBackColor = true;
            // 
            // rb50
            // 
            this.rb50.AutoSize = true;
            this.rb50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb50.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rb50.Location = new System.Drawing.Point(38, 55);
            this.rb50.Name = "rb50";
            this.rb50.Size = new System.Drawing.Size(41, 20);
            this.rb50.TabIndex = 12;
            this.rb50.Text = "50";
            this.rb50.UseVisualStyleBackColor = true;
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb100.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rb100.Location = new System.Drawing.Point(38, 25);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(49, 20);
            this.rb100.TabIndex = 11;
            this.rb100.Text = "100";
            this.rb100.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(305, 290);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(87, 36);
            this.btnTest.TabIndex = 15;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // CreateMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 556);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.gbPCTbw);
            this.Controls.Add(this.cbFormBorder);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateMacro";
            this.Text = "CreateMacro";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbPCTbw.ResumeLayout(false);
            this.gbPCTbw.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnFormRemote;
        private System.Windows.Forms.TextBox tbUrlText;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBrowseImg;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.CheckBox cbFormBorder;
        private System.Windows.Forms.GroupBox gbPCTbw;
        private System.Windows.Forms.RadioButton rb0pct;
        private System.Windows.Forms.RadioButton rb50;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.Button btnTest;
    }
}