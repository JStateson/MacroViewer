namespace MacroViewer.sources
{
    partial class EditOldUrls
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
            this.cbMacroList = new System.Windows.Forms.ComboBox();
            this.tbH = new System.Windows.Forms.TextBox();
            this.tbT = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancelExit = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnCancelChg = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnForm = new System.Windows.Forms.Button();
            this.lbChanged = new System.Windows.Forms.Label();
            this.gbText = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClrH = new System.Windows.Forms.Button();
            this.btnCanH = new System.Windows.Forms.Button();
            this.btnCanT = new System.Windows.Forms.Button();
            this.btnClrT = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbText.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMacroList
            // 
            this.cbMacroList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMacroList.FormattingEnabled = true;
            this.cbMacroList.Location = new System.Drawing.Point(15, 36);
            this.cbMacroList.Name = "cbMacroList";
            this.cbMacroList.Size = new System.Drawing.Size(121, 28);
            this.cbMacroList.TabIndex = 0;
            this.cbMacroList.SelectedIndexChanged += new System.EventHandler(this.cbMacroList_SelectedIndexChanged);
            // 
            // tbH
            // 
            this.tbH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbH.Location = new System.Drawing.Point(191, 32);
            this.tbH.Multiline = true;
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(400, 128);
            this.tbH.TabIndex = 1;
            // 
            // tbT
            // 
            this.tbT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbT.Location = new System.Drawing.Point(191, 38);
            this.tbT.Multiline = true;
            this.tbT.Name = "tbT";
            this.tbT.Size = new System.Drawing.Size(400, 137);
            this.tbT.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(164, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "Apply and exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnApplyExit);
            // 
            // btnCancelExit
            // 
            this.btnCancelExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelExit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancelExit.Location = new System.Drawing.Point(164, 253);
            this.btnCancelExit.Name = "btnCancelExit";
            this.btnCancelExit.Size = new System.Drawing.Size(128, 31);
            this.btnCancelExit.TabIndex = 5;
            this.btnCancelExit.Text = "Cancel and exit";
            this.btnCancelExit.UseVisualStyleBackColor = true;
            this.btnCancelExit.Click += new System.EventHandler(this.btnCancelExit_Click);
            // 
            // tbResult
            // 
            this.tbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResult.Location = new System.Drawing.Point(373, 505);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(591, 125);
            this.tbResult.TabIndex = 6;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnTest.Location = new System.Drawing.Point(27, 546);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(132, 31);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "Test Changes";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnCancelChg
            // 
            this.btnCancelChg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelChg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancelChg.Location = new System.Drawing.Point(199, 599);
            this.btnCancelChg.Name = "btnCancelChg";
            this.btnCancelChg.Size = new System.Drawing.Size(132, 31);
            this.btnCancelChg.TabIndex = 8;
            this.btnCancelChg.Text = "Undo Changes";
            this.btnCancelChg.UseVisualStyleBackColor = true;
            this.btnCancelChg.Click += new System.EventHandler(this.btnCancelChg_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMacroList);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnCancelExit);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 338);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Macro to change";
            // 
            // btnForm
            // 
            this.btnForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForm.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnForm.Location = new System.Drawing.Point(199, 505);
            this.btnForm.Name = "btnForm";
            this.btnForm.Size = new System.Drawing.Size(132, 31);
            this.btnForm.TabIndex = 9;
            this.btnForm.Text = "Form Changes";
            this.btnForm.UseVisualStyleBackColor = true;
            this.btnForm.Click += new System.EventHandler(this.btnForm_Click);
            // 
            // lbChanged
            // 
            this.lbChanged.AutoSize = true;
            this.lbChanged.BackColor = System.Drawing.SystemColors.Control;
            this.lbChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChanged.ForeColor = System.Drawing.Color.Red;
            this.lbChanged.Location = new System.Drawing.Point(609, 467);
            this.lbChanged.Name = "lbChanged";
            this.lbChanged.Size = new System.Drawing.Size(111, 20);
            this.lbChanged.TabIndex = 13;
            this.lbChanged.Text = "Changed URL";
            // 
            // gbText
            // 
            this.gbText.Controls.Add(this.btnCanT);
            this.gbText.Controls.Add(this.btnClrT);
            this.gbText.Controls.Add(this.tbT);
            this.gbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbText.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gbText.Location = new System.Drawing.Point(373, 237);
            this.gbText.Name = "gbText";
            this.gbText.Size = new System.Drawing.Size(610, 199);
            this.gbText.TabIndex = 14;
            this.gbText.TabStop = false;
            this.gbText.Text = "Text (or empty)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCanH);
            this.groupBox3.Controls.Add(this.btnClrH);
            this.groupBox3.Controls.Add(this.tbH);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox3.Location = new System.Drawing.Point(373, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(616, 191);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HREF or Image";
            // 
            // btnClrH
            // 
            this.btnClrH.Location = new System.Drawing.Point(28, 58);
            this.btnClrH.Name = "btnClrH";
            this.btnClrH.Size = new System.Drawing.Size(75, 28);
            this.btnClrH.TabIndex = 2;
            this.btnClrH.Text = "Clear";
            this.btnClrH.UseVisualStyleBackColor = true;
            this.btnClrH.Click += new System.EventHandler(this.btnClrH_Click);
            // 
            // btnCanH
            // 
            this.btnCanH.Location = new System.Drawing.Point(28, 108);
            this.btnCanH.Name = "btnCanH";
            this.btnCanH.Size = new System.Drawing.Size(75, 28);
            this.btnCanH.TabIndex = 3;
            this.btnCanH.Text = "Cancel";
            this.btnCanH.UseVisualStyleBackColor = true;
            this.btnCanH.Click += new System.EventHandler(this.btnCanH_Click);
            // 
            // btnCanT
            // 
            this.btnCanT.Location = new System.Drawing.Point(39, 107);
            this.btnCanT.Name = "btnCanT";
            this.btnCanT.Size = new System.Drawing.Size(75, 28);
            this.btnCanT.TabIndex = 5;
            this.btnCanT.Text = "Cancel";
            this.btnCanT.UseVisualStyleBackColor = true;
            this.btnCanT.Click += new System.EventHandler(this.btnCanT_Click);
            // 
            // btnClrT
            // 
            this.btnClrT.Location = new System.Drawing.Point(39, 57);
            this.btnClrT.Name = "btnClrT";
            this.btnClrT.Size = new System.Drawing.Size(75, 28);
            this.btnClrT.TabIndex = 4;
            this.btnClrT.Text = "Clear";
            this.btnClrT.UseVisualStyleBackColor = true;
            this.btnClrT.Click += new System.EventHandler(this.btnClrT_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.Location = new System.Drawing.Point(199, 556);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 31);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save Change";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditOldUrls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 653);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnForm);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbText);
            this.Controls.Add(this.btnCancelChg);
            this.Controls.Add(this.lbChanged);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.tbResult);
            this.Name = "EditOldUrls";
            this.Text = "EditOldUrls";
            this.groupBox1.ResumeLayout(false);
            this.gbText.ResumeLayout(false);
            this.gbText.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMacroList;
        private System.Windows.Forms.TextBox tbH;
        private System.Windows.Forms.TextBox tbT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancelExit;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnCancelChg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbChanged;
        private System.Windows.Forms.Button btnForm;
        private System.Windows.Forms.GroupBox gbText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClrH;
        private System.Windows.Forms.Button btnCanH;
        private System.Windows.Forms.Button btnCanT;
        private System.Windows.Forms.Button btnClrT;
        private System.Windows.Forms.Button btnSave;
    }
}