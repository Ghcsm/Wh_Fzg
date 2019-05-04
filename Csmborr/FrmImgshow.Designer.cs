namespace Csmborr
{
    partial class FrmImgshow
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
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImgshow));
            this.gr = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolbutqian = new System.Windows.Forms.ToolStripButton();
            this.toolbuthou = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbutmax = new System.Windows.Forms.ToolStripButton();
            this.toolbutsize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbutrote = new System.Windows.Forms.ToolStripButton();
            this.toolbutprint = new System.Windows.Forms.ToolStripButton();
            this.tooltxtpage1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tooltxtpage2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolbutclose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr
            // 
            this.gr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr.Location = new System.Drawing.Point(0, 28);
            this.gr.Name = "gr";
            this.gr.Size = new System.Drawing.Size(867, 415);
            this.gr.TabIndex = 0;
            this.gr.TabStop = false;
            this.gr.Text = "图像查看";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbutqian,
            this.toolbuthou,
            this.toolStripSeparator1,
            this.toolbutmax,
            this.toolbutsize,
            this.toolStripSeparator2,
            this.toolbutrote,
            this.toolbutprint,
            this.tooltxtpage1,
            this.toolStripLabel1,
            this.tooltxtpage2,
            this.toolbutclose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(867, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolbutqian
            // 
            this.toolbutqian.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbutqian.Image = ((System.Drawing.Image)(resources.GetObject("toolbutqian.Image")));
            this.toolbutqian.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbutqian.Name = "toolbutqian";
            this.toolbutqian.Size = new System.Drawing.Size(62, 25);
            this.toolbutqian.Text = "前翻";
            this.toolbutqian.Click += new System.EventHandler(this.toolbutqian_Click);
            // 
            // toolbuthou
            // 
            this.toolbuthou.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbuthou.Image = ((System.Drawing.Image)(resources.GetObject("toolbuthou.Image")));
            this.toolbuthou.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbuthou.Name = "toolbuthou";
            this.toolbuthou.Size = new System.Drawing.Size(62, 25);
            this.toolbuthou.Text = "后翻";
            this.toolbuthou.Click += new System.EventHandler(this.toolbuthou_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolbutmax
            // 
            this.toolbutmax.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbutmax.Image = ((System.Drawing.Image)(resources.GetObject("toolbutmax.Image")));
            this.toolbutmax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbutmax.Name = "toolbutmax";
            this.toolbutmax.Size = new System.Drawing.Size(62, 25);
            this.toolbutmax.Text = "放大";
            this.toolbutmax.Click += new System.EventHandler(this.toolbutmax_Click);
            // 
            // toolbutsize
            // 
            this.toolbutsize.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbutsize.Image = ((System.Drawing.Image)(resources.GetObject("toolbutsize.Image")));
            this.toolbutsize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbutsize.Name = "toolbutsize";
            this.toolbutsize.Size = new System.Drawing.Size(62, 25);
            this.toolbutsize.Text = "缩小";
            this.toolbutsize.Click += new System.EventHandler(this.toolbutsize_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolbutrote
            // 
            this.toolbutrote.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbutrote.Image = ((System.Drawing.Image)(resources.GetObject("toolbutrote.Image")));
            this.toolbutrote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbutrote.Name = "toolbutrote";
            this.toolbutrote.Size = new System.Drawing.Size(62, 25);
            this.toolbutrote.Text = "旋转";
            this.toolbutrote.Click += new System.EventHandler(this.toolbutrote_Click);
            // 
            // toolbutprint
            // 
            this.toolbutprint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbutprint.Image = ((System.Drawing.Image)(resources.GetObject("toolbutprint.Image")));
            this.toolbutprint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbutprint.Name = "toolbutprint";
            this.toolbutprint.Size = new System.Drawing.Size(62, 25);
            this.toolbutprint.Text = "打印";
            this.toolbutprint.Click += new System.EventHandler(this.toolbutprint_Click);
            // 
            // tooltxtpage1
            // 
            this.tooltxtpage1.Name = "tooltxtpage1";
            this.tooltxtpage1.Size = new System.Drawing.Size(100, 28);
            this.tooltxtpage1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tooltxtpage1_KeyPress);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(18, 25);
            this.toolStripLabel1.Text = "--";
            // 
            // tooltxtpage2
            // 
            this.tooltxtpage2.Name = "tooltxtpage2";
            this.tooltxtpage2.Size = new System.Drawing.Size(100, 28);
            this.tooltxtpage2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tooltxtpage2_KeyPress);
            // 
            // toolbutclose
            // 
            this.toolbutclose.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolbutclose.Image = ((System.Drawing.Image)(resources.GetObject("toolbutclose.Image")));
            this.toolbutclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbutclose.Name = "toolbutclose";
            this.toolbutclose.Size = new System.Drawing.Size(62, 25);
            this.toolbutclose.Text = "退出";
            this.toolbutclose.Click += new System.EventHandler(this.toolbutclose_Click);
            // 
            // FrmImgshow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(867, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gr);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImgshow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmImgshow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImgshow_FormClosing);
            this.Load += new System.EventHandler(this.FrmImgshow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gr;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolbutqian;
        private System.Windows.Forms.ToolStripButton toolbuthou;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolbutmax;
        private System.Windows.Forms.ToolStripButton toolbutsize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolbutrote;
        private System.Windows.Forms.ToolStripButton toolbutprint;
        private System.Windows.Forms.ToolStripTextBox tooltxtpage1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tooltxtpage2;
        private System.Windows.Forms.ToolStripButton toolbutclose;
    }
}