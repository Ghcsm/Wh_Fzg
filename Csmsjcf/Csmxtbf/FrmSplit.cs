﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLjscom;
using DataRow = System.Data.DataRow;
using System.Xml;
using System.Drawing;
using System.Net;
using Spire.Xls;
using Exception = System.Exception;
using System.Runtime.Serialization.Formatters.Binary;

namespace Csmsjcf
{
    public partial class FrmSplit : Form
    {
        public FrmSplit()
        {
            InitializeComponent();
        }

        public static Hljsimage Himg;
        HLFtp.HFTP ftp = new HLFtp.HFTP();
        #region baseInfo

        private void CombAddinfo()
        {

            comb_gr2_2_task.Items.Clear();
            comb_gr2_7_weizhi.Items.Clear();
            combHouseid.Items.Clear();
            combKf.Items.Clear();
            for (int i = 1; i <= 10; i++) {
                comb_gr2_2_task.Items.Add(i.ToString());
            }
            comb_gr2_2_task.SelectedIndex = 0;
            comb_gr2_7_weizhi.Items.Add("左上");
            comb_gr2_7_weizhi.Items.Add("左中");
            comb_gr2_7_weizhi.Items.Add("左下");
            comb_gr2_7_weizhi.Items.Add("右上");
            comb_gr2_7_weizhi.Items.Add("右中");
            comb_gr2_7_weizhi.Items.Add("右下");
            comb_gr2_7_weizhi.Items.Add("中上");
            comb_gr2_7_weizhi.Items.Add("居中");
            comb_gr2_7_weizhi.Items.Add("中下");
            comb_gr2_7_weizhi.Items.Add("以上所有位置");
            comb_gr2_7_weizhi.Items.Add("四角");
            comb_gr2_7_weizhi.SelectedIndex = 0;
            List<V_HouseSet> HouseEc = new List<V_HouseSet>();
            DataTable dt = T_Sysset.GetHouseName();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                V_HouseSet House = new V_HouseSet();
                House.HouseID = Convert.ToInt32(dr["id"].ToString());
                House.HouseName = dr["HOUSENAME"].ToString();
                HouseEc.Add(House);
            }
            combHouseid.Items.AddRange(HouseEc.ToArray());
            combHouseid.SelectedItem = HouseEc;
            combHouseid.SelectedIndex = 0;
            combKf.Items.AddRange(HouseEc.ToArray());
            combKf.SelectedItem = HouseEc;
            combKf.SelectedIndex = 0;
            ClsFrmInfoPar.LogPath = Application.StartupPath;
        }

        private int dirsn()
        {
            if (rab_gr2_8_ziduan.Checked)
                return 1;
            if (rab_gr2_8_mulu.Checked)
                return 2;
            else return 3;
        }

        private void Addboxsn()
        {
            try {
                string str = "";
                if (rab_gr2_5_boxsn.Checked) {
                    string t1 = txt_gr2_5_box1.Text.Trim();
                    string t2 = txt_gr2_5_box2.Text.Trim();
                    int b1 = Convert.ToInt32(t1);
                    int b2 = Convert.ToInt32(t2);
                    if (b1 > b2 || b1 == 0 || b2 == 0) {
                        MessageBox.Show("请检查起始盒号和终止盒号!");
                        txt_gr2_5_box1.Focus();
                        return;
                    }
                    str = t1 + "-" + t2;
                    if (ClsFrmInfoPar.TaskBoxCounttmp.IndexOf(str) >= 0)
                        return;
                }
                else {
                    str = txt_gr2_5_juan.Text.Trim();
                }
                lv_gr2_5_boxCount.Items.Add(str);
                ClsFrmInfoPar.TaskBoxCounttmp.Add(str);
                txt_gr2_5_juan.Text = "";
                txt_gr2_5_box1.Text = "";
                txt_gr2_5_box2.Text = "";

            } catch { }
        }

        private void Delboxsn()
        {
            if (ClsFrmInfoPar.TaskBoxCounttmp.Count <= 0 || lv_gr2_5_boxCount.Items.Count <= 0 || lv_gr2_5_boxCount.SelectedItems.Count <= 0)
                return;
            int id = lv_gr2_5_boxCount.SelectedIndices[0];
            lv_gr2_5_boxCount.Items.RemoveAt(id);
            ClsFrmInfoPar.TaskBoxCounttmp.RemoveAt(id);
        }


        private bool IsTxtInfo()
        {
            if (rab_Gr2_3_tb.Checked || rab_Gr2_3_xls.Checked) {
                if (txt_gr3_1_xlsPath.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择xls模版文件!");
                    txt_gr3_1_xlsPath.Focus();
                    return false;
                }
            }
            if (rab_Gr2_3_xls.Checked) {
                if (comb_gr2_2_task.SelectedIndex > 0) {
                    MessageBox.Show("只有在单转图像时才可以使用多任务");
                    return false;
                }
                if (ClsDataSplitPar.ClsExportTable.Count <= 0) {
                    MessageBox.Show("请先后台设置要导出的数据库表及字段!");
                    return false;
                }
                if (txt_gr3_1_xlsPath.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择xls模版文件!");
                    txt_gr3_1_xlsPath.Focus();
                    return false;
                }
            }
            if (rab_gr2_5_boxsn.Checked) {
                if (chk_gr2_5_juan.Checked) {
                    if (txt_gr2_5_box1.Text.Trim().Length <= 0 || txt_gr2_5_box2.Text.Trim().Length <= 0 ||
                        txt_gr2_5_juan.Text.Trim().Length <= 0) {
                        MessageBox.Show("请输入要转换的盒号及卷号!");
                        txt_gr2_5_box1.Focus();
                        return false;
                    }
                    else if (txt_gr2_5_box1.Text.Trim() != txt_gr2_5_box2.Text.Trim()) {
                        MessageBox.Show("单卷时起始盒号及终止盒号必须一致！");
                        txt_gr2_5_box1.Focus();
                        return false;
                    }
                    if (comb_gr2_2_task.SelectedIndex != 0) {
                        MessageBox.Show("转换单卷不能使用多任务模式!");
                        comb_gr2_2_task.Focus();
                        return false;
                    }
                }
                else if (lv_gr2_5_boxCount.Items.Count <= 0) {
                    MessageBox.Show("请先添加任务范围!");
                    txt_gr2_5_box1.Focus();
                    return false;
                }
            }
            else if (rab_gr2_5_col.Checked) {
                if (chk_gr2_5_juan.Checked) {
                    if (txt_gr2_5_juan.Text.Trim().Length <= 0) {
                        MessageBox.Show("请输入要转换的组合字段");
                        txt_gr2_5_juan.Focus();
                        return false;
                    }

                }
                else if (lv_gr2_5_boxCount.Items.Count <= 0) {
                    MessageBox.Show("请先添加任务范围!");
                    txt_gr2_5_box1.Focus();
                    return false;

                }

            }
            else if (lv_gr2_5_boxCount.Items.Count <= 0) {
                MessageBox.Show("请先添加任务范围!");
                txt_gr2_5_box1.Focus();
                return false;
            }
            if (!rab_Gr2_3_xls.Checked) {
                if (!chk_Gr2_4_jpg.Checked && !chk_gr2_4_tif.Checked && !chk_gr2_4_pdf.Checked &&
                    !chk_gr2_4_dou_pdf.Checked) {
                    MessageBox.Show("请选择要生成的图像格式!");
                    return false;
                }
                else {
                    ClsFrmInfoPar.FileFormat.Clear();
                    if (chk_Gr2_4_jpg.Checked)
                        ClsFrmInfoPar.FileFormat.Add("jpg");
                    if (chk_gr2_4_pdf.Checked)
                        ClsFrmInfoPar.FileFormat.Add("pdf");
                    if (chk_gr2_4_tif.Checked)
                        ClsFrmInfoPar.FileFormat.Add("tif");
                    for (int i = 0; i < ClsFrmInfoPar.FileFormat.Count; i++) {
                        string str = ClsFrmInfoPar.FileFormat[i];
                        ClsWritelog.Writeini(str, str);
                    }
                }

                if (rab_gr2_8_ziduAndmulu.Checked) {
                    if (!chk_gr2_8_conten.Checked && !chk_gr2_8_pages.Checked) {
                        MessageBox.Show("请选择文件名称中包含目录或页码的选项!");
                        return false;
                    }
                    else if (chk_gr2_8_conten.Checked) {
                        if (ClsDataSplitPar.ClsdirMl.Trim().Length <= 0) {
                            MessageBox.Show("未发现文件夹命名的目录字段!");
                            return false;
                        }
                    }
                }

                if (ClsDataSplitPar.ClsdirDirsn == 0) {
                    MessageBox.Show("文件夹命名规则不正确或请后台设置!");
                    return false;
                }
                if (ClsDataSplitPar.ClsFilesn == 0) {
                    MessageBox.Show("文件命名规则不正确或请后台设置!");
                    return false;
                }
                if (ClsDataSplitPar.ClsdirDirsn < dirsn()) {
                    MessageBox.Show("文件夹命名规则不正确或请后台设置!");
                    return false;
                }
                if (chk_gr2_4_dou_pdf.Checked) {
                    if (txt_gr2_6_ocrPath.Text.Trim().Length <= 0) {
                        MessageBox.Show("请指定ocr语言包路径！");
                        txt_gr2_6_ocrPath.Focus();
                        return false;
                    }
                    ClsWritelog.Writeini("Dpdf", "1");
                }
                if (!rab_gr2_7_wu.Checked) {
                    if (comb_gr2_7_weizhi.Text.Trim().Length <= 0) {
                        MessageBox.Show("请选择水印生成位置!");
                        comb_gr2_7_weizhi.Focus();
                        return false;
                    }
                    if (rab_gr2_7_wenzi.Checked) {
                        if (txt_gr2_7_wenzi.Text.Trim().Length <= 0) {
                            MessageBox.Show("生成水印文字不能为空!");
                            txt_gr2_7_wenzi.Focus();
                            return false;
                        }
                        if (txt_gr2_7_waterFontsize.Text.Trim().Length <= 0 || ClsFrmInfoPar.WaterFontColor == null || ClsFrmInfoPar.WaterFontColor.Trim().Length == 0) {
                            MessageBox.Show("请设置字号或字体颜色");
                            return false;
                        }

                        if (txt_gr2_7_waterwith.Text.Trim().Length <= 0 ||
                            txt_gr2_7_waterheight.Text.Trim().Length <= 0) {
                            MessageBox.Show("请设置水印宽度和高度!");
                            txt_gr2_7_waterwith.Focus();
                            return false;
                        }
                        if (txt_gr2_7_watertmd.Text.Trim().Length <= 0) {
                            MessageBox.Show("水印透明度不能为空!");
                            txt_gr2_7_watertmd.Focus();
                            return false;
                        }
                        else {
                            try {
                                int x = int.Parse(txt_gr2_7_watertmd.Text.Trim());
                                if (x < 0 || x > 255) {
                                    MessageBox.Show("透明度范围0-255");
                                    txt_gr2_7_watertmd.Focus();
                                    return false;
                                }
                            } catch {
                                MessageBox.Show("透明度范围0-255");
                                txt_gr2_7_watertmd.Focus();
                                return false;
                            }
                        }
                        ClsWritelog.Writeini("waterstr", txt_gr2_7_wenzi.Text.Trim());
                        ClsWritelog.Writeini("waterfontsize", txt_gr2_7_waterFontsize.Text);
                        ClsWritelog.Writeini("watercolor", ClsFrmInfoPar.WaterFontColor);
                        ClsWritelog.Writeini("watertmd", txt_gr2_7_watertmd.Text.Trim());
                        ClsWritelog.Writeini("waterwith", txt_gr2_7_waterwith.Text.Trim());
                        ClsWritelog.Writeini("waterheiht", txt_gr2_7_waterheight.Text.Trim());
                    }
                    if (rab_gr2_7_img.Checked) {
                        if (txt_gr2_7_img.Text.Trim().Length <= 0) {
                            MessageBox.Show("生成水印图像路径不能为空!");
                            txt_gr2_7_img.Focus();
                            return false;
                        }
                        if (txt_gr2_7_waterwith.Text.Trim().Length <= 0 ||
                            txt_gr2_7_waterheight.Text.Trim().Length <= 0) {
                            MessageBox.Show("请设置水印宽度和高度!");
                            txt_gr2_7_waterwith.Focus();
                            return false;
                        }

                        if (txt_gr2_7_watertmd.Text.Trim().Length <= 0) {
                            MessageBox.Show("水印透明度不能为空!");
                            txt_gr2_7_watertmd.Focus();
                            return false;
                        }
                        ClsWritelog.Writeini("waterstrimg", txt_gr2_7_wenzi.Text.Trim());
                        ClsWritelog.Writeini("watertmd", txt_gr2_7_watertmd.Text.Trim());
                        ClsWritelog.Writeini("waterwith", txt_gr2_7_waterwith.Text.Trim());
                        ClsWritelog.Writeini("waterheiht", txt_gr2_7_waterheight.Text.Trim());
                    }
                    ClsWritelog.Writeini("waterwz", comb_gr2_7_weizhi.SelectedIndex.ToString());
                }
                if (rab_gr2_4_duli.Checked) {
                    if (chk_Gr2_4_jpg.Checked) {
                        MessageBox.Show("单独文件不能使用Jpg格式");
                        return false;
                    }
                    if (!rab_gr2_8_ziduan.Checked) {
                        MessageBox.Show("单独文件时：文件夹名只能为<字段格式>");
                        return false;
                    }

                    if (rab_gr2_9_file_1.Checked) {
                        MessageBox.Show("单独文件时：文件名不能使用<每个文件夹为1>");
                        return false;
                    }
                }
                else if (rab_gr2_4_duo.Checked) {
                    if (chk_Gr2_4_jpg.Checked) {
                        MessageBox.Show("多页文件不能使用Jpg格式");
                        return false;
                    }
                }

                if (rab_gr2_9_file_1.Checked) {
                    if (rab_gr2_8_ziduan.Checked) {
                        MessageBox.Show("文件名此规则必须在文件夹命名规则包含目录才可用!");
                        return false;
                    }
                    if (ClsDataSplitPar.ClsdirMl.Trim().Length <= 0 && ClsDataSplitPar.ClsdirMlpage.Trim().Length <= 0) {
                        MessageBox.Show("未发现后台目录相关设置数据");
                        return false;
                    }

                    if (com_gr2_9_file_gz.Text.Trim().Length <= 0) {
                        MessageBox.Show("请选择文件名的生成规则!");
                        com_gr2_9_file_gz.Focus();
                        return false;
                    }
                }
                if (rab_gr2_9_juan_1.Checked) {
                    if (ClsDataSplitPar.ClsdirMlpage.Trim().Length <= 0) {
                        MessageBox.Show("请先在后台设置页码字段！");
                        return false;
                    }
                }
                if (rab_gr2_9_file_ziduan.Checked) {
                    if (ClsDataSplitPar.ClsFilesn != 3) {
                        MessageBox.Show("此选请先设置后台！");
                        return false;
                    }
                    if (!rab_gr2_8_ziduan.Checked || !rab_gr2_4_duli.Checked) {
                        MessageBox.Show("文件名为字段时：文件夹名只能为<字段格式>，图像生成格式只能是<单独文件>");
                        return false;
                    }

                    if (ClsDataSplitPar.ClsFileNamecol.Trim().Length <= 0) {
                        MessageBox.Show("未发现字段相关信息，请后台设置!");
                        return false;
                    }
                }
                if (rab_gr3_1_imgPath.Checked) {
                    if (txt_gr3_1_imgPath.Text.Trim().Length <= 0) {
                        MessageBox.Show("图像源路径不能为空,或改为Ftp传输!");
                        txt_gr3_1_imgPath.Focus();
                        return false;
                    }
                }
            }
            if (txt_gr3_1_splitPath.Text.Trim().Length <= 0) {
                MessageBox.Show("请指定转换后的路径!");
                txt_gr3_1_splitPath.Focus();
                return false;
            }
            GetParinfo();
            return true;
        }

        private void GetParinfo()
        {
            ClsFrmInfoPar.OneJuan = 0;
            V_HouseSet v_house = combHouseid.SelectedItem as V_HouseSet;
            ClsFrmInfoPar.Houseid = v_house.HouseID;
            ClsFrmInfoPar.Filenamegz = com_gr2_9_file_gz.SelectedIndex;
            if (rab_gr2_1_Zengliang.Checked)
                ClsFrmInfoPar.ConverMode = 1;
            if (rab_gr2_1_Newzhuanhuan.Checked)
                ClsFrmInfoPar.ConverMode = 2;
            if (rab_Gr2_3_tb.Checked)
                ClsFrmInfoPar.ExportType = 1;
            else if (rab_Gr2_3_img.Checked)
                ClsFrmInfoPar.ExportType = 2;
            else if (rab_Gr2_3_xls.Checked)
                ClsFrmInfoPar.ExportType = 3;
            if (chk_gr2_5_juan.Checked)
                ClsFrmInfoPar.OneJuan = 1;
            if (rab_gr2_8_ziduan.Checked)
                ClsFrmInfoPar.DirNamesn = 1;
            else if (rab_gr2_8_mulu.Checked)
                ClsFrmInfoPar.DirNamesn = 2;
            else if (rab_gr2_8_ziduAndmulu.Checked) {
                ClsFrmInfoPar.DirNamesn = 3;
                if (chk_gr2_8_conten.Checked)
                    ClsFrmInfoPar.DirNamesnconten = 1;
                if (chk_gr2_8_pages.Checked)
                    ClsFrmInfoPar.dirNamesnpages = 1;
            }
            if (rab_gr2_4_dan.Checked)
                ClsFrmInfoPar.FileFomat = 1;
            else if (rab_gr2_4_duo.Checked)
                ClsFrmInfoPar.FileFomat = 2;
            else if (rab_gr2_4_duli.Checked)
                ClsFrmInfoPar.FileFomat = 3;
            if (chk_gr2_4_dou_pdf.Checked) {
                ClsFrmInfoPar.Doublecor = 1;
                ClsFrmInfoPar.OcrPath = txt_gr2_6_ocrPath.Text.Trim();
                ClsFrmInfoPar.Ocrpdf = chk_gr2_6_ocrpdf.Checked;
                if (chk_gr2_6_ocrpdf.Checked)
                    ClsWritelog.Writeini("ppdf", "ppdf");
            }
            else ClsFrmInfoPar.Doublecor = 0;
            if (rab_gr2_7_wu.Checked)
                ClsFrmInfoPar.Watermark = 0;
            else if (rab_gr2_7_wenzi.Checked) {
                ClsFrmInfoPar.Watermark = 1;
                ClsFrmInfoPar.WaterStrImg = txt_gr2_7_wenzi.Text.Trim();
                ClsFrmInfoPar.Waterwith = Convert.ToInt32(txt_gr2_7_waterwith.Text.Trim());
                ClsFrmInfoPar.Waterheiht = Convert.ToInt32(txt_gr2_7_waterheight.Text.Trim());
                ClsFrmInfoPar.WaterFontsize = Convert.ToInt32(txt_gr2_7_waterFontsize.Text.Trim());
                ClsFrmInfoPar.Watertmd = Convert.ToInt32(txt_gr2_7_watertmd.Text.Trim());
                ClsFrmInfoPar.Waterwz = comb_gr2_7_weizhi.SelectedIndex + 1;
            }
            else if (rab_gr2_7_img.Checked) {
                ClsFrmInfoPar.Watermark = 2;
                ClsFrmInfoPar.WaterStrImg = txt_gr2_7_img.Text.Trim();
                ClsFrmInfoPar.Waterwith = Convert.ToInt32(txt_gr2_7_waterwith.Text.Trim());
                ClsFrmInfoPar.Waterheiht = Convert.ToInt32(txt_gr2_7_waterheight.Text.Trim());
                ClsFrmInfoPar.Watertmd = Convert.ToInt32(txt_gr2_7_watertmd.Text.Trim());
                ClsFrmInfoPar.Waterwz = comb_gr2_7_weizhi.SelectedIndex + 1;
            }
            if (rab_gr2_9_file_1.Checked)
                ClsFrmInfoPar.FileNamesn = 1;
            else if (rab_gr2_9_juan_1.Checked)
                ClsFrmInfoPar.FileNamesn = 2;
            else if (rab_gr2_9_file_ziduan.Checked)
                ClsFrmInfoPar.FileNamesn = 3;
            if (rab_gr3_1_ftp.Checked)
                ClsFrmInfoPar.Ftp = 1;
            else if (rab_gr3_1_imgPath.Checked)
                ClsFrmInfoPar.Ftp = 2;
            ClsFrmInfoPar.Taskxc = Convert.ToInt32(comb_gr2_2_task.Text.Trim());
            ClsFrmInfoPar.YimgPath = txt_gr3_1_imgPath.Text.Trim();
            ClsFrmInfoPar.MimgPath = txt_gr3_1_splitPath.Text.Trim();
            ClsFrmInfoPar.XlsPath = txt_gr3_1_xlsPath.Text.Trim();

            if (rab_gr2_5_boxsn.Checked)
                ClsFrmInfoPar.Task = 1;
            else ClsFrmInfoPar.Task = 2;

        }

        private void chk_gr2_5_juan_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_gr2_5_juan.Checked)
                txt_gr2_5_juan.Enabled = true;
            else txt_gr2_5_juan.Enabled = false;
        }
        private void but_gr2_5_add_Click(object sender, EventArgs e)
        {
            if (chk_gr2_5_juan.Checked) {
                MessageBox.Show("单卷无需要添加!");
                return;
            }
            Addboxsn();
        }

        private void lab_gr2_7_font_color_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                ClsFrmInfoPar.WaterFontColor =
                    colorDialog.Color.R + ";" + colorDialog.Color.G + ";" + colorDialog.Color.B;
                lab_gr2_7_font_color.BackColor = colorDialog.Color;
                return;
            }
            ClsFrmInfoPar.WaterFontColor = "";
        }

        private void but_gr2_5_del_Click(object sender, EventArgs e)
        {
            Delboxsn();
        }

        #endregion

        #region init
        private void ListBshowInfo(int xc, string boxsn, string archno, string str)
        {
            Application.DoEvents();
            string log = "线程：" + xc.ToString() + "--> 盒号:" + boxsn + "-->卷号:" + archno + " --> " + str;
            lock (log) {
                listB_gr3_2_log.Invoke(new Action(() =>
                   {
                       listB_gr3_2_log.BeginUpdate();
                       listB_gr3_2_log.Items.Add(log);
                       listB_gr3_2_log.SelectedIndex = listB_gr3_2_log.Items.Count - 1;
                       listB_gr3_2_log.EndUpdate();
                   }));
            }
        }

        private void Init(int xc, string box, string arno, DataTable dtfw)
        {
            DataTable dtArchNo = null;
            string str = "";
            Thread.Sleep(200);
            if (ClsFrmInfoPar.Task == 1) {
                if (ClsFrmInfoPar.OneJuan == 0)
                    dtArchNo = ClsOperate.SelectSql(box);
                else
                    dtArchNo = ClsOperate.SelectSql(box, arno);
                if (dtArchNo == null || dtArchNo.Rows.Count <= 0) {
                    str = "错误：未找到已质检盒号信息或已拆分完成 -->盒号:" + box;
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        ListBshowInfo(xc, "0", "0", "警告,错误线程退出");
                        return;
                    }
                }
            }
            else {
                if (ClsFrmInfoPar.OneJuan == 0)
                    dtArchNo = dtfw.Copy();
                else
                    dtArchNo = ClsOperate.SelectSqlcol(box);
                if (dtArchNo == null || dtArchNo.Rows.Count <= 0) {
                    str = "错误：未找到已质检盒号信息或已拆分 -->字段号:" + box;
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        ListBshowInfo(xc, "0", "0", "警告,错误线程退出");
                        return;
                    }
                }
            }

            if (ClsFrmInfoPar.Watermark > 0)
                ClsOperate.Setwaterpar();
            for (int i = 0; i < dtArchNo.Rows.Count; i++) {
                if (ClsFrmInfoPar.StopTag == 2) {
                    ListBshowInfo(xc, "0", "0", "已停止线程退出");
                    return;
                }
                string archid = dtArchNo.Rows[i][0].ToString();
                string boxsn = dtArchNo.Rows[i][1].ToString();
                string archno = dtArchNo.Rows[i][2].ToString();
                string pages = dtArchNo.Rows[i][3].ToString();
                string imgfile = dtArchNo.Rows[i][4].ToString();
                ListBshowInfo(xc, boxsn, archno, "正在查询信息");
                if (Convert.ToInt32(pages) <= 0 || imgfile.Trim().Length <= 0) {
                    lock (ClsFrmInfoPar.Filelock) {
                        str = "错误：此卷页码不正确或数据库中文件名不正确  -->盒号:" + boxsn + " -->卷号" + archno;
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                    continue;
                }
                if (ClsFrmInfoPar.ExportType == 3) {
                    ListBshowInfo(xc, boxsn, archno, "正在准备写入Xls信息");
                    str = ClsOperate.XlsPath();
                    if (str.IndexOf("错误") >= 0) {
                        lock (ClsFrmInfoPar.Filelock) {
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        }
                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                        continue;
                    }
                    str = ClsOperate.GetAnjuanInfo(archid, str);
                    if (str.IndexOf("错误") >= 0) {
                        lock (ClsFrmInfoPar.Filelock) {
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        }
                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                        continue;
                    }
                    else {
                        ListBshowInfo(xc, boxsn, archno, "单导xls信息完成线程退出");
                        continue;
                    }
                }
                else if (ClsFrmInfoPar.ExportType == 1) {
                    ListBshowInfo(xc, boxsn, archno, "正在写入Xls信息");
                    str = ClsOperate.GetAnjuanInfo(archid, ClsOperate.XlsPath());
                    if (str.IndexOf("错误") >= 0) {
                        lock (ClsFrmInfoPar.Filelock) {
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        }
                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                        continue;
                    }
                }
                ListBshowInfo(xc, boxsn, archno, "正在下载图像");
                str = ClsOperate.DownFile(ClsFrmInfoPar.YimgPath, imgfile);
                string Downfile = str;
                if (str.IndexOf("错误") >= 0) {
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                    continue;
                }
                if (Himg.GetFilePage(str).ToString() != pages) {
                    str = "图像页码和登记页码不一致!";
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                    continue;
                }
                ListBshowInfo(xc, boxsn, archno, "准备进行数据转换");
                List<string> lsinfopdf = new List<string>();
                int erro = 0;
                try {
                    for (int f = 0; f < ClsFrmInfoPar.FileFormat.Count; f++) {
                        string fs = ClsFrmInfoPar.FileFormat[f];
                        string dirname = "";
                        string filename = "";
                        string dpdfdir = "";
                        //文件名为字段 
                        if (ClsFrmInfoPar.FileNamesn == 3) {
                            string dir = ClsOperate.GetDirColName(archid);
                            if (dir.IndexOf("错误") >= 0) {
                                lock (ClsFrmInfoPar.Filelock) {
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, dir);
                                }
                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                erro = 1;
                                break;
                            }
                            string file = ClsOperate.GetFileName(archid);
                            if (file.IndexOf("错误") >= 0) {
                                lock (ClsFrmInfoPar.Filelock) {
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, file);
                                }
                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                erro = 1;
                                break;
                            }
                            dirname = Path.Combine(ClsFrmInfoPar.MimgPath, ClsFrmInfoPar.FileFormat[f], dir);
                            filename = Path.Combine(dirname, file + "." + fs);
                            if (!Directory.Exists(dirname))
                                Directory.CreateDirectory(dirname);
                            if (File.Exists(filename)) {
                                if (ClsFrmInfoPar.ConverMode == 2)
                                    File.Delete(filename);
                                else {
                                    ListBshowInfo(xc, boxsn, archno, "文件存在线程正常退出");
                                    erro = 1;
                                    break;
                                }
                            }
                            ListBshowInfo(xc, boxsn, archno, "正在转换格式为" + fs + "的单独文件");
                            if (fs == "tif") {
                                if (ClsFrmInfoPar.Watermark == 0) {
                                    try {
                                        File.Copy(Downfile, filename);
                                    } catch (Exception ex) {
                                        str = ex.ToString();
                                        if (str.IndexOf("错误") >= 0) {
                                            lock (ClsFrmInfoPar.Filelock) {
                                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                            }
                                        }
                                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                        erro = 1;
                                        break;
                                    }
                                }
                                else {
                                    str = Himg._SplitImg(Downfile, filename, fs);
                                    if (str.IndexOf("错误") >= 0) {
                                        lock (ClsFrmInfoPar.Filelock) {
                                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                        }
                                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                        erro = 1;
                                        break;
                                    }
                                }

                            }
                            else {
                                str = Himg._SplitImg(Downfile, filename, fs);
                                if (str.IndexOf("错误") >= 0) {
                                    lock (ClsFrmInfoPar.Filelock) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    erro = 1;
                                    break;
                                }
                            }
                            if (ClsFrmInfoPar.Doublecor == 1) {
                                ListBshowInfo(xc, boxsn, archno, "正在Ocr识别文件");
                                str = Himg._AutoPdfOcr2(Downfile, filename, ClsFrmInfoPar.OcrPath, 1);
                                if (str.IndexOf("错误") >= 0) {
                                    lock (ClsFrmInfoPar.Filelock) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    erro = 1;
                                    break;
                                }
                            }
                        }
                        //文件为每案卷或每文件夹 起始1   测试完成 
                        else if (ClsFrmInfoPar.FileNamesn == 2 || ClsFrmInfoPar.FileNamesn == 1) {

                            if (ClsFrmInfoPar.DirNamesn != 2) {
                                string dir = ClsOperate.GetDirColName(archid);
                                if (dir.IndexOf("错误") >= 0) {
                                    lock (ClsFrmInfoPar.Filelock) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, dir);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    erro = 1;
                                    break;
                                }
                                dirname = Path.Combine(ClsFrmInfoPar.MimgPath, ClsFrmInfoPar.FileFormat[f], dir);
                                dpdfdir = Path.Combine(ClsFrmInfoPar.MimgPath, "Dpdf", dir);
                            }
                            else {
                                dirname = Path.Combine(ClsFrmInfoPar.MimgPath, ClsFrmInfoPar.FileFormat[f], boxsn, archno);
                                dpdfdir = Path.Combine(ClsFrmInfoPar.MimgPath, "Dpdf", boxsn, archno);
                            }
                            if (!Directory.Exists(dirname))
                                Directory.CreateDirectory(dirname);
                            if (ClsFrmInfoPar.Doublecor == 1) {
                                if (!Directory.Exists(dpdfdir))
                                    Directory.CreateDirectory(dpdfdir);
                            }
                            DataTable dirtTable = ClsOperate.GetdirmlInfo(archid);
                            if (dirtTable == null || dirtTable.Rows.Count <= 0) {
                                str = "未找到文件夹命名规则目录信息  -->盒号:" + boxsn + " -->卷号" + archno;
                                lock (ClsFrmInfoPar.Filelock) {
                                    lock (ClsFrmInfoPar.Filelock) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    erro = 1;
                                    break;
                                }
                            }
                            for (int d = 0; d < dirtTable.Rows.Count; d++) {
                                string doublepdf = "";
                                string dirnamenew = "";
                                lsinfopdf.Clear();
                                int p1 = 0;
                                int p2 = 0;
                                string ml = "";
                                string pzer = "";
                                int p = 0;
                                if (ClsDataSplitPar.ClsdirMl.Trim().Length > 0 && ClsFrmInfoPar.DirNamesnconten == 1) {
                                    ml = dirtTable.Rows[d][0].ToString().Trim();
                                    p = 1;
                                }

                                p1 = Convert.ToInt32(dirtTable.Rows[d][p].ToString());
                                try {
                                    p2 = Convert.ToInt32(dirtTable.Rows[d + 1][p].ToString());
                                    p2 -= 1;
                                } catch {
                                    p2 = Convert.ToInt32(pages);
                                }

                                if (ClsFrmInfoPar.dirNamesnpages == 1) {
                                    if (ClsDataSplitPar.ClsdirPageZero == 0)
                                        pzer = p1.ToString();
                                    else
                                        pzer = p1.ToString().PadLeft(ClsDataSplitPar.ClsdirPageZero, '0');
                                }
                                //每卷为1 已测完成
                                if (ClsFrmInfoPar.FileNamesn == 2) {
                                    //为多页时 已测完成
                                    if (ClsFrmInfoPar.FileFomat == 2) {
                                        if (ClsFrmInfoPar.DirNamesn == 1) {
                                            dirnamenew = dirname;
                                            doublepdf = dpdfdir;
                                        }
                                        else if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            dirnamenew = Path.Combine(dirname, ml, pzer);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                            doublepdf = Path.Combine(dpdfdir, ml, pzer);
                                            if (ClsFrmInfoPar.Doublecor == 1) {
                                                if (!Directory.Exists(doublepdf))
                                                    Directory.CreateDirectory(doublepdf);
                                            }
                                        }
                                        int pags = p2 - p1;
                                        if (pags == 0)
                                            pags = 1;
                                        filename = Path.Combine(dirnamenew,
                                            p1.ToString() + "-" + pags.ToString() + "." + fs);
                                        if (File.Exists(filename)) {
                                            if (ClsFrmInfoPar.ConverMode == 2)
                                                File.Delete(filename);
                                            else {
                                                ListBshowInfo(xc, boxsn, archno, "文件线程正常退出");
                                                erro = 1;
                                                break;
                                            }
                                        }

                                        ListBshowInfo(xc, boxsn, archno,
                                          "正在进行" + fs + "数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                        lsinfopdf = Himg._SplitImgls(Downfile, filename, p1, p2, fs);
                                        if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                            ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                            erro = 1;
                                            break;
                                        }
                                        if (ClsFrmInfoPar.Doublecor == 1) {
                                            ListBshowInfo(xc, boxsn, archno,
                                                "正在进行Ocr识别页码：" + p1.ToString() + "-" + p2.ToString());
                                            lsinfopdf = Himg._SplitImgls(Downfile, filename, p1, p2, fs);
                                            str = ClsOperate.OcrPdf(Himg, lsinfopdf, doublepdf, xc, boxsn, archno);
                                            if (str.IndexOf("错误") >= 0) {
                                                lock (ClsFrmInfoPar.Filelock) {
                                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                        str + "盒号:" + box + " --> 卷号:" + archno);
                                                }
                                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                erro = 1;
                                                break;
                                            }
                                        }
                                    }
                                    //为单页时  已测完成
                                    else if (ClsFrmInfoPar.FileFomat == 1) {
                                        //文件夹为目录 已测完成
                                        if (ClsFrmInfoPar.DirNamesn == 1) {
                                            dirnamenew = dirname;
                                            doublepdf = dpdfdir;
                                        }
                                        else if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            dirnamenew = Path.Combine(dirname, ml, pzer);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                            doublepdf = Path.Combine(dpdfdir, ml, pzer);
                                            if (ClsFrmInfoPar.Doublecor == 1) {
                                                if (!Directory.Exists(doublepdf))
                                                    Directory.CreateDirectory(doublepdf);
                                            }
                                        }
                                        ListBshowInfo(xc, boxsn, archno,
                                                "正在进行" + fs + "数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                        lsinfopdf = Himg._SplitImgls(Downfile, dirnamenew, p1, p2,
                                            ClsDataSplitPar.ClsFileNameQian, ClsDataSplitPar.ClsFileNameHou,
                                            ClsDataSplitPar.ClsFileNmaecd, ClsFrmInfoPar.ConverMode, fs, 0);
                                        if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                            ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                            erro = 1;
                                            break;
                                        }
                                        if (ClsFrmInfoPar.Doublecor == 1) {
                                            ListBshowInfo(xc, boxsn, archno,
                                                "正在进行Ocr识别页码：" + p1.ToString() + "-" + p2.ToString());
                                            str = ClsOperate.OcrPdf(Himg, lsinfopdf, doublepdf, xc, boxsn, archno);
                                            if (str.IndexOf("错误") >= 0) {
                                                lock (ClsFrmInfoPar.Filelock) {
                                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                        str + "盒号:" + box + " --> 卷号:" + archno);
                                                }
                                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                erro = 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                                //每个目录为  已测完成
                                else if (ClsFrmInfoPar.FileNamesn == 1) {
                                    //为多页时  正则
                                    if (ClsFrmInfoPar.FileFomat == 2) {
                                        //文件夹为目录  已测完成 
                                        if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            dirnamenew = Path.Combine(dirname, ml, pzer);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                            doublepdf = Path.Combine(dpdfdir, ml, pzer);
                                            if (ClsFrmInfoPar.Doublecor == 1) {
                                                if (!Directory.Exists(doublepdf))
                                                    Directory.CreateDirectory(doublepdf);
                                            }
                                            int pags = p2 - p1;
                                            if (pags == 0)
                                                pags = 1;
                                            if (ClsFrmInfoPar.Filenamegz == 0)
                                                filename = Path.Combine(dirnamenew,
                                                    "1" + "-" + pags.ToString() + "." + fs);
                                            else if (ClsFrmInfoPar.Filenamegz == 1)
                                                filename = Path.Combine(dirnamenew,
                                                    p1.ToString() + "-" + pags.ToString() + "." + fs);
                                            else if (ClsFrmInfoPar.Filenamegz == 2)
                                                filename = Path.Combine(dirnamenew,
                                                    p1.ToString() + "." + fs);
                                            if (File.Exists(filename)) {
                                                if (ClsFrmInfoPar.ConverMode == 2)
                                                    File.Delete(filename);
                                                else {
                                                    ListBshowInfo(xc, boxsn, archno, "文件存在线程正常退出");
                                                    erro = 1;
                                                    break;
                                                }
                                            }
                                            ListBshowInfo(xc, boxsn, archno,
                                                "正在进行" + fs + "数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                            lsinfopdf = Himg._SplitImgls(Downfile, filename, p1, p2, fs);
                                            if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                erro = 1;
                                                break;
                                            }
                                            if (ClsFrmInfoPar.Doublecor == 1) {
                                                ListBshowInfo(xc, boxsn, archno,
                                                    "正在进行Ocr识别页码：" + p1.ToString() + "-" + p2.ToString());
                                                str = ClsOperate.OcrPdf(Himg, lsinfopdf, doublepdf, xc, boxsn, archno);
                                                if (str.IndexOf("错误") >= 0) {
                                                    lock (ClsFrmInfoPar.Filelock) {
                                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                            str + "盒号:" + box + " --> 卷号:" + archno);
                                                    }
                                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                    erro = 1;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    //为单页时   已测完成
                                    else if (ClsFrmInfoPar.FileFomat == 1) {
                                        //文件夹为目录   已测完成
                                        if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            dirnamenew = Path.Combine(dirname, ml, pzer);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                            doublepdf = Path.Combine(dpdfdir, ml, pzer);
                                            if (ClsFrmInfoPar.Doublecor == 1) {
                                                if (!Directory.Exists(doublepdf))
                                                    Directory.CreateDirectory(doublepdf);
                                            }
                                            ListBshowInfo(xc, boxsn, archno,
                                                "正在进行" + fs + "数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                            lsinfopdf = Himg._SplitImgls(Downfile, dirnamenew, p1, p2,
                                            ClsDataSplitPar.ClsFileNameQian, ClsDataSplitPar.ClsFileNameHou,
                                            ClsDataSplitPar.ClsFileNmaecd, ClsFrmInfoPar.ConverMode, fs, ClsFrmInfoPar.Filenamegz);
                                            if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                erro = 1;
                                                break;
                                            }
                                            if (ClsFrmInfoPar.Doublecor == 1) {
                                                ListBshowInfo(xc, boxsn, archno,
                                                    "正在进行Ocr识别页码：" + p1.ToString() + "-" + p2.ToString());
                                                str = ClsOperate.OcrPdf(Himg, lsinfopdf, doublepdf, xc, boxsn, archno);
                                                if (str.IndexOf("错误") >= 0) {
                                                    lock (ClsFrmInfoPar.Filelock) {
                                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                            str + "盒号:" + box + " --> 卷号:" + archno);
                                                    }
                                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                    erro = 1;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }

                    }
                    if (erro == 0)
                        Common.DataSplitUpdate(archid);
                } catch (Exception e) {
                    str = e.ToString();
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str + "盒号:" + box + " --> 卷号:" + archno);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                } finally {
                    try {
                        if (ClsFrmInfoPar.Ftp == 1) {
                            File.Delete(Downfile);
                            Directory.Delete(Path.GetDirectoryName(Downfile));
                        }
                    } catch { }
                    ListBshowInfo(xc, boxsn, archno, "线程退出");
                }

            }
        }

        private void TxtEnd(bool x)
        {
            if (x) {
                this.BeginInvoke(new Action(() =>
                    {
                        gr2.Enabled = true;
                        butStart.Enabled = true;
                        butLog.Enabled = true;
                        labinfo.Visible = false;
                        ClsFrmInfoPar.StopTag = 0;
                    }
                ));
                return;
            }
            this.BeginInvoke(new Action(() =>
                {
                    gr2.Enabled = false;
                    butStart.Enabled = false;
                    butLog.Enabled = false;
                    ClsFrmInfoPar.StopTag = 1;
                }
            ));
        }


        private void StartTaskxc()
        {
            AutoResetEvent[] waitEnents = new AutoResetEvent[ClsFrmInfoPar.Taskxc];
            for (int i = 1; i <= ClsFrmInfoPar.Taskxc; i++) {

                waitEnents[i - 1] = new AutoResetEvent(false);
                StatTask(i, waitEnents[i - 1]);
            }
            AutoResetEvent.WaitAll(waitEnents);
            TxtEnd(true);
        }

        private void StatTask(int id, AutoResetEvent obj)
        {
            if (ClsFrmInfoPar.Task == 1) {
                lock (ClsFrmInfoPar.TaskBoxCount) {
                    if (ClsFrmInfoPar.TaskBoxCount.Count > 0) {
                        string b = ClsFrmInfoPar.TaskBoxCount[0];
                        ClsFrmInfoPar.TaskBoxCount.RemoveAt(0);
                        ThreadPool.QueueUserWorkItem(h =>
                        {
                            Init(id
                                , b, null, null);
                            Thread.Sleep(1000);
                            StatTask(id, obj);
                        });
                    }
                    else
                        obj.Set();
                }
            }
            else {
                lock (ClsFrmInfoPar.TaskBoxCountcol) {
                    if (ClsFrmInfoPar.TaskBoxCountcol.Count > 0) {
                        DataRow dr = ClsFrmInfoPar.TaskBoxCountcol[0];
                        ClsFrmInfoPar.TaskBoxCountcol.RemoveAt(0);
                        DataTable dt = dr.Table.Clone();
                        ThreadPool.QueueUserWorkItem(h =>
                        {
                            dt.Rows.Add(dr.ItemArray);
                            int x = dt.Rows.Count;
                            Init(id
                                , null, null, dt);
                            Thread.Sleep(1000);
                            StatTask(id, obj);
                        });
                    }
                    else
                        obj.Set();
                }
            }

        }


        private void Stattask()
        {
            Task t = null;
            if (ClsFrmInfoPar.Task == 1)
                t = Task.Run(() => { Init(1, txt_gr2_5_box1.Text.Trim(), txt_gr2_5_juan.Text.Trim(), null); });
            else
                t = Task.Run(() => { Init(1, txt_gr2_5_juan.Text, null, null); });
            Task.WaitAll(t);
            TxtEnd(true);
        }

        #endregion

        #region FrmInfoPar

        private void FrmSplit_Shown(object sender, EventArgs e)
        {
            Himg = new Hljsimage();
            CombAddinfo();
            ClsInIPar.Getgzinfo();
            Getparinfo();
        }

        private void Getparinfo()
        {
            try {
                List<string> lskey = new List<string>();
                List<string> lsval = new List<string>();
                lskey = ClsWritelog.Readinikey();
                lsval = ClsWritelog.Readinivalue();
                for (int i = 0; i < lskey.Count; i++) {
                    string strkey = lskey[i];
                    string strval = lsval[i];
                    if (strkey.Trim().Length <= 0)
                        continue;
                    if (strkey == "dirname") {
                        if (strval == "1")
                            rab_gr2_8_ziduan.Checked = true;
                        else if (strval == "2")
                            rab_gr2_8_mulu.Checked = true;
                        else rab_gr2_8_ziduAndmulu.Checked = true;
                    }

                    if (strkey == "filename") {
                        if (strval == "1")
                            rab_gr2_9_file_1.Checked = true;
                        else if (strval == "2")
                            rab_gr2_9_juan_1.Checked = true;
                        else rab_gr2_9_file_ziduan.Checked = true;
                    }

                    if (strkey == "convfile") {
                        if (strval == "1")
                            rab_gr2_4_dan.Checked = true;
                        else if (strval == "2")
                            rab_gr2_4_duo.Checked = true;
                        else rab_gr2_4_duli.Checked = true;
                    }

                    if (strkey == "water") {
                        if (strval == "0")
                            rab_gr2_7_wu.Checked = true;
                        else if (strval == "1")
                            rab_gr2_7_wenzi.Checked = true;
                        else rab_gr2_7_img.Checked = true;
                    }

                    if (strkey == "ftp") {
                        if (strval == "1")
                            rab_gr3_1_ftp.Checked = true;
                        else rab_gr3_1_imgPath.Checked = true;
                    }

                    if (strkey == "jpg")
                        chk_Gr2_4_jpg.Checked = true;
                    if (strkey == "pdf")
                        chk_gr2_4_pdf.Checked = true;
                    if (strkey == "tif")
                        chk_gr2_4_tif.Checked = true;
                    if (strkey == "dpdf")
                        chk_gr2_4_dou_pdf.Checked = true;
                    if (strkey == "ppdf")
                        chk_gr2_6_ocrpdf.Checked = true;
                    if (strkey == "waterstr")
                        txt_gr2_7_wenzi.Text = strval;
                    if (strkey == "waterfontsize")
                        txt_gr2_7_waterFontsize.Text = strval;
                    if (strkey == "watertmd")
                        txt_gr2_7_watertmd.Text = strval;
                    if (strkey == "waterwith")
                        txt_gr2_7_waterwith.Text = strval;
                    if (strkey == "waterheiht")
                        txt_gr2_7_waterheight.Text = strval;
                    if (strkey == "watercolor")
                        ClsFrmInfoPar.WaterFontColor = strval;
                    if (strkey == "waterstrimg")
                        txt_gr2_7_img.Text = strval;
                    if (strkey == "convpath")
                        txt_gr3_1_splitPath.Text = strval;
                    if (strkey == "ftppath")
                        txt_gr3_1_imgPath.Text = strval;
                    if (strkey == "convxls")
                        txt_gr3_1_xlsPath.Text = strval;
                }
            } catch {
            } finally {
                com_gr2_9_file_gz.Items.Clear();
                com_gr2_9_file_gz.Enabled = true;
                if (rab_gr2_4_dan.Checked) {
                    if (rab_gr2_9_file_1.Checked) {
                        com_gr2_9_file_gz.Items.Add("1,2,3;4,5,6");
                        com_gr2_9_file_gz.Items.Add("1,2,3;1,2,3");
                    }

                }
                else if (rab_gr2_4_duo.Checked) {
                    if (rab_gr2_9_file_1.Checked) {
                        com_gr2_9_file_gz.Items.Add("1-3;1-4");
                        com_gr2_9_file_gz.Items.Add("1-3;4-6");
                        com_gr2_9_file_gz.Items.Add("1,4,7");
                    }

                }
                else
                    com_gr2_9_file_gz.Enabled = false;
            }
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            string file = Path.Combine(ClsFrmInfoPar.LogPath, "log.txt");
            if (File.Exists(file)) {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(file);
            }
            else MessageBox.Show("日志文件不存在!");

        }
        private void but_gr3_1_Xls_Click(object sender, EventArgs e)
        {
            string str = "";
            if (FdigXls.ShowDialog() == DialogResult.OK) {
                str = FdigXls.FileName;
                ClsWritelog.Writeini("convxls", str);
            }
            else
                str = "";
            txt_gr3_1_xlsPath.Text = str;
            ClsFrmInfoPar.XlsPath = str;
        }

        private void but_gr3_1_ImgToPath_Click(object sender, EventArgs e)
        {
            string str = "";
            if (fBdigImgPath.ShowDialog() == DialogResult.OK) {
                str = fBdigImgPath.SelectedPath;
                ClsWritelog.Writeini("convpath", str);
            }
            else
                str = "";
            txt_gr3_1_splitPath.Text = str;
            ClsFrmInfoPar.MimgPath = str;
        }

        private void but_gr3_1_ImgPath_Click(object sender, EventArgs e)
        {
            string str = "";
            if (fBdigImgPath.ShowDialog() == DialogResult.OK) {
                str = fBdigImgPath.SelectedPath;
                ClsWritelog.Writeini("ftppath", str);
            }
            else
                str = "";
            txt_gr3_1_imgPath.Text = str;
            ClsFrmInfoPar.YimgPath = str;
        }

        private void but_gr2_6_ocrpath_Click(object sender, EventArgs e)
        {
            if (fBdigImgPath.ShowDialog() == DialogResult.OK) {
                txt_gr2_6_ocrPath.Text = fBdigImgPath.SelectedPath;
                ClsWritelog.Writeini("ocr", fBdigImgPath.SelectedPath);
            }

            else txt_gr2_6_ocrPath.Text = "";
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            if (ClsFrmInfoPar.StopTag == 1) {
                ClsFrmInfoPar.StopTag = 2;
                labinfo.Visible = true;
                return;
            }
            MessageBox.Show("任务未开始无法停止");
        }

        private void rab_gr2_8_ziduan_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_8_ziduan.Checked)
                ClsWritelog.Writeini("dirname", "1");
            else if (rab_gr2_8_mulu.Checked)
                ClsWritelog.Writeini("dirname", "2");
            else if (rab_gr2_8_ziduAndmulu.Checked)
                ClsWritelog.Writeini("dirname", "3");
        }

        private void rab_gr2_8_mulu_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_8_ziduan.Checked)
                ClsWritelog.Writeini("dirname", "1");
            else if (rab_gr2_8_mulu.Checked)
                ClsWritelog.Writeini("dirname", "2");
            else if (rab_gr2_8_ziduAndmulu.Checked)
                ClsWritelog.Writeini("dirname", "3");
        }

        private void rab_gr2_9_file_1_CheckedChanged(object sender, EventArgs e)
        {
            com_gr2_9_file_gz.Enabled = true;
            com_gr2_9_file_gz.Items.Clear();
            if (rab_gr2_9_file_1.Checked) {
                ClsWritelog.Writeini("filename", "1");
                if (rab_gr2_4_dan.Checked) {
                    com_gr2_9_file_gz.Items.Add("1,2,3;4,5,6");
                    com_gr2_9_file_gz.Items.Add("1,2,3;1,2,3");
                }
                else if (rab_gr2_4_duo.Checked) {
                    com_gr2_9_file_gz.Items.Add("1-3;1-4");
                    com_gr2_9_file_gz.Items.Add("1-3;4-6");
                    com_gr2_9_file_gz.Items.Add("1,4,7");
                }
                return;
            }
            else if (rab_gr2_9_juan_1.Checked)
                ClsWritelog.Writeini("filename", "2");
            else if (rab_gr2_9_file_ziduan.Checked)
                ClsWritelog.Writeini("filename", "3");
            com_gr2_9_file_gz.Enabled = false;
        }

        private void rab_gr2_9_juan_1_CheckedChanged(object sender, EventArgs e)
        {
            com_gr2_9_file_gz.Enabled = true;
            com_gr2_9_file_gz.Items.Clear();
            if (rab_gr2_9_file_1.Checked) {
                ClsWritelog.Writeini("filename", "1");
                if (rab_gr2_4_dan.Checked) {
                    com_gr2_9_file_gz.Items.Add("1,2,3;4,5,6");
                    com_gr2_9_file_gz.Items.Add("1,2,3;1,2,3");
                }
                else if (rab_gr2_4_duo.Checked) {
                    com_gr2_9_file_gz.Items.Add("1-3;1-4");
                    com_gr2_9_file_gz.Items.Add("1-3;4-6");
                    com_gr2_9_file_gz.Items.Add("1,4,7");
                }
                return;
            }
            else if (rab_gr2_9_juan_1.Checked)
                ClsWritelog.Writeini("filename", "2");
            else if (rab_gr2_9_file_ziduan.Checked)
                ClsWritelog.Writeini("filename", "3");
            com_gr2_9_file_gz.Enabled = false;
        }

        private void rab_gr2_4_dan_CheckedChanged(object sender, EventArgs e)
        {
            com_gr2_9_file_gz.Items.Clear();
            com_gr2_9_file_gz.Enabled = true;
            if (rab_gr2_4_dan.Checked) {
                ClsWritelog.Writeini("convfile", "1");
                if (rab_gr2_9_file_1.Checked) {
                    com_gr2_9_file_gz.Items.Add("1,2,3;4,5,6");
                    com_gr2_9_file_gz.Items.Add("1,2,3;1,2,3");
                    return;
                }

            }
            else if (rab_gr2_4_duo.Checked) {
                ClsWritelog.Writeini("convfile", "2");
                if (rab_gr2_9_file_1.Checked) {
                    com_gr2_9_file_gz.Items.Add("1-3;1-4");
                    com_gr2_9_file_gz.Items.Add("1-3;4-6");
                    com_gr2_9_file_gz.Items.Add("1,4,7");
                    return;
                }

            }
            else if (rab_gr2_4_duli.Checked)
                ClsWritelog.Writeini("convfile", "3");
            com_gr2_9_file_gz.Enabled = false;
        }
        private void rab_gr2_4_duo_CheckedChanged(object sender, EventArgs e)
        {
            com_gr2_9_file_gz.Items.Clear();
            com_gr2_9_file_gz.Enabled = true;
            if (rab_gr2_4_dan.Checked) {
                ClsWritelog.Writeini("convfile", "1");
                if (rab_gr2_9_file_1.Checked) {
                    com_gr2_9_file_gz.Items.Add("1,2,3;4,5,6");
                    com_gr2_9_file_gz.Items.Add("1,2,3;1,2,3");
                    return;
                }

            }
            else if (rab_gr2_4_duo.Checked) {
                ClsWritelog.Writeini("convfile", "2");
                if (rab_gr2_9_file_1.Checked) {
                    com_gr2_9_file_gz.Items.Add("1-3;1-4");
                    com_gr2_9_file_gz.Items.Add("1-3;4-6");
                    com_gr2_9_file_gz.Items.Add("1,4,7");
                    return;
                }

            }
            else if (rab_gr2_4_duli.Checked)
                ClsWritelog.Writeini("convfile", "3");
            com_gr2_9_file_gz.Enabled = false;

        }

        private void rab_gr2_7_wenzi_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_7_wenzi.Checked) {
                ClsWritelog.Writeini("water", "1");
                txt_gr2_7_waterFontsize.Enabled = true;
                txt_gr2_7_waterwith.Enabled = false;
                txt_gr2_7_waterheight.Enabled = false;
                return;
            }
            else if (rab_gr2_7_img.Checked) {
                ClsWritelog.Writeini("water", "2");
                txt_gr2_7_waterwith.Enabled = true;
                txt_gr2_7_waterheight.Enabled = true;
                txt_gr2_7_waterFontsize.Enabled = false;
            }

            else if (rab_gr2_7_wu.Checked)
                ClsWritelog.Writeini("water", "0");
        }

        private void rab_gr3_1_ftp_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr3_1_ftp.Checked)
                ClsWritelog.Writeini("ftp", "1");
            else ClsWritelog.Writeini("ftp", "0");
        }

        private void rab_gr2_7_img_Click(object sender, EventArgs e)
        {
            if (rab_gr2_7_img.Checked) {
                txt_gr2_7_waterFontsize.Enabled = false;
                txt_gr2_7_waterwith.Enabled = true;
                txt_gr2_7_waterheight.Enabled = true;
            }

        }

        private void rab_gr2_5_boxsn_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_5_boxsn.Checked)
                txt_gr2_5_juan.Enabled = false;
            else txt_gr2_5_juan.Enabled = true;
        }

        private void rab_gr2_8_ziduAndmulu_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_8_ziduAndmulu.Checked) {
                chk_gr2_8_conten.Enabled = true;
                chk_gr2_8_pages.Enabled = true;
            }
            else {
                chk_gr2_8_conten.Enabled = false;
                chk_gr2_8_pages.Enabled = false;
            }
        }

        #endregion

        private void butStart_Click(object sender, EventArgs e)
        {
            if (!IsTxtInfo())
                return;
            TxtEnd(false);
            if (ClsFrmInfoPar.OneJuan == 1) {
                Action Act = Stattask;
                Act.BeginInvoke(null, null);
                return;
            }
            else {
                if (!ClsOperate.AddTask()) {
                    TxtEnd(true);
                    return;
                }
                Action Act = StartTaskxc;
                Act.BeginInvoke(null, null);
            }
        }


        #region 东丽

        private void butTfSelect_Click(object sender, EventArgs e)
        {
            txtTfPath.Text = "";
            ClsDL.FilePath = "";
            if (fBdigImgPath.ShowDialog() == DialogResult.OK) {
                txtTfPath.Text = fBdigImgPath.SelectedPath;
                ClsDL.FilePath = fBdigImgPath.SelectedPath;
                return;
            }
        }
        private void butCreatPth_Click(object sender, EventArgs e)
        {
            txtCreatePath.Text = "";
            ClsDL.NewPath = "";
            if (fBdigImgPath.ShowDialog() == DialogResult.OK) {
                txtCreatePath.Text = fBdigImgPath.SelectedPath;
                ClsDL.NewPath = fBdigImgPath.SelectedPath;
                return;
            }
        }
        bool isdltxt()
        {
            if (combKf.Text.Trim().Length <= 0) {
                MessageBox.Show("库房不能为空!");
                combKf.Focus();
                return false;
            }
            else
                ClsDL.Houseid = V_HouseSetCs.Houseid;

            if (radZlcy.Checked)
                ClsDL.Zlcy = 1;
            else
                ClsDL.Zlcy = 2;

            if (chkdj.Checked)
                ClsDL.dj = 1;
            else
                ClsDL.dj = 0;
            if (rabdlboxsn.Checked) {
                if (txtB1.Text.Trim().Length <= 0 || txtB2.Text.Trim().Length <= 0) {
                    MessageBox.Show("盒号范围不能为空!");
                    txtB1.Focus();
                    return false;
                }
                else {
                    int p1;
                    int p2;
                    bool b1 = int.TryParse(txtB1.Text.Trim(), out p1);
                    bool b2 = int.TryParse(txtB2.Text.Trim(), out p2);
                    if (!b1 || !b2) {
                        MessageBox.Show("盒号不正确!");
                        txtB1.Focus();
                        return false;
                    }
                    if (p1 > p2) {
                        MessageBox.Show("盒号范围不正确!");
                        txtB1.Focus();
                        return false;
                    }

                    ClsDL.Boxsn = p1.ToString();
                    ClsDL.Boxsn2 = p2.ToString();
                }
                ClsDL.Fanwei = 1;
            }
            else {
                ClsDL.Fanwei = 2;
                ClsDL.Quhao = txtXq.Text.Trim();
            }
            if (!chkjpgxml.Checked && !chkpdf.Checked && !chkxlsbiao.Checked) {
                MessageBox.Show("请选择生成类型!");
                return false;
            }
            if (chkjpgxml.Checked && chkpdf.Checked)
                ClsDL.jpgpdf = "jpgpdf";
            else if (chkjpgxml.Checked)
                ClsDL.jpgpdf = "jpg";
            else if (chkpdf.Checked)
                ClsDL.jpgpdf = "pdf";
            if (!chkxlsbiao.Checked) {
                ClsDL.lx = 1;
                if (chkxlsbiao.Checked) {
                    MessageBox.Show("单导xls无需选择重新转换!");
                    return false;
                }
            }
            else
                ClsDL.lx = 2;

            if (radFtp.Checked)
                ClsDL.Ftp = 1;
            else if (radTfPath.Checked) {
                if (txtTfPath.Text.Trim().Length <= 0) {
                    MessageBox.Show("本地图像路径不能为空!");
                    return false;
                }
                ClsDL.Ftp = 2;
            }
            if (txtCreatePath.Text.Trim().Length <= 0) {
                MessageBox.Show("生成路径不能为空!");
                txtCreatePath.Focus();
                return false;
            }
            ClsDL.Pcbox = false;
            return true;
        }

        void Endtxt(bool bl)
        {
            this.Invoke(new Action(() =>
            {
                if (!bl) {
                    grdl1.Enabled = false;
                    grdl2.Enabled = false;
                    grdl3.Enabled = false;
                    butDlStart.Enabled = false;
                    txtB1.Enabled = false;
                    txtB2.Enabled = false;
                    txtXq.Enabled = false;
                    return;
                }
                grdl1.Enabled = true;
                grdl2.Enabled = true;
                grdl3.Enabled = true;
                butDlStart.Enabled = true;
                label15.Visible = false;
                txtB1.Enabled = true;
                txtB2.Enabled = true;
                txtXq.Enabled = true;
                ClsDL.Lsboxsn.Clear();
                stop = 0;
                lab_dl_zx.Text = "完成";
            }));
        }


        private void butDlStart_Click(object sender, EventArgs e)
        {
            if (!isdltxt())
                return;
            Endtxt(false);
            Action Act = Getboxsn;
            Act.BeginInvoke(null, null);
        }

        private bool Getfile(out string xxDownFile)
        {
            if (ClsDL.Ftp == 2) {
                xxDownFile = Path.Combine(ClsDL.FilePath, ClsDL.ArchFile.Substring(0, 8), ClsDL.ArchFile);
                if (!File.Exists(xxDownFile)) {
                    return false;
                }
                return true;
            }
            else {
                string pathtmp = Path.Combine(Common.LocalTempPath, ClsDL.ArchFile.Substring(0, 8));
                xxDownFile = Path.Combine(Common.LocalTempPath, ClsDL.ArchFile.Substring(0, 8), ClsDL.ArchFile);
                if (!Directory.Exists(pathtmp)) {
                    Directory.CreateDirectory(pathtmp);
                }
                if (File.Exists(xxDownFile)) {
                    File.Delete(xxDownFile);
                }
                if (ftp.CheckRemoteFile(Common.ArchSavePah, ClsDL.ArchFile.Substring(0, 8), ClsDL.ArchFile)) {
                    if (ftp.DownLoadFile(Common.ArchSavePah, ClsDL.ArchFile.Substring(0, 8), xxDownFile, ClsDL.ArchFile)) {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        void Getboxsn()
        {
            try {
                if (ClsDL.Fanwei == 1) {
                    if (ClsDL.Zlcy == 2) {
                        Common.DataSplitUpdateboxsn(ClsDL.Houseid, ClsDL.Boxsn, ClsDL.Boxsn2);
                    }
                    if (ClsDL.lx == 1)
                        ClsDL.dtboxsn = Common.DataSplitGetdataxls(ClsDL.Houseid, ClsDL.Boxsn, ClsDL.Boxsn2);
                    else
                        ClsDL.dtboxsn = Common.DataSplitGetdata(ClsDL.Houseid, ClsDL.Boxsn, ClsDL.Boxsn2);
                }
                else if (ClsDL.Fanwei == 2) {
                    if (ClsDL.Zlcy == 2) {
                        Common.DataSplitshantou(ClsDL.Houseid, ClsDL.Quhao, ClsDL.dj);
                    }
                    if (ClsDL.dj == 0) {
                        if (ClsDL.lx == 1)
                            ClsDL.dtboxsn = Common.DataSplitGetdatashantou(ClsDL.Houseid, ClsDL.Quhao, ClsDL.dj);
                        else
                            ClsDL.dtboxsn = Common.DataSplitGetdatashantou2(ClsDL.Houseid, ClsDL.Quhao, ClsDL.dj);
                    }
                    else {
                        if (ClsDL.lx == 1)
                            ClsDL.dtboxsn = Common.DataSplitGetdatashantoud(ClsDL.Houseid, ClsDL.Quhao, ClsDL.dj);
                        else
                            ClsDL.dtboxsn = Common.DataSplitGetdatashantoud2(ClsDL.Houseid, ClsDL.Quhao, ClsDL.dj);
                    }
                }
                if (ClsDL.dtboxsn == null || ClsDL.dtboxsn.Rows.Count <= 0) {
                    string str = "盒号:" + ClsDL.BoxsnTag + "未发现拆分数据";
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    return;
                }
                this.BeginInvoke(new Action(() => { lab_dl_juan.Text = "共计:" + ClsDL.dtboxsn.Rows.Count.ToString(); }));
                XmlSpite(ClsDL.Quhao);

            } catch (Exception e) {
                string str = "盒号:" + ClsDL.BoxsnTag + "意外问题" + e.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
            } finally {
                Endtxt(true);
            }

        }

        void XmlSpite(string archname)
        {
            try {
                for (int i = 0; i < ClsDL.dtboxsn.Rows.Count; i++) {
                    try {
                        if (stop == 1) {
                            stop = 0;
                            return;
                        }

                        this.Invoke(new Action(() =>
                        {
                            lab_dl_zx.Text = string.Format("正在执行第{0}卷", (i + 1).ToString());
                        }));
                        ClsDL.Archid = ClsDL.dtboxsn.Rows[i][0].ToString();
                        string boxsn = ClsDL.dtboxsn.Rows[i][1].ToString();
                        ClsDL.BoxsnTag = boxsn;
                        string page = ClsDL.dtboxsn.Rows[i][2].ToString();
                        string file = ClsDL.dtboxsn.Rows[i][3].ToString();
                        string archpage = ClsDL.dtboxsn.Rows[i][4].ToString();
                        DataTable dt = Common.Getmlinfo(ClsDL.Archid);
                        if (dt == null || dt.Rows.Count <= 0) {
                            string str = "盒号:" + boxsn + "获取目录信息失败";
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                            continue;
                        }

                        string mj = "";
                        DataRow[] drt = dt.Select("len(密级)>0");
                        if (drt.Length > 0)
                            mj = "密级";
                        string loadfile = "";
                        if (ClsDL.lx == 1) {
                            List<string> Apage = new List<string>();
                            List<int> Aintpage = new List<int>();
                            string[] s = archpage.Split(';');
                            if (s.Length > 0) {
                                for (int a = 0; a < s.Length; a++) {
                                    string d = s[a].Trim();
                                    if (d.Trim().Length <= 0)
                                        continue;
                                    Apage.Add(s[a].Trim());
                                    string[] c = s[a].Trim().Split('-');
                                    Aintpage.Add(Convert.ToInt32(c[0]));
                                }
                            }
                            if (ClsDL.Pcbox) {
                                if (ClsDL.Lspcbox.IndexOf(boxsn) >= 0) {
                                    ClsDL.dtboxsn.Rows.RemoveAt(i);
                                    i--;
                                    continue;
                                }
                            }
                            ClsDL.ArchFile = DESEncrypt.DesDecrypt(file);
                            if (ClsDL.ArchFile.Trim().Length <= 0) {
                                string str = "盒号:" + boxsn + "文件名称解密失败!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            
                            if (!Getfile(out loadfile)) {
                                string str = "盒号:" + boxsn + "获取图像文件失败";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            Himg.Filename = loadfile;
                            int imgpage = Himg._CountPage();
                            if (page != imgpage.ToString()) {
                                string str = "盒号:" + boxsn + "图像页码不一致!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }

                            for (int j = 0; j < dt.Rows.Count; j++) {
                                string zong = dt.Rows[0][0].ToString();
                                string mlsn = dt.Rows[0][1].ToString();
                                string qi = dt.Rows[0][2].ToString();
                                string anjuan = dt.Rows[0][3].ToString();
                                string tit = dt.Rows[j][8].ToString();
                                string mpage = dt.Rows[j][10].ToString();
                                string zongsn = zong + "-" + mlsn.PadLeft(3, '0') + "-" + qi + "-" + anjuan.PadLeft(4, '0');
                                string jpgpath = Path.Combine(ClsDL.NewPath, zong, "jpg", mj, zongsn);
                                string pdfpath = Path.Combine(ClsDL.NewPath, zong, "pdf", mj, zongsn);
                                string pdffile = zongsn + "-" + (j + 1).ToString().PadLeft(3, '0');
                                if (ClsDL.jpgpdf == "jpg") {
                                    if (!Directory.Exists(jpgpath))
                                        Directory.CreateDirectory(jpgpath);
                                }
                                else if (ClsDL.jpgpdf == "pdf") {
                                    if (!Directory.Exists(pdfpath))
                                        Directory.CreateDirectory(pdfpath);
                                }
                                else if (ClsDL.jpgpdf == "jpgpdf") {
                                    if (!Directory.Exists(jpgpath))
                                        Directory.CreateDirectory(jpgpath);
                                    if (!Directory.Exists(pdfpath))
                                        Directory.CreateDirectory(pdfpath);
                                }
                                int p1, p2, Cpages;
                                Cpages = 0;
                                p1 = Convert.ToInt32(mpage);
                                if (j < dt.Rows.Count - 1) {
                                    string tpage = dt.Rows[j + 1][10].ToString();
                                    p2 = Convert.ToInt32(tpage) - 1;
                                }
                                else {
                                    p2 = Convert.ToInt32(page);
                                    Cpages = p2;
                                }
                                int imgp1, imgp2;
                                if (!Getnum(Aintpage, p1, p2, Cpages, out imgp1, out imgp2)) {
                                    string str = "盒号:" + boxsn + " ,行号:" + j.ToString() + "计算页码失败!";
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    continue;
                                }
                                if (Cpages > 0)
                                    p2 = imgp2;
                                if (Himg._SplitImgjpgPdf(loadfile, jpgpath, pdfpath, ClsDL.jpgpdf, Apage, p1, p2, imgp1,
                                        imgp2, ClsDL.Zlcy, pdffile) != "ok") {
                                    string str = "盒号:" + boxsn + " ,行号:" + j.ToString() + "拆分图像失败!";
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    continue;
                                }
                            }
                            if (WirteImporxls(dt, boxsn, mj, archname) != "ok")
                                continue;
                            Common.DataSplitUpdate(ClsDL.Archid);
                        }
                        else {
                            if (WirteImporxls(dt, boxsn, mj, archname) != "ok")
                                continue;
                        }
                        try {
                            if (File.Exists(loadfile))
                                File.Delete(loadfile);
                        } catch { }
                    } catch (Exception e) {
                        string str = "盒号:" + ClsDL.BoxsnTag + "意外问题" + e.ToString();
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        continue;
                    }
                }
            } catch (Exception e) {
                string str = "盒号:" + ClsDL.BoxsnTag + "意外问题" + e.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
            }
        }

        private Boolean Getnum(List<int> str, int p1, int p2, int p3, out int imgp1, out int imgp2)
        {
            imgp1 = 0;
            imgp2 = 0;
            try {
                for (int i = 0; i < str.Count; i++) {
                    int b = str[i];
                    if (b < p1)
                        imgp1 += 1;
                    if (b < p2)
                        imgp2 += 1;
                }
                imgp1 = p1 + imgp1;
                if (p3 <= 0)
                    imgp2 = p2 + imgp2;
                else
                    imgp2 = p3 - imgp2;
                return true;
            } catch {
                return false;
            }

        }

        private void butDlLog_Click(object sender, EventArgs e)
        {
            string file = Path.Combine(ClsFrmInfoPar.LogPath, "log.txt");
            if (File.Exists(file)) {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(file);
            }
            else MessageBox.Show("日志文件不存在!");
        }


        private int stop = 0;
        private void butDLStop_Click(object sender, EventArgs e)
        {
            stop = 1;
            label15.Visible = true;
        }

        void enddc(bool bl)
        {
            if (!bl) {
                txtB1.Enabled = false;
                txtB2.Enabled = false;
                txtXq.Enabled = false;
                butDLimpor.Enabled = false;
                return;
            }
            txtB1.Enabled = true;
            txtB2.Enabled = true;
            txtXq.Enabled = true;
            butDLimpor.Enabled = true;
        }

        private void butDLimpor_Click(object sender, EventArgs e)
        {
            if (rabdlboxsn.Checked) {
                if (txtB1.Text.Trim().Length <= 0 || txtB2.Text.Trim().Length <= 0) {
                    MessageBox.Show("盒号卷号不能为空!");
                    txtB1.Focus();
                    return;
                }
                int b1, b2;
                bool bl = int.TryParse(txtB1.Text, out b1);
                bool bl2 = int.TryParse(txtB2.Text, out b2);
                if (!bl || !bl2) {
                    MessageBox.Show("盒号不正确!");
                    txtB1.Focus();
                    return;
                }
                if (b1 > b2) {
                    MessageBox.Show("起始盒号不能大于终止盒号!");
                    txtB1.Focus();
                    return;
                }
            }
            if (txtCreatePath.Text.Trim().Length <= 0) {
                MessageBox.Show("生成路径不能为空!");
                txtCreatePath.Focus();
                return;
            }
            enddc(false);
        }


        string WirteImporxls(DataTable dt, string boxsn, string mj, string strname)
        {
            try {
                if (dt == null || dt.Rows.Count <= 0) {
                    string str = "未发现可导出数据!";
                    return str;
                }
                string strfile = Path.Combine(Application.StartupPath, "Tjd.xlsx");
                string dir = Path.Combine(txtCreatePath.Text.Trim(), mj);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string file = Path.Combine(dir, strname + ("目录数据.xlsx"));
                if (!File.Exists(file))
                    File.Copy(strfile, file);
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(file);
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                if (rows == 0)
                    rows = 1;
                for (int i = 0; i < dt.Rows.Count; i++) {

                    for (int j = 0; j < dt.Columns.Count; j++) {
                        string str = dt.Rows[i][j].ToString().Trim();
                        if (j == 10)
                            str = str.PadLeft(3, '0');
                        wsheek.Range[rows + i, j + 1].Text = str;
                    }
                }
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
                return "ok";
            } catch (Exception ex) {
                return "导出数据失败:" + ex.ToString();
            }
        }


        private void chkxlsbiao_Click(object sender, EventArgs e)
        {
            if (chkxlsbiao.Checked) {
                chkjpgxml.Checked = false;
                chkpdf.Checked = false;
            }
        }

        private void chkjpgxml_Click(object sender, EventArgs e)
        {
            if (chkjpgxml.Checked)
                chkxlsbiao.Checked = false;
        }
        private void chkpdf_Click(object sender, EventArgs e)
        {
            if (chkpdf.Checked)
                chkxlsbiao.Checked = false;
        }

        #endregion


    }
}
