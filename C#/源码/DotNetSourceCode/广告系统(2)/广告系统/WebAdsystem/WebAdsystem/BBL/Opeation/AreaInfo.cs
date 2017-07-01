using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class AreaInfo : BBL.Inface.IAreaInfo 
    {
        DAL.Dao.IAreaManage AreaMange = DAL.FactoryDao.GetAreaManage();
        public bool InsertAreaInfo(DAL.Model.AreaList areaList, int provinceID)
        {
            
            return AreaMange.AddArea(areaList,provinceID );
        }
        public bool UpdateAreaInfo(DAL.Model.AreaList areaList,int provinceID)
        {
            return  AreaMange.ModArea(areaList,provinceID);
        }
        public bool DelAreaInfobyID(int id)
        {
            return AreaMange .DelArea(id);
        }
        public DataSet SelectAreaInfo()
        {
            return AreaMange.SelArea() ;
        }
    }
}
