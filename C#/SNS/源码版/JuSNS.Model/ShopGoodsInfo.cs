/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopGoodsInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopGoodsInfo
  {
     private Int32 _id;
     private String _GoodsName;
     private Int32 _UserID;
     private Int32 _ShopID;
     private Double _Price;
     private Double _mPrice;
     private Double _MultePrice;
     private String _Tel;
     private String _AddRess;
     private String _keywords;
     private String _Content;
     private Int32 _Click;
     private Int32 _TopNumber;
     private Int32 _DownNumber;
     private DateTime _StartTime;
     private DateTime _EndTime;
     private Int32 _Number;
     private DateTime _PostTime;
     private String _PostIP;
     private String _TrueName;
     private Int32 _ClassID;
     private Int32 _AreaID;
     private Byte _ExpressStyle;
     private String _ExpressContent;
     private Byte _MulteBuy;
     private Int32 _MulteMinNumber;
     private Int32 _MulteMaxNumber;
     private Byte _IsLock;
     private Int32 _BuyNumber;
     private String _Pic;
     private String _ClassName;
     private Boolean _IsRec;
     private Boolean _IsOld;
     private Int32 _GPoint;
      
     public ShopGoodsInfo()
     {}

     public ShopGoodsInfo(Int32 id, String goodsName, Int32 userID, Int32 shopID, Double price, Double mPrice, Double multePrice, String tel, String addRess, String keywords, String content, Int32 click, Int32 topNumber, Int32 downNumber, DateTime startTime, DateTime endTime, Int32 number, DateTime postTime, String postIP, Int32 classID, Int32 areaID, Byte expressStyle, String expressContent, Byte multeBuy, Int32 multeMinNumber, Int32 multeMaxNumber, Byte isLock, Int32 buyNumber, String pic, Boolean isRec, Boolean isOld, String className, Int32 gPoint, String trueName)
     {
         this._id = id;
         this._GoodsName = goodsName;
         this._UserID = userID;
         this._ShopID = shopID;
         this._Price = price;
         this._mPrice = mPrice;
         this._Tel = tel;
         this._AddRess = addRess;
         this._keywords = keywords;
         this._Content = content;
         this._Click = click;
         this._TopNumber = topNumber;
         this._DownNumber = downNumber;
         this._StartTime = startTime;
         this._EndTime = endTime;
         this._Number = number;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._ClassID = classID;
         this._AreaID = areaID;
         this._ExpressStyle = expressStyle;
         this._ExpressContent = expressContent;
         this._MulteBuy = multeBuy;
         this._MulteMinNumber = multeMinNumber;
         this._MulteMaxNumber = multeMaxNumber;
         this._IsLock = isLock;
         this._BuyNumber = buyNumber;
         this._Pic = pic;
         this._IsRec = isRec;
         this._IsOld = isOld;
         this._ClassName = className;
         this._GPoint = gPoint;
         this._MultePrice = multePrice;
         this._TrueName = trueName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String GoodsName
     {
         get { return this._GoodsName; }
         set { this._GoodsName = value; }
     }

     public String ClassName
     {
         get { return this._ClassName; }
         set { this._ClassName = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public Int32 UserID
     {
         get { return this._UserID; }
         set { this._UserID = value; }
     }

     public Int32 GPoint
     {
         get { return this._GPoint; }
         set { this._GPoint = value; }
     }

     public Int32 ShopID
     {
         get{return this._ShopID;}
         set{this._ShopID = value;}
     }

     public Double Price
     {
         get{return this._Price;}
         set{this._Price = value;}
     }

     public Double MPrice
     {
         get { return this._mPrice; }
         set { this._mPrice = value; }
     }

     public Double MultePrice
     {
         get { return this._MultePrice; }
         set { this._MultePrice = value; }
     }

     public String Tel
     {
         get{return this._Tel;}
         set{this._Tel = value;}
     }

     public String AddRess
     {
         get{return this._AddRess;}
         set{this._AddRess = value;}
     }

     public String Keywords
     {
         get{return this._keywords;}
         set{this._keywords = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public Int32 Click
     {
         get{return this._Click;}
         set{this._Click = value;}
     }

     public Int32 TopNumber
     {
         get{return this._TopNumber;}
         set{this._TopNumber = value;}
     }

     public Int32 DownNumber
     {
         get{return this._DownNumber;}
         set{this._DownNumber = value;}
     }

     public DateTime StartTime
     {
         get{return this._StartTime;}
         set{this._StartTime = value;}
     }

     public DateTime EndTime
     {
         get{return this._EndTime;}
         set{this._EndTime = value;}
     }

     public Int32 Number
     {
         get{return this._Number;}
         set{this._Number = value;}
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

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
     }

     public Int32 AreaID
     {
         get{return this._AreaID;}
         set{this._AreaID = value;}
     }

     public Byte ExpressStyle
     {
         get{return this._ExpressStyle;}
         set{this._ExpressStyle = value;}
     }

     public String ExpressContent
     {
         get{return this._ExpressContent;}
         set{this._ExpressContent = value;}
     }

     public Byte MulteBuy
     {
         get{return this._MulteBuy;}
         set{this._MulteBuy = value;}
     }

     public Int32 MulteMinNumber
     {
         get{return this._MulteMinNumber;}
         set{this._MulteMinNumber = value;}
     }

     public Int32 MulteMaxNumber
     {
         get{return this._MulteMaxNumber;}
         set{this._MulteMaxNumber = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public Int32 BuyNumber
     {
         get{return this._BuyNumber;}
         set{this._BuyNumber = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Boolean IsRec
     {
         get { return this._IsRec; }
         set { this._IsRec = value; }
     }

     public Boolean IsOld
     {
         get { return this._IsOld; }
         set { this._IsOld = value; }
     }
  }
}