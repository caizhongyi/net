/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: OnlineUserInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class OnlineUserInfo
  {
     private Int32 _UserID;
     private String _LastIP;
     private String _LastUrl;
     private DateTime _LastTime;
     private String _UserName;

     public OnlineUserInfo()
     {}

     public OnlineUserInfo(Int32 userID,String lastIP,String lastUrl,DateTime lastTime,String userName)
     {
         this._UserID = userID;
         this._LastIP = lastIP;
         this._LastUrl = lastUrl;
         this._LastTime = lastTime;
         this._UserName = userName;
     }


     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String LastIP
     {
         get{return this._LastIP;}
         set{this._LastIP = value;}
     }

     public String LastUrl
     {
         get{return this._LastUrl;}
         set{this._LastUrl = value;}
     }

     public DateTime LastTime
     {
         get{return this._LastTime;}
         set{this._LastTime = value;}
     }

     public String UserName
     {
         get{return this._UserName;}
         set{this._UserName = value;}
     }
  }
}