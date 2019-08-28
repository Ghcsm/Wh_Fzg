namespace CsmImg
{
    partial class FrmCombin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCombin));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ImageView = new Leadtools.Controls.ImageViewer();
            this.butLoad = new System.Windows.Forms.Button();
            this.butZoomM = new System.Windows.Forms.Button();
            this.butZooms = new System.Windows.Forms.Button();
            this.butRectl = new System.Windows.Forms.Button();
            this.butSider = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ImageView);
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 620);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ImageView
            // 
            this.ImageView.BackColor = System.Drawing.Color.Gray;
            this.ImageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageView.IsSyncSource = true;
            this.ImageView.IsSyncTarget = true;
            this.ImageView.ItemPadding = new System.Windows.Forms.Padding(1);
            this.ImageView.Location = new System.Drawing.Point(3, 17);
            this.ImageView.Name = "ImageView";
            this.ImageView.Size = new System.Drawing.Size(798, 600);
            this.ImageView.TabIndex = 0;
            this.ImageView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageView_KeyDown);
            // 
            // butLoad
            // 
            this.butLoad.Location = new System.Drawing.Point(137, 12);
            this.butLoad.Name = "butLoad";
            this.butLoad.Size = new System.Drawing.Size(75, 41);
            this.butLoad.TabIndex = 1;
            this.butLoad.Text = "加载图像";
            this.butLoad.UseVisualStyleBackColor = true;
            this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
            // 
            // butZoomM
            // 
            this.butZoomM.Location = new System.Drawing.Point(245, 12);
            this.butZoomM.Name = "butZoomM";
            this.butZoomM.Size = new System.Drawing.Size(75, 41);
            this.butZoomM.TabIndex = 2;
            this.butZoomM.Text = "放大";
            this.butZoomM.UseVisualStyleBackColor = true;
            this.butZoomM.Click += new System.EventHandler(this.butZoomM_Click);
            // 
            // butZooms
            // 
            this.butZooms.Location = new System.Drawing.Point(336, 12);
            this.butZooms.Name = "butZooms";
            this.butZooms.Size = new System.Drawing.Size(75, 41);
            this.butZooms.TabIndex = 2;
            this.butZooms.Text = "缩小";
            this.butZooms.UseVisualStyleBackColor = true;
            this.butZooms.Click += new System.EventHandler(this.butZooms_Click);
            // 
            // butRectl
            // 
            this.butRectl.Location = new System.Drawing.Point(432, 12);
            this.butRectl.Name = "butRectl";
            this.butRectl.Size = new System.Drawing.Size(75, 41);
            this.butRectl.TabIndex = 2;
            this.butRectl.Text = "旋转";
            this.butRectl.UseVisualStyleBackColor = true;
            this.butRectl.Click += new System.EventHandler(this.butRectl_Click);
            // 
            // butSider
            // 
            this.butSider.Location = new System.Drawing.Point(528, 12);
            this.butSider.Name = "butSider";
            this.butSider.Size = new System.Drawing.Size(75, 41);
            this.butSider.TabIndex = 2;
            this.butSider.Text = "去边";
            this.butSider.UseVisualStyleBackColor = true;
            this.butSider.Click += new System.EventHandler(this.butSider_Click);
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(651, 12);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 41);
            this.butSave.TabIndex = 2;
            this.butSave.Text = "保存";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(62, 23);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(47, 21);
            this.txtPage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "页码:";
            // 
            // FrmCombin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 691);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butSider);
            this.Controls.Add(this.butRectl);
            this.Controls.Add(this.butZooms);
            this.Controls.Add(this.butZoomM);
            this.Controls.Add(this.butLoad);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCombin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像拼接";
            this.Shown += new System.EventHandler(this.FrmCombin_Shown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Leadtools.Controls.ImageViewer ImageView;
        private System.Windows.Forms.Button butLoad;
        private System.Windows.Forms.Button butZoomM;
        private System.Windows.Forms.Button butZooms;
        private System.Windows.Forms.Button butRectl;
        private System.Windows.Forms.Button butSider;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label label1;
    }
}