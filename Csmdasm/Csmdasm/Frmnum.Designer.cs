namespace Csmdasm
{
    partial class Frmnum
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_p = new System.Windows.Forms.TextBox();
            this.but_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "页码:";
            // 
            // txt_p
            // 
            this.txt_p.Location = new System.Drawing.Point(46, 12);
            this.txt_p.Name = "txt_p";
            this.txt_p.Size = new System.Drawing.Size(91, 21);
            this.txt_p.TabIndex = 1;
            this.txt_p.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_p_KeyPress);
            // 
            // but_ok
            // 
            this.but_ok.Location = new System.Drawing.Point(78, 46);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new System.Drawing.Size(49, 23);
            this.but_ok.TabIndex = 2;
            this.but_ok.Text = "确定";
            this.but_ok.UseVisualStyleBackColor = true;
            this.but_ok.Click += new System.EventHandler(this.but_ok_Click);
            // 
            // Frmnum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(157, 81);
            this.Controls.Add(this.but_ok);
            this.Controls.Add(this.txt_p);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frmnum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "转跳页码";
            this.Shown += new System.EventHandler(this.Frmnum_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_p;
        private System.Windows.Forms.Button but_ok;
    }
}