using System;

namespace JuSNS.Model
{

  public class BlogInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private String _Title;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private Boolean _IsLock;
     private Int32 _Comments;
     private Int32 _Privacy;
     private String _PicPath;
     private String _TrueName;
     private DateTime _LastModTime;
     private Int32 _Reads;
     private Byte _isRec;
     private Byte _IsDraft;
     private Int32 _attnumber;
     private Int32 _click;
     private Int32 _ShareNumber;
     private Int32 _myClassID;
     private Int32 _sysClassID;

     public BlogInfo()
     {}

     public BlogInfo(Int32 iD, Int32 userID, String trueName, String title, String content, DateTime postTime, String postIP, Boolean isLock, Int32 comments, Int32 privacy, String picPath, DateTime lastModTime, Int32 reads, Byte isRec, Byte isDraft, Int32 attnumber, Int32 click, Int32 shareNumber, Int32 myClassID, Int32 sysClassID)
     {
         this._ID = iD;
         this._UserID = userID;
         this._Title = title;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._Comments = comments;
         this._Privacy = privacy;
         this._PicPath = picPath;
         this._LastModTime = lastModTime;
         this._Reads = reads;
         this._isRec = isRec;
         this._IsDraft = isDraft;
         this._attnumber = attnumber;
         this._click = click;
         this._ShareNumber = shareNumber;
         this._myClassID = myClassID;
         this._sysClassID = sysClassID;
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

     public String Title
     {
         get { return this._Title; }
         set { this._Title = value; }
     }
     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
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

     public Int32 Privacy
     {
         get{return this._Privacy;}
         set{this._Privacy = value;}
     }

     public String PicPath
     {
         get{return this._PicPath;}
         set{this._PicPath = value;}
     }

     public DateTime LastModTime
     {
         get{return this._LastModTime;}
         set{this._LastModTime = value;}
     }

     public Int32 Reads
     {
         get{return this._Reads;}
         set{this._Reads = value;}
     }

     public Byte IsRec
     {
         get{return this._isRec;}
         set{this._isRec = value;}
     }

     public Byte IsDraft
     {
         get{return this._IsDraft;}
         set{this._IsDraft = value;}
     }

     public Int32 Attnumber
     {
         get{return this._attnumber;}
         set{this._attnumber = value;}
     }

     public Int32 Click
     {
         get{return this._click;}
         set{this._click = value;}
     }

     public Int32 ShareNumber
     {
         get{return this._ShareNumber;}
         set{this._ShareNumber = value;}
     }

     public Int32 MyClassID
     {
         get{return this._myClassID;}
         set{this._myClassID = value;}
     }

     public Int32 SysClassID
     {
         get{return this._sysClassID;}
         set{this._sysClassID = value;}
     }
  }
}