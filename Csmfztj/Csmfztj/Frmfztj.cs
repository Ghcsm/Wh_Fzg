using DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace Csmfztj
{
    public partial class Frmfztj : Form
    {

        public Frmfztj()
        {
            InitializeComponent();
        }

        private void Frmfztj_Load(object sender, EventArgs e)
        {
            if (DAL.T_User.UserId == 1) {
                sTabm2.Visible = true;
                sTabm3.Visible = true;
            }
        }

        private void Frmfztj_Shown(object sender, EventArgs e)
        {

            this.dateTime_qishi_date.CustomFormat = "yyyy-MM-dd";
            this.dateTime_qishi_date.Format = DateTimePickerFormat.Custom;

            this.dateTime_zhongz_date.CustomFormat = "yyyy-MM-dd";
            this.dateTime_zhongz_date.Format = DateTimePickerFormat.Custom;
        }



        private void button_tj_Click(object sender, EventArgs e)
        {
            this.but_datj_tj.Enabled = false;
            string BeginTime = dateTime_qishi_date.Text + " " + txt_qi_time.Text.Trim();
            string EndTime = dateTime_zhongz_date.Text + " " + txt_zh_time.Text.Trim();
            DateTime dt;
            dgvWorkgroup.Rows.Clear();
            if (!DateTime.TryParse(BeginTime, out dt)) {
                throw new ApplicationException("不是正确的日期格式类型，请检查！");

            }
            if (!DateTime.TryParse(BeginTime, out dt)) {
                throw new ApplicationException("不是正确的日期格式类型，请检查！");
            }
            if (sTabm1.IsSelected)
                WorkGroup(BeginTime, EndTime);
            else
                AdminWorkGroup(BeginTime, EndTime);
            this.but_datj_tj.Enabled = true;
        }

        private void WorkGroup(string s1, string s2)
        {
            try {
                DataTable data = Common.GetGroup(s1, s2);
                if (data == null || data.Rows.Count <= 0)
                    return;
                int i = 1;
                foreach (DataRow dr in data.Rows) {
                    // ListViewItem lvi = new ListViewItem();
                    string guser = dr["UserName"].ToString();
                    string gansj = dr["Cabinet"].ToString();
                    string gdamlt = dr["damlt"].ToString();
                    string gdamljuan = dr["dajuan"].ToString();
                    string gxxblt = dr["dablt"].ToString();
                    string gxxblt2 = dr["dablt2"].ToString();
                    string gxxbljuan = dr["dabljuan"].ToString();
                    string gxxbljuan2 = dr["dabljuan2"].ToString();
                    string gscanpagex = dr["scanpagex"].ToString();
                    string gscanjuanx = dr["scanjuanx"].ToString();
                    string gscanpages = dr["scanpages"].ToString();
                    string gscanjuans = dr["scanjuans"].ToString();
                    string gindexpage = dr["indexpage"].ToString();
                    string gindexjuan = dr["indexjuan"].ToString();
                    string gcheckpage = dr["checkpage"].ToString();
                    string gcheckjuan = dr["checkjuan"].ToString();
                    string gzhenglipage = dr["zhenglipage"].ToString();
                    string gzhenglijuan = dr["zhenglijuan"].ToString();
                    string gzhuangdpage = dr["zhuangdingpage"].ToString();
                    string gzhuangdjuan = dr["zhuangdingjuan"].ToString();
                    string infochkywid = dr["infochkywid"].ToString();
                    string infochkpage = dr["infochkpage"].ToString();
                    string infochkjuan = dr["infochjuan"].ToString();
                    string zchkpage = dr["zchkpage"].ToString();
                    string zchkjuan = dr["zhkjuan"].ToString();
                    string luru = dr["luru"].ToString();
                    if (gansj == "") {
                        gansj = "0";
                    }
                    if (gdamlt == "") {
                        gdamlt = "0";
                    }
                    if (gdamljuan == "") {
                        gdamljuan = "0";
                    }
                    if (gxxblt == "") {
                        gxxblt = "0";
                    }
                    if (gxxblt2 == "") {
                        gxxblt2 = "0";
                    }
                    if (gxxbljuan == "") {
                        gxxbljuan = "0";
                    }
                    if (gxxbljuan2 == "") {
                        gxxbljuan2 = "0";
                    }
                    if (gscanpagex == "") {
                        gscanpagex = "0";
                    }
                    if (gscanjuanx == "") {
                        gscanjuanx = "0";
                    }
                    if (gscanpages == "") {
                        gscanpages = "0";
                    }
                    if (gscanjuans == "") {
                        gscanjuans = "0";
                    }
                    if (gindexpage == "") {
                        gindexpage = "0";
                    }
                    if (gindexjuan == "") {
                        gindexjuan = "0";
                    }
                    if (gcheckpage == "") {
                        gcheckpage = "0";
                    }
                    if (gcheckjuan == "") {
                        gcheckjuan = "0";
                    }
                    if (gzhenglipage == "") {
                        gzhenglipage = "0";
                    }
                    if (gzhenglijuan == "") {
                        gzhenglijuan = "0";
                    }
                    if (gzhuangdpage == "") {
                        gzhuangdpage = "0";
                    }
                    if (gzhuangdjuan == "") {
                        gzhuangdjuan = "0";
                    }

                    if (infochkjuan == "")
                        infochkjuan = "0";
                    if (infochkpage == "")
                        infochkpage = "0";
                    if (infochkywid == "")
                        infochkywid = "0";
                    if (zchkjuan == "")
                        zchkjuan = "0";
                    if (zchkpage == "")
                        zchkpage = "0";
                    if (luru == "")
                        luru = "0";
                    int t = dgvWorkgroup.Rows.Add();
                    dgvWorkgroup.Rows[t].Cells["id"].Value = i.ToString();
                    dgvWorkgroup.Rows[t].Cells["T_user"].Value = guser;
                    dgvWorkgroup.Rows[t].Cells["T_ansj"].Value = gansj + "卷";
                    dgvWorkgroup.Rows[t].Cells["T_xxbl"].Value = gxxbljuan + "卷" + gxxblt + "条";
                    dgvWorkgroup.Rows[t].Cells["T_xxbl2"].Value = gxxbljuan2 + "卷" + gxxblt2 + "条";
                    dgvWorkgroup.Rows[t].Cells["T_mldj"].Value = gdamljuan+"卷"+gdamlt + "条";
                    dgvWorkgroup.Rows[t].Cells["T_scanx"].Value = gscanjuanx + "卷" + gscanpagex + "页";
                    dgvWorkgroup.Rows[t].Cells["T_scans"].Value = gscanjuans + "卷" + gscanpages + "页";
                    dgvWorkgroup.Rows[t].Cells["T_index"].Value = gindexjuan + "卷" + gindexpage + "页";
                    dgvWorkgroup.Rows[t].Cells["T_check"].Value = gcheckjuan + "卷" + gcheckpage + "页";
                    dgvWorkgroup.Rows[t].Cells["T_zhengli"].Value = gzhenglijuan + "卷" + gzhenglipage + "页";
                  //  dgvWorkgroup.Rows[t].Cells["T_zhuangD"].Value = gzhuangdjuan + "卷" + gzhuangdpage + "页";
                    dgvWorkgroup.Rows[t].Cells["T_Infochk"].Value = infochkywid + "手" + infochkjuan + "卷"+ infochkpage + "页";
                    dgvWorkgroup.Rows[t].Cells["T_Zchk"].Value = zchkjuan + "卷" +zchkpage + "页";
                    dgvWorkgroup.Rows[t].Cells["T_luru"].Value = luru + "卷";
                    dgvWorkgroup.Rows[t].Cells["T_zhangding"].Value = gzhuangdjuan + "卷" + gzhuangdpage + "页";
                    i++;
                }

            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void AdminWorkGroup(string s1, string s2)
        {
            DataTable dt = Common.GetGroupAdmin(s1, s2, "0", "0", "1");
            dgvAdminWorkGroup.DataSource = dt;
        }

        private bool istxt()
        {
            if (rabox.Checked) {
                if (txtbox1.Text.Trim().Length <= 0 || txtbox2.Text.Trim().Length <= 0)
                    return false;
                try {
                    int b;
                    int b1;
                    bool bl1 = int.TryParse(txtbox1.Text.Trim(), out b);
                    bool bl2 = int.TryParse(txtbox2.Text.Trim(), out b1);
                    if (!bl1 || !bl2)
                        return false;
                    if (b <= 0 | b > b1)
                        return false;
                    return true;
                } catch {
                    return false;
                }
            }
            else {
                if (txtbox1.Text.Trim().Length <= 0)
                    return false;
                int b;
                bool bl = int.TryParse(txtbox1.Text.Trim(), out b);
                if (!bl)
                    return false;
            }
            return true;

        }
        private void butBoxTj_Click(object sender, EventArgs e)
        {
            if (!istxt()) {
                MessageBox.Show("请输入正确的盒号范围!");
                txtbox1.Focus();
                return;
            }
            DataTable dt = null;
            if (rabox.Checked)
                dt = Common.GetGroupAdmin("0", "0", txtbox1.Text.Trim(), txtbox2.Text.Trim(), "2");
            else
                dt = Common.GetGroupAdmin("0", "0", txtbox1.Text.Trim().PadLeft(4,'0'), "", "3");
            dgvBoxWordGroup.DataSource = dt;
        }

        private void rabqu_CheckedChanged(object sender, EventArgs e)
        {
            if (rabqu.Checked)
                txtbox2.Enabled = false;
            else {
                txtbox1.Enabled = true;
                txtbox2.Enabled = true;
            }

        }
      
    }
}
