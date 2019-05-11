namespace Csmmldj
{
    partial class Frmmldj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmmldj));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.gArchSelect1 = new CsmCon.gArchSelect();
            this.gr2 = new System.Windows.Forms.GroupBox();
            this.gr1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.ico");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(954, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gr1
            // 
            this.gr1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gr1.Controls.Add(this.gArchSelect1);
            this.gr1.Location = new System.Drawing.Point(12, 28);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(395, 529);
            this.gr1.TabIndex = 1;
            this.gr1.TabStop = false;
            // 
            // gArchSelect1
            // 
            this.gArchSelect1.Archid = 0;
            this.gArchSelect1.ArchImgFile = null;
            this.gArchSelect1.ArchRegPages = 0;
            this.gArchSelect1.Archtype = null;
            this.gArchSelect1.Boxsn = 0;
            this.gArchSelect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gArchSelect1.GotoPages = false;
            this.gArchSelect1.LoadFileBoole = false;
            this.gArchSelect1.Location = new System.Drawing.Point(3, 17);
            this.gArchSelect1.Name = "gArchSelect1";
            this.gArchSelect1.PagesEnd = false;
            this.gArchSelect1.Size = new System.Drawing.Size(389, 509);
            this.gArchSelect1.TabIndex = 0;
            this.gArchSelect1.LineClickLoadInfo += new CsmCon.gArchSelect.ArchSelectHandle(this.gArchSelect1_LineClickLoadInfo);
            this.gArchSelect1.LineFocus += new CsmCon.gArchSelect.ArchSelectHandleFocus(this.gArchSelect1_LineFocus);
            // 
            // gr2
            // 
            this.gr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr2.Location = new System.Drawing.Point(413, 28);
            this.gr2.Name = "gr2";
            this.gr2.Size = new System.Drawing.Size(529, 529);
            this.gr2.TabIndex = 2;
            this.gr2.TabStop = false;
            // 
            // Frmmldj
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(954, 569);
            this.Controls.Add(this.gr2);
            this.Controls.Add(this.gr1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frmmldj";
            this.Text = "目录录入";
            this.Load += new System.EventHandler(this.Frmmldj_Load);
            this.Shown += new System.EventHandler(this.Frmmldj_Shown);
            this.gr1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.GroupBox gr2;
        private CsmCon.gArchSelect gArchSelect1;
    }
}