using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class ProductImages: INotifyPropertyChanged
    {
       private Int32 _pi_id;
       public Int32 pi_id
       {
            get{return _pi_id;}
            set
               {
                  if (value != _pi_id)
                  {
                       _pi_id = value;
                  }
                }
       }

       private String _pi_title;
       public String pi_title
       {
            get{return _pi_title;}
            set
               {
                  if (value != _pi_title)
                  {
                       _pi_title = value;
                  }
                }
       }

       private String _pi_url;
       public String pi_url
       {
            get{return _pi_url;}
            set
               {
                  if (value != _pi_url)
                  {
                       _pi_url = value;
                  }
                }
       }

       private String _pi_remark;
       public String pi_remark
       {
            get{return _pi_remark;}
            set
               {
                  if (value != _pi_remark)
                  {
                       _pi_remark = value;
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
