namespace CsmCon
{
    partial class UcContents
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
            this.gr0 = new System.Windows.Forms.GroupBox();
            this.chkTspages = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.chbModule = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butModule = new DevComponents.DotNetBar.ButtonX();
            this.butDel = new DevComponents.DotNetBar.ButtonX();
            this.butEdit = new DevComponents.DotNetBar.ButtonX();
            this.butAdd = new DevComponents.DotNetBar.ButtonX();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.splitCont = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LvContents = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colContentsSn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colContentsTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.LvModule = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colDoduleCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDoduleTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDodulelx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gr0.SuspendLayout();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCont)).BeginInit();
            this.splitCont.Panel1.SuspendLayout();
            this.splitCont.Panel2.SuspendLayout();
            this.splitCont.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gr2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr0
            // 
            this.gr0.Controls.Add(this.chkTspages);
            this.gr0.Controls.Add(this.panel1);
            this.gr0.Controls.Add(this.txtCode);
            this.gr0.Controls.Add(this.txtId);
            this.gr0.Controls.Add(this.chbModule);
            this.gr0.Controls.Add(this.label1);
            this.gr0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr0.Location = new System.Drawing.Point(0, 0);
            this.gr0.Name = "gr0";
            this.gr0.Size = new System.Drawing.Size(541, 196);
            this.gr0.TabIndex = 0;
            this.gr0.TabStop = false;
            // 
            // chkTspages
            // 
            this.chkTspages.AutoSize = true;
            this.chkTspages.Location = new System.Drawing.Point(363, 18);
            this.chkTspages.Name = "chkTspages";
            this.chkTspages.Size = new System.Drawing.Size(96, 16);
            this.chkTspages.TabIndex = 0;
            this.chkTspages.TabStop = false;
            this.chkTspages.Text = "显示特殊页码";
            this.chkTspages.UseVisualStyleBackColor = true;
            this.chkTspages.CheckedChanged += new System.EventHandler(this.chkTspages_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(6, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 148);
            this.panel1.TabIndex = 3;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(110, 15);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 21);
            this.txtCode.TabIndex = 2;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            this.txtCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyUp);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(52, 15);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(52, 21);
            this.txtId.TabIndex = 1;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            // 
            // chbModule
            // 
            this.chbModule.AutoSize = true;
            this.chbModule.Checked = true;
            this.chbModule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbModule.Location = new System.Drawing.Point(264, 18);
            this.chbModule.Name = "chbModule";
            this.chbModule.Size = new System.Drawing.Size(72, 16);
            this.chbModule.TabIndex = 0;
            this.chbModule.TabStop = false;
            this.chbModule.Text = "显示模版";
            this.chbModule.UseVisualStyleBackColor = true;
            this.chbModule.CheckedChanged += new System.EventHandler(this.chbModule_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "代码:";
            // 
            // butModule
            // 
            this.butModule.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModule.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModule.Location = new System.Drawing.Point(16, 181);
            this.butModule.Name = "butModule";
            this.butModule.Size = new System.Drawing.Size(59, 31);
            this.butModule.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModule.TabIndex = 7;
            this.butModule.Text = "模版";
            this.butModule.Click += new System.EventHandler(this.butModule_Click);
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(16, 126);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(56, 31);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 6;
            this.butDel.Text = "删除";
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // butEdit
            // 
            this.butEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butEdit.Location = new System.Drawing.Point(16, 73);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(55, 31);
            this.butEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butEdit.TabIndex = 5;
            this.butEdit.Text = "修改";
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // butAdd
            // 
            this.butAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butAdd.Location = new System.Drawing.Point(16, 22);
            this.butAdd.Name = "butAdd";
            this.butAdd.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.butAdd.Size = new System.Drawing.Size(54, 31);
            this.butAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butAdd.TabIndex = 4;
            this.butAdd.Text = "新增(Z)";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Controls.Add(this.splitCont);
            this.gr1.Location = new System.Drawing.Point(6, 3);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(448, 307);
            this.gr1.TabIndex = 2;
            this.gr1.TabStop = false;
            // 
            // splitCont
            // 
            this.splitCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitCont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCont.Location = new System.Drawing.Point(3, 17);
            this.splitCont.Name = "splitCont";
            // 
            // splitCont.Panel1
            // 
            this.splitCont.Panel1.AutoScroll = true;
            this.splitCont.Panel1.Controls.Add(this.panel2);
            // 
            // splitCont.Panel2
            // 
            this.splitCont.Panel2.AutoScroll = true;
            this.splitCont.Panel2.Controls.Add(this.LvModule);
            this.splitCont.Size = new System.Drawing.Size(442, 287);
            this.splitCont.SplitterDistance = 187;
            this.splitCont.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.LvContents);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 285);
            this.panel2.TabIndex = 0;
            // 
            // LvContents
            // 
            // 
            // 
            // 
            this.LvContents.Border.Class = "ListViewBorder";
            this.LvContents.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LvContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colContentsSn,
            this.colContentsTitle});
            this.LvContents.DisabledBackColor = System.Drawing.Color.Empty;
            this.LvContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvContents.Font = new System.Drawing.Font("宋体", 11F);
            this.LvContents.FullRowSelect = true;
            this.LvContents.GridLines = true;
            this.LvContents.HideSelection = false;
            this.LvContents.Location = new System.Drawing.Point(0, 0);
            this.LvContents.Name = "LvContents";
            this.LvContents.Size = new System.Drawing.Size(185, 285);
            this.LvContents.SmallImageList = this.imageList1;
            this.LvContents.TabIndex = 5;
            this.LvContents.TabStop = false;
            this.LvContents.UseCompatibleStateImageBehavior = false;
            this.LvContents.View = System.Windows.Forms.View.Details;
            this.LvContents.SelectedIndexChanged += new System.EventHandler(this.LvContents_SelectedIndexChanged);
            this.LvContents.Click += new System.EventHandler(this.LvContents_Click);
            this.LvContents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LvContents_KeyDown);
            // 
            // colContentsSn
            // 
            this.colContentsSn.Text = "序号";
            // 
            // colContentsTitle
            // 
            this.colContentsTitle.Text = "id";
            this.colContentsTitle.Width = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 25);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // LvModule
            // 
            // 
            // 
            // 
            this.LvModule.Border.Class = "ListViewBorder";
            this.LvModule.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LvModule.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDoduleCode,
            this.colDoduleTitle,
            this.columnHeader1,
            this.colDodulelx});
            this.LvModule.DisabledBackColor = System.Drawing.Color.Empty;
            this.LvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvModule.FullRowSelect = true;
            this.LvModule.GridLines = true;
            this.LvModule.HideSelection = false;
            this.LvModule.Location = new System.Drawing.Point(0, 0);
            this.LvModule.Name = "LvModule";
            this.LvModule.Size = new System.Drawing.Size(249, 285);
            this.LvModule.TabIndex = 0;
            this.LvModule.TabStop = false;
            this.LvModule.UseCompatibleStateImageBehavior = false;
            this.LvModule.View = System.Windows.Forms.View.Details;
            this.LvModule.DoubleClick += new System.EventHandler(this.LvModule_DoubleClick);
            // 
            // colDoduleCode
            // 
            this.colDoduleCode.Text = "代码";
            // 
            // colDoduleTitle
            // 
            this.colDoduleTitle.Text = "标题";
            this.colDoduleTitle.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "标题类型";
            this.columnHeader1.Width = 80;
            // 
            // colDodulelx
            // 
            this.colDodulelx.Text = "类型";
            // 
            // gr2
            // 
            this.gr2.Controls.Add(this.butAdd);
            this.gr2.Controls.Add(this.butEdit);
            this.gr2.Controls.Add(this.butModule);
            this.gr2.Controls.Add(this.butDel);
            this.gr2.Dock = System.Windows.Forms.DockStyle.Right;
            this.gr2.Location = new System.Drawing.Point(461, 0);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(80, 316);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.gr0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.gr2);
            this.splitContainer1.Panel2.Controls.Add(this.gr1);
            this.splitContainer1.Size = new System.Drawing.Size(541, 516);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
            // 
            // UcContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "UcContents";
            this.Size = new System.Drawing.Size(541, 516);
            this.Load += new System.EventHandler(this.UcContents_Load);
            this.gr0.ResumeLayout(false);
            this.gr0.PerformLayout();
            this.gr1.ResumeLayout(false);
            this.splitCont.Panel1.ResumeLayout(false);
            this.splitCont.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCont)).EndInit();
            this.splitCont.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gr2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr0;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbModule;
        private DevComponents.DotNetBar.ButtonX butModule;
        private DevComponents.DotNetBar.ButtonX butDel;
        private DevComponents.DotNetBar.ButtonX butEdit;
        private DevComponents.DotNetBar.Controls.ListViewEx LvModule;
        private System.Windows.Forms.ColumnHeader colDoduleTitle;
        private System.Windows.Forms.ColumnHeader colDoduleCode;
        private System.Windows.Forms.ColumnHeader colContentsSn;
        private System.Windows.Forms.ColumnHeader colContentsTitle;
        private System.Windows.Forms.CheckBox chkTspages;
        public System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader colDodulelx;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.SplitContainer splitCont;
        public System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public DevComponents.DotNetBar.Controls.ListViewEx LvContents;
        public DevComponents.DotNetBar.ButtonX butAdd;
        public System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.Panel panel2;
    }
}
