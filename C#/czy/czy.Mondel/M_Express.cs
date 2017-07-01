using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Express: INotifyPropertyChanged
    {
       private Int32 _e_id;
       public Int32 e_id
       {
            get{return _e_id;}
            set
               {
                  if (value != _e_id)
                  {
                       _e_id = value;
                  }
                }
       }

       private Decimal _e_price;
       public Decimal e_price
       {
            get{return _e_price;}
            set
               {
                  if (value != _e_price)
                  {
                       _e_price = value;
                  }
                }
       }

       private DateTime _e_createDate;
       public DateTime e_createDate
       {
            get{return _e_createDate;}
            set
               {
                  if (value != _e_createDate)
                  {
                       _e_createDate = value;
                  }
                }
       }

       private String _e_name;
       public String e_name
       {
            get{return _e_name;}
            set
               {
                  if (value != _e_name)
                  {
                       _e_name = value;
                  }
                }
       }

       private String _e_remark;
       public String e_remark
       {
            get{return _e_remark;}
            set
               {
                  if (value != _e_remark)
                  {
                       _e_remark = value;
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
