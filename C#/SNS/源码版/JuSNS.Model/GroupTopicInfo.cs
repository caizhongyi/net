using System;

namespace JuSNS.Model
{

  public class GroupTopicInfo
  {
     private Int32 _id;
     private Int32 _groupid;
     private Int32 _UserID;
     private String _title;
     private String _content;
     private String _TrueName;
     private String _PostIP;
     private DateTime _posttime;
     private Int32 _TopicID;
     private Boolean _isTop;
     private DateTime _lastpostTime;
     private Int32 _Replynumber;
     private Int32 _Clicks;
     private Byte _IsLock;
     private Byte _IsBest;

     public GroupTopicInfo()
     {}

     public GroupTopicInfo(Int32 id,Int32 groupid,Int32 userID,String title,String content,DateTime posttime,Int32 topicID,Boolean isTop,DateTime lastpostTime,Int32 replynumber,Int32 clicks,String trueName,Byte isLock,Byte isBest,String postIP)
     {
         this._id = id;
         this._groupid = groupid;
         this._UserID = userID;
         this._title = title;
         this._content = content;
         this._posttime = posttime;
         this._TopicID = topicID;
         this._isTop = isTop;
         this._lastpostTime = lastpostTime;
         this._Replynumber = replynumber;
         this._Clicks = clicks;
         this._TrueName = trueName;
         this._IsLock = isLock;
         this._IsBest = isBest;
         this._PostIP = postIP;
     }


     public Int32 Id
     {
         get { return this._id; }
         set { this._id = value; }
     }

     public Byte IsLock
     {
         get { return this._IsLock; }
         set { this._IsLock = value; }
     }

     public Byte IsBest
     {
         get { return this._IsBest; }
         set { this._IsBest = value; }
     }

     public Int32 Groupid
     {
         get{return this._groupid;}
         set{this._groupid = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String Title
     {
         get { return this._title; }
         set { this._title = value; }
     }
     public String PostIP
     {
         get { return this._PostIP; }
         set { this._PostIP = value; }
     }
     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public String Content
     {
         get{return this._content;}
         set{this._content = value;}
     }

     public DateTime Posttime
     {
         get{return this._posttime;}
         set{this._posttime = value;}
     }

     public Int32 TopicID
     {
         get{return this._TopicID;}
         set{this._TopicID = value;}
     }

     public Boolean IsTop
     {
         get{return this._isTop;}
         set{this._isTop = value;}
     }

     public DateTime LastpostTime
     {
         get{return this._lastpostTime;}
         set{this._lastpostTime = value;}
     }

     public Int32 Replynumber
     {
         get{return this._Replynumber;}
         set{this._Replynumber = value;}
     }

     public Int32 Clicks
     {
         get{return this._Clicks;}
         set{this._Clicks = value;}
     }
  }
}