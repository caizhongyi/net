using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class V_NewsInfo: INotifyPropertyChanged
    {
       private Int32 _n_newsTypeId;
       public Int32 n_newsTypeId
       {
            get{return _n_newsTypeId;}
            set
               {
                  if (value != _n_newsTypeId)
                  {
                       _n_newsTypeId = value;
                  }
                }
       }

       private Int32 _nt_id;
       public Int32 nt_id
       {
            get{return _nt_id;}
            set
               {
                  if (value != _nt_id)
                  {
                       _nt_id = value;
                  }
                }
       }

       private DateTime _n_createDate;
       public DateTime n_createDate
       {
            get{return _n_createDate;}
            set
               {
                  if (value != _n_createDate)
                  {
                       _n_createDate = value;
                  }
                }
       }

       private DateTime _n_startDate;
       public DateTime n_startDate
       {
            get{return _n_startDate;}
            set
               {
                  if (value != _n_startDate)
                  {
                       _n_startDate = value;
                  }
                }
       }

       private DateTime _n_endDate;
       public DateTime n_endDate
       {
            get{return _n_endDate;}
            set
               {
                  if (value != _n_endDate)
                  {
                       _n_endDate = value;
                  }
                }
       }

       private Int64 _n_id;
       public Int64 n_id
       {
            get{return _n_id;}
            set
               {
                  if (value != _n_id)
                  {
                       _n_id = value;
                  }
                }
       }

       private String _nt_name;
       public String nt_name
       {
            get{return _nt_name;}
            set
               {
                  if (value != _nt_name)
                  {
                       _nt_name = value;
                  }
                }
       }

       private String _nt_remark;
       public String nt_remark
       {
            get{return _nt_remark;}
            set
               {
                  if (value != _nt_remark)
                  {
                       _nt_remark = value;
                  }
                }
       }

       private String _n_title;
       public String n_title
       {
            get{return _n_title;}
            set
               {
                  if (value != _n_title)
                  {
                       _n_title = value;
                  }
                }
       }

       private String _n_content;
       public String n_content
       {
            get{return _n_content;}
            set
               {
                  if (value != _n_content)
                  {
                       _n_content = value;
                  }
                }
       }

    public event PropertyChangedEventHandler PropertyChanged;
     public virtual void OnPropertyChanged(string propName)
     {
     if (PropertyChanged != null)
     {PropertyChanged(this, new PropertyChangedEventArgs(propName));}
     }
    }
}
