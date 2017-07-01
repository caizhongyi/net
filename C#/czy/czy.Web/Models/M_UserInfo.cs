using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class UserInfo: INotifyPropertyChanged
    {
       private String _u_id;
       public String u_id
       {
            get{return _u_id;}
            set
               {
                  if (value != _u_id)
                  {
                       _u_id = value;
                  }
                }
       }

       private Int32 _u_typeId;
       public Int32 u_typeId
       {
            get{return _u_typeId;}
            set
               {
                  if (value != _u_typeId)
                  {
                       _u_typeId = value;
                  }
                }
       }

       private Int32 _u_province;
       public Int32 u_province
       {
            get{return _u_province;}
            set
               {
                  if (value != _u_province)
                  {
                       _u_province = value;
                  }
                }
       }

       private Int32 _u_city;
       public Int32 u_city
       {
            get{return _u_city;}
            set
               {
                  if (value != _u_city)
                  {
                       _u_city = value;
                  }
                }
       }

       private Int32 _u_town;
       public Int32 u_town
       {
            get{return _u_town;}
            set
               {
                  if (value != _u_town)
                  {
                       _u_town = value;
                  }
                }
       }

       private Int32 _u_qq;
       public Int32 u_qq
       {
            get{return _u_qq;}
            set
               {
                  if (value != _u_qq)
                  {
                       _u_qq = value;
                  }
                }
       }

       private Int32 _u_bussinessType;
       public Int32 u_bussinessType
       {
            get{return _u_bussinessType;}
            set
               {
                  if (value != _u_bussinessType)
                  {
                       _u_bussinessType = value;
                  }
                }
       }

       private Int32 _u_loginTime;
       public Int32 u_loginTime
       {
            get{return _u_loginTime;}
            set
               {
                  if (value != _u_loginTime)
                  {
                       _u_loginTime = value;
                  }
                }
       }

       private Int32 _u_loginMaxTime;
       public Int32 u_loginMaxTime
       {
            get{return _u_loginMaxTime;}
            set
               {
                  if (value != _u_loginMaxTime)
                  {
                       _u_loginMaxTime = value;
                  }
                }
       }

       private Int32 _u_state;
       public Int32 u_state
       {
            get{return _u_state;}
            set
               {
                  if (value != _u_state)
                  {
                       _u_state = value;
                  }
                }
       }

       private Decimal _u_money;
       public Decimal u_money
       {
            get{return _u_money;}
            set
               {
                  if (value != _u_money)
                  {
                       _u_money = value;
                  }
                }
       }

       private DateTime _u_birthday;
       public DateTime u_birthday
       {
            get{return _u_birthday;}
            set
               {
                  if (value != _u_birthday)
                  {
                       _u_birthday = value;
                  }
                }
       }

       private DateTime _u_createDate;
       public DateTime u_createDate
       {
            get{return _u_createDate;}
            set
               {
                  if (value != _u_createDate)
                  {
                       _u_createDate = value;
                  }
                }
       }

       private DateTime _u_loginDate;
       public DateTime u_loginDate
       {
            get{return _u_loginDate;}
            set
               {
                  if (value != _u_loginDate)
                  {
                       _u_loginDate = value;
                  }
                }
       }

       private Int64 _u_bankId;
       public Int64 u_bankId
       {
            get{return _u_bankId;}
            set
               {
                  if (value != _u_bankId)
                  {
                       _u_bankId = value;
                  }
                }
       }

       private String _u_name;
       public String u_name
       {
            get{return _u_name;}
            set
               {
                  if (value != _u_name)
                  {
                       _u_name = value;
                  }
                }
       }

       private String _u_pwd;
       public String u_pwd
       {
            get{return _u_pwd;}
            set
               {
                  if (value != _u_pwd)
                  {
                       _u_pwd = value;
                  }
                }
       }

       private String _u_address;
       public String u_address
       {
            get{return _u_address;}
            set
               {
                  if (value != _u_address)
                  {
                       _u_address = value;
                  }
                }
       }

       private String _u_tel;
       public String u_tel
       {
            get{return _u_tel;}
            set
               {
                  if (value != _u_tel)
                  {
                       _u_tel = value;
                  }
                }
       }

       private String _u_email;
       public String u_email
       {
            get{return _u_email;}
            set
               {
                  if (value != _u_email)
                  {
                       _u_email = value;
                  }
                }
       }

       private String _u_sex;
       public String u_sex
       {
            get{return _u_sex;}
            set
               {
                  if (value != _u_sex)
                  {
                       _u_sex = value;
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
