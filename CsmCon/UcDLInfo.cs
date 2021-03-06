﻿using System;
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
    public partial class UcDLInfo : UserControl
    {
        public UcDLInfo()
        {
            InitializeComponent();
        }
       
        public static int ywidts = 0;

        public void Cleinfo()
        {
            try {
                dgvInfo.Columns.Clear();
                dgvInfo.Rows.Clear();
            } catch { }

        }

        public void Getywid(string s)
        {
            labywid.Text = string.Format("第{0}手", s);
        }

        public void LoadInfo(int arid)
        {
            dgvInfo.Columns.Clear();
            dgvInfo.Rows.Clear();
            ywidts = 0;
            DataTable dt = Common.GetcheckInfo(arid);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ywidts = dt.Rows.Count;
            labcount.Text = string.Format("共{0}手", dt.Rows.Count.ToString());
            try {
                if (dgvInfo.Columns.Count <= 0) {
                    dgvInfo.Columns.Add("name", "名称");
                    for (int i = 0; i < 9; i++) {
                        dgvInfo.Rows.Add();
                    }
                    dgvInfo.Rows[0].Cells[0].Value = "登记类型";
                    dgvInfo.Rows[1].Cells[0].Value = "收件编号";
                    dgvInfo.Rows[2].Cells[0].Value = "权利人";
                    dgvInfo.Rows[3].Cells[0].Value = "坐落";
                    dgvInfo.Rows[4].Cells[0].Value = "抵押人";
                    dgvInfo.Rows[5].Cells[0].Value = "地号";
                    dgvInfo.Rows[6].Cells[0].Value = "产权证号";
                    dgvInfo.Rows[7].Cells[0].Value = "宗地号";
                    dgvInfo.Rows[8].Cells[0].Value = "不动产单元号";
                    dgvInfo.Rows[9].Cells[0].Value = "审批日期";

                }
                for (int t = 0; t < dt.Rows.Count; t++) {

                    dgvInfo.Columns.Add("ywid", ("业务ID:" + (t + 1).ToString()));
                    for (int i = 0; i < 10; i++) {
                        string str = dt.Rows[t][i].ToString();
                        dgvInfo.Rows[i].Cells[t + 1].Value = str;
                    }
                }

            } catch {
                MessageBox.Show("信息加载失败，请重新加载!");
            }

        }

        public void LoadInfo2(int arid, int ywid)
        {
            dgvInfo.Columns.Clear();
            dgvInfo.Rows.Clear();
            ywidts = 0;
            DataTable dtinfo2;
            try {
                if (ywid < 1) {
                    dtinfo2 = Common.GetcheckInfo(arid);
                    ywid = 1;
                }
                else
                    dtinfo2 = Common.GetcheckInfo(arid,ywid);
                if (dtinfo2 == null || dtinfo2.Rows.Count <= 0)
                    return;
                ywidts = dtinfo2.Rows.Count;
                labcount.Text = string.Format("共{0}手", dtinfo2.Rows.Count.ToString());

                if (dgvInfo.Columns.Count <= 0) {
                    dgvInfo.Columns.Add("name", "名称");
                    for (int i = 0; i < 9; i++) {
                        dgvInfo.Rows.Add();
                    }
                    dgvInfo.Rows[0].Cells[0].Value = "登记类型";
                    dgvInfo.Rows[1].Cells[0].Value = "收件编号";
                    dgvInfo.Rows[2].Cells[0].Value = "权利人";
                    dgvInfo.Rows[3].Cells[0].Value = "坐落";
                    dgvInfo.Rows[4].Cells[0].Value = "抵押人";
                    dgvInfo.Rows[5].Cells[0].Value = "地号";
                    dgvInfo.Rows[6].Cells[0].Value = "产权证号";
                    dgvInfo.Rows[7].Cells[0].Value = "宗地号";
                    dgvInfo.Rows[8].Cells[0].Value = "不动产单元号";
                    dgvInfo.Rows[9].Cells[0].Value = "审批日期";

                }
                for (int t = 0; t < 1; t++) {

                    dgvInfo.Columns.Add("ywid", ("业务ID:" + ywid.ToString()));
                    for (int i = 0; i < 10; i++) {
                        string str = dtinfo2.Rows[t][i].ToString();
                        dgvInfo.Rows[i].Cells[t + 1].Value = str;
                    }
                }

            } catch{
                MessageBox.Show("信息加载失败，请重新加载!");
            }
        }

        public void SetCol(int id)
        {
            if (dgvInfo.RowCount <= 0)
                return;
            if (dgvInfo.ColumnCount <= 0)
                return;
            for (int t = 0; t < dgvInfo.ColumnCount; t++) {
                if (id == t)
                    dgvInfo.Columns[t].DefaultCellStyle.BackColor = Color.Red;
                else
                    dgvInfo.Columns[t].DefaultCellStyle.BackColor = Color.White;
            }
            labywid.Text = string.Format("第{0}手", id);

        }



        public void Closedt()
        {
            try {
                if (dgvInfo.RowCount <= 0)
                    return;
                dgvInfo.Columns.Clear();
                dgvInfo.Rows.Clear();
            } catch { }
        }
    }
}
