namespace Bgkj
{
    partial class FrmUI
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUI));
            this.panle = new System.Windows.Forms.Panel();
            this.lbinfo = new System.Windows.Forms.Label();
            this.panleLogo = new System.Windows.Forms.Panel();
            this.labPwd = new System.Windows.Forms.Label();
            this.labUser = new System.Windows.Forms.Label();
            this.txtUser = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtPwd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnCle = new DevComponents.DotNetBar.ButtonX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.panle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panle
            // 
            this.panle.BackgroundImage = global::Bgkj.Properties.Resources.登录界面;
            this.panle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panle.Controls.Add(this.lbinfo);
            this.panle.Controls.Add(this.panleLogo);
            this.panle.Controls.Add(this.labPwd);
            this.panle.Controls.Add(this.labUser);
            this.panle.Controls.Add(this.txtUser);
            this.panle.Controls.Add(this.txtPwd);
            this.panle.Controls.Add(this.btnCle);
            this.panle.Controls.Add(this.btnOk);
            this.panle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panle.Location = new System.Drawing.Point(0, 0);
            this.panle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panle.Name = "panle";
            this.panle.Size = new System.Drawing.Size(824, 474);
            this.panle.TabIndex = 2;
            this.panle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panle_MouseDown);
            this.panle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panle_MouseMove);
            // 
            // lbinfo
            // 
            this.lbinfo.AutoSize = true;
            this.lbinfo.BackColor = System.Drawing.Color.Transparent;
            this.lbinfo.ForeColor = System.Drawing.Color.White;
            this.lbinfo.Location = new System.Drawing.Point(597, 448);
            this.lbinfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbinfo.Name = "lbinfo";
            this.lbinfo.Size = new System.Drawing.Size(195, 15);
            this.lbinfo.TabIndex = 6;
            this.lbinfo.Text = "数据库连接失败,请检查网络";
            this.lbinfo.Visible = false;
            // 
            // panleLogo
            // 
            this.panleLogo.BackColor = System.Drawing.Color.Transparent;
            this.panleLogo.BackgroundImage = global::Bgkj.Properties.Resources._11;
            this.panleLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panleLogo.Location = new System.Drawing.Point(100, 68);
            this.panleLogo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panleLogo.Name = "panleLogo";
            this.panleLogo.Size = new System.Drawing.Size(415, 64);
            this.panleLogo.TabIndex = 5;
            // 
            // labPwd
            // 
            this.labPwd.AutoSize = true;
            this.labPwd.BackColor = System.Drawing.Color.Transparent;
            this.labPwd.Location = new System.Drawing.Point(521, 342);
            this.labPwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labPwd.Name = "labPwd";
            this.labPwd.Size = new System.Drawing.Size(52, 15);
            this.labPwd.TabIndex = 4;
            this.labPwd.Text = "密码：";
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.BackColor = System.Drawing.Color.Transparent;
            this.labUser.Location = new System.Drawing.Point(521, 305);
            this.labUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(52, 15);
            this.labUser.TabIndex = 4;
            this.labUser.Text = "用户：";
            // 
            // txtUser
            // 
            this.txtUser.DisplayMember = "Text";
            this.txtUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.txtUser.FormattingEnabled = true;
            this.txtUser.ItemHeight = 15;
            this.txtUser.Location = new System.Drawing.Point(592, 299);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(151, 21);
            this.txtUser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtUser.TabIndex = 1;
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            // 
            // txtPwd
            // 
            // 
            // 
            // 
            this.txtPwd.Border.Class = "TextBoxBorder";
            this.txtPwd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPwd.Location = new System.Drawing.Point(592, 335);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.PreventEnterBeep = true;
            this.txtPwd.Size = new System.Drawing.Size(152, 25);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // btnCle
            // 
            this.btnCle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCle.Location = new System.Drawing.Point(668, 385);
            this.btnCle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCle.Name = "btnCle";
            this.btnCle.Size = new System.Drawing.Size(96, 44);
            this.btnCle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCle.TabIndex = 4;
            this.btnCle.Text = "取消";
            this.btnCle.Click += new System.EventHandler(this.btnCle_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(537, 385);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(93, 44);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 474);
            this.Controls.Add(this.panle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "档案管理系统";
            this.Load += new System.EventHandler(this.FrmUI_Load);
            this.Shown += new System.EventHandler(this.FrmUI_Shown);
            this.panle.ResumeLayout(false);
            this.panle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.ButtonX btnCle;
        private System.Windows.Forms.Panel panle;
        private System.Windows.Forms.Label labPwd;
        private System.Windows.Forms.Label labUser;
        private DevComponents.DotNetBar.Controls.ComboBoxEx txtUser;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPwd;
        private System.Windows.Forms.Panel panleLogo;
        private System.Windows.Forms.Label lbinfo;
    }
}

