using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IAtive
    {
        List<AtiveClassInfo> GetAtiveClassList(int parentid);
        int InsertUpdate(AtiveInfo info);
        AtiveInfo GetAtiveInfo(object aid);
        AtiveClassInfo GetAtiveClassInfo(object aid);
        int InsetAtiveClass(AtiveClassInfo info);
        List<AtiveInfo> GetFriendAtive(int number, string q, int userid);
        void UpdateClick(int aid);
        int GetMembers(int aid, int flag);
        int JoinAtive(int aid, int uid, int flag);
        int GetAtiveATT(int aid, int uid);
        int OutAtive(int aid, int uid);
        int GetCheckMembers(int aid);
        List<AtiveMemberInfo> GetCheckMemberList(int number,int aid,int flag);
        int CheckAtiveMember(int mid,int aid, int flag);
        int AtiveImgCount(int aid);
        int TheNumber(int aid, int photoID);
        int PrePhotoID(int aid, int photoID);
        int NextPhotoID(int aid, int photoID);
        List<PhotoInfo> GetAtiveAlbumList(int number, int aid);
        int InsertAtiveComment(AtiveCommentInfo info);
        int DeleteAtive(int aid, int uid);
        int DeleteAtiveClass(int aid, int uid);
        AtiveCommentInfo GetAtiveCommentInfo(int cid);
        int DeleteAtiveComment(int aid, int uid);
        PhotoInfo GetInfo(object PhotoID);
    }
    public sealed partial class DataAccess
    {
        public static IAtive CreateAtive()
        {
            string className = path + ".App.Ative";
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
            return (IAtive)objType;
        }
    }
}
