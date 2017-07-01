using System;

namespace JuSNS.Model
{

  public class PhotoInfo
  {
     private Int32 _id;
     private Int32 _AlbumID;
     private Int32 _AtiveID;
     private Int32 _UserID;
     private String _Description;
     private Int32 _Views;
     private Int32 _FileSize;
     private Int32 _State;
     private Boolean _IsCover;
     private Int32 _Comments;
     private DateTime _PostTime;
     private String _PostIP;
     private Int32 _PhotoType;
     private Int32 _Width;
     private Int32 _Height;
     private String _FilePath;
     private Int32 _ShareNumber;
     private Boolean _IsLock;
     private Boolean _IsRec;
     private String _TrueName;
     private String _AlbumName;

     public PhotoInfo()
     {}

     public PhotoInfo(Int32 id, Int32 albumID, Int32 ativeID, Int32 userID, String description, Int32 views, Int32 fileSize, Int32 state, Boolean isCover, Boolean isRec, Int32 comments, DateTime postTime, String postIP, Int32 photoType, Int32 width, Int32 height, String filePath, Int32 shareNumber, Boolean isLock, String trueName, String albumName)
     {
         this._id = id;
         this._AlbumID = albumID;
         this._AtiveID = ativeID;
         this._UserID = userID;
         this._Description = description;
         this._Views = views;
         this._FileSize = fileSize;
         this._State = state;
         this._IsCover = isCover;
         this._Comments = comments;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._PhotoType = photoType;
         this._Width = width;
         this._Height = height;
         this._FilePath = filePath;
         this._ShareNumber = shareNumber;
         this._IsLock = isLock;
         this._IsRec = isRec;
         this._TrueName = trueName;
         this._AlbumName = albumName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 AlbumID
     {
         get{return this._AlbumID;}
         set{this._AlbumID = value;}
     }

     public Int32 AtiveID
     {
         get { return this._AtiveID; }
         set { this._AtiveID = value; }
     }

      

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String Description
     {
         get { return this._Description; }
         set { this._Description = value; }
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }
     public String AlbumName
     {
         get { return this._AlbumName; }
         set { this._AlbumName = value; }
     }

     public Int32 Views
     {
         get{return this._Views;}
         set{this._Views = value;}
     }

     public Int32 FileSize
     {
         get{return this._FileSize;}
         set{this._FileSize = value;}
     }

     public Int32 State
     {
         get{return this._State;}
         set{this._State = value;}
     }

     public Boolean IsCover
     {
         get { return this._IsCover; }
         set { this._IsCover = value; }
     }

     public Boolean IsRec
     {
         get { return this._IsRec; }
         set { this._IsRec = value; }
     }

      

     public Int32 Comments
     {
         get{return this._Comments;}
         set{this._Comments = value;}
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

     public Int32 PhotoType
     {
         get{return this._PhotoType;}
         set{this._PhotoType = value;}
     }

     public Int32 Width
     {
         get{return this._Width;}
         set{this._Width = value;}
     }

     public Int32 Height
     {
         get{return this._Height;}
         set{this._Height = value;}
     }

     public String FilePath
     {
         get{return this._FilePath;}
         set{this._FilePath = value;}
     }

     public Int32 ShareNumber
     {
         get{return this._ShareNumber;}
         set{this._ShareNumber = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}