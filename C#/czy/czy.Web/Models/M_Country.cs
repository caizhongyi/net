using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Country: INotifyPropertyChanged
    {
       private Int32 _c_id;
       public Int32 c_id
       {
            get{return _c_id;}
            set
               {
                  if (value != _c_id)
                  {
                       _c_id = value;
                  }
                }
       }

       private String _c_name;
       public String c_name
       {
            get{return _c_name;}
            set
               {
                  if (value != _c_name)
                  {
                       _c_name = value;
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
