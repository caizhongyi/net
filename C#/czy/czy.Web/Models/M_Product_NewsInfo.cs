using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Product_NewsInfo: INotifyPropertyChanged
    {
       private String _p_n_productId;
       public String p_n_productId
       {
            get{return _p_n_productId;}
            set
               {
                  if (value != _p_n_productId)
                  {
                       _p_n_productId = value;
                  }
                }
       }

       private Int64 _p_n_id;
       public Int64 p_n_id
       {
            get{return _p_n_id;}
            set
               {
                  if (value != _p_n_id)
                  {
                       _p_n_id = value;
                  }
                }
       }

       private Int64 _p_n_newsId;
       public Int64 p_n_newsId
       {
            get{return _p_n_newsId;}
            set
               {
                  if (value != _p_n_newsId)
                  {
                       _p_n_newsId = value;
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
