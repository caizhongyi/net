using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace DAL.Dao.Present
{
    public interface IPresent
    {
        DataSet GetPresentList();
        void UpdataPresentCount(string Pid,int num);
        int InsertChangePresent(T_CustomerChangePresentInfo ccpi);
        void updatePresentInttral(string Num, string CusId);
        DataSet GetDefaultPresentList();
        bool InsertPicture(T_PresentInfo tpi);
        DataSet GetTypeList();
        void delMyPresent(string userid,string pid);
        DataSet MyPresent(string userid);
        DataSet SelExchangePresent(string userid);
        DataSet SelExchange(string userid, string info);
        void UpdateExchangePresent(string ccpid, string info);

        DataSet SelExchangeUp(string ccpid);
        DataSet SelPicture(string pid);
        DataSet SelAllPictureInfo(string pid);

        DataSet dtSelActivityInfo();

        DataSet dtSelSelSelActivityInfoByAID(int AId);
    }
}
