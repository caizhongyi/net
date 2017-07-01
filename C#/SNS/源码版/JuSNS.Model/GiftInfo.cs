/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: GiftInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class GiftInfo
  {
     private Int32 _id;
     private String _GiftName;
     private String _Pic;
     private Int32 _GPoint;
     private Int32 _Point;
     private DateTime _PostTime;
     private Int32 _SendNumber;
     private String _Content;
     private Int32 _ClassID;
     private Boolean _IsAd;
     private Byte _IsLock;

     public GiftInfo()
     {}

     public GiftInfo(Int32 id,String giftName,String pic,Int32 gPoint,Int32 point,DateTime postTime,Int32 sendNumber,String content,Int32 classID,Boolean isAd,Byte isLock)
     {
         this._id = id;
         this._GiftName = giftName;
         this._Pic = pic;
         this._GPoint = gPoint;
         this._Point = point;
         this._PostTime = postTime;
         this._SendNumber = sendNumber;
         this._Content = content;
         this._ClassID = classID;
         this._IsAd = isAd;
         this._IsLock = isLock;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String GiftName
     {
         get{return this._GiftName;}
         set{this._GiftName = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Int32 GPoint
     {
         get{return this._GPoint;}
         set{this._GPoint = value;}
     }

     public Int32 Point
     {
         get{return this._Point;}
         set{this._Point = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Int32 SendNumber
     {
         get{return this._SendNumber;}
         set{this._SendNumber = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
     }

     public Boolean IsAd
     {
         get{return this._IsAd;}
         set{this._IsAd = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}