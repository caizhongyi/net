using System;
namespace BBL.Inface
{
    public interface IProvinceInfo
    {
        bool DelProvinceInfobyID(int id);
        bool InsertProvinceInfo(DAL.Model.ProvinceList provinceList);
        System.Data.DataSet SelectProvinceInfo();
        bool UpdateProvinceInfo(DAL.Model.ProvinceList provinceList);
    }
}
