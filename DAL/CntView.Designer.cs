namespace DAL
{
    partial class CntView
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ml_sn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listview_Ml = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.ml_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_Page = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_zerenzhe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_Leix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_FileDaizi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_FileBiansn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_miji = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_chengwennian = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_chengwenyue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_chengwentime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_dengji = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ml_MMduan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.but_moban = new DevComponents.DotNetBar.ButtonX();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.check_moban = new System.Windows.Forms.CheckBox();
            this.btnModel = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.btnEdit = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd1 = new DevComponents.DotNetBar.ButtonX();
            this.txtPage = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.com_0 = new DevComponents.Editors.ComboItem();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.listview_moban = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.moban_code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.moban_title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ml_sn
            // 
            this.ml_sn.Text = "序号";
            this.ml_sn.Width = 50;
            // 
            // listview_Ml
            // 
            this.listview_Ml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listview_Ml.Border.Class = "ListViewBorder";
            this.listview_Ml.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listview_Ml.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ml_sn,
            this.ml_Title,
            this.ml_Page,
            this.ml_zerenzhe,
            this.ml_Leix,
            this.ml_FileDaizi,
            this.ml_FileBiansn,
            this.ml_miji,
            this.ml_chengwennian,
            this.ml_chengwenyue,
            this.ml_chengwentime,
            this.ml_dengji,
            this.ml_ID,
            this.ml_MMduan});
            this.listview_Ml.DisabledBackColor = System.Drawing.Color.Empty;
            this.listview_Ml.FullRowSelect = true;
            this.listview_Ml.GridLines = true;
            this.listview_Ml.HideSelection = false;
            this.listview_Ml.Location = new System.Drawing.Point(3, 119);
            this.listview_Ml.Name = "listview_Ml";
            this.listview_Ml.ShowItemToolTips = true;
            this.listview_Ml.Size = new System.Drawing.Size(246, 398);
            this.listview_Ml.SmallImageList = this.imageList1;
            this.listview_Ml.TabIndex = 10;
            this.listview_Ml.UseCompatibleStateImageBehavior = false;
            this.listview_Ml.View = System.Windows.Forms.View.Details;
            this.listview_Ml.Click += new System.EventHandler(this.listview_Ml_Click);
            this.listview_Ml.DoubleClick += new System.EventHandler(this.listview_Ml_DoubleClick);
            // 
            // ml_Title
            // 
            this.ml_Title.Text = "标题";
            this.ml_Title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ml_Title.Width = 200;
            // 
            // ml_Page
            // 
            this.ml_Page.Text = "页码";
            this.ml_Page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ml_Page.Width = 40;
            // 
            // ml_zerenzhe
            // 
            this.ml_zerenzhe.Text = "件数";
            this.ml_zerenzhe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ml_zerenzhe.Width = 90;
            // 
            // ml_Leix
            // 
            this.ml_Leix.Text = "档号";
            this.ml_Leix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ml_Leix.Width = 0;
            // 
            // ml_FileDaizi
            // 
            this.ml_FileDaizi.Text = "案卷号";
            this.ml_FileDaizi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ml_FileDaizi.Width = 0;
            // 
            // ml_FileBiansn
            // 
            this.ml_FileBiansn.Text = "序号";
            this.ml_FileBiansn.Width = 0;
            // 
            // ml_miji
            // 
            this.ml_miji.Text = "密级";
            this.ml_miji.Width = 100;
            // 
            // ml_chengwennian
            // 
            this.ml_chengwennian.Text = "保管期";
            this.ml_chengwennian.Width = 0;
            // 
            // ml_chengwenyue
            // 
            this.ml_chengwenyue.Text = "成文日期";
            this.ml_chengwenyue.Width = 0;
            // 
            // ml_chengwentime
            // 
            this.ml_chengwentime.Text = "文件编号";
            this.ml_chengwentime.Width = 0;
            // 
            // ml_dengji
            // 
            this.ml_dengji.Text = "目录ID";
            this.ml_dengji.Width = 0;
            // 
            // ml_ID
            // 
            this.ml_ID.Text = "目录ID";
            this.ml_ID.Width = 0;
            // 
            // ml_MMduan
            // 
            this.ml_MMduan.Text = "密级段";
            this.ml_MMduan.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(28, 28);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.but_moban);
            this.panel2.Controls.Add(this.txtTitle);
            this.panel2.Controls.Add(this.check_moban);
            this.panel2.Controls.Add(this.btnModel);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd1);
            this.panel2.Controls.Add(this.txtPage);
            this.panel2.Controls.Add(this.labelX4);
            this.panel2.Controls.Add(this.labelX3);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.txType);
            this.panel2.Controls.Add(this.labelX2);
            this.panel2.Controls.Add(this.cboType);
            this.panel2.Controls.Add(this.labelX1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 110);
            this.panel2.TabIndex = 54;
            // 
            // but_moban
            // 
            this.but_moban.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.but_moban.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.but_moban.Location = new System.Drawing.Point(279, 75);
            this.but_moban.Name = "but_moban";
            this.but_moban.Size = new System.Drawing.Size(74, 25);
            this.but_moban.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.but_moban.TabIndex = 16;
            this.but_moban.TabStop = false;
            this.but_moban.Text = "模板";
            this.but_moban.Click += new System.EventHandler(this.but_moban_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(48, 44);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(241, 21);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTitle_KeyPress);
            // 
            // check_moban
            // 
            this.check_moban.AutoSize = true;
            this.check_moban.Checked = true;
            this.check_moban.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_moban.Location = new System.Drawing.Point(370, 84);
            this.check_moban.Name = "check_moban";
            this.check_moban.Size = new System.Drawing.Size(72, 16);
            this.check_moban.TabIndex = 17;
            this.check_moban.Text = "显示目录";
            this.check_moban.UseVisualStyleBackColor = true;
            this.check_moban.CheckedChanged += new System.EventHandler(this.check_moban_CheckedChanged);
            // 
            // btnModel
            // 
            this.btnModel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnModel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModel.Enabled = false;
            this.btnModel.Location = new System.Drawing.Point(144, 75);
            this.btnModel.Name = "btnModel";
            this.btnModel.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.btnModel.Size = new System.Drawing.Size(57, 25);
            this.btnModel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnModel.TabIndex = 14;
            this.btnModel.Text = "提交(Z)";
            this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDel.Location = new System.Drawing.Point(210, 75);
            this.btnDel.Name = "btnDel";
            this.btnDel.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF);
            this.btnDel.Size = new System.Drawing.Size(57, 25);
            this.btnDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDel.TabIndex = 15;
            this.btnDel.Text = "删除(F)";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEdit.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEdit.Location = new System.Drawing.Point(72, 75);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlD);
            this.btnEdit.Size = new System.Drawing.Size(64, 25);
            this.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "修改(D)";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd1
            // 
            this.btnAdd1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd1.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdd1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd1.Location = new System.Drawing.Point(8, 75);
            this.btnAdd1.Name = "btnAdd1";
            this.btnAdd1.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.btnAdd1.Size = new System.Drawing.Size(57, 25);
            this.btnAdd1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd1.TabIndex = 12;
            this.btnAdd1.Text = "新增(S)";
            this.btnAdd1.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtPage
            // 
            // 
            // 
            // 
            this.txtPage.Border.Class = "TextBoxBorder";
            this.txtPage.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPage.Location = new System.Drawing.Point(372, 44);
            this.txtPage.Name = "txtPage";
            this.txtPage.PreventEnterBeep = true;
            this.txtPage.Size = new System.Drawing.Size(48, 21);
            this.txtPage.TabIndex = 5;
            this.txtPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txType_KeyDown);
            this.txtPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txType_KeyPress);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(317, 44);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(36, 23);
            this.labelX4.TabIndex = 82;
            this.labelX4.Text = "页码:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(4, 45);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(38, 23);
            this.labelX3.TabIndex = 81;
            this.labelX3.Text = "标题:";
            // 
            // txtCode
            // 
            // 
            // 
            // 
            this.txtCode.Border.Class = "TextBoxBorder";
            this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCode.Location = new System.Drawing.Point(89, 5);
            this.txtCode.Name = "txtCode";
            this.txtCode.PreventEnterBeep = true;
            this.txtCode.Size = new System.Drawing.Size(119, 21);
            this.txtCode.TabIndex = 1;
            this.txtCode.Enter += new System.EventHandler(this.txtCode_Enter);
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txType_KeyPress);
            // 
            // txType
            // 
            // 
            // 
            // 
            this.txType.Border.Class = "TextBoxBorder";
            this.txType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txType.Location = new System.Drawing.Point(48, 5);
            this.txType.Name = "txType";
            this.txType.PreventEnterBeep = true;
            this.txType.Size = new System.Drawing.Size(32, 21);
            this.txType.TabIndex = 0;
            this.txType.Text = "01";
            this.txType.Enter += new System.EventHandler(this.txType_Enter);
            this.txType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txType_KeyDown);
            this.txType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txType_KeyPress);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(4, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(38, 23);
            this.labelX2.TabIndex = 79;
            this.labelX2.Text = "代码:";
            // 
            // cboType
            // 
            this.cboType.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cboType.DisplayMember = "Text";
            this.cboType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboType.FormattingEnabled = true;
            this.cboType.ItemHeight = 15;
            this.cboType.Items.AddRange(new object[] {
            this.com_0});
            this.cboType.Location = new System.Drawing.Point(277, 6);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(147, 21);
            this.cboType.TabIndex = 3;
            this.cboType.Text = "1";
            this.cboType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboType_KeyPress);
            // 
            // com_0
            // 
            this.com_0.Text = "1";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(223, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(50, 23);
            this.labelX1.TabIndex = 78;
            this.labelX1.Text = "件数:";
            // 
            // listview_moban
            // 
            this.listview_moban.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listview_moban.Border.Class = "ListViewBorder";
            this.listview_moban.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listview_moban.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.moban_code,
            this.moban_title});
            this.listview_moban.DisabledBackColor = System.Drawing.Color.Empty;
            this.listview_moban.FullRowSelect = true;
            this.listview_moban.GridLines = true;
            this.listview_moban.HideSelection = false;
            this.listview_moban.Location = new System.Drawing.Point(244, 119);
            this.listview_moban.Name = "listview_moban";
            this.listview_moban.Size = new System.Drawing.Size(199, 398);
            this.listview_moban.TabIndex = 11;
            this.listview_moban.UseCompatibleStateImageBehavior = false;
            this.listview_moban.View = System.Windows.Forms.View.Details;
            this.listview_moban.DoubleClick += new System.EventHandler(this.listview_moban_DoubleClick);
            // 
            // moban_code
            // 
            this.moban_code.Text = "代码";
            this.moban_code.Width = 0;
            // 
            // moban_title
            // 
            this.moban_title.Text = "标题";
            this.moban_title.Width = 300;
            // 
            // CntView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listview_moban);
            this.Controls.Add(this.listview_Ml);
            this.Controls.Add(this.panel2);
            this.Name = "CntView";
            this.Size = new System.Drawing.Size(451, 520);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txType;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboType;
        public DevComponents.DotNetBar.Controls.TextBoxX txtPage;
        public DevComponents.DotNetBar.ButtonX btnModel;
        public DevComponents.DotNetBar.ButtonX btnDel;
        public DevComponents.DotNetBar.ButtonX btnEdit;
        public DevComponents.DotNetBar.ButtonX btnAdd1;
        private DevComponents.DotNetBar.Controls.ListViewEx listview_Ml;
        private System.Windows.Forms.ColumnHeader ml_Title;
        private System.Windows.Forms.ColumnHeader ml_Page;
        private System.Windows.Forms.ColumnHeader ml_ID;
        private DevComponents.DotNetBar.Controls.ListViewEx listview_moban;
        private System.Windows.Forms.ColumnHeader moban_code;
        private System.Windows.Forms.ColumnHeader moban_title;
        public System.Windows.Forms.CheckBox check_moban;
        private System.Windows.Forms.ColumnHeader ml_zerenzhe;
        private System.Windows.Forms.ColumnHeader ml_Leix;
        private System.Windows.Forms.ColumnHeader ml_FileDaizi;
        private System.Windows.Forms.ColumnHeader ml_FileBiansn;
        private System.Windows.Forms.ColumnHeader ml_chengwennian;
        private System.Windows.Forms.ColumnHeader ml_chengwenyue;
        private System.Windows.Forms.ColumnHeader ml_chengwentime;
        private System.Windows.Forms.ColumnHeader ml_miji;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ColumnHeader ml_dengji;
        public DevComponents.DotNetBar.ButtonX but_moban;
        private System.Windows.Forms.ColumnHeader ml_MMduan;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader ml_sn;
        public DevComponents.DotNetBar.Controls.TextBoxX txtCode;
        private DevComponents.Editors.ComboItem com_0;
    }
}
