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
            this.btnCencel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnFormRemote = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbUrlText = new System.Windows.Forms.TextBox();
            this.gbSelectType = new System.Windows.Forms.GroupBox();
            this.tbMacName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNextNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbImgCnt = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbSelectType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCencel
            // 
            this.btnCencel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCencel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCencel.Location = new System.Drawing.Point(793, 526);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(122, 36);
            this.btnCencel.TabIndex = 9;
            this.btnCencel.Text = "Cancel and exit";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(793, 469);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(122, 36);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(533, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(455, 367);
            this.pbImage.TabIndex = 7;
            this.pbImage.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFormRemote);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 274);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Location";
            // 
            // btnPaste
            // 
            this.btnPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPaste.Location = new System.Drawing.Point(793, 403);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(195, 36);
            this.btnPaste.TabIndex = 5;
            this.btnPaste.Text = "Paste From Clipboard";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnFormRemote
            // 
            this.btnFormRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormRemote.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFormRemote.Location = new System.Drawing.Point(28, 161);
            this.btnFormRemote.Name = "btnFormRemote";
            this.btnFormRemote.Size = new System.Drawing.Size(122, 36);
            this.btnFormRemote.TabIndex = 4;
            this.btnFormRemote.Text = "Show Image";
            this.btnFormRemote.UseVisualStyleBackColor = true;
            this.btnFormRemote.Click += new System.EventHandler(this.btnFormRemote_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbUrlText);
            this.groupBox3.Location = new System.Drawing.Point(28, 44);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 94);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remote URL or local path";
            // 
            // tbUrlText
            // 
            this.tbUrlText.Location = new System.Drawing.Point(35, 40);
            this.tbUrlText.Name = "tbUrlText";
            this.tbUrlText.Size = new System.Drawing.Size(343, 22);
            this.tbUrlText.TabIndex = 0;
            // 
            // gbSelectType
            // 
            this.gbSelectType.Controls.Add(this.tbImgCnt);
            this.gbSelectType.Controls.Add(this.label3);
            this.gbSelectType.Controls.Add(this.label2);
            this.gbSelectType.Controls.Add(this.tbNextNum);
            this.gbSelectType.Controls.Add(this.label1);
            this.gbSelectType.Controls.Add(this.tbMacName);
            this.gbSelectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectType.Location = new System.Drawing.Point(17, 33);
            this.gbSelectType.Name = "gbSelectType";
            this.gbSelectType.Size = new System.Drawing.Size(385, 183);
            this.gbSelectType.TabIndex = 5;
            this.gbSelectType.TabStop = false;
            this.gbSelectType.Text = "You Selected HTML";
            // 
            // tbMacName
            // 
            this.tbMacName.Location = new System.Drawing.Point(148, 35);
            this.tbMacName.Name = "tbMacName";
            this.tbMacName.Size = new System.Drawing.Size(198, 22);
            this.tbMacName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Macro Name";
            // 
            // tbNextNum
            // 
            this.tbNextNum.Location = new System.Drawing.Point(148, 80);
            this.tbNextNum.Name = "tbNextNum";
            this.tbNextNum.Size = new System.Drawing.Size(41, 22);
            this.tbNextNum.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Next Image Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Number Images";
            // 
            // tbImgCnt
            // 
            this.tbImgCnt.Location = new System.Drawing.Point(148, 127);
            this.tbImgCnt.Name = "tbImgCnt";
            this.tbImgCnt.Size = new System.Drawing.Size(41, 22);
            this.tbImgCnt.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.Location = new System.Drawing.Point(533, 403);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(122, 36);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // CreateMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 596);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSelectType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateMacro";
            this.Text = "CreateMacro";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbSelectType.ResumeLayout(false);
            this.gbSelectType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnFormRemote;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbUrlText;
        private System.Windows.Forms.GroupBox gbSelectType;
        private System.Windows.Forms.TextBox tbMacName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNextNum;
        private System.Windows.Forms.TextBox tbImgCnt;
        private System.Windows.Forms.Button btnClear;
    }
}