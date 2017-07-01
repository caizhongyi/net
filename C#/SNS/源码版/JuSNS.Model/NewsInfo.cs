using System;

namespace JuSNS.Model
{

  public class NewsInfo
  {
     private Int32 _id;
     private String _title;
     private String _Color;
     private Byte _Bold;
     private Byte _Italic;
     private String _Content;
     private Int32 _UserID;
     private Int32 _ClassID;
     private DateTime _PostTime;
     private String _PostIP;
     private Byte _IsLock;
     private Byte _IsRec;
     private Int32 _Click;
     private Byte _IsSys;
     private Int32 _ShareNumber;
     private String _Pic;
     private String _Files;
     private Int32 _AttNumber;
     private Int32 _Comments;
     private String _Keywords;
     private String _Source;
     private Int32 _Point;
     private Int32 _GPoint;
     private String _SpecialList;
     private String _TrueName;
     private String _ChannelName;

     public NewsInfo()
     {}

     public NewsInfo(Int32 id, String title, String color, Byte bold, Byte italic, String content, Int32 userID, Int32 classID, DateTime postTime, String postIP, Byte isLock, Byte isRec, Int32 click, Byte isSys, Int32 shareNumber, String pic, String files, Int32 attNumber, Int32 comments, String keywords, String source, Int32 point, Int32 gPoint, String specialList, String trueName, String channelName)
     {
         this._id = id;
         this._title = title;
         this._Color = color;
         this._Bold = bold;
         this._Italic = italic;
         this._Content = content;
         this._UserID = userID;
         this._ClassID = classID;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._IsLock = isLock;
         this._IsRec = isRec;
         this._Click = click;
         this._IsSys = isSys;
         this._ShareNumber = shareNumber;
         this._Pic = pic;
         this._Files = files;
         this._AttNumber = attNumber;
         this._Comments = comments;
         this._Keywords = keywords;
         this._Source = source;
         this._Point = point;
         this._GPoint = gPoint;
         this._SpecialList = specialList;
         this._TrueName = trueName;
         this._ChannelName = channelName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String Title
     {
         get { return this._title; }
         set { this._title = value; }
     }

     public String Color
     {
         get { return this._Color; }
         set { this._Color = value; }
     }

     public Byte Bold
     {
         get { return this._Bold; }
         set { this._Bold = value; }
     }

     public Byte Italic
     {
         get { return this._Italic; }
         set { this._Italic = value; }
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
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

     public Byte IsRec
     {
         get{return this._IsRec;}
         set{this._IsRec = value;}
     }

     public Int32 Click
     {
         get{return this._Click;}
         set{this._Click = value;}
     }

     public Byte IsSys
     {
         get{return this._IsSys;}
         set{this._IsSys = value;}
     }

     public Int32 ShareNumber
     {
         get{return this._ShareNumber;}
         set{this._ShareNumber = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public String Files
     {
         get{return this._Files;}
         set{this._Files = value;}
     }

     public Int32 AttNumber
     {
         get{return this._AttNumber;}
         set{this._AttNumber = value;}
     }

     public Int32 Comments
     {
         get{return this._Comments;}
         set{this._Comments = value;}
     }

     public String Keywords
     {
         get{return this._Keywords;}
         set{this._Keywords = value;}
     }

     public String Source
     {
         get{return this._Source;}
         set{this._Source = value;}
     }

     public Int32 Point
     {
         get{return this._Point;}
         set{this._Point = value;}
     }

     public Int32 GPoint
     {
         get{return this._GPoint;}
         set{this._GPoint = value;}
     }

     public String SpecialList
     {
         get { return this._SpecialList; }
         set { this._SpecialList = value; }
     }
     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }
     public String ChannelName
     {
         get { return this._ChannelName; }
         set { this._ChannelName = value; }
     }
  }
}