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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCvt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClip = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbTEXT = new System.Windows.Forms.TextBox();
            this.btnClearURL = new System.Windows.Forms.Button();
            this.btnClrHREF = new System.Windows.Forms.Button();
            this.tbScratch = new System.Windows.Forms.TextBox();
            this.btnClrScratch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Location = new System.Drawing.Point(19, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(208, 82);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Enter the text of the url in the url box and\r\nenter the text, if any, i in the HR" +
    "EF box.\r\nThen Click \"GO\" to create the proper url.";
            // 
            // btnCvt
            // 
            this.btnCvt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCvt.Location = new System.Drawing.Point(63, 122);
            this.btnCvt.Name = "btnCvt";
            this.btnCvt.Size = new System.Drawing.Size(75, 23);
            this.btnCvt.TabIndex = 1;
            this.btnCvt.Text = "GO";
            this.btnCvt.UseVisualStyleBackColor = true;
            this.btnCvt.Click += new System.EventHandler(this.btnCvt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClrScratch);
            this.groupBox1.Controls.Add(this.tbScratch);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnCvt);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(32, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 487);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Form URL";
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbResult);
            this.groupBox4.Controls.Add(this.btnClip);
            this.groupBox4.Location = new System.Drawing.Point(137, 187);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(578, 85);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Result";
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
            // tbScratch
            // 
            this.tbScratch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScratch.Location = new System.Drawing.Point(81, 301);
            this.tbScratch.Multiline = true;
            this.tbScratch.Name = "tbScratch";
            this.tbScratch.Size = new System.Drawing.Size(634, 168);
            this.tbScratch.TabIndex = 7;
            this.tbScratch.Text = "use this area for scratch work.  Be sure to use <br>, for a new line.  Do not pre" +
    "ss return.\r\nuse <img src=\"https://example.com/xyzzy.jpg\" border=\"1\"> for images";
            // 
            // btnClrScratch
            // 
            this.btnClrScratch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClrScratch.Location = new System.Drawing.Point(19, 360);
            this.btnClrScratch.Name = "btnClrScratch";
            this.btnClrScratch.Size = new System.Drawing.Size(48, 23);
            this.btnClrScratch.TabIndex = 8;
            this.btnClrScratch.Text = "Clear";
            this.btnClrScratch.UseVisualStyleBackColor = true;
            this.btnClrScratch.Click += new System.EventHandler(this.btnClrScratch_Click);
            // 
            // utils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 544);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
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
    }
}