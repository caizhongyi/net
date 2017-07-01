/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: FavoriteClassInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class FavoriteClassInfo
  {
     private Int32 _id;
     private String _ClassName;
     private Boolean _IsPub;
     private Int32 _UserID;

     public FavoriteClassInfo()
     {}

     public FavoriteClassInfo(Int32 id,String className,Boolean isPub,Int32 userID)
     {
         this._id = id;
         this._ClassName = className;
         this._IsPub = isPub;
         this._UserID = userID;
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

     public Boolean IsPub
     {
         get{return this._IsPub;}
         set{this._IsPub = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }
  }
}