namespace Csmrzgl
{
    partial class Frmrjgl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmrjgl));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combColname = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.combTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.but_rzgl_so = new DevComponents.DotNetBar.ButtonX();
            this.txt_gjz = new System.Windows.Forms.TextBox();
            this.dateTime_rzgl_zhi = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTime_rzgl_qi = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateview_rz = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateview_rz)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(709, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = " 就 绪 ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.combColname);
            this.groupBox1.Controls.Add(this.combTable);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.but_rzgl_so);
            this.groupBox1.Controls.Add(this.txt_gjz);
            this.groupBox1.Controls.Add(this.dateTime_rzgl_zhi);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTime_rzgl_qi);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 90);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询";
            // 
            // combColname
            // 
            this.combColname.DisplayMember = "Text";
            this.combColname.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combColname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combColname.FormattingEnabled = true;
            this.combColname.ItemHeight = 15;
            this.combColname.Location = new System.Drawing.Point(75, 56);
            this.combColname.Name = "combColname";
            this.combColname.Size = new System.Drawing.Size(121, 21);
            this.combColname.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combColname.TabIndex = 11;
            // 
            // combTable
            // 
            this.combTable.DisplayMember = "Text";
            this.combTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTable.FormattingEnabled = true;
            this.combTable.ItemHeight = 15;
            this.combTable.Location = new System.Drawing.Point(75, 20);
            this.combTable.Name = "combTable";
            this.combTable.Size = new System.Drawing.Size(121, 21);
            this.combTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combTable.TabIndex = 10;
            this.combTable.SelectedIndexChanged += new System.EventHandler(this.combTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "关键字：";
            // 
            // but_rzgl_so
            // 
            this.but_rzgl_so.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.but_rzgl_so.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.but_rzgl_so.Location = new System.Drawing.Point(583, 27);
            this.but_rzgl_so.Name = "but_rzgl_so";
            this.but_rzgl_so.Size = new System.Drawing.Size(75, 45);
            this.but_rzgl_so.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.but_rzgl_so.TabIndex = 3;
            this.but_rzgl_so.Text = "查找";
            this.but_rzgl_so.Click += new System.EventHandler(this.but_rzgl_so_Click);
            // 
            // txt_gjz
            // 
            this.txt_gjz.Location = new System.Drawing.Point(283, 59);
            this.txt_gjz.Name = "txt_gjz";
            this.txt_gjz.Size = new System.Drawing.Size(262, 21);
            this.txt_gjz.TabIndex = 2;
            // 
            // dateTime_rzgl_zhi
            // 
            this.dateTime_rzgl_zhi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_rzgl_zhi.Location = new System.Drawing.Point(457, 24);
            this.dateTime_rzgl_zhi.Name = "dateTime_rzgl_zhi";
            this.dateTime_rzgl_zhi.Size = new System.Drawing.Size(91, 21);
            this.dateTime_rzgl_zhi.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "终止时间:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "表字段:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "表名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "起始时间:";
            // 
            // dateTime_rzgl_qi
            // 
            this.dateTime_rzgl_qi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_rzgl_qi.Location = new System.Drawing.Point(286, 25);
            this.dateTime_rzgl_qi.Name = "dateTime_rzgl_qi";
            this.dateTime_rzgl_qi.Size = new System.Drawing.Size(90, 21);
            this.dateTime_rzgl_qi.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dateview_rz);
            this.groupBox2.Location = new System.Drawing.Point(12, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(686, 334);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // dateview_rz
            // 
            this.dateview_rz.AllowUserToAddRows = false;
            this.dateview_rz.AllowUserToDeleteRows = false;
            this.dateview_rz.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dateview_rz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dateview_rz.DefaultCellStyle = dataGridViewCellStyle1;
            this.dateview_rz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateview_rz.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dateview_rz.Location = new System.Drawing.Point(3, 17);
            this.dateview_rz.Name = "dateview_rz";
            this.dateview_rz.ReadOnly = true;
            this.dateview_rz.RowTemplate.Height = 23;
            this.dateview_rz.Size = new System.Drawing.Size(680, 314);
            this.dateview_rz.TabIndex = 0;
            // 
            // Frmrjgl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(709, 466);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Frmrjgl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志管理";
            this.Shown += new System.EventHandler(this.Frmrjgl_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateview_rz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTime_rzgl_zhi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTime_rzgl_qi;
        private System.Windows.Forms.TextBox txt_gjz;
        private DevComponents.DotNetBar.ButtonX but_rzgl_so;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dateview_rz;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combColname;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}