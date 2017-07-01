/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: FavoriteInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class FavoriteInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private String _URL;
     private Int32 _ClassID;
     private Boolean _IsPub;
     private String _title;
     private String _content;
     private DateTime _PostTime;

     public FavoriteInfo()
     {}

     public FavoriteInfo(Int32 id,Int32 userID,String uRL,Int32 classID,Boolean isPub,String title,String content,DateTime postTime)
     {
         this._id = id;
         this._UserID = userID;
         this._URL = uRL;
         this._ClassID = classID;
         this._IsPub = isPub;
         this._title = title;
         this._content = content;
         this._PostTime = postTime;
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

     public String URL
     {
         get{return this._URL;}
         set{this._URL = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
     }

     public Boolean IsPub
     {
         get{return this._IsPub;}
         set{this._IsPub = value;}
     }

     public String Title
     {
         get{return this._title;}
         set{this._title = value;}
     }

     public String Content
     {
         get{return this._content;}
         set{this._content = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }
  }
}