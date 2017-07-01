using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class UserType: INotifyPropertyChanged
    {
       private Int32 _ut_id;
       public Int32 ut_id
       {
            get{return _ut_id;}
            set
               {
                  if (value != _ut_id)
                  {
                       _ut_id = value;
                  }
                }
       }

       private String _ut_right;
       public String ut_right
       {
            get{return _ut_right;}
            set
               {
                  if (value != _ut_right)
                  {
                       _ut_right = value;
                  }
                }
       }

       private String _ut_name;
       public String ut_name
       {
            get{return _ut_name;}
            set
               {
                  if (value != _ut_name)
                  {
                       _ut_name = value;
                  }
                }
       }

       private String _ut_remark;
       public String ut_remark
       {
            get{return _ut_remark;}
            set
               {
                  if (value != _ut_remark)
                  {
                       _ut_remark = value;
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
