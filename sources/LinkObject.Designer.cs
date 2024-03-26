namespace MacroViewer
{
    partial class LinkObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkObject));
            this.gbSelectType = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbNotImage = new System.Windows.Forms.RadioButton();
            this.rbimage = new System.Windows.Forms.RadioButton();
            this.tbSelectedItem = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbBoxed = new System.Windows.Forms.Label();
            this.btnBoxIT = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnApplyText = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbUrlText = new System.Windows.Forms.TextBox();
            this.tbImageUrl = new System.Windows.Forms.TextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCencel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BlinkTimer = new System.Windows.Forms.Timer(this.components);
            this.gbSelectType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSelectType
            // 
            this.gbSelectType.Controls.Add(this.groupBox2);
            this.gbSelectType.Controls.Add(this.tbSelectedItem);
            this.gbSelectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectType.Location = new System.Drawing.Point(12, 32);
            this.gbSelectType.Name = "gbSelectType";
            this.gbSelectType.Size = new System.Drawing.Size(385, 199);
            this.gbSelectType.TabIndex = 0;
            this.gbSelectType.TabStop = false;
            this.gbSelectType.Text = "You Selected HTML";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbNotImage);
            this.groupBox2.Controls.Add(this.rbimage);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(28, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "This Is";
            // 
            // rbNotImage
            // 
            this.rbNotImage.AutoSize = true;
            this.rbNotImage.Location = new System.Drawing.Point(20, 62);
            this.rbNotImage.Name = "rbNotImage";
            this.rbNotImage.Size = new System.Drawing.Size(105, 20);
            this.rbNotImage.TabIndex = 1;
            this.rbNotImage.TabStop = true;
            this.rbNotImage.Text = "Treat as URL";
            this.rbNotImage.UseVisualStyleBackColor = true;
            this.rbNotImage.CheckedChanged += new System.EventHandler(this.rbNotImage_CheckedChanged);
            // 
            // rbimage
            // 
            this.rbimage.AutoSize = true;
            this.rbimage.Location = new System.Drawing.Point(20, 34);
            this.rbimage.Name = "rbimage";
            this.rbimage.Size = new System.Drawing.Size(63, 20);
            this.rbimage.TabIndex = 0;
            this.rbimage.TabStop = true;
            this.rbimage.Text = "Image";
            this.rbimage.UseVisualStyleBackColor = true;
            this.rbimage.CheckedChanged += new System.EventHandler(this.rbimage_CheckedChanged);
            // 
            // tbSelectedItem
            // 
            this.tbSelectedItem.Location = new System.Drawing.Point(28, 159);
            this.tbSelectedItem.Name = "tbSelectedItem";
            this.tbSelectedItem.Size = new System.Drawing.Size(341, 22);
            this.tbSelectedItem.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbBoxed);
            this.groupBox1.Controls.Add(this.btnBoxIT);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnApplyText);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.tbImageUrl);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(647, 296);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compose URL or image";
            // 
            // lbBoxed
            // 
            this.lbBoxed.AutoSize = true;
            this.lbBoxed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBoxed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBoxed.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbBoxed.Location = new System.Drawing.Point(402, 168);
            this.lbBoxed.Name = "lbBoxed";
            this.lbBoxed.Size = new System.Drawing.Size(73, 22);
            this.lbBoxed.TabIndex = 7;
            this.lbBoxed.Text = "BOXED";
            this.lbBoxed.Visible = false;
            // 
            // btnBoxIT
            // 
            this.btnBoxIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoxIT.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnBoxIT.Location = new System.Drawing.Point(172, 161);
            this.btnBoxIT.Name = "btnBoxIT";
            this.btnBoxIT.Size = new System.Drawing.Size(210, 36);
            this.btnBoxIT.TabIndex = 6;
            this.btnBoxIT.Text = "Put In Box";
            this.btnBoxIT.UseVisualStyleBackColor = true;
            this.btnBoxIT.Click += new System.EventHandler(this.btnBoxIT_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTest.Location = new System.Drawing.Point(496, 161);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(122, 36);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test Object";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnApplyText
            // 
            this.btnApplyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyText.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApplyText.Location = new System.Drawing.Point(28, 161);
            this.btnApplyText.Name = "btnApplyText";
            this.btnApplyText.Size = new System.Drawing.Size(122, 36);
            this.btnApplyText.TabIndex = 4;
            this.btnApplyText.Text = "Form Object";
            this.btnApplyText.UseVisualStyleBackColor = true;
            this.btnApplyText.Click += new System.EventHandler(this.btnApplyText_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbUrlText);
            this.groupBox3.Location = new System.Drawing.Point(28, 44);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(590, 94);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Text for URL (can be ignored)";
            // 
            // tbUrlText
            // 
            this.tbUrlText.Location = new System.Drawing.Point(43, 37);
            this.tbUrlText.Name = "tbUrlText";
            this.tbUrlText.Size = new System.Drawing.Size(404, 22);
            this.tbUrlText.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbUrlText, "Can be left blank if no text wanted");
            // 
            // tbImageUrl
            // 
            this.tbImageUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbImageUrl.Location = new System.Drawing.Point(28, 219);
            this.tbImageUrl.Multiline = true;
            this.tbImageUrl.Name = "tbImageUrl";
            this.tbImageUrl.Size = new System.Drawing.Size(590, 52);
            this.tbImageUrl.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbImageUrl, "This can be edited");
            // 
            // pbImage
            // 
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(477, 32);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(335, 199);
            this.pbImage.TabIndex = 2;
            this.pbImage.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnApply.Location = new System.Drawing.Point(690, 352);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(122, 36);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply and exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCencel
            // 
            this.btnCencel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCencel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCencel.Location = new System.Drawing.Point(690, 407);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(122, 36);
            this.btnCencel.TabIndex = 4;
            this.btnCencel.Text = "Cancel and exit";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // BlinkTimer
            // 
            this.BlinkTimer.Interval = 500;
            this.BlinkTimer.Tick += new System.EventHandler(this.BlinkTimer_Tick);
            // 
            // LinkObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 558);
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSelectType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinkObject";
            this.Text = "LinkObject";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinkObject_FormClosing);
            this.Shown += new System.EventHandler(this.LinkObject_Shown);
            this.gbSelectType.ResumeLayout(false);
            this.gbSelectType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSelectType;
        private System.Windows.Forms.TextBox tbSelectedItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbImageUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbNotImage;
        private System.Windows.Forms.RadioButton rbimage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnApplyText;
        private System.Windows.Forms.TextBox tbUrlText;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBoxIT;
        private System.Windows.Forms.Label lbBoxed;
        private System.Windows.Forms.Timer BlinkTimer;
    }
}