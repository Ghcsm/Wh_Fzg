namespace Csmborr
{
    partial class FrmBorr
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
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBorr));
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.butborrDc = new DevComponents.DotNetBar.ButtonX();
            this.dateborrtime2 = new System.Windows.Forms.DateTimePicker();
            this.dateborrtime1 = new System.Windows.Forms.DateTimePicker();
            this.chkborrtime = new System.Windows.Forms.CheckBox();
            this.chkborrgjz = new System.Windows.Forms.CheckBox();
            this.butborrQuer = new DevComponents.DotNetBar.ButtonX();
            this.txtborrgjz = new System.Windows.Forms.TextBox();
            this.comborrbczf = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.combdy = new DevComponents.Editors.ComboItem();
            this.combbh = new DevComponents.Editors.ComboItem();
            this.combbdy = new DevComponents.Editors.ComboItem();
            this.combbbh = new DevComponents.Editors.ComboItem();
            this.comborrbcol = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.contMenu = new DevComponents.DotNetBar.ContextMenuBar();
            this.butImgload = new DevComponents.DotNetBar.ButtonItem();
            this.butitemArchjy = new DevComponents.DotNetBar.ButtonItem();
            this.butitemArchgh = new DevComponents.DotNetBar.ButtonItem();
            this.lvborrQuer = new System.Windows.Forms.ListView();
            this.colimg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imglist = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolslb_box = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslb_archno = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslb_f = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolslb_File = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolproess = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.gr1.SuspendLayout();
            this.gr2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contMenu)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr1.Controls.Add(this.butborrDc);
            this.gr1.Controls.Add(this.dateborrtime2);
            this.gr1.Controls.Add(this.dateborrtime1);
            this.gr1.Controls.Add(this.chkborrtime);
            this.gr1.Controls.Add(this.chkborrgjz);
            this.gr1.Controls.Add(this.butborrQuer);
            this.gr1.Controls.Add(this.txtborrgjz);
            this.gr1.Controls.Add(this.comborrbczf);
            this.gr1.Controls.Add(this.comborrbcol);
            this.gr1.Controls.Add(this.label3);
            this.gr1.Controls.Add(this.label2);
            this.gr1.Controls.Add(this.label1);
            this.gr1.Location = new System.Drawing.Point(7, 24);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(915, 99);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            // 
            // butborrDc
            // 
            this.butborrDc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butborrDc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butborrDc.Location = new System.Drawing.Point(817, 34);
            this.butborrDc.Name = "butborrDc";
            this.butborrDc.Size = new System.Drawing.Size(75, 46);
            this.butborrDc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butborrDc.TabIndex = 9;
            this.butborrDc.Text = "导出";
            this.butborrDc.Click += new System.EventHandler(this.butborrDc_Click);
            // 
            // dateborrtime2
            // 
            this.dateborrtime2.Location = new System.Drawing.Point(536, 62);
            this.dateborrtime2.Name = "dateborrtime2";
            this.dateborrtime2.Size = new System.Drawing.Size(125, 21);
            this.dateborrtime2.TabIndex = 8;
            // 
            // dateborrtime1
            // 
            this.dateborrtime1.Location = new System.Drawing.Point(379, 62);
            this.dateborrtime1.Name = "dateborrtime1";
            this.dateborrtime1.Size = new System.Drawing.Size(122, 21);
            this.dateborrtime1.TabIndex = 7;
            // 
            // chkborrtime
            // 
            this.chkborrtime.AutoSize = true;
            this.chkborrtime.Location = new System.Drawing.Point(289, 67);
            this.chkborrtime.Name = "chkborrtime";
            this.chkborrtime.Size = new System.Drawing.Size(84, 16);
            this.chkborrtime.TabIndex = 6;
            this.chkborrtime.Text = "时间范围：";
            this.chkborrtime.UseVisualStyleBackColor = true;
            // 
            // chkborrgjz
            // 
            this.chkborrgjz.AutoSize = true;
            this.chkborrgjz.Checked = true;
            this.chkborrgjz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkborrgjz.Location = new System.Drawing.Point(289, 29);
            this.chkborrgjz.Name = "chkborrgjz";
            this.chkborrgjz.Size = new System.Drawing.Size(72, 16);
            this.chkborrgjz.TabIndex = 5;
            this.chkborrgjz.Text = "关键字：";
            this.chkborrgjz.UseVisualStyleBackColor = true;
            // 
            // butborrQuer
            // 
            this.butborrQuer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butborrQuer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butborrQuer.Location = new System.Drawing.Point(704, 34);
            this.butborrQuer.Name = "butborrQuer";
            this.butborrQuer.Size = new System.Drawing.Size(75, 45);
            this.butborrQuer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butborrQuer.TabIndex = 4;
            this.butborrQuer.Text = "查询";
            this.butborrQuer.Click += new System.EventHandler(this.butQuer_Click);
            // 
            // txtborrgjz
            // 
            this.txtborrgjz.Location = new System.Drawing.Point(379, 25);
            this.txtborrgjz.Name = "txtborrgjz";
            this.txtborrgjz.Size = new System.Drawing.Size(282, 21);
            this.txtborrgjz.TabIndex = 3;
            // 
            // comborrbczf
            // 
            this.comborrbczf.DisplayMember = "Text";
            this.comborrbczf.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comborrbczf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comborrbczf.FormattingEnabled = true;
            this.comborrbczf.ItemHeight = 15;
            this.comborrbczf.Items.AddRange(new object[] {
            this.comboItem1,
            this.combdy,
            this.combbh,
            this.combbdy,
            this.combbbh});
            this.comborrbczf.Location = new System.Drawing.Point(86, 62);
            this.comborrbczf.Name = "comborrbczf";
            this.comborrbczf.Size = new System.Drawing.Size(169, 21);
            this.comborrbczf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comborrbczf.TabIndex = 2;
            // 
            // combdy
            // 
            this.combdy.Text = "等于";
            // 
            // combbh
            // 
            this.combbh.Text = "包含";
            // 
            // combbdy
            // 
            this.combbdy.Text = "不等于";
            // 
            // combbbh
            // 
            this.combbbh.Text = "不包含";
            // 
            // comborrbcol
            // 
            this.comborrbcol.DisplayMember = "Text";
            this.comborrbcol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comborrbcol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comborrbcol.FormattingEnabled = true;
            this.comborrbcol.ItemHeight = 15;
            this.comborrbcol.Location = new System.Drawing.Point(86, 26);
            this.comborrbcol.Name = "comborrbcol";
            this.comborrbcol.Size = new System.Drawing.Size(169, 21);
            this.comborrbcol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comborrbcol.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "操作符：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询字段：";
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Controls.Add(this.contMenu);
            this.gr2.Controls.Add(this.lvborrQuer);
            this.gr2.Location = new System.Drawing.Point(7, 129);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(915, 339);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            // 
            // contMenu
            // 
            this.contMenu.AntiAlias = true;
            this.contMenu.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.contMenu.IsMaximized = false;
            this.contMenu.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.butImgload,
            this.butitemArchjy,
            this.butitemArchgh});
            this.contMenu.Location = new System.Drawing.Point(806, 236);
            this.contMenu.Name = "contMenu";
            this.contMenu.Size = new System.Drawing.Size(75, 78);
            this.contMenu.Stretch = true;
            this.contMenu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.contMenu.TabIndex = 3;
            this.contMenu.TabStop = false;
            this.contMenu.Text = "contextMenuBar1";
            // 
            // butImgload
            // 
            this.butImgload.AutoExpandOnClick = true;
            this.butImgload.Name = "butImgload";
            this.butImgload.Text = "查看图像";
            this.butImgload.Click += new System.EventHandler(this.butImgload_Click);
            // 
            // butitemArchjy
            // 
            this.butitemArchjy.AutoExpandOnClick = true;
            this.butitemArchjy.BeginGroup = true;
            this.butitemArchjy.Name = "butitemArchjy";
            this.butitemArchjy.Text = "档案借阅";
            this.butitemArchjy.Click += new System.EventHandler(this.butitemArchjy_Click);
            // 
            // butitemArchgh
            // 
            this.butitemArchgh.AutoExpandOnClick = true;
            this.butitemArchgh.Name = "butitemArchgh";
            this.butitemArchgh.Text = "档案归还";
            this.butitemArchgh.Click += new System.EventHandler(this.butitemArchgh_Click);
            // 
            // lvborrQuer
            // 
            this.lvborrQuer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colimg});
            this.lvborrQuer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvborrQuer.FullRowSelect = true;
            this.lvborrQuer.GridLines = true;
            this.lvborrQuer.LargeImageList = this.imglist;
            this.lvborrQuer.Location = new System.Drawing.Point(3, 17);
            this.lvborrQuer.Name = "lvborrQuer";
            this.lvborrQuer.Size = new System.Drawing.Size(909, 319);
            this.lvborrQuer.SmallImageList = this.imglist;
            this.lvborrQuer.StateImageList = this.imglist;
            this.lvborrQuer.TabIndex = 0;
            this.lvborrQuer.UseCompatibleStateImageBehavior = false;
            this.lvborrQuer.View = System.Windows.Forms.View.Details;
            this.lvborrQuer.Click += new System.EventHandler(this.lvborrQuer_Click);
            this.lvborrQuer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvborrQuer_MouseClick);
            this.lvborrQuer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvborrQuer_MouseUp);
            // 
            // colimg
            // 
            this.colimg.Text = "序号";
            this.colimg.Width = 80;
            // 
            // imglist
            // 
            this.imglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglist.ImageStream")));
            this.imglist.TransparentColor = System.Drawing.Color.Transparent;
            this.imglist.Images.SetKeyName(0, "1.png");
            this.imglist.Images.SetKeyName(1, "2.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolslb_box,
            this.toolslb_archno,
            this.toolslb_f,
            this.toolslb_File,
            this.toolStripStatusLabel1,
            this.toolproess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 474);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(928, 22);
            this.statusStrip1.TabIndex = 2;
            // 
            // toolslb_box
            // 
            this.toolslb_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.toolslb_box.Name = "toolslb_box";
            this.toolslb_box.Size = new System.Drawing.Size(44, 17);
            this.toolslb_box.Text = "盒号：";
            // 
            // toolslb_archno
            // 
            this.toolslb_archno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.toolslb_archno.Name = "toolslb_archno";
            this.toolslb_archno.Size = new System.Drawing.Size(44, 17);
            this.toolslb_archno.Text = "卷号：";
            // 
            // toolslb_f
            // 
            this.toolslb_f.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.toolslb_f.Name = "toolslb_f";
            this.toolslb_f.Size = new System.Drawing.Size(44, 17);
            this.toolslb_f.Text = "文件：";
            // 
            // toolslb_File
            // 
            this.toolslb_File.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.toolslb_File.Name = "toolslb_File";
            this.toolslb_File.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(781, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolproess
            // 
            this.toolproess.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolproess.Name = "toolproess";
            this.toolproess.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolproess.Size = new System.Drawing.Size(100, 16);
            this.toolproess.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(928, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveFile
            // 
            this.saveFile.Filter = "xls文件|*.xls";
            // 
            // FrmBorr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(928, 496);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gr2);
            this.Controls.Add(this.gr1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBorr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询借阅";
            this.Shown += new System.EventHandler(this.FrmBorr_Shown);
            this.gr1.ResumeLayout(false);
            this.gr1.PerformLayout();
            this.gr2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contMenu)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comborrbcol;
        private DevComponents.DotNetBar.ButtonX butborrQuer;
        private System.Windows.Forms.TextBox txtborrgjz;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comborrbczf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gr2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView lvborrQuer;
        private DevComponents.Editors.ComboItem combdy;
        private DevComponents.Editors.ComboItem combbh;
        private DevComponents.Editors.ComboItem combbdy;
        private DevComponents.Editors.ComboItem combbbh;
        private System.Windows.Forms.ToolStripStatusLabel toolslb_box;
        private System.Windows.Forms.ToolStripStatusLabel toolslb_archno;
        private System.Windows.Forms.ToolStripStatusLabel toolslb_f;
        private System.Windows.Forms.ToolStripStatusLabel toolslb_File;
        private DevComponents.DotNetBar.ContextMenuBar contMenu;
        private DevComponents.DotNetBar.ButtonItem butImgload;
        private DevComponents.DotNetBar.ButtonItem butitemArchjy;
        private DevComponents.DotNetBar.ButtonItem butitemArchgh;
        private System.Windows.Forms.ImageList imglist;
        private System.Windows.Forms.ColumnHeader colimg;
        private System.Windows.Forms.ToolStripProgressBar toolproess;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckBox chkborrgjz;
        private System.Windows.Forms.CheckBox chkborrtime;
        private System.Windows.Forms.DateTimePicker dateborrtime2;
        private System.Windows.Forms.DateTimePicker dateborrtime1;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX butborrDc;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private DevComponents.Editors.ComboItem comboItem1;
    }
}

