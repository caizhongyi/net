using System;
namespace BBL.Inface
{
    public interface IAreaInfo
    {
        bool DelAreaInfobyID(int id);
        bool InsertAreaInfo(DAL.Model.AreaList areaList, int provinceID);
        System.Data.DataSet SelectAreaInfo();
        bool UpdateAreaInfo(DAL.Model.AreaList areaList, int provinceID);
    }
}
