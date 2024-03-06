namespace MacroViewer
{
    partial class help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(help));
            this.rFILE = new System.Windows.Forms.RichTextBox();
            this.rUTIL = new System.Windows.Forms.RichTextBox();
            this.rSIG = new System.Windows.Forms.RichTextBox();
            this.rEDIT = new System.Windows.Forms.RichTextBox();
            this.rEDITLINK = new System.Windows.Forms.RichTextBox();
            this.rMANAGE = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rFILE
            // 
            this.rFILE.Location = new System.Drawing.Point(0, 0);
            this.rFILE.Name = "rFILE";
            this.rFILE.Size = new System.Drawing.Size(266, 183);
            this.rFILE.TabIndex = 1;
            this.rFILE.Text = resources.GetString("rFILE.Text");
            // 
            // rUTIL
            // 
            this.rUTIL.Location = new System.Drawing.Point(329, 26);
            this.rUTIL.Name = "rUTIL";
            this.rUTIL.Size = new System.Drawing.Size(258, 172);
            this.rUTIL.TabIndex = 2;
            this.rUTIL.Text = resources.GetString("rUTIL.Text");
            // 
            // rSIG
            // 
            this.rSIG.Location = new System.Drawing.Point(604, 26);
            this.rSIG.Name = "rSIG";
            this.rSIG.Size = new System.Drawing.Size(197, 131);
            this.rSIG.TabIndex = 3;
            this.rSIG.Text = resources.GetString("rSIG.Text");
            // 
            // rEDIT
            // 
            this.rEDIT.Location = new System.Drawing.Point(12, 225);
            this.rEDIT.Name = "rEDIT";
            this.rEDIT.Size = new System.Drawing.Size(254, 228);
            this.rEDIT.TabIndex = 4;
            this.rEDIT.Text = resources.GetString("rEDIT.Text");
            // 
            // rEDITLINK
            // 
            this.rEDITLINK.Location = new System.Drawing.Point(288, 225);
            this.rEDITLINK.Name = "rEDITLINK";
            this.rEDITLINK.Size = new System.Drawing.Size(265, 228);
            this.rEDITLINK.TabIndex = 5;
            this.rEDITLINK.Text = resources.GetString("rEDITLINK.Text");
            // 
            // rMANAGE
            // 
            this.rMANAGE.Location = new System.Drawing.Point(571, 225);
            this.rMANAGE.Name = "rMANAGE";
            this.rMANAGE.Size = new System.Drawing.Size(262, 218);
            this.rMANAGE.TabIndex = 6;
            this.rMANAGE.Text = resources.GetString("rMANAGE.Text");
            // 
            // help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 541);
            this.Controls.Add(this.rMANAGE);
            this.Controls.Add(this.rEDITLINK);
            this.Controls.Add(this.rEDIT);
            this.Controls.Add(this.rSIG);
            this.Controls.Add(this.rUTIL);
            this.Controls.Add(this.rFILE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "help";
            this.Text = "help";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox rFILE;
        private System.Windows.Forms.RichTextBox rUTIL;
        private System.Windows.Forms.RichTextBox rSIG;
        private System.Windows.Forms.RichTextBox rEDIT;
        private System.Windows.Forms.RichTextBox rEDITLINK;
        private System.Windows.Forms.RichTextBox rMANAGE;
    }
}