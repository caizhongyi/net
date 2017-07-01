using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class NewsType: INotifyPropertyChanged
    {
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

       private Int32 _parentId;
       public Int32 parentId
       {
            get{return _parentId;}
            set
               {
                  if (value != _parentId)
                  {
                       _parentId = value;
                  }
                }
       }

       private Int32 _nt_parentId;
       public Int32 nt_parentId
       {
            get{return _nt_parentId;}
            set
               {
                  if (value != _nt_parentId)
                  {
                       _nt_parentId = value;
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

    public event PropertyChangedEventHandler PropertyChanged;
     public virtual void OnPropertyChanged(string propName)
     {
     if (PropertyChanged != null)
     {PropertyChanged(this, new PropertyChangedEventArgs(propName));}
     }
    }
}
