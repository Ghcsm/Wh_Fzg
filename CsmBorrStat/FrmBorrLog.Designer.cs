namespace CsmBorrStat
{
    partial class FrmBorrLog
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
            this.gr = new System.Windows.Forms.GroupBox();
            this.chktime = new System.Windows.Forms.CheckBox();
            this.ButBorrDc = new DevComponents.DotNetBar.ButtonX();
            this.ButBorrQuer = new DevComponents.DotNetBar.ButtonX();
            this.datetime2 = new System.Windows.Forms.DateTimePicker();
            this.datetime1 = new System.Windows.Forms.DateTimePicker();
            this.txtgjz = new System.Windows.Forms.TextBox();
            this.combCol = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.combTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.lvQuer = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.gr.SuspendLayout();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr
            // 
            this.gr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr.Controls.Add(this.chktime);
            this.gr.Controls.Add(this.ButBorrDc);
            this.gr.Controls.Add(this.ButBorrQuer);
            this.gr.Controls.Add(this.datetime2);
            this.gr.Controls.Add(this.datetime1);
            this.gr.Controls.Add(this.txtgjz);
            this.gr.Controls.Add(this.combCol);
            this.gr.Controls.Add(this.combTable);
            this.gr.Controls.Add(this.label5);
            this.gr.Controls.Add(this.label3);
            this.gr.Controls.Add(this.label2);
            this.gr.Controls.Add(this.label1);
            this.gr.Location = new System.Drawing.Point(2, 24);
            this.gr.Name = "gr";
            this.gr.Size = new System.Drawing.Size(1066, 79);
            this.gr.TabIndex = 0;
            this.gr.TabStop = false;
            // 
            // chktime
            // 
            this.chktime.AutoSize = true;
            this.chktime.Location = new System.Drawing.Point(524, 39);
            this.chktime.Name = "chktime";
            this.chktime.Size = new System.Drawing.Size(84, 16);
            this.chktime.TabIndex = 8;
            this.chktime.Text = "时间范围：";
            this.chktime.UseVisualStyleBackColor = true;
            // 
            // ButBorrDc
            // 
            this.ButBorrDc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButBorrDc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButBorrDc.Location = new System.Drawing.Point(979, 26);
            this.ButBorrDc.Name = "ButBorrDc";
            this.ButBorrDc.Size = new System.Drawing.Size(75, 31);
            this.ButBorrDc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButBorrDc.TabIndex = 7;
            this.ButBorrDc.Text = "导出";
            // 
            // ButBorrQuer
            // 
            this.ButBorrQuer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButBorrQuer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButBorrQuer.Location = new System.Drawing.Point(882, 26);
            this.ButBorrQuer.Name = "ButBorrQuer";
            this.ButBorrQuer.Size = new System.Drawing.Size(75, 31);
            this.ButBorrQuer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButBorrQuer.TabIndex = 6;
            this.ButBorrQuer.Text = "查询";
            this.ButBorrQuer.Click += new System.EventHandler(this.ButBorrQuer_Click);
            // 
            // datetime2
            // 
            this.datetime2.Location = new System.Drawing.Point(756, 36);
            this.datetime2.Name = "datetime2";
            this.datetime2.Size = new System.Drawing.Size(103, 21);
            this.datetime2.TabIndex = 5;
            // 
            // datetime1
            // 
            this.datetime1.Location = new System.Drawing.Point(610, 36);
            this.datetime1.Name = "datetime1";
            this.datetime1.Size = new System.Drawing.Size(111, 21);
            this.datetime1.TabIndex = 4;
            // 
            // txtgjz
            // 
            this.txtgjz.Location = new System.Drawing.Point(366, 36);
            this.txtgjz.Name = "txtgjz";
            this.txtgjz.Size = new System.Drawing.Size(124, 21);
            this.txtgjz.TabIndex = 3;
            // 
            // combCol
            // 
            this.combCol.DisplayMember = "Text";
            this.combCol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combCol.FormattingEnabled = true;
            this.combCol.ItemHeight = 15;
            this.combCol.Location = new System.Drawing.Point(217, 36);
            this.combCol.Name = "combCol";
            this.combCol.Size = new System.Drawing.Size(91, 21);
            this.combCol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combCol.TabIndex = 2;
            // 
            // combTable
            // 
            this.combTable.DisplayMember = "Text";
            this.combTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTable.FormattingEnabled = true;
            this.combTable.ItemHeight = 15;
            this.combTable.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.combTable.Location = new System.Drawing.Point(59, 36);
            this.combTable.Name = "combTable";
            this.combTable.Size = new System.Drawing.Size(113, 21);
            this.combTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combTable.TabIndex = 2;
            this.combTable.SelectedIndexChanged += new System.EventHandler(this.combTable_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(727, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "---";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "关键字：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "字段：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询表：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Controls.Add(this.lvQuer);
            this.gr1.Location = new System.Drawing.Point(2, 109);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(1066, 395);
            this.gr1.TabIndex = 2;
            this.gr1.TabStop = false;
            this.gr1.Text = "操作日志";
            // 
            // lvQuer
            // 
            this.lvQuer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvQuer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvQuer.FullRowSelect = true;
            this.lvQuer.GridLines = true;
            this.lvQuer.Location = new System.Drawing.Point(3, 17);
            this.lvQuer.Name = "lvQuer";
            this.lvQuer.Size = new System.Drawing.Size(1060, 375);
            this.lvQuer.TabIndex = 0;
            this.lvQuer.UseCompatibleStateImageBehavior = false;
            this.lvQuer.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "借阅记录";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "操作日志";
            // 
            // FrmBorrLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1071, 507);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBorrLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "借阅日志";
            this.Shown += new System.EventHandler(this.FrmBorrLog_Shown);
            this.gr.ResumeLayout(false);
            this.gr.PerformLayout();
            this.gr1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gr;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combCol;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combTable;
        private System.Windows.Forms.TextBox txtgjz;
        private System.Windows.Forms.DateTimePicker datetime2;
        private System.Windows.Forms.DateTimePicker datetime1;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.ButtonX ButBorrQuer;
        private System.Windows.Forms.GroupBox gr1;
        private DevComponents.DotNetBar.ButtonX ButBorrDc;
        private System.Windows.Forms.ListView lvQuer;
        private System.Windows.Forms.CheckBox chktime;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
    }
}

