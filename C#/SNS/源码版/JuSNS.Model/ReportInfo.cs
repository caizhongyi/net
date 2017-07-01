/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ReportInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ReportInfo
  {
     private Int32 _id;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private String _Urls;
     private Int32 _UserID;

     public ReportInfo()
     {}

     public ReportInfo(Int32 id,String content,DateTime postTime,String postIP,String urls,Int32 userID)
     {
         this._id = id;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._Urls = urls;
         this._UserID = userID;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
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

     public String Urls
     {
         get{return this._Urls;}
         set{this._Urls = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }
  }
}