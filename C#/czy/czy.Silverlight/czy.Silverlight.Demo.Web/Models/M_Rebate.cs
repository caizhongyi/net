using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class Rebate
    {
       private int _r_id;
       public int r_id
       {
            get{return _r_id;}
            set{_r_id = value;}
       }

       private int _R_userTypeId;
       public int R_userTypeId
       {
            get{return _R_userTypeId;}
            set{_R_userTypeId = value;}
       }

       private int _R_rebateValue;
       public int R_rebateValue
       {
            get{return _R_rebateValue;}
            set{_R_rebateValue = value;}
       }

       private string _R_userId;
       public string R_userId
       {
            get{return _R_userId;}
            set{_R_userId = value;}
       }

    }
}
