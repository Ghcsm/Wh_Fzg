using System;
using System.Windows.Forms;
using DAL;
namespace Csmgy
{
    public partial class Frmgy : Form
    {
        
        public Frmgy()
        {
            InitializeComponent();
        }
        string id = "";
     
        private void Frmgy_Load(object sender, EventArgs e)
        {
            string str= T_ConFigure.SfCoName;
            if (str.Trim().Length<=0)
                Application.Exit();
            lab_soft.Text = str;
            id = T_ConFigure.Moid;            
        }
		void FrmgyShown(object sender, EventArgs e)
		{
           
			this.lab_kh.Text="   致力打造全国\n档案电子互联网化，方便民生推动社会加快发展!";
            this.ID.Text = string.Format("ID:{0}", id);            
		}

        private void ID_Click(object sender, EventArgs e)
        {
            if (id.Length > 1)
            {
                Clipboard.SetText(id);
                MessageBox.Show("已复制到剪贴板.");
            }
        }
	
    }
}
