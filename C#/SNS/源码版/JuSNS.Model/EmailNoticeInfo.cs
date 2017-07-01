/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: EmailNoticeInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class EmailNoticeInfo
  {
     private Int32 _id;
     private Int32 _userid;
     private String _email;
     private String _vcode;
     private DateTime _posttime;
     private Byte _ntype;

     public EmailNoticeInfo()
     {}

     public EmailNoticeInfo(Int32 id,Int32 userid,String email,String vcode,DateTime posttime,Byte ntype)
     {
         this._id = id;
         this._userid = userid;
         this._email = email;
         this._vcode = vcode;
         this._posttime = posttime;
         this._ntype = ntype;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 Userid
     {
         get{return this._userid;}
         set{this._userid = value;}
     }

     public String Email
     {
         get{return this._email;}
         set{this._email = value;}
     }

     public String Vcode
     {
         get{return this._vcode;}
         set{this._vcode = value;}
     }

     public DateTime Posttime
     {
         get{return this._posttime;}
         set{this._posttime = value;}
     }

     public Byte Ntype
     {
         get{return this._ntype;}
         set{this._ntype = value;}
     }
  }
}