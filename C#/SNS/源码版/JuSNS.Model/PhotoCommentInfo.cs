using System;

namespace JuSNS.Model
{

  public class PhotoCommentInfo
  {
     private Int32 _ID;
     private Int32 _PhotoID;
     private Int32 _UserID;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private String _TrueName;
     private Boolean _IsLock;
     private Int32 _CommentID;

     public PhotoCommentInfo()
     {}

     public PhotoCommentInfo(Int32 iD,Int32 photoID,Int32 userID,String content,DateTime postTime,String postIP,Boolean isLock,Int32 commentID,String trueName)
     {
         this._ID = iD;
         this._PhotoID = photoID;
         this._UserID = userID;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._CommentID = commentID;
         this._TrueName = trueName;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 PhotoID
     {
         get{return this._PhotoID;}
         set{this._PhotoID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
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

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public Int32 CommentID
     {
         get{return this._CommentID;}
         set{this._CommentID = value;}
     }
  }
}