using System;

namespace JuSNS.Model
{

  public class PrivacyInfo
  {
     private Int32 _UserID;
     private Int32 _PrivSpace;
     private Int32 _PrivFavourite;
     private Int32 _PrivEducate;
     private Int32 _PrivLasso;
     private Int32 _PrivFriends;
     private Int32 _PrivLeaveWord;
     private Int32 _PrivMiniBlog;
     private Int32 _PrivShare;
     private Int32 _PrivGroup;
     private Int32 _PrivMovies;
     private Boolean _ActUpdateData;
     private Boolean _ActAddFriend;
     private Boolean _ActLeaveWord;
     private Boolean _ActPicComment;
     private Boolean _ActSecede;
     private Boolean _ActDeliver;
     private Boolean _ActLogComment;
     private Boolean _ActMovieComment;
     private Boolean _ActPhotoLasso;
     private Boolean _ActShareComment;
     private DateTime _LastPostTime;
     private String _LastPostIP;

     public PrivacyInfo()
     {}

     public PrivacyInfo(Int32 userID,Int32 privSpace,Int32 privFavourite,Int32 privEducate,Int32 privLasso,Int32 privFriends,Int32 privLeaveWord,Int32 privMiniBlog,Int32 privShare,Int32 privGroup,Int32 privMovies,Boolean actUpdateData,Boolean actAddFriend,Boolean actLeaveWord,Boolean actPicComment,Boolean actSecede,Boolean actDeliver,Boolean actLogComment,Boolean actMovieComment,Boolean actPhotoLasso,Boolean actShareComment,DateTime lastPostTime,String lastPostIP)
     {
         this._UserID = userID;
         this._PrivSpace = privSpace;
         this._PrivFavourite = privFavourite;
         this._PrivEducate = privEducate;
         this._PrivLasso = privLasso;
         this._PrivFriends = privFriends;
         this._PrivLeaveWord = privLeaveWord;
         this._PrivMiniBlog = privMiniBlog;
         this._PrivShare = privShare;
         this._PrivGroup = privGroup;
         this._PrivMovies = privMovies;
         this._ActUpdateData = actUpdateData;
         this._ActAddFriend = actAddFriend;
         this._ActLeaveWord = actLeaveWord;
         this._ActPicComment = actPicComment;
         this._ActSecede = actSecede;
         this._ActDeliver = actDeliver;
         this._ActLogComment = actLogComment;
         this._ActMovieComment = actMovieComment;
         this._ActPhotoLasso = actPhotoLasso;
         this._ActShareComment = actShareComment;
         this._LastPostTime = lastPostTime;
         this._LastPostIP = lastPostIP;
     }


     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 PrivSpace
     {
         get{return this._PrivSpace;}
         set{this._PrivSpace = value;}
     }

     public Int32 PrivFavourite
     {
         get{return this._PrivFavourite;}
         set{this._PrivFavourite = value;}
     }

     public Int32 PrivEducate
     {
         get{return this._PrivEducate;}
         set{this._PrivEducate = value;}
     }

     public Int32 PrivLasso
     {
         get{return this._PrivLasso;}
         set{this._PrivLasso = value;}
     }

     public Int32 PrivFriends
     {
         get{return this._PrivFriends;}
         set{this._PrivFriends = value;}
     }

     public Int32 PrivLeaveWord
     {
         get{return this._PrivLeaveWord;}
         set{this._PrivLeaveWord = value;}
     }

     public Int32 PrivMiniBlog
     {
         get{return this._PrivMiniBlog;}
         set{this._PrivMiniBlog = value;}
     }

     public Int32 PrivShare
     {
         get{return this._PrivShare;}
         set{this._PrivShare = value;}
     }

     public Int32 PrivGroup
     {
         get{return this._PrivGroup;}
         set{this._PrivGroup = value;}
     }

     public Int32 PrivMovies
     {
         get{return this._PrivMovies;}
         set{this._PrivMovies = value;}
     }

     public Boolean ActUpdateData
     {
         get{return this._ActUpdateData;}
         set{this._ActUpdateData = value;}
     }

     public Boolean ActAddFriend
     {
         get{return this._ActAddFriend;}
         set{this._ActAddFriend = value;}
     }

     public Boolean ActLeaveWord
     {
         get{return this._ActLeaveWord;}
         set{this._ActLeaveWord = value;}
     }

     public Boolean ActPicComment
     {
         get{return this._ActPicComment;}
         set{this._ActPicComment = value;}
     }

     public Boolean ActSecede
     {
         get{return this._ActSecede;}
         set{this._ActSecede = value;}
     }

     public Boolean ActDeliver
     {
         get{return this._ActDeliver;}
         set{this._ActDeliver = value;}
     }

     public Boolean ActLogComment
     {
         get{return this._ActLogComment;}
         set{this._ActLogComment = value;}
     }

     public Boolean ActMovieComment
     {
         get{return this._ActMovieComment;}
         set{this._ActMovieComment = value;}
     }

     public Boolean ActPhotoLasso
     {
         get{return this._ActPhotoLasso;}
         set{this._ActPhotoLasso = value;}
     }

     public Boolean ActShareComment
     {
         get{return this._ActShareComment;}
         set{this._ActShareComment = value;}
     }

     public DateTime LastPostTime
     {
         get{return this._LastPostTime;}
         set{this._LastPostTime = value;}
     }

     public String LastPostIP
     {
         get{return this._LastPostIP;}
         set{this._LastPostIP = value;}
     }
  }
}