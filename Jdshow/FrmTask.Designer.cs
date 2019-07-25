namespace Jdshow
{
    partial class FrmTask
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.butUpdate = new DevComponents.DotNetBar.ButtonX();
            this.labjd = new System.Windows.Forms.Label();
            this.pbgUpdata = new System.Windows.Forms.ProgressBar();
            this.butDel = new DevComponents.DotNetBar.ButtonX();
            this.butStart = new DevComponents.DotNetBar.ButtonX();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(565, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr1
            // 
            this.gr1.CanvasColor = System.Drawing.SystemColors.Control;
            this.gr1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gr1.Controls.Add(this.butUpdate);
            this.gr1.Controls.Add(this.labjd);
            this.gr1.Controls.Add(this.pbgUpdata);
            this.gr1.Controls.Add(this.butDel);
            this.gr1.Controls.Add(this.butStart);
            this.gr1.Controls.Add(this.dgData);
            this.gr1.DisabledBackColor = System.Drawing.Color.Empty;
            this.gr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr1.Location = new System.Drawing.Point(0, 25);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(565, 336);
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
            this.gr1.TabIndex = 5;
            // 
            // butUpdate
            // 
            this.butUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butUpdate.Location = new System.Drawing.Point(254, 279);
            this.butUpdate.Name = "butUpdate";
            this.butUpdate.Size = new System.Drawing.Size(75, 42);
            this.butUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butUpdate.TabIndex = 5;
            this.butUpdate.Text = "刷新";
            this.butUpdate.Click += new System.EventHandler(this.butUpdate_Click);
            // 
            // labjd
            // 
            this.labjd.AutoSize = true;
            this.labjd.Location = new System.Drawing.Point(263, 279);
            this.labjd.Name = "labjd";
            this.labjd.Size = new System.Drawing.Size(0, 12);
            this.labjd.TabIndex = 4;
            this.labjd.Visible = false;
            // 
            // pbgUpdata
            // 
            this.pbgUpdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbgUpdata.Location = new System.Drawing.Point(375, 304);
            this.pbgUpdata.Name = "pbgUpdata";
            this.pbgUpdata.Size = new System.Drawing.Size(142, 23);
            this.pbgUpdata.TabIndex = 3;
            this.pbgUpdata.Visible = false;
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(137, 279);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(83, 42);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 2;
            this.butDel.Text = "删除任务";
            this.butDel.Visible = false;
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // butStart
            // 
            this.butStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butStart.Location = new System.Drawing.Point(21, 279);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(83, 42);
            this.butStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butStart.TabIndex = 1;
            this.butStart.Text = "开始任务";
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.EnableHeadersVisualStyles = false;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(3, 0);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.RowTemplate.Height = 23;
            this.dgData.Size = new System.Drawing.Size(553, 267);
            this.dgData.TabIndex = 0;
            // 
            // FrmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(565, 361);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTask";
            this.Text = "FrmTask";
            this.Load += new System.EventHandler(this.FrmTask_Load);
            this.Shown += new System.EventHandler(this.FrmTask_Shown);
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.Controls.GroupPanel gr1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX butStart;
        private DevComponents.DotNetBar.ButtonX butDel;
        private System.Windows.Forms.ProgressBar pbgUpdata;
        private System.Windows.Forms.Label labjd;
        private DevComponents.DotNetBar.ButtonX butUpdate;
    }
}