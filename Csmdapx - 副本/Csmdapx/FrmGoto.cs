using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmdapx
{
    public partial class FrmGoto : Form
    {
        public FrmGoto()
        {
            InitializeComponent();
        }
        public static int Npage;
        public static int Maxpage;
      
        private void but_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_page.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请输入相应页码!");
                    return;
                }
                if (Convert.ToInt32(txt_page.Text.Trim()) <= 0)
                {
                    MessageBox.Show("页码不能为0!");
                    return;
                }
                if (Convert.ToInt32(txt_page.Text.Trim()) > Maxpage)
                {
                    MessageBox.Show("指定页码不能大于总页码!");
                    return;
                }
                Npage = Convert.ToInt32(txt_page.Text.Trim());
            }
            catch
            {
                Npage = 0;
            }
            finally
            {
                this.Close();
            }
        }

        private void txt_page_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                but_ok.Focus();
            }
        }
    }
}
