using System;
namespace BBL.Inface
{
   public  interface IAdvInfo
    {
        bool DelAdvInfobyID(int advid);
        bool InsertAdvInfo(DAL.Model.AdvInfo AdvInfo, int masterID, int userID);
        System.Data.DataSet SelectAdvInfo();
        bool UpdateAdvInfo(DAL.Model.AdvInfo AdvInfo, int masterID, int userID);
    }
}
