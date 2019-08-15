using DAL;
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
                if (txtXq.Text.Trim().Length <= 0) {
                    MessageBox.Show("小区代码不能为空!");
                    txtXq.Focus();
                    return false;
                }
                ClsDL.Fanwei = 2;
                ClsDL.Quhao = txtXq.Text.Trim();
            }
            if (!chkjpgxml.Checked && !chkxls.Checked) {
                MessageBox.Show("请选择生成类型!");
                return false;
            }
            if (chkjpgxml.Checked)
                ClsDL.jpgxml = 1;
            else
                ClsDL.jpgxml = 0;
            if (chkxls.Checked)
                ClsDL.xls = 1;
            else
                ClsDL.xls = 0;
            if (!chkjpgxml.Checked && chkxls.Checked) {
                ClsDL.lx = 1;
                if (radCxzh.Checked) {
                    MessageBox.Show("单导xls无需选择重新转换!");
                    return false;
                }
            }

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
            ClsDL.lsh = 0;
            if (chkboxsn.Checked) {
                if (txtlsh.Text.Trim().Length <= 0) {
                    MessageBox.Show("流水号不能为空!");
                    txtlsh.Focus();
                    return false;
                }
                else {
                    int lsh;
                    bool bl = int.TryParse(txtlsh.Text.Trim(), out lsh);
                    if (!bl) {
                        MessageBox.Show("流水号不正确!");
                        return false;
                    }
                    ClsDL.lsh = lsh;
                }
            }
            else
                ClsDL.lsh = 0;

            ClsDL.Pcbox = false;
            if (chkPcboxn.Checked) {
                if (lbPcbox.Items.Count <= 0) {
                    MessageBox.Show("请输入排除的盒号!");
                    lbPcbox.Focus();
                    return false;
                }
                ClsDL.Pcbox = true;
            }
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
                    return;
                }
                grdl1.Enabled = true;
                grdl2.Enabled = true;
                grdl3.Enabled = true;
                butDlStart.Enabled = true;
                label15.Visible = false;
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
        private string Getbase64(string file)
        {
            try {
                Bitmap bmp = new Bitmap(file);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            } catch {
                return null;
            }
        }
        private bool WriteFwtj(string file, string archid, string ariveid, string tm)
        {
            try {
                if (!File.Exists(file)) {
                    string strfile = Path.Combine(Application.StartupPath, "Fwtj.xlsx");
                    File.Copy(strfile, file);
                }
                DataTable dt = Common.Getinfosx(archid);
                if (dt == null || dt.Rows.Count <= 0) {
                    string str = "未找到录入信息,id号" + archid + " 二维码:" + tm;
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    return false;
                }
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(file);
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                for (int i = 0; i < dt.Rows.Count; i++) {

                    if (i == 0) {
                        wsheek.Range[rows + i, 1].Text = ariveid;
                        wsheek.Range[rows + i, 3].Text = tm;
                    }
                    for (int t = 0; t < dt.Columns.Count - 1; t++) {

                        string str = dt.Rows[i][t].ToString();
                        if (i == 0 && t == 0)
                            wsheek.Range[rows + i, 2].Text = str;
                        else if (i == 0 && t == 1)
                            wsheek.Range[rows + i, 4].Text = str;
                        else if (t == 4)
                            wsheek.Range[rows + i, t + 4].Text = "东丽区";
                        else if (t >= 2)
                            wsheek.Range[rows + i, t + 4].Text = str;

                    }
                }
                int count = wsheek.LastRow;
                CellRange range = wsheek.Range["A" + rows + ":R" + count];
                range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
                return true;
            } catch (Exception ex) {
                string str = "写入xls失败,id号" + archid + " 二维码:" + tm + " -->错误信息" + ex.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                return false;
            }
        }

        private bool WriteMltj(string file, string archid, string ariveid, string tm, string pagecount)
        {
            try {
                if (!File.Exists(file)) {
                    if (!Directory.Exists(Path.GetDirectoryName(file)))
                        Directory.CreateDirectory(Path.GetDirectoryName(file));
                    string strfile = Path.Combine(Application.StartupPath, "jnmltj.xlsx");
                    File.Copy(strfile, file);
                }
                DataTable dt = Common.Getmlinfo2(archid);
                if (dt == null || dt.Rows.Count <= 0) {
                    string str = "未找到目录信息,id号" + archid + " 二维码:";
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    return false;
                }
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(file);
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    if (i == 0) {
                        wsheek.Range[rows + i, 1].Text = ariveid;
                        wsheek.Range[rows + i, 2].Text = tm;
                        continue;
                    }
                    for (int t = 0; t < dt.Columns.Count; t++) {
                        string str = dt.Rows[i][t].ToString();
                        if (t < 3)
                            wsheek.Range[rows + i - 1, t + 3].Text = str;
                        else if (t == 3) {
                            int p = 0;
                            try {
                                int p1 = Convert.ToInt32(dt.Rows[i + 1][2].ToString());
                                p = p1 - 1;
                            } catch {
                                p = Convert.ToInt32(pagecount);

                            }
                            wsheek.Range[rows + i - 1, 6].Text = p.ToString();
                            wsheek.Range[rows + i - 1, 7].Text = str;
                        }
                    }
                }
                int count = wsheek.LastRow;
                wsheek.Range["A" + rows + ":A" + count].Style.HorizontalAlignment = HorizontalAlignType.Center;
                wsheek.Range["A" + rows + ":A" + count].Merge();
                wsheek.Range["B" + rows + ":B" + count].Merge();
                CellRange range = wsheek.Range["A" + rows + ":G" + count];
                range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
                return true;
            } catch (Exception ex) {
                string str = "写入xls失败,id号" + archid + " 二维码:" + tm + " -->错误信息" + ex.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                return false;
            }
        }

        private bool WriteTxtj(string file, string tm, string pagecount, List<string> A0, List<string> A1, List<string> A2, List<string> A3, List<string> A4)
        {
            try {
                if (!File.Exists(file)) {
                    if (!Directory.Exists(Path.GetDirectoryName(file)))
                        Directory.CreateDirectory(Path.GetDirectoryName(file));
                    string strfile = Path.Combine(Application.StartupPath, "txtj.xlsx");
                    File.Copy(strfile, file);
                }
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(file);
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                wsheek.Range[rows, 1].Text = tm;
                wsheek.Range[rows, 2].Text = pagecount;
                wsheek.Range[rows, 3].Text = A0.Count.ToString();
                if (A0.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A0.Count; i++) {
                        string s = A0[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 4].Text = str;
                }
                wsheek.Range[rows, 5].Text = A1.Count.ToString();
                if (A1.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A1.Count; i++) {
                        string s = A1[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 6].Text = str;
                }
                wsheek.Range[rows, 7].Text = A2.Count.ToString();
                if (A2.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A2.Count; i++) {
                        string s = A2[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 8].Text = str;
                }
                wsheek.Range[rows, 9].Text = A3.Count.ToString();
                if (A3.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A3.Count; i++) {
                        string s = A3[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 10].Text = str;
                }
                wsheek.Range[rows, 11].Text = A4.Count.ToString();
                CellRange range = wsheek.Range["A" + rows + ":K" + rows];
                range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
                return true;
            } catch (Exception ex) {
                string str = "写入xls失败" + " 二维码:" + tm + " -->错误信息" + ex.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                return false;
            }
        }
        private bool WriteTxtj(string file, string tm, string XFileName, string pagecount)
        {
            try {
                if (!File.Exists(file)) {
                    if (!Directory.Exists(Path.GetDirectoryName(file)))
                        Directory.CreateDirectory(Path.GetDirectoryName(file));
                    string strfile = Path.Combine(Application.StartupPath, "txtj.xlsx");
                    File.Copy(strfile, file);
                }
                List<string> A0 = new List<string>();
                List<string> A1 = new List<string>();
                List<string> A2 = new List<string>();
                List<string> A3 = new List<string>();
                List<string> A4 = new List<string>();
                Himg.Filename = XFileName;
                Himg.GetImgSize(out A0, out A1, out A2, out A3, out A4);
                try {
                    if (ClsDL.Ftp == 1) {
                        if (File.Exists(XFileName)) {
                            File.Delete(XFileName);
                        }
                    }
                } catch { }
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(file);
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                wsheek.Range[rows, 1].Text = tm;
                wsheek.Range[rows, 2].Text = pagecount;
                wsheek.Range[rows, 3].Text = A0.Count.ToString();
                if (A0.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A0.Count; i++) {
                        string s = A0[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 4].Text = str;
                }
                wsheek.Range[rows, 5].Text = A1.Count.ToString();
                if (A1.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A1.Count; i++) {
                        string s = A1[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 6].Text = str;
                }
                wsheek.Range[rows, 7].Text = A2.Count.ToString();
                if (A2.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A2.Count; i++) {
                        string s = A2[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 8].Text = str;
                }
                wsheek.Range[rows, 9].Text = A3.Count.ToString();
                if (A3.Count > 0) {
                    string str = "";
                    for (int i = 0; i < A3.Count; i++) {
                        string s = A3[i];
                        if (str.Trim().Length <= 0)
                            str += s.PadLeft(3, '0') + ".jpg";
                        else
                            str += ";" + s.PadLeft(3, '0') + ".jpg";
                    }
                    wsheek.Range[rows, 10].Text = str;
                }
                wsheek.Range[rows, 11].Text = A4.Count.ToString();
                CellRange range = wsheek.Range["A" + rows + ":K" + rows];
                range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
                return true;
            } catch (Exception ex) {
                string str = "写入xls失败" + " 二维码:" + tm + " -->错误信息" + ex.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                return false;
            }
        }

        XmlDocument xmldoc;
        XmlElement xmlelem;
        private bool Wirtexml(string archid, string ewm, string file, string pagecount, List<string> lsjpg, string ariveid)
        {
            try {
                xmldoc = new XmlDocument();
                //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                //加入一个根元素
                xmlelem = xmldoc.CreateElement("", "Message", "");
                xmldoc.AppendChild(xmlelem);

                XmlNode root = xmldoc.SelectSingleNode("Message");//查找<Employees>
                XmlElement xe1 = xmldoc.CreateElement("Head");

                DataTable dt = Common.getinfo(archid);
                if (dt == null || dt.Rows.Count <= 0) {
                    string str = "录入信息获取失败 id号：" + archid + " 二维码信息：" + ewm;
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    return false;
                }
                string archtype = dt.Rows[0][1].ToString();
                string page = dt.Rows[0][12].ToString();
                if (page.Trim().Length <= 0) {
                    string str = "录入信息中页码不正确 id号：" + archid + " 二维码信息：" + ewm;
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    return false;
                }
                XmlElement xesub1 = xmldoc.CreateElement("ArchiveID");
                xesub1.InnerText = file;
                xe1.AppendChild(xesub1);

                xesub1 = xmldoc.CreateElement("ArchiveType");
                xesub1.InnerText = archtype;
                xe1.AppendChild(xesub1);

                xesub1 = xmldoc.CreateElement("ArchiveNumber");
                xesub1.InnerText = ewm;
                xe1.AppendChild(xesub1);

                xesub1 = xmldoc.CreateElement("PageCount");
                xesub1.InnerText = page;
                xe1.AppendChild(xesub1);

                xesub1 = xmldoc.CreateElement("ArchiveLocation");
                xesub1.InnerText = "";
                xe1.AppendChild(xesub1);

                root.AppendChild(xe1);//添加到<Employees>节点中

                root = xmldoc.SelectSingleNode("Message");//查找<Employees>
                xe1 = xmldoc.CreateElement("Data");
                root.AppendChild(xe1);//添加到<Employees>节点中
                                      //循环几手信息
                for (int i = 0; i < dt.Rows.Count; i++) {

                    string xh = (i + 1).ToString();
                    string bzid = dt.Rows[i][11].ToString();
                    string djlx = dt.Rows[i][10].ToString();
                    string qu = "东丽区";
                    string zl = dt.Rows[i][14].ToString();
                    string bdcsj = dt.Rows[i][2].ToString();
                    string zdsn = dt.Rows[i][7].ToString();
                    string dh = dt.Rows[i][5].ToString();
                    string bdcdyh = dt.Rows[i][8].ToString();
                    string qlr = dt.Rows[i][3].ToString();
                    string dyqlr = dt.Rows[i][4].ToString();
                    string bdczh = dt.Rows[i][6].ToString();
                    string spsj = dt.Rows[i][9].ToString();
                    if (bzid.Trim().Length <= 0) {
                        string str = "业务编号不正确 id号：" + archid + " 二维码信息：" + ewm;
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        return false;
                    }
                    XmlElement xesub2 = xmldoc.CreateElement("BDC_BUSINESS");
                    xesub2.SetAttribute("XH", xh);
                    xesub2.SetAttribute("BZID", bzid);
                    xesub2.SetAttribute("DJLX", djlx);
                    xesub2.SetAttribute("QX", qu);
                    xesub2.SetAttribute("ZL", zl);
                    xesub2.SetAttribute("BDCSJH", bdcsj);
                    xesub2.SetAttribute("ZDZHHM", zdsn);
                    xesub2.SetAttribute("DH", dh);
                    xesub2.SetAttribute("BDCDYH", bdcdyh);
                    xesub2.SetAttribute("QLRMC", qlr);
                    xesub2.SetAttribute("DYQRMC", dyqlr);
                    xesub2.SetAttribute("BDCQZH", bdczh);
                    xesub2.SetAttribute("SPSJ", spsj);
                    xesub2.SetAttribute("ZT", "");
                    xe1.AppendChild(xesub2);
                }
                //循环目录
                int mlpage = 1;
                List<string> lsywid = new List<string>();
                dt = Common.Getmlinfo(archid);
                if (dt == null || dt.Rows.Count <= 0) {
                    string str = "目录信息获取失败 id号：" + archid + " 二维码信息：" + ewm;
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    return false;
                }
                for (int i = 0; i < dt.Rows.Count; i++) {

                    string xh = i.ToString();
                    string title = dt.Rows[i][1].ToString();
                    string mllx = dt.Rows[i][2].ToString();
                    string qsy = dt.Rows[i][4].ToString();
                    string ywid = dt.Rows[i][3].ToString();
                    string ys = "";
                    int p1 = 0;
                    try {
                        p1 = Convert.ToInt32(qsy);
                        string p2 = dt.Rows[i + 1][4].ToString();
                        ys = (Convert.ToInt32(p2) - p1).ToString();
                    } catch (Exception) {
                        ys = (Convert.ToInt32(pagecount) - p1).ToString();
                    }
                    if (title == "目录") {
                        mlpage = Convert.ToInt32(ys);
                        continue;
                    }
                    lsywid.Add(ys);
                    XmlElement xesub2 = xmldoc.CreateElement("CATALOGUE");
                    xesub2.SetAttribute("XH", xh);
                    xesub2.SetAttribute("CATAID", xh);
                    xesub2.SetAttribute("MLZL", mllx);
                    xesub2.SetAttribute("MLMC", title);
                    xesub2.SetAttribute("MLQSY", qsy);
                    xesub2.SetAttribute("YS", ys);
                    xesub2.SetAttribute("FJ", "");
                    xesub2.SetAttribute("BZID", ywid);
                    xe1.AppendChild(xesub2);

                }
                //循环写入图片信息
                int id = 0;  //总顺序号
                int ys1 = 0;  //每个目录下页数
                int ywid1 = 1; //每个目录的id
                int tmpjl = 0;  //记录当前循环是否大于目前页数;
                for (int i = 0; i < lsjpg.Count; i++) {
                    if (i < mlpage)
                        continue;
                    id += 1;
                    if (ys1 == 0) {
                        if (lsywid.Count > 0)
                        {
                            ys1 = Convert.ToInt32(lsywid[0]);
                            lsywid.RemoveAt(0);
                        }
                    }
                    if (tmpjl < ys1)
                        tmpjl += 1;
                    else {
                        if (lsywid.Count > 0) {
                            ys1 = Convert.ToInt32(lsywid[0]);
                            lsywid.RemoveAt(0);
                            tmpjl = 1;
                            ywid1 += 1;
                        }
                    }
                    string jpg = lsjpg[i];
                    string xh = id.ToString();
                    string filenmae = Path.GetFileName(jpg);
                    string kzm = ".jpg";
                    string cataid = ywid1.ToString();
                    string info = Getbase64(jpg);
                    if (info.Trim().Length <= 0) {
                        string str = "jpg转base64失败 id号：" + archid + " 二维码信息：" + ewm;
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        return false;
                    }

                    XmlElement xesub2 = xmldoc.CreateElement("CATALOGUE_ATTACHMENT");
                    xesub2.SetAttribute("XH", xh);
                    xesub2.SetAttribute("ATTACHMENTID", xh);
                    xesub2.SetAttribute("CATAID", cataid);
                    xesub2.SetAttribute("WJMC", filenmae);
                    xesub2.SetAttribute("WJLX", kzm);
                    xesub2.SetAttribute("WJNY", info);
                    xe1.AppendChild(xesub2);
                }
                xmldoc.Save(ariveid);
            } catch (Exception ex) {
                string str = "xml写入失败:" + archid + " 二维码信息：" + ewm + " --> 错误信息:" + ex.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                return false;
            }
            return true;
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
                else {
                    if (ClsDL.Zlcy == 2) {
                        Common.DataSplitUpdateboxsn(ClsDL.Houseid, ClsDL.Quhao);
                    }
                    if (ClsDL.lx == 1)
                        ClsDL.dtboxsn = Common.DataSplitGetdataxls(ClsDL.Houseid, ClsDL.Quhao);
                    else
                        ClsDL.dtboxsn = Common.DataSplitGetdata(ClsDL.Houseid, ClsDL.Quhao);
                }
                if (ClsDL.dtboxsn == null || ClsDL.dtboxsn.Rows.Count <= 0)
                    return;
                this.BeginInvoke(new Action(() => { lab_dl_juan.Text = "共计:" + ClsDL.dtboxsn.Rows.Count.ToString(); }));
                for (int i = 0; i < ClsDL.dtboxsn.Rows.Count; i++) {
                    try {
                        if (stop == 1) {
                            stop = 0;
                            return;
                        }
                        this.Invoke(new Action(() => { lab_dl_zx.Text = string.Format("正在执行第0卷", i.ToString()); }));
                        ClsDL.Archid = ClsDL.dtboxsn.Rows[i][0].ToString();
                        string boxsn = ClsDL.dtboxsn.Rows[i][1].ToString();
                        ClsDL.BoxsnTag = boxsn;
                        string page = ClsDL.dtboxsn.Rows[i][2].ToString();
                        string file = ClsDL.dtboxsn.Rows[i][3].ToString();
                        string qu = ClsDL.dtboxsn.Rows[i][4].ToString();
                        string lx = ClsDL.dtboxsn.Rows[i][5].ToString();
                        if (ClsDL.Pcbox) {
                            if (ClsDL.Lspcbox.IndexOf(boxsn) >= 0) {
                                ClsDL.dtboxsn.Rows.RemoveAt(i);
                                i--;
                                continue;
                            }
                        }
                        if (qu.Trim().Length <= 0) {
                            string str = "盒号:" + boxsn + "小区代码不正确!";
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                            continue;
                        }
                        if (lx.Trim().Length <= 0) {
                            string str = "盒号:" + boxsn + "档案所属类型不正确!";
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                            continue;
                        }
                        ClsDL.ArchFile = DESEncrypt.DesDecrypt(file);
                        if (ClsDL.ArchFile.Trim().Length <= 0) {
                            string str = "盒号:" + boxsn + "文件名称解密失败!";
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                            continue;
                        }
                        string loadfile = "";
                        if (!Getfile(out loadfile)) {
                            string str = "盒号:" + boxsn + "获取图像文件失败";
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                            continue;
                        }
                        ClsDL.ewmname = "DL-" + qu + "-" + lx + boxsn.PadLeft(6, '0');
                        string time = DateTime.Now.ToString("yyMMdd").ToString().Trim();
                        if (ClsDL.lsh > 0)
                            ClsDL.xmlname = "120110" + time + (ClsDL.lsh + i).ToString().PadLeft(6, '0');
                        else
                            ClsDL.xmlname = "120110" + time + (i + 1).ToString().PadLeft(6, '0');
                        if (ClsDL.xls == 1 && ClsDL.jpgxml == 1) {
                            string jpgdir = Path.Combine(ClsDL.NewPath, qu, "jpg", ClsDL.ewmname);
                            string xmldir = Path.Combine(ClsDL.NewPath, qu, "xml");
                            if (!Directory.Exists(jpgdir))
                                Directory.CreateDirectory(jpgdir);
                            if (!Directory.Exists(xmldir))
                                Directory.CreateDirectory(xmldir);
                            Himg.Filename = loadfile;
                            int imgpage = Himg._CountPage();
                            if (page != imgpage.ToString()) {
                                string str = "盒号:" + boxsn + "图像页码不一致!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            List<string> lsjpg = new List<string>();
                            List<string> A0 = new List<string>();
                            List<string> A1 = new List<string>();
                            List<string> A2 = new List<string>();
                            List<string> A3 = new List<string>();
                            List<string> A4 = new List<string>();
                            if (!Himg._SplitImg(jpgdir, ClsDL.Zlcy, out lsjpg, out A0, out A1, out A2, out A3, out A4)) {
                                string str = "盒号:" + boxsn + "转换jpg图像文件失败!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            else {
                                try {
                                    if (ClsDL.Ftp == 1) {
                                        if (File.Exists(loadfile)) {
                                            File.Delete(loadfile);
                                        }
                                    }
                                } catch { }
                                string xmlfile = Path.Combine(xmldir, ClsDL.xmlname + ".xml");
                                if (!Wirtexml(ClsDL.Archid, ClsDL.ewmname, ClsDL.xmlname, page, lsjpg, xmlfile)) {
                                    string str = "盒号:" + boxsn + "生成xml失败!";
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    continue;
                                }
                                else {

                                    string newpath = Path.Combine(ClsDL.NewPath, qu);
                                    if (!Directory.Exists(newpath))
                                        Directory.CreateDirectory(newpath);
                                    Common.DataSplitUpdate(ClsDL.Archid);
                                    string ScPathXls = Path.Combine(newpath,
                                        ClsDL.Boxsn + "-" + ClsDL.Boxsn + "房屋统计.xlsx");
                                    string scpathxlsml = Path.Combine(newpath,
                                        ClsDL.Boxsn + "-" + ClsDL.Boxsn + "卷内目录统计.xlsx");
                                    string scpathxlstj = Path.Combine(newpath,
                                        ClsDL.Boxsn + "-" + ClsDL.Boxsn + "图像统计.xlsx");
                                    if (!WriteFwtj(ScPathXls, ClsDL.Archid, ClsDL.xmlname, ClsDL.ewmname)) {
                                        string str = "盒号:" + boxsn + "房屋统计写入xls表失败!";
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                        continue;
                                    }

                                    if (!WriteMltj(scpathxlsml, ClsDL.Archid, ClsDL.xmlname, ClsDL.ewmname, page)) {
                                        string str = "盒号:" + boxsn + "目录统计写入xls表失败!";
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                        continue;
                                    }

                                    if (!WriteTxtj(scpathxlstj, ClsDL.ewmname, page, A0, A1, A2, A3, A4)) {
                                        string str = "盒号:" + boxsn + "图像统计写入xls表失败!";
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                        continue;
                                    }
                                }
                            }
                        }
                        else if (ClsDL.jpgxml == 1) {
                            string jpgdir = Path.Combine(ClsDL.NewPath, "jpg", ClsDL.ewmname);
                            string xmldir = Path.Combine(ClsDL.NewPath, "xml");
                            if (!Directory.Exists(jpgdir))
                                Directory.CreateDirectory(jpgdir);
                            if (!Directory.Exists(xmldir))
                                Directory.CreateDirectory(xmldir);
                            Himg.Filename = loadfile;
                            List<string> lsjpg = new List<string>();
                            List<string> A0 = new List<string>();
                            List<string> A1 = new List<string>();
                            List<string> A2 = new List<string>();
                            List<string> A3 = new List<string>();
                            List<string> A4 = new List<string>();
                            if (!Himg._SplitImg(jpgdir, ClsDL.Zlcy, out lsjpg)) {
                                string str = "盒号:" + boxsn + "转换jpg图像文件失败!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            else {
                                if (ClsDL.Ftp == 1) {
                                    try {
                                        if (File.Exists(loadfile)) {
                                            File.Delete(loadfile);
                                        }
                                    } catch { }
                                }
                                string xmlfile = Path.Combine(xmldir, ClsDL.xmlname + ".xml");
                                if (!Wirtexml(ClsDL.Archid, ClsDL.ewmname, ClsDL.xmlname, page, lsjpg, xmlfile)) {
                                    string str = "盒号:" + boxsn + "生成xml失败!";
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    continue;
                                }
                                Common.DataSplitUpdate(ClsDL.Archid);
                            }

                        }
                        else if (ClsDL.xls == 1) {
                            string newpath = Path.Combine(ClsDL.NewPath, qu);
                            if (!Directory.Exists(newpath))
                                Directory.CreateDirectory(newpath);
                            string ScPathXls = Path.Combine(newpath,
                                ClsDL.Boxsn + "-" + ClsDL.Boxsn + "房屋统计.xlsx");
                            string scpathxlsml = Path.Combine(newpath,
                                ClsDL.Boxsn + "-" + ClsDL.Boxsn + "卷内目录统计.xlsx");
                            string scpathxlstj = Path.Combine(newpath,
                                ClsDL.Boxsn + "-" + ClsDL.Boxsn + "图像统计.xlsx");
                            if (!WriteFwtj(ScPathXls, ClsDL.Archid, ClsDL.xmlname, ClsDL.ewmname)) {
                                string str = "盒号:" + boxsn + "房屋统计写入xls表失败!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            if (!WriteMltj(scpathxlsml, ClsDL.Archid, ClsDL.xmlname, ClsDL.ewmname, page)) {
                                string str = "盒号:" + boxsn + "目录统计写入xls表失败!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                            if (!WriteTxtj(scpathxlstj, ClsDL.ewmname, loadfile, page)) {
                                string str = "盒号:" + boxsn + "图像统计写入xls表失败!";
                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                continue;
                            }
                        }

                    } catch (Exception e) {
                        string str = "盒号:" + ClsDL.BoxsnTag + "意外问题" + e.ToString();
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        continue;
                    }
                }
            } catch (Exception e) {
                string str = "盒号:" + ClsDL.BoxsnTag + "意外问题" + e.ToString();
                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
            } finally {
                Endtxt(true);
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
            else {
                if (txtXq.Text.Trim().Length <= 0) {
                    MessageBox.Show("小区代码不能为空!");
                    txtXq.Focus();
                    return;
                }
            }
            if (txtCreatePath.Text.Trim().Length <= 0) {
                MessageBox.Show("生成路径不能为空!");
                txtCreatePath.Focus();
                return;
            }
            enddc(false);
            WirteImporxls();
        }


        void WirteImporxls()
        {
            try {
                DataTable dt = null;
                if (rabdlboxsn.Checked)
                    dt = Common.GetDataImportxls(ClsDL.Houseid, txtB1.Text.Trim(), txtB2.Text.Trim());
                else
                    dt = Common.GetDataImportxls(ClsDL.Houseid, txtXq.Text.Trim());
                if (dt == null || dt.Rows.Count <= 0) {
                    MessageBox.Show("未发现可导出数据!");
                    return;
                }
                string strfile = Path.Combine(Application.StartupPath, "Tjd.xlsx");
                string file = Path.Combine(txtCreatePath.Text.Trim(), DateTime.Now.ToString("yyyyMMdd").ToString() + ("提交单.xlsx"));
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

                    wsheek.Range[rows + i, 1].Text = (i + 1).ToString();
                    wsheek.Range[rows + i, 2].Text = dt.Rows[i][0].ToString();
                    wsheek.Range[rows + i, 3].Text = dt.Rows[i][1].ToString();
                    wsheek.Range[rows + i, 5].Text = dt.Rows[i][2].ToString();
                    wsheek.Range[rows + i, 7].Text = dt.Rows[i][2].ToString();
                }
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            } finally {
                enddc(true);
            }
        }


        private void butPcadd_Click(object sender, EventArgs e)
        {
            if (txtPcbox.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入盒号!");
                txtPcbox.Focus();
                return;
            }

            if (ClsDL.Lspcbox.IndexOf(txtPcbox.Text.Trim()) >= 0) {
                MessageBox.Show("此盒号已存在!");
                txtPcbox.Focus();
                return;
            }
            lbPcbox.Items.Add(txtPcbox.Text.Trim());
            ClsDL.Lspcbox.Add(txtPcbox.Text.Trim());
        }


        private void butPcdel_Click(object sender, EventArgs e)
        {
            if (lbPcbox.Items.Count <= 0)
                return;
            if (lbPcbox.SelectedItems.Count <= 0)
                return;
            int x = lbPcbox.SelectedIndex;
            lbPcbox.Items.RemoveAt(x);
            if (x >ClsDL.Lspcbox.Count)
                return;
            ClsDL.Lspcbox.RemoveAt(x);
        }
        #endregion


    }
}
