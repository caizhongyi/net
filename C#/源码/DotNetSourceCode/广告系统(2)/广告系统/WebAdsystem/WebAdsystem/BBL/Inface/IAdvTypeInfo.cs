using System;
namespace BBL.Inface
{
    public interface IAdvTypeInfo
    {
        bool DelAdvTypeInfobyID(int id);
        bool InsertAdvTypeInfo(DAL.Model.AdvType advType);
        System.Data.DataSet SelectAdvTypeInfo();
        bool UpdateAdvTypeInfo(DAL.Model.AdvType advType);
    }
}
