using System;
using System.Collections.Generic;
using System.Text;
namespace czy.Models
{
    public class RolesInfo
    {
       private int _r_id;
       public int r_id
       {
            get{return _r_id;}
            set{_r_id = value;}
       }

       private int _r_right;
       public int r_right
       {
            get{return _r_right;}
            set{_r_right = value;}
       }

       private string _r_name;
       public string r_name
       {
            get{return _r_name;}
            set{_r_name = value;}
       }

     

    }
}
