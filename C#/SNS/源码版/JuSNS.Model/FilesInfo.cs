/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: FilesInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class FilesInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private String _title;
     private Int32 _GroupID;
     private String _FileName;
     private Int32 _FileSize;
     private String _PostIP;
     private DateTime _PostTime;
     private Int32 _DownNumber;

     public FilesInfo()
     {}

     public FilesInfo(Int32 id,Int32 userID,String title,Int32 groupID,String fileName,Int32 fileSize,String postIP,DateTime postTime,Int32 downNumber)
     {
         this._id = id;
         this._UserID = userID;
         this._title = title;
         this._GroupID = groupID;
         this._FileName = fileName;
         this._FileSize = fileSize;
         this._PostIP = postIP;
         this._PostTime = postTime;
         this._DownNumber = downNumber;
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
         get{return this._title;}
         set{this._title = value;}
     }

     public Int32 GroupID
     {
         get{return this._GroupID;}
         set{this._GroupID = value;}
     }

     public String FileName
     {
         get{return this._FileName;}
         set{this._FileName = value;}
     }

     public Int32 FileSize
     {
         get{return this._FileSize;}
         set{this._FileSize = value;}
     }

     public String PostIP
     {
         get{return this._PostIP;}
         set{this._PostIP = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Int32 DownNumber
     {
         get{return this._DownNumber;}
         set{this._DownNumber = value;}
     }
  }
}