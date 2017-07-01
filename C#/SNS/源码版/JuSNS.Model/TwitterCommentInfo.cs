/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: TwitterCommentInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class TwitterCommentInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private Int32 _Tid;
     private String _Content;
     private Int32 _CommentID;
     private DateTime _PostTime;
     private String _PostIP;
     private String _TrueName;
     private Boolean _IsLock;

     public TwitterCommentInfo()
     {}

     public TwitterCommentInfo(Int32 iD, Int32 userID, Int32 tid, String content, Int32 commentID, DateTime postTime, String postIP, Boolean isLock, String trueName)
     {
         this._ID = iD;
         this._UserID = userID;
         this._Tid = tid;
         this._Content = content;
         this._CommentID = commentID;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._TrueName = trueName;
         
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 Tid
     {
         get{return this._Tid;}
         set{this._Tid = value;}
     }

     public String Content
     {
         get { return this._Content; }
         set { this._Content = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public Int32 CommentID
     {
         get{return this._CommentID;}
         set{this._CommentID = value;}
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
  }
}