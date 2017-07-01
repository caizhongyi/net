using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class OrderDetail: INotifyPropertyChanged
    {
       private String _od_id;
       public String od_id
       {
            get{return _od_id;}
            set
               {
                  if (value != _od_id)
                  {
                       _od_id = value;
                  }
                }
       }

       private String _od_productId;
       public String od_productId
       {
            get{return _od_productId;}
            set
               {
                  if (value != _od_productId)
                  {
                       _od_productId = value;
                  }
                }
       }

       private String _od_remark;
       public String od_remark
       {
            get{return _od_remark;}
            set
               {
                  if (value != _od_remark)
                  {
                       _od_remark = value;
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
