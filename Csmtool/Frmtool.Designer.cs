namespace Csmtool
{
    partial class Frmtool
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
            this.TabControl = new DevComponents.DotNetBar.SuperTabControl();
            this.TabcontrolArchStat = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.TabitemlArchStat = new DevComponents.DotNetBar.SuperTabItem();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rabBoxsn = new System.Windows.Forms.RadioButton();
            this.rabArchid = new System.Windows.Forms.RadioButton();
            this.rabArchcol = new System.Windows.Forms.RadioButton();
            this.txtBoxsn = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rabcleScan = new System.Windows.Forms.RadioButton();
            this.rabcleCheck = new System.Windows.Forms.RadioButton();
            this.rabcleInfobl = new System.Windows.Forms.RadioButton();
            this.rabcleConten = new System.Windows.Forms.RadioButton();
            this.chkAllstat = new System.Windows.Forms.CheckBox();
            this.butStart = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butArchStat = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labbox = new System.Windows.Forms.Label();
            this.labarchno = new System.Windows.Forms.Label();
            this.labarchid = new System.Windows.Forms.Label();
            this.labArchcol = new System.Windows.Forms.Label();
            this.txtArchno = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.gr3 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rabtjBoxsn = new System.Windows.Forms.RadioButton();
            this.rabtjCol = new System.Windows.Forms.RadioButton();
            this.txtTjBoxsn1 = new System.Windows.Forms.TextBox();
            this.txtTjBoxsn2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttjStart = new DevComponents.DotNetBar.ButtonX();
            this.label9 = new System.Windows.Forms.Label();
            this.buttjxls = new DevComponents.DotNetBar.ButtonX();
            this.combtjSql = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dgvTjdata = new System.Windows.Forms.DataGridView();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControl)).BeginInit();
            this.TabControl.SuspendLayout();
            this.TabcontrolArchStat.SuspendLayout();
            this.gr2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gr3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTjdata)).BeginInit();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gr1.Controls.Add(this.TabControl);
            this.gr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr1.Location = new System.Drawing.Point(0, 25);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(908, 613);
            this.gr1.TabIndex = 2;
            this.gr1.TabStop = false;
            // 
            // TabControl
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.TabControl.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.TabControl.ControlBox.MenuBox.Name = "";
            this.TabControl.ControlBox.Name = "";
            this.TabControl.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.TabControl.ControlBox.MenuBox,
            this.TabControl.ControlBox.CloseBox});
            this.TabControl.Controls.Add(this.TabcontrolArchStat);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(3, 17);
            this.TabControl.Name = "TabControl";
            this.TabControl.ReorderTabsEnabled = true;
            this.TabControl.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.TabControl.SelectedTabIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(902, 593);
            this.TabControl.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabControl.TabIndex = 0;
            this.TabControl.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.TabitemlArchStat});
            this.TabControl.Text = "superTabControl1";
            // 
            // TabcontrolArchStat
            // 
            this.TabcontrolArchStat.Controls.Add(this.gr3);
            this.TabcontrolArchStat.Controls.Add(this.gr2);
            this.TabcontrolArchStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabcontrolArchStat.Location = new System.Drawing.Point(0, 28);
            this.TabcontrolArchStat.Name = "TabcontrolArchStat";
            this.TabcontrolArchStat.Size = new System.Drawing.Size(902, 565);
            this.TabcontrolArchStat.TabIndex = 1;
            this.TabcontrolArchStat.TabItem = this.TabitemlArchStat;
            // 
            // TabitemlArchStat
            // 
            this.TabitemlArchStat.AttachedControl = this.TabcontrolArchStat;
            this.TabitemlArchStat.GlobalItem = false;
            this.TabitemlArchStat.Name = "TabitemlArchStat";
            this.TabitemlArchStat.Text = "案卷处理";
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr2.BackColor = System.Drawing.Color.Transparent;
            this.gr2.Controls.Add(this.txtArchno);
            this.gr2.Controls.Add(this.groupBox2);
            this.gr2.Controls.Add(this.groupBox1);
            this.gr2.Controls.Add(this.txtBoxsn);
            this.gr2.Controls.Add(this.rabArchcol);
            this.gr2.Controls.Add(this.rabArchid);
            this.gr2.Controls.Add(this.rabBoxsn);
            this.gr2.Controls.Add(this.label2);
            this.gr2.Controls.Add(this.label1);
            this.gr2.Location = new System.Drawing.Point(3, 14);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(280, 548);
            this.gr2.TabIndex = 0;
            this.gr2.TabStop = false;
            this.gr2.Text = "档案装态管理";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "盒号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "卷号：";
            // 
            // rabBoxsn
            // 
            this.rabBoxsn.AutoSize = true;
            this.rabBoxsn.Checked = true;
            this.rabBoxsn.Location = new System.Drawing.Point(16, 35);
            this.rabBoxsn.Name = "rabBoxsn";
            this.rabBoxsn.Size = new System.Drawing.Size(71, 16);
            this.rabBoxsn.TabIndex = 1;
            this.rabBoxsn.TabStop = true;
            this.rabBoxsn.Text = "盒号卷号";
            this.rabBoxsn.UseVisualStyleBackColor = true;
            this.rabBoxsn.CheckedChanged += new System.EventHandler(this.rabBoxsn_CheckedChanged);
            // 
            // rabArchid
            // 
            this.rabArchid.AutoSize = true;
            this.rabArchid.Location = new System.Drawing.Point(102, 35);
            this.rabArchid.Name = "rabArchid";
            this.rabArchid.Size = new System.Drawing.Size(71, 16);
            this.rabArchid.TabIndex = 1;
            this.rabArchid.Text = "Archid号";
            this.rabArchid.UseVisualStyleBackColor = true;
            // 
            // rabArchcol
            // 
            this.rabArchcol.AutoSize = true;
            this.rabArchcol.Location = new System.Drawing.Point(188, 35);
            this.rabArchcol.Name = "rabArchcol";
            this.rabArchcol.Size = new System.Drawing.Size(59, 16);
            this.rabArchcol.TabIndex = 1;
            this.rabArchcol.Text = "字段号";
            this.rabArchcol.UseVisualStyleBackColor = true;
            // 
            // txtBoxsn
            // 
            this.txtBoxsn.Location = new System.Drawing.Point(55, 81);
            this.txtBoxsn.Name = "txtBoxsn";
            this.txtBoxsn.Size = new System.Drawing.Size(76, 21);
            this.txtBoxsn.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butStart);
            this.groupBox1.Controls.Add(this.chkAllstat);
            this.groupBox1.Controls.Add(this.rabcleConten);
            this.groupBox1.Controls.Add(this.rabcleInfobl);
            this.groupBox1.Controls.Add(this.rabcleCheck);
            this.groupBox1.Controls.Add(this.rabcleScan);
            this.groupBox1.Location = new System.Drawing.Point(6, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 192);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作状态";
            // 
            // rabcleScan
            // 
            this.rabcleScan.AutoSize = true;
            this.rabcleScan.Checked = true;
            this.rabcleScan.Location = new System.Drawing.Point(23, 60);
            this.rabcleScan.Name = "rabcleScan";
            this.rabcleScan.Size = new System.Drawing.Size(71, 16);
            this.rabcleScan.TabIndex = 0;
            this.rabcleScan.TabStop = true;
            this.rabcleScan.Text = "清空扫描";
            this.rabcleScan.UseVisualStyleBackColor = true;
            // 
            // rabcleCheck
            // 
            this.rabcleCheck.AutoSize = true;
            this.rabcleCheck.Location = new System.Drawing.Point(146, 59);
            this.rabcleCheck.Name = "rabcleCheck";
            this.rabcleCheck.Size = new System.Drawing.Size(95, 16);
            this.rabcleCheck.TabIndex = 0;
            this.rabcleCheck.Text = "清空质检状态";
            this.rabcleCheck.UseVisualStyleBackColor = true;
            // 
            // rabcleInfobl
            // 
            this.rabcleInfobl.AutoSize = true;
            this.rabcleInfobl.Location = new System.Drawing.Point(23, 95);
            this.rabcleInfobl.Name = "rabcleInfobl";
            this.rabcleInfobl.Size = new System.Drawing.Size(95, 16);
            this.rabcleInfobl.TabIndex = 0;
            this.rabcleInfobl.Text = "清空补录信息";
            this.rabcleInfobl.UseVisualStyleBackColor = true;
            // 
            // rabcleConten
            // 
            this.rabcleConten.AutoSize = true;
            this.rabcleConten.Location = new System.Drawing.Point(146, 94);
            this.rabcleConten.Name = "rabcleConten";
            this.rabcleConten.Size = new System.Drawing.Size(95, 16);
            this.rabcleConten.TabIndex = 0;
            this.rabcleConten.Text = "清空目录信息";
            this.rabcleConten.UseVisualStyleBackColor = true;
            // 
            // chkAllstat
            // 
            this.chkAllstat.AutoSize = true;
            this.chkAllstat.Location = new System.Drawing.Point(24, 28);
            this.chkAllstat.Name = "chkAllstat";
            this.chkAllstat.Size = new System.Drawing.Size(120, 16);
            this.chkAllstat.TabIndex = 1;
            this.chkAllstat.Text = "清空以下所有状态";
            this.chkAllstat.UseVisualStyleBackColor = true;
            // 
            // butStart
            // 
            this.butStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butStart.Location = new System.Drawing.Point(170, 137);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(75, 37);
            this.butStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butStart.TabIndex = 4;
            this.butStart.Text = "执行";
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.butArchStat);
            this.groupBox2.Controls.Add(this.labArchcol);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.labarchid);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.labbox);
            this.groupBox2.Controls.Add(this.labarchno);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 341);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 189);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询盒号,ID,字段号";
            // 
            // butArchStat
            // 
            this.butArchStat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butArchStat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butArchStat.Location = new System.Drawing.Point(170, 142);
            this.butArchStat.Name = "butArchStat";
            this.butArchStat.Size = new System.Drawing.Size(75, 37);
            this.butArchStat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butArchStat.TabIndex = 1;
            this.butArchStat.Text = "查询";
            this.butArchStat.Click += new System.EventHandler(this.butArchStat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前盒号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "当前卷号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "当前Archid号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "当前字段号：";
            // 
            // labbox
            // 
            this.labbox.AutoSize = true;
            this.labbox.Location = new System.Drawing.Point(83, 45);
            this.labbox.Name = "labbox";
            this.labbox.Size = new System.Drawing.Size(0, 12);
            this.labbox.TabIndex = 0;
            // 
            // labarchno
            // 
            this.labarchno.AutoSize = true;
            this.labarchno.Location = new System.Drawing.Point(206, 46);
            this.labarchno.Name = "labarchno";
            this.labarchno.Size = new System.Drawing.Size(0, 12);
            this.labarchno.TabIndex = 0;
            // 
            // labarchid
            // 
            this.labarchid.AutoSize = true;
            this.labarchid.Location = new System.Drawing.Point(111, 78);
            this.labarchid.Name = "labarchid";
            this.labarchid.Size = new System.Drawing.Size(0, 12);
            this.labarchid.TabIndex = 0;
            // 
            // labArchcol
            // 
            this.labArchcol.AutoSize = true;
            this.labArchcol.Location = new System.Drawing.Point(90, 114);
            this.labArchcol.Name = "labArchcol";
            this.labArchcol.Size = new System.Drawing.Size(0, 12);
            this.labArchcol.TabIndex = 0;
            // 
            // txtArchno
            // 
            // 
            // 
            // 
            this.txtArchno.Border.Class = "TextBoxBorder";
            this.txtArchno.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtArchno.Location = new System.Drawing.Point(184, 81);
            this.txtArchno.Name = "txtArchno";
            this.txtArchno.PreventEnterBeep = true;
            this.txtArchno.Size = new System.Drawing.Size(75, 21);
            this.txtArchno.TabIndex = 1;
            // 
            // gr3
            // 
            this.gr3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr3.BackColor = System.Drawing.Color.Transparent;
            this.gr3.Controls.Add(this.groupBox4);
            this.gr3.Controls.Add(this.groupBox3);
            this.gr3.Location = new System.Drawing.Point(289, 14);
            this.gr3.Name = "gr3";
            this.gr3.Size = new System.Drawing.Size(604, 546);
            this.gr3.TabIndex = 1;
            this.gr3.TabStop = false;
            this.gr3.Text = "统计案卷状态";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(908, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.combtjSql);
            this.groupBox3.Controls.Add(this.buttjxls);
            this.groupBox3.Controls.Add(this.buttjStart);
            this.groupBox3.Controls.Add(this.txtTjBoxsn2);
            this.groupBox3.Controls.Add(this.txtTjBoxsn1);
            this.groupBox3.Controls.Add(this.rabtjCol);
            this.groupBox3.Controls.Add(this.rabtjBoxsn);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(6, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(592, 109);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "条件";
            // 
            // rabtjBoxsn
            // 
            this.rabtjBoxsn.AutoSize = true;
            this.rabtjBoxsn.Checked = true;
            this.rabtjBoxsn.Location = new System.Drawing.Point(20, 33);
            this.rabtjBoxsn.Name = "rabtjBoxsn";
            this.rabtjBoxsn.Size = new System.Drawing.Size(71, 16);
            this.rabtjBoxsn.TabIndex = 0;
            this.rabtjBoxsn.TabStop = true;
            this.rabtjBoxsn.Text = "盒号卷号";
            this.rabtjBoxsn.UseVisualStyleBackColor = true;
            this.rabtjBoxsn.CheckedChanged += new System.EventHandler(this.rabtjBoxsn_CheckedChanged);
            // 
            // rabtjCol
            // 
            this.rabtjCol.AutoSize = true;
            this.rabtjCol.Location = new System.Drawing.Point(22, 71);
            this.rabtjCol.Name = "rabtjCol";
            this.rabtjCol.Size = new System.Drawing.Size(59, 16);
            this.rabtjCol.TabIndex = 1;
            this.rabtjCol.Text = "字段号";
            this.rabtjCol.UseVisualStyleBackColor = true;
            // 
            // txtTjBoxsn1
            // 
            this.txtTjBoxsn1.Location = new System.Drawing.Point(140, 28);
            this.txtTjBoxsn1.Name = "txtTjBoxsn1";
            this.txtTjBoxsn1.Size = new System.Drawing.Size(111, 21);
            this.txtTjBoxsn1.TabIndex = 2;
            // 
            // txtTjBoxsn2
            // 
            this.txtTjBoxsn2.Location = new System.Drawing.Point(140, 68);
            this.txtTjBoxsn2.Name = "txtTjBoxsn2";
            this.txtTjBoxsn2.Size = new System.Drawing.Size(111, 21);
            this.txtTjBoxsn2.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(97, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "盒号2：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "盒号1：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvTjdata);
            this.groupBox4.Location = new System.Drawing.Point(6, 137);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(592, 393);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "统计结果";
            // 
            // buttjStart
            // 
            this.buttjStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttjStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttjStart.Location = new System.Drawing.Point(363, 69);
            this.buttjStart.Name = "buttjStart";
            this.buttjStart.Size = new System.Drawing.Size(75, 31);
            this.buttjStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttjStart.TabIndex = 3;
            this.buttjStart.Text = "执行";
            this.buttjStart.Click += new System.EventHandler(this.buttjStart_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(282, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "选择条件：";
            // 
            // buttjxls
            // 
            this.buttjxls.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttjxls.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttjxls.Location = new System.Drawing.Point(489, 69);
            this.buttjxls.Name = "buttjxls";
            this.buttjxls.Size = new System.Drawing.Size(75, 31);
            this.buttjxls.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttjxls.TabIndex = 4;
            this.buttjxls.Text = "导出";
            this.buttjxls.Click += new System.EventHandler(this.buttjxls_Click);
            // 
            // combtjSql
            // 
            this.combtjSql.DisplayMember = "Text";
            this.combtjSql.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combtjSql.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combtjSql.FormattingEnabled = true;
            this.combtjSql.ItemHeight = 15;
            this.combtjSql.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.combtjSql.Location = new System.Drawing.Point(353, 20);
            this.combtjSql.Name = "combtjSql";
            this.combtjSql.Size = new System.Drawing.Size(211, 21);
            this.combtjSql.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combtjSql.TabIndex = 5;
            // 
            // dgvTjdata
            // 
            this.dgvTjdata.AllowUserToAddRows = false;
            this.dgvTjdata.AllowUserToDeleteRows = false;
            this.dgvTjdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTjdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTjdata.Location = new System.Drawing.Point(3, 17);
            this.dgvTjdata.Name = "dgvTjdata";
            this.dgvTjdata.ReadOnly = true;
            this.dgvTjdata.RowTemplate.Height = 23;
            this.dgvTjdata.Size = new System.Drawing.Size(586, 373);
            this.dgvTjdata.TabIndex = 0;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "未扫描";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "未排序";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "未质检";
            // 
            // Frmtool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 638);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frmtool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.Frmtool_Shown);
            this.gr1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabControl)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.TabcontrolArchStat.ResumeLayout(false);
            this.gr2.ResumeLayout(false);
            this.gr2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gr3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTjdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gr1;
        private DevComponents.DotNetBar.SuperTabControl TabControl;
        private DevComponents.DotNetBar.SuperTabControlPanel TabcontrolArchStat;
        private DevComponents.DotNetBar.SuperTabItem TabitemlArchStat;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.RadioButton rabArchcol;
        private System.Windows.Forms.RadioButton rabArchid;
        private System.Windows.Forms.RadioButton rabBoxsn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxsn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAllstat;
        private System.Windows.Forms.RadioButton rabcleConten;
        private System.Windows.Forms.RadioButton rabcleInfobl;
        private System.Windows.Forms.RadioButton rabcleCheck;
        private System.Windows.Forms.RadioButton rabcleScan;
        private DevComponents.DotNetBar.ButtonX butStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX butArchStat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labArchcol;
        private System.Windows.Forms.Label labarchid;
        private System.Windows.Forms.Label labbox;
        private System.Windows.Forms.Label labarchno;
        private DevComponents.DotNetBar.Controls.TextBoxX txtArchno;
        private System.Windows.Forms.GroupBox gr3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rabtjCol;
        private System.Windows.Forms.RadioButton rabtjBoxsn;
        private System.Windows.Forms.TextBox txtTjBoxsn2;
        private System.Windows.Forms.TextBox txtTjBoxsn1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevComponents.DotNetBar.ButtonX buttjStart;
        private System.Windows.Forms.Label label9;
        private DevComponents.DotNetBar.ButtonX buttjxls;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combtjSql;
        private System.Windows.Forms.DataGridView dgvTjdata;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
    }
}

