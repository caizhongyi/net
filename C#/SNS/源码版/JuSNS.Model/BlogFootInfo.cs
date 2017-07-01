using System;

namespace JuSNS.Model
{

  public class BlogFootInfo
  {
     private Int32 _id;
     private Int32 _BlogID;
     private Int32 _UserID;
     private DateTime _CreatTime;
     private String _TrueName;

     public BlogFootInfo()
     {}

     public BlogFootInfo(Int32 id,Int32 blogID,Int32 userID,DateTime creatTime,String trueName)
     {
         this._id = id;
         this._BlogID = blogID;
         this._UserID = userID;
         this._CreatTime = creatTime;
         this._TrueName = trueName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 BlogID
     {
         get{return this._BlogID;}
         set{this._BlogID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public DateTime CreatTime
     {
         get { return this._CreatTime; }
         set { this._CreatTime = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }
  }
}