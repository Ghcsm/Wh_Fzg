using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmdasm
{
    public partial class Frmnum : Form
    {
        public Frmnum()
        {
            InitializeComponent();
        }
        public static int Page=0;
        private void txt_p_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.but_ok.Focus();
            if (!(Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8))
                e.Handled = true;
        }

        private void Frmnum_Shown(object sender, EventArgs e)
        {
            this.txt_p.Focus();
        }

        private int istxt(string txt)
        {
            try
            {
                int numPage = int.Parse(txt);
                if (numPage > 0)
                {
                    return numPage;
                }
                return 0;
            }
            catch { return 0; }
        }
        private void but_ok_Click(object sender, EventArgs e)
        {
            Page = istxt(this.txt_p.Text.Trim());
            this.Close();
        }
    }
}
