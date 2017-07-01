/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: GoodsCommentInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class GoodsCommentInfo
  {
     private Int32 _id;
     private Int32 _PID;
     private Int32 _UserID;
     private DateTime _PostTime;
     private String _PostIP;
     private Boolean _Islock;
     private Int32 _commid;
     private Byte _cType;
     private Int32 _ShopID;
     private String _TrueName;
     private String _Content;

     public GoodsCommentInfo()
     {}

     public GoodsCommentInfo(Int32 id,Int32 pID,Int32 userID,DateTime postTime,String postIP,Boolean islock,Int32 commid,Byte cType,Int32 shopID,String trueName,String content)
     {
         this._id = id;
         this._PID = pID;
         this._UserID = userID;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._Islock = islock;
         this._commid = commid;
         this._cType = cType;
         this._ShopID = shopID;
         this._TrueName = trueName;
         this._Content = content;
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

     public String Content
     {
         get { return this._Content; }
         set { this._Content = value; }
     }

     public Int32 PID
     {
         get{return this._PID;}
         set{this._PID = value;}
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

     public String PostIP
     {
         get{return this._PostIP;}
         set{this._PostIP = value;}
     }

     public Boolean Islock
     {
         get{return this._Islock;}
         set{this._Islock = value;}
     }

     public Int32 Commid
     {
         get{return this._commid;}
         set{this._commid = value;}
     }

     public Byte CType
     {
         get{return this._cType;}
         set{this._cType = value;}
     }

     public Int32 ShopID
     {
         get{return this._ShopID;}
         set{this._ShopID = value;}
     }
  }
}