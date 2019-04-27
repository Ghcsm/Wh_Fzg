namespace Csmggmm
{
    partial class FrmPwd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPwd));
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.butPwd = new DevComponents.DotNetBar.ButtonX();
            this.txtNewPwd2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOldpwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.Controls.Add(this.butPwd);
            this.gr1.Controls.Add(this.txtNewPwd2);
            this.gr1.Controls.Add(this.label3);
            this.gr1.Controls.Add(this.txtNewPwd);
            this.gr1.Controls.Add(this.label2);
            this.gr1.Controls.Add(this.txtOldpwd);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Location = new System.Drawing.Point(7, 29);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(276, 195);
            this.gr1.TabIndex = 1;
            this.gr1.TabStop = false;
            // 
            // butPwd
            // 
            this.butPwd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butPwd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butPwd.Location = new System.Drawing.Point(147, 139);
            this.butPwd.Name = "butPwd";
            this.butPwd.Size = new System.Drawing.Size(75, 36);
            this.butPwd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butPwd.TabIndex = 4;
            this.butPwd.Text = "确定";
            this.butPwd.Click += new System.EventHandler(this.butPwd_Click);
            // 
            // txtNewPwd2
            // 
            this.txtNewPwd2.Location = new System.Drawing.Point(93, 95);
            this.txtNewPwd2.Name = "txtNewPwd2";
            this.txtNewPwd2.PasswordChar = '*';
            this.txtNewPwd2.Size = new System.Drawing.Size(152, 21);
            this.txtNewPwd2.TabIndex = 3;
            this.txtNewPwd2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewPwd2_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "确认输入：";
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(93, 56);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.PasswordChar = '*';
            this.txtNewPwd.Size = new System.Drawing.Size(152, 21);
            this.txtNewPwd.TabIndex = 2;
            this.txtNewPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewPwd_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "新 密 码：";
            // 
            // txtOldpwd
            // 
            this.txtOldpwd.Location = new System.Drawing.Point(93, 20);
            this.txtOldpwd.Name = "txtOldpwd";
            this.txtOldpwd.PasswordChar = '*';
            this.txtOldpwd.Size = new System.Drawing.Size(152, 21);
            this.txtOldpwd.TabIndex = 1;
            this.txtOldpwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOldpwd_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "旧 密 码：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(293, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FrmPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(293, 240);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gr1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPwd";
            this.Text = "更改密码";
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.TextBox txtNewPwd2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOldpwd;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX butPwd;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}