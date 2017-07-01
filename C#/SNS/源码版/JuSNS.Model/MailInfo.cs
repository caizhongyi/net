using System;

namespace JuSNS.Model
{

  public class MailInfo
  {
     private Int32 _ID;
     private Int32 _Sender;
     private Int32 _Receiver;
     private String _Title;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private Int32 _TopicID;
     private Boolean _IsReply;
     private Int32 _LtType;
     private Int32 _RelativeID;
     private Int32 _IsRead;

     public MailInfo()
     {}

     public MailInfo(Int32 iD,Int32 sender,Int32 receiver,String title,String content,DateTime postTime,String postIP,Int32 topicID,Boolean isReply,Int32 ltType,Int32 relativeID,Int32 isRead)
     {
         this._ID = iD;
         this._Sender = sender;
         this._Receiver = receiver;
         this._Title = title;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._TopicID = topicID;
         this._IsReply = isReply;
         this._LtType = ltType;
         this._RelativeID = relativeID;
         this._IsRead = isRead;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 Sender
     {
         get{return this._Sender;}
         set{this._Sender = value;}
     }

     public Int32 Receiver
     {
         get{return this._Receiver;}
         set{this._Receiver = value;}
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

     public Int32 TopicID
     {
         get{return this._TopicID;}
         set{this._TopicID = value;}
     }

     public Boolean IsReply
     {
         get{return this._IsReply;}
         set{this._IsReply = value;}
     }

     public Int32 LtType
     {
         get{return this._LtType;}
         set{this._LtType = value;}
     }

     public Int32 RelativeID
     {
         get{return this._RelativeID;}
         set{this._RelativeID = value;}
     }

     public Int32 IsRead
     {
         get{return this._IsRead;}
         set{this._IsRead = value;}
     }
  }
}