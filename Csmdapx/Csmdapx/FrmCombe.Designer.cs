namespace Csmdapx
{
    partial class FrmCombe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCombe));
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.pic2 = new System.Windows.Forms.PictureBox();
            this.butImgsave = new DevComponents.DotNetBar.ButtonX();
            this.butImgcombe = new DevComponents.DotNetBar.ButtonX();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.pict1 = new System.Windows.Forms.PictureBox();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.butImgLoad = new DevComponents.DotNetBar.ButtonX();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbImgPage = new System.Windows.Forms.ListBox();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            this.gr2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict1)).BeginInit();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.pic2);
            this.groupPanel1.Controls.Add(this.butImgsave);
            this.groupPanel1.Controls.Add(this.butImgcombe);
            this.groupPanel1.Controls.Add(this.gr2);
            this.groupPanel1.Controls.Add(this.gr1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(762, 497);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 1;
            // 
            // pic2
            // 
            this.pic2.BackColor = System.Drawing.Color.Transparent;
            this.pic2.Image = global::Csmdapx.Properties.Resources._0_1309168158c9z6;
            this.pic2.Location = new System.Drawing.Point(431, 32);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(163, 21);
            this.pic2.TabIndex = 4;
            this.pic2.TabStop = false;
            this.pic2.Visible = false;
            // 
            // butImgsave
            // 
            this.butImgsave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butImgsave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butImgsave.Location = new System.Drawing.Point(633, 10);
            this.butImgsave.Name = "butImgsave";
            this.butImgsave.Size = new System.Drawing.Size(77, 43);
            this.butImgsave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butImgsave.TabIndex = 3;
            this.butImgsave.Text = "保存";
            this.butImgsave.Click += new System.EventHandler(this.butImgsave_Click);
            // 
            // butImgcombe
            // 
            this.butImgcombe.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butImgcombe.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butImgcombe.Location = new System.Drawing.Point(319, 10);
            this.butImgcombe.Name = "butImgcombe";
            this.butImgcombe.Size = new System.Drawing.Size(77, 43);
            this.butImgcombe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butImgcombe.TabIndex = 2;
            this.butImgcombe.Text = "拼接";
            this.butImgcombe.Click += new System.EventHandler(this.butImgcombe_Click);
            // 
            // gr2
            // 
            this.gr2.BackColor = System.Drawing.Color.Transparent;
            this.gr2.Controls.Add(this.pict1);
            this.gr2.Location = new System.Drawing.Point(238, 68);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(509, 389);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            this.gr2.Text = "拼接图像";
            // 
            // pict1
            // 
            this.pict1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pict1.Location = new System.Drawing.Point(3, 17);
            this.pict1.Name = "pict1";
            this.pict1.Size = new System.Drawing.Size(503, 369);
            this.pict1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pict1.TabIndex = 0;
            this.pict1.TabStop = false;
            // 
            // gr1
            // 
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Controls.Add(this.butImgLoad);
            this.gr1.Controls.Add(this.txtPage);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Controls.Add(this.lbImgPage);
            this.gr1.Location = new System.Drawing.Point(9, 15);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(223, 442);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            this.gr1.Text = "加载图像";
            // 
            // butImgLoad
            // 
            this.butImgLoad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butImgLoad.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butImgLoad.Location = new System.Drawing.Point(119, 43);
            this.butImgLoad.Name = "butImgLoad";
            this.butImgLoad.Size = new System.Drawing.Size(75, 33);
            this.butImgLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butImgLoad.TabIndex = 4;
            this.butImgLoad.Text = "加载";
            this.butImgLoad.Click += new System.EventHandler(this.butImgLoad_Click);
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(14, 53);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(83, 21);
            this.txtPage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "原图像页码:";
            // 
            // lbImgPage
            // 
            this.lbImgPage.FormattingEnabled = true;
            this.lbImgPage.ItemHeight = 12;
            this.lbImgPage.Location = new System.Drawing.Point(6, 85);
            this.lbImgPage.Name = "lbImgPage";
            this.lbImgPage.Size = new System.Drawing.Size(211, 352);
            this.lbImgPage.TabIndex = 1;
            // 
            // FrmCombe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 497);
            this.Controls.Add(this.groupPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCombe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像拼接";
            this.Shown += new System.EventHandler(this.FrmCombe_Shown);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            this.gr2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pict1)).EndInit();
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbImgPage;
        private DevComponents.DotNetBar.ButtonX butImgLoad;
        private System.Windows.Forms.GroupBox gr2;
        private DevComponents.DotNetBar.ButtonX butImgsave;
        private DevComponents.DotNetBar.ButtonX butImgcombe;
        private System.Windows.Forms.PictureBox pict1;
        private System.Windows.Forms.PictureBox pic2;
    }
}