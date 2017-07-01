using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class ProductType: INotifyPropertyChanged
    {
       private Int64 _pt_id;
       public Int64 pt_id
       {
            get{return _pt_id;}
            set
               {
                  if (value != _pt_id)
                  {
                       _pt_id = value;
                  }
                }
       }

       private Int64 _pt_parentId;
       public Int64 pt_parentId
       {
            get{return _pt_parentId;}
            set
               {
                  if (value != _pt_parentId)
                  {
                       _pt_parentId = value;
                  }
                }
       }

       private String _pt_name;
       public String pt_name
       {
            get{return _pt_name;}
            set
               {
                  if (value != _pt_name)
                  {
                       _pt_name = value;
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
