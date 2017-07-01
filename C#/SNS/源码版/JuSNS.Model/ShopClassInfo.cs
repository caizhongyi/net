/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopClassInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopClassInfo
  {
     private Int32 _id;
     private String _ClassName;
     private Int32 _ParentID;
     private Boolean _IsLock;
     private Int32 _OrderID;

     public ShopClassInfo()
     {}

     public ShopClassInfo(Int32 id,String className,Int32 parentID,Boolean isLock,Int32 orderID)
     {
         this._id = id;
         this._ClassName = className;
         this._ParentID = parentID;
         this._IsLock = isLock;
         this._OrderID = orderID;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String ClassName
     {
         get{return this._ClassName;}
         set{this._ClassName = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
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
  }
}