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
            this.gArchSelect1 = new CsmCon.gArchSelect();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.gr3 = new System.Windows.Forms.GroupBox();
            this.chkInfo = new System.Windows.Forms.CheckBox();
            this.butSave = new DevComponents.DotNetBar.ButtonX();
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
            // gArchSelect1
            // 
            this.gArchSelect1.Archid = 0;
            this.gArchSelect1.ArchImgFile = null;
            this.gArchSelect1.ArchRegPages = 0;
            this.gArchSelect1.Archstat = null;
            this.gArchSelect1.Archtype = null;
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
            this.gr3.Controls.Add(this.chkInfo);
            this.gr3.Controls.Add(this.butSave);
            this.gr3.Location = new System.Drawing.Point(415, 484);
            this.gr3.Name = "gr3";
            this.gr3.Size = new System.Drawing.Size(693, 60);
            this.gr3.TabIndex = 3;
            this.gr3.TabStop = false;
            // 
            // chkInfo
            // 
            this.chkInfo.AutoSize = true;
            this.chkInfo.Location = new System.Drawing.Point(195, 29);
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
            this.butSave.Location = new System.Drawing.Point(37, 20);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 34);
            this.butSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSave.TabIndex = 0;
            this.butSave.Text = "保存";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // FrmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1120, 553);
            this.Controls.Add(this.gr3);
            this.Controls.Add(this.gr2);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
    }
}