/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopNewsInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopNewsInfo
  {
     private Int32 _id;
     private Int32 _ShopID;
     private String _Title;
     private String _Content;
     private DateTime _creatTime;
     private Boolean _islock;
     private Int32 _click;

     public ShopNewsInfo()
     {}

     public ShopNewsInfo(Int32 id,Int32 shopID,String title,String content,DateTime creatTime,Boolean islock,Int32 click)
     {
         this._id = id;
         this._ShopID = shopID;
         this._Title = title;
         this._Content = content;
         this._creatTime = creatTime;
         this._islock = islock;
         this._click = click;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 ShopID
     {
         get{return this._ShopID;}
         set{this._ShopID = value;}
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

     public DateTime CreatTime
     {
         get{return this._creatTime;}
         set{this._creatTime = value;}
     }

     public Boolean Islock
     {
         get{return this._islock;}
         set{this._islock = value;}
     }

     public Int32 Click
     {
         get{return this._click;}
         set{this._click = value;}
     }
  }
}