/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AtiveMemberInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AtiveMemberInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private Int32 _Aid;
     private DateTime _PostTime;
     private Int32 _Members;
     private Byte _State;
     private String _TrueName;

     public AtiveMemberInfo()
     {}

     public AtiveMemberInfo(Int32 id, Int32 userID, Int32 aid, DateTime postTime, Int32 members, Byte state, String trueName)
     {
         this._id = id;
         this._UserID = userID;
         this._Aid = aid;
         this._PostTime = postTime;
         this._Members = members;
         this._TrueName = trueName;
         this._State = state;
     }


     public Int32 Id
     {
         get { return this._id; }
         set { this._id = value; }
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

     public Int32 Aid
     {
         get{return this._Aid;}
         set{this._Aid = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Int32 Members
     {
         get{return this._Members;}
         set{this._Members = value;}
     }

     public Byte State
     {
         get{return this._State;}
         set{this._State = value;}
     }
  }
}