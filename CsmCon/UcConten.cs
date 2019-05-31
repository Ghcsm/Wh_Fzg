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
        public static int Mtmpid { get; set; } = 0;
        ClsContenInfo info = new ClsContenInfo();
        void Ini()
        {
            info.GetContenInfo();
            Lvnameadd();
        }

        private void Lvnameadd()
        {
            if (info.ContenCoList.Count <= 0)
                return;
            for (int i = 0; i < info.ContenCoList.Count; i++) {
                string str = info.ContenCoList[i];
                if (i == info.TitleWz + 2)
                    lvconten.Columns[i].Width = 200;
                else if (i > 1)
                    lvconten.Columns[i].Width = 100;
                lvconten.Columns.Add(str);
            }
        }

        public void LoadConten(int archid)
        {
            Archid = archid;
            info.LoadContents(archid, lvconten);
        }

        public void CloseConten()
        {
            lvconten.Items.Clear(); ;
        }

        private void UcConten_Load(object sender, EventArgs e)
        {
            Ini();
        }

        private void lvconten_Click(object sender, EventArgs e)
        {
            int x = lvconten.SelectedItems[0].Index;
            if (info.PageCount2.Count > 0) {
                ArchPages = Convert.ToInt32(info.PageCount2[x]);
                if (ArchPages > 0)
                    OneClickGotoPage?.Invoke(sender, e);
            }
        }

    }
}
