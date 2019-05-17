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
        public event CntSelectHandle OneClickGotoPage;
        public delegate void CntSelectHandle(object sender, EventArgs e);
        public static int Archid { get; set; }
        public static int ArchPages { get; set; }

        void Ini()
        {
            ClsConten.GetContenInfo();
            Lvnameadd();
        }

        private void Lvnameadd()
        {
            if (ClsContenInfo.ContenCoList.Count <= 0)
                return;
            for (int i = 0; i < ClsContenInfo.ContenCoList.Count; i++) {
                string str = ClsContenInfo.ContenCoList[i];
                lvconten.Columns.Add(str);
            }
        }

        public void LoadConten(int archid)
        {
            Archid = archid;
            ClsConten.LoadContents(archid, lvconten);
        }

        public void CloseConten()
        {
            lvconten.Items.Clear();;
        }

        private void UcConten_Load(object sender, EventArgs e)
        {
            Ini();
        }

        private void lvconten_Click(object sender, EventArgs e)
        {
            int x = lvconten.SelectedItems[0].Index;
            if (ClsContenInfo.PageCount2.Count > 0)
            {
                ArchPages = Convert.ToInt32(ClsContenInfo.PageCount2[x]);
                if (ArchPages > 0)
                    OneClickGotoPage?.Invoke(sender, e);
            }
        }

    }
}
