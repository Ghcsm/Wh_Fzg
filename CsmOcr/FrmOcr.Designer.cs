namespace CsmOcr
{
    partial class FrmOcr
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.combOcr = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.label1 = new System.Windows.Forms.Label();
            this.rabAllTabk = new System.Windows.Forms.RadioButton();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.butStart = new DevComponents.DotNetBar.ButtonX();
            this.rabWzx = new System.Windows.Forms.RadioButton();
            this.rabZdTabk = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolslabTaskCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolsTaskzx = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolProess = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.datGrivew = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.groupPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datGrivew)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1147, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.combOcr);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.Controls.Add(this.rabAllTabk);
            this.groupPanel1.Controls.Add(this.buttonX2);
            this.groupPanel1.Controls.Add(this.butStart);
            this.groupPanel1.Controls.Add(this.rabWzx);
            this.groupPanel1.Controls.Add(this.rabZdTabk);
            this.groupPanel1.Controls.Add(this.statusStrip1);
            this.groupPanel1.Controls.Add(this.groupBox2);
            this.groupPanel1.Controls.Add(this.gr1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 25);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(1147, 615);
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
            // combOcr
            // 
            this.combOcr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.combOcr.DisplayMember = "Text";
            this.combOcr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combOcr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combOcr.FormattingEnabled = true;
            this.combOcr.ItemHeight = 15;
            this.combOcr.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.combOcr.Location = new System.Drawing.Point(803, 542);
            this.combOcr.Name = "combOcr";
            this.combOcr.Size = new System.Drawing.Size(82, 21);
            this.combOcr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combOcr.TabIndex = 14;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "Pro";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Adv";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(747, 547);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ocr引擎";
            // 
            // rabAllTabk
            // 
            this.rabAllTabk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rabAllTabk.AutoSize = true;
            this.rabAllTabk.BackColor = System.Drawing.Color.Transparent;
            this.rabAllTabk.Location = new System.Drawing.Point(638, 546);
            this.rabAllTabk.Name = "rabAllTabk";
            this.rabAllTabk.Size = new System.Drawing.Size(95, 16);
            this.rabAllTabk.TabIndex = 12;
            this.rabAllTabk.TabStop = true;
            this.rabAllTabk.Text = "显示所有任务";
            this.rabAllTabk.UseVisualStyleBackColor = false;
            this.rabAllTabk.Click += new System.EventHandler(this.rabAllTabk_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(1036, 546);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 33);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 11;
            this.buttonX2.Text = "停止";
            // 
            // butStart
            // 
            this.butStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butStart.Location = new System.Drawing.Point(929, 546);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(75, 33);
            this.butStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butStart.TabIndex = 10;
            this.butStart.Text = "开始";
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // rabWzx
            // 
            this.rabWzx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rabWzx.AutoSize = true;
            this.rabWzx.BackColor = System.Drawing.Color.Transparent;
            this.rabWzx.Location = new System.Drawing.Point(516, 546);
            this.rabWzx.Name = "rabWzx";
            this.rabWzx.Size = new System.Drawing.Size(107, 16);
            this.rabWzx.TabIndex = 9;
            this.rabWzx.Text = "显示未执行任务";
            this.rabWzx.UseVisualStyleBackColor = false;
            this.rabWzx.Click += new System.EventHandler(this.rabWzx_Click);
            // 
            // rabZdTabk
            // 
            this.rabZdTabk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rabZdTabk.AutoSize = true;
            this.rabZdTabk.BackColor = System.Drawing.Color.Transparent;
            this.rabZdTabk.Checked = true;
            this.rabZdTabk.Location = new System.Drawing.Point(425, 546);
            this.rabZdTabk.Name = "rabZdTabk";
            this.rabZdTabk.Size = new System.Drawing.Size(71, 16);
            this.rabZdTabk.TabIndex = 8;
            this.rabZdTabk.TabStop = true;
            this.rabZdTabk.Text = "指定任务";
            this.rabZdTabk.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolslabTaskCount,
            this.toolsTaskzx,
            this.toolProess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 587);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1141, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolslabTaskCount
            // 
            this.toolslabTaskCount.BackColor = System.Drawing.Color.Transparent;
            this.toolslabTaskCount.Name = "toolslabTaskCount";
            this.toolslabTaskCount.Size = new System.Drawing.Size(35, 17);
            this.toolslabTaskCount.Text = "共计:";
            // 
            // toolsTaskzx
            // 
            this.toolsTaskzx.BackColor = System.Drawing.Color.Transparent;
            this.toolsTaskzx.Name = "toolsTaskzx";
            this.toolsTaskzx.Size = new System.Drawing.Size(59, 17);
            this.toolsTaskzx.Text = "正在执行:";
            this.toolsTaskzx.Visible = false;
            // 
            // toolProess
            // 
            this.toolProess.Name = "toolProess";
            this.toolProess.Size = new System.Drawing.Size(100, 16);
            this.toolProess.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.datGrivew);
            this.groupBox2.Location = new System.Drawing.Point(405, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 510);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "任务池";
            // 
            // datGrivew
            // 
            this.datGrivew.AllowUserToAddRows = false;
            this.datGrivew.AllowUserToDeleteRows = false;
            this.datGrivew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datGrivew.DefaultCellStyle = dataGridViewCellStyle1;
            this.datGrivew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datGrivew.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.datGrivew.Location = new System.Drawing.Point(3, 17);
            this.datGrivew.Name = "datGrivew";
            this.datGrivew.ReadOnly = true;
            this.datGrivew.RowTemplate.Height = 23;
            this.datGrivew.Size = new System.Drawing.Size(721, 490);
            this.datGrivew.TabIndex = 0;
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Location = new System.Drawing.Point(3, 3);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(396, 574);
            this.gr1.TabIndex = 1;
            this.gr1.TabStop = false;
            // 
            // FrmOcr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1147, 640);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOcr";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmOcr_Load);
            this.Shown += new System.EventHandler(this.FrmOcr_Shown);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datGrivew)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX butStart;
        private System.Windows.Forms.RadioButton rabWzx;
        private System.Windows.Forms.RadioButton rabZdTabk;
        private System.Windows.Forms.RadioButton rabAllTabk;
        private System.Windows.Forms.ToolStripProgressBar toolProess;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combOcr;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.DataGridViewX datGrivew;
        private System.Windows.Forms.ToolStripStatusLabel toolslabTaskCount;
        private System.Windows.Forms.ToolStripStatusLabel toolsTaskzx;
    }
}

