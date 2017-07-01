/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: FlashInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class FlashInfo
  {
     private Int32 _id;
     private String _bPic;
     private String _sPic;
     private String _URL;
     private Boolean _IsLock;
     private Int32 _OrderID;
     private DateTime _PostTime;

     public FlashInfo()
     {}

     public FlashInfo(Int32 id,String bPic,String sPic,String uRL,Boolean isLock,Int32 orderID,DateTime postTime)
     {
         this._id = id;
         this._bPic = bPic;
         this._sPic = sPic;
         this._URL = uRL;
         this._IsLock = isLock;
         this._OrderID = orderID;
         this._PostTime = postTime;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String BPic
     {
         get{return this._bPic;}
         set{this._bPic = value;}
     }

     public String SPic
     {
         get{return this._sPic;}
         set{this._sPic = value;}
     }

     public String URL
     {
         get{return this._URL;}
         set{this._URL = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public Int32 OrderID
     {
         get{return this._OrderID;}
         set{this._OrderID = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }
  }
}