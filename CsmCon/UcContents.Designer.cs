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
            this.LvContents = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colContentsSn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colContentsTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LvModule = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colDoduleTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDoduleCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.gr0.SuspendLayout();
            this.gr1.SuspendLayout();
            this.gr2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr0
            // 
            this.gr0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr0.Controls.Add(this.chkTspages);
            this.gr0.Controls.Add(this.panel1);
            this.gr0.Controls.Add(this.txtCode);
            this.gr0.Controls.Add(this.txtId);
            this.gr0.Controls.Add(this.chbModule);
            this.gr0.Controls.Add(this.label1);
            this.gr0.Location = new System.Drawing.Point(3, 3);
            this.gr0.Name = "gr0";
            this.gr0.Size = new System.Drawing.Size(470, 161);
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
            this.panel1.Size = new System.Drawing.Size(458, 113);
            this.panel1.TabIndex = 3;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(101, 14);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 21);
            this.txtCode.TabIndex = 2;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(52, 15);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(43, 21);
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
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(16, 126);
            this.butDel.Name = "butDel";
            this.butDel.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlD);
            this.butDel.Size = new System.Drawing.Size(56, 31);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 6;
            this.butDel.Text = "删除(D)";
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // butEdit
            // 
            this.butEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butEdit.Location = new System.Drawing.Point(16, 73);
            this.butEdit.Name = "butEdit";
            this.butEdit.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF);
            this.butEdit.Size = new System.Drawing.Size(55, 31);
            this.butEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butEdit.TabIndex = 5;
            this.butEdit.Text = "修改(F)";
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // butAdd
            // 
            this.butAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butAdd.Location = new System.Drawing.Point(16, 22);
            this.butAdd.Name = "butAdd";
            this.butAdd.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.butAdd.Size = new System.Drawing.Size(54, 31);
            this.butAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butAdd.TabIndex = 4;
            this.butAdd.Text = "新增(S)";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Controls.Add(this.LvContents);
            this.gr1.Controls.Add(this.LvModule);
            this.gr1.Location = new System.Drawing.Point(3, 170);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(384, 343);
            this.gr1.TabIndex = 2;
            this.gr1.TabStop = false;
            // 
            // LvContents
            // 
            this.LvContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.LvContents.Border.Class = "ListViewBorder";
            this.LvContents.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LvContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colContentsSn,
            this.colContentsTitle});
            this.LvContents.DisabledBackColor = System.Drawing.Color.Empty;
            this.LvContents.Font = new System.Drawing.Font("宋体", 12F);
            this.LvContents.FullRowSelect = true;
            this.LvContents.GridLines = true;
            this.LvContents.HideSelection = false;
            this.LvContents.Location = new System.Drawing.Point(5, 17);
            this.LvContents.Name = "LvContents";
            this.LvContents.Size = new System.Drawing.Size(205, 320);
            this.LvContents.TabIndex = 5;
            this.LvContents.TabStop = false;
            this.LvContents.UseCompatibleStateImageBehavior = false;
            this.LvContents.View = System.Windows.Forms.View.Details;
            this.LvContents.Click += new System.EventHandler(this.LvContents_Click);
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
            // LvModule
            // 
            this.LvModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.LvModule.Border.Class = "ListViewBorder";
            this.LvModule.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LvModule.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDoduleTitle,
            this.colDoduleCode});
            this.LvModule.DisabledBackColor = System.Drawing.Color.Empty;
            this.LvModule.GridLines = true;
            this.LvModule.Location = new System.Drawing.Point(207, 17);
            this.LvModule.Name = "LvModule";
            this.LvModule.Size = new System.Drawing.Size(171, 320);
            this.LvModule.TabIndex = 0;
            this.LvModule.TabStop = false;
            this.LvModule.UseCompatibleStateImageBehavior = false;
            this.LvModule.View = System.Windows.Forms.View.Details;
            // 
            // colDoduleTitle
            // 
            this.colDoduleTitle.Text = "标题";
            this.colDoduleTitle.Width = 150;
            // 
            // colDoduleCode
            // 
            this.colDoduleCode.Text = "代码";
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Controls.Add(this.butAdd);
            this.gr2.Controls.Add(this.butEdit);
            this.gr2.Controls.Add(this.butModule);
            this.gr2.Controls.Add(this.butDel);
            this.gr2.Location = new System.Drawing.Point(393, 170);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(80, 343);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            // 
            // UcContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gr2);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.gr0);
            this.Name = "UcContents";
            this.Size = new System.Drawing.Size(476, 516);
            this.Load += new System.EventHandler(this.UcContents_Load);
            this.gr0.ResumeLayout(false);
            this.gr0.PerformLayout();
            this.gr1.ResumeLayout(false);
            this.gr2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr0;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.CheckBox chbModule;
        private DevComponents.DotNetBar.ButtonX butModule;
        private DevComponents.DotNetBar.ButtonX butDel;
        private DevComponents.DotNetBar.ButtonX butEdit;
        private DevComponents.DotNetBar.ButtonX butAdd;
        private DevComponents.DotNetBar.Controls.ListViewEx LvModule;
        private System.Windows.Forms.ColumnHeader colDoduleTitle;
        private System.Windows.Forms.ColumnHeader colDoduleCode;
        private DevComponents.DotNetBar.Controls.ListViewEx LvContents;
        private System.Windows.Forms.ColumnHeader colContentsSn;
        private System.Windows.Forms.ColumnHeader colContentsTitle;
        private System.Windows.Forms.CheckBox chkTspages;
        public System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.Panel panel1;
    }
}
