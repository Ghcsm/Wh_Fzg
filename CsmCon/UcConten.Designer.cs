﻿namespace CsmCon
{
    partial class UcConten
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gr = new System.Windows.Forms.GroupBox();
            this.lvconten = new System.Windows.Forms.ListView();
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gr.SuspendLayout();
            this.SuspendLayout();
            // 
            // gr
            // 
            this.gr.Controls.Add(this.lvconten);
            this.gr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr.Location = new System.Drawing.Point(0, 0);
            this.gr.Name = "gr";
            this.gr.Size = new System.Drawing.Size(219, 342);
            this.gr.TabIndex = 0;
            this.gr.TabStop = false;
            // 
            // lvconten
            // 
            this.lvconten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1,
            this.columnHeader1});
            this.lvconten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvconten.FullRowSelect = true;
            this.lvconten.GridLines = true;
            this.lvconten.HideSelection = false;
            this.lvconten.Location = new System.Drawing.Point(3, 17);
            this.lvconten.Name = "lvconten";
            this.lvconten.Size = new System.Drawing.Size(213, 322);
            this.lvconten.SmallImageList = this.imageList1;
            this.lvconten.TabIndex = 0;
            this.lvconten.UseCompatibleStateImageBehavior = false;
            this.lvconten.View = System.Windows.Forms.View.Details;
            this.lvconten.Click += new System.EventHandler(this.lvconten_Click);
            // 
            // col1
            // 
            this.col1.Text = "序号";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "id";
            this.columnHeader1.Width = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 25);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // UcConten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gr);
            this.Name = "UcConten";
            this.Size = new System.Drawing.Size(219, 342);
            this.Load += new System.EventHandler(this.UcConten_Load);
            this.gr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr;
        private System.Windows.Forms.ListView lvconten;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
