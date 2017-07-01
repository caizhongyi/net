/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AdsInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AdsInfo
  {
     private Int32 _id;
     private String _Title;
     private String _Content;
     private String _Pic;
     private String _URL;
     private Int32 _Click;
     private Boolean _IsLock;
     private String _positionType;
     private DateTime _EndTime;

     public AdsInfo()
     {}

     public AdsInfo(Int32 id,String title,String content,String pic,String uRL,Int32 click,Boolean isLock,String positionType,DateTime endTime)
     {
         this._id = id;
         this._Title = title;
         this._Content = content;
         this._Pic = pic;
         this._URL = uRL;
         this._Click = click;
         this._IsLock = isLock;
         this._positionType = positionType;
         this._EndTime = endTime;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
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

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public String URL
     {
         get{return this._URL;}
         set{this._URL = value;}
     }

     public Int32 Click
     {
         get{return this._Click;}
         set{this._Click = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public String PositionType
     {
         get{return this._positionType;}
         set{this._positionType = value;}
     }

     public DateTime EndTime
     {
         get{return this._EndTime;}
         set{this._EndTime = value;}
     }
  }
}