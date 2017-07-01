using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class NewsType
    {
       private int _nt_id;
       public int nt_id
       {
            get{return _nt_id;}
            set{_nt_id = value;}
       }

       private string _nt_name;
       public string nt_name
       {
            get{return _nt_name;}
            set{_nt_name = value;}
       }

       private string _nt_remark;
       public string nt_remark
       {
            get{return _nt_remark;}
            set{_nt_remark = value;}
       }

    }
}
