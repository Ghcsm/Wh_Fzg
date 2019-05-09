using DAL;
using System;
using System.Data;
using System.Windows.Forms;
using Spire.Xls;
using System.IO;
using System.Threading.Tasks;

namespace Csmsjdr
{
    public partial class FrmImport : Form
    {
        public FrmImport()
        {
            InitializeComponent();
        }

        private Workbook work = null;
        Worksheet wsheek = null;

        private void GetImportInfo()
        {
            combImportTable.Items.Clear();
            combImportTable.Items.Add("");
            DataTable dt = Common.GetImportTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                string s = dr[0].ToString();
                if (s.Trim().Length > 0) {
                    combImportTable.Items.Add(s);
                }
            }
        }

        private void GetImportInfo(string str)
        {
            DataTable dt = Common.GetImportTable(str);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                string s = dr[0].ToString();
                if (s.Trim().Length >= 0) {
                    string[] a = s.Split(';');
                    for (int i = 0; i < a.Length; i++) {
                        string b = a[i];
                        lvImportTable.Items.Add(b);
                        combImportZhi.Items.Add(b);
                    }
                }
            }
        }

        private void OpenXls()
        {
            TxtEnd(0);
            combXlsTable.Items.Clear();
            dgvXlsData.DataSource = null;
            work = new Workbook();
            try {
                if (opdXlsFile.ShowDialog() != DialogResult.OK) {
                    txtXlsPath.Text = "";
                    return;
                }
                txtXlsPath.Text = opdXlsFile.FileName;
                if (!File.Exists(txtXlsPath.Text.Trim())) {
                    MessageBox.Show(txtXlsPath.Text.Trim() + "不存在!");
                    return;
                }
                work.LoadFromFile(txtXlsPath.Text.Trim());
                int id = work.Worksheets.Count;
                for (int i = 0; i < id; i++) {
                    string s = work.Worksheets[i].Name;
                    combXlsTable.Items.Add(s);
                    combXlsTable.SelectedIndex = 0;
                }
                wsheek = work.Worksheets[combXlsTable.SelectedIndex];
                dgvXlsData.DataSource = wsheek.ExportDataTable();
                labXlsCount.Text = string.Format("共 {0} 条", dgvXlsData.Rows.Count);
            } catch (Exception e) {
                MessageBox.Show("错误:" + e.ToString());
            } finally {
                TxtEnd(1);
            }
        }


        private void GetXlsTable()
        {
            TxtEnd(0);
            try {
                dgvXlsData.DataSource = null;
                wsheek = work.Worksheets[combXlsTable.SelectedIndex];
                dgvXlsData.DataSource = wsheek.ExportDataTable();
                labXlsCount.Text = string.Format("共 {0} 条", dgvXlsData.Rows.Count);
            } catch (Exception e) {
                MessageBox.Show("错误:" + e.ToString());
            } finally {
                TxtEnd(1);
            }

        }

        private void TxtEnd(int id)
        {
            if (id == 0) {
                butXlsSelect.Enabled = false;
                butImport.Enabled = false;
                combImportTable.Enabled = false;
                combXlsTable.Enabled = false;
                chbImportNew.Enabled = false;
                butLog.Enabled = false;
                return;
            }
            butXlsSelect.Enabled = true;
            butImport.Enabled = true;
            combImportTable.Enabled = true;
            combXlsTable.Enabled = true;
            chbImportNew.Enabled = true;
            butLog.Enabled = true;
        }

        private bool Istxt()
        {
            if (combImportTable.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择导入目标表!");
                combImportTable.Focus();
                return false;
            }

            if (txtXlsPath.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择Xls文件!");
                txtXlsPath.Focus();
                return false;
            }

            if (combXlsTable.Text.Trim().Length <= 0) {
                MessageBox.Show("未发现Xls工作表!");
                return false;
            }
            if (dgvXlsData.Rows.Count <= 0) {
                MessageBox.Show("未发现要导入的数据!");
                return false;
            }

            if (chbImportZhi.Checked)
            {
                if (combImportZhi.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请选择唯一字段!");
                    return false;
                }
            }
            return true;
        }


        private void WriteLog(string str)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string dt = DateTime.Now.ToString();
            try {
                string file = "log.txt";
                string filepath = Path.Combine(Application.StartupPath, file);
                if (!File.Exists(filepath)) {
                    fs = new FileStream(filepath, FileMode.Create);
                }
                else {
                    fs = new FileStream(filepath, FileMode.Append);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(" 操作时间: " + dt + "-->" + str);
                sw.Flush();
            } catch { } finally {
                sw.Close();
                fs.Close();
            }
        }

        private Task<bool> StartImport(string table, string tzd, string xzd, bool chk, int count,bool wyzb ,int wyz)
        {
            return Task.Run(() =>
            {
                int id = 0;;
                 for (int i = 0; i < dgvXlsData.Rows.Count; i++) {
                     xzd = "";
                     i = 0;
                     id+=1;
                     for (int a = 0; a < count; a++) {
                         if (a < count - 1)
                             xzd += dgvXlsData.Rows[i].Cells[a].Value.ToString() + ",";
                         else
                             xzd += dgvXlsData.Rows[i].Cells[a].Value.ToString();
                     }
                     try {
                         Common.ImportData(table, tzd, xzd, chk,wyzb,wyz);
                         this.Invoke(new Action(() =>
                         {
                             dgvXlsData.Rows.RemoveAt(i);
                             dgvXlsData.Refresh();
                             lbsy.Text = string.Format("剩余 {0} 条", dgvXlsData.Rows.Count.ToString());
                         }));
                     } catch (Exception e)
                     {
                         string s = "错误行:" + id;
                         WriteLog(s+" 详细信息-->"+e);
                     } finally {
                         this.Invoke(new Action(() =>
                         {
                             dgvXlsData.Rows.RemoveAt(i);
                             dgvXlsData.Refresh();
                             lbsy.Text = string.Format("剩余 {0} 条", dgvXlsData.Rows.Count.ToString());
                         }));
                     }
                 }

                 return true;
             });
        }

        private async void ImportData()
        {
            TxtEnd(0);
            try
            {
                if (!Istxt())
                    return;
                string tzd = "";
                string xzd = "";
                int count = lvImportTable.Items.Count;
                bool chk = chbImportNew.Checked;
                string table = combImportTable.Text.Trim();
                int wyz = combImportZhi.SelectedIndex;
                bool wyzbl = chbImportZhi.Checked;
                for (int i = 0; i < count; i++)
                {
                    if (i < count -1)
                        tzd += lvImportTable.Items[i].Text + ",";
                    else
                        tzd += lvImportTable.Items[i].Text;

                }
                bool bl = await StartImport(table, tzd, xzd, chk, count,wyzbl,wyz);
                if (bl == true)
                    TxtEnd(1);

            }
            catch (Exception e)
            {
                MessageBox.Show("错误:" + e.ToString());
            }
            finally
            {
                TxtEnd(1);
                string s = "导入数据:数据库表名:" + combImportTable.Text.Trim() + ";文件名：" + txtXlsPath.Text.Trim() + ";工作表名:" +
                           combXlsTable.Text.Trim();
                Common.Writelog(0,s);
            }
        }

        private void FrmImport_Shown(object sender, EventArgs e)
        {
            GetImportInfo();
        }

        private void combImportTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvImportTable.Items.Clear();
            combImportZhi.Items.Clear();
            string s = combImportTable.Text.Trim();
            if (s.Length <= 0)
                return;
            GetImportInfo(s);
        }

        private void butXlsSelect_Click(object sender, EventArgs e)
        {
            OpenXls();
        }

        private void combXlsTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetXlsTable();
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            ImportData();
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            string file = "log.txt";
            string filepath = Path.Combine(Application.StartupPath, file);
            if (!File.Exists(filepath)) {
                MessageBox.Show("日志文件不存在！");
            }
            else {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(filepath);
            }
        }

        private void chbImportZhi_CheckedChanged(object sender, EventArgs e)
        {
            if (chbImportZhi.Checked)
            {
                if (!chbImportNew.Checked) {
                    chbImportZhi.Checked = false;
                    combImportZhi.Enabled = false;
                    return;
                }
                combImportZhi.Enabled = true;
            }
        }

        private void chbImportNew_CheckedChanged(object sender, EventArgs e)
        {
            if (!chbImportNew.Checked)
            {
                chbImportZhi.Checked = false;
                combImportZhi.Enabled = false;
            }
        }
    }
}
