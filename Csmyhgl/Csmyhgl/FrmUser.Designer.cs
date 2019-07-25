namespace Csmyhgl
{
    partial class FrmUser
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.grpan = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gr4 = new System.Windows.Forms.GroupBox();
            this.lvOtherModule = new System.Windows.Forms.CheckedListBox();
            this.gr3 = new System.Windows.Forms.GroupBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.trModule = new System.Windows.Forms.TreeView();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.butDel = new DevComponents.DotNetBar.ButtonX();
            this.butUpdate = new DevComponents.DotNetBar.ButtonX();
            this.butAdd = new DevComponents.DotNetBar.ButtonX();
            this.txtBz = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtPwd2 = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpan.SuspendLayout();
            this.gr4.SuspendLayout();
            this.gr3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gr2.SuspendLayout();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(872, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // grpan
            // 
            this.grpan.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpan.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpan.Controls.Add(this.gr4);
            this.grpan.Controls.Add(this.gr3);
            this.grpan.Controls.Add(this.gr2);
            this.grpan.Controls.Add(this.gr1);
            this.grpan.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpan.Location = new System.Drawing.Point(0, 25);
            this.grpan.Name = "grpan";
            this.grpan.Size = new System.Drawing.Size(872, 422);
            // 
            // 
            // 
            this.grpan.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpan.Style.BackColorGradientAngle = 90;
            this.grpan.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpan.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpan.Style.BorderBottomWidth = 1;
            this.grpan.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpan.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpan.Style.BorderLeftWidth = 1;
            this.grpan.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpan.Style.BorderRightWidth = 1;
            this.grpan.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpan.Style.BorderTopWidth = 1;
            this.grpan.Style.CornerDiameter = 4;
            this.grpan.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpan.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpan.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpan.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpan.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpan.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpan.TabIndex = 1;
            // 
            // gr4
            // 
            this.gr4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gr4.BackColor = System.Drawing.Color.Transparent;
            this.gr4.Controls.Add(this.lvOtherModule);
            this.gr4.Location = new System.Drawing.Point(271, 271);
            this.gr4.Name = "gr4";
            this.gr4.Size = new System.Drawing.Size(187, 136);
            this.gr4.TabIndex = 3;
            this.gr4.TabStop = false;
            this.gr4.Text = "特殊权限";
            // 
            // lvOtherModule
            // 
            this.lvOtherModule.CheckOnClick = true;
            this.lvOtherModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvOtherModule.FormattingEnabled = true;
            this.lvOtherModule.Location = new System.Drawing.Point(3, 17);
            this.lvOtherModule.Name = "lvOtherModule";
            this.lvOtherModule.Size = new System.Drawing.Size(181, 116);
            this.lvOtherModule.TabIndex = 11;
            // 
            // gr3
            // 
            this.gr3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr3.BackColor = System.Drawing.Color.Transparent;
            this.gr3.Controls.Add(this.dgvData);
            this.gr3.Location = new System.Drawing.Point(464, 3);
            this.gr3.Name = "gr3";
            this.gr3.Size = new System.Drawing.Size(393, 404);
            this.gr3.TabIndex = 2;
            this.gr3.TabStop = false;
            this.gr3.Text = "用户列表";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 17);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(387, 384);
            this.dgvData.TabIndex = 12;
            this.dgvData.Click += new System.EventHandler(this.dgvData_Click);
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr2.BackColor = System.Drawing.Color.Transparent;
            this.gr2.Controls.Add(this.trModule);
            this.gr2.Location = new System.Drawing.Point(271, 3);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(187, 262);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            this.gr2.Text = "模块权限";
            // 
            // trModule
            // 
            this.trModule.CheckBoxes = true;
            this.trModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trModule.Font = new System.Drawing.Font("宋体", 10F);
            this.trModule.Location = new System.Drawing.Point(3, 17);
            this.trModule.Name = "trModule";
            this.trModule.Size = new System.Drawing.Size(181, 242);
            this.trModule.TabIndex = 11;
            this.trModule.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trModule_AfterCheck);
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Controls.Add(this.butDel);
            this.gr1.Controls.Add(this.butUpdate);
            this.gr1.Controls.Add(this.butAdd);
            this.gr1.Controls.Add(this.txtBz);
            this.gr1.Controls.Add(this.txtTime);
            this.gr1.Controls.Add(this.txtPhone);
            this.gr1.Controls.Add(this.txtPwd2);
            this.gr1.Controls.Add(this.txtPwd);
            this.gr1.Controls.Add(this.txtUser);
            this.gr1.Controls.Add(this.label6);
            this.gr1.Controls.Add(this.label5);
            this.gr1.Controls.Add(this.label4);
            this.gr1.Controls.Add(this.label3);
            this.gr1.Controls.Add(this.label2);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Location = new System.Drawing.Point(3, 3);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(260, 404);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            this.gr1.Text = "用户信息";
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(175, 345);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(59, 42);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 9;
            this.butDel.Text = "删除";
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // butUpdate
            // 
            this.butUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butUpdate.Location = new System.Drawing.Point(96, 345);
            this.butUpdate.Name = "butUpdate";
            this.butUpdate.Size = new System.Drawing.Size(59, 42);
            this.butUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butUpdate.TabIndex = 8;
            this.butUpdate.Text = "修改";
            this.butUpdate.Click += new System.EventHandler(this.butUpdate_Click);
            // 
            // butAdd
            // 
            this.butAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butAdd.Location = new System.Drawing.Point(16, 345);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(59, 42);
            this.butAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butAdd.TabIndex = 7;
            this.butAdd.Text = "添加";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // txtBz
            // 
            this.txtBz.Location = new System.Drawing.Point(67, 221);
            this.txtBz.Multiline = true;
            this.txtBz.Name = "txtBz";
            this.txtBz.Size = new System.Drawing.Size(167, 95);
            this.txtBz.TabIndex = 6;
            this.txtBz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBz_KeyPress);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(67, 184);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(167, 21);
            this.txtTime.TabIndex = 5;
            this.txtTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(67, 147);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(167, 21);
            this.txtPhone.TabIndex = 4;
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // txtPwd2
            // 
            this.txtPwd2.Location = new System.Drawing.Point(67, 109);
            this.txtPwd2.Name = "txtPwd2";
            this.txtPwd2.PasswordChar = '*';
            this.txtPwd2.Size = new System.Drawing.Size(167, 21);
            this.txtPwd2.TabIndex = 3;
            this.txtPwd2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd2_KeyPress);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(67, 71);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(167, 21);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(67, 33);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(167, 21);
            this.txtUser.TabIndex = 1;
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "备  注:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "时  间:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "电  话:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "重  复:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密  码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名:";
            // 
            // FrmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 447);
            this.Controls.Add(this.grpan);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUser";
            this.Text = "FrmUser";
            this.Shown += new System.EventHandler(this.FrmUser_Shown);
            this.grpan.ResumeLayout(false);
            this.gr4.ResumeLayout(false);
            this.gr3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gr2.ResumeLayout(false);
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.Controls.GroupPanel grpan;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtPwd2;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBz;
        private DevComponents.DotNetBar.ButtonX butAdd;
        private DevComponents.DotNetBar.ButtonX butDel;
        private DevComponents.DotNetBar.ButtonX butUpdate;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.GroupBox gr3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox gr4;
        private System.Windows.Forms.CheckedListBox lvOtherModule;
        private System.Windows.Forms.TreeView trModule;
    }
}