﻿namespace Csmmldj
{
    partial class Frmmldj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmmldj));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.splitCont = new DevComponents.DotNetBar.Controls.CollapsibleSplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butSaveInfo = new DevComponents.DotNetBar.ButtonX();
            this.grinfo = new System.Windows.Forms.GroupBox();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tools_user = new System.Windows.Forms.ToolStripStatusLabel();
            this.tools_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.gArchSelect1 = new CsmCon.gArchSelect();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCont)).BeginInit();
            this.splitCont.Panel1.SuspendLayout();
            this.splitCont.Panel2.SuspendLayout();
            this.splitCont.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.ico");
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1342, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr1
            // 
            this.gr1.Controls.Add(this.splitCont);
            this.gr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr1.Location = new System.Drawing.Point(0, 0);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(392, 609);
            this.gr1.TabIndex = 1;
            this.gr1.TabStop = false;
            // 
            // splitCont
            // 
            this.splitCont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCont.Location = new System.Drawing.Point(3, 21);
            this.splitCont.Name = "splitCont";
            this.splitCont.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCont.Panel1
            // 
            this.splitCont.Panel1.Controls.Add(this.gArchSelect1);
            // 
            // splitCont.Panel2
            // 
            this.splitCont.Panel2.Controls.Add(this.groupBox1);
            this.splitCont.Panel2.Controls.Add(this.grinfo);
            this.splitCont.Size = new System.Drawing.Size(386, 585);
            this.splitCont.SplitterDistance = 345;
            this.splitCont.SplitterWidth = 20;
            this.splitCont.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.butSaveInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 55);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "注：此界面只有一录";
            // 
            // butSaveInfo
            // 
            this.butSaveInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butSaveInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butSaveInfo.Location = new System.Drawing.Point(290, 15);
            this.butSaveInfo.Name = "butSaveInfo";
            this.butSaveInfo.Size = new System.Drawing.Size(75, 37);
            this.butSaveInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butSaveInfo.TabIndex = 1;
            this.butSaveInfo.Text = "保存";
            this.butSaveInfo.Click += new System.EventHandler(this.butSaveInfo_Click);
            // 
            // grinfo
            // 
            this.grinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grinfo.BackColor = System.Drawing.Color.Transparent;
            this.grinfo.Location = new System.Drawing.Point(0, 2);
            this.grinfo.Name = "grinfo";
            this.grinfo.Size = new System.Drawing.Size(383, 157);
            this.grinfo.TabIndex = 0;
            this.grinfo.TabStop = false;
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Location = new System.Drawing.Point(0, 0);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(943, 572);
            this.gr2.TabIndex = 2;
            this.gr2.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gr1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.gr2);
            this.splitContainer1.Size = new System.Drawing.Size(1342, 609);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tools_user,
            this.tools_time});
            this.statusStrip1.Location = new System.Drawing.Point(0, 584);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(946, 25);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tools_user
            // 
            this.tools_user.BackColor = System.Drawing.Color.Transparent;
            this.tools_user.Name = "tools_user";
            this.tools_user.Size = new System.Drawing.Size(43, 20);
            this.tools_user.Text = "录入:";
            // 
            // tools_time
            // 
            this.tools_time.BackColor = System.Drawing.Color.Transparent;
            this.tools_time.Name = "tools_time";
            this.tools_time.Size = new System.Drawing.Size(43, 20);
            this.tools_time.Text = "时间:";
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
            this.gArchSelect1.Location = new System.Drawing.Point(0, 0);
            this.gArchSelect1.Margin = new System.Windows.Forms.Padding(4);
            this.gArchSelect1.Name = "gArchSelect1";
            this.gArchSelect1.PagesEnd = false;
            this.gArchSelect1.Size = new System.Drawing.Size(386, 345);
            this.gArchSelect1.TabIndex = 0;
            this.gArchSelect1.LineClickLoadInfo += new CsmCon.gArchSelect.ArchSelectHandle(this.gArchSelect1_LineClickLoadInfo);
            this.gArchSelect1.LineFocus += new CsmCon.gArchSelect.ArchSelectHandleFocus(this.gArchSelect1_LineFocus);
            this.gArchSelect1.LineGetInfo += new CsmCon.gArchSelect.ArchSelectHandleGetInfo(this.gArchSelect1_LineGetInfo);
            // 
            // Frmmldj
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1342, 634);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frmmldj";
            this.Text = "目录录入";
            this.Load += new System.EventHandler(this.Frmmldj_Load);
            this.gr1.ResumeLayout(false);
            this.splitCont.Panel1.ResumeLayout(false);
            this.splitCont.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCont)).EndInit();
            this.splitCont.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.GroupBox gr2;
        private CsmCon.gArchSelect gArchSelect1;
        private DevComponents.DotNetBar.Controls.CollapsibleSplitContainer splitCont;
        private System.Windows.Forms.GroupBox grinfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX butSaveInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tools_user;
        private System.Windows.Forms.ToolStripStatusLabel tools_time;
    }
}