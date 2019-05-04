namespace Csmborr
{
    partial class FrmBorr
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
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.butQuer = new DevComponents.DotNetBar.ButtonX();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBoxEx2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.lvborrQuer = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gr1.SuspendLayout();
            this.gr2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Controls.Add(this.butQuer);
            this.gr1.Controls.Add(this.textBox1);
            this.gr1.Controls.Add(this.comboBoxEx2);
            this.gr1.Controls.Add(this.comboBoxEx1);
            this.gr1.Controls.Add(this.label3);
            this.gr1.Controls.Add(this.label2);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Location = new System.Drawing.Point(7, 2);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(902, 85);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            // 
            // butQuer
            // 
            this.butQuer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butQuer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butQuer.Location = new System.Drawing.Point(700, 31);
            this.butQuer.Name = "butQuer";
            this.butQuer.Size = new System.Drawing.Size(75, 34);
            this.butQuer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butQuer.TabIndex = 4;
            this.butQuer.Text = "查询";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(517, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 21);
            this.textBox1.TabIndex = 3;
            // 
            // comboBoxEx2
            // 
            this.comboBoxEx2.DisplayMember = "Text";
            this.comboBoxEx2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx2.FormattingEnabled = true;
            this.comboBoxEx2.ItemHeight = 15;
            this.comboBoxEx2.Location = new System.Drawing.Point(314, 37);
            this.comboBoxEx2.Name = "comboBoxEx2";
            this.comboBoxEx2.Size = new System.Drawing.Size(115, 21);
            this.comboBoxEx2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx2.TabIndex = 2;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 15;
            this.comboBoxEx1.Location = new System.Drawing.Point(90, 35);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(144, 21);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(458, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "关键字：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "操作符：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询字段：";
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Controls.Add(this.lvborrQuer);
            this.gr2.Location = new System.Drawing.Point(7, 87);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(902, 350);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            // 
            // lvborrQuer
            // 
            this.lvborrQuer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvborrQuer.FullRowSelect = true;
            this.lvborrQuer.GridLines = true;
            this.lvborrQuer.Location = new System.Drawing.Point(3, 17);
            this.lvborrQuer.Name = "lvborrQuer";
            this.lvborrQuer.Size = new System.Drawing.Size(896, 330);
            this.lvborrQuer.TabIndex = 0;
            this.lvborrQuer.UseCompatibleStateImageBehavior = false;
            this.lvborrQuer.View = System.Windows.Forms.View.Details;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 443);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(915, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FrmBorr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(915, 465);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gr2);
            this.Controls.Add(this.gr1);
            this.Name = "FrmBorr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询借阅";
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.gr2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.ButtonX butQuer;
        private System.Windows.Forms.TextBox textBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView lvborrQuer;
    }
}

