namespace Csmxxbl
{
    partial class FrmInfo
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
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.gr3 = new System.Windows.Forms.GroupBox();
            this.labsx = new System.Windows.Forms.Label();
            this.butBL = new DevComponents.DotNetBar.ButtonX();
            this.butDel = new DevComponents.DotNetBar.ButtonX();
            this.chkInfo = new System.Windows.Forms.CheckBox();
            this.butSave = new DevComponents.DotNetBar.ButtonX();
            this.gArchSelect1 = new CsmCon.gArchSelect();
            this.butZd = new DevComponents.DotNetBar.ButtonX();
            this.gr1.SuspendLayout();
            this.gr3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1120, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr1.Controls.Add(this.gArchSelect1);
            this.gr1.Location = new System.Drawing.Point(0, 28);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(409, 519);
            this.gr1.TabIndex = 1;
            this.gr1.TabStop = false;
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Location = new System.Drawing.Point(415, 28);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(696, 447);
            this.gr2.TabIndex = 2;
            this.gr2.TabStop = false;
            this.gr2.Text = "案卷信息";
            // 
            // gr3
            // 
            this.gr3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr3.Controls.Add(this.butZd);
            this.gr3.Controls.Add(this.labsx);
            this.gr3.Controls.Add(this.butBL);
            this.gr3.Controls.Add(this.butDel);
            this.gr3.Controls.Add(this.chkInfo);
            this.gr3.Controls.Add(this.butSave);
            this.gr3.Location = new System.Drawing.Point(415, 484);
            this.gr3.Name = "gr3";
            this.gr3.Size = new System.Drawing.Size(693, 60);
            this.gr3.TabIndex = 3;
            this.gr3.TabStop = false;
            // 
            // labsx
            // 
            this.labsx.AutoSize = true;
            this.labsx.ForeColor = System.Drawing.Color.Red;
            this.labsx.Location = new System.Drawing.Point(628, 30);
            this.labsx.Name = "labsx";
            this.labsx.Size = new System.Drawing.Size(47, 12);
            this.labsx.TabIndex = 0;
            this.labsx.Text = "已录0手";
            // 
            // butBL
            // 
            this.butBL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butBL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butBL.Location = new System.Drawing.Point(272, 18);
            this.butBL.Name = "butBL";
            this.butBL.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.butBL.Size = new System.Drawing.Size(77, 36);
            this.butBL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butBL.TabIndex = 3;
            this.butBL.Text = "补录页码";
            this.butBL.Click += new System.EventHandler(this.butBL_Click);
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(155, 18);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(77, 36);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 2;
            this.butDel.Text = "删除";
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // chkInfo
            // 
            this.chkInfo.AutoSize = true;
            this.chkInfo.Location = new System.Drawing.Point(509, 30);
            this.chkInfo.Name = "chkInfo";
            this.chkInfo.Size = new System.Drawing.Size(72, 16);
            this.chkInfo.TabIndex = 1;
            this.chkInfo.Text = "二录信息";
            this.chkInfo.UseVisualStyleBackColor = true;
            this.chkInfo.CheckedChanged += new System.EventHandler(this.chkInfo_CheckedChanged);
            this.chkInfo.Click += new System.EventHandler(this.chkInfo_Click);
            // 
            // butSave
            // 
            this.butSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butSave.Location = new System.Drawing.Point(43, 18);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(77, 36);
            this.butSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSave.TabIndex = 0;
            this.butSave.Text = "保存";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // gArchSelect1
            // 
            this.gArchSelect1.Archid = 0;
            this.gArchSelect1.ArchImgFile = null;
            this.gArchSelect1.ArchNo = null;
            this.gArchSelect1.ArchPos = null;
            this.gArchSelect1.ArchRegPages = 0;
            this.gArchSelect1.Archstat = null;
            this.gArchSelect1.Archtype = null;
            this.gArchSelect1.ArchXqzt = null;
            this.gArchSelect1.Archxystat = null;
            this.gArchSelect1.Boxsn = 0;
            this.gArchSelect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gArchSelect1.GotoPages = false;
            this.gArchSelect1.LoadFileBoole = false;
            this.gArchSelect1.Location = new System.Drawing.Point(3, 17);
            this.gArchSelect1.Name = "gArchSelect1";
            this.gArchSelect1.PagesEnd = false;
            this.gArchSelect1.Size = new System.Drawing.Size(403, 499);
            this.gArchSelect1.TabIndex = 0;
            this.gArchSelect1.LineClickLoadInfo += new CsmCon.gArchSelect.ArchSelectHandle(this.gArchSelect1_LineClickLoadInfo);
            this.gArchSelect1.LineFocus += new CsmCon.gArchSelect.ArchSelectHandleFocus(this.gArchSelect1_LineFocus);
            this.gArchSelect1.LineGetInfo += new CsmCon.gArchSelect.ArchSelectHandleGetInfo(this.gArchSelect1_LineGetInfo);
            // 
            // butZd
            // 
            this.butZd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butZd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butZd.Location = new System.Drawing.Point(389, 17);
            this.butZd.Name = "butZd";
            this.butZd.Size = new System.Drawing.Size(75, 37);
            this.butZd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butZd.TabIndex = 4;
            this.butZd.Text = "装订录入";
            this.butZd.Click += new System.EventHandler(this.butZd_Click);
            // 
            // FrmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1120, 553);
            this.Controls.Add(this.gr3);
            this.Controls.Add(this.gr2);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInfo";
            this.Shown += new System.EventHandler(this.FrmInfo_Shown);
            this.gr1.ResumeLayout(false);
            this.gr3.ResumeLayout(false);
            this.gr3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox gr1;
        private CsmCon.gArchSelect gArchSelect1;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.GroupBox gr3;
        private DevComponents.DotNetBar.ButtonX butSave;
        private System.Windows.Forms.CheckBox chkInfo;
        private DevComponents.DotNetBar.ButtonX butDel;
        private DevComponents.DotNetBar.ButtonX butBL;
        private System.Windows.Forms.Label labsx;
        private DevComponents.DotNetBar.ButtonX butZd;
    }
}