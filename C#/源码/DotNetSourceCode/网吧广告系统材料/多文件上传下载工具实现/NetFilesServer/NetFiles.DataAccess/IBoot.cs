using System;
using System.Collections.Generic;
using System.Text;
using HFSoft.Data;
using HFSoft.Data.Expressions;
namespace NetFiles.DataAccess
{
    public interface IBoot
    {
        void Add(Entities.BootInfo item);
        void Edit(Entities.BootInfo item);
        Entities.BootInfo Load(string directory);
        void Delete(string directory);
        IList<Entities.BootInfo> List();
    }
    class Boot:IBoot
    {
        #region IBoot ≥…‘±

        void IBoot.Add(NetFiles.DataAccess.Entities.BootInfo item)
        {
            DAOContext.Add(item);
        }

        void IBoot.Edit(NetFiles.DataAccess.Entities.BootInfo item)
        {
            DAOContext.Edit(item);
        }

        NetFiles.DataAccess.Entities.BootInfo IBoot.Load(string directory)
        {
            return DAOContext.Load<Entities.BootInfo>(directory);
        }

        void IBoot.Delete(string directory)
        {
            (Entities.DBMapping.BootInfo.BootDirectory == directory).Delete();
        }

        IList<NetFiles.DataAccess.Entities.BootInfo> IBoot.List()
        {
            HFSoft.Data.Expressions.Expression exp = new Expression(
                Entities.DBMapping.BootInfo);
            return exp.List<Entities.BootInfo>();
        }

        #endregion
    }
}
