using System;

namespace JuSNS.Model
{

  public class NewsCommentInfo
  {
     private Int32 _id;
     private Int32 _NewsID;
     private String _content;
     private String _TrueName;
     private Int32 _UserID;
     private Int32 _parentID;
     private DateTime _PostTime;
     private String _PostIP;
     private Byte _IsLock;

     public NewsCommentInfo()
     {}

     public NewsCommentInfo(Int32 id,Int32 newsID,String content,Int32 userID,Int32 parentID,DateTime postTime,String postIP,Byte isLock,String trueName)
     {
         this._id = id;
         this._NewsID = newsID;
         this._content = content;
         this._TrueName = trueName;
         this._UserID = userID;
         this._parentID = parentID;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 NewsID
     {
         get{return this._NewsID;}
         set{this._NewsID = value;}
     }

     public String Content
     {
         get { return this._content; }
         set { this._content = value; }
     }
     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 ParentID
     {
         get{return this._parentID;}
         set{this._parentID = value;}
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
  }
}