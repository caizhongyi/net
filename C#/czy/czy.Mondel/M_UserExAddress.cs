using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class UserExAddress: INotifyPropertyChanged
    {
       private Int32 _uea_id;
       public Int32 uea_id
       {
            get{return _uea_id;}
            set
               {
                  if (value != _uea_id)
                  {
                       _uea_id = value;
                  }
                }
       }

       private Int32 _uea_type;
       public Int32 uea_type
       {
            get{return _uea_type;}
            set
               {
                  if (value != _uea_type)
                  {
                       _uea_type = value;
                  }
                }
       }

       private String _uea_name;
       public String uea_name
       {
            get{return _uea_name;}
            set
               {
                  if (value != _uea_name)
                  {
                       _uea_name = value;
                  }
                }
       }

       private String _uea_address;
       public String uea_address
       {
            get{return _uea_address;}
            set
               {
                  if (value != _uea_address)
                  {
                       _uea_address = value;
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
