using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsmCon;

namespace Csmmldj
{
    public partial class FrmLsinfo : Form
    {
        public FrmLsinfo()
        {
            InitializeComponent();
        }

        private UcInfoGet lsinfo;
        public static List<string> lsname = new List<string>();
        public static List<string> lsdata = new List<string>();
        private void FrmLsinfo_Load(object sender, EventArgs e)
        {
            lsname.Clear();
            lsdata.Clear();
            lsinfo =new UcInfoGet();
            lsinfo.Dock = DockStyle.Fill;
            lsinfo.DoubleClk += Lsinfo_DoubleClk;
            gr1.Controls.Add(lsinfo);
        }

        private void Lsinfo_DoubleClk(object sender, EventArgs e)
        {
            lsname = UcInfoGet.lsName;
            lsdata = UcInfoGet.lsdata;
            this.Close();
        }
    }
}
