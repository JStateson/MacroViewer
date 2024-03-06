namespace MacroViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDiags = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgManage = new System.Windows.Forms.DataGridView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.gbDiags.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDiags
            // 
            this.gbDiags.Controls.Add(this.button1);
            this.gbDiags.Controls.Add(this.btnView);
            this.gbDiags.Controls.Add(this.btnDelAll);
            this.gbDiags.Location = new System.Drawing.Point(28, 313);
            this.gbDiags.Name = "gbDiags";
            this.gbDiags.Size = new System.Drawing.Size(129, 151);
            this.gbDiags.TabIndex = 1;
            this.gbDiags.TabStop = false;
            this.gbDiags.Text = "Clean local files";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(16, 68);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(99, 23);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "View Folder";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
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
            this.groupBox1.Controls.Add(this.dgManage);
            this.groupBox1.Controls.Add(this.gbDiags);
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
            this.dgManage.Location = new System.Drawing.Point(17, 23);
            this.dgManage.MultiSelect = false;
            this.dgManage.Name = "dgManage";
            this.dgManage.ReadOnly = true;
            this.dgManage.RowHeadersWidth = 16;
            this.dgManage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgManage.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgManage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgManage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgManage.ShowCellToolTips = false;
            this.dgManage.Size = new System.Drawing.Size(387, 250);
            this.dgManage.TabIndex = 4;
            this.dgManage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgManage_CellContentClick);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(506, 119);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(453, 381);
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLogIn.Location = new System.Drawing.Point(529, 48);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(100, 38);
            this.btnLogIn.TabIndex = 5;
            this.btnLogIn.Text = "Login";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // ManageMacros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 523);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.groupBox1);
            this.Name = "ManageMacros";
            this.Text = "ManageMacros";
            this.gbDiags.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDiags;
        private System.Windows.Forms.Button btnDelAll;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgManage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnLogIn;
    }
}