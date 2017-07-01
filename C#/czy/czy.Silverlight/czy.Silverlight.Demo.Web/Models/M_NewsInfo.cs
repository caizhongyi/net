using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class NewsInfo
    {
       private int _n_newsTypeId;
       public int n_newsTypeId
       {
            get{return _n_newsTypeId;}
            set{_n_newsTypeId = value;}
       }

       private DateTime _n_createDate;
       public DateTime n_createDate
       {
            get{return _n_createDate;}
            set{_n_createDate = value;}
       }

       private DateTime _n_startDate;
       public DateTime n_startDate
       {
            get{return _n_startDate;}
            set{_n_startDate = value;}
       }

       private DateTime _n_endDate;
       public DateTime n_endDate
       {
            get{return _n_endDate;}
            set{_n_endDate = value;}
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

    }
}
