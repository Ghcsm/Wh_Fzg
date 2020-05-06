using DAL;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Csmdady
{
    public partial class FrmPrint : Form
    {
        public FrmPrint()
        {
            InitializeComponent();
        }
        private void FrmPrint_Load(object sender, EventArgs e)
        {
            CreatTxt();
            ClsPrintInfoDocument.printDocument.PrintPage += PrintDocument_PrintPage;
            // ClsPrintInfoDocument.PrintConten.PrintPage += PrintConten_PrintPage;
        }
        private void FrmPrint_Shown(object sender, EventArgs e)
        {
            GetPrintParmXy();
            GetContenInfo();
        }

        #region CretaTxt

        private void cle()
        {
            ClsPrintInfo.PrintTable = "";
            ClsPrintInfo.Tagid = 0;
            ClsPrintInfo.lsinfoShow.Clear();
            ClsPrintInfo.lsinfoXy.Clear();
            ClsPrintInfo.lsSpecCol.Clear();
            ClsPrintInfo.lsSpecFont.Clear();
            ClsPrintInfo.lsSpecFontcolo.Clear();
            ClsPrintInfo.lsSpecFontbuld.Clear();
            ClsPrintInfo.lsSpecFontsize.Clear();
            ClsPrintInfo.PrintXy.Clear();
            ClsPrintInfo.lsSpecFontcolshow.Clear();
            ClsPrintInfo.lsSpecFontLine.Clear();

        }


        private void CreatTxt()
        {
            cle();
            ClsPrintInfo.PrintInfo = T_Sysset.GetGensetPrint();
            if (ClsPrintInfo.PrintInfo == null || ClsPrintInfo.PrintInfo.Rows.Count <= 0)
                return;
            DataRow dr = ClsPrintInfo.PrintInfo.Rows[0];
            ClsPrintInfo.PrintTable = dr["printTable"].ToString();
            string strxy = dr["PrintxyCol"].ToString();
            string strcol = dr["PrintColInfo"].ToString();
            string fontall = dr["PrintFontColAll"].ToString();
            string fontspec = dr["PrintFontSpec"].ToString();
            if (strcol.Length > 0) {
                string[] col = strcol.Split(';');
                for (int i = 0; i < col.Length; i++) {
                    string str = col[i];
                    CreateTxtShow(i, str);
                    ClsPrintInfo.lsinfoShow.Add(str);
                }
            }
            if (strxy.Length > 0) {
                string[] xy = strxy.Split(';');
                for (int i = 0; i < xy.Length; i++) {
                    string str = xy[i];
                    CreateTxtXy(i, str);
                    ClsPrintInfo.lsinfoXy.Add(str);
                }
            }
            if (fontspec.Length > 0) {
                string[] spec = fontspec.Split(';');
                for (int i = 0; i < spec.Length; i++) {
                    string str = spec[i];
                    if (str.Trim().Length > 0) {
                        string[] a = str.Split(':');
                        ClsPrintInfo.lsSpecCol.Add(a[0].ToString());
                        ClsPrintInfo.lsSpecFont.Add(a[1].ToString());
                        ClsPrintInfo.lsSpecFontcolo.Add(a[2].ToString());
                        ClsPrintInfo.lsSpecFontsize.Add(a[3].ToString());
                        ClsPrintInfo.lsSpecFontbuld.Add(a[4].ToString());
                        ClsPrintInfo.lsSpecFontcolshow.Add(a[5].ToString());
                        ClsPrintInfo.lsSpecFontLine.Add(a[6].ToString());
                    }
                }
            }
            if (fontall.Length > 0) {
                string[] str = fontall.Split(':');
                ClsPrintInfo.FontName = str[0];
                ClsPrintInfo.FontColor = str[4];
                ClsPrintInfo.Fontsize = Convert.ToInt32(str[2]);
                ClsPrintInfo.FontBold = str[3];
            }
        }

        private void CreateTxtShow(int id, string strname)
        {
            id += 1;
            Label lb = new Label();
            lb.Name = "lb" + id.ToString();
            lb.Text = strname + " : ";
            lb.Width = 80;
            lb.SendToBack();
            lb.BackColor = Color.Transparent;
            lb.Location = new Point(24, 5 + id * 30);
            panePrintInfoShow.Controls.Add(lb);
            TextBox txt = new TextBox();
            txt.Name = strname;
            txt.Width = 350;
            txt.ReadOnly = true;
            txt.TabIndex = id;
            txt.Tag = id;
            txt.BringToFront();
            txt.Location = new Point(110, 2 + id * 30);
            panePrintInfoShow.Controls.Add(txt);
        }
        private void CreateTxtXy(int id, string strname)
        {
            id += 1;
            int xx1 = 0;
            int yy1 = 0;
            int xx = 0;
            int yy = 0;
            if (ClsPrintInfo.x == 0 || ClsPrintInfo.y == 0) {
                if (id % 2 == 0) {
                    ClsPrintInfo.y = 1;
                    yy = ClsPrintInfo.y + 10;
                }
                else {
                    ClsPrintInfo.x = 1;
                    xx = ClsPrintInfo.x + 10;
                }
            }
            else {
                if (id % 2 == 0)
                    yy = ClsPrintInfo.y + 30;
                else
                    xx = ClsPrintInfo.x + 30;
            }
            Label lb = new Label();
            lb.Name = "lb" + id.ToString();
            lb.Text = strname + " : ";
            lb.ForeColor = Color.Red;
            lb.Width = 80;
            lb.SendToBack();
            lb.BackColor = Color.Transparent;
            if (id % 2 == 0) {
                lb.Location = new Point(238, yy);
                panelPrintXY.Controls.Add(lb);
            }
            else {
                lb.Location = new Point(24, xx);
                panelPrintXY.Controls.Add(lb);
            }

            for (int i = 1; i <= 2; i++) {
                ClsPrintInfo.Tagid += 1;
                lb = new Label();
                lb.Name = "xy" + i.ToString();
                if (i == 1)
                    lb.Text = "X坐标:";
                else
                    lb.Text = "Y坐标:";
                lb.Width = 50;
                lb.SendToBack();
                lb.BackColor = Color.Transparent;
                if (id % 2 == 0) {
                    yy1 = yy + i * 25;
                    lb.Location = new Point(248, yy1);
                }
                else {
                    xx1 = xx + i * 25;
                    lb.Location = new Point(34, xx1);
                }
                TextBox txt = new TextBox();
                txt.Name = "txt" + id.ToString() + i.ToString();
                txt.Width = 80;
                txt.TabIndex = ClsPrintInfo.Tagid;
                txt.Tag = ClsPrintInfo.Tagid;
                txt.KeyPress += Txt_KeyPress;
                txt.BringToFront();
                if (id % 2 == 0) {
                    txt.Location = new Point(317, yy1 - 5);
                    panelPrintXY.Controls.Add(lb);
                    panelPrintXY.Controls.Add(txt);
                }
                else {
                    txt.Location = new Point(105, xx1 - 5);
                    panelPrintXY.Controls.Add(lb);
                    panelPrintXY.Controls.Add(txt);
                }
            }
            if (id % 2 == 0)
                ClsPrintInfo.y = yy1;
            else
                ClsPrintInfo.x = xx1;
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        #endregion

        #region GetPrintParm
        private void butPrintXyinfo_Click(object sender, EventArgs e)
        {
            SavePrintParm();
        }
        private void SavePrintParm()
        {
            try {
                if (!istxt(panelPrintXY)) {
                    MessageBox.Show("请输入相关坐标!");
                    return;
                }
                string xy = "";
                int a = 0;
                foreach (var item in ClsPrintInfo.PrintXy) {
                    a += 1;
                    if (a != ClsPrintInfo.PrintXy.Count) {
                        if (a % 2 != 0)
                            xy += item.Key + ":" + item.Value + ";";
                        else
                            xy += item.Key + ":" + item.Value + ";";
                    }
                    else {
                        xy += item.Key + ":" + item.Value;
                    }
                }
                Common.SavePrintParmXy(xy);
                MessageBox.Show("保存完成!");
            } catch (Exception e) {
                MessageBox.Show("保存失败" + e);
            }

        }

        private bool istxt(Panel pl)
        {
            if (ClsPrintInfo.PrintXy != null)
                ClsPrintInfo.PrintXy.Clear();
            Dictionary<int, string> dic1 = new Dictionary<int, string>();
            bool tf = true;
            foreach (Control c in pl.Controls) {
                if (c is TextBox) {
                    if (c.Text.Trim().Length > 0)
                        dic1.Add((int)c.Tag, c.Text.Trim());
                    else
                        tf = false;
                }
            }
            ClsPrintInfo.PrintXy = dic1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            return tf;
        }

        private void GetPrintParmXy()
        {
            string str = Common.GetPrintParmXy();
            if (str.Trim().Length > 0) {
                if (ClsPrintInfo.PrintXy != null)
                    ClsPrintInfo.PrintXy.Clear();
                string[] a = str.Trim().Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    if (b.Trim().Length > 0) {
                        string[] c = b.Split(':');
                        int tag = Convert.ToInt32(c[0]);
                        string txt = c[1];
                        ClsPrintInfo.PrintXy.Add(tag, txt);
                        SettxtXy(tag, txt);
                    }
                }
            }
        }
        private void SettxtXy(int tag, string txt)
        {
            foreach (Control c in panelPrintXY.Controls) {
                if (c is TextBox) {
                    if (c.Tag.ToString() == tag.ToString())
                        c.Text = txt;
                }
            }
        }
        private void SettxtInfo(string col, string txt)
        {
            foreach (Control c in panePrintInfoShow.Controls) {
                if (c is TextBox) {
                    if (c.Name == col)
                        c.Text = txt;
                }
            }
        }
        private void GetPrintinfoSql()
        {
            if (ClsPrintInfo.Archid <= 0)
                return;
            int stat = Common.GetArchWorkState(ClsPrintInfo.Archid);
            if (stat < (int)T_ConFigure.ArchStat.质检完) {
                labStat.Visible = true;
                return;
            }
            labStat.Visible = false;
            DataTable dt = Common.GetPrintInfoDataTable(ClsPrintInfo.PrintTable, ClsPrintInfo.lsinfoShow, ClsPrintInfo.Archid);
            if (dt != null && dt.Rows.Count > 0) {
                ClsPrintInfo.ArchInfoDataTable = dt;
                DataRow dr = dt.Rows[0];
                for (int i = 0; i < ClsPrintInfo.lsinfoShow.Count; i++) {
                    string str = ClsPrintInfo.lsinfoShow[i];
                    string a = dr[str].ToString();
                    SettxtInfo(str, a);
                }
                return;
            }
        }

        private bool GetPrintinfoSql(int archid)
        {
            if (archid <= 0)
                return false;
            int stat = Common.GetArchWorkState(archid);
            if (stat < (int)T_ConFigure.ArchStat.质检完)
                return false;
            DataTable dt = Common.GetPrintInfoDataTable(ClsPrintInfo.PrintTable, ClsPrintInfo.lsinfoShow, archid);
            if (dt == null && dt.Rows.Count <= 0)
                return false;
            ClsPrintInfo.ArchInfoDataTable = dt;
            return true;
        }

        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            ClsPrintInfo.Archid = gArchSelect1.Archid;
            ClsPrintInfo.Boxsn = gArchSelect1.Boxsn;
            GetPrintinfoSql();
        }


        #endregion

        #region GetContenParm


        private void GetContenInfo()
        {
            if (ClsPrintInfo.PrintInfo == null || ClsPrintInfo.PrintInfo.Rows.Count <= 0)
                return;
            DataRow dr = ClsPrintInfo.PrintInfo.Rows[0];
            ClsPrintConten.PrintContenTable = dr["PrintContenTable"].ToString();
            string str = dr["PrintContenInfo"].ToString();
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    string[] c = b.Split(':');
                    if (c.Length > 0) {
                        if (c[0].IndexOf("SN0") >= 0) {
                            string[] f = b.Split(':');
                            ClsPrintConten.PrintContenSn = f[1];
                        }
                        else {
                            string d = c[3];
                            if (d == "True") {
                                ClsPrintConten.PrintContenPagesn = c[0];
                                ClsPrintConten.PrintContenPageMode = Convert.ToInt32(c[4]);
                            }
                            ClsPrintConten.PrintContenCol.Add(c[0]);
                            ClsPrintConten.printContenXls.Add(c[1]);
                            ClsPrintConten.PrintContenDz.Add(c[2]);
                            ClsPrintConten.PrintContenPage.Add(c[3]);
                        }
                    }
                    ClsPrintConten.PrintContenAll.Add(b);
                }
            }
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            string file = "打印日志.txt";
            string filepath = Path.Combine(Application.StartupPath, file);
            if (!File.Exists(filepath)) {
                MessageBox.Show("日志文件不存在！");
            }
            else {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(filepath);
            }
        }

        #endregion

        #region printinfo

        private async void butPrintInfo_Click(object sender, EventArgs e)
        {
            if (ClsPrintInfo.Archid <= 0)
                return;
            bool x = await PrintInfo();
            if (x) {
                MessageBox.Show("打印完成!");
                lbInfo.Text = "";
                butBoxRangeAdd.Enabled = true;
                butDelbox.Enabled = true;
                return;
            }
            MessageBox.Show("打印失败!");
            butBoxRangeAdd.Enabled = true;
            butDelbox.Enabled = true;
        }

        private void WriteLog(string str)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string dt = DateTime.Now.ToString();
            try {
                string file = "打印日志.txt";
                string filepath = Path.Combine(Application.StartupPath, file);
                if (!File.Exists(filepath)) {
                    fs = new FileStream(filepath, FileMode.Create);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(str + " 操作时间 " + dt);
                    sw.Flush();
                }
                else {
                    fs = new FileStream(filepath, FileMode.Append);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(str + " 操作时间 " + dt);
                    sw.Flush();
                }
            } catch { } finally {
                sw.Close();
                fs.Close();
            }
        }

        private Task<bool> PrintInfo()
        {
            butBoxRangeAdd.Enabled = false;
            butDelbox.Enabled = false;
            return Task.Run(() =>
             {
                 if (tabSelectbox.IsSelected) {
                     if (rbboxOne.Checked)
                         ClsPrintInfoDocument.printDocument.Print();
                     else {
                         if (ClsPrintInfo.Boxsn <= 0) {
                             string s1 = "盒号:" + ClsPrintInfo.Boxsn + "获取失败!";
                             WriteLog(s1);
                             return false;
                         }
                         DataTable dt = Common.GetboxArchno(ClsPrintInfo.Boxsn);
                         if (dt == null || dt.Rows.Count <= 0) {
                             string str = "盒号:" + ClsPrintInfo.Boxsn + "信息获取失败!";
                             WriteLog(str);
                             return false;
                         }
                         for (int id = 1; id < dt.Rows.Count; id++) {
                             int arid = Convert.ToInt32(dt.Rows[id][0].ToString());
                             if (GetPrintinfoSql(arid)) {
                                 Thread.Sleep(200);
                                 ClsPrintInfoDocument.printDocument.Print();
                             }
                         }
                     }
                     return true;
                 }

                 else {
                     lock (ClsPrintInfo.Lsitems) {
                         try {
                             for (int i = 0; i < lvboxRange.Items.Count; i++) {
                                 string[] str = ClsPrintInfo.Lsitems[i].Split('-');
                                 int a = Convert.ToInt32(str[0]);
                                 int b = Convert.ToInt32(str[1]);
                                 for (int box = a; box <= b; box++) {
                                     DataTable dt = Common.GetboxArchno(box);
                                     if (dt == null || dt.Rows.Count <= 0)
                                         continue;
                                     for (int id = 1; id < dt.Rows.Count; id++) {
                                         int arid = Convert.ToInt32(dt.Rows[id][0].ToString());
                                         if (GetPrintinfoSql(arid)) {
                                             this.BeginInvoke(new Action(() => { lbInfo.Text = string.Format("正在打印第{0}盒", box); }));
                                             Thread.Sleep(200);
                                             ClsPrintInfoDocument.printDocument.Print();
                                         }
                                     }
                                 }
                             }
                             return true;
                         } catch (Exception e) {
                             WriteLog(e.ToString());
                             return false;
                         }
                     }
                 }
             });
        }
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            for (int i = 0; i < ClsPrintInfo.lsinfoXy.Count; i++) {
                string col = ClsPrintInfo.lsinfoXy[i];
                DataRow dr = ClsPrintInfo.ArchInfoDataTable.Rows[0];
                string str = dr[col].ToString();
                int a = ClsPrintInfo.lsSpecCol.IndexOf(col);
                int x = Convert.ToInt32(ClsPrintInfo.PrintXy.ElementAt(i).Value);
                int y = Convert.ToInt32(ClsPrintInfo.PrintXy.ElementAt(i + 1).Value);
                if (a < 0)
                    e.Graphics.DrawString(str,
                        new System.Drawing.Font(new FontFamily(ClsPrintInfo.FontName), Convert.ToInt32(ClsPrintInfo.Fontsize), FontStyle.Bold),
                        new SolidBrush(Color.FromArgb(Convert.ToInt32(ClsPrintInfo.FontColor))), Convert.ToSingle(x), Convert.ToSingle(y));

                else {
                    string c = "";
                    if (!Convert.ToBoolean(ClsPrintInfo.lsSpecFontcolshow[a]))
                        c = str;
                    else
                        c = col + ":" + str;
                    e.Graphics.DrawString(c,
                        new System.Drawing.Font(new FontFamily(ClsPrintInfo.lsSpecFont[a]),
                            Convert.ToInt32(ClsPrintInfo.lsSpecFontsize[a]),
                            (FontStyle)Convert.ToInt32(ClsPrintInfo.lsSpecFontbuld[a])),
                        new SolidBrush(Color.FromArgb(Convert.ToInt32(ClsPrintInfo.lsSpecFontcolo[a]))),
                        Convert.ToSingle(x), Convert.ToSingle(y));
                }
            }
        }

        private bool isNum()
        {
            if (txtBox1.Text.Trim().Length <= 0 || txtBox2.Text.Trim().Length <= 0)
                return false;
            try {
                int a = Convert.ToInt32(txtBox1.Text.Trim());
                int b = Convert.ToInt32(txtBox2.Text.Trim());
                if (a == 0 || b == 0)
                    return false;
                if (a > b)
                    return false;
                return true;
            } catch {
                return false;
            }
        }

        private void AddlvboxRange(string a, string b)
        {
            string str = a + "-" + b;
            if (ClsPrintInfo.Lsitems.IndexOf(str) >= 0) {
                MessageBox.Show("此盒号范围已存在!");
                return;
            }
            ClsPrintInfo.Lsitems.Add(str);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = (lvboxRange.Items.Count + 1).ToString();
            lvi.SubItems.AddRange(new[] { a, b });
            lvboxRange.Items.Add(lvi);
        }
        private void butBoxRangeAdd_Click(object sender, EventArgs e)
        {
            if (!isNum()) {
                MessageBox.Show("请检查盒号范围!");
                txtBox1.Focus();
                return;
            }
            AddlvboxRange(txtBox1.Text.Trim(), txtBox2.Text.Trim());
        }

        private void butDelbox_Click(object sender, EventArgs e)
        {
            if (lvboxRange.SelectedItems.Count <= 0 || lvboxRange.SelectedIndices.Count < 0)
                return;
            int id = lvboxRange.SelectedItems[0].Index;
            lvboxRange.Items.RemoveAt(id);
            ClsPrintInfo.Lsitems.RemoveAt(id);
        }

        #endregion

        #region printxls
        private async void butPrintConten_Click(object sender, EventArgs e)
        {
            if (!IsXls()) {
                MessageBox.Show("本程序路径下找不到名为<目录模版.xlsx> 的模版文件");
                return;
            }
            bool x = await PrintConten();
            if (x) {
                butBoxRangeAdd.Enabled = true;
                butDelbox.Enabled = true;
                MessageBox.Show("目录打印完成!");
                lbInfo.Text = "";
                return;
            }
            butBoxRangeAdd.Enabled = true;
            butDelbox.Enabled = true;
            MessageBox.Show("目录打印失败!");
        }

        private bool IsXls()
        {
            string strxls = Path.Combine(Application.StartupPath, "目录模版.xlsx");
            if (!File.Exists(strxls))
                return false;
            if (tabItemboxRange.IsSelected) {
                if (lvboxRange.Items.Count <= 0)
                    return false;
            }

            if (TabBoxsnArchno.IsSelected) {
                if (txtBosn.Text.Trim().Length <= 0 || txtArchno.Text.Trim().Length <= 0 || txtArchno2.Text.Trim().Length <= 0) {
                    MessageBox.Show("盒号或卷号不能为空!");
                    txtBosn.Focus();
                }

                int b, b1, b2;
                bool bl = int.TryParse(txtBosn.Text.Trim(), out b);
                if (!bl || b <= 0) {
                    MessageBox.Show("盒号不正确!");
                    return false;
                }
                bool b11 = int.TryParse(txtArchno.Text.Trim(), out b1);
                bool b22 = int.TryParse(txtArchno2.Text.Trim(), out b2);
                if (!b11 || !b22) {
                    MessageBox.Show("卷号不正确!");
                    return false;
                }
                if (b1 > b2) {
                    MessageBox.Show("起始卷号不能于小终止卷号!");
                    return false;
                }
            }
            return true;
        }

        private Task<bool> PrintConten()
        {
            butBoxRangeAdd.Enabled = false;
            butDelbox.Enabled = false;
            return Task.Run(() =>
             {
                 if (tabSelectbox.IsSelected) {
                     if (rbboxOne.Checked)
                         return GetArchContenWriteXlsPrint(ClsPrintInfo.Archid);
                     else {
                         if (ClsPrintInfo.Boxsn <= 0) {
                             MessageBox.Show("盒号获取失败!");
                             return false;
                         }
                         DataTable dt = Common.GetboxArchno(ClsPrintInfo.Boxsn);
                         if (dt == null || dt.Rows.Count <= 0) {
                             string str = "盒号：" + ClsPrintInfo.Boxsn + "信息获取失败!";
                             WriteLog(str);
                             return false;
                         }
                         for (int id = 1; id <= dt.Rows.Count; id++) {
                             int arid = Convert.ToInt32(dt.Rows[id][0].ToString());
                             GetArchContenWriteXlsPrint(arid);
                             Thread.Sleep(200);
                             //ClsPrintInfoDocument.printDocument.Print();
                         }
                     }
                     return true;
                 }
                 else if (tabItemboxRange.IsSelected) {
                     int arid = 0;
                     try {
                         for (int i = 0; i < lvboxRange.Items.Count; i++) {
                             int a = Convert.ToInt32(lvboxRange.Items[i].SubItems[1].Text);
                             int b = Convert.ToInt32(lvboxRange.Items[i].SubItems[2].Text);
                             for (int box = a; box <= b; box++) {
                                 DataTable dt = Common.GetboxArchno(box);
                                 if (dt == null || dt.Rows.Count <= 0)
                                     continue;
                                 for (int id = 1; id <= dt.Rows.Count; id++) {
                                     arid = Convert.ToInt32(dt.Rows[id][0].ToString());
                                     GetArchContenWriteXlsPrint(arid);
                                     this.BeginInvoke(new Action(() => { lbInfo.Text = string.Format("正在打印第{0}盒", box); }));
                                     Thread.Sleep(200);
                                     //ClsPrintInfoDocument.printDocument.Print();
                                 }
                             }
                         }
                         this.BeginInvoke(new Action(() => { lbInfo.Text = string.Format("打印完成"); }));
                         return true;
                     } catch (Exception e) {
                         WriteLog("ID:号" + arid.ToString() + e.ToString());
                         return false;
                     }
                 }
                 else {
                     int arid = 0;
                     try
                     {
                         bool bl = chkCq.Checked;
                         int boxsn = Convert.ToInt32(txtBosn.Text.Trim());
                         int box1 = Convert.ToInt32(txtArchno.Text.Trim());
                         int box2 = Convert.ToInt32(txtArchno2.Text.Trim());
                         for (int i = box1; i <= box2; i++) {
                             DataTable dt = Common.GetboxArchnoinfo(boxsn, i, bl);
                             if (dt == null || dt.Rows.Count <= 0)
                                 continue;
                             arid = Convert.ToInt32(dt.Rows[0][0].ToString());
                             GetArchContenWriteXlsPrint(arid);
                             this.BeginInvoke(new Action(() => { labprint.Text = string.Format("正在打印第{0}卷", i); }));
                             Thread.Sleep(200);
                             //ClsPrintInfoDocument.printDocument.Print();

                         }
                     } catch (Exception e) {
                         WriteLog("ID号:" + arid.ToString() + e.ToString());
                         return false;
                     }
                     this.BeginInvoke(new Action(() => { labprint.Text = string.Format("打印完成"); }));
                     return true;
                 }
             });
        }
        private bool GetArchContenWriteXlsPrint(int archid)
        {
            DataTable ArchConten = Common.GetPrintConten(archid, ClsPrintConten.PrintContenTable, ClsPrintConten.PrintContenCol, ClsPrintConten.PrintContenPagesn);
            if (ArchConten == null || ArchConten.Rows.Count <= 0) {
                string str = "ID号:" + archid + "目录获取失败!";
                WriteLog(str);
                return false;
            }
            DataTable dt = Common.GetArchPages1(archid);
            if (dt == null || dt.Rows.Count <= 0) {
                string str = "ID号:" + archid + "总页码或条码获取失败!";
                WriteLog(str);
                return false;
            }

            string page = dt.Rows[0][0].ToString();
            if (page.Trim().Length <= 0) {
                string str = "ID号:" + archid + "总页码获取失败!";
                WriteLog(str);
                return false;
            }
            int countpage;
            bool bl = int.TryParse(page, out countpage);
            if (!bl || countpage <= 0) {
                string str = "ID号:" + archid + "总页码获取失败!";
                WriteLog(str);
                return false;
            }
            string tm = dt.Rows[0][1].ToString();
            string xyr = dt.Rows[0][2].ToString();
            if (countpage <= 0) {
                string str = "ID号:" + archid + "总页码获取失败!";
                WriteLog(str);
                return false;
            }
            if (tm.Trim().Length <= 0) {
                string str = "ID号:" + archid + "条码信息获取失败!";
                WriteLog(str);
                return false;
            }
            //if (xyr.Trim().Length <= 0) {
            //    string str = "ID号:" + archid + "嫌疑人不能为空!";
            //    WriteLog(str);
            //    return false;
            //}
            try {
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(Path.Combine(Application.StartupPath, "目录模版.xlsx"));
                wsheek = work.Worksheets[0];
                //生成条码
                Bitmap bmp = CreateTm(tm);
                if (bmp == null)
                    return false;
                wsheek.Pictures.Add(2, 2, bmp);
                string strsn = ClsPrintConten.PrintContenSn;
                wsheek.Range["A3:A3"].Text = tm;
                //  string[] strxls = ClsPrintConten.printContenXls.ToArray();
                //获取起始行和列
                int rowsn = 0;
                int colsn = 0;
                if (strsn.Trim().Length > 1) {
                    rowsn = Convert.ToInt32(strsn.Remove(0, 1));
                    colsn = ClsInfo.ToNum(strsn.Substring(0, 1)) + 1;
                }
                int arow = 0;
                int bcol = 0;
                int dz = 0;
                for (int i = 0; i < ArchConten.Rows.Count; i++) {
                    dz = 0;
                    string zrz = ArchConten.Rows[i][0].ToString();
                    for (int j = 0; j < ArchConten.Columns.Count; j++) {
                        string str = ArchConten.Rows[i][j].ToString();
                        if (i == ArchConten.Rows.Count - 1 & j == ArchConten.Columns.Count - 1)
                            str = str + "/" + countpage.ToString();

                        if (zrz.Contains("即墨区")) {
                            if (!zrz.Contains("青岛市"))
                                zrz = "青岛市" + zrz;
                        }

                        if (j == 0) {
                            if (str.Contains("即墨区")) {
                                if (!str.Contains("青岛市"))
                                    str = "青岛市" + str;
                            }
                        }

                        if (j == 1) {
                            if (!str.Contains("即墨市") && !str.Contains("即墨区")) {
                                if (zrz.Contains("检察院")) {
                                    int id = zrz.IndexOf("院");
                                    zrz = zrz.Substring(0, id + 1);
                                    str = zrz + str;
                                }
                                else if (zrz.Trim().Length == 3 && !zrz.Contains("即墨"))
                                    str = "即墨市人民检察院" + zrz + str;
                                else if (!str.Contains("事务所"))
                                    str = zrz + str;
                            }
                            if (str.ToLower().Contains("xxx"))
                                str = str.Replace("xxx", xyr);
                            if (str.Contains("XXX"))
                                str = str.Replace("XXX", xyr);
                            if (str.Contains("刑事判决书") || str.Contains("刑事附带民事判决书")) {
                                int id = str.IndexOf("院");
                                str = str.Insert(id + 1, "关于" + xyr + "案");
                            }
                            else if (str.Contains("起诉书")) {
                                int id = str.IndexOf("院");
                                str = str.Insert(id + 1, "关于" + xyr + "案");
                            }
                            else if (str.Contains("即墨市人民检察院起诉书"))
                                str = str.Insert(8, "关于" + xyr + "案");
                            else if (str.Contains("批准逮捕决定书")) {
                                int id = str.IndexOf("院");
                                if (id >= 0)
                                    str = str.Insert(id + 1, "关于" + xyr + "案");
                                else {
                                    id = str.IndexOf("分局");
                                    if (id >= 0) {
                                        str = str.Insert(id + 2, "关于" + xyr + "案");
                                    }
                                    else {
                                        id = str.IndexOf("局");
                                        if (id >= 0)
                                            str = str.Insert(id + 1, "关于" + xyr + "案");
                                    }
                                }
                            }

                        }
                        if (rowsn > 0)
                            wsheek.Range[rowsn + i, colsn].Text = "0" + (i + 1).ToString();
                        if (ClsPrintConten.printContenXls.Count > 0) {
                            if (dz < ClsPrintConten.printContenXls.Count) {
                                arow = Convert.ToInt32(ClsPrintConten.printContenXls[dz].Remove(0, 1));
                                bcol = ClsInfo.ToNum(ClsPrintConten.printContenXls[dz].Substring(0, 1)) + 1;
                            }
                            if (dz < ClsPrintConten.PrintContenPage.Count && ClsPrintConten.PrintContenPage[dz] == "True") {
                                if (ClsPrintConten.PrintContenPageMode == 2) {
                                    int p = Convert.ToInt32(str);
                                    int p1 = 0;
                                    try {
                                        if (i != ArchConten.Rows.Count - 1) {
                                            p1 = Convert.ToInt32(ArchConten.Rows[i + 1][j].ToString());
                                            if (p == p1)
                                                str = p + "-" + p1;
                                            else {
                                                str = p + "-" + (p1 - 1);
                                            }
                                        }
                                        else {
                                            p1 = countpage;
                                            if (p == p1)
                                                str = p + "-" + p1;
                                            else {
                                                str = p + "-" + (p1 - 1);
                                            }
                                        }
                                    } catch {
                                        string str1 = "ID号:" + archid + "目录页码不正确";
                                        WriteLog(str1);
                                        return false;
                                    }
                                }
                                wsheek.Range[arow + i, bcol].Text = str;
                            }
                            //else if (dz < ClsPrintConten.PrintContenDz.Count && ClsPrintConten.PrintContenDz[dz] == "True")
                            //    wsheek.Range[arow + i, bcol].Text = str;
                            //else if (arow == i && j == bcol) {
                            //    wsheek.Range[arow + i, bcol].Text = str;
                            //}
                            else
                                wsheek.Range[arow + i, bcol].Text = str;
                        }
                        dz += 1;
                    }
                }
                rowsn = wsheek.LastRow;
                string fontname = wsheek.Rows[5].Cells[3].Style.Font.FontName;
                double fontsize = wsheek.Rows[6].Cells[3].Style.Font.Size;
                CellRange range = wsheek.Range["A6" + ":G" + rowsn];
                range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                range.Style.Font.Size = fontsize;
                range.Style.Font.FontName = fontname;
                range.HorizontalAlignment = HorizontalAlignType.Center;
                range.VerticalAlignment = VerticalAlignType.Center;
                range.Style.WrapText = true;
                range.AutoFitRows();
                double rowcount = 0;
                for (int i = 5; i < rowsn; i++) {
                    double row = wsheek.Rows[i].RowHeight;
                    if (row < 29)
                        wsheek.Rows[i].SetRowHeight(30, true);
                }
                for (int i = 5; i < 5 + ArchConten.Rows.Count; i++) {
                    double row = wsheek.Rows[i].RowHeight;
                    rowcount += row;
                }
                double pa1 = rowcount / (double)420;
                string pa11 = pa1.ToString();
                string s1 = "";
                if (pa11.Trim().Length > 2)
                    s1 = pa1.ToString().Substring(0, 3);
                else
                    s1 = pa1.ToString();
                double x1 = Math.Ceiling(Convert.ToDouble(s1));
                int inserrow = InsterRow((int)x1, rowcount);
                for (int i = 0; i < inserrow; i++) {
                    wsheek.InsertRow(i + rowsn + 1);
                    range = wsheek.Range["A" + (i + rowsn + 1) + ":G" + (i + rowsn + 1)];
                    range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                    range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                    wsheek.Rows[i + rowsn].SetRowHeight(30, true);
                }
                wsheek.PageSetup.PrintTitleRows = "$1:$5";
                // work.SaveToFile("d:\\123.xls");
                work.PrintDocument.Print();
                return true;
            } catch (Exception e) {
                string str = "ID号:" + archid + ":" + e;
                WriteLog(str);
                return false;
            }
        }

        int InsterRow(int pagecount, double rowcount)
        {
            double Pcount = pagecount * 30 * 15;
            double row = Pcount - rowcount;
            double id = row / (double)30;
            double x = Math.Ceiling(id);
            int num = (int)x;
            return num;
        }

        #endregion

        #region print EWm

        private string GetTmgz(string boxsn, string lx)
        {
            lx = lx.PadLeft(4, '0');
            boxsn = boxsn.PadLeft(6, '0');
            string c = "";
            if (V_HouseSetCs.HouseName == "不动产产权")
                c = "C";
            else if (V_HouseSetCs.HouseName == "不动产抵押")
                c = "Y";
            else if (V_HouseSetCs.HouseName == "不动产查封")
                c = "F";
            string str = "DL-" + lx + "-" + c + boxsn;
            return str;
        }
        public static Bitmap Createwm(string asset, int x, int y)
        {
            Bitmap result = null;
            try {
                BarcodeWriter barCodeWriter = new BarcodeWriter();
                barCodeWriter.Format = BarcodeFormat.QR_CODE;
                barCodeWriter.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
                barCodeWriter.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
                barCodeWriter.Options.Height = y;
                barCodeWriter.Options.Width = x;
                barCodeWriter.Options.Margin = 0;
                ZXing.Common.BitMatrix bm = barCodeWriter.Encode(asset);
                result = barCodeWriter.Write(bm);
                return result;
            } catch {
                return null;
            }
        }

        public static Image GetPrintPicture(Bitmap image, int picWidth, int picHeight, string str)
        {
            Bitmap printPicture = new Bitmap(picWidth, picHeight);
            string filePath = "c:\\temp\\tewm.e";
            Font font = new Font("Airal", 7f);
            Font font1 = new Font("Airal", 9f);
            Graphics gs = Graphics.FromImage(printPicture);
            Metafile mf = new Metafile(filePath, gs.GetHdc());
            Graphics g = Graphics.FromImage(mf);
            HatchBrush hb = new HatchBrush(HatchStyle.Shingle, Color.Black, Color.White);
            Brush brush = new SolidBrush(Color.Black);

            string str1 = str;
            Rectangle destRect = new Rectangle(50, 0, image.Width + 4, image.Height + 4);
            g.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            g.DrawString(str, font1, brush, 50, 35);

            g.RotateTransform(-90.0F);
            str1 = str.Substring(0, 3).ToString();
            g.DrawString(str1, font, brush, -30, 85);

            str1 = str.Substring(3, 3).ToString();
            g.DrawString(str1, font, brush, -30, 98);

            str1 = str.Substring(6, 3).ToString();
            g.DrawString(str1, font, brush, -30, 111);

            str1 = str.Substring(9, 3).ToString();
            g.DrawString(str1, font, brush, -30, 124);

            str1 = str.Substring(12, 3).ToString();
            g.DrawString(str1, font, brush, -30, 140);

            g.RotateTransform(90.0F);
            destRect = new Rectangle(150, 0, image.Width + 20, image.Height + 20);
            g.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            g.DrawString(" ", font, brush, 0, 0);
            g.DrawString(" ", font, brush, 300, 30);

            g.Dispose();
            gs.Dispose();
            mf.Dispose();
            return printPicture;
        }

        private void Printewm(string txt)
        {
            Image img = Createwm(txt, 30, 30);
            Bitmap bmp = (Bitmap)img;
            img = GetPrintPicture(bmp, 400, 60, txt);
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintController = new StandardPrintController();
            printDoc.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            printDoc.Print();
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            string emf = "c:\\temp\\tewm.e";
            Metafile metaFile = new Metafile(emf);
            e.Graphics.DrawImage(metaFile, 0, 0, new RectangleF(0, 0, metaFile.Width, metaFile.Height), GraphicsUnit.Point);
            metaFile.Dispose();
        }

        private void PrintEwm()
        {
            butBoxRangeAdd.Enabled = false;
            butDelbox.Enabled = false;
            if (tabSelectbox.IsSelected) {
                if (rbboxOne.Checked) {
                    string boxsn = gArchSelect1.Boxsn.ToString();
                    string lx = gArchSelect1.ArchXqzt;
                    Printewm(GetTmgz(boxsn, lx));
                }

            }
        }

        private void butPrintTm_Click(object sender, EventArgs e)
        {
            if (ClsPrintInfo.Archid <= 0)
                return;
            PrintEwm();
        }


        #endregion

        #region CreateTm
        public static Bitmap CreateTm(string str)
        {
            try {
                BarcodeWriter writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.CODE_128;
                EncodingOptions options = new EncodingOptions()
                {
                    Width = 472,
                    Height = 35,
                    Margin = 10,
                    PureBarcode = true
                };
                writer.Options = options;
                Bitmap map = writer.Write(str);
                return map;
            } catch (Exception e) {
                MessageBox.Show("条码信息中不能包含汉字!" + e.ToString());
                return null;
            }
        }

        #endregion

        private void txtBosn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtArchno.Focus();
        }

        private void txtArchno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtArchno2.Focus();
        }

        private void txtArchno2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butPrintConten.Focus();
        }
    }
}
