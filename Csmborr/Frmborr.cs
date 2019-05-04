using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmborr
{
    public partial class FrmBorr : Form
    {
        public FrmBorr()
        {
            InitializeComponent();
        }


        private bool istxt()
        {
            if (comborrbcol.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询字段!");
                comborrbcol.Focus();
                return false;
            }

            if (comborrbczf.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择操作符!");
                comborrbczf.Focus();
                return false;
            }
            if (txtborrgjz.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入关键字!");
                txtborrgjz.Focus();
                return false;
            }
            return true;
        }

        private void butQuer_Click(object sender, EventArgs e)
        {
            if (!istxt())
                return;
            string col = comborrbcol.Text.Trim();
            string czf = comborrbczf.Text.Trim();
            string gjz = txtborrgjz.Text.Trim();
            BorrMethod.Getdata(lvborrQuer, col, czf, gjz);
        }


        private void FrmBorr_Shown(object sender, EventArgs e)
        {
            BorrMethod.Getinfo(lvborrQuer, comborrbcol);
        }

        private void lvborrQuer_Click(object sender, EventArgs e)
        {


        }

        private void lvborrQuer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                contMenu.Visible = true;
                contMenu.Location = e.Location;
            }
        }

        private void lvborrQuer_MouseUp(object sender, MouseEventArgs e)
        {
            if (lvborrQuer.SelectedItems.Count <= 0)
                contMenu.Visible = false;
        }

        private void butImgload_Click(object sender, EventArgs e)
        {

        }
        private void butitemArchgh_Click(object sender, EventArgs e)
        {
            contMenu.Visible = false;
            FrmArchBorr borr=new FrmArchBorr();
            borr.ShowDialog();
        }

        private void butitemArchjy_Click(object sender, EventArgs e)
        {
            contMenu.Visible = false;
            FrmArchBorr borr = new FrmArchBorr();
            borr.ShowDialog();
        }
    }
}
