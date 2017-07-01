/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopPhotoInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopPhotoInfo
  {
     private Int32 _id;
     private Int32 _ShopID;
     private String _Title;
     private String _Pic;
     private Boolean _IsLock;

     public ShopPhotoInfo()
     {}

     public ShopPhotoInfo(Int32 id,Int32 shopID,String title,String pic,Boolean isLock)
     {
         this._id = id;
         this._ShopID = shopID;
         this._Title = title;
         this._Pic = pic;
         this._IsLock = isLock;
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

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}