namespace WareHouse
{
    partial class StoreImg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreImg));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolsImgOK = new System.Windows.Forms.ToolStripButton();
            this.toolsImgNo = new System.Windows.Forms.ToolStripButton();
            this.toolsPrivePages = new System.Windows.Forms.ToolStripButton();
            this.toolsNextPages = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsBigPages = new System.Windows.Forms.ToolStripButton();
            this.toolsSmallPages = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsRoate = new System.Windows.Forms.ToolStripButton();
            this.toolsPrintPages = new System.Windows.Forms.ToolStripButton();
            this.txtPage1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtPages2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsColse = new System.Windows.Forms.ToolStripButton();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolslabCheck = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslab_PagesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslab_PagesCrre = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslab_scanuser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslab_Indexuser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslab_Checkuser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsImgOK,
            this.toolsImgNo,
            this.toolsPrivePages,
            this.toolsNextPages,
            this.toolStripSeparator1,
            this.toolsBigPages,
            this.toolsSmallPages,
            this.toolStripSeparator2,
            this.toolsRoate,
            this.toolsPrintPages,
            this.txtPage1,
            this.toolStripLabel1,
            this.txtPages2,
            this.toolStripSeparator3,
            this.toolsColse});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1166, 28);
            this.toolStrip1.TabIndex = 1;
            // 
            // toolsImgOK
            // 
            this.toolsImgOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolsImgOK.Image = ((System.Drawing.Image)(resources.GetObject("toolsImgOK.Image")));
            this.toolsImgOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsImgOK.Name = "toolsImgOK";
            this.toolsImgOK.Size = new System.Drawing.Size(62, 25);
            this.toolsImgOK.Text = "通过";
            this.toolsImgOK.ToolTipText = "验收通过";
            this.toolsImgOK.Click += new System.EventHandler(this.toolsImgOK_Click);
            // 
            // toolsImgNo
            // 
            this.toolsImgNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolsImgNo.Image = ((System.Drawing.Image)(resources.GetObject("toolsImgNo.Image")));
            this.toolsImgNo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsImgNo.Name = "toolsImgNo";
            this.toolsImgNo.Size = new System.Drawing.Size(62, 25);
            this.toolsImgNo.Text = "否决";
            this.toolsImgNo.ToolTipText = "验收否决";
            this.toolsImgNo.Click += new System.EventHandler(this.toolsImgNo_Click);
            // 
            // toolsPrivePages
            // 
            this.toolsPrivePages.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsPrivePages.Image = ((System.Drawing.Image)(resources.GetObject("toolsPrivePages.Image")));
            this.toolsPrivePages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsPrivePages.Name = "toolsPrivePages";
            this.toolsPrivePages.Size = new System.Drawing.Size(62, 25);
            this.toolsPrivePages.Text = "前翻";
            this.toolsPrivePages.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolsPrivePages.Click += new System.EventHandler(this.toolsPrivePages_Click);
            // 
            // toolsNextPages
            // 
            this.toolsNextPages.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsNextPages.Image = ((System.Drawing.Image)(resources.GetObject("toolsNextPages.Image")));
            this.toolsNextPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsNextPages.Name = "toolsNextPages";
            this.toolsNextPages.Size = new System.Drawing.Size(62, 25);
            this.toolsNextPages.Text = "后翻";
            this.toolsNextPages.Click += new System.EventHandler(this.toolsNextPages_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolsBigPages
            // 
            this.toolsBigPages.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsBigPages.Image = ((System.Drawing.Image)(resources.GetObject("toolsBigPages.Image")));
            this.toolsBigPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsBigPages.Name = "toolsBigPages";
            this.toolsBigPages.Size = new System.Drawing.Size(62, 25);
            this.toolsBigPages.Text = "放大";
            this.toolsBigPages.Click += new System.EventHandler(this.toolsBigPages_Click);
            // 
            // toolsSmallPages
            // 
            this.toolsSmallPages.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsSmallPages.Image = ((System.Drawing.Image)(resources.GetObject("toolsSmallPages.Image")));
            this.toolsSmallPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsSmallPages.Name = "toolsSmallPages";
            this.toolsSmallPages.Size = new System.Drawing.Size(62, 25);
            this.toolsSmallPages.Text = "缩小";
            this.toolsSmallPages.Click += new System.EventHandler(this.toolsSmallPages_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolsRoate
            // 
            this.toolsRoate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsRoate.Image = ((System.Drawing.Image)(resources.GetObject("toolsRoate.Image")));
            this.toolsRoate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsRoate.Name = "toolsRoate";
            this.toolsRoate.Size = new System.Drawing.Size(62, 25);
            this.toolsRoate.Text = "旋转";
            this.toolsRoate.Click += new System.EventHandler(this.toolsRoate_Click);
            // 
            // toolsPrintPages
            // 
            this.toolsPrintPages.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsPrintPages.Image = ((System.Drawing.Image)(resources.GetObject("toolsPrintPages.Image")));
            this.toolsPrintPages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsPrintPages.Name = "toolsPrintPages";
            this.toolsPrintPages.Size = new System.Drawing.Size(62, 25);
            this.toolsPrintPages.Text = "打印";
            this.toolsPrintPages.Click += new System.EventHandler(this.toolsPrintPages_Click);
            // 
            // txtPage1
            // 
            this.txtPage1.Name = "txtPage1";
            this.txtPage1.Size = new System.Drawing.Size(100, 28);
            this.txtPage1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPage1_KeyPress);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(18, 25);
            this.toolStripLabel1.Text = "--";
            // 
            // txtPages2
            // 
            this.txtPages2.Name = "txtPages2";
            this.txtPages2.Size = new System.Drawing.Size(100, 28);
            this.txtPages2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPages2_KeyPress);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // toolsColse
            // 
            this.toolsColse.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolsColse.Image = ((System.Drawing.Image)(resources.GetObject("toolsColse.Image")));
            this.toolsColse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsColse.Name = "toolsColse";
            this.toolsColse.Size = new System.Drawing.Size(62, 25);
            this.toolsColse.Text = "退出";
            this.toolsColse.Click += new System.EventHandler(this.toolsColse_Click);
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Location = new System.Drawing.Point(0, 28);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(1166, 485);
            this.gr1.TabIndex = 2;
            this.gr1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolslabCheck,
            this.toolslab_PagesCount,
            this.toolslab_PagesCrre,
            this.toolslab_scanuser,
            this.toolslab_Indexuser,
            this.toolslab_Checkuser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 513);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1166, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolslabCheck
            // 
            this.toolslabCheck.ForeColor = System.Drawing.Color.Red;
            this.toolslabCheck.Name = "toolslabCheck";
            this.toolslabCheck.Size = new System.Drawing.Size(59, 17);
            this.toolslabCheck.Text = "验收状态:";
            // 
            // toolslab_PagesCount
            // 
            this.toolslab_PagesCount.Name = "toolslab_PagesCount";
            this.toolslab_PagesCount.Size = new System.Drawing.Size(39, 17);
            this.toolslab_PagesCount.Text = "共0页";
            // 
            // toolslab_PagesCrre
            // 
            this.toolslab_PagesCrre.Name = "toolslab_PagesCrre";
            this.toolslab_PagesCrre.Size = new System.Drawing.Size(39, 17);
            this.toolslab_PagesCrre.Text = "第0页";
            // 
            // toolslab_scanuser
            // 
            this.toolslab_scanuser.Name = "toolslab_scanuser";
            this.toolslab_scanuser.Size = new System.Drawing.Size(35, 17);
            this.toolslab_scanuser.Text = "扫描:";
            // 
            // toolslab_Indexuser
            // 
            this.toolslab_Indexuser.Name = "toolslab_Indexuser";
            this.toolslab_Indexuser.Size = new System.Drawing.Size(35, 17);
            this.toolslab_Indexuser.Text = "排序:";
            // 
            // toolslab_Checkuser
            // 
            this.toolslab_Checkuser.Name = "toolslab_Checkuser";
            this.toolslab_Checkuser.Size = new System.Drawing.Size(35, 17);
            this.toolslab_Checkuser.Text = "质检:";
            // 
            // StoreImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 535);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StoreImg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像浏览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StoreImg_FormClosing);
            this.Load += new System.EventHandler(this.StoreImg_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolsPrivePages;
        private System.Windows.Forms.ToolStripButton toolsNextPages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolsBigPages;
        private System.Windows.Forms.ToolStripButton toolsSmallPages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolsRoate;
        private System.Windows.Forms.ToolStripButton toolsPrintPages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolsColse;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.ToolStripTextBox txtPage1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtPages2;
        private System.Windows.Forms.ToolStripButton toolsImgOK;
        private System.Windows.Forms.ToolStripButton toolsImgNo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolslabCheck;
        private System.Windows.Forms.ToolStripStatusLabel toolslab_PagesCount;
        private System.Windows.Forms.ToolStripStatusLabel toolslab_PagesCrre;
        private System.Windows.Forms.ToolStripStatusLabel toolslab_scanuser;
        private System.Windows.Forms.ToolStripStatusLabel toolslab_Indexuser;
        private System.Windows.Forms.ToolStripStatusLabel toolslab_Checkuser;
    }
}