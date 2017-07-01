using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class V_UserInfo: INotifyPropertyChanged
    {
       private Int32 _ub_number;
       public Int32 ub_number
       {
            get{return _ub_number;}
            set
               {
                  if (value != _ub_number)
                  {
                       _ub_number = value;
                  }
                }
       }

       private Int32 _ub_province;
       public Int32 ub_province
       {
            get{return _ub_province;}
            set
               {
                  if (value != _ub_province)
                  {
                       _ub_province = value;
                  }
                }
       }

       private Int32 _ub_city;
       public Int32 ub_city
       {
            get{return _ub_city;}
            set
               {
                  if (value != _ub_city)
                  {
                       _ub_city = value;
                  }
                }
       }

       private Int32 _ub_town;
       public Int32 ub_town
       {
            get{return _ub_town;}
            set
               {
                  if (value != _ub_town)
                  {
                       _ub_town = value;
                  }
                }
       }

       private Int32 _ut_id;
       public Int32 ut_id
       {
            get{return _ut_id;}
            set
               {
                  if (value != _ut_id)
                  {
                       _ut_id = value;
                  }
                }
       }

       private Int64 _ub_id;
       public Int64 ub_id
       {
            get{return _ub_id;}
            set
               {
                  if (value != _ub_id)
                  {
                       _ub_id = value;
                  }
                }
       }

       private String _ub_name;
       public String ub_name
       {
            get{return _ub_name;}
            set
               {
                  if (value != _ub_name)
                  {
                       _ub_name = value;
                  }
                }
       }

       private String _ub_country;
       public String ub_country
       {
            get{return _ub_country;}
            set
               {
                  if (value != _ub_country)
                  {
                       _ub_country = value;
                  }
                }
       }

       private String _ub_branch;
       public String ub_branch
       {
            get{return _ub_branch;}
            set
               {
                  if (value != _ub_branch)
                  {
                       _ub_branch = value;
                  }
                }
       }

       private String _ut_name;
       public String ut_name
       {
            get{return _ut_name;}
            set
               {
                  if (value != _ut_name)
                  {
                       _ut_name = value;
                  }
                }
       }

       private String _ut_remark;
       public String ut_remark
       {
            get{return _ut_remark;}
            set
               {
                  if (value != _ut_remark)
                  {
                       _ut_remark = value;
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
