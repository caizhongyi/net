/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: DynInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class DynInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private Int32 _cUserID;
     private Int32 _dynType;
     private String _Content;
     private String _TrueName;
     private DateTime _PostTime;
     private Int32 _infoarr;

     public DynInfo()
     {}

     public DynInfo(Int32 iD, Int32 userID, Int32 cUserID, Int32 dynType, String content, DateTime postTime, Int32 infoarr, String trueName)
     {
         this._ID = iD;
         this._UserID = userID;
         this._cUserID = cUserID;
         this._dynType = dynType;
         this._Content = content;
         this._PostTime = postTime;
         this._infoarr = infoarr;
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

     public Int32 CUserID
     {
         get{return this._cUserID;}
         set{this._cUserID = value;}
     }

     public Int32 DynType
     {
         get{return this._dynType;}
         set{this._dynType = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
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

     public Int32 Infoarr
     {
         get{return this._infoarr;}
         set{this._infoarr = value;}
     }
  }
}