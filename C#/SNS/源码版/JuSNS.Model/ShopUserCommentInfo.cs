/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ShopUserCommentInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ShopUserCommentInfo
  {
     private Int32 _id;
     private Int32 _GoodsID;
     private Byte _Sore;
     private Int32 _UserID;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private Int32 _CommentID;

     public ShopUserCommentInfo()
     {}

     public ShopUserCommentInfo(Int32 id,Int32 goodsID,Byte sore,Int32 userID,String content,DateTime postTime,String postIP,Int32 commentID)
     {
         this._id = id;
         this._GoodsID = goodsID;
         this._Sore = sore;
         this._UserID = userID;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._CommentID = commentID;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 GoodsID
     {
         get{return this._GoodsID;}
         set{this._GoodsID = value;}
     }

     public Byte Sore
     {
         get{return this._Sore;}
         set{this._Sore = value;}
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

     public Int32 CommentID
     {
         get{return this._CommentID;}
         set{this._CommentID = value;}
     }
  }
}