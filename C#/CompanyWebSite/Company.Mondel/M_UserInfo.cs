using System;
using System.Collections.Generic;
using System.Text;
namespace Company.Models
{
    public class UserInfo
    {
       private int _u_roleId;
       public int u_roleId
       {
            get{return _u_roleId;}
            set{_u_roleId = value;}
       }

       private int _u_loginTime;
       public int u_loginTime
       {
            get{return _u_loginTime;}
            set{_u_loginTime = value;}
       }

       private int _u_loginTotalTime;
       public int u_loginTotalTime
       {
            get{return _u_loginTotalTime;}
            set{_u_loginTotalTime = value;}
       }

       private DateTime _u_createDate;
       public DateTime u_createDate
       {
            get{return _u_createDate;}
            set{_u_createDate = value;}
       }

       private DateTime _u_loginDate;
       public DateTime u_loginDate
       {
            get{return _u_loginDate;}
            set{_u_loginDate = value;}
       }

       private bool _u_isDel;
       public bool u_isDel
       {
            get{return _u_isDel;}
            set{_u_isDel = value;}
       }

       private Int64 _u_id;
       public Int64 u_id
       {
            get{return _u_id;}
            set{_u_id = value;}
       }

       private string _u_name;
       public string u_name
       {
            get{return _u_name;}
            set{_u_name = value;}
       }

       private string _u_pwd;
       public string u_pwd
       {
            get{return _u_pwd;}
            set{_u_pwd = value;}
       }

       private string _u_state;
       public string u_state
       {
           get { return _u_state; }
           set { _u_state = value; }
       }

    }
}
