using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class ProvinceInfo : BBL.Inface.IProvinceInfo 
    {
        public DAL.Dao.IProvinceListManage province = DAL.FactoryDao.GetProvinceListManage();
        public bool InsertProvinceInfo(DAL.Model.ProvinceList provinceList)
        {
            
            return province .AddProvinceList(provinceList);
        }
        public bool UpdateProvinceInfo(DAL.Model.ProvinceList provinceList)
        {
            return province .ModProvinceList(provinceList );
        }
        public bool DelProvinceInfobyID(int id)
        {
            return province .DelProvinceList(id);
        }
        public DataSet SelectProvinceInfo()
        {
            return province.SelProvinceList() ;
        }
    }
}
