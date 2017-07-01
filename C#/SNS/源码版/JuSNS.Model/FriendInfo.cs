using System;

namespace JuSNS.Model
{

  public class FriendInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private Int32 _FriendID;
     private Int32 _State;
     private String _descript;
     private String _TrueName;
     private DateTime _PostTime;
     private Int32 _ClassID;
     private Int32 _FDegree;

     public FriendInfo()
     {}

     public FriendInfo(Int32 iD, Int32 userID, Int32 friendID, Int32 state, String descript, DateTime postTime, Int32 classID, Int32 fDegree, String trueName)
     {
         this._ID = iD;
         this._UserID = userID;
         this._FriendID = friendID;
         this._State = state;
         this._descript = descript;
         this._PostTime = postTime;
         this._ClassID = classID;
         this._FDegree = fDegree;
         this._TrueName = trueName;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 FriendID
     {
         get{return this._FriendID;}
         set{this._FriendID = value;}
     }

     public Int32 State
     {
         get{return this._State;}
         set{this._State = value;}
     }

     public String Descript
     {
         get { return this._descript; }
         set { this._descript = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
     }

     public Int32 FDegree
     {
         get{return this._FDegree;}
         set{this._FDegree = value;}
     }
  }
}