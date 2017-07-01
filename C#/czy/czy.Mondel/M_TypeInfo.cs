using System;
using System.Collections.Generic;
using System.Text;
namespace czy.Models
{
    public class TypeInfo
    {
       private int _t_Id;
       public int t_Id
       {
            get{return _t_Id;}
            set{_t_Id = value;}
       }

       private int _t_parentId;
       public int t_parentId
       {
            get{return _t_parentId;}
            set{_t_parentId = value;}
       }

       private bool _t_isDel;
       public bool t_isDel
       {
            get{return _t_isDel;}
            set{_t_isDel = value;}
       }

       private string _t_name;
       public string t_name
       {
            get{return _t_name;}
            set{_t_name = value;}
       }

       private string _t_url;
       public string t_url
       {
            get{return _t_url;}
            set{_t_url = value;}
       }
       private int _t_order;
       public int t_order
       {
           get { return _t_order; }
           set { _t_order= value; }
       }
      
    }
}
