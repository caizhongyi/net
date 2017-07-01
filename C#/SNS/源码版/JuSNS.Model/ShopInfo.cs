/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopInfo
  {
     private Int32 _id;
     private String _ShopName;
     private String _CompanyName;
     private String _Faren;
     private String _FarenMobile;
     private String _linkMan;
     private String _Mobile;
     private String _Tel;
     private String _Fax;
     private String _AddRess;
     private String _PostCode;
     private Int32 _ClassID;
     private Int32 _AreaID;
     private String _ShopRName;
     private String _ShopAddress;
     private String _JoinCase;
     private Byte _HasSerive;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private Byte _IsLock;
     private Int32 _UserID;
     private String _Keywords;
     private String _TrueName;
     private Int32 _TopNumber;
     private Int32 _DownNumber;
     private Int32 _Click;
     private String _Pic;
     private Boolean _IsRec;

     public ShopInfo()
     {}

     public ShopInfo(Int32 id, String shopName, String companyName, String faren, String farenMobile, String linkMan, String mobile, String tel, String fax, String addRess, String postCode, Int32 classID, Int32 areaID, String shopRName, String shopAddress, String joinCase, Byte hasSerive, String content, DateTime postTime, String postIP, Byte isLock, Int32 userID, String keywords, Int32 topNumber, Int32 downNumber, Int32 click, String pic, Boolean isRec, String trueName)
     {
         this._id = id;
         this._ShopName = shopName;
         this._CompanyName = companyName;
         this._Faren = faren;
         this._FarenMobile = farenMobile;
         this._linkMan = linkMan;
         this._Mobile = mobile;
         this._Tel = tel;
         this._Fax = fax;
         this._AddRess = addRess;
         this._PostCode = postCode;
         this._ClassID = classID;
         this._AreaID = areaID;
         this._ShopRName = shopRName;
         this._ShopAddress = shopAddress;
         this._JoinCase = joinCase;
         this._HasSerive = hasSerive;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._UserID = userID;
         this._Keywords = keywords;
         this._TopNumber = topNumber;
         this._DownNumber = downNumber;
         this._Click = click;
         this._Pic = pic;
         this._IsRec = isRec;
         this._TrueName = trueName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String ShopName
     {
         get{return this._ShopName;}
         set{this._ShopName = value;}
     }

     public String CompanyName
     {
         get { return this._CompanyName; }
         set { this._CompanyName = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public String Faren
     {
         get{return this._Faren;}
         set{this._Faren = value;}
     }

     public String FarenMobile
     {
         get{return this._FarenMobile;}
         set{this._FarenMobile = value;}
     }

     public String LinkMan
     {
         get{return this._linkMan;}
         set{this._linkMan = value;}
     }

     public String Mobile
     {
         get{return this._Mobile;}
         set{this._Mobile = value;}
     }

     public String Tel
     {
         get{return this._Tel;}
         set{this._Tel = value;}
     }

     public String Fax
     {
         get{return this._Fax;}
         set{this._Fax = value;}
     }

     public String AddRess
     {
         get{return this._AddRess;}
         set{this._AddRess = value;}
     }

     public String PostCode
     {
         get{return this._PostCode;}
         set{this._PostCode = value;}
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

     public String ShopRName
     {
         get{return this._ShopRName;}
         set{this._ShopRName = value;}
     }

     public String ShopAddress
     {
         get{return this._ShopAddress;}
         set{this._ShopAddress = value;}
     }

     public String JoinCase
     {
         get{return this._JoinCase;}
         set{this._JoinCase = value;}
     }

     public Byte HasSerive
     {
         get{return this._HasSerive;}
         set{this._HasSerive = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
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

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String Keywords
     {
         get{return this._Keywords;}
         set{this._Keywords = value;}
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

     public Int32 Click
     {
         get{return this._Click;}
         set{this._Click = value;}
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