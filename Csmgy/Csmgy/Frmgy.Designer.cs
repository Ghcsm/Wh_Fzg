namespace Csmgy
{
    partial class Frmgy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lab_kh;
        private System.Windows.Forms.Label lab_about;
        private System.Windows.Forms.Label lab_soft;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmgy));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lab_ab = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_about = new System.Windows.Forms.Label();
            this.lab_soft = new System.Windows.Forms.Label();
            this.lab_kh = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lab_ab);
            this.groupBox1.Controls.Add(this.ID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lab_about);
            this.groupBox1.Controls.Add(this.lab_soft);
            this.groupBox1.Controls.Add(this.lab_kh);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lab_ab
            // 
            this.lab_ab.AutoSize = true;
            this.lab_ab.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ab.Location = new System.Drawing.Point(210, 136);
            this.lab_ab.Name = "lab_ab";
            this.lab_ab.Size = new System.Drawing.Size(0, 15);
            this.lab_ab.TabIndex = 6;
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(16, 171);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(23, 12);
            this.ID.TabIndex = 5;
            this.ID.Text = "ID:";
            this.ID.Click += new System.EventHandler(this.ID_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(93, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 4;
            // 
            // lab_about
            // 
            this.lab_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_about.Location = new System.Drawing.Point(140, 194);
            this.lab_about.Name = "lab_about";
            this.lab_about.Size = new System.Drawing.Size(173, 23);
            this.lab_about.TabIndex = 3;
            this.lab_about.Text = "版权所有(C)：2011-2015 ";
            // 
            // lab_soft
            // 
            this.lab_soft.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_soft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_soft.Location = new System.Drawing.Point(33, 132);
            this.lab_soft.Name = "lab_soft";
            this.lab_soft.Size = new System.Drawing.Size(280, 23);
            this.lab_soft.TabIndex = 2;
            this.lab_soft.Text = "XXX档案管理系统";
            // 
            // lab_kh
            // 
            this.lab_kh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_kh.Location = new System.Drawing.Point(165, 46);
            this.lab_kh.Name = "lab_kh";
            this.lab_kh.Size = new System.Drawing.Size(129, 73);
            this.lab_kh.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Frmgy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(357, 246);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frmgy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于";
            this.Load += new System.EventHandler(this.Frmgy_Load);
            this.Shown += new System.EventHandler(this.FrmgyShown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ID;
        public System.Windows.Forms.Label lab_ab;
    }
}