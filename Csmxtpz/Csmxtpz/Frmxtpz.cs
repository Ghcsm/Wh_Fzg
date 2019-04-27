using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DAL;

namespace Csmxtpz
{
    public partial class Frmxtpz : Form
    {
        public Frmxtpz()
        {
            InitializeComponent();
        }
        private void Frmxtpz_Load(object sender, EventArgs e)
        {
            LoadFtpInfo();
            Action ac = LoadInfo;
            ac();
        }

        private void LoadInfo()
        {
            LoadHouse();
            InitHouse();
        }

        private void txtGuiHao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isValid()) {
                return;
            }

            V_HouseName.HouseSetName = txtHoseName.Text.Trim();
            if (T_Sysset.SelectHouseName()) {
                MessageBox.Show("库房名称已存在！");
                this.txtHoseName.Focus();
                return;
            }
            V_HouseName.HouseSetType = cboHouseType.Text.Trim();
            V_HouseName.MaxBoxNum = Convert.ToInt32(txtHouseBoxjuan.Text.Trim());
            T_Sysset.AddHouseInfo();
            LoadHouse();
        }


        private void LoadHouse()
        {
            try {
                cboHouseType.Text = "";
                txtHoseName.Text = "";
                txtHouseBoxjuan.Text = "";
                DataTable dt = T_Sysset.GetHouseInfo();
                if (dt != null && dt.Rows.Count > 0) {
                    this.lsvHouse.Items.Clear();
                    int i = 1;
                    string tmpHouseType = string.Empty;
                    string tmpHouse = string.Empty;
                    string tmpMaxBox = string.Empty;
                    string tmpHouseID = string.Empty;

                    foreach (DataRow dr in dt.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        tmpHouseType = dr["HouseType"].ToString();
                        tmpHouse = dr["HouseName"].ToString();
                        tmpMaxBox = dr["MaxBoxNum"].ToString();
                        tmpHouseID = dr["id"].ToString();
                        lvi.SubItems.AddRange(new string[] { tmpHouseType, tmpHouse, tmpMaxBox, tmpHouseID });
                        this.lsvHouse.Items.Add(lvi);
                        i++;
                    }
                }

            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }


        private void lsvHouse_Click(object sender, EventArgs e)
        {
            try {
                if (lsvHouse.SelectedItems != null && lsvHouse.SelectedItems.Count > 0) {
                    cboHouseType.Text = lsvHouse.SelectedItems[0].SubItems[1].Text;
                    txtHoseName.Text = lsvHouse.SelectedItems[0].SubItems[2].Text;
                    txtHouseBoxjuan.Text = lsvHouse.SelectedItems[0].SubItems[3].Text;
                    Clsxtpz.selectHouseID = Convert.ToInt32(lsvHouse.SelectedItems[0].SubItems[4].Text);
                }
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isValid()) {
                return;
            }
            if (Clsxtpz.selectHouseID == 0)
                return;
            V_HouseName.HouseSetName = txtHoseName.Text.Trim();
            if (T_Sysset.SelectHouseNameid() != Clsxtpz.selectHouseID) {
                MessageBox.Show("库房名称已存在！");
                this.txtHoseName.Focus();
                return;
            }
            V_HouseName.HouseSetType = cboHouseType.Text.Trim();
            V_HouseName.MaxBoxNum = Convert.ToInt32(txtHouseBoxjuan.Text.Trim());
            T_Sysset.UpdateHouseInfo(Clsxtpz.selectHouseID.ToString());
            LoadHouse();

        }

        private bool isValid()
        {
            if (this.cboHouseType.Text.Trim() == "")
                return false;
            if (txtHoseName.Text.Trim() == "")
                return false;
            if (txtHouseBoxjuan.Text.Trim() == "")
                return false;
            if (txtHoseName.Text.IndexOf(cboHouseType.Text) == -1) {
                MessageBox.Show("库房名称中未含有库房类型");
                return false;
            }
            return true;
        }

        private void InitHouse()
        {
            try {
                List<V_HouseSet> HouseEc = new List<V_HouseSet>();
                DataTable dt = T_Sysset.GetHouseInfo();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                cboHouse.Items.Clear();
                foreach (DataRow dr in dt.Rows) {
                    V_HouseSet House = new V_HouseSet();
                    House.HouseName = dr["HOUSENAME"].ToString();
                    House.HouseID = Convert.ToInt32(dr["id"].ToString());
                    House.HouseBox = Convert.ToInt32(dr["MaxBoxNum"].ToString());
                    HouseEc.Add(House);
                }
                cboHouse.BeginUpdate();
                cboHouse.Items.Clear();
                cboHouse.Items.AddRange(HouseEc.ToArray());
                cboHouse.EndUpdate();
                cboHouse.SelectedItem = HouseEc;

            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }




        private void AddHouseSet()
        {
            V_HouseSetCs.Houseid = Clsxtpz.myHouseID;
            V_HouseSetCs.HouseGui = Convert.ToInt32(txtGuiHao.Text);
            V_HouseSetCs.HouseCol = Convert.ToInt32(txtLie.Text);
            V_HouseSetCs.HouseRow = Convert.ToInt32(txtCeng.Text);
            V_HouseSetCs.Housebox = Convert.ToInt32(txtboxsn.Text);
            V_HouseSetCs.Housejuan = Convert.ToInt16(txtJuan.Text);
            int id = 0;
            if (radio_kfsz_box.Checked) {
                foreach (ListViewItem item in lsvCabinet.Items) {
                    int CabinetNum = Convert.ToInt32(item.Text.ToString());
                    if (V_HouseSetCs.HouseGui == CabinetNum) {
                        txtGuiHao.Focus();
                        MessageBox.Show("柜号已存在");
                        return;
                    }
                }
                id = 0;
            }
            else {
                id = 1;
            }
            T_Sysset.HouseSetAdd(id);
        }
        private void btnCabinetAdd_Click(object sender, EventArgs e)
        {
            if (!CabinetValid()) {
                return;
            }
            AddHouseSet();
            LoadCabinet();
        }

        private bool CabinetValid()
        {
            if (Clsxtpz.myHouseID == 0 || cboHouse.Text.Trim().Length <= 0) {
                return false;
            }

            if (txtGuiHao.Text.Trim().Length <= 0 ||
                txtLie.Text.Trim().Length <= 0 ||
                txtCeng.Text.Trim().Length <= 0 ||
                txtboxsn.Text.Trim().Length <= 0 ||
                txtJuan.Text.Trim().Length <= 0) {
                return false;
            }
            return true;
        }

        private void LoadCabinet()
        {
            try {
                if (Clsxtpz.myHouseID == 0) {
                    return;
                }
                DataTable dt = T_Sysset.GetHouseGui(Clsxtpz.myHouseID);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                this.lsvCabinet.Items.Clear();
                int i = 1;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dr["HouseGui"].ToString();
                    lvi.SubItems.AddRange(new string[] { dr["HouseCol"].ToString(), dr["HouseRow"].ToString(), dr["HoseBoxCount"].ToString(), dr["HouseJuan"].ToString(), dr["ID"].ToString() });
                    this.lsvCabinet.Items.Add(lvi);
                    i++;
                }
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void lsvCabinet_Click(object sender, EventArgs e)
        {
            try {
                int imgboxq = 1;
                if (lsvCabinet.SelectedItems != null && lsvCabinet.SelectedItems.Count > 0) {
                    txtGuiHao.Text = lsvCabinet.SelectedItems[0].Text;
                    txtLie.Text = lsvCabinet.SelectedItems[0].SubItems[1].Text;
                    txtCeng.Text = lsvCabinet.SelectedItems[0].SubItems[2].Text;
                    txtboxsn.Text = lsvCabinet.SelectedItems[0].SubItems[3].Text;
                    txtJuan.Text = lsvCabinet.SelectedItems[0].SubItems[4].Text;
                    Clsxtpz.myCabinetID = Convert.ToInt32(lsvCabinet.SelectedItems[0].SubItems[5].Text);
                    V_HouseSetCs.Id = Clsxtpz.myCabinetID;
                }
                txt_kfsz_rongliang.Text = Convert.ToString(SnValid(txtGuiHao.Text) * SnValid(txtLie.Text) * SnValid(txtCeng.Text) * SnValid(txtboxsn.Text) * SnValid(txtJuan.Text) * 2);
                if (txtGuiHao.Text == "1") {
                    imgboxq = 1;
                }
                else {
                    imgboxq = SnValid(txtLie.Text) * SnValid(txtCeng.Text) * SnValid(txtboxsn.Text) * 2;

                }
                int imgboxz = SnValid(txtGuiHao.Text) * SnValid(txtLie.Text) * SnValid(txtCeng.Text) * SnValid(txtboxsn.Text) * 2;
                Clsxtpz.imagecount = T_Sysset.Getboxsnimage(imgboxq, imgboxz);
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private int SnValid(string sn)
        {
            try {
                if (int.Parse(sn) > 0) {
                    return int.Parse(sn);
                }
                return 1;
            } catch {
                return 1;
            }
        }

        private void ChangeHouseSet()
        {
            V_HouseSetCs.Houseid = Clsxtpz.myHouseID;
            V_HouseSetCs.HouseGui = Convert.ToInt32(txtGuiHao.Text);
            V_HouseSetCs.HouseCol = Convert.ToInt32(txtLie.Text);
            V_HouseSetCs.HouseRow = Convert.ToInt32(txtCeng.Text);
            V_HouseSetCs.Housebox = Convert.ToInt32(txtboxsn.Text);
            V_HouseSetCs.Housejuan = Convert.ToInt16(txtJuan.Text);
            T_Sysset.HouseSetChanger();

        }

        private void btnCabinetEdit_Click(object sender, EventArgs e)
        {
            if (!CabinetValid()) {
                return;
            }
            if (Clsxtpz.myCabinetID == 0) {
                return;
            }
            if (Clsxtpz.imagecount > 0) {
                MessageBox.Show("此柜存在数据，无法修改！");
                return;
            }
            ChangeHouseSet();
            LoadCabinet();
        }


        private bool isFwqTxt()
        {
            bool tf = true;
            foreach (Control c in gr1.Controls) {
                if (c is TextBox) {
                    if (c.Text.Length <= 0)
                        tf = false;
                }
            }
            return tf;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                if (!isFwqTxt()) {
                    MessageBox.Show("请完善相关信息!");
                    return;
                }
                T_ConFigure.FtpIP = txtFtpIP.Text.Trim();
                T_ConFigure.FtpPort = Convert.ToInt32(txtFtpPort.Text.Trim());
                T_ConFigure.FtpUser = txtFtpUser.Text.Trim();
                T_ConFigure.FtpPwd = txtFtpPwd.Text.Trim();
                T_ConFigure.FtpArchScan = txtFtpArchScan.Text.Trim();
                T_ConFigure.FtpArchIndex = txtFtpArchIndex.Text.Trim();
                T_ConFigure.FtpArchSave = txtFtpArchSave.Text.Trim();
                T_ConFigure.FtpArchUpdate = txtFtpArchUpdate.Text.Trim();
                T_ConFigure.FtpTmp = txtFtpArchScanTmp.Text.Trim();
                T_ConFigure.FtpTmpPath = txtFwqYsPath.Text.Trim();
                T_ConFigure.FtpBakimgFwq = Convert.ToInt32(chkFtpSever.Checked);
                T_ConFigure.FtpBakimgBd = Convert.ToInt32(chkFtpBd.Checked);
                T_ConFigure.FtpStyle = Convert.ToInt32(chkFtpStyle.Checked);
                T_ConFigure.FtpFwqPath = txtFwqPath.Text.Trim();
                if (T_Sysset.SetFtpInfo() > 0)
                    MessageBox.Show("更新成功!");
                else
                    MessageBox.Show("更新失败!");
            } catch (Exception ex) {
                MessageBox.Show("更新失败!" + ex.ToString());
            }
        }

        private void LoadFtpInfo()
        {
            try {
                T_Sysset.GetFtpset();
                txtFtpIP.Text = T_ConFigure.FtpIP;
                txtFtpPort.Text = T_ConFigure.FtpPort.ToString();
                txtFtpUser.Text = T_ConFigure.FtpUser;
                txtFtpPwd.Text = T_ConFigure.FtpPwd;
                txtFtpArchScan.Text = T_ConFigure.FtpArchScan;
                txtFtpArchIndex.Text = T_ConFigure.FtpArchIndex;
                txtFtpArchSave.Text = T_ConFigure.FtpArchSave;
                txtFtpArchUpdate.Text = T_ConFigure.FtpArchUpdate;
                txtFtpArchScanTmp.Text = T_ConFigure.FtpTmp;
                txtFwqYsPath.Text = T_ConFigure.FtpTmpPath;
                chkFtpSever.Checked = Convert.ToBoolean(T_ConFigure.FtpBakimgFwq);
                chkFtpBd.Checked = Convert.ToBoolean(T_ConFigure.FtpBakimgBd);
                chkFtpStyle.Checked = Convert.ToBoolean(T_ConFigure.FtpStyle);
                txtFwqPath.Text = T_ConFigure.FtpFwqPath;
            } catch (Exception ex) {
                MessageBox.Show("FTP信息获取失败!" + ex.ToString());
            }
        }

        private void cboHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                V_HouseSet v_house = cboHouse.SelectedItem as V_HouseSet;
                Clsxtpz.myHouseID = v_house.HouseID;
                txtJuan.Text = v_house.HouseBox.ToString();
                LoadCabinet();
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void tabCont_Enter(object sender, EventArgs e)
        {
            if (cboHouse.Items.Count > 0)
                cboHouse.SelectedIndex = 0;
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            if (this.lsvHouse.SelectedItems.Count <= 0) {
                MessageBox.Show("请选择要删除的库房！");
                return;
            }
            if (Clsxtpz.selectHouseID == 0) {
                return;
            }
            if (T_Sysset.isHouseData()) {
                MessageBox.Show("此库房存在数据无法删除!");
                return;
            }
            V_HouseName.HouseSetid = Clsxtpz.selectHouseID;
            T_Sysset.DeleteHouse();
            LoadHouse();
        }

        private void butSelectDir_Click(object sender, EventArgs e)
        {
            if (fbdSelect.ShowDialog() == DialogResult.OK)
                this.txtFwqYsPath.Text = this.fbdSelect.SelectedPath;
            else
                this.txtFwqYsPath.Text = "";
        }
        private void butfwq_Click(object sender, EventArgs e)
        {
            if (fbdSelect.ShowDialog() == DialogResult.OK)
                this.txtFwqPath.Text = this.fbdSelect.SelectedPath;
            else
                this.txtFwqPath.Text = "";
        }
        #region 转跳
        private void txt_fwq_ftp_ip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt_fwq_ftp_duanko_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt_fwq_ftpuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt_fwq_ftppwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt_fwq_ftpscan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt_fwq_ftppaixu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt_fwq_ftpsave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void text_xtpz_fuwuqi_update_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtFwqTmpScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtFwqTmpIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtFwqTmpSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSave.Focus();
        }
        #endregion

        
    }
}
