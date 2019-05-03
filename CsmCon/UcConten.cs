using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace CsmCon
{
    public partial class UcConten : UserControl
    {
        public UcConten()
        {
            InitializeComponent();
        }
        public static int Archid { get; set; }

        void Ini()
        {
            ClsConten.LoadContents(Archid, lvconten);
        }

        public void LoadConten(int archid)
        {
            Archid = archid;
            ClsConten.LoadContents(archid, lvconten);
        }

        private void UcConten_Load(object sender, EventArgs e)
        {
            Ini();
        }
    }
}
