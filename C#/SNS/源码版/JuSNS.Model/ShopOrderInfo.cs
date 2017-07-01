/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopOrderInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopOrderInfo
  {
     private Int32 _id;
     private Int32 _GoodsID;
     private Int32 _UserID;
     private String _OrderNumber;
     private DateTime _PostTime;
     private Byte _IsLock;
     private String _PostIP;
     private Decimal _Money;
     private Int32 _GPoint;

     public ShopOrderInfo()
     {}

     public ShopOrderInfo(Int32 id,Int32 goodsID,Int32 userID,String orderNumber,DateTime postTime,Byte isLock,String postIP,Decimal money,Int32 gPoint)
     {
         this._id = id;
         this._GoodsID = goodsID;
         this._UserID = userID;
         this._OrderNumber = orderNumber;
         this._PostTime = postTime;
         this._IsLock = isLock;
         this._PostIP = postIP;
         this._Money = money;
         this._GPoint = gPoint;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 GoodsID
     {
         get{return this._GoodsID;}
         set{this._GoodsID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String OrderNumber
     {
         get{return this._OrderNumber;}
         set{this._OrderNumber = value;}
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

     public String PostIP
     {
         get{return this._PostIP;}
         set{this._PostIP = value;}
     }

     public Decimal Money
     {
         get{return this._Money;}
         set{this._Money = value;}
     }

     public Int32 GPoint
     {
         get{return this._GPoint;}
         set{this._GPoint = value;}
     }
  }
}