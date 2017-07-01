/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: GBookInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class GBookInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private Int32 _SendID;
     private String _Content;
     private Int32 _ParentID;
     private DateTime _PostTime;
     private Byte _IsLock;

     public GBookInfo()
     {}

     public GBookInfo(Int32 id,Int32 userID,Int32 sendID,String content,Int32 parentID,DateTime postTime,Byte isLock)
     {
         this._id = id;
         this._UserID = userID;
         this._SendID = sendID;
         this._Content = content;
         this._ParentID = parentID;
         this._PostTime = postTime;
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

     public Int32 SendID
     {
         get{return this._SendID;}
         set{this._SendID = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}