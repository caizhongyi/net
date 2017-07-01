/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AppDeveloperInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AppDeveloperInfo
  {
     private Int32 _userid;
     private String _username;
     private String _userkey;
     private DateTime _JoinTime;
     private String _tel;
     private String _mobile;
     private String _email;
     private Byte _IsLock;

     public AppDeveloperInfo()
     {}

     public AppDeveloperInfo(Int32 userid,String username,String userkey,DateTime joinTime,String tel,String mobile,String email,Byte isLock)
     {
         this._userid = userid;
         this._username = username;
         this._userkey = userkey;
         this._JoinTime = joinTime;
         this._tel = tel;
         this._mobile = mobile;
         this._email = email;
         this._IsLock = isLock;
     }


     public Int32 Userid
     {
         get{return this._userid;}
         set{this._userid = value;}
     }

     public String Username
     {
         get{return this._username;}
         set{this._username = value;}
     }

     public String Userkey
     {
         get{return this._userkey;}
         set{this._userkey = value;}
     }

     public DateTime JoinTime
     {
         get{return this._JoinTime;}
         set{this._JoinTime = value;}
     }

     public String Tel
     {
         get{return this._tel;}
         set{this._tel = value;}
     }

     public String Mobile
     {
         get{return this._mobile;}
         set{this._mobile = value;}
     }

     public String Email
     {
         get{return this._email;}
         set{this._email = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}