﻿namespace MacroViewer
{
    partial class ManageMacros
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDiags = new System.Windows.Forms.GroupBox();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgManage = new System.Windows.Forms.DataGridView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.btnUpdateURL = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gbDiags.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDiags
            // 
            this.gbDiags.Controls.Add(this.btnDelAll);
            this.gbDiags.Location = new System.Drawing.Point(813, 12);
            this.gbDiags.Name = "gbDiags";
            this.gbDiags.Size = new System.Drawing.Size(146, 88);
            this.gbDiags.TabIndex = 1;
            this.gbDiags.TabStop = false;
            this.gbDiags.Text = "Clean local files";
            this.gbDiags.Visible = false;
            // 
            // btnDelAll
            // 
            this.btnDelAll.Location = new System.Drawing.Point(16, 33);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(99, 23);
            this.btnDelAll.TabIndex = 0;
            this.btnDelAll.Text = "Clean All";
            this.toolTip1.SetToolTip(this.btnDelAll, "Remove all .prg and .id files in executable folder");
            this.btnDelAll.UseVisualStyleBackColor = true;
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdateURL);
            this.groupBox1.Controls.Add(this.btnBrowseImage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLogIn);
            this.groupBox1.Controls.Add(this.dgManage);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 486);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Macro List and URL";
            // 
            // dgManage
            // 
            this.dgManage.AllowUserToAddRows = false;
            this.dgManage.AllowUserToDeleteRows = false;
            this.dgManage.AllowUserToResizeRows = false;
            this.dgManage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dgManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgManage.Location = new System.Drawing.Point(17, 70);
            this.dgManage.MultiSelect = false;
            this.dgManage.Name = "dgManage";
            this.dgManage.ReadOnly = true;
            this.dgManage.RowHeadersWidth = 16;
            this.dgManage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgManage.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgManage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgManage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgManage.ShowCellToolTips = false;
            this.dgManage.Size = new System.Drawing.Size(387, 250);
            this.dgManage.TabIndex = 4;
            this.dgManage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgManage_CellContentClick);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(475, 119);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(453, 381);
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLogIn.Location = new System.Drawing.Point(17, 350);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(176, 38);
            this.btnLogIn.TabIndex = 5;
            this.btnLogIn.Text = "Login to your Albums";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select any row below for image";
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseImage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnBrowseImage.Location = new System.Drawing.Point(17, 419);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(176, 38);
            this.btnBrowseImage.TabIndex = 7;
            this.btnBrowseImage.Text = "Browse to image file";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // btnUpdateURL
            // 
            this.btnUpdateURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateURL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnUpdateURL.Location = new System.Drawing.Point(246, 384);
            this.btnUpdateURL.Name = "btnUpdateURL";
            this.btnUpdateURL.Size = new System.Drawing.Size(158, 38);
            this.btnUpdateURL.TabIndex = 8;
            this.btnUpdateURL.Text = "Update Image URL";
            this.btnUpdateURL.UseVisualStyleBackColor = true;
            this.btnUpdateURL.Click += new System.EventHandler(this.btnUpdateURL_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnHelp.Location = new System.Drawing.Point(526, 45);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(169, 38);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "Click for help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // ManageMacros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 523);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.gbDiags);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.Name = "ManageMacros";
            this.Text = "ManageMacros";
            this.gbDiags.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDiags;
        private System.Windows.Forms.Button btnDelAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgManage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateURL;
        private System.Windows.Forms.Button btnHelp;
    }
}