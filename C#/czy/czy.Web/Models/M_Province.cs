using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Province: INotifyPropertyChanged
    {
       private Int32 _p_id;
       public Int32 p_id
       {
            get{return _p_id;}
            set
               {
                  if (value != _p_id)
                  {
                       _p_id = value;
                  }
                }
       }

       private Int32 _p_countryId;
       public Int32 p_countryId
       {
            get{return _p_countryId;}
            set
               {
                  if (value != _p_countryId)
                  {
                       _p_countryId = value;
                  }
                }
       }

       private String _p_name;
       public String p_name
       {
            get{return _p_name;}
            set
               {
                  if (value != _p_name)
                  {
                       _p_name = value;
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
