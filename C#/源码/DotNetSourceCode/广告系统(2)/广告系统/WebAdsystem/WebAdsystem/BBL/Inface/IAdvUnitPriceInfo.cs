using System;
namespace BBL.Inface
{
    public interface IAdvUnitPriceInfo
    {
        bool DelAdvUnitPriceInfobyID(int rkid);
        bool InsertAdvUnitPriceInfo(DAL.Model.AdvUnitprice advUnitPrice, int rkid, int advTypeID);
        System.Data.DataSet SelectAdvUnitPriceInfo();
        bool UpdateAdvUnitPriceInfo(DAL.Model.AdvUnitprice advUnitPrice, int rkid, int advTypeID);
    }
}
