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
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCencel
            // 
            this.btnCencel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCencel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCencel.Location = new System.Drawing.Point(204, 403);
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
            this.btnApply.Location = new System.Drawing.Point(204, 331);
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
            this.pbImage.Location = new System.Drawing.Point(423, 12);
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
            this.btnPaste.Location = new System.Drawing.Point(154, 261);
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
            this.btnClear.Location = new System.Drawing.Point(66, 331);
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
            // CreateMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 510);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateMacro";
            this.Text = "CreateMacro";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

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
    }
}