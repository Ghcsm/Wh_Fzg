namespace CsmImgOcr
{
    partial class FrmImgOcr
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImgOcr));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butImgPath = new DevComponents.DotNetBar.ButtonX();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.butStart = new DevComponents.DotNetBar.ButtonX();
            this.butOcr = new DevComponents.DotNetBar.ButtonX();
            this.chkOcr = new System.Windows.Forms.CheckBox();
            this.ImgView = new Leadtools.Controls.ImageViewer();
            this.gr.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gr2.SuspendLayout();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(795, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr
            // 
            this.gr.CanvasColor = System.Drawing.SystemColors.Control;
            this.gr.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gr.Controls.Add(this.splitContainer1);
            this.gr.Controls.Add(this.toolStrip2);
            this.gr.DisabledBackColor = System.Drawing.Color.Empty;
            this.gr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr.Location = new System.Drawing.Point(0, 25);
            this.gr.Name = "gr";
            this.gr.Size = new System.Drawing.Size(795, 493);
            // 
            // 
            // 
            this.gr.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gr.Style.BackColorGradientAngle = 90;
            this.gr.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gr.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr.Style.BorderBottomWidth = 1;
            this.gr.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gr.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr.Style.BorderLeftWidth = 1;
            this.gr.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr.Style.BorderRightWidth = 1;
            this.gr.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr.Style.BorderTopWidth = 1;
            this.gr.Style.CornerDiameter = 4;
            this.gr.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gr.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gr.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gr.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gr.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gr.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gr.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(789, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.toolStripButton1.Image = global::CsmImgOcr.Properties.Resources._2;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(74, 24);
            this.toolStripButton1.Text = "选择源";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gr1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gr2);
            this.splitContainer1.Size = new System.Drawing.Size(789, 460);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 2;
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Controls.Add(this.chkOcr);
            this.gr2.Controls.Add(this.butOcr);
            this.gr2.Controls.Add(this.butStart);
            this.gr2.Controls.Add(this.textBox3);
            this.gr2.Controls.Add(this.textBox2);
            this.gr2.Controls.Add(this.label3);
            this.gr2.Controls.Add(this.label2);
            this.gr2.Controls.Add(this.butImgPath);
            this.gr2.Controls.Add(this.textBox1);
            this.gr2.Controls.Add(this.label1);
            this.gr2.Location = new System.Drawing.Point(3, 16);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(368, 441);
            this.gr2.TabIndex = 0;
            this.gr2.TabStop = false;
            this.gr2.Text = "配置信息";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(222, 131);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(109, 21);
            this.textBox3.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(62, 131);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "卷号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "盒号:";
            // 
            // butImgPath
            // 
            this.butImgPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butImgPath.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butImgPath.Location = new System.Drawing.Point(304, 57);
            this.butImgPath.Name = "butImgPath";
            this.butImgPath.Size = new System.Drawing.Size(38, 23);
            this.butImgPath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butImgPath.TabIndex = 8;
            this.butImgPath.TabStop = false;
            this.butImgPath.Text = "...";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(183, 21);
            this.textBox1.TabIndex = 7;
            this.textBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "本地图像路径：";
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Controls.Add(this.ImgView);
            this.gr1.Location = new System.Drawing.Point(0, 16);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(408, 441);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            this.gr1.Text = "信息采集";
            // 
            // butStart
            // 
            this.butStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butStart.Location = new System.Drawing.Point(44, 278);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(97, 58);
            this.butStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butStart.TabIndex = 3;
            this.butStart.Text = "采集";
            // 
            // butOcr
            // 
            this.butOcr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butOcr.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butOcr.Location = new System.Drawing.Point(193, 278);
            this.butOcr.Name = "butOcr";
            this.butOcr.Size = new System.Drawing.Size(99, 58);
            this.butOcr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butOcr.TabIndex = 4;
            this.butOcr.Text = "Ocr识别";
            // 
            // chkOcr
            // 
            this.chkOcr.AutoSize = true;
            this.chkOcr.Location = new System.Drawing.Point(34, 215);
            this.chkOcr.Name = "chkOcr";
            this.chkOcr.Size = new System.Drawing.Size(150, 16);
            this.chkOcr.TabIndex = 5;
            this.chkOcr.Text = "采集完成-后台自动识别";
            this.chkOcr.UseVisualStyleBackColor = true;
            // 
            // ImgView
            // 
            this.ImgView.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ImgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImgView.IsSyncSource = true;
            this.ImgView.IsSyncTarget = true;
            this.ImgView.ItemPadding = new System.Windows.Forms.Padding(1);
            this.ImgView.Location = new System.Drawing.Point(3, 17);
            this.ImgView.Name = "ImgView";
            this.ImgView.Size = new System.Drawing.Size(402, 421);
            this.ImgView.TabIndex = 0;
            // 
            // FrmImgOcr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 518);
            this.Controls.Add(this.gr);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmImgOcr";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像识别";
            this.Load += new System.EventHandler(this.FrmImgOcr_Load);
            this.gr.ResumeLayout(false);
            this.gr.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gr2.ResumeLayout(false);
            this.gr2.PerformLayout();
            this.gr1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.Controls.GroupPanel gr;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX butImgPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX butStart;
        private DevComponents.DotNetBar.ButtonX butOcr;
        private System.Windows.Forms.CheckBox chkOcr;
        private Leadtools.Controls.ImageViewer ImgView;
    }
}

