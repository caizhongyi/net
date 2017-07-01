using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Rebate: INotifyPropertyChanged
    {
       private Int32 _r_id;
       public Int32 r_id
       {
            get{return _r_id;}
            set
               {
                  if (value != _r_id)
                  {
                       _r_id = value;
                  }
                }
       }

       private Int32 _R_userTypeId;
       public Int32 R_userTypeId
       {
            get{return _R_userTypeId;}
            set
               {
                  if (value != _R_userTypeId)
                  {
                       _R_userTypeId = value;
                  }
                }
       }

       private Int32 _R_rebateValue;
       public Int32 R_rebateValue
       {
            get{return _R_rebateValue;}
            set
               {
                  if (value != _R_rebateValue)
                  {
                       _R_rebateValue = value;
                  }
                }
       }

       private String _R_userId;
       public String R_userId
       {
            get{return _R_userId;}
            set
               {
                  if (value != _R_userId)
                  {
                       _R_userId = value;
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
