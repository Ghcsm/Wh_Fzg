namespace Csmmldj
{
    partial class FrmLsinfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLsinfo));
            this.gr1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.CanvasColor = System.Drawing.SystemColors.Control;
            this.gr1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gr1.DisabledBackColor = System.Drawing.Color.Empty;
            this.gr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr1.Location = new System.Drawing.Point(0, 0);
            this.gr1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(1273, 567);
            // 
            // 
            // 
            this.gr1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gr1.Style.BackColorGradientAngle = 90;
            this.gr1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gr1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr1.Style.BorderBottomWidth = 1;
            this.gr1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gr1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr1.Style.BorderLeftWidth = 1;
            this.gr1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr1.Style.BorderRightWidth = 1;
            this.gr1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr1.Style.BorderTopWidth = 1;
            this.gr1.Style.CornerDiameter = 4;
            this.gr1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gr1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gr1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gr1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gr1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gr1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gr1.TabIndex = 0;
            // 
            // FrmLsinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 567);
            this.Controls.Add(this.gr1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLsinfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据调用";
            this.Load += new System.EventHandler(this.FrmLsinfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gr1;
    }
}