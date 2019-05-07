using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CsmGenSet
{
    public partial class FrmGetSet : Form
    {
        public FrmGetSet()
        {
            InitializeComponent();
        }
        #region print Set
        private void butSqlTableName_Click(object sender, EventArgs e)
        {
            IsTable();
        }
        private void cletxt()
        {
            chklbSql.DataSource = null;
            chklbSql.Items.Clear();
            chklbFieldsXy.Items.Clear();
            chklbFieldsShow.Items.Clear();
            ClsGenSet.PrintFont = "";
            ClsGenSet.lsFont.Clear();
            ClsGenSet.lsxy.Clear();
            ClsGenSet.lscol.Clear();
            ClsGenSet.lsfonttmp.Clear();
        }

        private void IsTable()
        {
            ClsGenSet.Sqltable = txtTable.Text.Trim();
            cletxt();
            if (txtTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入表名称!");
                txtTable.Focus();
                return;
            }
            string table = T_Sysset.isTable(ClsGenSet.Sqltable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存在!");
                txtTable.Focus();
                return;
            }
            DataTable dt = T_Sysset.GetTableName(ClsGenSet.Sqltable);
            if (dt != null && dt.Rows.Count > 0) {
                chklbSql.DataSource = dt;
                chklbSql.DisplayMember = "Name";
            }
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            AddColName();
        }

        private void AddColName()
        {
            if (chklbSql.Items.Count <= 0)
                return;
            if (chklbSql.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chklbSql.Items.Count; i++) {
                if (chklbSql.GetItemChecked(i)) {
                    string strid = chklbSql.GetItemText(chklbSql.Items[i]);
                    if (strid.ToLower() == "id" || strid.ToLower() == "archid" || strid.ToLower() == "entertag")
                        continue;
                    if (tabitemPrintXy.IsSelected) {
                        if (ClsGenSet.lsxy.IndexOf(strid) < 0) {
                            chklbFieldsXy.Items.Add(strid);
                            ClsGenSet.lsxy.Add(strid);
                        }
                    }
                    else {
                        if (ClsGenSet.lscol.IndexOf(strid) < 0) {
                            chklbFieldsShow.Items.Add(strid);
                            ClsGenSet.lscol.Add(strid);
                        }
                    }
                }
            }
        }

        private void butdel_Click(object sender, EventArgs e)
        {
            Deltable();
        }
        private void Deltable()
        {
            if (tabitemPrintXy.IsSelected) {
                delchklbContr(chklbFieldsXy, ClsGenSet.lsxy, 1);
            }
            else if (tabitemPrintColshow.IsSelected)
                delchklbContr(chklbFieldsShow, ClsGenSet.lscol, 0);

        }

        private void DelFont(string a)
        {
            if (lbFontSet.Items.Count <= 0)
                return;
            for (int i = 0; i < lbFontSet.Items.Count; i++) {
                string str = lbFontSet.Items[i].ToString();
                string[] b = str.Split(':');
                if (a == b[0]) {
                    lbFontSet.Items.Remove(str);
                    ClsGenSet.lsFont.Remove(str);
                }
            }
            if (lbFontSet.Items.Count <= 0) {
                ClsGenSet.PrintFont = "";
                labfont.Text = "";
                labFontSize.Text = "";
                labFontx.Text = "";
                labcolor.Text = "";
                ClsGenSet.lsFont.Clear();
                lbFontSet.Items.Clear();
            }
        }

        private void delchklbContr(CheckedListBox chk, List<string> ls, int id)
        {
            if (chk.Items.Count <= 0)
                return;
            if (chk.CheckedItems.Count <= 0)
                return;

            for (int i = 0; i < chk.Items.Count; i++) {
                if (chk.GetItemChecked(i)) {
                    string strid = chk.GetItemText(chk.Items[i]);
                    chk.Items.RemoveAt(i);
                    if (ls.IndexOf(strid) >= 0)
                        ls.Remove(strid);
                    i--;
                    if (id > 0) {
                        if (ClsGenSet.lscol.IndexOf(strid) >= 0) {
                            for (int b = 0; b < chklbFieldsShow.Items.Count; b++) {
                                string c = chklbFieldsShow.Items[b].ToString();
                                if (strid == c) {
                                    chklbFieldsShow.Items.RemoveAt(b);
                                }
                            }
                            ClsGenSet.lscol.Remove(strid);
                        }
                        DelFont(strid);
                    }

                }
            }
        }

        private void butSqlSave_Click(object sender, EventArgs e)
        {
            SavePrint();
        }

        private void SavePrint()
        {
            string xy = "";
            string col = "";
            string fontspec = "";

            if (ClsGenSet.Sqltable.Length <= 0) {
                MessageBox.Show("表名称不能空!");
                return;
            }

            if (ClsGenSet.lsxy.Count <= 0) {
                MessageBox.Show("坐标字段不能为空!");
                return;
            }
            if (ClsGenSet.PrintFont.Length <= 0) {
                MessageBox.Show("字体信息未设置!");
                return;
            }
            try {
                for (int i = 0; i < ClsGenSet.lsxy.Count; i++) {
                    if (i != ClsGenSet.lsxy.Count - 1)
                        xy += ClsGenSet.lsxy[i] + ";";
                    else
                        xy += ClsGenSet.lsxy[i];
                }

                for (int i = 0; i < ClsGenSet.lscol.Count; i++) {
                    if (i != ClsGenSet.lscol.Count - 1)
                        col += ClsGenSet.lscol[i] + ";";
                    else
                        col += ClsGenSet.lscol[i];
                }

                for (int i = 0; i < ClsGenSet.lsFont.Count; i++) {
                    if (i != ClsGenSet.lsFont.Count - 1)
                        fontspec += ClsGenSet.lsFont[i] + ";";
                    else
                        fontspec += ClsGenSet.lsFont[i];
                }

                T_Sysset.UpdateGensetPrint(ClsGenSet.Sqltable, xy, col, ClsGenSet.PrintFont, fontspec);
                MessageBox.Show("设置完成!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                String s = "修改打印信息;数据表:" + ClsGenSet.Sqltable + " ->" + xy + "->" + col + "->" + ClsGenSet.PrintFont + "->" + fontspec;
                Common.Writelog(0, s);
            }
        }

        private void GetGenSetPrint()
        {
            cletxt();
            DataTable dt = T_Sysset.GetGensetPrint();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ClsGenSet.PrintInfo = dt;
            DataRow dr = ClsGenSet.PrintInfo.Rows[0];
            ClsGenSet.Sqltable = dr["printTable"].ToString();
            string strxy = dr["PrintxyCol"].ToString();
            string strcol = dr["PrintColInfo"].ToString();
            string fontall = dr["PrintFontColAll"].ToString();
            string fontspec = dr["PrintFontSpec"].ToString();
            if (ClsGenSet.Sqltable.Length > 0) {
                txtTable.Text = ClsGenSet.Sqltable;
                butSqlTableName_Click(null, null);
            }
            if (strxy.Length > 0) {
                string[] xy = strxy.Split(';');
                if (xy.Length > 0) {
                    for (int i = 0; i < xy.Length; i++) {
                        string str = xy[i];
                        ClsGenSet.lsxy.Add(str);
                        chklbFieldsXy.Items.Add(str);
                    }
                }
            }
            if (strcol.Length > 0) {
                string[] col = strcol.Split(';');
                if (col.Length > 0) {
                    for (int i = 0; i < col.Length; i++) {
                        string str = col[i];
                        ClsGenSet.lscol.Add(str);
                        chklbFieldsShow.Items.Add(str);
                    }
                }
            }
            if (fontall.Length > 0) {
                ClsGenSet.PrintFont = fontall;
                string[] a = fontall.Split(':');
                labfont.Text = a[0];
                labcolor.Text = a[1];
                labFontSize.Text = a[2];
                labFontx.Text = a[3];
            }
            if (fontspec.Length > 0) {
                string[] spec = fontspec.Split(';');
                if (spec.Length > 0) {
                    chkFontSetTs.Checked = true;
                    grFontSet.Enabled = true;
                    for (int i = 0; i < spec.Length; i++) {
                        string str = spec[i];
                        ClsGenSet.lsFont.Add(str);
                        lbFontSet.Items.Add(str);
                    }
                }
            }
           
        }



        private void chkFontSet_Click(object sender, EventArgs e)
        {
            if (ClsGenSet.PrintFont.Length <= 0) {
                MessageBox.Show("请先进行统一设置");
                chkFontSetTs.Checked = false;
                return;
            }
            if (chkFontSetTs.Checked) {
                grFontSet.Enabled = true;
                cbPrintColFont.Items.Clear();
                for (int i = 0; i < ClsGenSet.lsxy.Count; i++) {
                    string str = ClsGenSet.lsxy[i];
                    cbPrintColFont.Items.Add(str);
                }
            }
            else {
                grFontSet.Enabled = false;
                lbFontSet.Items.Clear();
                ClsGenSet.lsFont.Clear();
                ClsGenSet.lsfonttmp.Clear();
            }
        }
        private void butColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                labcolor.Text = colorDialog.Color.Name;
                string[] str = ClsGenSet.PrintFont.Split(':');
                str[1] = colorDialog.Color.Name.ToString();
                str[4] = colorDialog.Color.ToArgb().ToString();
                ClsGenSet.PrintFont = "";
                for (int i = 0; i < str.Length; i++) {
                    if (i < str.Length - 1)
                        ClsGenSet.PrintFont += str[i] + ":";
                    else
                        ClsGenSet.PrintFont += str[i];
                }
            }
        }


        private void butFontAllSet_Click(object sender, EventArgs e)
        {
            if (fontdlg.ShowDialog() == DialogResult.OK) {
                labfont.Text = fontdlg.Font.Name;
                labcolor.Text = fontdlg.Color.Name;
                labFontSize.Text = fontdlg.Font.Size.ToString();
                labFontx.Text = fontdlg.Font.Bold.ToString();
                ClsGenSet.PrintFont = fontdlg.Font.Name + ":" + fontdlg.Color.Name.ToString() +
                                      ":" + fontdlg.Font.Size + ":" + fontdlg.Font.Bold+":"+fontdlg.Color.ToArgb();
            }
            else {
                ClsGenSet.PrintFont = "";
                labfont.Text = "";
                labcolor.Text = "";
                labFontSize.Text = "";
                labFontx.Text = "";
            }
        }
        private void tabitemPrintFontSet_Click(object sender, EventArgs e)
        {
            if (tabitemPrintFontSet.IsSelected) {
                if (chkFontSetTs.Checked) {
                    cbPrintColFont.Items.Clear();
                    for (int i = 0; i < ClsGenSet.lsxy.Count; i++) {
                        string str = ClsGenSet.lsxy[i];
                        cbPrintColFont.Items.Add(str);
                    }
                }
            }
        }
        private void butFontAdd_Click(object sender, EventArgs e)
        {
            if (cbPrintColFont.Text.Trim().Length <= 0) {
                MessageBox.Show("请先选择字段!");
                cbPrintColFont.Focus();
                return;
            }
            if (fontdlg.ShowDialog() == DialogResult.OK) {
                string str = cbPrintColFont.Text.Trim();
                if (ClsGenSet.lsfonttmp.IndexOf(str) >= 0) {
                    MessageBox.Show("已存在无法再时行设置!");
                    return;
                }
                ClsGenSet.lsfonttmp.Add(str);
                str += ":" + fontdlg.Font.Name;
                str += ":" + fontdlg.Color.Name;
                str += ":" + fontdlg.Font.Size;
                str += ":" + fontdlg.Font.Bold;
                str += ":" + chkPrintColname.Checked;
                str += ":" + chkPrintLine.Checked;
                lbFontSet.Items.Add(str);
                ClsGenSet.lsFont.Add(str);
            }
        }

        private void butFontdel_Click(object sender, EventArgs e)
        {
            if (lbFontSet.Items.Count <= 0)
                return;
            if (lbFontSet.SelectedItems.Count <= 0)
                return;
            string str = lbFontSet.SelectedItems[0].ToString();
            lbFontSet.Items.Remove(str);
            ClsGenSet.lsFont.Remove(str);
        }

        private void chkPrintColname_Click(object sender, EventArgs e)
        {
            if (chkPrintColname.Checked)
                labcolname.Text = "字段名称：+字段信息";
            else labcolname.Text = "字段信息";
            ClsGenSet.PrintcolName = chkPrintColname.Checked;
        }

        private void chkPrintLine_Click(object sender, EventArgs e)
        {
            ClsGenSet.PrintLine = chkPrintLine.Checked;
        }


        #endregion

        #region ContenSet

        private void chkContenTableCol_Click(object sender, EventArgs e)
        {
            if (chkPrintContenTableCol.Items.Count <= 0)
                return;
            for (int i = 0; i < chkPrintContenTableCol.Items.Count; i++) {
                chkPrintContenTableCol.SetItemChecked(i, false);
            }
            ClsPrintConten.Coltmp = chkPrintContenTableCol.GetItemText(chkPrintContenTableCol.Items[chkPrintContenTableCol.SelectedIndex]);
        }

        private void ContenPrintCls()
        {
            ClsPrintConten.Contencoltmp.Clear();
            ClsPrintConten.ContenXlstmp.Clear();
            ClsPrintConten.ContenPagetmp.Clear();
            ClsPrintConten.ContenallSet.Clear();
        }
        private void butContenPrintTableis_Click(object sender, EventArgs e)
        {
            IsPrintContenTable();
        }

        private void IsPrintContenTable()
        {
            if (txtPrintContenPrintTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtPrintContenPrintTable.Focus();
                return;
            }
            ClsPrintConten.ContenTable = txtPrintContenPrintTable.Text.Trim();
            string table = T_Sysset.isTable(ClsPrintConten.ContenTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存在!");
                txtTable.Focus();
                return;
            }
            lbPrintContenSet.Items.Clear();
            ClsPrintConten.Contencoltmp.Clear();
            ClsPrintConten.ContenXlstmp.Clear();
            ClsPrintConten.ContenPagetmp.Clear();
            ClsPrintConten.ContenallSet.Clear();
        DataTable dt = T_Sysset.GetTableName(ClsPrintConten.ContenTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkPrintContenTableCol.DataSource = dt;
                chkPrintContenTableCol.DisplayMember = "Name";
            }
        }

        private void chkContenSetPage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrintContenSetPage.Checked) {
                grContenPage.Enabled = true;
                chkPrintContenSn.Checked = false;
            }
            else
                grContenPage.Enabled = false;

        }

        private void chkContenSn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrintContenSn.Checked)
                chkPrintContenSetPage.Checked = false;
        }

        private void butContenAdd_Click(object sender, EventArgs e)
        {
            string str = "";
            string zero = "0";
            if (chkPrintContenSn.Checked) {
                if (txtPrintContenXls.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入绑定Xls中的表格名称!");
                    txtPrintContenXls.Focus();
                    return;
                }
                str = "SN0:" + txtPrintContenXls.Text.Trim();
                if (ClsPrintConten.ContenSn.Trim().Length > 0) {
                    MessageBox.Show("只能设置一个序列号!");
                    return;
                }
                ClsPrintConten.ContenSn = str;
                ClsPrintConten.ContenXlstmp.Add(txtPrintContenXls.Text.Trim());
                ClsPrintConten.ContenallSet.Add(str);
                lbPrintContenSet.Items.Add(str);
                return;
            }
            if (!isContentxt())
                return;
            if (chkPrintContenSetPage.Checked) {
                if (rbPrintContenPage1.Checked)
                    zero = "1";
                else zero = "2";
            }
            str = ClsPrintConten.Coltmp + ":" + txtPrintContenXls.Text.Trim() + ":" + chkPrintContendz.Checked + ":" +
                        chkPrintContenSetPage.Checked + ":" + zero;
            if (ClsPrintConten.ContenallSet.IndexOf(str) >= 0) {
                MessageBox.Show("此规则已存在!");
                return;
            }
            ClsPrintConten.ContenallSet.Add(str);
            lbPrintContenSet.Items.Add(str);
        }

        private bool isContentxt()
        {

            if (chkPrintContenTableCol.CheckedItems.Count <= 0) {
                MessageBox.Show("请选择要设置的字段!");
                chkPrintContenTableCol.Focus();
                return false;
            }
            if (txtPrintContenXls.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入绑定Xls表格名称!");
                txtPrintContenXls.Focus();
                return false;
            }
            if (ClsPrintConten.Contencoltmp.IndexOf(ClsPrintConten.Coltmp) >= 0) {
                MessageBox.Show("字段已存在!");
                return false;
            }

            if (ClsPrintConten.ContenXlstmp.IndexOf(txtPrintContenXls.Text.Trim()) > 0) {
                MessageBox.Show("Xls表格名字已存在!");
                return false;
            }
            if (chkPrintContenSetPage.Checked) {
                if (ClsPrintConten.ContenPagetmp.IndexOf("True") >= 0) {
                    MessageBox.Show("只能一个字段为页码!");
                    return false;
                }
            }
            ClsPrintConten.Contencoltmp.Add(ClsPrintConten.Coltmp);
            ClsPrintConten.ContenXlstmp.Add(txtPrintContenXls.Text.Trim());
            ClsPrintConten.ContenPagetmp.Add(chkPrintContenSetPage.Checked.ToString());
            return true;
        }
        private void butContenSetdel_Click(object sender, EventArgs e)
        {
            if (lbPrintContenSet.Items.Count <= 0)
                return;
            if (lbPrintContenSet.SelectedItems.Count <= 0)
                return;
            string str = lbPrintContenSet.SelectedItems[0].ToString();
            if (str.Length > 0) {
                if (str.IndexOf("SN0") >= 0) {
                    ClsPrintConten.ContenSn = "";
                    ClsPrintConten.ContenallSet.Remove(str);
                    lbPrintContenSet.Items.Remove(str);
                    ClsPrintConten.ContenallSet.Remove(str);
                    return;
                }
                string[] a = str.Split(':');
                if (ClsPrintConten.Contencoltmp.IndexOf(a[0]) >= 0)
                    ClsPrintConten.Contencoltmp.Remove(a[0]);

                if (ClsPrintConten.ContenXlstmp.IndexOf(a[1]) >= 0)
                    ClsPrintConten.ContenXlstmp.Remove(a[1]);
                if (ClsPrintConten.ContenPagetmp.IndexOf(a[3]) >= 0)
                    ClsPrintConten.ContenPagetmp.Remove(a[3]);
                lbPrintContenSet.Items.Remove(str);
                ClsPrintConten.ContenallSet.Remove(str);
            }
        }

        private void butContenSave_Click(object sender, EventArgs e)
        {
            string str = "";
            if (ClsPrintConten.ContenTable.Length <= 0 || ClsPrintConten.ContenallSet.Count <= 0)
                return;
            try {
                for (int i = 0; i < ClsPrintConten.ContenallSet.Count; i++) {
                    if (i != ClsPrintConten.ContenallSet.Count - 1)
                        str += ClsPrintConten.ContenallSet[i] + ";";
                    else
                        str += ClsPrintConten.ContenallSet[i];
                }

                T_Sysset.UpdateGensetPrintConten(ClsPrintConten.ContenTable, str);
                MessageBox.Show("保存成功!");
            } catch (Exception exception) {
                MessageBox.Show(exception.ToString());
            } finally {
                GetnPrintConten();
                String s = "修改目录数据表:" + ClsPrintConten.ContenTable + " 及相关Xls信息:" + str;
                Common.Writelog(0, s);
            }
        }


        private void lbFontSet_Click(object sender, EventArgs e)
        {
            try {
                string str = lbFontSet.SelectedItems[0].ToString();
                string[] a = str.Split(':');
                cbPrintColFont.Text = a[0];
                chkPrintColname.Checked = Convert.ToBoolean(a[5]);
                chkPrintLine.Checked = Convert.ToBoolean(a[6]);
            } catch { }

        }

        private void cleConten()
        {
            lbPrintContenSet.Items.Clear();
            ClsPrintConten.ContenTable = "";
            ClsPrintConten.ContenXlstmp.Clear();
            ClsPrintConten.ContenPagetmp.Clear();
            ClsPrintConten.ContenallSet.Clear();
            ClsPrintConten.Contencoltmp.Clear();
            ClsPrintConten.ContenSn = "";
            ClsPrintConten.Coltmp = "";
        }
        private void GetnPrintConten()
        {
            cleConten();
            DataTable dt = T_Sysset.GetGensetPrint();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            ClsPrintConten.ContenTable = dr["PrintContenTable"].ToString();
            string str = dr["PrintContenInfo"].ToString();
            txtPrintContenPrintTable.Text = ClsPrintConten.ContenTable;
            butContenPrintTableis_Click(null, null);
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    string[] c = b.Split(':');
                    if (c.Length > 0) {
                        if (c[0].IndexOf("SN0") >= 0) {
                            ClsPrintConten.ContenSn = b;
                        }
                        else {
                            ClsPrintConten.Contencoltmp.Add(c[0]);
                            ClsPrintConten.ContenXlstmp.Add(c[1]);
                            ClsPrintConten.ContenPagetmp.Add(c[3]);
                        }
                    }
                    lbPrintContenSet.Items.Add(b);
                    ClsPrintConten.ContenallSet.Add(b);
                }
            }
        }

        #endregion

        #region ImportInfo
        private void butImportzd_Click(object sender, EventArgs e)
        {
            IsImPortTable();
        }

        private void Importcle()
        {
            ClsImportTable.ImportInfo.Clear();
            ClsImportTable.ImportTableLs.Clear();
        }
        private void IsImPortTable()
        {
            if (txtImportTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtImportTable.Focus();
                return;
            }
            ClsImportTable.ImportTable = txtImportTable.Text.Trim();
            string table = T_Sysset.isTable(ClsImportTable.ImportTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存！");
                txtImportTable.Focus();
                return;
            }
            chkImportAddzd.Items.Clear();
            ClsImportTable.ImportInfotmp.Clear();
            DataTable dt = T_Sysset.GetTableName(ClsImportTable.ImportTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkImportTable.DataSource = dt;
                chkImportTable.DisplayMember = "Name";
            }
        }

        private void AddImportInfo()
        {
            if (chkImportTable.Items.Count <= 0)
                return;
            if (chkImportTable.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkImportTable.Items.Count; i++) {
                if (chkImportTable.GetItemChecked(i)) {
                    string strid = chkImportTable.GetItemText(chkImportTable.Items[i]);
                    if (strid != "Archid") {
                        if (ClsImportTable.ImportInfotmp.IndexOf(strid) < 0) {
                            chkImportAddzd.Items.Add(strid);
                            //ClsImportTable.ImportInfo.Add(strid);
                            ClsImportTable.ImportInfotmp.Add(strid);
                        }
                    }
                }
            }
        }

        private void DelImportInfo()
        {
            if (chkImportAddzd.Items.Count <= 0)
                return;
            if (chkImportAddzd.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkImportAddzd.Items.Count; i++) {
                if (chkImportAddzd.GetItemChecked(i)) {
                    string str = chkImportAddzd.GetItemText(chkImportAddzd.Items[i]);
                    chkImportAddzd.Items.RemoveAt(i);
                    if (ClsImportTable.ImportInfotmp.IndexOf(str) >= 0)
                        ClsImportTable.ImportInfo.Remove(str);
                    ClsImportTable.ImportInfotmp.Remove(str);
                    i--;
                }
            }
        }

        private void SaveImport()
        {
            string str = "";
            if (ClsImportTable.ImportTable.Length <= 0) {
                MessageBox.Show("请设置表名称!");
                return;
            }
            if (chkImportAddzd.Items.Count <= 0) {
                MessageBox.Show("请先添加需导入的字段!");
                return;
            }
            try {
                for (int i = 0; i < ClsImportTable.ImportInfotmp.Count; i++) {
                    if (i != ClsImportTable.ImportInfotmp.Count - 1)
                        str += ClsImportTable.ImportInfotmp[i] + ";";
                    else
                        str += ClsImportTable.ImportInfotmp[i];
                }

                if (ClsImportTable.ImportTableLs.IndexOf(ClsImportTable.ImportTable) < 0)
                    T_Sysset.SaveGensetImport(ClsImportTable.ImportTable, str);
                else
                    T_Sysset.UpdateGensetImport(ClsImportTable.ImportTable, str);
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetImportSet();
                string s = "修改导入数据表:" + ClsImportTable.ImportTable + "->" + str;
                Common.Writelog(0, s);
            }
        }

        private void CleImport()
        {
            txtImportTable.Text = "";
            if (chkImportTable.Items.Count > 0)
                chkImportTable.DataSource = null;
            if (chkImportAddzd.Items.Count > 0)
                chkImportAddzd.Items.Clear();
            ClsImportTable.ImportTable = "";
            ClsImportTable.ImportTableLs.Clear();
            ClsImportTable.ImportInfo.Clear();
            combImportTable.Items.Clear();
        }
        private void GetImportSet()
        {
            CleImport();
            if (ClsGenSet.PrintInfo != null && ClsGenSet.PrintInfo.Rows.Count > 0) {
                DataTable dt = T_Sysset.GetImportDt();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                foreach (DataRow dr in dt.Rows) {
                    string t = dr["ImportTable"].ToString();
                    if (t.Trim().Length <= 0)
                        return;
                    combImportTable.Items.Add(t);
                    ClsImportTable.ImportTableLs.Add(t);
                }
                DataRow dr1 = dt.Rows[0];
                ClsImportTable.ImportTable = dr1["ImportTable"].ToString();
                string str = dr1["ImportInfoZd"].ToString();
                txtImportTable.Text = ClsImportTable.ImportTable;
                if (str.Length > 0) {
                    string[] a = str.Split(';');
                    for (int i = 0; i < a.Length; i++) {
                        string b = a[i];
                        chkImportAddzd.Items.Add(b);
                        ClsImportTable.ImportInfo.Add(b);
                    }
                }

            }
        }

        private void GetImportTabble()
        {
            string str = combImportTable.Text.Trim();
            if (str.Length <= 0)
                return;
            DataTable dt = T_Sysset.GetImportTable(str);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            chkImportAddzd.Items.Clear();
            ClsImportTable.ImportInfotmp.Clear();
            foreach (DataRow dr in dt.Rows) {
                string s = dr["ImportInfoZd"].ToString();
                if (s.Trim().Length > 0) {
                    string[] a = s.Split(';');
                    for (int i = 0; i < a.Length; i++) {
                        string b = a[i];
                        chkImportAddzd.Items.Add(b);
                        ClsImportTable.ImportInfotmp.Add(b);
                    }
                }
            }


        }

        private void DelImportTable()
        {
            string str = combImportTable.Text.Trim();
            try {
                if (str.Length <= 0)
                    return;
                if (ClsImportTable.ImportTableLs.IndexOf(str) >= 0)
                    ClsImportTable.ImportTableLs.Remove(str);
                T_Sysset.DelImportTable(str);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetImportSet();
                string s = "删除导入数据表:" + str;
                Common.Writelog(0, s);
            }
        }

        private void butImPortAdd_Click(object sender, EventArgs e)
        {
            AddImportInfo();
        }

        private void butImportdel_Click(object sender, EventArgs e)
        {
            DelImportInfo();
        }

        private void butImportSave_Click(object sender, EventArgs e)
        {
            SaveImport();
        }

        private void butImportDelTable_Click(object sender, EventArgs e)
        {
            DelImportTable();
        }

        private void combImportTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetImportTabble();
        }


        #endregion

        #region Infoadd

        private void IsInfoTable()
        {
            if (txtInfoAdd.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtInfoAdd.Focus();
                return;
            }
            ClsInfoAdd.InfoTable = txtInfoAdd.Text.Trim();
            string table = T_Sysset.isTable(ClsInfoAdd.InfoTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存在!");
                txtInfoAdd.Focus();
                return;
            }
            chkInfoZd.Items.Clear();
            ClsInfoAdd.InfoInfoZdtmp.Clear();
            DataTable dt = T_Sysset.GetTableName(ClsInfoAdd.InfoTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkInfoTable.DataSource = dt;
                chkInfoTable.DisplayMember = "Name";
            }
        }

        private void AddInfozd()
        {
            if (chkInfoTable.Items.Count <= 0)
                return;
            if (chkInfoTable.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkInfoTable.Items.Count; i++) {
                if (chkInfoTable.GetItemChecked(i)) {
                    string strid = chkInfoTable.GetItemText(chkInfoTable.Items[i]);
                    if (strid.ToLower() == "archid" || strid.ToLower() == "id" || strid.ToLower() == "entertag" ||strid.ToLower()=="borrtag")
                        continue;
                    if (ClsInfoAdd.InfoInfoZdtmp.IndexOf(strid) < 0) {
                        chkInfoZd.Items.Add(strid);
                        ClsInfoAdd.InfoInfoZdtmp.Add(strid);
                    }
                }
            }
        }

        private void DelInfozd()
        {
            if (chkInfoZd.Items.Count <= 0)
                return;
            if (chkInfoZd.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkInfoZd.Items.Count; i++) {
                if (chkInfoZd.GetItemChecked(i)) {
                    string str = chkInfoZd.GetItemText(chkInfoZd.Items[i]);
                    chkInfoZd.Items.RemoveAt(i);
                    if (ClsInfoAdd.InfoInfoZd.IndexOf(str) >= 0)
                        ClsInfoAdd.InfoInfoZd.Remove(str);
                    i--;
                }
            }
        }

        private void CleInfo()
        {
            txtInfoAdd.Text = "";
            if (chkInfoTable.Items.Count > 0)
                chkInfoTable.DataSource = null;
            if (chkInfoZd.Items.Count > 0)
                chkInfoZd.Items.Clear();
            ClsInfoAdd.InfoTable = "";
            ClsInfoAdd.InfoInfoZd.Clear();
            ClsInfoAdd.InfoTableLs.Clear();
            ClsInfoAdd.InfoColNum.Clear();
            ClsInfoAdd.InfoTableName.Clear();
            ClsInfoAdd.InfoLabWidth.Clear();
            combInfoTable.Items.Clear();
            combInfoColNum.Items.Clear();
            combInfoTableName.Items.Clear();
            combInfoLabWith.Items.Clear();
        }

        private void GetInfoSet()
        {
            CleInfo();
            if (ClsGenSet.PrintInfo != null && ClsGenSet.PrintInfo.Rows.Count > 0) {
                DataTable dt = T_Sysset.GetInfoTable();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                foreach (DataRow dr in dt.Rows) {
                    string t = dr["InfoTable"].ToString();
                    string zd = dr["InfoAddzd"].ToString();
                    string name = dr["InfoName"].ToString();
                    string num = dr["InfoNum"].ToString();
                    string width = dr["InfoLabWidth"].ToString();
                    string txtwidth = dr["InfoTxtWidth"].ToString();
                    if (t.Trim().Length <= 0)
                        return;
                    combInfoTable.Items.Add(t);
                    ClsInfoAdd.InfoTableLs.Add(t);
                    ClsInfoAdd.InfoInfoZd.Add(zd);
                    combInfoTableName.Items.Add(name);
                    ClsInfoAdd.InfoTableName.Add(name);
                    combInfoColNum.Items.Add(num);
                    ClsInfoAdd.InfoColNum.Add(num);
                    combInfoLabWith.Items.Add(width);
                    ClsInfoAdd.InfoLabWidth.Add(width);
                    combInfotxtWith.Items.Add(txtwidth);
                    ClsInfoAdd.InfotxtWidth.Add(txtwidth);
                }
                DataRow dr1 = dt.Rows[0];
                ClsInfoAdd.InfoTable = dr1["InfoTable"].ToString();
                string str = dr1["InfoAddzd"].ToString();
                txtInfoAdd.Text = ClsInfoAdd.InfoTable;
                if (str.Length > 0) {
                    string[] a = str.Split(';');
                    for (int i = 0; i < a.Length; i++) {
                        string b = a[i];
                        chkInfoZd.Items.Add(b);
                    }
                }
            }
        }

        private void SaveInfoAdd()
        {
            string str = "";
            if (ClsInfoAdd.InfoTable.Length <= 0) {
                MessageBox.Show("请设置表名称!");
                return;
            }
            if (chkInfoZd.Items.Count <= 0) {
                MessageBox.Show("请先添加需导入的字段!");
                return;
            }
            if (combInfoTableName.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入前台标题名称!");
                combInfoTableName.Focus();
                return;
            }

            if (combInfoColNum.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入前台字段显示列数!");
                combInfoColNum.Focus();
                return;
            }
            if (combInfoLabWith.Text.Trim().Length <= 0) {
                MessageBox.Show("请设置字段宽度!");
                combInfoLabWith.Focus();
                return;
            }
            if (combInfotxtWith.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入信息框宽度!");
                combInfotxtWith.Focus();
                return;
            }

            if (combInfoTable.Text.Trim().Length <= 0) {
                if (ClsInfoAdd.InfoTableName.IndexOf(combInfoTableName.Text.Trim()) >= 0) {
                    MessageBox.Show("此名字已存在请更换!");
                    return;
                }
            }
            try {
                for (int i = 0; i < ClsInfoAdd.InfoInfoZdtmp.Count; i++) {
                    if (i != ClsInfoAdd.InfoInfoZdtmp.Count - 1)
                        str += ClsInfoAdd.InfoInfoZdtmp[i] + ";";
                    else
                        str += ClsInfoAdd.InfoInfoZdtmp[i];
                }
                if (ClsInfoAdd.InfoTableLs.IndexOf(ClsInfoAdd.InfoTable) < 0)
                    T_Sysset.SaveGensetInfo(ClsInfoAdd.InfoTable, str, combInfoTableName.Text.Trim(),
                        combInfoColNum.Text.Trim(), combInfoLabWith.Text.Trim(), combInfotxtWith.Text.Trim());
                else
                    T_Sysset.UpdateGensetInfo(ClsInfoAdd.InfoTable, str, combInfoTableName.Text.Trim(),
                        combInfoColNum.Text.Trim(), combInfoLabWith.Text.Trim(), combInfotxtWith.Text.Trim());
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetInfoSet();
                string s = "修改信息补录数据表:" + ClsImportTable.ImportTable + "->" + str;
                Common.Writelog(0, s);
            }
        }

        private void DelInfoTable()
        {
            string str = combInfoTable.Text.Trim();
            if (str.Length <= 0)
                return;
            try {
                if (ClsInfoAdd.InfoTableLs.IndexOf(str) >= 0)
                    ClsInfoAdd.InfoTableLs.Remove(str);
                T_Sysset.DelInfoTable(str);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetInfoSet();
                string s = "删除信息补录数据表:" + str;
                Common.Writelog(0, s);
            }
        }

        private void SelectInfoadd(int id)
        {
            combInfoTableName.SelectedIndex = id;
            combInfoColNum.SelectedIndex = id;
            combInfoLabWith.SelectedIndex = id;
            combInfotxtWith.SelectedIndex = id;
            string s = ClsInfoAdd.InfoInfoZd[id].ToString();
            if (s.IndexOf(';') >= 0) {
                chkInfoZd.Items.Clear();
                ClsInfoAdd.InfoInfoZdtmp.Clear();
                string[] c = s.Split(';');
                for (int i = 0; i < c.Length; i++) {
                    chkInfoZd.Items.Add(c[i].ToString());
                    ClsInfoAdd.InfoInfoZdtmp.Add(c[i].ToString());
                }
            }
        }
        private void butInfoAddIs_Click(object sender, EventArgs e)
        {
            IsInfoTable();
        }

        private void butInfoadd_Click(object sender, EventArgs e)
        {
            AddInfozd();
        }

        private void butInfoDel_Click(object sender, EventArgs e)
        {
            DelInfozd();
        }

        private void butInfoSave_Click(object sender, EventArgs e)
        {
            SaveInfoAdd();
        }
        private void butInfoTableDel_Click(object sender, EventArgs e)
        {
            DelInfoTable();
        }
        private void combInfoTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                int id = combInfoTable.SelectedIndex;
                if (id < 0)
                    return;
                SelectInfoadd(id);
            } catch { }
        }


        private void combInfoColNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }

        #endregion

        #region QuerInfo

        private void IsQuerTable()
        {
            if (txtQuerTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtQuerTable.Focus();
                return;
            }
            ClsQuerInfo.QuerTable = txtQuerTable.Text.Trim();
            string table = T_Sysset.isTable(ClsQuerInfo.QuerTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存！");
                txtQuerTable.Focus();
                return;
            }
            chkQuerZd.Items.Clear();
            ClsQuerInfo.QuerInfoZd.Clear();
            DataTable dt = T_Sysset.GetTableName(ClsQuerInfo.QuerTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkQuerTable.DataSource = dt;
                chkQuerTable.DisplayMember = "Name";
            }
        }


        private void AddQuerzd()
        {
            if (chkQuerTable.Items.Count <= 0)
                return;
            if (chkQuerTable.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkQuerTable.Items.Count; i++) {
                if (chkQuerTable.GetItemChecked(i)) {
                    string strid = chkQuerTable.GetItemText(chkQuerTable.Items[i]);
                    if (strid.ToLower() == "id" || strid.ToLower() == "archid" || strid.ToLower() == "entertag")
                        continue;
                    if (ClsQuerInfo.QuerInfoZd.IndexOf(strid) < 0) {
                        chkQuerZd.Items.Add(strid);
                        ClsQuerInfo.QuerInfoZd.Add(strid);
                    }
                }
            }
        }

        private void DelQuerInfo()
        {
            if (chkQuerZd.Items.Count <= 0)
                return;
            if (chkQuerZd.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkQuerZd.Items.Count; i++) {
                if (chkQuerZd.GetItemChecked(i)) {
                    string str = chkQuerZd.GetItemText(chkQuerZd.Items[i]);
                    chkQuerZd.Items.RemoveAt(i);
                    if (ClsQuerInfo.QuerInfoZd.IndexOf(str) >= 0)
                        ClsQuerInfo.QuerInfoZd.Remove(str);
                    i--;
                }
            }
        }


        private void SaveQuerInfo()
        {
            string str = "";
            if (ClsQuerInfo.QuerTable.Length <= 0) {
                MessageBox.Show("请设置表名称!");
                return;
            }
            if (chkQuerZd.Items.Count <= 0) {
                MessageBox.Show("请先添加需导入的字段!");
                return;
            }
            try {
                for (int i = 0; i < ClsQuerInfo.QuerInfoZd.Count; i++) {
                    if (i != ClsQuerInfo.QuerInfoZd.Count - 1)
                        str += ClsQuerInfo.QuerInfoZd[i] + ";";
                    else
                        str += ClsQuerInfo.QuerInfoZd[i];
                }

                int enter = 1;
                if (rabQuerInfoTwo.Checked)
                    enter = 2;
                else if (rabQuerInfoAll.Checked)
                    enter = 3;
                T_Sysset.UpdateQuerInfo(ClsQuerInfo.QuerTable, str, enter);
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetnQuerInfo();
                string s = "修改导入数据表:" + ClsQuerInfo.QuerTable + "->" + str;
                Common.Writelog(0, s);
            }
        }

        private void Txtcls()
        {
            ClsQuerInfo.QuerTable = "";
            ClsQuerInfo.QuerInfoZd.Clear();
            chkQuerTable.DataSource = null;
            chkQuerZd.Items.Clear();
        }

        private void GetnQuerInfo()
        {
            Txtcls();
            DataTable dt = T_Sysset.GetGensetPrint();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            ClsQuerInfo.QuerTable = dr["QuerTable"].ToString();
            string str = dr["QuerInfoZd"].ToString();
            txtQuerTable.Text = ClsQuerInfo.QuerTable;
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    if (b.Trim().Length > 0) {
                        ClsQuerInfo.QuerInfoZd.Add(b);
                        chkQuerZd.Items.Add(b);
                    }
                }
            }


        }

        private void butQuerTable_Click(object sender, EventArgs e)
        {
            IsQuerTable();
        }

        private void butQuerAdd_Click(object sender, EventArgs e)
        {
            AddQuerzd();
        }
        private void butQuerDel_Click(object sender, EventArgs e)
        {
            DelQuerInfo();
        }

        private void butQuerSave_Click(object sender, EventArgs e)
        {
            SaveQuerInfo();
        }

        #endregion

        #region datasplit

        private void rabDataSplit_File_zd_CheckedChanged(object sender, EventArgs e)
        {
            if (rabDataSplit_File_zd.Checked) {
                txtDataSplit_File_cd.Enabled = false;
                txtDataSplit_File_qian.Enabled = false;
                txtDataSplit_File_hou.Enabled = false;
                Clechk();
                ClsDataSplit.DataSplitFilesn = 3;
                return;
            }
            txtDataSplit_File_cd.Enabled = true;
            txtDataSplit_File_qian.Enabled = true;
            txtDataSplit_File_hou.Enabled = true;
        }

        private void chkDataSplit_dir_ml_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDataSplit_dir_ml.Checked) {
                combDataSplit_dir_ml.Enabled = true;
                combDataSplit_dir_pages.Enabled = true;
                return;
            }
            combDataSplit_dir_ml.Enabled = false;
            combDataSplit_dir_pages.Enabled = false;
        }

        private void IsDataTable()
        {
            if (txtDataSplitTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtDataSplitTable.Focus();
                return;
            }
            ClsDataSplit.DataSplitTable = txtDataSplitTable.Text.Trim();
            string table = T_Sysset.isTable(ClsDataSplit.DataSplitTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存！");
                txtDataSplitTable.Focus();
                return;
            }
            combDataSplit_dir_pages.Items.Clear();
            combDataSplit_dir_ml.Items.Clear();
            ClsDataSplit.DataSplitExportColtmp.Clear();
            DataTable dt = T_Sysset.GetTableName(ClsDataSplit.DataSplitTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkDataSplit.DataSource = dt;
                chkDataSplit.DisplayMember = "Name";
                foreach (DataRow dr in dt.Rows) {
                    string str = dr[0].ToString();
                    combDataSplit_dir_ml.Items.Add(str);
                    combDataSplit_dir_pages.Items.Add(str);
                }
            }
        }

        private string Getzd()
        {
            string str = "";
            string str1 = "";
            if (chkDataSplit.Items.Count <= 0 && chkDataSplit.SelectedItems.Count <= 0)
                return str;
            int id = 0;
            for (int i = 0; i < chkDataSplit.Items.Count; i++) {
                if (chkDataSplit.GetItemChecked(i)) {
                    id += 1;
                    string s = chkDataSplit.GetItemText(chkDataSplit.Items[i]);
                    if (id < chkDataSplit.SelectedItems.Count - 1)
                        str += s + "\\";
                    else str += s;
                }
            }
            str1 = str.Replace('\\', ';');
            return str;
        }

        private string Getfilezd()
        {
            string str = "";
            if (chkDataSplit.Items.Count <= 0 && chkDataSplit.SelectedItems.Count <= 0)
                return str;
            for (int i = 0; i < chkDataSplit.Items.Count; i++) {
                if (chkDataSplit.GetItemChecked(i)) {
                    string s = chkDataSplit.GetItemText(chkDataSplit.Items[i]);
                    str += s;
                }
            }
            return str;
        }

        private void Clechk()
        {
            if (chkDataSplit.Items.Count <= 0)
                return;
            for (int i = 0; i < chkDataSplit.Items.Count; i++) {
                chkDataSplit.SetItemChecked(i, false);
            }
        }
        private string GetFileGz()
        {
            string str = "1";
            int cd = 0;
            if (rabDataSplit_File_dirname.Checked || rabDataSplit_File_anjuan.Checked) {

                if (txtDataSplit_File_cd.Text.Trim().Length > 0) {
                    cd = txtDataSplit_File_qian.TextLength + txtDataSplit_File_hou.TextLength + str.Length;
                    if (cd > Convert.ToInt32(txtDataSplit_File_cd.Text.Trim())) {
                        MessageBox.Show("文件名长度设置不正确!");
                        txtDataSplit_File_cd.Focus();
                        return "";
                    }
                    else {
                        if (chkDataSplit_File_zero.Checked) {
                            str = str.PadLeft((Convert.ToInt32(txtDataSplit_File_cd.Text.Trim()) - (str.Length - 1) - txtDataSplit_File_qian.Text.Trim().Length - txtDataSplit_File_hou.Text.Trim().Length), '0');
                            str = txtDataSplit_File_qian.Text.Trim() + str + txtDataSplit_File_hou.Text.Trim();
                        }
                        else
                            str = txtDataSplit_File_qian.Text.Trim() + str + txtDataSplit_File_hou.Text.Trim();
                    }
                }
                else
                    str = txtDataSplit_File_qian.Text.Trim() + str + txtDataSplit_File_hou.Text.Trim();
            }
            else {
                str = Getfilezd();
            }
            return str;
        }

        private void Getgzzd()
        {
            string str = "";
            if (rabDataSplitdir.Checked) {
                ClsDataSplit.DataSplitDirMl = "";
                ClsDataSplit.DataSplitDirCol = "";
                if (!chkDataSplit_dir_zd.Checked && !chkDataSplit_dir_ml.Checked) {
                    MessageBox.Show("请选择文件夹要生成的规则选择！");
                    labDataSplit_dir_zd.Text = string.Format("字段示例：{0}", "");
                    return;
                }
                if (chkDataSplit_dir_zd.Checked) {
                    str = Getzd();
                    ClsDataSplit.DataSplitDirCol = str;
                }
                if (chkDataSplit_dir_ml.Checked) {
                    if (combDataSplit_dir_ml.Text.Trim().Length <= 0 || combDataSplit_dir_pages.Text.Trim().Length <= 0) {
                        MessageBox.Show("请选择目录字段或页码字段!");
                        combDataSplit_dir_ml.Focus();
                        labDataSplit_dir_zd.Text = string.Format("字段示例：{0}", "");
                        return;
                    }
                    else if (combDataSplit_dir_ml.Text.Trim() == combDataSplit_dir_pages.Text.Trim()) {
                        MessageBox.Show("目录和页码字段不能一致!");
                        combDataSplit_dir_ml.Focus();
                        return;
                    }
                    string s = combDataSplit_dir_ml.Text.Trim() + "\\" + combDataSplit_dir_pages.Text.Trim();
                    ClsDataSplit.DataSplitDirMl = s;
                    str += "\\" + s;
                }
                if (str.Length <= 0)
                    labDataSplit_dir_zd.Text = string.Format("字段示例：{0}", "");
                else {
                    labDataSplit_dir_zd.Text = string.Format("字段示例：{0}", str);
                }

            }
            else {
                ClsDataSplit.DataSplitFileName = "";
                if (rabDataSplit_File_zd.Checked) {
                    str = GetFileGz();
                    ClsDataSplit.DataSplitFileTable = txtDataSplitTable.Text.Trim();
                    labDataSplit_Filetable.Text = string.Format("文件名规则Table为： {0}", txtDataSplitTable.Text.Trim());
                    ClsDataSplit.DataSplitfilenamecol = str;
                }
                else {
                    labDataSplit_Filetable.Text = "";
                    ClsDataSplit.DataSplitFileName = txtDataSplit_File_cd.Text.Trim() + ";" + txtDataSplit_File_qian.Text.Trim() + ";" + txtDataSplit_File_hou.Text.Trim();
                }
                if (str.Length <= 0)
                    labDataSplit_Filesl.Text = "";
                else
                    labDataSplit_Filesl.Text = str;
                 
            }
        }

        private void SaveDataSplitinfo()
        {

            try {
                if (ClsDataSplit.DataSplitTable.Trim().Length <= 0) {
                    MessageBox.Show("请查询表是否存在!");
                    return;
                }
                else if (ClsDataSplit.DataSplitDirCol.Length <= 0 && ClsDataSplit.DataSplitDirMl.Length <= 0) {
                    MessageBox.Show("请选择文件夹生成规则选项!");
                    return;
                }
                if (chkDataSplit_dir_zd.Checked && chkDataSplit_dir_ml.Checked)
                    ClsDataSplit.DataSplitDirsn = 3;
                else if (chkDataSplit_dir_zd.Checked)
                    ClsDataSplit.DataSplitDirsn = 1;
                else if (chkDataSplit_dir_ml.Checked)
                    ClsDataSplit.DataSplitDirsn = 2;
                else {
                    MessageBox.Show("请选择文件夹生成规则选项!");
                    return;
                }
                if (ClsDataSplit.DataSplitFileName.Length <= 0 && ClsDataSplit.DataSplitfilenamecol.Trim().Length<=0) {
                    MessageBox.Show("请先生成文件名规则!");
                    return;
                }
                if (chkDataSplit_dir_ml.Checked && chkDataSplit_dir_zd.Checked)
                    ClsDataSplit.DataSplitDirsn = 3;

                T_Sysset.UPdateDataSplitInfo(ClsDataSplit.DataSplitTable, ClsDataSplit.DataSplitDirsn, ClsDataSplit.DataSplitDirCol,
                    ClsDataSplit.DataSplitDirMl, ClsDataSplit.DataSplitFileTable, ClsDataSplit.DataSplitFilesn,
                    ClsDataSplit.DataSplitFileName, chkDataSplit_File_zero.Checked, ClsDataSplit.DataSplitfilenamecol);
                MessageBox.Show("设置完成");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                string s = "修改数据拆分信息:" + ClsDataSplit.DataSplitDirCol + ";" + ClsDataSplit.DataSplitDirMl + ";" + ClsDataSplit.DataSplitFileName;
                Common.Writelog(0, s);
            }
        }

        private void GetdataSplit()
        {
            DataTable dt = T_Sysset.GetDataSplit();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ClsDataSplit.DataSplitTable = dt.Rows[0][1].ToString();
            ClsDataSplit.DataSplitDirsn = Convert.ToInt32(dt.Rows[0][2].ToString());
            ClsDataSplit.DataSplitDirCol = dt.Rows[0][3].ToString();
            ClsDataSplit.DataSplitDirMl = dt.Rows[0][4].ToString();
            ClsDataSplit.DataSplitFileTable = dt.Rows[0][5].ToString();
            ClsDataSplit.DataSplitFilesn = Convert.ToInt32(dt.Rows[0][6].ToString());
            ClsDataSplit.DataSplitFileName = dt.Rows[0][7].ToString();
            ClsDataSplit.DataSplitzero = Convert.ToBoolean(dt.Rows[0][8].ToString());
            txtDataSplitTable.Text = ClsDataSplit.DataSplitTable;

            butDataSplit_Click(null, null);
            if (ClsDataSplit.DataSplitDirsn == 3) {
                chkDataSplit_dir_zd.Checked = true;
                chkDataSplit_dir_ml.Checked = true;
                string[] str = ClsDataSplit.DataSplitDirMl.Split('\\');
                combDataSplit_dir_ml.Text = str[0];
                combDataSplit_dir_pages.Text = str[1];
            }
            else if (ClsDataSplit.DataSplitDirsn == 2) {
                chkDataSplit_dir_ml.Checked = true;
                string[] str = ClsDataSplit.DataSplitDirMl.Split('\\');
                combDataSplit_dir_ml.Text = str[0];
                combDataSplit_dir_pages.Text = str[1];
            }
            else if (ClsDataSplit.DataSplitDirsn == 1)
                chkDataSplit_dir_zd.Checked = true;

            labDataSplit_dir_zd.Text = "字段示例：" + ClsDataSplit.DataSplitDirCol + "\\" + ClsDataSplit.DataSplitDirMl;
            if (ClsDataSplit.DataSplitFilesn == 3) {
                rabDataSplit_File_zd.Checked = true;
                labDataSplit_Filesl.Text = ClsDataSplit.DataSplitFileName;
                labDataSplit_Filetable.Text = "文件名规则Table为：" + ClsDataSplit.DataSplitFileTable;
            }
            else {
                if (ClsDataSplit.DataSplitFilesn == 1)
                    rabDataSplit_File_dirname.Checked = true;
                else
                    rabDataSplit_File_anjuan.Checked = true;
                string[] str = ClsDataSplit.DataSplitFileName.Split(';');
                txtDataSplit_File_cd.Text = str[0];
                txtDataSplit_File_qian.Text = str[1];
                txtDataSplit_File_hou.Text = str[2];
                GetFileGz();
            }
            chkDataSplit_File_zero.Checked = ClsDataSplit.DataSplitzero;
            GetDataSplitExport();
        }


        private void AddDataSplitImport()
        {
            if (chkDataSplit.Items.Count <= 0)
                return;
            if (chkDataSplit.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkDataSplit.Items.Count; i++) {
                if (chkDataSplit.GetItemChecked(i)) {
                    string strid = chkDataSplit.GetItemText(chkDataSplit.Items[i]);
                    if (ClsDataSplit.DataSplitExportColtmp.IndexOf(strid) < 0) {
                        chkDataSplit_ExportTable.Items.Add(strid);
                        ClsDataSplit.DataSplitExportColtmp.Add(strid);
                    }
                }
            }
        }

        private void DelDataSplitImportcol()
        {
            if (chkDataSplit_ExportTable.Items.Count <= 0)
                return;
            if (chkDataSplit_ExportTable.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkDataSplit_ExportTable.Items.Count; i++) {
                if (chkDataSplit_ExportTable.GetItemChecked(i)) {
                    string str = chkDataSplit_ExportTable.GetItemText(chkDataSplit_ExportTable.Items[i]);
                    chkDataSplit_ExportTable.Items.RemoveAt(i);
                    if (ClsDataSplit.DataSplitExportColtmp.IndexOf(str) >= 0)
                        ClsDataSplit.DataSplitExportColtmp.Remove(str);
                    i--;
                }
            }
        }

        private void SaveDataSplitExport()
        {
            string str = "";
            if (txtDataSplitTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请先查询设置表名称!");
                txtDataSplitTable.Focus();
                return;
            }
            if (chkDataSplit_ExportTable.Items.Count <= 0) {
                MessageBox.Show("请先添加需导出的字段!");
                return;
            }
            if (combDataSplit_Export_Xlsid.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入要绑定的Xls工作薄ID!");
                combDataSplit_Export_Xlsid.Focus();
                return;
            }
            try {
                for (int i = 0; i < ClsDataSplit.DataSplitExportColtmp.Count; i++) {
                    if (i != ClsDataSplit.DataSplitExportColtmp.Count - 1)
                        str += ClsDataSplit.DataSplitExportColtmp[i] + ";";
                    else
                        str += ClsDataSplit.DataSplitExportColtmp[i];
                }

                if (ClsDataSplit.DataSplitExportTable.IndexOf(txtDataSplitTable.Text.Trim()) < 0) {
                    if (ClsDataSplit.DataSplitExportxlsid.IndexOf(combDataSplit_Export_Xlsid.Text.Trim()) >= 0) {
                        MessageBox.Show("此Xls工作薄ID已绑定，请更换!");
                        return;
                    }
                    T_Sysset.SaveDataSplitExport(txtDataSplitTable.Text.Trim(), str, combDataSplit_Export_Xlsid.Text.Trim());
                }
                else
                    T_Sysset.UpdateDataSplitExport(txtDataSplitTable.Text.Trim(), str, combDataSplit_Export_Xlsid.Text.Trim());
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetDataSplitExport();
                string s = "修改数据拆分导出表:" + txtDataSplitTable.Text.Trim() + "->" + str + "->" + combDataSplit_Export_Xlsid.Text.Trim();
                Common.Writelog(0, s);
            }
        }

        private void GetDataSplitExport()
        {
            try
            {
                DataTable dt = T_Sysset.GetDataSplitExporTable();
                if (dt == null || dt.Rows.Count < 0)
                    return;
                ClsDataSplit.DataSplitExportTable.Clear();
                ClsDataSplit.DataSplitExportCol.Clear();
                ClsDataSplit.DataSplitExportxlsid.Clear();
                chkDataSplit_ExportTable.Items.Clear();
                foreach (DataRow dr in dt.Rows) {
                    string table = dr["ImportTable"].ToString();
                    string col = dr["ImportCol"].ToString();
                    string xlsid = dr["BindId"].ToString();
                    combDataSplit_Export_table.Items.Add(table);
                    ClsDataSplit.DataSplitExportTable.Add(table);
                    combDataSplit_Export_Xlsid.Items.Add(xlsid);
                    ClsDataSplit.DataSplitExportxlsid.Add(xlsid);
                    ClsDataSplit.DataSplitExportCol.Add(col);
                }

                string col1 = dt.Rows[0][2].ToString();
                if (col1.IndexOf(';') >= 0) {
                    string[] c = col1.Split(';');
                    for (int i = 0; i < c.Length; i++) {
                        chkDataSplit_ExportTable.Items.Add(c[i].ToString());
                        ClsDataSplit.DataSplitExportxlsid.Add(c[i].ToString());
                    }
                }
                else {
                    chkDataSplit_ExportTable.Items.Add(col1);
                    ClsDataSplit.DataSplitExportxlsid.Add(col1);
                }
            }
            catch 
            {}
        }


        private void DataSplitExportIndex(int id)
        {
            if (id < 0)
                return;
            combDataSplit_Export_Xlsid.SelectedIndex = id;
            string s = ClsDataSplit.DataSplitExportCol[id].ToString();
            if (s.IndexOf(';') >= 0) {
                chkDataSplit_ExportTable.Items.Clear();
                ClsDataSplit.DataSplitExportColtmp.Clear();
                string[] c = s.Split(';');
                for (int i = 0; i < c.Length; i++) {
                    chkDataSplit_ExportTable.Items.Add(c[i].ToString());
                    ClsDataSplit.DataSplitExportColtmp.Add(c[i].ToString());
                }
            }
        }

        private void DelDataSplitExportTable()
        {
            string str = combDataSplit_Export_table.Text.Trim();
            if (str.Length <= 0)
                return;
            try {
                if (ClsDataSplit.DataSplitExportTable.IndexOf(str) >= 0)
                    T_Sysset.DelDataSplitExportTable(str);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetDataSplitExport();
                string s = "删除信息->数据导出表:" + str;
                Common.Writelog(0, s);
            }
        }
        private void butDataSplit_Click(object sender, EventArgs e)
        {
            IsDataTable();
        }

        private void butDataSplitgz_Click(object sender, EventArgs e)
        {
            Getgzzd();
        }
        private void chkDataSplit_dir_ml_Click(object sender, EventArgs e)
        {
            if (chkDataSplit_dir_ml.Checked)
            {
                combDataSplit_dir_ml.Enabled = true;
                combDataSplit_dir_pages.Enabled = true;
                return;
            }
            combDataSplit_dir_ml.Enabled = false;
            combDataSplit_dir_pages.Enabled = false;
        }
        private void chkDataSplit_Click(object sender, EventArgs e)
        {
            if (rabDataSplit_File_zd.Checked && rabDataSplitFile.Checked)
                Clechk();
        }
        private void rabDataSplit_File_dirname_Click(object sender, EventArgs e)
        {
            ClsDataSplit.DataSplitFilesn = 1;
        }
        private void rabDataSplit_File_anjuan_Click(object sender, EventArgs e)
        {
            ClsDataSplit.DataSplitFilesn = 2;
        }
        private void rabDataSplit_File_zd_Click(object sender, EventArgs e)
        {
            ClsDataSplit.DataSplitFilesn = 3;
        }
        private void butDataSplitSave_Click(object sender, EventArgs e)
        {
            SaveDataSplitinfo();
        }

        private void butDataSplit_Import_add_Click(object sender, EventArgs e)
        {
            AddDataSplitImport();
        }

        private void butDataSplit_Import_delzd_Click(object sender, EventArgs e)
        {
            DelDataSplitImportcol();
        }

        private void butDataSplit_Export_Save_Click(object sender, EventArgs e)
        {
            SaveDataSplitExport();
        }

        private void combDataSplit_Export_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSplitExportIndex(combDataSplit_Export_table.SelectedIndex);
        }

        private void butDataSplit_Export_DelInfo_Click(object sender, EventArgs e)
        {
            DelDataSplitExportTable();
        }

        #endregion

        #region Infocheck
        private void IsInfocheckTable()
        {
            if (txtInfoCheckTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtInfoCheckTable.Focus();
                return;
            }
            ClsQuerInfo.QuerTable = txtInfoCheckTable.Text.Trim();
            string table = T_Sysset.isTable(ClsQuerInfo.QuerTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存！");
                txtInfoCheckTable.Focus();
                return;
            }
            chkInfoCheckTableCol.Items.Clear();
            ClsInfoCheck.InfoCheckColtmp.Clear();
            DataTable dt = T_Sysset.GetTableName(ClsQuerInfo.QuerTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkInfoCheckTable.DataSource = dt;
                chkInfoCheckTable.DisplayMember = "Name";
            }
        }


        private void AddInfocheckcol()
        {
            if (chkInfoCheckTable.Items.Count <= 0)
                return;
            if (chkInfoCheckTable.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkInfoCheckTable.Items.Count; i++) {
                if (chkInfoCheckTable.GetItemChecked(i)) {
                    string strid = chkInfoCheckTable.GetItemText(chkInfoCheckTable.Items[i]);
                    if (strid.ToLower() == "id" || strid.ToLower() == "archid" || strid.ToLower() == "entertag")
                        continue;
                    if (ClsInfoCheck.InfoCheckColtmp.IndexOf(strid) < 0) {
                        chkInfoCheckTableCol.Items.Add(strid);
                        ClsInfoCheck.InfoCheckColtmp.Add(strid);
                    }
                }
            }
        }

        private void DelInfocheckcol()
        {
            if (chkInfoCheckTableCol.Items.Count <= 0)
                return;
            if (chkInfoCheckTableCol.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkInfoCheckTableCol.Items.Count; i++) {
                if (chkInfoCheckTableCol.GetItemChecked(i)) {
                    string strid = chkInfoCheckTableCol.GetItemText(chkInfoCheckTableCol.Items[i]);
                    if (ClsInfoCheck.InfoCheckColtmp.IndexOf(strid) < 0) {
                        chkInfoCheckTableCol.Items.Remove(strid);
                        ClsInfoCheck.InfoCheckColtmp.Remove(strid);
                        i--;
                    }
                }
            }
        }

        private void InfoCheckSave()
        {
            string str = "";
            string table = txtInfoCheckTable.Text.Trim();
            try {
                if (table.Length <= 0) {
                    if (combInfoCheckTable.Text.Trim().Length <= 0) {
                        MessageBox.Show("请输入数据库表名称!");
                        txtInfoCheckTable.Focus();
                        return;
                    }
                    table = combInfoCheckTable.Text.Trim();
                }
                if (chkInfoCheckTableCol.Items.Count <= 0) {
                    MessageBox.Show("请先添加字段！");
                    return;
                }
                if (combInfoCheck_info.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择所属信息框!");
                    combInfoCheck_info.Focus();
                    return;
                }
                for (int i = 0; i < ClsInfoCheck.InfoCheckColtmp.Count; i++) {
                    if (i != ClsInfoCheck.InfoCheckColtmp.Count - 1)
                        str += ClsInfoCheck.InfoCheckColtmp[i] + ";";
                    else
                        str += ClsInfoCheck.InfoCheckColtmp[i];
                }
                if (ClsInfoCheck.InfoCheckTable.IndexOf(table) < 0)
                    T_Sysset.SaveGensetInfoCheck(table, str, combInfoCheck_info.Text.Trim().ToString());
                else
                    T_Sysset.UpdateGensetInfoCheck(table, str, combInfoCheck_info.Text.Trim().ToString());
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetInfoCheck();
                string s = "修改数据校验表:" + ClsImportTable.ImportTable + "->" + str;
                Common.Writelog(0, s);
            }
        }

        private void GetInfoCheck()
        {
            ClsInfoCheck.InfoCheckTable.Clear();
            ClsInfoCheck.InfocheckMsg.Clear();
            ClsInfoCheck.InfoCheckCol.Clear();
            ClsInfoCheck.InfoCheckColtmp.Clear();
            combInfoCheckTable.Items.Clear();
            chkInfoCheckTableCol.Items.Clear();
            DataTable dt = T_Sysset.GetInfoCheck();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                string tb = dr["InfoCheckTable"].ToString();
                string col = dr["InfocheckCol"].ToString();
                string ms = dr["InfoCheckMsg"].ToString();
                ClsInfoCheck.InfoCheckTable.Add(tb);
                combInfoCheckTable.Items.Add(tb);
                ClsInfoCheck.InfoCheckCol.Add(col);
                ClsInfoCheck.InfocheckMsg.Add(ms);

            }
            ClsInfoAdd.InfoTable = ClsInfoCheck.InfoCheckTable[0];
            txtInfoCheckTable.Text = ClsInfoCheck.InfoCheckTable[0];
            string str = ClsInfoCheck.InfoCheckCol[0];
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    chkInfoCheckTableCol.Items.Add(b);
                    ClsInfoCheck.InfoCheckColtmp.Add(b);
                }
            }

        }

        private void SelectInfoCheck(int id)
        {
            combInfoCheck_info.Text = ClsInfoCheck.InfocheckMsg[id];
            string str = ClsInfoCheck.InfoCheckCol[id];
            if (str.Length > 0) {
                chkInfoCheckTableCol.Items.Clear();
                ClsInfoCheck.InfoCheckColtmp.Clear();
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    chkInfoCheckTableCol.Items.Add(b);
                    ClsInfoCheck.InfoCheckColtmp.Add(b);
                }
            }
        }

        private void DelInfoCheckTable()
        {
            string str = combInfoCheckTable.Text.Trim();
            if (str.Length <= 0)
                return;
            try {
                if (ClsInfoCheck.InfoCheckTable.IndexOf(str) >= 0)
                    ClsInfoCheck.InfoCheckTable.Remove(str);
                T_Sysset.DelInfoCheckTable(str);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetInfoCheck();
                string s = "删除数据校验表:" + str;
                Common.Writelog(0, s);
            }
        }

        private void butInfoCheckTableis_Click(object sender, EventArgs e)
        {
            IsInfocheckTable();
        }

        private void butInfoCheckColadd_Click(object sender, EventArgs e)
        {
            AddInfocheckcol();
        }

        private void butInfoCheckTableSave_Click(object sender, EventArgs e)
        {
            InfoCheckSave();
        }
        private void butInfoCheckColdel_Click(object sender, EventArgs e)
        {
            DelInfocheckcol();
        }

        private void combInfoCheckTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectInfoCheck(combInfoCheckTable.SelectedIndex);
        }

        private void butinfoCheckTableDel_Click(object sender, EventArgs e)
        {
            DelInfoCheckTable();
        }

        #endregion

        #region Conten

        private void IsContenTable()
        {
            if (txtContenTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtContenTable.Focus();
                return;
            }
            ClsConten.ContenTable = txtContenTable.Text.Trim();
            string table = T_Sysset.isTable(ClsConten.ContenTable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存！");
                txtContenTable.Focus();
                return;
            }
            chkContenCol.Items.Clear();
            DataTable dt = T_Sysset.GetTableName(ClsConten.ContenTable);
            if (dt != null && dt.Rows.Count > 0) {
                chkContenCol.DataSource = dt;
                chkContenCol.DisplayMember = "Name";
            }
        }

        private void AddContenCol()
        {
            if (chkContenCol.Items.Count <= 0)
                return;
            if (chkContenCol.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkContenCol.Items.Count; i++) {
                if (chkContenCol.GetItemChecked(i)) {
                    string strid = chkContenCol.GetItemText(chkContenCol.Items[i]);
                    if (strid.ToLower() == "id" || strid.ToLower() == "archid" || strid.ToLower() == "entertag" || strid.ToLower()=="borrtag")
                        continue;
                    if (ClsConten.ContenCol.IndexOf(strid) < 0) {
                        chkContenColShow.Items.Add(strid);
                        combContenTitle.Items.Add(strid);
                        combContenPages.Items.Add(strid);
                        ClsConten.ContenCol.Add(strid);
                    }
                }
            }
        }


        private void DelContencol()
        {
            if (chkContenColShow.Items.Count <= 0)
                return;
            if (chkContenColShow.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkContenColShow.Items.Count; i++) {
                if (chkContenColShow.GetItemChecked(i)) {
                    string str = chkContenColShow.GetItemText(chkContenColShow.Items[i]);
                    chkContenColShow.Items.RemoveAt(i);
                    if (ClsConten.ContenCol.IndexOf(str) >= 0)
                        ClsConten.ContenCol.Remove(str);
                    combContenTitle.Items.Remove(str);
                    combContenPages.Items.Remove(str);
                    i--;
                }
            }
        }

        private void SaveConten()
        {
            string str = "";
            if (ClsConten.ContenTable.Length <= 0) {
                MessageBox.Show("请设置表名称!");
                return;
            }
            if (txtContenLieSn.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入显示列数!");
                txtContenLieSn.Focus();
                return;
            }
            if (txtContenlabWith.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入字段宽度!");
                txtContenlabWith.Focus();
                return;
            }

            if (txtContentxtWith.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入前台信息框宽度!");
                txtContentxtWith.Focus();
                return;
            }
            if (ClsConten.ContenCol.Count <= 0) {
                MessageBox.Show("请先添加需导入的字段!");
                return;
            }
            if (combContenTitle.Text.Trim().Length <= 0 || combContenPages.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择标题或页码字段！");
                combContenTitle.Focus();
                return;
            }
            else {
                if (combContenPages.Text.Trim() == combContenTitle.Text.Trim()) {
                    MessageBox.Show("标题字段和页码字段不能一致！");
                    combContenTitle.Focus();
                    return;
                }
            }
            try {
                for (int i = 0; i < ClsConten.ContenCol.Count; i++) {
                    if (i != ClsConten.ContenCol.Count - 1)
                        str += ClsConten.ContenCol[i] + ";";
                    else
                        str += ClsConten.ContenCol[i];
                }
                T_Sysset.UpdateConten(ClsConten.ContenTable, str, txtContenLieSn.Text.Trim(), txtContenlabWith.Text.Trim(), txtContentxtWith.Text.Trim(),
                    combContenTitle.Text.Trim(), combContenPages.Text.Trim());
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetnContenInfo();
                string s = "修改目录补录表:" + ClsConten.ContenTable + "->" + str;
                Common.Writelog(0, s);
            }
        }

        private void GetnContenInfo()
        {
            DataTable dt = T_Sysset.GetConten();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            chkContenColShow.Items.Clear();
            chkContenCol.DataSource = null;
            combContenPages.Items.Clear();
            combContenTitle.Items.Clear();
            ClsConten.ContenCol.Clear();
            DataRow dr = dt.Rows[0];
            ClsConten.ContenTable = dr["ContenTable"].ToString();
            txtContenLieSn.Text = dr["ContenLie"].ToString();
            txtContenlabWith.Text = dr["ContenWith"].ToString();
            txtContentxtWith.Text = dr["ContentxtWith"].ToString();
            string str = dr["ContenCol"].ToString();
            txtContenTable.Text = ClsConten.ContenTable;
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    if (b.Trim().Length > 0) {
                        ClsConten.ContenCol.Add(b);
                        chkContenColShow.Items.Add(b);
                        combContenPages.Items.Add(b);
                        combContenTitle.Items.Add(b);
                    }
                }
            }
            combContenTitle.Text = dr["ContenTitle"].ToString();
            combContenPages.Text = dr["ContenPages"].ToString();
        }


        private void butConten_Click(object sender, EventArgs e)
        {
            IsContenTable();
        }


        private void butContenColAdd_Click(object sender, EventArgs e)
        {
            AddContenCol();
        }

        private void butContenColDel_Click(object sender, EventArgs e)
        {
            DelContencol();
        }

        private void butContenTableSave_Click(object sender, EventArgs e)
        {
            SaveConten();
        }

        #endregion


        #region CreateTable
        private void CreateTableab()
        {
            ClsCreateTable.CreatetableTf = false;
            labsm.Text = "警告：保存表时会自动\n创建ID及Archid\nEnterTab字段此字段\n为系统保留\n不可删除!";
            string str = ClsCreateTable.coltmp;
            ClsCreateTable.CreateTableCollx.Clear();
            combCreateTableLx.Items.Clear();
            ClsCreateTable.CreateTableSys.Clear();
            ClsCreateTable.CreateTableCollx2.Clear();
            ClsCreateTable.CreateTableCol.Clear();
            ClsCreateTable.CreateTableCollx = str.Split(';').ToList();
            for (int i = 0; i < ClsCreateTable.CreateTableCollx.Count; i++) {
                str = ClsCreateTable.CreateTableCollx[i].ToString();
                combCreateTableLx.Items.Add(str);
                if (str.IndexOf("(") >= 0) {
                    str = str.Substring(0, str.IndexOf("(") + 1);
                    ClsCreateTable.CreateTableCollx2.Add(str);
                }
            }
            DataTable dt = T_Sysset.GetSysTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                str = dr["TableName"].ToString();
                if (str.Trim().Length <= 0)
                    return;
                ClsCreateTable.CreateTableSys.Add(str);
            }
        }

        private void IsCreateTable()
        {
            ClsCreateTable.CreatetableTf = false;
            if (txtCreateTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入目录表名称!");
                txtCreateTable.Focus();
                return;
            }
            ClsCreateTable.CreateTable = txtCreateTable.Text.Trim();
            lvCreateTable.Items.Clear();
            ClsCreateTable.CreateTableCol.Clear();
            ClsCreateTable.CreateTableColtmp.Clear();
            ClsCreateTable.CreateTablecolsm.Clear();
            ClsCreateTable.CreateTableCollxtmp.Clear();
            ClsCreateTable.CreateTablecolnullktmp.Clear();
            string table = T_Sysset.isTable(ClsCreateTable.CreateTable);
            if (table.Length <= 0) {
                MessageBox.Show("表名称不存在可以创建！");
                txtCreateTable.Focus();
                ClsCreateTable.CreatetableTf = true;
            }
            else {
                ClsCreateTable.CreatetableTf = false;
                DataTable dt = T_Sysset.GetTableCol(ClsCreateTable.CreateTable);
                if (dt == null || dt.Rows.Count < 0)
                    return;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lt = new ListViewItem();
                    string col = dr["fieldName"].ToString();
                    lt.Text = col;
                    ClsCreateTable.CreateTableCol.Add(col);
                    lt.SubItems.Add(dr["fieldType"].ToString());
                    string cd = dr["fieldLength"].ToString();
                    if (cd == "-1")
                        cd = "MAX";
                    lt.SubItems.Add(cd);
                    lt.SubItems.Add(dr["allowEmpty"].ToString());
                    lt.SubItems.Add(dr["fieldDescript"].ToString());
                    lvCreateTable.Items.Add(lt);
                }
            }
            if (ClsCreateTable.CreateTableSys.IndexOf(ClsCreateTable.CreateTable) >= 0) {
                butCreateTableSave.Enabled = false;
                butCreateTableDel.Enabled = false;
            }
            else {
                butCreateTableSave.Enabled = true;
                butCreateTableDel.Enabled = true;
            }
        }
        private bool istxtCt()
        {
            if (txtCreateTableColName.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入字段名称!");
                txtCreateTableColName.Focus();
                return false;
            }
            if (ClsCreateTable.CreateTableColtmp.IndexOf(txtCreateTableColName.Text.Trim()) >= 0) {
                MessageBox.Show("字段名称已存在请更改!");
                txtCreateTableColName.Focus();
                return false;
            }
            if (ClsCreateTable.CreateTableCol.IndexOf(txtCreateTableColName.Text.Trim()) >= 0) {
                MessageBox.Show("字段名称已存在请更改!");
                txtCreateTableColName.Focus();
                return false;
            }
            if (combCreateTableLx.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入字段类型!");
                combCreateTableLx.Focus();
                return false;
            }
            string str = combCreateTableLx.Text.Trim();
            if (str.IndexOf("(") < 0) {
                if (ClsCreateTable.CreateTableCollx.IndexOf(str) < 0) {
                    MessageBox.Show("此类型不存在!");
                    combCreateTableLx.Focus();
                    return false;
                }
            }
            return true;
        }

        private void LvitemAdd()
        {
            if (!istxtCt())
                return;

            string col = txtCreateTableColName.Text.Trim();
            if (col.IndexOf("(") >= 0 || col.IndexOf("（") >= 0)
            {
                MessageBox.Show("字段名称不允许带各种符号!");
                return;
            }
            string lx = combCreateTableLx.Text.Trim();
            string sm = txtCreateTableColsm.Text.Trim();
            string nullk = "";
            if (chbCreateTablenull.Checked)
                nullk = "是";
            else
                nullk = "否";

            ListViewItem lt = new ListViewItem();
            lt.Text = col;
            lt.ForeColor = Color.Red;
            lt.SubItems.Add(lx);
            lt.SubItems.Add("");
            lt.SubItems.Add(nullk);
            lt.SubItems.Add(sm);
            lvCreateTable.Items.Add(lt);
            ClsCreateTable.CreateTableColtmp.Add(col);
            ClsCreateTable.CreateTablecolsm.Add(sm);
            ClsCreateTable.CreateTableCollxtmp.Add(lx);
            if (nullk == "是")
                nullk = "null";
            else nullk = "not null";
            ClsCreateTable.CreateTablecolnullktmp.Add(nullk);
        }

        private void Getinfo()
        {
            string col = lvCreateTable.SelectedItems[0].SubItems[0].Text;
            if (col.ToLower() == "id" || col.ToLower() == "archid" || col.ToLower() == "entertab")
                return;
            ClsCreateTable.CreateTableLvcol = col;
            string lx = lvCreateTable.SelectedItems[0].SubItems[1].Text;
            string cd = lvCreateTable.SelectedItems[0].SubItems[2].Text;
            string nullk = lvCreateTable.SelectedItems[0].SubItems[3].Text;
            string sm = lvCreateTable.SelectedItems[0].SubItems[4].Text;
            ClsCreateTable.CreateTableLvsm = sm;
            txtCreateTableColName.Text = col;
            txtCreateTableColsm.Text = sm;
            if (ClsCreateTable.CreateTableCollx2.IndexOf(lx + "(") >= 0)
                combCreateTableLx.Text = lx + "(" + cd + ")";
            else
                combCreateTableLx.Text = lx;
            if (nullk == "是")
                chbCreateTablenull.Checked = true;
            else
                chbCreateTablenull.Checked = false;
        }

        private void Lvcoldel(int id)
        {
            string str = lvCreateTable.SelectedItems[0].SubItems[0].Text;
            if (str.Trim().Length <= 0 || str.ToLower() == "id" || str.ToLower() == "archid" || str.ToLower() == "entertab") {

                MessageBox.Show("未选中字段或为系统保留字段无法删除!");
                return;
            }
            if (id == 0) {
                int colid = ClsCreateTable.CreateTableColtmp.IndexOf(str);
                if (colid >= 0) {
                    lvCreateTable.Items.RemoveAt(lvCreateTable.SelectedItems[0].Index);
                    ClsCreateTable.CreateTableColtmp.RemoveAt(colid);
                    ClsCreateTable.CreateTableCollxtmp.RemoveAt(colid);
                    ClsCreateTable.CreateTablecolnullktmp.RemoveAt(colid);
                }
                else {
                    MessageBox.Show("此字段无法删除或不是临时添加字段!");
                    return;
                }
            }
            else {
                if (ClsCreateTable.CreateTableColtmp.IndexOf(str) >= 0) {
                    MessageBox.Show("此字段为临时字段，请用移除功能!");
                    return;
                }
                T_Sysset.DelTableCol(ClsCreateTable.CreateTable, str);
                butCreateTableis_Click(null, null);
                string s = "删除表字段:" + ClsCreateTable.CreateTable + "->字段:" + str;
                Common.Writelog(0, s);
            }
        }

        private void CreateTableSave()
        {
            if (txtCreateTable.Text.Trim().Length <= 0 || ClsCreateTable.CreateTable==null) {
                MessageBox.Show("请输入要创建或保存的表名称!");
                txtCreateTable.Focus();
                return;
            }
            if (ClsCreateTable.CreateTableColtmp.Count <= 0) {
                MessageBox.Show("未添加新字段无法进行保存!");
                return;
            }

            string str = "";
            string lx = "";
            string nullk = "";
            for (int i = 0; i < ClsCreateTable.CreateTableColtmp.Count; i++) {
                str += ClsCreateTable.CreateTableColtmp[i] + ",";
                lx += ClsCreateTable.CreateTableCollxtmp[i] + ",";
                nullk += ClsCreateTable.CreateTablecolnullktmp[i] + ",";
            }
            try {
                T_Sysset.SaveCreateTable(ClsCreateTable.CreateTable, ClsCreateTable.CreateTableColtmp,
                    ClsCreateTable.CreateTableCollxtmp, ClsCreateTable.CreateTablecolnullktmp,
                     ClsCreateTable.CreatetableTf);
                T_Sysset.CreateTableExplain(ClsCreateTable.CreateTable, ClsCreateTable.CreateTableColtmp, ClsCreateTable.CreateTablecolsm);
                MessageBox.Show("保存成功");
                butCreateTableis_Click(null, null);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                string s = "创建修改表:" + ClsCreateTable.CreateTable + "->字段:" + str + " ->类型" + lx + " ->是否允许空" + nullk;
                Common.Writelog(0, s);
            }
        }

        private void CreatetableUpdate()
        {
            if (txtCreateTable.Text.Trim().Length <= 0 || ClsCreateTable.CreateTable==null ||ClsCreateTable.CreateTable.Trim().Length <= 0) {
                MessageBox.Show("请输入要创建或保存的表名称!");
                txtCreateTable.Focus();
                return;
            }
            if (ClsCreateTable.CreateTableLvcol==null ||ClsCreateTable.CreateTableLvcol.Trim().Length <= 0) {
                MessageBox.Show("请选择相关字段!");
                return;
            }
            T_Sysset.CreateTableColAlter(ClsCreateTable.CreateTable, ClsCreateTable.CreateTableLvcol,
                combCreateTableLx.Text.Trim(),
                chbCreateTablenull.Checked);
            if (ClsCreateTable.CreateTableLvsm.Trim().Length <= 0 && txtCreateTableColsm.Text.Trim().Length <= 0)
                return;
            else if (ClsCreateTable.CreateTableLvsm.Trim().Length <= 0 && txtCreateTableColsm.Text.Trim().Length > 0) {
                try {
                    T_Sysset.CreateTableExplain(ClsCreateTable.CreateTable, ClsCreateTable.CreateTableLvcol, txtCreateTableColsm.Text.Trim());
                } catch {
                    T_Sysset.CreateTableUpdateExplain(ClsCreateTable.CreateTable, ClsCreateTable.CreateTableLvcol, txtCreateTableColsm.Text.Trim());
                }
            }
            else if (ClsCreateTable.CreateTableLvsm.Trim().Length > 0 && txtCreateTableColsm.Text.Trim().Length <= 0)
                T_Sysset.CreateTableDelExplain(ClsCreateTable.CreateTable, ClsCreateTable.CreateTableLvcol);
        }


        private void butCreateTableis_Click(object sender, EventArgs e)
        {
            IsCreateTable();
        }

        private void butCreateTableSave_Click(object sender, EventArgs e)
        {
            CreateTableSave();
        }

        private void lvCreateTable_Click(object sender, EventArgs e)
        {
            if (lvCreateTable.SelectedItems.Count > 0 && lvCreateTable.SelectedIndices.Count > 0)
                Getinfo();
        }

        private void butCreateTableLvadd_Click(object sender, EventArgs e)
        {
            LvitemAdd();
        }
        private void butCreateTableLvcle_Click(object sender, EventArgs e)
        {
            if (lvCreateTable.SelectedItems.Count > 0 && lvCreateTable.SelectedIndices.Count > 0)
                Lvcoldel(0);
        }
        private void butCreateTableDel_Click(object sender, EventArgs e)
        {
            if (lvCreateTable.SelectedItems.Count > 0 && lvCreateTable.SelectedIndices.Count > 0)
                Lvcoldel(1);
        }

        private void butCreateTableUpdate_Click(object sender, EventArgs e)
        {
            if (lvCreateTable.SelectedItems.Count > 0 && lvCreateTable.SelectedIndices.Count > 0)
                CreatetableUpdate();
        }

        #endregion


        #region borrset

        private void IsborrTable()
        {
            ClsborrTable.Clsborrtag = false;
            if (txtBorrTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入表名称!");
                txtQuerTable.Focus();
                return;
            }
            ClsborrTable.Clsborrtable = txtBorrTable.Text.Trim();
            string table = T_Sysset.isTable(ClsborrTable.Clsborrtable);
            if (table.Length <= 0) {
                MessageBox.Show("表不存！");
                txtBorrTable.Focus();
                return;
            }
            chkBorrTablecol.DataSource = null;
            chkBorrtablquer.Items.Clear();
            comborrTime.Items.Clear();
            ClsborrTable.ClsBorrColzd.Clear();
            comborrTime.Items.Add("");
            DataTable dt = T_Sysset.GetTableName(ClsborrTable.Clsborrtable);
            if (dt != null && dt.Rows.Count > 0) {
                chkBorrTablecol.DataSource = dt;
                chkBorrTablecol.DisplayMember = "Name";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str = dt.Rows[i][0].ToString();
                    comborrTime.Items.Add(str);
                    if (str.ToLower() == "borrtag")
                        ClsborrTable.Clsborrtag = true;
                }
            }
        }


        private void Addborrcol()
        {
            if (chkBorrTablecol.Items.Count <= 0)
                return;
            if (chkBorrTablecol.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkBorrTablecol.Items.Count; i++) {
                if (chkBorrTablecol.GetItemChecked(i)) {
                    string strid = chkBorrTablecol.GetItemText(chkBorrTablecol.Items[i]);
                    if (strid.ToLower() == "id" || strid.ToLower() == "archid" || strid.ToLower() == "entertag")
                        continue;
                    if (ClsborrTable.ClsBorrColzd.IndexOf(strid) < 0) {
                        chkBorrtablquer.Items.Add(strid);
                        ClsborrTable.ClsBorrColzd.Add(strid);
                    }
                }
            }
        }

        private void Delborrcol()
        {
            if (chkBorrtablquer.Items.Count <= 0)
                return;
            if (chkBorrtablquer.CheckedItems.Count <= 0)
                return;
            for (int i = 0; i < chkBorrtablquer.Items.Count; i++) {
                if (chkBorrtablquer.GetItemChecked(i)) {
                    string str = chkBorrtablquer.GetItemText(chkBorrtablquer.Items[i]);
                    chkBorrtablquer.Items.RemoveAt(i);
                    if (ClsborrTable.ClsBorrColzd.IndexOf(str) >= 0)
                        ClsborrTable.ClsBorrColzd.Remove(str);
                    i--;
                }
            }
        }

        private void SaveborrInfo()
        {
            string str = "";
            string timecol = comborrTime.Text.Trim();
            if (ClsborrTable.Clsborrtable.Length <= 0) {
                MessageBox.Show("请设置表名称!");
                return;
            }
            if (chkBorrtablquer.Items.Count <= 0) {
                MessageBox.Show("请先添加需导入的字段!");
                return;
            }
            try {
                for (int i = 0; i < ClsborrTable.ClsBorrColzd.Count; i++) {
                    if (i != ClsborrTable.ClsBorrColzd.Count - 1)
                        str += ClsborrTable.ClsBorrColzd[i] + ";";
                    else
                        str += ClsborrTable.ClsBorrColzd[i];
                }

                T_Sysset.UpdateBorrInfo(ClsborrTable.Clsborrtable, str,ClsborrTable.Clsborrtag, timecol);
                MessageBox.Show("保存成功!");
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                GetnborrInfo();
                string s = "修改借阅表:" + ClsborrTable.Clsborrtable + "->" + str;
                Common.Writelog(0, s);
            }
        }

        private void GetnborrInfo()
        {
            ClsborrTable.Clsborrtable = "";
            ClsborrTable.ClsBorrColzd.Clear();
            chkBorrTablecol.DataSource = null;
            chkBorrtablquer.Items.Clear();
            DataTable dt = T_Sysset.GetborrTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            ClsborrTable.Clsborrtable = dr["Tablename"].ToString();
            string str = dr["Tabcolname"].ToString();
            string time = dr["Timecol"].ToString();
            txtBorrTable.Text = ClsborrTable.Clsborrtable;
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    if (b.Trim().Length > 0) {
                        ClsborrTable.ClsBorrColzd.Add(b);
                        chkBorrtablquer.Items.Add(b);
                    }
                }
            }
            comborrTime.Text = time;
        }

        private void butBorrIs_Click(object sender, EventArgs e)
        {
            IsborrTable();
        }

        private void butBorradd_Click(object sender, EventArgs e)
        {
            Addborrcol();
        }

        private void butBorrdel_Click(object sender, EventArgs e)
        {
            Delborrcol();
        }

        private void butBorrSave_Click(object sender, EventArgs e)
        {
            SaveborrInfo();
        }
        #endregion

        private void Infoshow()
        {
            GetGenSetPrint();
            GetnPrintConten();
            GetImportSet();
            GetInfoSet();
            GetnQuerInfo();
            GetdataSplit();
            GetInfoCheck();
            GetnContenInfo();
            CreateTableab();
            GetnborrInfo();
        }
        private void FrmGetSet_Shown(object sender, EventArgs e)
        {
            Infoshow();
        }

    }

}
