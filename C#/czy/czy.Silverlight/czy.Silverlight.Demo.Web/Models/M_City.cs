using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class City
    {
       private int _c_id;
       public int c_id
       {
            get{return _c_id;}
            set{_c_id = value;}
       }

       private int _c_provinceId;
       public int c_provinceId
       {
            get{return _c_provinceId;}
            set{_c_provinceId = value;}
       }

       private string _c_name;
       public string c_name
       {
            get{return _c_name;}
            set{_c_name = value;}
       }

    }
}
