using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLjscom;

namespace Csmsjcf
{
    public partial class FrmSplit : Form
    {
        public FrmSplit()
        {
            InitializeComponent();
        }

        private Hljsimage Himg;
        #region baseInfo

        private void CombAddinfo()
        {
            comb_gr2_6_ocr.Items.Clear();
            comb_gr2_2_task.Items.Clear();
            comb_gr2_7_weizhi.Items.Clear();
            combHouseid.Items.Clear();
            comb_gr2_6_ocr.Items.Add("无");
            comb_gr2_6_ocr.Items.Add("Pro");
            comb_gr2_6_ocr.Items.Add("Adv");
            comb_gr2_6_ocr.SelectedIndex = 0;
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
                string t1 = txt_gr2_5_box1.Text.Trim();
                string t2 = txt_gr2_5_box2.Text.Trim();
                int b1 = Convert.ToInt32(t1);
                int b2 = Convert.ToInt32(t2);
                if (b1 > b2 || b1 == 0 || b2 == 0) {
                    MessageBox.Show("请检查起始盒号和终止盒号!");
                    txt_gr2_5_box1.Focus();
                    return;
                }
                string str = t1 + "-" + t2;
                if (ClsFrmInfoPar.TaskBoxCounttmp.IndexOf(str) >= 0)
                    return;
                lv_gr2_5_boxCount.Items.Add(str);
                ClsFrmInfoPar.TaskBoxCounttmp.Add(str);

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
            ClsFrmInfoPar.OneJuan = 0;
            if (rab_Gr2_3_tb.Checked || rab_Gr2_3_xls.Checked) {
                if (comb_gr2_2_task.SelectedIndex > 0) {
                    MessageBox.Show("只有在单转图像时才可以使用多任务");
                    return false;
                }
                if (ClsDataSplit.ClsExportTable.Count <= 0) {
                    MessageBox.Show("请先后台设置要导出的数据库表及字段!");
                    return false;
                }
            }
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
                ClsFrmInfoPar.OneJuan = 1;
            }
            else if (lv_gr2_5_boxCount.Items.Count <= 0) {
                MessageBox.Show("请先添加盒号范围!");
                txt_gr2_5_box1.Focus();
                return false;
            }
            if (ClsDataSplit.ClsdirDirsn == 0) {
                MessageBox.Show("文件夹命名规则不正确或请后台设置!");
                return false;
            }
            if (ClsDataSplit.ClsFilesn == 0) {
                MessageBox.Show("文件命名规则不正确或请后台设置!");
                return false;
            }
            if (ClsDataSplit.ClsdirDirsn < dirsn()) {
                MessageBox.Show("文件夹命名规则不正确或请后台设置!");
                return false;
            }

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
                if (chk_gr2_4_dou_pdf.Checked) {
                    ClsFrmInfoPar.FileFormat.Add("2pdf");
                    if (comb_gr2_6_ocr.Text.Trim().Length <= 0) {
                        MessageBox.Show("生成双层Pdf必须选择Ocr引擎!");
                        comb_gr2_6_ocr.Focus();
                        return false;
                    }
                }


            }
            if (chk_gr2_4_dou_pdf.Checked) {
                if (comb_gr2_6_ocr.Text.Trim().Length <= 0) {
                    MessageBox.Show("生成双层PDF文件必须选择OCR引擎");
                    comb_gr2_6_ocr.Focus();
                    return false;
                }
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
                    ClsFrmInfoPar.WaterStrImg = txt_gr2_7_wenzi.Text.Trim();
                }
                if (rab_gr2_7_img.Checked) {
                    if (txt_gr2_7_img.Text.Trim().Length <= 0) {
                        MessageBox.Show("生成水印图像路径不能为空!");
                        txt_gr2_7_img.Focus();
                        return false;
                    }
                    ClsFrmInfoPar.WaterStrImg = txt_gr2_7_img.Text.Trim();
                }
                if (ClsFrmInfoPar.WaterFontsize <= 0 || ClsFrmInfoPar.WaterFontColor.Trim().Length <= 0) {
                    MessageBox.Show("请设置字号或字体颜色");
                    return false;
                }
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
                ClsFrmInfoPar.FileFomat = 3;
            }
            else if (rab_gr2_4_duo.Checked) {
                if (chk_Gr2_4_jpg.Checked) {
                    MessageBox.Show("多页文件不能使用Jpg格式");
                    return false;
                }
                ClsFrmInfoPar.FileFomat = 2;
            }
            else
                ClsFrmInfoPar.FileFomat = 1;

            if (rab_gr2_8_ziduan.Checked)
                ClsFrmInfoPar.DirNamesn = 1;
            else if (rab_gr2_8_mulu.Checked)
                ClsFrmInfoPar.DirNamesn = 2;
            else
                ClsFrmInfoPar.DirNamesn = 3;

            if (rab_gr2_9_file_1.Checked) {
                if (rab_gr2_8_ziduan.Checked) {
                    MessageBox.Show("文件名此规则必须在文件夹命名规则包含目录才可用!");
                    return false;
                }
                ClsFrmInfoPar.FileNamesn = 1;
            }
            if (rab_gr2_9_file_ziduan.Checked) {
                if (ClsDataSplit.ClsFilesn != 3) {
                    MessageBox.Show("此选请先设置后台！");
                    return false;
                }
                if (!rab_gr2_8_ziduan.Checked || !rab_gr2_4_duli.Checked) {
                    MessageBox.Show("文件名为字段时：文件夹名只能为<字段格式>，图像生成格式只能是<单独文件>");
                    return false;
                }
                ClsFrmInfoPar.FileNamesn = 3;
            }
            if (rab_gr2_9_juan_1.Checked) {
                ClsFrmInfoPar.FileNamesn = 2;
            }
            if (rab_gr3_1_imgPath.Checked) {
                if (txt_gr3_1_imgPath.Text.Trim().Length <= 0) {
                    MessageBox.Show("图像源路径不能为空,或改为Ftp传输!");
                    txt_gr3_1_imgPath.Focus();
                    return false;
                }
            }
            if (txt_gr3_1_splitPath.Text.Trim().Length <= 0) {
                MessageBox.Show("请指定图像转换后的路径!");
                txt_gr3_1_splitPath.Focus();
                return false;
            }
            if (rab_Gr2_3_tb.Checked || rab_Gr2_3_xls.Checked) {
                if (txt_gr3_1_xlsPath.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择xls模版文件!");
                    txt_gr3_1_xlsPath.Focus();
                    return false;
                }
            }
            return true;
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

        private void but_gr2_5_del_Click(object sender, EventArgs e)
        {
            Delboxsn();
        }

        #endregion



        #region init
        private void ListBshowInfo(string xc, string boxsn, string archno, string str)
        {
            string log = "线程：" + xc + "--> 盒号:" + boxsn + "-->  卷号:" + archno + " --> " + str;
            lock (log) {
                this.BeginInvoke(new Action(() =>
                {
                    listB_gr3_2_log.BeginUpdate();
                    listB_gr3_2_log.Items.Add(log);
                    listB_gr3_2_log.SelectedIndex = listB_gr3_2_log.Items.Count - 1;
                    listB_gr3_2_log.EndUpdate();
                }));
            }
        }

        private void Init(string xc, string box, string arno)
        {
            DataTable dtArchNo = null;
            string str = "";
            Thread.Sleep(1000);
            if (ClsFrmInfoPar.OneJuan == 0)
                dtArchNo = ClsOperate.SelectSql(box.ToString());
            else
                dtArchNo = ClsOperate.SelectSql(box, arno);
            if (dtArchNo == null || dtArchNo.Rows.Count <= 0) {
                str = "错误：未找到已质检盒号信息或已拆分 -->盒号:" + box;
                lock (str) {
                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    ListBshowInfo(xc, "0", "0", "警告,错误线程退出");
                    return;
                }
            }
            for (int i = 0; i < dtArchNo.Rows.Count; i++) {
                string archid = dtArchNo.Rows[i][0].ToString();
                string boxsn = dtArchNo.Rows[i][1].ToString();
                string archno = dtArchNo.Rows[i][2].ToString();
                string pages = dtArchNo.Rows[i][3].ToString();
                string imgfile = dtArchNo.Rows[i][4].ToString();
                ListBshowInfo(xc, boxsn, archno, "正在查询信息");
                if (Convert.ToInt32(pages) <= 0 || imgfile.Trim().Length <= 0) {
                    str = "此卷页码不正确或数据库中文件名不正确  -->盒号:" + boxsn + " -->卷号" + archno;
                    lock (str) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                    continue;
                }
                if (ClsFrmInfoPar.ExportType == 3) {
                    ListBshowInfo(xc, boxsn, archno, "正在写入Xls信息");
                    str = ClsOperate.GetAnjuanInfo(archid, ClsOperate.XlsPath());
                    if (str.IndexOf("错误") >= 0) {
                        lock (str) {
                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                        }
                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                    }
                    continue;
                }
                else if (ClsFrmInfoPar.ExportType == 1) {
                    ListBshowInfo(xc, boxsn, archno, "正在写入Xls信息");
                    str = ClsOperate.GetAnjuanInfo(archid, ClsOperate.XlsPath());
                    if (str.IndexOf("错误") >= 0) {
                        lock (str) {
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
                    lock (str) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                    continue;
                }
                ListBshowInfo(xc, boxsn, archno, "准备进行数据转换");
                List<string> lsinfopdf = new List<string>();
                try {
                    for (int f = 0; f < ClsFrmInfoPar.FileFormat.Count; f++) {
                        string fs = ClsFrmInfoPar.FileFormat[f];
                        string dirname = "";
                        string filename = "";
                        //文件名为字段 
                        if (ClsFrmInfoPar.FileNamesn == 3) {
                            string dir = ClsOperate.GetDirColName(archid);
                            if (dir.IndexOf("错误") >= 0) {
                                lock (dir) {
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, dir);
                                }
                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                continue;
                            }
                            string file = ClsOperate.GetFileName(archid);
                            if (file.IndexOf("错误") >= 0) {
                                lock (file) {
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, file);
                                }
                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                continue;
                            }

                            dirname = Path.Combine(ClsFrmInfoPar.MimgPath, ClsFrmInfoPar.FileFormat[f], dir);
                            if (fs.IndexOf("2") >= 0)
                                filename = Path.Combine(dirname, file + "." + "pdf");
                            else
                                filename = Path.Combine(dirname, file + "." + fs);
                            if (!Directory.Exists(dirname))
                                Directory.CreateDirectory(dirname);
                            if (File.Exists(filename)) {
                                if (ClsFrmInfoPar.ConverMode == 2)
                                    File.Delete(filename);
                                else {
                                    ListBshowInfo(xc, boxsn, archno, "线程正常退出");
                                    continue;
                                }
                            }

                            if (fs.IndexOf("2") >= 0) {
                                ListBshowInfo(xc, boxsn, archno, "正在转换格式为双层pdf的Ocr单独文件");
                                str = Hljsimage._AutoPdfOcr2(Downfile, filename, ClsFrmInfoPar.Ocr);
                                if (str.IndexOf("错误") >= 0) {
                                    lock (str) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    continue;
                                }
                            }
                            else {
                                ListBshowInfo(xc, boxsn, archno, "正在转换格式为" + fs + "的单独文件");
                                if (!Directory.Exists(dirname))
                                    Directory.CreateDirectory(dirname);
                                if (File.Exists(filename)) {
                                    if (ClsFrmInfoPar.ConverMode == 2)
                                        File.Delete(filename);
                                    else {
                                        ListBshowInfo(xc, boxsn, archno, "线程正常退出");
                                        continue;
                                    }

                                }
                            }

                            str = Hljsimage._SplitImg(Downfile, filename, fs);
                            if (str.IndexOf("错误") >= 0) {
                                lock (str) {
                                    ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                }
                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                continue;
                            }
                        }

                        //文件为每案卷或每文件夹 起始1   
                        else if (ClsFrmInfoPar.FileNamesn == 2 || ClsFrmInfoPar.FileNamesn == 1) {

                            if (ClsFrmInfoPar.DirNamesn != 2) {
                                string dir = ClsOperate.GetDirColName(archid);
                                if (dir.IndexOf("错误") >= 0) {
                                    lock (dir) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, dir);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    continue;
                                }
                                dirname = Path.Combine(ClsFrmInfoPar.MimgPath, ClsFrmInfoPar.FileFormat[f], dir);
                            }
                            else
                                dirname = Path.Combine(ClsFrmInfoPar.MimgPath, ClsFrmInfoPar.FileFormat[f], boxsn, archno);
                            if (!Directory.Exists(dirname))
                                Directory.CreateDirectory(dirname);
                            DataTable dirtTable = ClsOperate.GetdirmlInfo(archid);
                            if (dirtTable == null || dirtTable.Rows.Count <= 0) {
                                str = "未找到文件夹命名规则目录信息  -->盒号:" + boxsn + " -->卷号" + archno;
                                lock (str) {
                                    lock (str) {
                                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                    }
                                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                    continue;
                                }
                            }

                            for (int d = 0; d < dirtTable.Rows.Count; d++) {
                                int p1 = 0;
                                int p2 = 0;
                                string ml = dirtTable.Rows[d][0].ToString();
                                p1 = Convert.ToInt32(dirtTable.Rows[d][1].ToString());
                                try {
                                    p2 = Convert.ToInt32(dirtTable.Rows[d + 1][1].ToString());
                                    p2 -= 1;
                                } catch {
                                    p2 = Convert.ToInt32(pages);
                                }

                                //每卷为1
                                if (ClsFrmInfoPar.FileNamesn == 2) {
                                    //为多页时 正测
                                    if (ClsFrmInfoPar.FileFomat == 2) {
                                        string dirnamenew = "";
                                        if (ClsFrmInfoPar.DirNamesn == 1) {
                                            dirnamenew = dirname;
                                        }
                                        else if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            dirnamenew = Path.Combine(dirname, ml);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                        }
                                        filename = Path.Combine(dirnamenew,
                                            p1.ToString() + "-" + p2.ToString() + "." + fs);
                                        if (File.Exists(filename)) {
                                            if (ClsFrmInfoPar.ConverMode == 2)
                                                File.Delete(filename);
                                            else {
                                                ListBshowInfo(xc, boxsn, archno, "线程正常退出");
                                                continue;
                                            }
                                        }
                                        ListBshowInfo(xc, boxsn, archno,
                                              "正在进行数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                        lsinfopdf = Himg._SplitImgls(Downfile, filename, p1, p2, fs);
                                        if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                            ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                            continue;
                                        }
                                        if (fs.IndexOf("2") >= 0) {
                                            for (int pdf = 0; pdf < lsinfopdf.Count; pdf++) {
                                                string yfile = lsinfopdf[pdf];
                                                ListBshowInfo(xc, boxsn, archno, "正在进行Ocr数据转换页");
                                                str = Hljsimage._AutoPdfOcr2(yfile, filename, 1);
                                                if (str.IndexOf("错误") >= 0) {
                                                    str = Hljsimage._AutoPdfOcr2(yfile, filename, 2);
                                                    if (str.IndexOf("错误") >= 0) {
                                                        lock (str) {
                                                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                                str + "盒号:" + box + " --> 卷号:" + archno);
                                                        }
                                                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                        continue;
                                                    }

                                                }
                                            }

                                        }
                                    }
                                    //为单页时  已测完成
                                    else if (ClsFrmInfoPar.FileFomat == 1) {
                                        //文件夹为目录 已测完成
                                        string dirnamenew = "";
                                        if (ClsFrmInfoPar.DirNamesn == 1) {
                                            dirnamenew = dirname;
                                        }
                                        else if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            dirnamenew = Path.Combine(dirname, ml);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                        }
                                        ListBshowInfo(xc, boxsn, archno,
                                                "正在进行数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                        str = Hljsimage._SplitImg(Downfile, dirnamenew, p1, p2,
                                            ClsDataSplit.ClsFileNameQian, ClsDataSplit.ClsFileNameHou,
                                            ClsDataSplit.ClsFileNmaecd, ClsFrmInfoPar.ConverMode, fs, 0);
                                        if (str.IndexOf("错误") >= 0) {
                                            lock (str) {
                                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str);
                                            }
                                            ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                            continue;
                                        }
                                        if (fs.IndexOf("2") >= 0) {
                                            for (int pdf = 0; pdf < lsinfopdf.Count; pdf++) {
                                                string yfile = lsinfopdf[pdf];
                                                ListBshowInfo(xc, boxsn, archno, "正在进行Ocr数据转换页");
                                                str = Hljsimage._AutoPdfOcr2(yfile, filename, 1);
                                                if (str.IndexOf("错误") >= 0) {
                                                    str = Hljsimage._AutoPdfOcr2(yfile, filename, 2);
                                                    if (str.IndexOf("错误") >= 0) {
                                                        lock (str) {
                                                            ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                                str + "盒号:" + box + " --> 卷号:" + archno);
                                                        }
                                                        ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                        continue;
                                                    }

                                                }
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
                                            string dirnamenew = Path.Combine(dirname, ml);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                            int pags = p2 - p1;
                                            if (pags == 0)
                                                pags = 1;
                                            filename = Path.Combine(dirnamenew,
                                                "1" + "-" + pags.ToString() + "." + fs);
                                            if (File.Exists(filename)) {
                                                if (ClsFrmInfoPar.ConverMode == 2)
                                                    File.Delete(filename);
                                                else {
                                                    ListBshowInfo(xc, boxsn, archno, "线程正常退出");
                                                    continue;
                                                }
                                            }

                                            ListBshowInfo(xc, boxsn, archno,
                                                "正在进行数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                            lsinfopdf = Himg._SplitImgls(Downfile, filename, p1, p2, fs);
                                            if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                continue;
                                            }

                                            if (fs.IndexOf("2") >= 0) {
                                                for (int pdf = 0; pdf < lsinfopdf.Count; pdf++) {
                                                    string yfile = lsinfopdf[pdf];
                                                    ListBshowInfo(xc, boxsn, archno, "正在进行Ocr数据转换页");
                                                    str = Hljsimage._AutoPdfOcr2(yfile, filename, 1);
                                                    if (str.IndexOf("错误") >= 0) {
                                                        str = Hljsimage._AutoPdfOcr2(yfile, filename, 2);
                                                        if (str.IndexOf("错误") >= 0) {
                                                            lock (str) {
                                                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                                    str + "盒号:" + box + " --> 卷号:" + archno);
                                                            }
                                                            ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                            continue;
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }
                                    //为单页时   已测完成
                                    else if (ClsFrmInfoPar.FileFomat == 1) {
                                        //文件夹为目录   已测完成
                                        if (ClsFrmInfoPar.DirNamesn == 2 || ClsFrmInfoPar.DirNamesn == 3) {
                                            string dirnamenew = Path.Combine(dirname, ml);
                                            if (!Directory.Exists(dirnamenew))
                                                Directory.CreateDirectory(dirnamenew);
                                            ListBshowInfo(xc, boxsn, archno,
                                                "正在进行数据转换页：" + p1.ToString() + "-" + p2.ToString());
                                            lsinfopdf = Himg._SplitImgls(Downfile, dirnamenew, p1, p2,
                                                ClsDataSplit.ClsFileNameQian, ClsDataSplit.ClsFileNameHou,
                                                ClsDataSplit.ClsFileNmaecd, ClsFrmInfoPar.ConverMode, fs, 1);
                                            if (!ClsOperate.Iserror(lsinfopdf, ClsFrmInfoPar.LogPath)) {
                                                ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                continue;
                                            }
                                            if (fs.IndexOf("2") >= 0) {
                                                for (int pdf = 0; pdf < lsinfopdf.Count; pdf++) {
                                                    string yfile = lsinfopdf[pdf];
                                                    ListBshowInfo(xc, boxsn, archno, "正在进行Ocr数据转换页");
                                                    str = Hljsimage._AutoPdfOcr2(yfile, filename, 1);
                                                    if (str.IndexOf("错误") >= 0) {
                                                        str = Hljsimage._AutoPdfOcr2(yfile, filename, 2);
                                                        if (str.IndexOf("错误") >= 0) {
                                                            lock (str) {
                                                                ClsWritelog.Writelog(ClsFrmInfoPar.LogPath,
                                                                    str + "盒号:" + box + " --> 卷号:" + archno);
                                                            }
                                                            ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                                                            continue;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                } catch (Exception e) {
                    str = e.ToString();
                    lock (str) {
                        ClsWritelog.Writelog(ClsFrmInfoPar.LogPath, str + "盒号:" + box + " --> 卷号:" + archno);
                    }
                    ListBshowInfo(xc, boxsn, archno, "警告,错误线程退出");
                } finally {
                    ListBshowInfo(xc, boxsn, archno, "线程退出");
                }

            }
        }


        private void StartTask()
        {
            for (int i = 0; i <= ClsFrmInfoPar.Taskxc; i++) {
                lock (ClsFrmInfoPar.TaskBoxCount) {
                    Thread.Sleep(1000);
                    if (ClsFrmInfoPar.TaskBoxCount.Count > 0) {
                        string b = ClsFrmInfoPar.TaskBoxCount[0];
                        ClsFrmInfoPar.TaskBoxCount.RemoveAt(0);
                        ThreadPool.QueueUserWorkItem(h =>
                            {
                                Init((i + 1).ToString(), b, "");
                                Thread.Sleep(1000);
                                StartTask();
                            });
                    }
                }
            }
        }

        #endregion

        private void butStart_Click(object sender, EventArgs e)
        {
            if (!IsTxtInfo())
                return;
            if (ClsFrmInfoPar.OneJuan == 1) {
                Task.Run(() => { Init("1", txt_gr2_5_box1.Text.Trim(), txt_gr2_5_juan.Text.Trim()); });
                return;
            }
            else {
                if (!ClsOperate.AddTask())
                    return;
                Action Act = StartTask;
                Act.BeginInvoke(null, null);
            }

        }

        #region FrmInfoPar

        private void FrmSplit_Shown(object sender, EventArgs e)
        {
            CombAddinfo();
            ClsInIPar.Getgzinfo();
            Himg = new Hljsimage();
        }
        private void butLog_Click(object sender, EventArgs e)
        {
            string file = Path.Combine(ClsFrmInfoPar.LogPath, "log.txt");
            if (File.Exists(file)) {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(file);
            }
            else MessageBox.Show("日志文件不存在!");

        }
        private void comb_gr2_2_task_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsFrmInfoPar.Taskxc = comb_gr2_2_task.SelectedIndex;
        }
        private void but_gr3_1_Xls_Click(object sender, EventArgs e)
        {
            string str = "";
            if (FdigXls.ShowDialog() == DialogResult.OK)
                str = FdigXls.FileName;
            else
                str = "";
            txt_gr3_1_xlsPath.Text = str;
            ClsFrmInfoPar.XlsPath = str;
        }

        private void but_gr3_1_ImgToPath_Click(object sender, EventArgs e)
        {
            string str = "";
            if (fBdigImgPath.ShowDialog() == DialogResult.OK)
                str = fBdigImgPath.SelectedPath;
            else
                str = "";
            txt_gr3_1_splitPath.Text = str;
            ClsFrmInfoPar.MimgPath = str;
        }

        private void but_gr3_1_ImgPath_Click(object sender, EventArgs e)
        {
            string str = "";
            if (fBdigImgPath.ShowDialog() == DialogResult.OK)
                str = fBdigImgPath.SelectedPath;
            else
                str = "";
            but_gr3_1_ImgPath.Text = str;
            ClsFrmInfoPar.YimgPath = str;
        }


        private void combHouseid_SelectedIndexChanged(object sender, EventArgs e)
        {
            V_HouseSet v_house = combHouseid.SelectedItem as V_HouseSet;
            ClsFrmInfoPar.Houseid = v_house.HouseID;
        }


        private void rab_gr2_1_Zengliang_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_1_Zengliang.Checked)
                ClsFrmInfoPar.ConverMode = 1;
            else ClsFrmInfoPar.ConverMode = 2;
        }

        private void rab_Gr2_3_tb_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_Gr2_3_tb.Checked)
                ClsFrmInfoPar.ExportType = 1;
            else if (rab_Gr2_3_img.Checked)
                ClsFrmInfoPar.ExportType = 2;
            else ClsFrmInfoPar.ExportType = 3;
        }
      

        private void comb_gr2_6_ocr_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsFrmInfoPar.Ocr = comb_gr2_6_ocr.SelectedIndex;
        }

        private void rab_gr2_7_wu_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_7_wu.Checked) {
                comb_gr2_7_weizhi.Enabled = true;
                ClsFrmInfoPar.Watermark = 1;
            }
            else if (rab_gr2_7_wenzi.Checked)
                ClsFrmInfoPar.Watermark = 2;
            else
                ClsFrmInfoPar.Watermark = 3;
            comb_gr2_7_weizhi.Enabled = false;
        }

        private void rab_gr2_4_dian_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_4_dan.Checked) {
                ClsFrmInfoPar.FileFomat = 1;
                return;
            }
            else if (rab_gr2_4_duo.Checked)
                ClsFrmInfoPar.FileFomat = 2;
            else
                ClsFrmInfoPar.FileNamesn = 3;
            chk_Gr2_4_jpg.Checked = false;
        }

        private void rab_gr3_1_ftp_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr3_1_ftp.Checked)
                ClsFrmInfoPar.Ftp = 1;
            else
                ClsFrmInfoPar.Ftp = 2;
        }

        private void rab_gr2_4_duo_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_4_dan.Checked) {
                ClsFrmInfoPar.FileFomat = 1;
                return;
            }
            else if (rab_gr2_4_duo.Checked)
                ClsFrmInfoPar.FileFomat = 2;
            else
                ClsFrmInfoPar.FileNamesn = 3;
            chk_Gr2_4_jpg.Checked = false;
        }

        private void rab_Gr2_3_img_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_Gr2_3_tb.Checked)
                ClsFrmInfoPar.ExportType = 1;
            else if (rab_Gr2_3_img.Checked)
                ClsFrmInfoPar.ExportType = 2;
            else ClsFrmInfoPar.ExportType = 3;
        }

        private void rab_gr2_7_wenzi_CheckedChanged(object sender, EventArgs e)
        {
            if (rab_gr2_7_wu.Checked) {
                comb_gr2_7_weizhi.Enabled = true;
                ClsFrmInfoPar.Watermark = 1;
            }
            else if (rab_gr2_7_wenzi.Checked)
                ClsFrmInfoPar.Watermark = 2;
            else
                ClsFrmInfoPar.Watermark = 3;
            comb_gr2_7_weizhi.Enabled = false;
        }



        #endregion

      
    }
}
