namespace CsmCon
{
    partial class UcInfoGet
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvInfo = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.combZd = new System.Windows.Forms.ComboBox();
            this.combtj = new System.Windows.Forms.ComboBox();
            this.txtgjz = new System.Windows.Forms.TextBox();
            this.butSql = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvInfo);
            this.groupBox1.Location = new System.Drawing.Point(3, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(794, 227);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息显示";
            // 
            // dgvInfo
            // 
            this.dgvInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfo.Location = new System.Drawing.Point(3, 17);
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.RowTemplate.Height = 23;
            this.dgvInfo.Size = new System.Drawing.Size(788, 207);
            this.dgvInfo.TabIndex = 5;
            this.dgvInfo.DoubleClick += new System.EventHandler(this.dgvInfo_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查询字段:";
            // 
            // combZd
            // 
            this.combZd.FormattingEnabled = true;
            this.combZd.Location = new System.Drawing.Point(133, 19);
            this.combZd.Name = "combZd";
            this.combZd.Size = new System.Drawing.Size(157, 20);
            this.combZd.TabIndex = 1;
            this.combZd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combZd_KeyPress);
            // 
            // combtj
            // 
            this.combtj.FormattingEnabled = true;
            this.combtj.Items.AddRange(new object[] {
            "包含",
            "等于"});
            this.combtj.Location = new System.Drawing.Point(296, 19);
            this.combtj.Name = "combtj";
            this.combtj.Size = new System.Drawing.Size(88, 20);
            this.combtj.TabIndex = 2;
            this.combtj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combtj_KeyPress);
            // 
            // txtgjz
            // 
            this.txtgjz.Location = new System.Drawing.Point(390, 19);
            this.txtgjz.Name = "txtgjz";
            this.txtgjz.Size = new System.Drawing.Size(127, 21);
            this.txtgjz.TabIndex = 3;
            this.txtgjz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtgjz_KeyPress);
            // 
            // butSql
            // 
            this.butSql.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butSql.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butSql.Location = new System.Drawing.Point(536, 12);
            this.butSql.Name = "butSql";
            this.butSql.Size = new System.Drawing.Size(75, 35);
            this.butSql.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSql.TabIndex = 4;
            this.butSql.Text = "查询";
            this.butSql.Click += new System.EventHandler(this.butSql_Click);
            // 
            // UcInfoGet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.butSql);
            this.Controls.Add(this.txtgjz);
            this.Controls.Add(this.combtj);
            this.Controls.Add(this.combZd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcInfoGet";
            this.Size = new System.Drawing.Size(800, 281);
            this.Load += new System.EventHandler(this.UcInfoGet_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combZd;
        private System.Windows.Forms.ComboBox combtj;
        private System.Windows.Forms.TextBox txtgjz;
        private DevComponents.DotNetBar.ButtonX butSql;
    }
}
