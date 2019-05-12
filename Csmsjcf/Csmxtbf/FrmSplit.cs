using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLjscom;
using DataRow = System.Data.DataRow;

namespace Csmsjcf
{
    public partial class FrmSplit : Form
    {
        public FrmSplit()
        {
            InitializeComponent();
        }

        public static Hljsimage Himg;

        #region baseInfo

        private void CombAddinfo()
        {

            comb_gr2_2_task.Items.Clear();
            comb_gr2_7_weizhi.Items.Clear();
            combHouseid.Items.Clear();
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
            else if (rab_gr2_8_ziduAndmulu.Checked)
                ClsFrmInfoPar.DirNamesn = 3;
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
                    str = "错误：未找到已质检盒号信息或已拆分 -->盒号:" + box;
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
                                p1 = Convert.ToInt32(dirtTable.Rows[d][1].ToString());
                                try {
                                    p2 = Convert.ToInt32(dirtTable.Rows[d + 1][1].ToString());
                                    p2 -= 1;
                                } catch {
                                    p2 = Convert.ToInt32(pages);
                                }
                                if (ClsDataSplitPar.ClsdirMl.Trim().Length > 0)
                                    ml = dirtTable.Rows[d][0].ToString().Trim();
                                if (ClsDataSplitPar.ClsdirPageZero == 0)
                                    pzer = p1.ToString();
                                else
                                    pzer = p1.ToString().PadLeft(ClsDataSplitPar.ClsdirPageZero, '0');
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
                                        filename = Path.Combine(dirnamenew,
                                            p1.ToString() + "-" + p2.ToString() + "." + fs);
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
                                //每个文件夹为始1  已测完成
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
                                            filename = Path.Combine(dirnamenew,
                                                "1" + "-" + pags.ToString() + "." + fs);
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
                                            ClsDataSplitPar.ClsFileNmaecd, ClsFrmInfoPar.ConverMode, fs, 1);
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
                    if (erro==0)
                    Common.DataSplitUpdate(archid);
                } catch (Exception e) {
                    str = e.ToString();
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str + "盒号:" + box + " --> 卷号:" + archno);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                } finally {
                    try {
                        File.Delete(Downfile);
                        Directory.Delete(Path.GetDirectoryName(Downfile));
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
                        txt_gr3_1_splitPath.Text = strval;
                    if (strkey == "convxls")
                        txt_gr3_1_xlsPath.Text = strval;
                }
            } catch { }
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
            but_gr3_1_ImgPath.Text = str;
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
            if (rab_gr2_9_file_1.Checked)
                ClsWritelog.Writeini("filename", "1");
            else if (rab_gr2_9_juan_1.Checked)
                ClsWritelog.Writeini("filename", "2");
            else if (rab_gr2_9_file_ziduan.Checked)
                ClsWritelog.Writeini("filename", "3");
        }

        private void rab_gr2_9_juan_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_9_file_1.Checked)
                ClsWritelog.Writeini("filename", "1");
            else if (rab_gr2_9_juan_1.Checked)
                ClsWritelog.Writeini("filename", "2");
            else if (rab_gr2_9_file_ziduan.Checked)
                ClsWritelog.Writeini("filename", "3");
        }

        private void rab_gr2_4_dan_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_4_dan.Checked)
                ClsWritelog.Writeini("convfile", "1");
            else if (rab_gr2_4_duo.Checked)
                ClsWritelog.Writeini("convfile", "2");
            else if (rab_gr2_4_duli.Checked)
                ClsWritelog.Writeini("convfile", "3");
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
        #endregion


    }
}
