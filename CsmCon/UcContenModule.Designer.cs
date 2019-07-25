namespace CsmCon
{
    partial class UcContenModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcContenModule));
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtlx = new System.Windows.Forms.TextBox();
            this.lvConten = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.butDel = new DevComponents.DotNetBar.ButtonX();
            this.butNew = new DevComponents.DotNetBar.ButtonX();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.txtLb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtlx);
            this.groupPanel1.Controls.Add(this.lvConten);
            this.groupPanel1.Controls.Add(this.butDel);
            this.groupPanel1.Controls.Add(this.butNew);
            this.groupPanel1.Controls.Add(this.txtTitle);
            this.groupPanel1.Controls.Add(this.txtNum);
            this.groupPanel1.Controls.Add(this.txtLb);
            this.groupPanel1.Controls.Add(this.label3);
            this.groupPanel1.Controls.Add(this.label2);
            this.groupPanel1.Controls.Add(this.label4);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(749, 444);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            // 
            // txtlx
            // 
            this.txtlx.Location = new System.Drawing.Point(88, 63);
            this.txtlx.Name = "txtlx";
            this.txtlx.Size = new System.Drawing.Size(241, 21);
            this.txtlx.TabIndex = 4;
            this.txtlx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // lvConten
            // 
            this.lvConten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.m_id});
            this.lvConten.FullRowSelect = true;
            this.lvConten.GridLines = true;
            this.lvConten.HideSelection = false;
            this.lvConten.Location = new System.Drawing.Point(6, 105);
            this.lvConten.Name = "lvConten";
            this.lvConten.Size = new System.Drawing.Size(731, 329);
            this.lvConten.TabIndex = 7;
            this.lvConten.UseCompatibleStateImageBehavior = false;
            this.lvConten.View = System.Windows.Forms.View.Details;
            this.lvConten.Click += new System.EventHandler(this.lvConten_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类别";
            this.columnHeader3.Width = 65;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "代码";
            this.columnHeader1.Width = 65;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标题";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 4;
            this.columnHeader4.Text = "所属类型";
            this.columnHeader4.Width = 200;
            // 
            // m_id
            // 
            this.m_id.DisplayIndex = 3;
            this.m_id.Text = "id";
            this.m_id.Width = 0;
            // 
            // butDel
            // 
            this.butDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDel.Location = new System.Drawing.Point(478, 64);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(64, 23);
            this.butDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDel.TabIndex = 6;
            this.butDel.Text = "删除";
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // butNew
            // 
            this.butNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butNew.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butNew.Location = new System.Drawing.Point(363, 64);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(64, 23);
            this.butNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butNew.TabIndex = 5;
            this.butNew.Text = "新增";
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            this.butNew.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.butNew_KeyPress);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(245, 29);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(465, 21);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTitle_KeyPress);
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(152, 29);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(46, 21);
            this.txtNum.TabIndex = 2;
            this.txtNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            // 
            // txtLb
            // 
            this.txtLb.Location = new System.Drawing.Point(59, 29);
            this.txtLb.Name = "txtLb";
            this.txtLb.Size = new System.Drawing.Size(46, 21);
            this.txtLb.TabIndex = 1;
            this.txtLb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLb_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(204, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "标题:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(111, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "代码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(21, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "所属类型:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类别:";
            // 
            // UcContenModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 444);
            this.Controls.Add(this.groupPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UcContenModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "目录模版";
            this.Shown += new System.EventHandler(this.UcContenModule_Shown);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtLb;
        private DevComponents.DotNetBar.ButtonX butDel;
        private DevComponents.DotNetBar.ButtonX butNew;
        private System.Windows.Forms.ListView lvConten;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader m_id;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtlx;
    }
}