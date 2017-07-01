/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopMulteBuyInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopMulteBuyInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private Int32 _GoodsID;
     private String _Title;
     private String _Content;
     private Int32 _MinMember;
     private Int32 _MaxMember;
     private Byte _IsLock;
     private DateTime _StartTime;
     private DateTime _EndTime;
     private Int32 _JoinMember;
     private Int32 _AttMember;
     private Decimal _Price;
     private DateTime _PostTime;
     private String _PostIP;
     private Int32 _ProvinceID;
     private Int32 _CityID;
     private String _AddRess;
     private String _LinkStyle;
     private String _Keywords;
     private String _Pic;
     private Boolean _IsRec;

     public ShopMulteBuyInfo()
     {}

     public ShopMulteBuyInfo(Int32 id,Int32 userID,Int32 goodsID,String title,String content,Int32 minMember,Int32 maxMember,Byte isLock,DateTime startTime,DateTime endTime,Int32 joinMember,Int32 attMember,Decimal price,DateTime postTime,String postIP,Int32 provinceID,Int32 cityID,String addRess,String linkStyle,String keywords,String pic,Boolean isRec)
     {
         this._id = id;
         this._UserID = userID;
         this._GoodsID = goodsID;
         this._Title = title;
         this._Content = content;
         this._MinMember = minMember;
         this._MaxMember = maxMember;
         this._IsLock = isLock;
         this._StartTime = startTime;
         this._EndTime = endTime;
         this._JoinMember = joinMember;
         this._AttMember = attMember;
         this._Price = price;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._ProvinceID = provinceID;
         this._CityID = cityID;
         this._AddRess = addRess;
         this._LinkStyle = linkStyle;
         this._Keywords = keywords;
         this._Pic = pic;
         this._IsRec = isRec;
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

     public Int32 GoodsID
     {
         get{return this._GoodsID;}
         set{this._GoodsID = value;}
     }

     public String Title
     {
         get{return this._Title;}
         set{this._Title = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public Int32 MinMember
     {
         get{return this._MinMember;}
         set{this._MinMember = value;}
     }

     public Int32 MaxMember
     {
         get{return this._MaxMember;}
         set{this._MaxMember = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
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

     public Int32 JoinMember
     {
         get{return this._JoinMember;}
         set{this._JoinMember = value;}
     }

     public Int32 AttMember
     {
         get{return this._AttMember;}
         set{this._AttMember = value;}
     }

     public Decimal Price
     {
         get{return this._Price;}
         set{this._Price = value;}
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

     public Int32 ProvinceID
     {
         get{return this._ProvinceID;}
         set{this._ProvinceID = value;}
     }

     public Int32 CityID
     {
         get{return this._CityID;}
         set{this._CityID = value;}
     }

     public String AddRess
     {
         get{return this._AddRess;}
         set{this._AddRess = value;}
     }

     public String LinkStyle
     {
         get{return this._LinkStyle;}
         set{this._LinkStyle = value;}
     }

     public String Keywords
     {
         get{return this._Keywords;}
         set{this._Keywords = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Boolean IsRec
     {
         get{return this._IsRec;}
         set{this._IsRec = value;}
     }
  }
}