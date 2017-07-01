using System;
using System.Data;
using DAL.DataBaseModel;
namespace BBL
{
    public interface IWbInfo
    {
        int InsetWbInfo(int Wb_ID, string WB_Name, string WB_IP, string WB_Address, string WB_Phone, string WB_Remark);
        DataSet GetWbInfo();
        void UpdataWbInfo(WBInfoInfo wbinfo);
        void DelWbInfo(int id);
    }
}
