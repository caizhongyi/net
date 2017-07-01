using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class Product
    {
       private string _p_id;
       public string p_id
       {
            get{return _p_id;}
            set{_p_id = value;}
       }

       private int _p_rebateValue;
       public int p_rebateValue
       {
            get{return _p_rebateValue;}
            set{_p_rebateValue = value;}
       }

       private decimal _p_price;
       public decimal p_price
       {
            get{return _p_price;}
            set{_p_price = value;}
       }

       private decimal _p_currentPrice;
       public decimal p_currentPrice
       {
            get{return _p_currentPrice;}
            set{_p_currentPrice = value;}
       }

       private DateTime _p_createDate;
       public DateTime p_createDate
       {
            get{return _p_createDate;}
            set{_p_createDate = value;}
       }

       private Int64 _p_count;
       public Int64 p_count
       {
            get{return _p_count;}
            set{_p_count = value;}
       }

       private Int64 _p_totalCount;
       public Int64 p_totalCount
       {
            get{return _p_totalCount;}
            set{_p_totalCount = value;}
       }

       private Int64 _p_needCount;
       public Int64 p_needCount
       {
            get{return _p_needCount;}
            set{_p_needCount = value;}
       }

       private Int64 _p_typeId;
       public Int64 p_typeId
       {
            get{return _p_typeId;}
            set{_p_typeId = value;}
       }

       private string _p_userId;
       public string p_userId
       {
            get{return _p_userId;}
            set{_p_userId = value;}
       }

       private string _p_name;
       public string p_name
       {
            get{return _p_name;}
            set{_p_name = value;}
       }

       private string _p_name;
       public string p_name
       {
            get{return _p_name;}
            set{_p_name = value;}
       }

    }
}
