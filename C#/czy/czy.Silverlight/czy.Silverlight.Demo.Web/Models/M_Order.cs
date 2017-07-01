using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class Order
    {
       private string _o_id;
       public string o_id
       {
            get{return _o_id;}
            set{_o_id = value;}
       }

       private string _o_orderDetailId;
       public string o_orderDetailId
       {
            get{return _o_orderDetailId;}
            set{_o_orderDetailId = value;}
       }

       private int _o_state;
       public int o_state
       {
            get{return _o_state;}
            set{_o_state = value;}
       }

       private DateTime _o_createDate;
       public DateTime o_createDate
       {
            get{return _o_createDate;}
            set{_o_createDate = value;}
       }

       private DateTime _o_startDate;
       public DateTime o_startDate
       {
            get{return _o_startDate;}
            set{_o_startDate = value;}
       }

       private DateTime _o_endDate;
       public DateTime o_endDate
       {
            get{return _o_endDate;}
            set{_o_endDate = value;}
       }

       private string _o_userId;
       public string o_userId
       {
            get{return _o_userId;}
            set{_o_userId = value;}
       }

       private string _o_orderUserId;
       public string o_orderUserId
       {
            get{return _o_orderUserId;}
            set{_o_orderUserId = value;}
       }

       private string _o_fromAddress;
       public string o_fromAddress
       {
            get{return _o_fromAddress;}
            set{_o_fromAddress = value;}
       }

       private string _o_toAddress;
       public string o_toAddress
       {
            get{return _o_toAddress;}
            set{_o_toAddress = value;}
       }

       private string _o_fromAddress;
       public string o_fromAddress
       {
            get{return _o_fromAddress;}
            set{_o_fromAddress = value;}
       }

       private string _o_toAddress;
       public string o_toAddress
       {
            get{return _o_toAddress;}
            set{_o_toAddress = value;}
       }

    }
}
