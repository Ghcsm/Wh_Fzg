namespace Csmxxbl
{
    partial class FrminfoSql
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butSql = new DevComponents.DotNetBar.ButtonX();
            this.txtgjz = new System.Windows.Forms.TextBox();
            this.combInfoTj = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.combInfocol = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.groupPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.groupBox2);
            this.groupPanel1.Controls.Add(this.groupBox1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(800, 450);
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dataGrid);
            this.groupBox2.Location = new System.Drawing.Point(3, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(788, 349);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询结果";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGrid.Location = new System.Drawing.Point(3, 17);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowTemplate.Height = 23;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(782, 329);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.DoubleClick += new System.EventHandler(this.dataGrid_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.butSql);
            this.groupBox1.Controls.Add(this.txtgjz);
            this.groupBox1.Controls.Add(this.combInfoTj);
            this.groupBox1.Controls.Add(this.combInfocol);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // butSql
            // 
            this.butSql.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butSql.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butSql.Location = new System.Drawing.Point(650, 23);
            this.butSql.Name = "butSql";
            this.butSql.Size = new System.Drawing.Size(77, 38);
            this.butSql.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSql.TabIndex = 4;
            this.butSql.Text = "查询";
            this.butSql.Click += new System.EventHandler(this.butSql_Click);
            // 
            // txtgjz
            // 
            this.txtgjz.Location = new System.Drawing.Point(330, 32);
            this.txtgjz.Name = "txtgjz";
            this.txtgjz.Size = new System.Drawing.Size(297, 21);
            this.txtgjz.TabIndex = 3;
            this.txtgjz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtgjz_KeyPress);
            // 
            // combInfoTj
            // 
            this.combInfoTj.DisplayMember = "Text";
            this.combInfoTj.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combInfoTj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combInfoTj.FormattingEnabled = true;
            this.combInfoTj.ItemHeight = 15;
            this.combInfoTj.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.combInfoTj.Location = new System.Drawing.Point(211, 31);
            this.combInfoTj.Name = "combInfoTj";
            this.combInfoTj.Size = new System.Drawing.Size(104, 21);
            this.combInfoTj.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combInfoTj.TabIndex = 2;
            this.combInfoTj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combInfoTj_KeyPress);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "包含";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "等于";
            // 
            // combInfocol
            // 
            this.combInfocol.DisplayMember = "Text";
            this.combInfocol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combInfocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combInfocol.FormattingEnabled = true;
            this.combInfocol.ItemHeight = 15;
            this.combInfocol.Location = new System.Drawing.Point(78, 31);
            this.combInfocol.Name = "combInfocol";
            this.combInfocol.Size = new System.Drawing.Size(127, 21);
            this.combInfocol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combInfocol.TabIndex = 1;
            this.combInfocol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combInfocol_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "信息查询";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "不等于";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "不包含";
            // 
            // FrminfoSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrminfoSql";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "信息查询";
            this.Shown += new System.EventHandler(this.FrminfoSql_Shown);
            this.groupPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combInfocol;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combInfoTj;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.ButtonX butSql;
        private System.Windows.Forms.TextBox txtgjz;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGrid;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
    }
}