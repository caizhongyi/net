using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class ProductType
    {
       private Int64 _pt_id;
       public Int64 pt_id
       {
            get{return _pt_id;}
            set{_pt_id = value;}
       }

       private Int64 _pt_praentId;
       public Int64 pt_praentId
       {
            get{return _pt_praentId;}
            set{_pt_praentId = value;}
       }

       private string _pt_name;
       public string pt_name
       {
            get{return _pt_name;}
            set{_pt_name = value;}
       }

    }
}
