using System;
using System.Collections.Generic;
using System.Text;
namespace Company.Models
{
    public class NewsInfo
    {
       private int _n_typeId;
       public int n_typeId
       {
            get{return _n_typeId;}
            set{_n_typeId = value;}
       }

       private DateTime _n_createDate;
       public DateTime n_createDate
       {
            get{return _n_createDate;}
            set{_n_createDate = value;}
       }

       private bool _n_isDel;
       public bool n_isDel
       {
            get{return _n_isDel;}
            set{_n_isDel = value;}
       }

       private bool _n_isShow;
       public bool n_isShow
       {
            get{return _n_isShow;}
            set{_n_isShow = value;}
       }

       private Int64 _n_id;
       public Int64 n_id
       {
            get{return _n_id;}
            set{_n_id = value;}
       }

       private string _n_title;
       public string n_title
       {
            get{return _n_title;}
            set{_n_title = value;}
       }

       private string _n_content;
       public string n_content
       {
            get{return _n_content;}
            set{_n_content = value;}
       }

       private string _n_source;
       public string n_source
       {
            get{return _n_source;}
            set{_n_source = value;}
       }

       private string _n_author;
       public string n_author
       {
            get{return _n_author;}
            set{_n_author = value;}
       }

      

    }
}
