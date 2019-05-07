using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace Bgkj
{
    public static class ClsIsinfo
    {


        public static void showerr2(int id)
        {
            if (id == 0)
                MessageBox.Show(DESEncrypt.DesDecrypt("1Fb/S1ng72+UkVRYiCA/Q74H5D5MLdlzLaEMa5KSGUsP1QbMcl3VOvB7i+KLEzScqeUBuMqnCXRMyaridNgV+w=="));
            else if (id == 1)
                MessageBox.Show(DESEncrypt.DesDecrypt("meoLKAkxrxANmOfAwGS12V4KHBEJUa/1VIa+qHccp9s="));
            else if (id == 2)
                MessageBox.Show(DESEncrypt.DesDecrypt("jCYeMNb2MK7CP0BWr8IzJR83pCx8xJ6XtGv2ni/RL9LqH15JePx2O/9vB45nmNXZzo7EErnAA3jUhQ7SI4HSTA=="));
            else if (id == 3)
                MessageBox.Show(DESEncrypt.DesDecrypt("jCYeMNb2MK7CCH4XVR2HyqqfcTePyp0XOjr8U7kIHp/6tmtJp7lHozncfoZPiDwq"));
        }

        public static void Istime()
        {
            string strtime = Common.Getsqltime();
            if (strtime.Trim().Length <= 0) {
                T_ConFigure.Bgsoft = true;
                showerr2(3);
                return;
            }
            DateTime dt1 = DateTime.Now.Date;
            DateTime dt2 = Convert.ToDateTime(strtime).Date;
            if (DateTime.Compare(dt1, dt2) != 0) {
                T_ConFigure.Bgsoft = true;
                showerr2(1);
                return;
            }
            if (DESEncrypt.DesDecrypt(T_ConFigure.Mwtime).Trim().Length < 1) {
                T_ConFigure.Bgsoft = true;
                showerr2(0);
                return;
            }
            dt1 = Convert.ToDateTime(strtime);
            dt2 = Convert.ToDateTime(DESEncrypt.DesDecrypt(T_ConFigure.Mwtime));
            if (DateTime.Compare(dt1, dt2) >= 0) {
                Common.Setsqltime(DESEncrypt.DesEncrypt(dt1.ToString()));
                return;
            }
            T_ConFigure.Bgsoft = true;
            showerr2(2);
        }
    }
}
