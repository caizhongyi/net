using System;

namespace JuSNS.Model
{

  public class BlogClassInfo
  {
     private Int32 _id;
     private String _CName;
     private Int32 _UserID;
     private Int32 _OrderID;
     private Int32 _ParentID;

     public BlogClassInfo()
     {}

     public BlogClassInfo(Int32 id,String cName,Int32 userID,Int32 orderID,Int32 parentID)
     {
         this._id = id;
         this._CName = cName;
         this._UserID = userID;
         this._OrderID = orderID;
         this._ParentID = parentID;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String CName
     {
         get{return this._CName;}
         set{this._CName = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 OrderID
     {
         get{return this._OrderID;}
         set{this._OrderID = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
     }
  }
}