using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.DataBaseModel;

namespace BBL
{
    //public interface IBBLOPertion
    //{
    //    DataTable GetAdTypeList();
       
    //}
    public interface IAdInfo
    {
        int InsetAdInfo(int Ad_ID, string Ad_name, string Ad_url, string Ad_operation,  int Ad_type_id,string Remark);
        DataSet SelectAdInfo();
        void UpdateAdInfo(AdInfoInfo adinfoinfo);
        void DelAdInfo(int id);
        
    }
        
    
}
