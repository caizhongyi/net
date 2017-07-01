using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Order: INotifyPropertyChanged
    {
       private String _o_id;
       public String o_id
       {
            get{return _o_id;}
            set
               {
                  if (value != _o_id)
                  {
                       _o_id = value;
                  }
                }
       }

       private String _o_orderDetailId;
       public String o_orderDetailId
       {
            get{return _o_orderDetailId;}
            set
               {
                  if (value != _o_orderDetailId)
                  {
                       _o_orderDetailId = value;
                  }
                }
       }

       private Int32 _o_state;
       public Int32 o_state
       {
            get{return _o_state;}
            set
               {
                  if (value != _o_state)
                  {
                       _o_state = value;
                  }
                }
       }

       private DateTime _o_createDate;
       public DateTime o_createDate
       {
            get{return _o_createDate;}
            set
               {
                  if (value != _o_createDate)
                  {
                       _o_createDate = value;
                  }
                }
       }

       private DateTime _o_startDate;
       public DateTime o_startDate
       {
            get{return _o_startDate;}
            set
               {
                  if (value != _o_startDate)
                  {
                       _o_startDate = value;
                  }
                }
       }

       private DateTime _o_endDate;
       public DateTime o_endDate
       {
            get{return _o_endDate;}
            set
               {
                  if (value != _o_endDate)
                  {
                       _o_endDate = value;
                  }
                }
       }

       private String _o_userId;
       public String o_userId
       {
            get{return _o_userId;}
            set
               {
                  if (value != _o_userId)
                  {
                       _o_userId = value;
                  }
                }
       }

       private String _o_orderUserId;
       public String o_orderUserId
       {
            get{return _o_orderUserId;}
            set
               {
                  if (value != _o_orderUserId)
                  {
                       _o_orderUserId = value;
                  }
                }
       }

       private String _o_fromAddress;
       public String o_fromAddress
       {
            get{return _o_fromAddress;}
            set
               {
                  if (value != _o_fromAddress)
                  {
                       _o_fromAddress = value;
                  }
                }
       }

       private String _o_toAddress;
       public String o_toAddress
       {
            get{return _o_toAddress;}
            set
               {
                  if (value != _o_toAddress)
                  {
                       _o_toAddress = value;
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
