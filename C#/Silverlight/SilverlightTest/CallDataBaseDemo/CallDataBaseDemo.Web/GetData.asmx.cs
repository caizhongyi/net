using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using System.Data;
using DAL;

namespace CallDataBaseDemo.Web
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GetData : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public List<UserInfo> SelectUserInfo()
        {

            string cmd = "select * from tbl_UserInfo";
            DataTable dt = DataBase.GetDataTable(cmd);
            List<UserInfo> list = new List<UserInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new UserInfo() { UID = dt.Rows[i].ItemArray[0].ToString(), UName = dt.Rows[i].ItemArray[2].ToString() });
            }
            return list;
        }
        [WebMethod]
        public void Yes()
        {

        }
    }

    [DataContract]
    public class UserInfo
    {
        string _UID;
        string _UName;
        [DataMember]
        public string UID
        {
            get { return _UID; }
            set { _UID = value; }
        }
        [DataMember]
        public string UName
        {
            get { return _UName; }
            set { _UName = value; }
        }
    }
}
