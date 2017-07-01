using System;

namespace JuSNS.Model
{

  public class TwitterInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private Boolean _IsLock;
     private Int32 _Comments;
     private String _MType;
     private String _TrueName;
     private Byte _isRec;
     private String _pic;
     private String _media;

     public TwitterInfo()
     {}

     public TwitterInfo(Int32 iD, Int32 userID, String content, DateTime postTime, String postIP, Boolean isLock, Int32 comments, String mType, Byte isRec, String pic, String media, String trueName)
     {
         this._ID = iD;
         this._UserID = userID;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._Comments = comments;
         this._MType = mType;
         this._isRec = isRec;
         this._pic = pic;
         this._media = media;
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

     public Int32 Comments
     {
         get{return this._Comments;}
         set{this._Comments = value;}
     }

     public String MType
     {
         get{return this._MType;}
         set{this._MType = value;}
     }

     public Byte IsRec
     {
         get{return this._isRec;}
         set{this._isRec = value;}
     }

     public String Pic
     {
         get{return this._pic;}
         set{this._pic = value;}
     }

     public String Media
     {
         get{return this._media;}
         set{this._media = value;}
     }
  }
}