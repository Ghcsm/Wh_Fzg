namespace Csmdady
{
    partial class FrmPrint
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr0 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.butPrintTm = new DevComponents.DotNetBar.ButtonX();
            this.butLog = new DevComponents.DotNetBar.ButtonX();
            this.labStat = new System.Windows.Forms.Label();
            this.butPrintConten = new DevComponents.DotNetBar.ButtonX();
            this.butPrintInfo = new DevComponents.DotNetBar.ButtonX();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.tabControlPrint = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.panePrintInfoShow = new DevComponents.DotNetBar.PanelEx();
            this.tabItemInfoColShow = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.panelPrintXY = new DevComponents.DotNetBar.PanelEx();
            this.butPrintXyinfo = new DevComponents.DotNetBar.ButtonX();
            this.tabItemInfoColXy = new DevComponents.DotNetBar.TabItem(this.components);
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.tabContrSelect = new DevComponents.DotNetBar.TabControl();
            this.tabControlSelectPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.rbboxOne = new System.Windows.Forms.RadioButton();
            this.rbBoxAll = new System.Windows.Forms.RadioButton();
            this.gArchSelect1 = new CsmCon.gArchSelect();
            this.tabSelectbox = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.butDelbox = new DevComponents.DotNetBar.ButtonX();
            this.butBoxRangeAdd = new DevComponents.DotNetBar.ButtonX();
            this.txtBox2 = new System.Windows.Forms.TextBox();
            this.txtBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvboxRange = new System.Windows.Forms.ListView();
            this.c_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_box1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_box2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabItemboxRange = new DevComponents.DotNetBar.TabItem(this.components);
            this.gr0.SuspendLayout();
            this.gr2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlPrint)).BeginInit();
            this.tabControlPrint.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabContrSelect)).BeginInit();
            this.tabContrSelect.SuspendLayout();
            this.tabControlSelectPanel1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(929, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr0
            // 
            this.gr0.CanvasColor = System.Drawing.SystemColors.Control;
            this.gr0.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gr0.Controls.Add(this.butPrintTm);
            this.gr0.Controls.Add(this.butLog);
            this.gr0.Controls.Add(this.labStat);
            this.gr0.Controls.Add(this.butPrintConten);
            this.gr0.Controls.Add(this.butPrintInfo);
            this.gr0.Controls.Add(this.gr2);
            this.gr0.Controls.Add(this.gr1);
            this.gr0.DisabledBackColor = System.Drawing.Color.Empty;
            this.gr0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr0.Location = new System.Drawing.Point(0, 25);
            this.gr0.Name = "gr0";
            this.gr0.Size = new System.Drawing.Size(929, 542);
            // 
            // 
            // 
            this.gr0.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gr0.Style.BackColorGradientAngle = 90;
            this.gr0.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gr0.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderBottomWidth = 1;
            this.gr0.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gr0.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderLeftWidth = 1;
            this.gr0.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderRightWidth = 1;
            this.gr0.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gr0.Style.BorderTopWidth = 1;
            this.gr0.Style.CornerDiameter = 4;
            this.gr0.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gr0.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gr0.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gr0.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gr0.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gr0.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gr0.TabIndex = 1;
            // 
            // butPrintTm
            // 
            this.butPrintTm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butPrintTm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butPrintTm.Enabled = false;
            this.butPrintTm.Location = new System.Drawing.Point(709, 477);
            this.butPrintTm.Name = "butPrintTm";
            this.butPrintTm.Size = new System.Drawing.Size(75, 45);
            this.butPrintTm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butPrintTm.TabIndex = 6;
            this.butPrintTm.Text = "打印条码";
            this.butPrintTm.Click += new System.EventHandler(this.butPrintTm_Click);
            // 
            // butLog
            // 
            this.butLog.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butLog.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butLog.Location = new System.Drawing.Point(843, 479);
            this.butLog.Name = "butLog";
            this.butLog.Size = new System.Drawing.Size(66, 45);
            this.butLog.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butLog.TabIndex = 5;
            this.butLog.Text = "日志";
            this.butLog.Click += new System.EventHandler(this.butLog_Click);
            // 
            // labStat
            // 
            this.labStat.BackColor = System.Drawing.Color.Transparent;
            this.labStat.ForeColor = System.Drawing.Color.Red;
            this.labStat.Location = new System.Drawing.Point(397, 492);
            this.labStat.Name = "labStat";
            this.labStat.Size = new System.Drawing.Size(100, 23);
            this.labStat.TabIndex = 4;
            this.labStat.Text = "未质检无法打印";
            this.labStat.Visible = false;
            // 
            // butPrintConten
            // 
            this.butPrintConten.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butPrintConten.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butPrintConten.Location = new System.Drawing.Point(602, 477);
            this.butPrintConten.Name = "butPrintConten";
            this.butPrintConten.Size = new System.Drawing.Size(75, 45);
            this.butPrintConten.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butPrintConten.TabIndex = 3;
            this.butPrintConten.Text = "打印目录";
            this.butPrintConten.Click += new System.EventHandler(this.butPrintConten_Click);
            // 
            // butPrintInfo
            // 
            this.butPrintInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butPrintInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butPrintInfo.Location = new System.Drawing.Point(503, 477);
            this.butPrintInfo.Name = "butPrintInfo";
            this.butPrintInfo.Size = new System.Drawing.Size(75, 45);
            this.butPrintInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butPrintInfo.TabIndex = 2;
            this.butPrintInfo.Text = "打印信息";
            this.butPrintInfo.Click += new System.EventHandler(this.butPrintInfo_Click);
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.BackColor = System.Drawing.Color.Transparent;
            this.gr2.Controls.Add(this.tabControlPrint);
            this.gr2.Location = new System.Drawing.Point(392, 10);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(524, 447);
            this.gr2.TabIndex = 1;
            this.gr2.TabStop = false;
            // 
            // tabControlPrint
            // 
            this.tabControlPrint.BackColor = System.Drawing.Color.Transparent;
            this.tabControlPrint.CanReorderTabs = true;
            this.tabControlPrint.Controls.Add(this.tabControlPanel2);
            this.tabControlPrint.Controls.Add(this.tabControlPanel3);
            this.tabControlPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrint.Location = new System.Drawing.Point(3, 17);
            this.tabControlPrint.Name = "tabControlPrint";
            this.tabControlPrint.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControlPrint.SelectedTabIndex = 0;
            this.tabControlPrint.Size = new System.Drawing.Size(518, 427);
            this.tabControlPrint.TabIndex = 0;
            this.tabControlPrint.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControlPrint.Tabs.Add(this.tabItemInfoColShow);
            this.tabControlPrint.Tabs.Add(this.tabItemInfoColXy);
            this.tabControlPrint.Text = "tabControl1";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.panePrintInfoShow);
            this.tabControlPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(518, 401);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 1;
            this.tabControlPanel2.TabItem = this.tabItemInfoColShow;
            // 
            // panePrintInfoShow
            // 
            this.panePrintInfoShow.AutoScroll = true;
            this.panePrintInfoShow.CanvasColor = System.Drawing.SystemColors.Control;
            this.panePrintInfoShow.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panePrintInfoShow.DisabledBackColor = System.Drawing.Color.Empty;
            this.panePrintInfoShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panePrintInfoShow.Location = new System.Drawing.Point(1, 1);
            this.panePrintInfoShow.Name = "panePrintInfoShow";
            this.panePrintInfoShow.Size = new System.Drawing.Size(516, 399);
            this.panePrintInfoShow.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panePrintInfoShow.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panePrintInfoShow.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panePrintInfoShow.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panePrintInfoShow.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panePrintInfoShow.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panePrintInfoShow.Style.GradientAngle = 90;
            this.panePrintInfoShow.TabIndex = 0;
            // 
            // tabItemInfoColShow
            // 
            this.tabItemInfoColShow.AttachedControl = this.tabControlPanel2;
            this.tabItemInfoColShow.Name = "tabItemInfoColShow";
            this.tabItemInfoColShow.Text = "信息显示";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.panelPrintXY);
            this.tabControlPanel3.Controls.Add(this.butPrintXyinfo);
            this.tabControlPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(518, 401);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 5;
            this.tabControlPanel3.TabItem = this.tabItemInfoColXy;
            // 
            // panelPrintXY
            // 
            this.panelPrintXY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPrintXY.AutoScroll = true;
            this.panelPrintXY.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelPrintXY.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelPrintXY.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelPrintXY.Location = new System.Drawing.Point(0, 0);
            this.panelPrintXY.Name = "panelPrintXY";
            this.panelPrintXY.Size = new System.Drawing.Size(514, 352);
            this.panelPrintXY.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelPrintXY.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelPrintXY.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelPrintXY.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelPrintXY.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelPrintXY.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelPrintXY.Style.GradientAngle = 90;
            this.panelPrintXY.TabIndex = 13;
            // 
            // butPrintXyinfo
            // 
            this.butPrintXyinfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butPrintXyinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butPrintXyinfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butPrintXyinfo.Location = new System.Drawing.Point(4, 358);
            this.butPrintXyinfo.Name = "butPrintXyinfo";
            this.butPrintXyinfo.Size = new System.Drawing.Size(85, 39);
            this.butPrintXyinfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butPrintXyinfo.TabIndex = 9;
            this.butPrintXyinfo.Text = "保存";
            this.butPrintXyinfo.Click += new System.EventHandler(this.butPrintXyinfo_Click);
            // 
            // tabItemInfoColXy
            // 
            this.tabItemInfoColXy.AttachedControl = this.tabControlPanel3;
            this.tabItemInfoColXy.Name = "tabItemInfoColXy";
            this.tabItemInfoColXy.Text = "打印信息坐标";
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr1.AutoSize = true;
            this.gr1.BackColor = System.Drawing.Color.Transparent;
            this.gr1.Controls.Add(this.tabContrSelect);
            this.gr1.Location = new System.Drawing.Point(3, 3);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(384, 524);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            // 
            // tabContrSelect
            // 
            this.tabContrSelect.BackColor = System.Drawing.Color.Transparent;
            this.tabContrSelect.CanReorderTabs = true;
            this.tabContrSelect.Controls.Add(this.tabControlSelectPanel1);
            this.tabContrSelect.Controls.Add(this.tabControlPanel1);
            this.tabContrSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContrSelect.Location = new System.Drawing.Point(3, 17);
            this.tabContrSelect.Name = "tabContrSelect";
            this.tabContrSelect.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabContrSelect.SelectedTabIndex = 0;
            this.tabContrSelect.Size = new System.Drawing.Size(378, 504);
            this.tabContrSelect.TabIndex = 0;
            this.tabContrSelect.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabContrSelect.Tabs.Add(this.tabSelectbox);
            this.tabContrSelect.Tabs.Add(this.tabItemboxRange);
            this.tabContrSelect.Text = "tabControl1";
            // 
            // tabControlSelectPanel1
            // 
            this.tabControlSelectPanel1.Controls.Add(this.rbboxOne);
            this.tabControlSelectPanel1.Controls.Add(this.rbBoxAll);
            this.tabControlSelectPanel1.Controls.Add(this.gArchSelect1);
            this.tabControlSelectPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlSelectPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSelectPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlSelectPanel1.Name = "tabControlSelectPanel1";
            this.tabControlSelectPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlSelectPanel1.Size = new System.Drawing.Size(378, 478);
            this.tabControlSelectPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlSelectPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlSelectPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlSelectPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlSelectPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlSelectPanel1.Style.GradientAngle = 90;
            this.tabControlSelectPanel1.TabIndex = 1;
            this.tabControlSelectPanel1.TabItem = this.tabSelectbox;
            // 
            // rbboxOne
            // 
            this.rbboxOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbboxOne.AutoSize = true;
            this.rbboxOne.BackColor = System.Drawing.Color.Transparent;
            this.rbboxOne.Checked = true;
            this.rbboxOne.Location = new System.Drawing.Point(146, 450);
            this.rbboxOne.Name = "rbboxOne";
            this.rbboxOne.Size = new System.Drawing.Size(71, 16);
            this.rbboxOne.TabIndex = 2;
            this.rbboxOne.TabStop = true;
            this.rbboxOne.Text = "单卷档案";
            this.rbboxOne.UseVisualStyleBackColor = false;
            // 
            // rbBoxAll
            // 
            this.rbBoxAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbBoxAll.AutoSize = true;
            this.rbBoxAll.BackColor = System.Drawing.Color.Transparent;
            this.rbBoxAll.Location = new System.Drawing.Point(40, 450);
            this.rbBoxAll.Name = "rbBoxAll";
            this.rbBoxAll.Size = new System.Drawing.Size(71, 16);
            this.rbBoxAll.TabIndex = 1;
            this.rbBoxAll.Text = "整盒档案";
            this.rbBoxAll.UseVisualStyleBackColor = false;
            // 
            // gArchSelect1
            // 
            this.gArchSelect1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gArchSelect1.Archid = 0;
            this.gArchSelect1.ArchImgFile = null;
            this.gArchSelect1.ArchRegPages = 0;
            this.gArchSelect1.Archstat = null;
            this.gArchSelect1.Archtype = null;
            this.gArchSelect1.Archxystat = null;
            this.gArchSelect1.BackColor = System.Drawing.Color.Transparent;
            this.gArchSelect1.Boxsn = 0;
            this.gArchSelect1.GotoPages = false;
            this.gArchSelect1.LoadFileBoole = false;
            this.gArchSelect1.Location = new System.Drawing.Point(8, 0);
            this.gArchSelect1.Name = "gArchSelect1";
            this.gArchSelect1.PagesEnd = true;
            this.gArchSelect1.Size = new System.Drawing.Size(366, 435);
            this.gArchSelect1.TabIndex = 0;
            this.gArchSelect1.LineClickLoadInfo += new CsmCon.gArchSelect.ArchSelectHandle(this.gArchSelect1_LineClickLoadInfo);
            // 
            // tabSelectbox
            // 
            this.tabSelectbox.AttachedControl = this.tabControlSelectPanel1;
            this.tabSelectbox.Name = "tabSelectbox";
            this.tabSelectbox.Text = "案卷打印";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.lbInfo);
            this.tabControlPanel1.Controls.Add(this.butDelbox);
            this.tabControlPanel1.Controls.Add(this.butBoxRangeAdd);
            this.tabControlPanel1.Controls.Add(this.txtBox2);
            this.tabControlPanel1.Controls.Add(this.txtBox1);
            this.tabControlPanel1.Controls.Add(this.label2);
            this.tabControlPanel1.Controls.Add(this.label1);
            this.tabControlPanel1.Controls.Add(this.lvboxRange);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(378, 478);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 5;
            this.tabControlPanel1.TabItem = this.tabItemboxRange;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbInfo.Location = new System.Drawing.Point(196, 446);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(0, 12);
            this.lbInfo.TabIndex = 6;
            // 
            // butDelbox
            // 
            this.butDelbox.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDelbox.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDelbox.Location = new System.Drawing.Point(25, 431);
            this.butDelbox.Name = "butDelbox";
            this.butDelbox.Size = new System.Drawing.Size(75, 39);
            this.butDelbox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDelbox.TabIndex = 5;
            this.butDelbox.Text = "删除";
            this.butDelbox.Click += new System.EventHandler(this.butDelbox_Click);
            // 
            // butBoxRangeAdd
            // 
            this.butBoxRangeAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butBoxRangeAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butBoxRangeAdd.Location = new System.Drawing.Point(271, 2);
            this.butBoxRangeAdd.Name = "butBoxRangeAdd";
            this.butBoxRangeAdd.Size = new System.Drawing.Size(75, 29);
            this.butBoxRangeAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butBoxRangeAdd.TabIndex = 4;
            this.butBoxRangeAdd.Text = "添加";
            this.butBoxRangeAdd.Click += new System.EventHandler(this.butBoxRangeAdd_Click);
            // 
            // txtBox2
            // 
            this.txtBox2.Location = new System.Drawing.Point(186, 6);
            this.txtBox2.Name = "txtBox2";
            this.txtBox2.Size = new System.Drawing.Size(62, 21);
            this.txtBox2.TabIndex = 3;
            // 
            // txtBox1
            // 
            this.txtBox1.Location = new System.Drawing.Point(92, 6);
            this.txtBox1.Name = "txtBox1";
            this.txtBox1.Size = new System.Drawing.Size(62, 21);
            this.txtBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(157, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "---";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "盒号范围：";
            // 
            // lvboxRange
            // 
            this.lvboxRange.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c_id,
            this.c_box1,
            this.c_box2});
            this.lvboxRange.FullRowSelect = true;
            this.lvboxRange.GridLines = true;
            this.lvboxRange.Location = new System.Drawing.Point(5, 35);
            this.lvboxRange.Name = "lvboxRange";
            this.lvboxRange.Size = new System.Drawing.Size(371, 390);
            this.lvboxRange.TabIndex = 0;
            this.lvboxRange.UseCompatibleStateImageBehavior = false;
            this.lvboxRange.View = System.Windows.Forms.View.Details;
            // 
            // c_id
            // 
            this.c_id.Text = "序号";
            // 
            // c_box1
            // 
            this.c_box1.Text = "起始盒号";
            this.c_box1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_box1.Width = 150;
            // 
            // c_box2
            // 
            this.c_box2.Text = "终止盒号";
            this.c_box2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_box2.Width = 150;
            // 
            // tabItemboxRange
            // 
            this.tabItemboxRange.AttachedControl = this.tabControlPanel1;
            this.tabItemboxRange.Name = "tabItemboxRange";
            this.tabItemboxRange.Text = "批量打印";
            // 
            // FrmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 567);
            this.Controls.Add(this.gr0);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPrint";
            this.Text = "FrmPrint";
            this.Load += new System.EventHandler(this.FrmPrint_Load);
            this.Shown += new System.EventHandler(this.FrmPrint_Shown);
            this.gr0.ResumeLayout(false);
            this.gr0.PerformLayout();
            this.gr2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlPrint)).EndInit();
            this.tabControlPrint.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            this.gr1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabContrSelect)).EndInit();
            this.tabContrSelect.ResumeLayout(false);
            this.tabControlSelectPanel1.ResumeLayout(false);
            this.tabControlSelectPanel1.PerformLayout();
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevComponents.DotNetBar.Controls.GroupPanel gr0;
        private System.Windows.Forms.GroupBox gr1;
        private DevComponents.DotNetBar.TabControl tabContrSelect;
        private DevComponents.DotNetBar.TabControlPanel tabControlSelectPanel1;
        private DevComponents.DotNetBar.TabItem tabSelectbox;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItemboxRange;
        private CsmCon.gArchSelect gArchSelect1;
        private System.Windows.Forms.RadioButton rbBoxAll;
        private System.Windows.Forms.RadioButton rbboxOne;
        private System.Windows.Forms.GroupBox gr2;
        private DevComponents.DotNetBar.TabControl tabControlPrint;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItemInfoColShow;
        private System.Windows.Forms.ListView lvboxRange;
        private System.Windows.Forms.ColumnHeader c_id;
        private System.Windows.Forms.ColumnHeader c_box1;
        private System.Windows.Forms.ColumnHeader c_box2;
        private DevComponents.DotNetBar.ButtonX butBoxRangeAdd;
        private System.Windows.Forms.TextBox txtBox2;
        private System.Windows.Forms.TextBox txtBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX butPrintInfo;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabItemInfoColXy;
        private DevComponents.DotNetBar.PanelEx panePrintInfoShow;
        private DevComponents.DotNetBar.ButtonX butPrintXyinfo;
        private DevComponents.DotNetBar.ButtonX butPrintConten;
        private DevComponents.DotNetBar.PanelEx panelPrintXY;
        private System.Windows.Forms.Label labStat;
        private DevComponents.DotNetBar.ButtonX butDelbox;
        private DevComponents.DotNetBar.ButtonX butLog;
        private DevComponents.DotNetBar.ButtonX butPrintTm;
        private System.Windows.Forms.Label lbInfo;
    }
}