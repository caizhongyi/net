using System;

namespace JuSNS.Model
{

  public class AlbumInfo
  {
     private Int32 _AlbumID;
     private Int32 _UserID;
     private String _Title;
     private String _Description;
     private Int32 _ImagesCount;
     private DateTime _CreateTime;
     private Int32 _Privacy;
     private DateTime _LastUploadTime;
     private Int32 _GroupID;

     public AlbumInfo()
     {}

     public AlbumInfo(Int32 albumID,Int32 userID,String title,String description,Int32 imagesCount,DateTime createTime,Int32 privacy,DateTime lastUploadTime,Int32 groupID)
     {
         this._AlbumID = albumID;
         this._UserID = userID;
         this._Title = title;
         this._Description = description;
         this._ImagesCount = imagesCount;
         this._CreateTime = createTime;
         this._Privacy = privacy;
         this._LastUploadTime = lastUploadTime;
         this._GroupID = groupID;
     }


     public Int32 AlbumID
     {
         get{return this._AlbumID;}
         set{this._AlbumID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String Title
     {
         get{return this._Title;}
         set{this._Title = value;}
     }

     public String Description
     {
         get{return this._Description;}
         set{this._Description = value;}
     }

     public Int32 ImagesCount
     {
         get{return this._ImagesCount;}
         set{this._ImagesCount = value;}
     }

     public DateTime CreateTime
     {
         get{return this._CreateTime;}
         set{this._CreateTime = value;}
     }

     public Int32 Privacy
     {
         get{return this._Privacy;}
         set{this._Privacy = value;}
     }

     public DateTime LastUploadTime
     {
         get{return this._LastUploadTime;}
         set{this._LastUploadTime = value;}
     }

     public Int32 GroupID
     {
         get{return this._GroupID;}
         set{this._GroupID = value;}
     }
  }
}