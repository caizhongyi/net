/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShareInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShareInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private Byte _ShareType;
     private Int32 _infoid;
     private DateTime _PostTime;
     private String _PostIP;
     private Byte _IsLock;
     private String _Title;
     private String _Content;
     private String _webURL;
     private Int32 _Comments;
     private Boolean _IsRec;

     public ShareInfo()
     {}

     public ShareInfo(Int32 id,Int32 userID,Byte shareType,Int32 infoid,DateTime postTime,String postIP,Byte isLock,String title,String content,String webURL,Int32 comments,Boolean isRec)
     {
         this._id = id;
         this._UserID = userID;
         this._ShareType = shareType;
         this._infoid = infoid;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._Title = title;
         this._Content = content;
         this._webURL = webURL;
         this._Comments = comments;
         this._IsRec = isRec;
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

     public Byte ShareType
     {
         get{return this._ShareType;}
         set{this._ShareType = value;}
     }

     public Int32 Infoid
     {
         get{return this._infoid;}
         set{this._infoid = value;}
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

     public String Title
     {
         get{return this._Title;}
         set{this._Title = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public String WebURL
     {
         get{return this._webURL;}
         set{this._webURL = value;}
     }

     public Int32 Comments
     {
         get{return this._Comments;}
         set{this._Comments = value;}
     }

     public Boolean IsRec
     {
         get{return this._IsRec;}
         set{this._IsRec = value;}
     }
  }
}