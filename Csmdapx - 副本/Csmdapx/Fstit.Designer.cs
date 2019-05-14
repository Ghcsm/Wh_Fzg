namespace Csmdapx
{
    partial class Fstit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fstit));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.but_sizex = new System.Windows.Forms.Button();
            this.but_sized = new System.Windows.Forms.Button();
            this.but_Save = new System.Windows.Forms.Button();
            this.but_Duiq = new System.Windows.Forms.Button();
            this.but_add = new System.Windows.Forms.Button();
            this.txt_page = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic2 = new System.Windows.Forms.PictureBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.but_sizex);
            this.groupBox1.Controls.Add(this.but_sized);
            this.groupBox1.Controls.Add(this.but_Save);
            this.groupBox1.Controls.Add(this.but_Duiq);
            this.groupBox1.Controls.Add(this.but_add);
            this.groupBox1.Controls.Add(this.txt_page);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(843, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // but_sizex
            // 
            this.but_sizex.Location = new System.Drawing.Point(560, 17);
            this.but_sizex.Name = "but_sizex";
            this.but_sizex.Size = new System.Drawing.Size(75, 36);
            this.but_sizex.TabIndex = 6;
            this.but_sizex.Text = "缩小";
            this.but_sizex.UseVisualStyleBackColor = true;
            this.but_sizex.Click += new System.EventHandler(this.but_sizex_Click);
            // 
            // but_sized
            // 
            this.but_sized.Location = new System.Drawing.Point(420, 18);
            this.but_sized.Name = "but_sized";
            this.but_sized.Size = new System.Drawing.Size(75, 36);
            this.but_sized.TabIndex = 5;
            this.but_sized.Text = "放大";
            this.but_sized.UseVisualStyleBackColor = true;
            this.but_sized.Click += new System.EventHandler(this.but_sized_Click);
            // 
            // but_Save
            // 
            this.but_Save.Location = new System.Drawing.Point(724, 18);
            this.but_Save.Name = "but_Save";
            this.but_Save.Size = new System.Drawing.Size(75, 36);
            this.but_Save.TabIndex = 4;
            this.but_Save.Text = "保存";
            this.but_Save.UseVisualStyleBackColor = true;
            this.but_Save.Click += new System.EventHandler(this.but_Save_Click);
            // 
            // but_Duiq
            // 
            this.but_Duiq.Location = new System.Drawing.Point(292, 20);
            this.but_Duiq.Name = "but_Duiq";
            this.but_Duiq.Size = new System.Drawing.Size(75, 36);
            this.but_Duiq.TabIndex = 3;
            this.but_Duiq.Text = "自动对齐";
            this.but_Duiq.UseVisualStyleBackColor = true;
            this.but_Duiq.Click += new System.EventHandler(this.but_Duiq_Click);
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(161, 20);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(75, 36);
            this.but_add.TabIndex = 2;
            this.but_add.Text = "加载";
            this.but_add.UseVisualStyleBackColor = true;
            this.but_add.Click += new System.EventHandler(this.but_add_Click);
            // 
            // txt_page
            // 
            this.txt_page.Location = new System.Drawing.Point(97, 25);
            this.txt_page.Name = "txt_page";
            this.txt_page.Size = new System.Drawing.Size(43, 21);
            this.txt_page.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图像原页码:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.pic2);
            this.panel1.Controls.Add(this.pic1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 406);
            this.panel1.TabIndex = 1;
            // 
            // pic2
            // 
            this.pic2.Location = new System.Drawing.Point(384, 24);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(328, 346);
            this.pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic2.TabIndex = 1;
            this.pic2.TabStop = false;
            this.pic2.Visible = false;
            this.pic2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic2_MouseDown);
            this.pic2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic2_MouseMove);
            this.pic2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic2_MouseUp);
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(24, 24);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(328, 346);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 0;
            this.pic1.TabStop = false;
            this.pic1.Visible = false;
            this.pic1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseDown);
            this.pic1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseMove);
            this.pic1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic1_MouseUp);
            // 
            // Fstit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(843, 465);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Fstit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像拼接";
            this.Shown += new System.EventHandler(this.Fstit_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_page;
        private System.Windows.Forms.Button but_add;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button but_Duiq;
        private System.Windows.Forms.Button but_Save;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.PictureBox pic2;
        private System.Windows.Forms.Button but_sizex;
        private System.Windows.Forms.Button but_sized;
    }
}