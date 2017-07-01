using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Menu: INotifyPropertyChanged
    {
       private Int32 _m_id;
       public Int32 m_id
       {
            get{return _m_id;}
            set
               {
                  if (value != _m_id)
                  {
                       _m_id = value;
                  }
                }
       }

       private String _m_url;
       public String m_url
       {
            get{return _m_url;}
            set
               {
                  if (value != _m_url)
                  {
                       _m_url = value;
                  }
                }
       }

       private String _m_name;
       public String m_name
       {
            get{return _m_name;}
            set
               {
                  if (value != _m_name)
                  {
                       _m_name = value;
                  }
                }
       }

       private String _m_parentId;
       public String m_parentId
       {
            get{return _m_parentId;}
            set
               {
                  if (value != _m_parentId)
                  {
                       _m_parentId = value;
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
