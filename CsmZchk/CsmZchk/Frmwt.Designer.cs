namespace CsmZchk
{
    partial class Frmwt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmwt));
            this.but_zuo = new System.Windows.Forms.Button();
            this.but_you = new System.Windows.Forms.Button();
            this.but_ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imgview = new Leadtools.Controls.ImageViewer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsmem_left = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsmem_right = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsmem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // but_zuo
            // 
            this.but_zuo.Location = new System.Drawing.Point(34, 33);
            this.but_zuo.Name = "but_zuo";
            this.but_zuo.Size = new System.Drawing.Size(75, 31);
            this.but_zuo.TabIndex = 0;
            this.but_zuo.Text = "向左";
            this.but_zuo.UseVisualStyleBackColor = true;
            this.but_zuo.Click += new System.EventHandler(this.but_zuo_Click);
            // 
            // but_you
            // 
            this.but_you.Location = new System.Drawing.Point(147, 31);
            this.but_you.Name = "but_you";
            this.but_you.Size = new System.Drawing.Size(75, 33);
            this.but_you.TabIndex = 1;
            this.but_you.Text = "向右";
            this.but_you.UseVisualStyleBackColor = true;
            this.but_you.Click += new System.EventHandler(this.but_you_Click);
            // 
            // but_ok
            // 
            this.but_ok.Location = new System.Drawing.Point(271, 31);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new System.Drawing.Size(75, 35);
            this.but_ok.TabIndex = 2;
            this.but_ok.Text = "确定";
            this.but_ok.UseVisualStyleBackColor = true;
            this.but_ok.Click += new System.EventHandler(this.but_ok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imgview);
            this.groupBox1.Location = new System.Drawing.Point(3, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 338);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // imgview
            // 
            this.imgview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgview.IsSyncSource = true;
            this.imgview.IsSyncTarget = true;
            this.imgview.ItemPadding = new System.Windows.Forms.Padding(1);
            this.imgview.Location = new System.Drawing.Point(3, 17);
            this.imgview.Name = "imgview";
            this.imgview.Size = new System.Drawing.Size(391, 318);
            this.imgview.TabIndex = 0;
            this.imgview.TextVerticalAlignment = Leadtools.Controls.ControlAlignment.Center;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(402, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsmem_left,
            this.toolsmem_right,
            this.toolsmem_save});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // toolsmem_left
            // 
            this.toolsmem_left.Name = "toolsmem_left";
            this.toolsmem_left.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.toolsmem_left.Size = new System.Drawing.Size(167, 22);
            this.toolsmem_left.Text = "向左";
            this.toolsmem_left.Click += new System.EventHandler(this.toolsmem_left_Click);
            // 
            // toolsmem_right
            // 
            this.toolsmem_right.Name = "toolsmem_right";
            this.toolsmem_right.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.toolsmem_right.Size = new System.Drawing.Size(167, 22);
            this.toolsmem_right.Text = "向右";
            this.toolsmem_right.Click += new System.EventHandler(this.toolsmem_right_Click);
            // 
            // toolsmem_save
            // 
            this.toolsmem_save.Name = "toolsmem_save";
            this.toolsmem_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolsmem_save.Size = new System.Drawing.Size(167, 22);
            this.toolsmem_save.Text = "保存";
            this.toolsmem_save.Click += new System.EventHandler(this.toolsmem_save_Click);
            // 
            // Frmwt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 417);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.but_ok);
            this.Controls.Add(this.but_you);
            this.Controls.Add(this.but_zuo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frmwt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像微调";
            this.Shown += new System.EventHandler(this.Frmwt_Shown);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_zuo;
        private System.Windows.Forms.Button but_you;
        private System.Windows.Forms.Button but_ok;
        private System.Windows.Forms.GroupBox groupBox1;
        public Leadtools.Controls.ImageViewer imgview;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsmem_left;
        private System.Windows.Forms.ToolStripMenuItem toolsmem_right;
        private System.Windows.Forms.ToolStripMenuItem toolsmem_save;
    }
}