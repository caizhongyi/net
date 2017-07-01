using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Models
{
    public class Product: INotifyPropertyChanged
    {
       private String _p_id;
       public String p_id
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

       private Int32 _p_rebateValue;
       public Int32 p_rebateValue
       {
            get{return _p_rebateValue;}
            set
               {
                  if (value != _p_rebateValue)
                  {
                       _p_rebateValue = value;
                  }
                }
       }

       private Int32 _P_imageId;
       public Int32 P_imageId
       {
            get{return _P_imageId;}
            set
               {
                  if (value != _P_imageId)
                  {
                       _P_imageId = value;
                  }
                }
       }

       private Decimal _p_price;
       public Decimal p_price
       {
            get{return _p_price;}
            set
               {
                  if (value != _p_price)
                  {
                       _p_price = value;
                  }
                }
       }

       private Decimal _p_currentPrice;
       public Decimal p_currentPrice
       {
            get{return _p_currentPrice;}
            set
               {
                  if (value != _p_currentPrice)
                  {
                       _p_currentPrice = value;
                  }
                }
       }

       private DateTime _p_createDate;
       public DateTime p_createDate
       {
            get{return _p_createDate;}
            set
               {
                  if (value != _p_createDate)
                  {
                       _p_createDate = value;
                  }
                }
       }

       private Int64 _p_count;
       public Int64 p_count
       {
            get{return _p_count;}
            set
               {
                  if (value != _p_count)
                  {
                       _p_count = value;
                  }
                }
       }

       private Int64 _p_totalCount;
       public Int64 p_totalCount
       {
            get{return _p_totalCount;}
            set
               {
                  if (value != _p_totalCount)
                  {
                       _p_totalCount = value;
                  }
                }
       }

       private Int64 _p_needCount;
       public Int64 p_needCount
       {
            get{return _p_needCount;}
            set
               {
                  if (value != _p_needCount)
                  {
                       _p_needCount = value;
                  }
                }
       }

       private Int64 _p_typeId;
       public Int64 p_typeId
       {
            get{return _p_typeId;}
            set
               {
                  if (value != _p_typeId)
                  {
                       _p_typeId = value;
                  }
                }
       }

       private Int64 _p_brandId;
       public Int64 p_brandId
       {
            get{return _p_brandId;}
            set
               {
                  if (value != _p_brandId)
                  {
                       _p_brandId = value;
                  }
                }
       }

       private Int64 _p_materialId;
       public Int64 p_materialId
       {
            get{return _p_materialId;}
            set
               {
                  if (value != _p_materialId)
                  {
                       _p_materialId = value;
                  }
                }
       }

       private Int64 _p_sizeId;
       public Int64 p_sizeId
       {
            get{return _p_sizeId;}
            set
               {
                  if (value != _p_sizeId)
                  {
                       _p_sizeId = value;
                  }
                }
       }

       private Int64 _p_colorId;
       public Int64 p_colorId
       {
            get{return _p_colorId;}
            set
               {
                  if (value != _p_colorId)
                  {
                       _p_colorId = value;
                  }
                }
       }

       private String _p_userId;
       public String p_userId
       {
            get{return _p_userId;}
            set
               {
                  if (value != _p_userId)
                  {
                       _p_userId = value;
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

       private String _p_description;
       public String p_description
       {
            get{return _p_description;}
            set
               {
                  if (value != _p_description)
                  {
                       _p_description = value;
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
