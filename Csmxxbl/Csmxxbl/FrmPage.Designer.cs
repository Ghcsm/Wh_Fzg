namespace Csmxxbl
{
    partial class FrmPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPage));
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbPage = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.butSavePage = new DevComponents.DotNetBar.ButtonX();
            this.butDel = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.butDel);
            this.groupPanel1.Controls.Add(this.butSavePage);
            this.groupPanel1.Controls.Add(this.txtPage);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.Controls.Add(this.groupBox1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(379, 192);
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
            this.groupPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lbPage);
            this.groupBox1.Location = new System.Drawing.Point(18, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 171);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "页码范围";
            // 
            // lbPage
            // 
            this.lbPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPage.FormattingEnabled = true;
            this.lbPage.HorizontalScrollbar = true;
            this.lbPage.ItemHeight = 12;
            this.lbPage.Location = new System.Drawing.Point(3, 17);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(92, 151);
            this.lbPage.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(144, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "带-页码";
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(142, 55);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(208, 21);
            this.txtPage.TabIndex = 1;
            this.txtPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPage_KeyPress);
            // 
            // butSavePage
            // 
            this.butSavePage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butSavePage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butSavePage.Location = new System.Drawing.Point(143, 114);
            this.butSavePage.Name = "butSavePage";
            this.butSavePage.Size = new System.Drawing.Size(75, 35);
            this.butSavePage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSavePage.TabIndex = 2;
            this.butSavePage.Text = "添加";
            this.butSavePage.Click += new System.EventHandler(this.butSavePage_Click);
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(266, 114);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(75, 35);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 4;
            this.butDel.Text = "删除";
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // FrmPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 192);
            this.Controls.Add(this.groupPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "页码补录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPage_FormClosing);
            this.Shown += new System.EventHandler(this.FrmPage_Shown);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbPage;
        private DevComponents.DotNetBar.ButtonX butDel;
        private DevComponents.DotNetBar.ButtonX butSavePage;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label label1;
    }
}