using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Town: INotifyPropertyChanged
    {
       private Int32 _t_id;
       public Int32 t_id
       {
            get{return _t_id;}
            set
               {
                  if (value != _t_id)
                  {
                       _t_id = value;
                  }
                }
       }

       private Int32 _t_cityId;
       public Int32 t_cityId
       {
            get{return _t_cityId;}
            set
               {
                  if (value != _t_cityId)
                  {
                       _t_cityId = value;
                  }
                }
       }

       private String _t_name;
       public String t_name
       {
            get{return _t_name;}
            set
               {
                  if (value != _t_name)
                  {
                       _t_name = value;
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
