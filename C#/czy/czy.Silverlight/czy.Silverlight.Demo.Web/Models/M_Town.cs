using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class Town
    {
       private int _t_id;
       public int t_id
       {
            get{return _t_id;}
            set{_t_id = value;}
       }

       private int _t_cityId;
       public int t_cityId
       {
            get{return _t_cityId;}
            set{_t_cityId = value;}
       }

       private string _t_name;
       public string t_name
       {
            get{return _t_name;}
            set{_t_name = value;}
       }

    }
}
