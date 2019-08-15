namespace Csmdajc
{
    partial class FrmSharePenSet
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
            this.butHf = new System.Windows.Forms.Button();
            this.txtYuzhi = new System.Windows.Forms.TextBox();
            this.txtBanj = new System.Windows.Forms.TextBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butHf
            // 
            this.butHf.Location = new System.Drawing.Point(121, 129);
            this.butHf.Name = "butHf";
            this.butHf.Size = new System.Drawing.Size(73, 30);
            this.butHf.TabIndex = 11;
            this.butHf.TabStop = false;
            this.butHf.Text = "恢复默认";
            this.butHf.UseVisualStyleBackColor = true;
            this.butHf.Click += new System.EventHandler(this.butHf_Click);
            // 
            // txtYuzhi
            // 
            this.txtYuzhi.Location = new System.Drawing.Point(118, 92);
            this.txtYuzhi.Name = "txtYuzhi";
            this.txtYuzhi.Size = new System.Drawing.Size(77, 21);
            this.txtYuzhi.TabIndex = 3;
            this.txtYuzhi.Text = "0";
            this.txtYuzhi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYuzhi_KeyPress);
            // 
            // txtBanj
            // 
            this.txtBanj.Location = new System.Drawing.Point(118, 54);
            this.txtBanj.Name = "txtBanj";
            this.txtBanj.Size = new System.Drawing.Size(77, 21);
            this.txtBanj.TabIndex = 2;
            this.txtBanj.Text = "250";
            this.txtBanj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBanj_KeyPress);
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(118, 16);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(77, 21);
            this.txtNum.TabIndex = 1;
            this.txtNum.Tag = "";
            this.txtNum.Text = "60";
            this.txtNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "阈值 0-255";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "半径 1-250 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "数量 0-500 :";
            // 
            // FrmSharePenSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 177);
            this.Controls.Add(this.butHf);
            this.Controls.Add(this.txtYuzhi);
            this.Controls.Add(this.txtBanj);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSharePenSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSharePenSet_FormClosing);
            this.Shown += new System.EventHandler(this.FrmSharePenSet_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSharePenSet_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butHf;
        private System.Windows.Forms.TextBox txtYuzhi;
        private System.Windows.Forms.TextBox txtBanj;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}