namespace Bgkj
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.StatusBar = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.comHouse = new DevComponents.DotNetBar.ComboBoxItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.labUser = new DevComponents.DotNetBar.LabelItem();
            this.labDate = new DevComponents.DotNetBar.LabelItem();
            this.labCo = new DevComponents.DotNetBar.LabelItem();
            this.dotBarManger = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite9 = new DevComponents.DotNetBar.DockSite();
            this.bar3 = new DevComponents.DotNetBar.Bar();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.pandleDock2 = new DevComponents.DotNetBar.PanelDockContainer();
            this.sideBarManger = new DevComponents.DotNetBar.SideBar();
            this.imgListManger = new System.Windows.Forms.ImageList(this.components);
            this.dock2 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.pandleDock1 = new DevComponents.DotNetBar.PanelDockContainer();
            this.pictTop = new System.Windows.Forms.PictureBox();
            this.dock1 = new DevComponents.DotNetBar.DockContainerItem();
            this.dock3 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockConta = new DevComponents.DotNetBar.DockContainerItem();
            this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.t = new System.Windows.Forms.Timer(this.components);
            this.dockSite9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).BeginInit();
            this.dockSite1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.bar2.SuspendLayout();
            this.pandleDock2.SuspendLayout();
            this.dockSite3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.bar1.SuspendLayout();
            this.pandleDock1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictTop)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.StatusBar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.StatusBar.ContainerControlProcessDialogKey = true;
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.DragDropSupport = true;
            this.StatusBar.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.comHouse,
            this.labUser,
            this.labDate,
            this.labCo});
            this.StatusBar.Location = new System.Drawing.Point(0, 425);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(800, 25);
            this.StatusBar.TabIndex = 0;
            // 
            // comHouse
            // 
            this.comHouse.ComboWidth = 125;
            this.comHouse.DropDownHeight = 106;
            this.comHouse.ItemHeight = 20;
            this.comHouse.Items.AddRange(new object[] {
            this.comboItem1});
            this.comHouse.Name = "comHouse";
            this.comHouse.WatermarkColor = System.Drawing.Color.Transparent;
            this.comHouse.SelectedIndexChanged += new System.EventHandler(this.comHouse_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.comboItem1.FontSize = 10F;
            this.comboItem1.Text = "产权库房";
            this.comboItem1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.comboItem1.TextLineAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labUser
            // 
            this.labUser.BeginGroup = true;
            this.labUser.ForeColor = System.Drawing.Color.Black;
            this.labUser.Name = "labUser";
            // 
            // labDate
            // 
            this.labDate.BeginGroup = true;
            this.labDate.ForeColor = System.Drawing.Color.Black;
            this.labDate.Name = "labDate";
            // 
            // labCo
            // 
            this.labCo.BeginGroup = true;
            this.labCo.ForeColor = System.Drawing.Color.Black;
            this.labCo.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.labCo.Name = "labCo";
            // 
            // dotBarManger
            // 
            this.dotBarManger.BottomDockSite = this.dockSite4;
            this.dotBarManger.EnableFullSizeDock = false;
            this.dotBarManger.FillDockSite = this.dockSite9;
            this.dotBarManger.LeftDockSite = this.dockSite1;
            this.dotBarManger.ParentForm = this;
            this.dotBarManger.RightDockSite = this.dockSite2;
            this.dotBarManger.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.dotBarManger.ToolbarBottomDockSite = this.dockSite8;
            this.dotBarManger.ToolbarLeftDockSite = this.dockSite5;
            this.dotBarManger.ToolbarRightDockSite = this.dockSite6;
            this.dotBarManger.ToolbarTopDockSite = this.dockSite7;
            this.dotBarManger.TopDockSite = this.dockSite3;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(0, 450);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(800, 0);
            this.dockSite4.TabIndex = 4;
            this.dockSite4.TabStop = false;
            // 
            // dockSite9
            // 
            this.dockSite9.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite9.Controls.Add(this.bar3);
            this.dockSite9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockSite9.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar3, 641, 316)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSite9.Location = new System.Drawing.Point(159, 109);
            this.dockSite9.Name = "dockSite9";
            this.dockSite9.Size = new System.Drawing.Size(641, 316);
            this.dockSite9.TabIndex = 9;
            this.dockSite9.TabStop = false;
            // 
            // bar3
            // 
            this.bar3.AccessibleDescription = "DotNetBar Bar (bar3)";
            this.bar3.AccessibleName = "DotNetBar Bar";
            this.bar3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.bar3.AlwaysDisplayDockTab = true;
            this.bar3.BackgroundImage = global::Bgkj.Properties.Resources._3;
            this.bar3.BackgroundImageAlpha = ((byte)(30));
            this.bar3.CanAutoHide = false;
            this.bar3.CanCustomize = false;
            this.bar3.CanDockBottom = false;
            this.bar3.CanDockLeft = false;
            this.bar3.CanDockRight = false;
            this.bar3.CanDockTab = false;
            this.bar3.CanDockTop = false;
            this.bar3.CanHide = true;
            this.bar3.CanUndock = false;
            this.bar3.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.bar3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar3.IsMaximized = false;
            this.bar3.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar3.Location = new System.Drawing.Point(0, 0);
            this.bar3.Name = "bar3";
            this.bar3.Size = new System.Drawing.Size(641, 316);
            this.bar3.Stretch = true;
            this.bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar3.TabIndex = 0;
            this.bar3.TabNavigation = true;
            this.bar3.TabStop = false;
            this.bar3.Closing += new DevComponents.DotNetBar.DotNetBarManager.BarClosingEventHandler(this.bar3_Closing);
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Controls.Add(this.bar2);
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar2, 156, 316)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSite1.Location = new System.Drawing.Point(0, 109);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(159, 316);
            this.dockSite1.TabIndex = 1;
            this.dockSite1.TabStop = false;
            // 
            // bar2
            // 
            this.bar2.AccessibleDescription = "DotNetBar Bar (bar2)";
            this.bar2.AccessibleName = "DotNetBar Bar";
            this.bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.bar2.AutoSyncBarCaption = true;
            this.bar2.CloseSingleTab = true;
            this.bar2.Controls.Add(this.pandleDock2);
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar2.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.bar2.IsMaximized = false;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dock2});
            this.bar2.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(156, 316);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar2.TabIndex = 0;
            this.bar2.TabStop = false;
            this.bar2.Text = "相关模块";
            // 
            // pandleDock2
            // 
            this.pandleDock2.Controls.Add(this.sideBarManger);
            this.pandleDock2.DisabledBackColor = System.Drawing.Color.Empty;
            this.pandleDock2.Location = new System.Drawing.Point(3, 23);
            this.pandleDock2.Name = "pandleDock2";
            this.pandleDock2.Size = new System.Drawing.Size(150, 290);
            this.pandleDock2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pandleDock2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.pandleDock2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.pandleDock2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.pandleDock2.Style.GradientAngle = 90;
            this.pandleDock2.TabIndex = 0;
            // 
            // sideBarManger
            // 
            this.sideBarManger.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.sideBarManger.BorderStyle = DevComponents.DotNetBar.eBorderType.None;
            this.sideBarManger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideBarManger.ExpandedPanel = null;
            this.sideBarManger.Images = this.imgListManger;
            this.sideBarManger.Location = new System.Drawing.Point(0, 0);
            this.sideBarManger.Name = "sideBarManger";
            this.sideBarManger.Size = new System.Drawing.Size(150, 290);
            this.sideBarManger.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sideBarManger.TabIndex = 0;
            // 
            // imgListManger
            // 
            this.imgListManger.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListManger.ImageStream")));
            this.imgListManger.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListManger.Images.SetKeyName(0, "1.png");
            this.imgListManger.Images.SetKeyName(1, "2.png");
            this.imgListManger.Images.SetKeyName(2, "3.png");
            this.imgListManger.Images.SetKeyName(3, "4.png");
            this.imgListManger.Images.SetKeyName(4, "5.png");
            this.imgListManger.Images.SetKeyName(5, "6.png");
            this.imgListManger.Images.SetKeyName(6, "7.png");
            this.imgListManger.Images.SetKeyName(7, "8.png");
            this.imgListManger.Images.SetKeyName(8, "9.png");
            this.imgListManger.Images.SetKeyName(9, "10.png");
            this.imgListManger.Images.SetKeyName(10, "11.png");
            this.imgListManger.Images.SetKeyName(11, "12.png");
            this.imgListManger.Images.SetKeyName(12, "13.png");
            this.imgListManger.Images.SetKeyName(13, "14.png");
            this.imgListManger.Images.SetKeyName(14, "15.png");
            this.imgListManger.Images.SetKeyName(15, "16.png");
            this.imgListManger.Images.SetKeyName(16, "17.png");
            this.imgListManger.Images.SetKeyName(17, "18.png");
            this.imgListManger.Images.SetKeyName(18, "19.png");
            this.imgListManger.Images.SetKeyName(19, "20.png");
            this.imgListManger.Images.SetKeyName(20, "21.png");
            this.imgListManger.Images.SetKeyName(21, "22.png");
            // 
            // dock2
            // 
            this.dock2.Control = this.pandleDock2;
            this.dock2.Name = "dock2";
            this.dock2.Text = "相关模块";
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite2.Location = new System.Drawing.Point(800, 109);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(0, 316);
            this.dockSite2.TabIndex = 2;
            this.dockSite2.TabStop = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(0, 450);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(800, 0);
            this.dockSite8.TabIndex = 8;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(0, 0);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 450);
            this.dockSite5.TabIndex = 5;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(800, 0);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 450);
            this.dockSite6.TabIndex = 6;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(800, 0);
            this.dockSite7.TabIndex = 7;
            this.dockSite7.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Controls.Add(this.bar1);
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar1, 800, 106)))}, DevComponents.DotNetBar.eOrientation.Vertical);
            this.dockSite3.Location = new System.Drawing.Point(0, 0);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(800, 109);
            this.dockSite3.TabIndex = 3;
            this.dockSite3.TabStop = false;
            // 
            // bar1
            // 
            this.bar1.AccessibleDescription = "DotNetBar Bar (bar1)";
            this.bar1.AccessibleName = "DotNetBar Bar";
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.bar1.AutoSyncBarCaption = true;
            this.bar1.CloseSingleTab = true;
            this.bar1.Controls.Add(this.pandleDock1);
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dock1});
            this.bar1.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(800, 106);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "BG";
            // 
            // pandleDock1
            // 
            this.pandleDock1.Controls.Add(this.pictTop);
            this.pandleDock1.DisabledBackColor = System.Drawing.Color.Empty;
            this.pandleDock1.Location = new System.Drawing.Point(3, 23);
            this.pandleDock1.Name = "pandleDock1";
            this.pandleDock1.Size = new System.Drawing.Size(794, 80);
            this.pandleDock1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pandleDock1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.pandleDock1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.pandleDock1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.pandleDock1.Style.GradientAngle = 90;
            this.pandleDock1.TabIndex = 0;
            // 
            // pictTop
            // 
            this.pictTop.BackColor = System.Drawing.Color.Transparent;
            this.pictTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictTop.Image = global::Bgkj.Properties.Resources._11;
            this.pictTop.Location = new System.Drawing.Point(0, 0);
            this.pictTop.Name = "pictTop";
            this.pictTop.Size = new System.Drawing.Size(794, 80);
            this.pictTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictTop.TabIndex = 0;
            this.pictTop.TabStop = false;
            // 
            // dock1
            // 
            this.dock1.Control = this.pandleDock1;
            this.dock1.Name = "dock1";
            this.dock1.Text = "BG";
            // 
            // dock3
            // 
            this.dock3.Name = "dock3";
            this.dock3.Text = "Show Module";
            // 
            // dockConta
            // 
            this.dockConta.CanClose = DevComponents.DotNetBar.eDockContainerClose.Yes;
            this.dockConta.Name = "dockConta";
            this.dockConta.Text = "ShowModule";
            // 
            // styleManager
            // 
            this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204))))));
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // t
            // 
            this.t.Interval = 3000;
            this.t.Tick += new System.EventHandler(this.t_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dockSite9);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.dockSite9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).EndInit();
            this.dockSite1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.bar2.ResumeLayout(false);
            this.pandleDock2.ResumeLayout(false);
            this.dockSite3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.bar1.ResumeLayout(false);
            this.pandleDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictTop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Metro.MetroStatusBar StatusBar;
        private DevComponents.DotNetBar.DotNetBarManager dotBarManger;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.PanelDockContainer pandleDock2;
        private DevComponents.DotNetBar.DockContainerItem dock2;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.PanelDockContainer pandleDock1;
        private DevComponents.DotNetBar.DockContainerItem dock1;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.SideBar sideBarManger;
        private System.Windows.Forms.PictureBox pictTop;
        public System.Windows.Forms.ImageList imgListManger;
        private DevComponents.DotNetBar.DockContainerItem dock3;
        private DevComponents.DotNetBar.DockContainerItem dockConta;
        private DevComponents.DotNetBar.StyleManager styleManager;
        private DevComponents.DotNetBar.DockSite dockSite9;
        private DevComponents.DotNetBar.Bar bar3;
        private DevComponents.DotNetBar.LabelItem labUser;
        private DevComponents.DotNetBar.LabelItem labCo;
        private DevComponents.DotNetBar.LabelItem labDate;
        private DevComponents.DotNetBar.ComboBoxItem comHouse;
        private DevComponents.Editors.ComboItem comboItem1;
        private System.Windows.Forms.Timer t;
        private System.Windows.Forms.Timer timer;
    }
}