using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsmInfoChk
{
    public partial class Frmgoto : Form
    {
        public Frmgoto()
        {
            InitializeComponent();
        }
        public static int Npage;
        public static int Maxpage;
        private void butOK_Click(object sender, EventArgs e)
        {
            try {
                if (txtPage.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入相应页码!");
                    return;
                }
                if (Convert.ToInt32(txtPage.Text.Trim()) <= 0) {
                    MessageBox.Show("页码不能为0!");
                    return;
                }
                if (Convert.ToInt32(txtPage.Text.Trim()) > Maxpage) {
                    MessageBox.Show("指定页码不能大于总页码!");
                    return;
                }
                Npage = Convert.ToInt32(txtPage.Text.Trim());
            } catch {
                Npage = 0;
            } finally {
                this.Close();
            }
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butOK.Focus();
        }
    }
}
