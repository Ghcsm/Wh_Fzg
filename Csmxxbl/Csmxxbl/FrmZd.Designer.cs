namespace Csmxxbl
{
    partial class FrmZd
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
            this.txtzong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtml = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comQi = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtanjuan = new System.Windows.Forms.TextBox();
            this.butRu = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtzong
            // 
            this.txtzong.Location = new System.Drawing.Point(63, 57);
            this.txtzong.Name = "txtzong";
            this.txtzong.Size = new System.Drawing.Size(59, 21);
            this.txtzong.TabIndex = 0;
            this.txtzong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtzong_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "全宗号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目录号:";
            // 
            // txtml
            // 
            this.txtml.Location = new System.Drawing.Point(190, 57);
            this.txtml.Name = "txtml";
            this.txtml.Size = new System.Drawing.Size(64, 21);
            this.txtml.TabIndex = 2;
            this.txtml.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtml_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "期限:";
            // 
            // comQi
            // 
            this.comQi.FormattingEnabled = true;
            this.comQi.Items.AddRange(new object[] {
            "长期",
            "永久"});
            this.comQi.Location = new System.Drawing.Point(60, 100);
            this.comQi.Name = "comQi";
            this.comQi.Size = new System.Drawing.Size(62, 20);
            this.comQi.TabIndex = 3;
            this.comQi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comQi_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "案卷号:";
            // 
            // txtanjuan
            // 
            this.txtanjuan.Location = new System.Drawing.Point(190, 100);
            this.txtanjuan.Name = "txtanjuan";
            this.txtanjuan.Size = new System.Drawing.Size(64, 21);
            this.txtanjuan.TabIndex = 4;
            this.txtanjuan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtanjuan_KeyPress);
            // 
            // butRu
            // 
            this.butRu.Location = new System.Drawing.Point(310, 86);
            this.butRu.Name = "butRu";
            this.butRu.Size = new System.Drawing.Size(75, 46);
            this.butRu.TabIndex = 5;
            this.butRu.Text = "录入";
            this.butRu.UseVisualStyleBackColor = true;
            this.butRu.Click += new System.EventHandler(this.butRu_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "信息：";
            // 
            // FrmZd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(414, 157);
            this.Controls.Add(this.butRu);
            this.Controls.Add(this.txtanjuan);
            this.Controls.Add(this.comQi);
            this.Controls.Add(this.txtml);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtzong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmZd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "装订产量录入";
            this.Shown += new System.EventHandler(this.FrmZd_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtzong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtml;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comQi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtanjuan;
        private System.Windows.Forms.Button butRu;
        private System.Windows.Forms.Label label5;
    }
}