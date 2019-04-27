namespace Csmxxbl
{
    partial class Frmxxcx
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmxxcx));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateviewG = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_gjz = new System.Windows.Forms.TextBox();
            this.but_sql = new System.Windows.Forms.Button();
            this.com_ziduan = new System.Windows.Forms.ComboBox();
            this.com_Leixing = new System.Windows.Forms.ComboBox();
            this.lab_zj = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateviewG)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dateviewG);
            this.groupBox1.Location = new System.Drawing.Point(2, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(799, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dateviewG
            // 
            this.dateviewG.AllowUserToAddRows = false;
            this.dateviewG.AllowUserToDeleteRows = false;
            this.dateviewG.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateviewG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dateviewG.DefaultCellStyle = dataGridViewCellStyle1;
            this.dateviewG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateviewG.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dateviewG.Location = new System.Drawing.Point(3, 17);
            this.dateviewG.Name = "dateviewG";
            this.dateviewG.ReadOnly = true;
            this.dateviewG.RowTemplate.Height = 23;
            this.dateviewG.Size = new System.Drawing.Size(793, 239);
            this.dateviewG.TabIndex = 0;
            this.dateviewG.Click += new System.EventHandler(this.dateviewG_Click);
            this.dateviewG.DoubleClick += new System.EventHandler(this.dateviewG_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入信息：";
            // 
            // txt_gjz
            // 
            this.txt_gjz.Location = new System.Drawing.Point(343, 18);
            this.txt_gjz.Name = "txt_gjz";
            this.txt_gjz.Size = new System.Drawing.Size(291, 21);
            this.txt_gjz.TabIndex = 0;
            this.txt_gjz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_gjz_KeyPress);
            // 
            // but_sql
            // 
            this.but_sql.Location = new System.Drawing.Point(658, 13);
            this.but_sql.Name = "but_sql";
            this.but_sql.Size = new System.Drawing.Size(78, 29);
            this.but_sql.TabIndex = 1;
            this.but_sql.Text = "查询";
            this.but_sql.UseVisualStyleBackColor = true;
            this.but_sql.Click += new System.EventHandler(this.but_sql_Click);
            // 
            // com_ziduan
            // 
            this.com_ziduan.FormattingEnabled = true;
            this.com_ziduan.Location = new System.Drawing.Point(199, 18);
            this.com_ziduan.Name = "com_ziduan";
            this.com_ziduan.Size = new System.Drawing.Size(127, 20);
            this.com_ziduan.TabIndex = 7;
            this.com_ziduan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.com_ziduan_KeyPress);
            // 
            // com_Leixing
            // 
            this.com_Leixing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Leixing.FormattingEnabled = true;
            this.com_Leixing.Items.AddRange(new object[] {
            "土地信息",
            "房屋信息"});
            this.com_Leixing.Location = new System.Drawing.Point(83, 18);
            this.com_Leixing.Name = "com_Leixing";
            this.com_Leixing.Size = new System.Drawing.Size(110, 20);
            this.com_Leixing.TabIndex = 8;
            this.com_Leixing.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.com_Leixing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.com_Leixing_KeyPress);
            // 
            // lab_zj
            // 
            this.lab_zj.AutoSize = true;
            this.lab_zj.ForeColor = System.Drawing.Color.Red;
            this.lab_zj.Location = new System.Drawing.Point(27, 64);
            this.lab_zj.Name = "lab_zj";
            this.lab_zj.Size = new System.Drawing.Size(143, 12);
            this.lab_zj.TabIndex = 9;
            this.lab_zj.Text = "此卷已扫描无需继续操作!";
            this.lab_zj.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "不动产证号只输入数字即可，查询后选中内容即可查看是否已扫描";
            // 
            // Frmxxcx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 348);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lab_zj);
            this.Controls.Add(this.com_Leixing);
            this.Controls.Add(this.com_ziduan);
            this.Controls.Add(this.but_sql);
            this.Controls.Add(this.txt_gjz);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Frmxxcx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "信息补录-查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Frmxxcx_Shown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateviewG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_gjz;
        private System.Windows.Forms.Button but_sql;
        private DevComponents.DotNetBar.Controls.DataGridViewX dateviewG;
        private System.Windows.Forms.ComboBox com_ziduan;
        private System.Windows.Forms.ComboBox com_Leixing;
        private System.Windows.Forms.Label lab_zj;
        private System.Windows.Forms.Label label2;
    }
}