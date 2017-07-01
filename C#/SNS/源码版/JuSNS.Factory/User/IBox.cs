using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.User
{
    public interface IBox
    {
        int GetBoxUnRead(int UserID);
        int Add(MailInfo Info);
        int Del(int dType, int BoxID, int UserID);
        int Reply(MailInfo info);
        string GetNewReContent(int boxID, bool isMy, int UserID);
        int GetNewReCNT(int boxID, int UserID);
        int Read(int boxID, int UserID);
        void UpdateNoticeMode(int UserID);
        MailInfo Info(int ID);
    }

    public sealed partial class DataAccess
    {
        public static IBox CreateBox()
        {
            string className = path + ".User.Box";
            object objType = JuSNS.Common.DataCache.GetCache(className);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(className);
                    JuSNS.Common.DataCache.SetCache(className, objType);// 写入缓存
                }
                catch { }
            }
            return (IBox)objType;
        }
    }
}
