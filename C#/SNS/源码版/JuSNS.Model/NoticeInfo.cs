/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: NoticeInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class NoticeInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private Int32 _ReviceID;
     private String _Content;
     private Boolean _IsRead;
     private DateTime _PostTime;
     private String _PostIP;
     private Byte _MsgType;
     private Int32 _CorrID;

     public NoticeInfo()
     {}

     public NoticeInfo(Int32 iD,Int32 userID,Int32 reviceID,String content,Boolean isRead,DateTime postTime,String postIP,Byte msgType,Int32 corrID)
     {
         this._ID = iD;
         this._UserID = userID;
         this._ReviceID = reviceID;
         this._Content = content;
         this._IsRead = isRead;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._MsgType = msgType;
         this._CorrID = corrID;
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

     public Int32 ReviceID
     {
         get{return this._ReviceID;}
         set{this._ReviceID = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public Boolean IsRead
     {
         get{return this._IsRead;}
         set{this._IsRead = value;}
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

     public Byte MsgType
     {
         get{return this._MsgType;}
         set{this._MsgType = value;}
     }

     public Int32 CorrID
     {
         get{return this._CorrID;}
         set{this._CorrID = value;}
     }
  }
}