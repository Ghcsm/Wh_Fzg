namespace Bgkj
{
    partial class FrmSet
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.butModulesysDelStop = new DevComponents.DotNetBar.ButtonX();
            this.butModulesysdel = new DevComponents.DotNetBar.ButtonX();
            this.butModulesysadd = new DevComponents.DotNetBar.ButtonX();
            this.chkMouduleColSet = new System.Windows.Forms.CheckedListBox();
            this.chkMouduleCol = new System.Windows.Forms.CheckedListBox();
            this.tabModuleSet = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.txtModuleFileName = new System.Windows.Forms.TextBox();
            this.pict = new System.Windows.Forms.PictureBox();
            this.butModuleCle = new DevComponents.DotNetBar.ButtonX();
            this.butModuleSet = new DevComponents.DotNetBar.ButtonX();
            this.comModuleImg = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comModuleFz = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.c0 = new DevComponents.Editors.ComboItem();
            this.c1 = new DevComponents.Editors.ComboItem();
            this.c2 = new DevComponents.Editors.ComboItem();
            this.c4 = new DevComponents.Editors.ComboItem();
            this.txtModuleName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtModuleChName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabModule = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.chkUpdateTime = new System.Windows.Forms.CheckBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.butCle = new DevComponents.DotNetBar.ButtonX();
            this.butOk = new DevComponents.DotNetBar.ButtonX();
            this.txtTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPer = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(386, 295);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabModule);
            this.tabControl1.Tabs.Add(this.tabPer);
            this.tabControl1.Tabs.Add(this.tabModuleSet);
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.butModulesysDelStop);
            this.tabControlPanel3.Controls.Add(this.butModulesysdel);
            this.tabControlPanel3.Controls.Add(this.butModulesysadd);
            this.tabControlPanel3.Controls.Add(this.chkMouduleColSet);
            this.tabControlPanel3.Controls.Add(this.chkMouduleCol);
            this.tabControlPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(386, 269);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 9;
            this.tabControlPanel3.TabItem = this.tabModuleSet;
            // 
            // butModulesysDelStop
            // 
            this.butModulesysDelStop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModulesysDelStop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModulesysDelStop.Location = new System.Drawing.Point(154, 178);
            this.butModulesysDelStop.Name = "butModulesysDelStop";
            this.butModulesysDelStop.Size = new System.Drawing.Size(67, 35);
            this.butModulesysDelStop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModulesysDelStop.TabIndex = 5;
            this.butModulesysDelStop.Text = "永久禁用";
            this.butModulesysDelStop.Tooltip = "将删除模块";
            this.butModulesysDelStop.Click += new System.EventHandler(this.butModulesysDelStop_Click);
            // 
            // butModulesysdel
            // 
            this.butModulesysdel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModulesysdel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModulesysdel.Location = new System.Drawing.Point(156, 111);
            this.butModulesysdel.Name = "butModulesysdel";
            this.butModulesysdel.Size = new System.Drawing.Size(60, 23);
            this.butModulesysdel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModulesysdel.TabIndex = 4;
            this.butModulesysdel.Text = "<<解禁";
            this.butModulesysdel.Click += new System.EventHandler(this.butModulesysdel_Click);
            // 
            // butModulesysadd
            // 
            this.butModulesysadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModulesysadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModulesysadd.Location = new System.Drawing.Point(156, 51);
            this.butModulesysadd.Name = "butModulesysadd";
            this.butModulesysadd.Size = new System.Drawing.Size(60, 23);
            this.butModulesysadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModulesysadd.TabIndex = 3;
            this.butModulesysadd.Text = "禁用>>";
            this.butModulesysadd.Click += new System.EventHandler(this.butModulesysadd_Click);
            // 
            // chkMouduleColSet
            // 
            this.chkMouduleColSet.CheckOnClick = true;
            this.chkMouduleColSet.FormattingEnabled = true;
            this.chkMouduleColSet.Location = new System.Drawing.Point(242, 14);
            this.chkMouduleColSet.Name = "chkMouduleColSet";
            this.chkMouduleColSet.Size = new System.Drawing.Size(132, 228);
            this.chkMouduleColSet.TabIndex = 2;
            // 
            // chkMouduleCol
            // 
            this.chkMouduleCol.CheckOnClick = true;
            this.chkMouduleCol.FormattingEnabled = true;
            this.chkMouduleCol.Location = new System.Drawing.Point(8, 20);
            this.chkMouduleCol.Name = "chkMouduleCol";
            this.chkMouduleCol.Size = new System.Drawing.Size(132, 228);
            this.chkMouduleCol.TabIndex = 0;
            // 
            // tabModuleSet
            // 
            this.tabModuleSet.AttachedControl = this.tabControlPanel3;
            this.tabModuleSet.Name = "tabModuleSet";
            this.tabModuleSet.Text = "模块授权设置";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.gr1);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(386, 269);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabModule;
            // 
            // gr1
            // 
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Controls.Add(this.txtModuleFileName);
            this.gr1.Controls.Add(this.pict);
            this.gr1.Controls.Add(this.butModuleCle);
            this.gr1.Controls.Add(this.butModuleSet);
            this.gr1.Controls.Add(this.comModuleImg);
            this.gr1.Controls.Add(this.comModuleFz);
            this.gr1.Controls.Add(this.txtModuleName);
            this.gr1.Controls.Add(this.txtModuleChName);
            this.gr1.Controls.Add(this.label4);
            this.gr1.Controls.Add(this.label3);
            this.gr1.Controls.Add(this.label7);
            this.gr1.Controls.Add(this.label2);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr1.Location = new System.Drawing.Point(1, 1);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(384, 267);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            // 
            // txtModuleFileName
            // 
            this.txtModuleFileName.Location = new System.Drawing.Point(148, 84);
            this.txtModuleFileName.Name = "txtModuleFileName";
            this.txtModuleFileName.Size = new System.Drawing.Size(160, 21);
            this.txtModuleFileName.TabIndex = 18;
            this.txtModuleFileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModuleFileName_KeyPress);
            // 
            // pict
            // 
            this.pict.BackColor = System.Drawing.Color.Transparent;
            this.pict.Location = new System.Drawing.Point(262, 146);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(54, 42);
            this.pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pict.TabIndex = 28;
            this.pict.TabStop = false;
            // 
            // butModuleCle
            // 
            this.butModuleCle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModuleCle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModuleCle.Location = new System.Drawing.Point(221, 204);
            this.butModuleCle.Name = "butModuleCle";
            this.butModuleCle.Size = new System.Drawing.Size(92, 43);
            this.butModuleCle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModuleCle.TabIndex = 25;
            this.butModuleCle.Text = "取消";
            this.butModuleCle.Click += new System.EventHandler(this.butModuleCle_Click);
            // 
            // butModuleSet
            // 
            this.butModuleSet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModuleSet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModuleSet.Location = new System.Drawing.Point(86, 204);
            this.butModuleSet.Name = "butModuleSet";
            this.butModuleSet.Size = new System.Drawing.Size(92, 43);
            this.butModuleSet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModuleSet.TabIndex = 23;
            this.butModuleSet.Text = "设置";
            this.butModuleSet.Click += new System.EventHandler(this.butModuleSet_Click);
            // 
            // comModuleImg
            // 
            this.comModuleImg.DisplayMember = "Text";
            this.comModuleImg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comModuleImg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModuleImg.FormattingEnabled = true;
            this.comModuleImg.ItemHeight = 15;
            this.comModuleImg.Location = new System.Drawing.Point(148, 155);
            this.comModuleImg.Name = "comModuleImg";
            this.comModuleImg.Size = new System.Drawing.Size(94, 21);
            this.comModuleImg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comModuleImg.TabIndex = 20;
            this.comModuleImg.SelectedIndexChanged += new System.EventHandler(this.comModuleImg_SelectedIndexChanged);
            this.comModuleImg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comModuleImg_KeyPress);
            // 
            // comModuleFz
            // 
            this.comModuleFz.DisplayMember = "Text";
            this.comModuleFz.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comModuleFz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModuleFz.FormattingEnabled = true;
            this.comModuleFz.ItemHeight = 15;
            this.comModuleFz.Items.AddRange(new object[] {
            this.c0,
            this.c1,
            this.c2,
            this.c4});
            this.comModuleFz.Location = new System.Drawing.Point(148, 119);
            this.comModuleFz.Name = "comModuleFz";
            this.comModuleFz.Size = new System.Drawing.Size(163, 21);
            this.comModuleFz.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comModuleFz.TabIndex = 19;
            this.comModuleFz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comModuleFz_KeyPress);
            // 
            // c0
            // 
            this.c0.Text = "数据模块";
            // 
            // c1
            // 
            this.c1.Text = "图像模块";
            // 
            // c2
            // 
            this.c2.Text = "系统配置";
            // 
            // c4
            // 
            this.c4.Text = "其他模块";
            // 
            // txtModuleName
            // 
            // 
            // 
            // 
            this.txtModuleName.Border.Class = "TextBoxBorder";
            this.txtModuleName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtModuleName.Location = new System.Drawing.Point(148, 51);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.PreventEnterBeep = true;
            this.txtModuleName.Size = new System.Drawing.Size(163, 21);
            this.txtModuleName.TabIndex = 17;
            this.txtModuleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModuleName_KeyPress);
            // 
            // txtModuleChName
            // 
            // 
            // 
            // 
            this.txtModuleChName.Border.Class = "TextBoxBorder";
            this.txtModuleChName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtModuleChName.Location = new System.Drawing.Point(148, 19);
            this.txtModuleChName.Name = "txtModuleChName";
            this.txtModuleChName.PreventEnterBeep = true;
            this.txtModuleChName.Size = new System.Drawing.Size(163, 21);
            this.txtModuleChName.TabIndex = 16;
            this.txtModuleChName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModuleChName_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(61, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "显示图标:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(61, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "所属分组:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(55, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "模块文件名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(56, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "模块空间名:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(56, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "模块中文名:";
            // 
            // tabModule
            // 
            this.tabModule.AttachedControl = this.tabControlPanel1;
            this.tabModule.Name = "tabModule";
            this.tabModule.Text = "模块设置";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.chkUpdateTime);
            this.tabControlPanel2.Controls.Add(this.txtId);
            this.tabControlPanel2.Controls.Add(this.butCle);
            this.tabControlPanel2.Controls.Add(this.butOk);
            this.tabControlPanel2.Controls.Add(this.txtTime);
            this.tabControlPanel2.Controls.Add(this.txtSn);
            this.tabControlPanel2.Controls.Add(this.label6);
            this.tabControlPanel2.Controls.Add(this.label8);
            this.tabControlPanel2.Controls.Add(this.label5);
            this.tabControlPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(386, 269);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 5;
            this.tabControlPanel2.TabItem = this.tabPer;
            // 
            // chkUpdateTime
            // 
            this.chkUpdateTime.AutoSize = true;
            this.chkUpdateTime.BackColor = System.Drawing.Color.Transparent;
            this.chkUpdateTime.Enabled = false;
            this.chkUpdateTime.Location = new System.Drawing.Point(60, 156);
            this.chkUpdateTime.Name = "chkUpdateTime";
            this.chkUpdateTime.Size = new System.Drawing.Size(156, 16);
            this.chkUpdateTime.TabIndex = 6;
            this.chkUpdateTime.Text = "更新所有计算机授权时间";
            this.chkUpdateTime.UseVisualStyleBackColor = false;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(60, 24);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(286, 21);
            this.txtId.TabIndex = 1;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // butCle
            // 
            this.butCle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butCle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butCle.Location = new System.Drawing.Point(219, 196);
            this.butCle.Name = "butCle";
            this.butCle.Size = new System.Drawing.Size(86, 46);
            this.butCle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butCle.TabIndex = 5;
            this.butCle.Text = "取消";
            this.butCle.Click += new System.EventHandler(this.butCle_Click);
            // 
            // butOk
            // 
            this.butOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butOk.Location = new System.Drawing.Point(80, 196);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(86, 46);
            this.butOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butOk.TabIndex = 4;
            this.butOk.Text = "设置";
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // txtTime
            // 
            // 
            // 
            // 
            this.txtTime.Border.Class = "TextBoxBorder";
            this.txtTime.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTime.Location = new System.Drawing.Point(60, 115);
            this.txtTime.Name = "txtTime";
            this.txtTime.PreventEnterBeep = true;
            this.txtTime.Size = new System.Drawing.Size(286, 21);
            this.txtTime.TabIndex = 3;
            this.txtTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // txtSn
            // 
            // 
            // 
            // 
            this.txtSn.Border.Class = "TextBoxBorder";
            this.txtSn.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSn.Location = new System.Drawing.Point(60, 70);
            this.txtSn.Name = "txtSn";
            this.txtSn.PreventEnterBeep = true;
            this.txtSn.Size = new System.Drawing.Size(286, 21);
            this.txtSn.TabIndex = 2;
            this.txtSn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSn_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(18, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "期限：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(18, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "ID码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(16, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "SN码：";
            // 
            // tabPer
            // 
            this.tabPer.AttachedControl = this.tabControlPanel2;
            this.tabPer.Name = "tabPer";
            this.tabPer.Text = "计算机授权";
            // 
            // FrmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 295);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "权限、模块设置";
            this.Load += new System.EventHandler(this.FrmSet_Load);
            this.Shown += new System.EventHandler(this.FrmSet_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            this.tabControlPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabModule;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabPer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTime;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSn;
        private DevComponents.DotNetBar.ButtonX butCle;
        private DevComponents.DotNetBar.ButtonX butOk;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkUpdateTime;
        private System.Windows.Forms.TextBox txtModuleFileName;
        private System.Windows.Forms.PictureBox pict;
        private DevComponents.DotNetBar.ButtonX butModuleCle;
        private DevComponents.DotNetBar.ButtonX butModuleSet;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comModuleImg;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comModuleFz;
        private DevComponents.Editors.ComboItem c0;
        internal DevComponents.Editors.ComboItem c1;
        private DevComponents.Editors.ComboItem c2;
        private DevComponents.Editors.ComboItem c4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtModuleName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtModuleChName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox gr1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabModuleSet;
        private System.Windows.Forms.CheckedListBox chkMouduleCol;
        private System.Windows.Forms.CheckedListBox chkMouduleColSet;
        private DevComponents.DotNetBar.ButtonX butModulesysdel;
        private DevComponents.DotNetBar.ButtonX butModulesysadd;
        private DevComponents.DotNetBar.ButtonX butModulesysDelStop;
    }
}