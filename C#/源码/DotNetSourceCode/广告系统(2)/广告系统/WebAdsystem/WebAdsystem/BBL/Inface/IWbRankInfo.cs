using System;
namespace BBL.Inface
{
    public interface IWbRankInfo
    {
        bool DelWbRankInfobyID(int id);
        bool InsertWbRankInfo(DAL.Model.WbRank wbRank);
        System.Data.DataSet SelectWbRankInfo();
        bool UpdateWbRankInfo(DAL.Model.WbRank wbRank);
    }
}
