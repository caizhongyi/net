/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: JoinVipInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class JoinVipInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private DateTime _PostTime;
     private DateTime _EndTime;
     private Byte _IsLock;

     public JoinVipInfo()
     {}

     public JoinVipInfo(Int32 id,Int32 userID,DateTime postTime,DateTime endTime,Byte isLock)
     {
         this._id = id;
         this._UserID = userID;
         this._PostTime = postTime;
         this._EndTime = endTime;
         this._IsLock = isLock;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public DateTime EndTime
     {
         get{return this._EndTime;}
         set{this._EndTime = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}