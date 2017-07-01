/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AtiveCommentInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AtiveCommentInfo
  {
     private Int32 _id;
     private Int32 _userid;
     private Int32 _ativeID;
     private DateTime _posttime;
     private String _postIP;
     private String _TrueName;
     private Boolean _IsLock;
     private String _content;
     private Int32 _CommentID;

     public AtiveCommentInfo()
     {}

     public AtiveCommentInfo(Int32 id, Int32 userid, Int32 ativeID, DateTime posttime, String postIP, Boolean isLock, String content, Int32 commentID, String trueName)
     {
         this._id = id;
         this._userid = userid;
         this._ativeID = ativeID;
         this._posttime = posttime;
         this._postIP = postIP;
         this._IsLock = isLock;
         this._content = content;
         this._CommentID = commentID;
         this._TrueName = trueName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 Userid
     {
         get{return this._userid;}
         set{this._userid = value;}
     }

     public Int32 AtiveID
     {
         get{return this._ativeID;}
         set{this._ativeID = value;}
     }

     public DateTime Posttime
     {
         get{return this._posttime;}
         set{this._posttime = value;}
     }

     public String PostIP
     {
         get { return this._postIP; }
         set { this._postIP = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public String Content
     {
         get{return this._content;}
         set{this._content = value;}
     }

     public Int32 CommentID
     {
         get{return this._CommentID;}
         set{this._CommentID = value;}
     }
  }
}