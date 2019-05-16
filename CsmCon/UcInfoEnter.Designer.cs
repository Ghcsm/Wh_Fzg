namespace CsmCon
{
    partial class UcInfoEnter
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.gr = new System.Windows.Forms.GroupBox();
            this.gr.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 17);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(817, 481);
            this.tabControl.TabIndex = 0;
            // 
            // gr
            // 
            this.gr.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gr.Controls.Add(this.tabControl);
            this.gr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr.Location = new System.Drawing.Point(0, 0);
            this.gr.Name = "gr";
            this.gr.Size = new System.Drawing.Size(823, 501);
            this.gr.TabIndex = 0;
            this.gr.TabStop = false;
            // 
            // UcInfoEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.gr);
            this.Name = "UcInfoEnter";
            this.Size = new System.Drawing.Size(823, 501);
            this.gr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.GroupBox gr;
    }
}
