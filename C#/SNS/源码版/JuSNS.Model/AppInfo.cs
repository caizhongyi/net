/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AppInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AppInfo
  {
     private Int32 _id;
     private String _appname;
     private String _appkey;
     private Int32 _classid;
     private String _icon;
     private String _pic;
     private Int32 _UserID;
     private DateTime _CreatTime;
     private Byte _IsLock;
     private String _Content;
     private String _url;
     private Int32 _click;
     private Int32 _setupNumber;
     private Byte _targetStyle;
     private Int32 _width;
     private Int32 _height;
     private String _SetupContent;

     public AppInfo()
     {}

     public AppInfo(Int32 id,String appname,String appkey,Int32 classid,String icon,String pic,Int32 userID,DateTime creatTime,Byte isLock,String content,String url,Int32 click,Int32 setupNumber,Byte targetStyle,Int32 width,Int32 height,String setupContent)
     {
         this._id = id;
         this._appname = appname;
         this._appkey = appkey;
         this._classid = classid;
         this._icon = icon;
         this._pic = pic;
         this._UserID = userID;
         this._CreatTime = creatTime;
         this._IsLock = isLock;
         this._Content = content;
         this._url = url;
         this._click = click;
         this._setupNumber = setupNumber;
         this._targetStyle = targetStyle;
         this._width = width;
         this._height = height;
         this._SetupContent = setupContent;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String Appname
     {
         get{return this._appname;}
         set{this._appname = value;}
     }

     public String Appkey
     {
         get{return this._appkey;}
         set{this._appkey = value;}
     }

     public Int32 Classid
     {
         get{return this._classid;}
         set{this._classid = value;}
     }

     public String Icon
     {
         get{return this._icon;}
         set{this._icon = value;}
     }

     public String Pic
     {
         get{return this._pic;}
         set{this._pic = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public DateTime CreatTime
     {
         get{return this._CreatTime;}
         set{this._CreatTime = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public String Url
     {
         get{return this._url;}
         set{this._url = value;}
     }

     public Int32 Click
     {
         get{return this._click;}
         set{this._click = value;}
     }

     public Int32 SetupNumber
     {
         get{return this._setupNumber;}
         set{this._setupNumber = value;}
     }

     public Byte TargetStyle
     {
         get{return this._targetStyle;}
         set{this._targetStyle = value;}
     }

     public Int32 Width
     {
         get{return this._width;}
         set{this._width = value;}
     }

     public Int32 Height
     {
         get{return this._height;}
         set{this._height = value;}
     }

     public String SetupContent
     {
         get{return this._SetupContent;}
         set{this._SetupContent = value;}
     }
  }
}