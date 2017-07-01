/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: GiftUserInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class GiftUserInfo
  {
     private Int32 _id;
     private Int32 _giftID;
     private Int32 _UserID;
     private Int32 _ReviceID;
     private DateTime _PostTime;
     private String _Content;

     public GiftUserInfo()
     {}

     public GiftUserInfo(Int32 id,Int32 giftID,Int32 userID,Int32 reviceID,DateTime postTime,String content)
     {
         this._id = id;
         this._giftID = giftID;
         this._UserID = userID;
         this._ReviceID = reviceID;
         this._PostTime = postTime;
         this._Content = content;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 GiftID
     {
         get{return this._giftID;}
         set{this._giftID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 ReviceID
     {
         get{return this._ReviceID;}
         set{this._ReviceID = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }
  }
}