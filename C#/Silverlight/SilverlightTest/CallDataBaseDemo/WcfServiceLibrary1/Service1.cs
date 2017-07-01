using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using DAL;

namespace WcfServiceLibrary1
{
    // 注意: 如果更改此处的类名“IService1”，也必须更新 App.config 中对“IService1”的引用。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<UserInfo> SelectUserInfo()
        {

            string cmd = "select * from tbl_UserInfo";
            DataTable dt = Util.GetDataTable(cmd);
            List<UserInfo> list = new List<UserInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new UserInfo() { UID = dt.Rows[i].ItemArray[0].ToString(), UName = dt.Rows[i].ItemArray[2].ToString() });
            }
            return list;
        }
    }
   
}
