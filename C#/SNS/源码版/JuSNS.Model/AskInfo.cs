/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AskInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AskInfo
  {
     private Int32 _id;
     private Int32 _ClassID;
     private Int32 _ParentID;
     private String _Title;
     private String _ClassName;
     private String _Content;
     private DateTime _PostTime;
     private Int32 _jiFen;
     private Int32 _UserID;
     private Byte _isLock;
     private String _Tag;
     private Int32 _click;
     private String _Pic;
     private String _TrueName;
     private Byte _isClose;
     private Byte _isJinji;
     private Byte _isBest;

     public AskInfo()
     {}

     public AskInfo(Int32 id, Int32 classID, Int32 parentID, String title, String content, DateTime postTime, Int32 jiFen, Int32 userID, Byte isLock, String tag, Int32 click, String pic, Byte isClose, Byte isJinji, Byte isBest, String className, String trueName)
     {
         this._id = id;
         this._ClassID = classID;
         this._ParentID = parentID;
         this._Title = title;
         this._Content = content;
         this._PostTime = postTime;
         this._jiFen = jiFen;
         this._UserID = userID;
         this._isLock = isLock;
         this._Tag = tag;
         this._click = click;
         this._Pic = pic;
         this._isClose = isClose;
         this._isJinji = isJinji;
         this._isBest = isBest;
         this._ClassName = className;
         this._TrueName = trueName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
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

     public String ClassName
     {
         get { return this._ClassName; }
         set { this._ClassName = value; }
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

     public Int32 JiFen
     {
         get{return this._jiFen;}
         set{this._jiFen = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Byte IsLock
     {
         get{return this._isLock;}
         set{this._isLock = value;}
     }

     public String Tag
     {
         get{return this._Tag;}
         set{this._Tag = value;}
     }

     public Int32 Click
     {
         get{return this._click;}
         set{this._click = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Byte IsClose
     {
         get{return this._isClose;}
         set{this._isClose = value;}
     }

     public Byte IsJinji
     {
         get{return this._isJinji;}
         set{this._isJinji = value;}
     }

     public Byte IsBest
     {
         get{return this._isBest;}
         set{this._isBest = value;}
     }
  }
}