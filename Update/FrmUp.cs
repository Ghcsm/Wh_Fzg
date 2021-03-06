﻿using DAL;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Update
{
    public partial class FrmUpate : Form
    {
        public FrmUpate()
        {
            InitializeComponent();
        }

        private int id = 0;
        private void FrmUpate_Shown(object sender, EventArgs e)
        {
            label1.Text = "    更新程序前请将各个环节操作\n保存退出以免数据丢失！ ";
        }

        private void but_Up_Click(object sender, EventArgs e)
        {
            try {
                if (Common.GetUserRw(Common.OperID) >= 1) {
                    MessageBox.Show("任务正在进行中请稍候退出程序！");
                    return;
                }

                id = 1;
                Application.Exit();
            } catch {
                Application.Exit();
            }
        }

        private void FrmUpate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (id == 1)
            {
                string appName = Application.StartupPath + "\\" + "getupdate.exe";
                Process.Start(appName);
            }
        }
    }
}
