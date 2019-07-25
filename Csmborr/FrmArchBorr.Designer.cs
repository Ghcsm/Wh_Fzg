namespace Csmborr
{
    partial class FrmArchBorr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArchBorr));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combsex = new System.Windows.Forms.ComboBox();
            this.txtadd = new System.Windows.Forms.TextBox();
            this.txtlxfs = new System.Windows.Forms.TextBox();
            this.txtsfz = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combarchlx = new System.Windows.Forms.ComboBox();
            this.rabyijao = new System.Windows.Forms.RadioButton();
            this.rabguihuan = new System.Windows.Forms.RadioButton();
            this.rabjieyong = new System.Windows.Forms.RadioButton();
            this.rablook = new System.Windows.Forms.RadioButton();
            this.rabfy = new System.Windows.Forms.RadioButton();
            this.txt2sm = new System.Windows.Forms.TextBox();
            this.txt2page = new System.Windows.Forms.TextBox();
            this.txt2time = new System.Windows.Forms.TextBox();
            this.txt2jyyt = new System.Windows.Forms.TextBox();
            this.butSave = new DevComponents.DotNetBar.ButtonX();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combsex);
            this.groupBox1.Controls.Add(this.txtadd);
            this.groupBox1.Controls.Add(this.txtlxfs);
            this.groupBox1.Controls.Add(this.txtsfz);
            this.groupBox1.Controls.Add(this.txtname);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "借阅人";
            // 
            // combsex
            // 
            this.combsex.FormattingEnabled = true;
            this.combsex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.combsex.Location = new System.Drawing.Point(175, 25);
            this.combsex.Name = "combsex";
            this.combsex.Size = new System.Drawing.Size(50, 20);
            this.combsex.TabIndex = 2;
            this.combsex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combsex_KeyPress);
            // 
            // txtadd
            // 
            this.txtadd.Location = new System.Drawing.Point(304, 69);
            this.txtadd.Name = "txtadd";
            this.txtadd.Size = new System.Drawing.Size(204, 21);
            this.txtadd.TabIndex = 5;
            this.txtadd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtadd_KeyPress);
            // 
            // txtlxfs
            // 
            this.txtlxfs.Location = new System.Drawing.Point(75, 69);
            this.txtlxfs.Name = "txtlxfs";
            this.txtlxfs.Size = new System.Drawing.Size(143, 21);
            this.txtlxfs.TabIndex = 4;
            this.txtlxfs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlxfs_KeyPress);
            // 
            // txtsfz
            // 
            this.txtsfz.Location = new System.Drawing.Point(304, 25);
            this.txtsfz.Name = "txtsfz";
            this.txtsfz.Size = new System.Drawing.Size(204, 21);
            this.txtsfz.TabIndex = 3;
            this.txtsfz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsfz_KeyPress);
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(55, 25);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(73, 21);
            this.txtname.TabIndex = 1;
            this.txtname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtname_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "通讯地址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "联系方式：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "证件号码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "性别：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combarchlx);
            this.groupBox2.Controls.Add(this.rabyijao);
            this.groupBox2.Controls.Add(this.rabguihuan);
            this.groupBox2.Controls.Add(this.rabjieyong);
            this.groupBox2.Controls.Add(this.rablook);
            this.groupBox2.Controls.Add(this.rabfy);
            this.groupBox2.Controls.Add(this.txt2sm);
            this.groupBox2.Controls.Add(this.txt2page);
            this.groupBox2.Controls.Add(this.txt2time);
            this.groupBox2.Controls.Add(this.txt2jyyt);
            this.groupBox2.Controls.Add(this.butSave);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(526, 253);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "借阅信息";
            // 
            // combarchlx
            // 
            this.combarchlx.FormattingEnabled = true;
            this.combarchlx.Items.AddRange(new object[] {
            "文书档案",
            "科技档案",
            "财务档案",
            "人事档案",
            "电子档案",
            "声像档案"});
            this.combarchlx.Location = new System.Drawing.Point(77, 64);
            this.combarchlx.Name = "combarchlx";
            this.combarchlx.Size = new System.Drawing.Size(115, 20);
            this.combarchlx.TabIndex = 6;
            this.combarchlx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combarchlx_KeyPress);
            // 
            // rabyijao
            // 
            this.rabyijao.AutoSize = true;
            this.rabyijao.Location = new System.Drawing.Point(429, 28);
            this.rabyijao.Name = "rabyijao";
            this.rabyijao.Size = new System.Drawing.Size(47, 16);
            this.rabyijao.TabIndex = 16;
            this.rabyijao.Text = "移交";
            this.rabyijao.UseVisualStyleBackColor = true;
            // 
            // rabguihuan
            // 
            this.rabguihuan.AutoSize = true;
            this.rabguihuan.Location = new System.Drawing.Point(338, 28);
            this.rabguihuan.Name = "rabguihuan";
            this.rabguihuan.Size = new System.Drawing.Size(71, 16);
            this.rabguihuan.TabIndex = 15;
            this.rabguihuan.Text = "归还实物";
            this.rabguihuan.UseVisualStyleBackColor = true;
            // 
            // rabjieyong
            // 
            this.rabjieyong.AutoSize = true;
            this.rabjieyong.Location = new System.Drawing.Point(257, 28);
            this.rabjieyong.Name = "rabjieyong";
            this.rabjieyong.Size = new System.Drawing.Size(71, 16);
            this.rabjieyong.TabIndex = 14;
            this.rabjieyong.Text = "借用实物";
            this.rabjieyong.UseVisualStyleBackColor = true;
            // 
            // rablook
            // 
            this.rablook.AutoSize = true;
            this.rablook.Location = new System.Drawing.Point(172, 28);
            this.rablook.Name = "rablook";
            this.rablook.Size = new System.Drawing.Size(71, 16);
            this.rablook.TabIndex = 13;
            this.rablook.Text = "查看实物";
            this.rablook.UseVisualStyleBackColor = true;
            // 
            // rabfy
            // 
            this.rabfy.AutoSize = true;
            this.rabfy.Checked = true;
            this.rabfy.Location = new System.Drawing.Point(93, 28);
            this.rabfy.Name = "rabfy";
            this.rabfy.Size = new System.Drawing.Size(71, 16);
            this.rabfy.TabIndex = 12;
            this.rabfy.TabStop = true;
            this.rabfy.Text = "查档复印";
            this.rabfy.UseVisualStyleBackColor = true;
            // 
            // txt2sm
            // 
            this.txt2sm.Location = new System.Drawing.Point(75, 155);
            this.txt2sm.Name = "txt2sm";
            this.txt2sm.Size = new System.Drawing.Size(403, 21);
            this.txt2sm.TabIndex = 10;
            this.txt2sm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt2sm_KeyPress);
            // 
            // txt2page
            // 
            this.txt2page.Location = new System.Drawing.Point(292, 108);
            this.txt2page.Name = "txt2page";
            this.txt2page.Size = new System.Drawing.Size(163, 21);
            this.txt2page.TabIndex = 9;
            this.txt2page.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt2page_KeyPress);
            // 
            // txt2time
            // 
            this.txt2time.Location = new System.Drawing.Point(75, 110);
            this.txt2time.Name = "txt2time";
            this.txt2time.Size = new System.Drawing.Size(115, 21);
            this.txt2time.TabIndex = 8;
            this.txt2time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt2time_KeyPress);
            // 
            // txt2jyyt
            // 
            this.txt2jyyt.Location = new System.Drawing.Point(271, 64);
            this.txt2jyyt.Name = "txt2jyyt";
            this.txt2jyyt.Size = new System.Drawing.Size(207, 21);
            this.txt2jyyt.TabIndex = 7;
            this.txt2jyyt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt2jyyt_KeyPress);
            // 
            // butSave
            // 
            this.butSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butSave.Location = new System.Drawing.Point(211, 205);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 42);
            this.butSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSave.TabIndex = 11;
            this.butSave.Text = "提交";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "借阅用途：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(199, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "复印范围：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(461, 113);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "页";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(269, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "第";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "备注说明：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "借阅时间：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "档案类型：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "业务类型：";
            // 
            // FrmArchBorr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(550, 393);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArchBorr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "借阅归还";
            this.Shown += new System.EventHandler(this.FrmArchBorr_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtsfz;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtadd;
        private System.Windows.Forms.TextBox txtlxfs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt2sm;
        private System.Windows.Forms.TextBox txt2page;
        private System.Windows.Forms.TextBox txt2time;
        private System.Windows.Forms.TextBox txt2jyyt;
        private DevComponents.DotNetBar.ButtonX butSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rabyijao;
        private System.Windows.Forms.RadioButton rabjieyong;
        private System.Windows.Forms.RadioButton rablook;
        private System.Windows.Forms.RadioButton rabfy;
        private System.Windows.Forms.ComboBox combsex;
        private System.Windows.Forms.ComboBox combarchlx;
        public System.Windows.Forms.RadioButton rabguihuan;
    }
}