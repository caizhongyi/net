using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WbSystem.GetIp
{
    public class LanTimeManageInfo
    {
        public static DataSet myds = new DataSet();
        public static DataTable mydt = new DataTable("datainfo");
        public static DataRow mydr;
        public static string lanIpAddress;//��������Ӧ�ľ�������IP��Ϣ
        public static bool IsTrue = true;//�����߳�
        //����datatable����
        public static void ColumnInit()
        {
            mydt.Columns.Add(new DataColumn("mIP", typeof(string)));
            mydt.Columns.Add(new DataColumn("mDAY", typeof(string)));

        }
        //���Ӿ������ڵ�IP������
        public static void Addmydr(string smip, string smDay)
        {
            string sip = smip;
            string sday = smDay;
            int irow = mydt.Rows.Count;
            int iCheckrow = 0;
            for (int i = 0; i < irow; i++)
            {
                string s1 = mydt.Rows[i][0].ToString();
                string s2 = mydt.Rows[i][1].ToString();
                if (s1 == sip)
                {
                    iCheckrow = i + 1;

                }
            }
            if (iCheckrow > 0)
            {
                mydt.Rows[iCheckrow - 1][1] = smDay;
            }
            else
            {
                mydr = mydt.NewRow();
                mydr[0] = sip;
                mydr[1] = sday;
                mydt.Rows.Add(mydr);
            }
        }
    }
}
