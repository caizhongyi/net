using System;

namespace JuSNS.Model
{

  public class GroupMemberInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private Int32 _GroupID;
     private DateTime _JoinTime;
     private Int32 _Grade;
     private Boolean _Islock;
     private String _TrueName;

     public GroupMemberInfo()
     {}

     public GroupMemberInfo(Int32 iD,Int32 userID,Int32 groupID,DateTime joinTime,Int32 grade,Boolean islock,String trueName)
     {
         this._ID = iD;
         this._UserID = userID;
         this._GroupID = groupID;
         this._JoinTime = joinTime;
         this._Grade = grade;
         this._Islock = islock;
         this._TrueName = trueName;
     }


     public Int32 ID
     {
         get { return this._ID; }
         set { this._ID = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 GroupID
     {
         get{return this._GroupID;}
         set{this._GroupID = value;}
     }

     public DateTime JoinTime
     {
         get{return this._JoinTime;}
         set{this._JoinTime = value;}
     }

     public Int32 Grade
     {
         get{return this._Grade;}
         set{this._Grade = value;}
     }

     public Boolean Islock
     {
         get{return this._Islock;}
         set{this._Islock = value;}
     }
  }
}