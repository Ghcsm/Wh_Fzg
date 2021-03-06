﻿using DAL;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using CsmCheck;
using System.Collections.Generic;

namespace Bgkj
{
    public partial class FrmSet : Form
    {
        public FrmSet()
        {
            InitializeComponent();
        }


        #region set

        private void butModuleCle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void butCle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (!chkUpdateTime.Checked) {
                if (txtId.Text.Trim().Length <= 0 ||
                    txtSn.Text.Trim().Length <= 0 ||
                    txtTime.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入相关信息!");
                    this.txtId.Focus();
                    return;
                }

                if (txtSn.Text.Trim().Length > 38 ||
                    txtSn.Text.Trim().Length < 30) {
                    MessageBox.Show("SN字段不正确!");
                    txtSn.Focus();
                    return;
                }
            }
            if (txtTime.Text.Trim().Length > 65 ||
                txtTime.Text.Trim().Length < 45) {
                MessageBox.Show("时间字段不正确!");
                this.txtTime.Focus();
                return;
            }
            T_addModule.T_id = txtId.Text.Trim();
            T_addModule.T_sn = txtSn.Text.Trim();
            T_addModule.T_time = txtTime.Text.Trim();
            T_addModule.T_timecs = DateTime.Now.ToString();
            T_Sysset.SaveModuleSofte(chkUpdateTime.Checked);
            MessageBox.Show("保存成功!");

        }

        private void butModuleSet_Click(object sender, EventArgs e)
        {
            if (T_ConFigure.Bgsoft) {
                MessageBox.Show("警告：软件未授权无法进行操作！");
                return;
            }
            if (txtModuleChName.Text.Trim().Length <= 0 ||
                txtModuleChName.Text.Trim().Length <= 0 ||
                txtModuleFileName.Text.Trim().Length <= 0 ||
                comModuleFz.Text.Trim().Length <= 0 ||
                comModuleImg.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入相关信息!");
                txtModuleChName.Focus();
                return;
            }
            T_addModule.T_moduleChName = this.txtModuleChName.Text.Trim();
            T_addModule.T_moduleName = this.txtModuleName.Text.Trim();
            T_addModule.T_moduleFileName = this.txtModuleFileName.Text.Trim();
            T_addModule.T_moduleInt = ClsSetInfopar.lsxh[comModuleFz.SelectedIndex].ToString();
            T_addModule.T_moduleImgIdx = this.comModuleImg.SelectedIndex;
            T_Sysset.SaveModule();
            MessageBox.Show("保存成功!");
            this.txtModuleChName.Text = "";
            this.txtModuleFileName.Text = "";
            this.txtModuleName.Text = "";
            this.comModuleImg.Text = "";
            this.comModuleFz.Text = "";
        }

        private void FrmSet_Load(object sender, EventArgs e)
        {
            GetImgList();
            if (T_User.UserId == 1)
                chkUpdateTime.Enabled = true;
            else
                chkUpdateTime.Enabled = false;
        }

        void GetImgList()
        {
            this.comModuleImg.Items.Clear();
            for (int i = 0; i < ClsSetInfopar.imList.Images.Count; i++) {
                this.comModuleImg.Items.Add(i);
            }
        }

        private void comModuleImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClsSetInfopar.imList.Images.Count <= 0)
                return;
            if (this.comModuleImg.Text.Trim().Length <= 0)
                return;
            int id = Convert.ToInt32(comModuleImg.Text.Trim());
            pict.Image = ClsSetInfopar.imList.Images[id];
        }
        private void txtModuleChName_Leave(object sender, EventArgs e)
        {
            if (txtModuleChName.Text.Trim().Length <= 0)
                return;
            try
            {
                T_addModule.T_moduleChName = txtModuleChName.Text.Trim();
                T_Sysset.SeleModule();
                txtModuleName.Text = T_addModule.T_moduleName;
                txtModuleFileName.Text = T_addModule.T_moduleFileName;
                comModuleFz.SelectedIndex = Convert.ToInt32(T_addModule.T_moduleInt.ToString()) - 1;
                comModuleImg.Text = T_addModule.T_moduleImgIdx.ToString();
            }
            catch 
            {}
        }
        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtSn.Text.Trim().Length <= 0)
                return;
            T_addModule.T_id = txtId.Text.Trim();
            T_Sysset.SeleModuleSofte();
            txtSn.Text = T_addModule.T_sn;
            txtTime.Text = T_addModule.T_time;
        }
        #endregion

        #region module set
        private void Getmodule()
        {
            DataTable dt = T_Sysset.IsGetModule(0);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow[] drt = dt.Select("ModuleSys is null");
            chkMouduleCol.Items.Clear();
            chkMouduleColSet.Items.Clear();
            T_addModule.ModuleColSet.Clear();
            foreach (DataRow dr in drt) {
                string str = dr[1].ToString();
                chkMouduleCol.Items.Add(str);
            }
            DataRow[] drj = dt.Select("ModuleSys='1'");
            foreach (DataRow dr in drj) {
                string str = dr[1].ToString();
                chkMouduleColSet.Items.Add(str);
                T_addModule.ModuleColSet.Add(str);
            }
        }

        private void SetModulesys(bool bl)
        {

            if (bl) {
                if (chkMouduleCol.Items.Count <= 0 || chkMouduleCol.SelectedItems.Count <= 0)
                    return;
                for (int i = 0; i < chkMouduleCol.Items.Count; i++) {
                    if (chkMouduleCol.GetItemChecked(i)) {
                        string str = chkMouduleCol.Items[i].ToString();
                        if (T_addModule.ModuleColSet.IndexOf(str) < 0) {
                            Thread.Sleep(300);
                            T_Sysset.UpdateModuelSys(str, bl);
                            chkMouduleColSet.Items.Add(str);
                            T_addModule.ModuleColSet.Add(str);
                            i--;
                        }
                    }
                }
            }
            else {
                if (chkMouduleColSet.Items.Count <= 0 || chkMouduleColSet.SelectedItems.Count <= 0)
                    return;
                for (int i = 0; i < chkMouduleColSet.Items.Count; i++) {
                    if (chkMouduleColSet.GetItemChecked(i)) {
                        string str = chkMouduleColSet.Items[i].ToString();
                        if (T_addModule.ModuleColSet.IndexOf(str) >= 0) {
                            Thread.Sleep(300);
                            T_Sysset.UpdateModuelSys(str, bl);
                            chkMouduleColSet.Items.Remove(str);
                            T_addModule.ModuleColSet.Remove(str);
                            i--;
                        }

                    }
                }
            }
        }

        private void DelModulesys()
        {
            if (chkMouduleColSet.Items.Count <= 0 || chkMouduleColSet.SelectedItems.Count <= 0)
                return;
            for (int i = 0; i < chkMouduleColSet.Items.Count; i++) {
                if (chkMouduleColSet.GetItemChecked(i)) {
                    string str = chkMouduleColSet.Items[i].ToString();
                    if (T_addModule.ModuleColSet.IndexOf(str) >= 0) {
                        Thread.Sleep(300);
                        T_Sysset.DelModuelSys(str);
                        chkMouduleColSet.Items.Remove(str);
                        T_addModule.ModuleColSet.Remove(str);
                    }

                }
            }
        }
        #endregion

        #region 转跳
        private void txtModuleChName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtModuleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void comModuleFz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void comModuleImg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtSn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
        private void txtModuleFileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }


        private void FrmSet_Shown(object sender, EventArgs e)
        {
            Getmodule();
            Getmenuset();
        }

        private void butModulesysadd_Click(object sender, EventArgs e)
        {
            SetModulesys(true);
        }

        private void butModulesysdel_Click(object sender, EventArgs e)
        {
            SetModulesys(false);
        }
        private void butModulesysDelStop_Click(object sender, EventArgs e)
        {
            DelModulesys();
        }

        #endregion

        #region meun set 

        private void Getmenuset()
        {
            ClsSetInfopar.lsname.Clear();
            ClsSetInfopar.lsid.Clear();
            comModuleFz.Items.Clear();
            DataTable dt = T_Sysset.IsGetMenuSet();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string strid = dt.Rows[i][0].ToString();
                string strname = dt.Rows[i][1].ToString();
                string strxh = dt.Rows[i][2].ToString();
                ClsSetInfopar.lsid.Add(strid);
                ClsSetInfopar.lsname.Add(strname);
                ClsSetInfopar.lsxh.Add(strxh);
                comModuleFz.Items.Add(strname);
            }
            chkMenuName.DataSource = dt;
            chkMenuName.DisplayMember = "ModuleName";
        }

        private int Getmenuid()
        {
            int id = -1;

            for (int i = 0; i < chkMenuName.Items.Count; i++) {
                if (chkMenuName.GetItemChecked(i)) {
                    id = Convert.ToInt32(ClsSetInfopar.lsid[i]);
                    break;
                }
            }
            return id;
        }

        private void butMenuSave_Click(object sender, EventArgs e)
        {
            string txtname = txtMenuname.Text.Trim();
            string txtxh = txtMenuxh.Text.Trim();
            int id = -1;
            if (txtname.Length <= 0 || txtxh.Length <= 0) {
                MessageBox.Show("请输入菜单名称或顺序号!");
                txtMenuname.Focus();
                return;
            }
            if (chkMenuName.SelectedItems.Count >= 0)
                id = Getmenuid();
            if (ClsSetInfopar.lsname.IndexOf(txtname) >= 0) {
                MessageBox.Show("菜单名称已存在请更改!");
                txtMenuname.Focus();
                return;
            }
            if (ClsSetInfopar.lsxh.IndexOf(txtxh) >= 0 && id < 0) {
                MessageBox.Show("菜单顺序号已存在!");
                txtMenuxh.Focus();
                return;
            }
            try {

                T_Sysset.SaveMenuset(id.ToString(), txtname, txtxh);
            } catch { } finally {
                Getmenuset();
            }
        }

        private void chkMenuName_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkMenuName.Items.Count; i++) {
                chkMenuName.SetItemChecked(i, false);
            }
        }
        private void chkMenuName_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkMenuName.Items.Count; i++) {
                if (chkMenuName.GetItemChecked(i)) {
                    txtMenuname.Text = ClsSetInfopar.lsname[i];
                    txtMenuxh.Text = ClsSetInfopar.lsxh[i];
                }
            }
        }


        #endregion

        private void butMenuDel_Click(object sender, EventArgs e)
        {
            int id = -1;
            string name = txtMenuname.Text.Trim();
            if (chkMenuName.SelectedItems.Count >= 0)
                id = Getmenuid();
            if (id < 0) {
                MessageBox.Show("请选择删除的菜单!");
                return;
            }
            try {
                T_Sysset.DelMenuSet(id, name);
            } catch {
            } finally {
                Getmenuset();
            }

        }
    }
}
