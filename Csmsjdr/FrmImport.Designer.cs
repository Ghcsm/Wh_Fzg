namespace Csmsjdr
{
    partial class FrmImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gr0 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gr4 = new System.Windows.Forms.GroupBox();
            this.combLx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.butLog = new DevComponents.DotNetBar.ButtonX();
            this.label4 = new System.Windows.Forms.Label();
            this.chbImportNew = new System.Windows.Forms.CheckBox();
            this.butImport = new DevComponents.DotNetBar.ButtonX();
            this.lbsy = new System.Windows.Forms.Label();
            this.labXlsCount = new System.Windows.Forms.Label();
            this.gr3 = new System.Windows.Forms.GroupBox();
            this.dgvXlsData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.combXlsTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.butXlsSelect = new System.Windows.Forms.Button();
            this.txtXlsPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTablecol = new System.Windows.Forms.CheckedListBox();
            this.combImportTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.opdXlsFile = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.combPages = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.gr0.SuspendLayout();
            this.gr4.SuspendLayout();
            this.gr3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXlsData)).BeginInit();
            this.gr2.SuspendLayout();
            this.gr1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr0
            // 
            this.gr0.CanvasColor = System.Drawing.SystemColors.Control;
            this.gr0.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gr0.Controls.Add(this.toolStrip1);
            this.gr0.Controls.Add(this.gr4);
            this.gr0.Controls.Add(this.gr3);
            this.gr0.Controls.Add(this.gr2);
            this.gr0.Controls.Add(this.gr1);
            this.gr0.DisabledBackColor = System.Drawing.Color.Empty;
            this.gr0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr0.Location = new System.Drawing.Point(0, 0);
            this.gr0.Name = "gr0";
            this.gr0.Size = new System.Drawing.Size(1177, 541);
            // 
            // 
            // 
            this.gr0.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gr0.Style.BackColorGradientAngle = 90;
            this.gr0.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gr0.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderBottomWidth = 1;
            this.gr0.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gr0.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderLeftWidth = 1;
            this.gr0.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderRightWidth = 1;
            this.gr0.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderTopWidth = 1;
            this.gr0.Style.CornerDiameter = 4;
            this.gr0.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gr0.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gr0.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gr0.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gr0.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gr0.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gr0.TabIndex = 0;
            // 
            // gr4
            // 
            this.gr4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr4.BackColor = System.Drawing.Color.Transparent;
            this.gr4.Controls.Add(this.combPages);
            this.gr4.Controls.Add(this.combLx);
            this.gr4.Controls.Add(this.butLog);
            this.gr4.Controls.Add(this.label5);
            this.gr4.Controls.Add(this.label4);
            this.gr4.Controls.Add(this.chbImportNew);
            this.gr4.Controls.Add(this.butImport);
            this.gr4.Controls.Add(this.lbsy);
            this.gr4.Controls.Add(this.labXlsCount);
            this.gr4.Location = new System.Drawing.Point(245, 454);
            this.gr4.Name = "gr4";
            this.gr4.Size = new System.Drawing.Size(914, 72);
            this.gr4.TabIndex = 3;
            this.gr4.TabStop = false;
            // 
            // combLx
            // 
            this.combLx.DisplayMember = "Text";
            this.combLx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combLx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combLx.FormattingEnabled = true;
            this.combLx.ItemHeight = 15;
            this.combLx.Items.AddRange(new object[] {
            this.comboItem2,
            this.comboItem1,
            this.comboItem3});
            this.combLx.Location = new System.Drawing.Point(200, 45);
            this.combLx.Name = "combLx";
            this.combLx.Size = new System.Drawing.Size(115, 21);
            this.combLx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combLx.TabIndex = 4;
            this.combLx.SelectedIndexChanged += new System.EventHandler(this.combLx_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "案卷信息";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "目录信息";
            // 
            // butLog
            // 
            this.butLog.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butLog.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butLog.Location = new System.Drawing.Point(675, 24);
            this.butLog.Name = "butLog";
            this.butLog.Size = new System.Drawing.Size(77, 42);
            this.butLog.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butLog.TabIndex = 2;
            this.butLog.Text = "查看日志";
            this.butLog.Click += new System.EventHandler(this.butLog_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(105, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "导入xls类型：";
            // 
            // chbImportNew
            // 
            this.chbImportNew.AutoSize = true;
            this.chbImportNew.Location = new System.Drawing.Point(17, 50);
            this.chbImportNew.Name = "chbImportNew";
            this.chbImportNew.Size = new System.Drawing.Size(72, 16);
            this.chbImportNew.TabIndex = 1;
            this.chbImportNew.Text = "重新导入";
            this.chbImportNew.UseVisualStyleBackColor = true;
            // 
            // butImport
            // 
            this.butImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butImport.Location = new System.Drawing.Point(544, 23);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(77, 42);
            this.butImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butImport.TabIndex = 0;
            this.butImport.Text = "开始";
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // lbsy
            // 
            this.lbsy.AutoSize = true;
            this.lbsy.Location = new System.Drawing.Point(108, 21);
            this.lbsy.Name = "lbsy";
            this.lbsy.Size = new System.Drawing.Size(59, 12);
            this.lbsy.TabIndex = 0;
            this.lbsy.Text = "剩余 0 条";
            // 
            // labXlsCount
            // 
            this.labXlsCount.AutoSize = true;
            this.labXlsCount.Location = new System.Drawing.Point(19, 21);
            this.labXlsCount.Name = "labXlsCount";
            this.labXlsCount.Size = new System.Drawing.Size(47, 12);
            this.labXlsCount.TabIndex = 0;
            this.labXlsCount.Text = "共 0 条";
            // 
            // gr3
            // 
            this.gr3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr3.BackColor = System.Drawing.Color.Transparent;
            this.gr3.Controls.Add(this.dgvXlsData);
            this.gr3.Location = new System.Drawing.Point(245, 109);
            this.gr3.Name = "gr3";
            this.gr3.Size = new System.Drawing.Size(917, 339);
            this.gr3.TabIndex = 2;
            this.gr3.TabStop = false;
            this.gr3.Text = "Xls待导入数据---注意：下列从左往右字段对应<表字段>中顺序";
            // 
            // dgvXlsData
            // 
            this.dgvXlsData.AllowUserToAddRows = false;
            this.dgvXlsData.AllowUserToDeleteRows = false;
            this.dgvXlsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvXlsData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvXlsData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXlsData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvXlsData.Location = new System.Drawing.Point(3, 17);
            this.dgvXlsData.Name = "dgvXlsData";
            this.dgvXlsData.ReadOnly = true;
            this.dgvXlsData.RowTemplate.Height = 23;
            this.dgvXlsData.Size = new System.Drawing.Size(911, 319);
            this.dgvXlsData.TabIndex = 0;
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.BackColor = System.Drawing.Color.Transparent;
            this.gr2.Controls.Add(this.combXlsTable);
            this.gr2.Controls.Add(this.butXlsSelect);
            this.gr2.Controls.Add(this.txtXlsPath);
            this.gr2.Controls.Add(this.label3);
            this.gr2.Controls.Add(this.label2);
            this.gr2.Location = new System.Drawing.Point(245, 37);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(917, 66);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            this.gr2.Text = "选择Xls文件";
            // 
            // combXlsTable
            // 
            this.combXlsTable.DisplayMember = "Text";
            this.combXlsTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combXlsTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combXlsTable.FormattingEnabled = true;
            this.combXlsTable.ItemHeight = 15;
            this.combXlsTable.Location = new System.Drawing.Point(493, 28);
            this.combXlsTable.Name = "combXlsTable";
            this.combXlsTable.Size = new System.Drawing.Size(121, 21);
            this.combXlsTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combXlsTable.TabIndex = 3;
            this.combXlsTable.SelectedIndexChanged += new System.EventHandler(this.combXlsTable_SelectedIndexChanged);
            // 
            // butXlsSelect
            // 
            this.butXlsSelect.Location = new System.Drawing.Point(327, 27);
            this.butXlsSelect.Name = "butXlsSelect";
            this.butXlsSelect.Size = new System.Drawing.Size(41, 23);
            this.butXlsSelect.TabIndex = 2;
            this.butXlsSelect.Text = "...";
            this.butXlsSelect.UseVisualStyleBackColor = true;
            this.butXlsSelect.Click += new System.EventHandler(this.butXlsSelect_Click);
            // 
            // txtXlsPath
            // 
            this.txtXlsPath.Location = new System.Drawing.Point(90, 29);
            this.txtXlsPath.Name = "txtXlsPath";
            this.txtXlsPath.ReadOnly = true;
            this.txtXlsPath.Size = new System.Drawing.Size(231, 21);
            this.txtXlsPath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(437, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "工作表：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "文件路径：";
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Controls.Add(this.groupBox1);
            this.gr1.Controls.Add(this.combImportTable);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Location = new System.Drawing.Point(9, 36);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(217, 490);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            this.gr1.Text = "选择目标表";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkTablecol);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(6, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 410);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择字段唯一值";
            // 
            // chkTablecol
            // 
            this.chkTablecol.CheckOnClick = true;
            this.chkTablecol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTablecol.FormattingEnabled = true;
            this.chkTablecol.Location = new System.Drawing.Point(3, 17);
            this.chkTablecol.Name = "chkTablecol";
            this.chkTablecol.Size = new System.Drawing.Size(194, 390);
            this.chkTablecol.TabIndex = 0;
            // 
            // combImportTable
            // 
            this.combImportTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combImportTable.DisplayMember = "Text";
            this.combImportTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combImportTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combImportTable.FormattingEnabled = true;
            this.combImportTable.ItemHeight = 15;
            this.combImportTable.Location = new System.Drawing.Point(76, 29);
            this.combImportTable.Name = "combImportTable";
            this.combImportTable.Size = new System.Drawing.Size(131, 21);
            this.combImportTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combImportTable.TabIndex = 1;
            this.combImportTable.SelectedIndexChanged += new System.EventHandler(this.combImportTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库表：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1171, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // combPages
            // 
            this.combPages.DisplayMember = "Text";
            this.combPages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combPages.FormattingEnabled = true;
            this.combPages.ItemHeight = 15;
            this.combPages.Location = new System.Drawing.Point(420, 43);
            this.combPages.Name = "combPages";
            this.combPages.Size = new System.Drawing.Size(97, 21);
            this.combPages.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combPages.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(339, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "总页码字段：";
            // 
            // FrmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 541);
            this.Controls.Add(this.gr0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导入";
            this.Shown += new System.EventHandler(this.FrmImport_Shown);
            this.gr0.ResumeLayout(false);
            this.gr0.PerformLayout();
            this.gr4.ResumeLayout(false);
            this.gr4.PerformLayout();
            this.gr3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXlsData)).EndInit();
            this.gr2.ResumeLayout(false);
            this.gr2.PerformLayout();
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gr0;
        private System.Windows.Forms.GroupBox gr1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combImportTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gr3;
        private System.Windows.Forms.GroupBox gr2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvXlsData;
        private System.Windows.Forms.GroupBox gr4;
        private System.Windows.Forms.TextBox txtXlsPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butXlsSelect;
        private DevComponents.DotNetBar.ButtonX butImport;
        private System.Windows.Forms.OpenFileDialog opdXlsFile;
        private System.Windows.Forms.Label labXlsCount;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combXlsTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbImportNew;
        private DevComponents.DotNetBar.ButtonX butLog;
        private System.Windows.Forms.Label lbsy;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combLx;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkTablecol;
        private DevComponents.Editors.ComboItem comboItem2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combPages;
        private System.Windows.Forms.Label label5;
    }
}