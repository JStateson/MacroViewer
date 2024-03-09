namespace MacroViewer
{
    partial class ShowErrors
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
            this.lbMacNames = new System.Windows.Forms.ListBox();
            this.tbError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbMacNames
            // 
            this.lbMacNames.FormattingEnabled = true;
            this.lbMacNames.Location = new System.Drawing.Point(22, 56);
            this.lbMacNames.Name = "lbMacNames";
            this.lbMacNames.Size = new System.Drawing.Size(215, 368);
            this.lbMacNames.TabIndex = 0;
            this.lbMacNames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMacNames_MouseClick);
            this.lbMacNames.SelectedIndexChanged += new System.EventHandler(this.lbMacNames_SelectedIndexChanged);
            // 
            // tbError
            // 
            this.tbError.Location = new System.Drawing.Point(316, 56);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.Size = new System.Drawing.Size(451, 368);
            this.tbError.TabIndex = 1;
            // 
            // ShowErrors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbError);
            this.Controls.Add(this.lbMacNames);
            this.Name = "ShowErrors";
            this.Text = "Macro Errors";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMacNames;
        private System.Windows.Forms.TextBox tbError;
    }
}