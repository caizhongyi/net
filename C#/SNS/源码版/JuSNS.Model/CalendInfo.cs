/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: CalendInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class CalendInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private String _Title;
     private String _Content;
     private DateTime _PostTime;
     private Int32 _NoteNumber;
     private DateTime _StartTime;
     private DateTime _EndTime;

     public CalendInfo()
     {}

     public CalendInfo(Int32 id,Int32 userID,String title,String content,DateTime postTime,Int32 noteNumber,DateTime startTime,DateTime endTime)
     {
         this._id = id;
         this._UserID = userID;
         this._Title = title;
         this._Content = content;
         this._PostTime = postTime;
         this._NoteNumber = noteNumber;
         this._StartTime = startTime;
         this._EndTime = endTime;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
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

     public Int32 NoteNumber
     {
         get{return this._NoteNumber;}
         set{this._NoteNumber = value;}
     }

     public DateTime StartTime
     {
         get{return this._StartTime;}
         set{this._StartTime = value;}
     }

     public DateTime EndTime
     {
         get{return this._EndTime;}
         set{this._EndTime = value;}
     }
  }
}