namespace CsmCon
{
    partial class gArchSelect
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
            if (disposing && (components != null))
            {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gArchSelect));
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.LvData = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.c_sn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_boxsn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_archno = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_pages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_archid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_stat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_xyzd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImgList = new System.Windows.Forms.ImageList(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.butOk = new DevComponents.DotNetBar.ButtonX();
            this.txtBoxsn = new System.Windows.Forms.TextBox();
            this.comboxClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.radioClass = new System.Windows.Forms.RadioButton();
            this.radioBoxsn = new System.Windows.Forms.RadioButton();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.butLoad = new DevComponents.DotNetBar.ButtonX();
            this.butPageUpdate = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.gr1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.gr2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Controls.Add(this.LvData);
            this.gr1.Controls.Add(this.panelTop);
            this.gr1.Location = new System.Drawing.Point(3, 3);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(360, 416);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            this.gr1.Text = "盒内档案";
            // 
            // LvData
            // 
            this.LvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.LvData.Border.Class = "ListViewBorder";
            this.LvData.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c_sn,
            this.c_boxsn,
            this.c_archno,
            this.c_file,
            this.c_pages,
            this.c_archid,
            this.c_type,
            this.c_stat,
            this.c_xyzd});
            this.LvData.DisabledBackColor = System.Drawing.Color.Empty;
            this.LvData.FullRowSelect = true;
            this.LvData.GridLines = true;
            this.LvData.HideSelection = false;
            this.LvData.Location = new System.Drawing.Point(5, 63);
            this.LvData.Name = "LvData";
            this.LvData.Size = new System.Drawing.Size(349, 348);
            this.LvData.SmallImageList = this.ImgList;
            this.LvData.StateImageList = this.ImgList;
            this.LvData.TabIndex = 1;
            this.LvData.Tag = "6";
            this.LvData.UseCompatibleStateImageBehavior = false;
            this.LvData.View = System.Windows.Forms.View.Details;
            this.LvData.SelectedIndexChanged += new System.EventHandler(this.LvData_SelectedIndexChanged);
            this.LvData.Click += new System.EventHandler(this.LvData_Click);
            this.LvData.DoubleClick += new System.EventHandler(this.LvData_DoubleClick);
            this.LvData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvData_KeyPress);
            // 
            // c_sn
            // 
            this.c_sn.Text = "序号";
            this.c_sn.Width = 90;
            // 
            // c_boxsn
            // 
            this.c_boxsn.Text = "盒号";
            this.c_boxsn.Width = 70;
            // 
            // c_archno
            // 
            this.c_archno.Text = "卷号";
            this.c_archno.Width = 70;
            // 
            // c_file
            // 
            this.c_file.Text = "文件";
            this.c_file.Width = 150;
            // 
            // c_pages
            // 
            this.c_pages.Text = "页码";
            this.c_pages.Width = 30;
            // 
            // c_archid
            // 
            this.c_archid.Text = "ID";
            this.c_archid.Width = 50;
            // 
            // c_type
            // 
            this.c_type.Text = "ctype";
            this.c_type.Width = 0;
            // 
            // c_stat
            // 
            this.c_stat.Text = "stat";
            this.c_stat.Width = 0;
            // 
            // c_xyzd
            // 
            this.c_xyzd.Text = "xy";
            this.c_xyzd.Width = 0;
            // 
            // ImgList
            // 
            this.ImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgList.ImageStream")));
            this.ImgList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgList.Images.SetKeyName(0, "red.png");
            this.ImgList.Images.SetKeyName(1, "yel.png");
            this.ImgList.Images.SetKeyName(2, "lv.png");
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.butOk);
            this.panelTop.Controls.Add(this.txtBoxsn);
            this.panelTop.Controls.Add(this.comboxClass);
            this.panelTop.Controls.Add(this.radioClass);
            this.panelTop.Controls.Add(this.radioBoxsn);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(3, 17);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(354, 40);
            this.panelTop.TabIndex = 0;
            // 
            // butOk
            // 
            this.butOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butOk.Location = new System.Drawing.Point(291, 7);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(53, 26);
            this.butOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butOk.TabIndex = 4;
            this.butOk.Tag = "5";
            this.butOk.Text = "确定";
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            this.butOk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.butOk_KeyPress);
            // 
            // txtBoxsn
            // 
            this.txtBoxsn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxsn.Location = new System.Drawing.Point(193, 11);
            this.txtBoxsn.Name = "txtBoxsn";
            this.txtBoxsn.Size = new System.Drawing.Size(87, 21);
            this.txtBoxsn.TabIndex = 3;
            this.txtBoxsn.Tag = "4";
            this.txtBoxsn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxsn_KeyPress);
            // 
            // comboxClass
            // 
            this.comboxClass.DisplayMember = "Text";
            this.comboxClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboxClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxClass.Enabled = false;
            this.comboxClass.FormattingEnabled = true;
            this.comboxClass.ItemHeight = 15;
            this.comboxClass.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.comboxClass.Location = new System.Drawing.Point(111, 11);
            this.comboxClass.Name = "comboxClass";
            this.comboxClass.Size = new System.Drawing.Size(76, 21);
            this.comboxClass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboxClass.TabIndex = 2;
            this.comboxClass.Tag = "3";
            this.comboxClass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboxClass_KeyPress);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "案卷信息";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "目录信息";
            // 
            // radioClass
            // 
            this.radioClass.AutoSize = true;
            this.radioClass.Location = new System.Drawing.Point(61, 13);
            this.radioClass.Name = "radioClass";
            this.radioClass.Size = new System.Drawing.Size(47, 16);
            this.radioClass.TabIndex = 1;
            this.radioClass.Tag = "2";
            this.radioClass.Text = "类别";
            this.radioClass.UseVisualStyleBackColor = true;
            this.radioClass.Click += new System.EventHandler(this.radioClass_Click);
            // 
            // radioBoxsn
            // 
            this.radioBoxsn.AutoSize = true;
            this.radioBoxsn.Checked = true;
            this.radioBoxsn.Location = new System.Drawing.Point(12, 13);
            this.radioBoxsn.Name = "radioBoxsn";
            this.radioBoxsn.Size = new System.Drawing.Size(47, 16);
            this.radioBoxsn.TabIndex = 0;
            this.radioBoxsn.TabStop = true;
            this.radioBoxsn.Tag = "1";
            this.radioBoxsn.Text = "盒号";
            this.radioBoxsn.UseVisualStyleBackColor = true;
            this.radioBoxsn.Click += new System.EventHandler(this.radioBoxsn_Click);
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Controls.Add(this.butLoad);
            this.gr2.Controls.Add(this.butPageUpdate);
            this.gr2.Controls.Add(this.label1);
            this.gr2.Controls.Add(this.txtPages);
            this.gr2.Location = new System.Drawing.Point(0, 415);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(366, 47);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            // 
            // butLoad
            // 
            this.butLoad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butLoad.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butLoad.Location = new System.Drawing.Point(283, 12);
            this.butLoad.Name = "butLoad";
            this.butLoad.Size = new System.Drawing.Size(62, 30);
            this.butLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butLoad.TabIndex = 3;
            this.butLoad.Text = "加载";
            this.butLoad.Visible = false;
            this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
            // 
            // butPageUpdate
            // 
            this.butPageUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butPageUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butPageUpdate.Location = new System.Drawing.Point(194, 12);
            this.butPageUpdate.Name = "butPageUpdate";
            this.butPageUpdate.Size = new System.Drawing.Size(58, 30);
            this.butPageUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butPageUpdate.TabIndex = 2;
            this.butPageUpdate.Text = "更新";
            this.butPageUpdate.Click += new System.EventHandler(this.butPageUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "页码：";
            // 
            // txtPages
            // 
            this.txtPages.Location = new System.Drawing.Point(71, 15);
            this.txtPages.Name = "txtPages";
            this.txtPages.Size = new System.Drawing.Size(100, 21);
            this.txtPages.TabIndex = 1;
            this.txtPages.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPages_KeyPress);
            // 
            // gArchSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.gr2);
            this.Name = "gArchSelect";
            this.Size = new System.Drawing.Size(366, 462);
            this.Load += new System.EventHandler(this.gArchSelect_Load);
            this.gr1.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.gr2.ResumeLayout(false);
            this.gr2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.RadioButton radioBoxsn;
        private System.Windows.Forms.RadioButton radioClass;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboxClass;
        private System.Windows.Forms.TextBox txtBoxsn;
        private DevComponents.DotNetBar.ButtonX butOk;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX butPageUpdate;
        private System.Windows.Forms.ColumnHeader c_sn;
        private System.Windows.Forms.ColumnHeader c_boxsn;
        private System.Windows.Forms.ColumnHeader c_archno;
        private System.Windows.Forms.ColumnHeader c_file;
        private System.Windows.Forms.ColumnHeader c_archid;
        private System.Windows.Forms.ImageList ImgList;
        private System.Windows.Forms.ColumnHeader c_pages;
        private System.Windows.Forms.TextBox txtPages;
        public DevComponents.DotNetBar.ButtonX butLoad;
        private System.Windows.Forms.ColumnHeader c_type;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private System.Windows.Forms.ColumnHeader c_stat;
        public DevComponents.DotNetBar.Controls.ListViewEx LvData;
        private System.Windows.Forms.ColumnHeader c_xyzd;
    }
}
