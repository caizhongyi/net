/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AtiveInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AtiveInfo
  {
     private Int32 _Id;
     private String _AtiveName;
     private Int32 _AreaID;
     private Int32 _AreaID1;
     private Int32 _ClassID;
     private DateTime _StartTime;
     private DateTime _EndTime;
     private Int32 _UserID;
     private Int32 _Members;
     private Byte _IsLock;
     private String _Money;
     private String _TrueName;
     private String _ClassName;
     private DateTime _BaoMingTime;
     private Int32 _PersionNumber;
     private Int32 _Clicks;
     private Int32 _ATT;
     private String _AddRess;
     private Int32 _GroupID;
     private String _Content;
     private DateTime _PostTime;
     private String _PostIP;
     private String _Links;
     private String _Photo;
     private Byte _IsChecks;
     private Byte _IsRec;
     private String _Note;

     public AtiveInfo()
     {}

     public AtiveInfo(Int32 id, String ativeName, String trueName, String className, Int32 areaID, Int32 areaID1, Int32 classID, DateTime startTime, DateTime endTime, Int32 userID, Int32 members, Byte isLock, String money, DateTime baoMingTime, Int32 persionNumber, Int32 clicks, Int32 aTT, String addRess, Int32 groupID, String content, DateTime postTime, String postIP, String links, String photo, Byte isChecks, Byte isRec, String note)
     {
         this._Id = id;
         this._AtiveName = ativeName;
         this._TrueName = trueName;
         this._ClassName = className;
         this._AreaID = areaID;
         this._AreaID1 = areaID1;
         this._ClassID = classID;
         this._StartTime = startTime;
         this._EndTime = endTime;
         this._UserID = userID;
         this._Members = members;
         this._IsLock = isLock;
         this._Money = money;
         this._BaoMingTime = baoMingTime;
         this._PersionNumber = persionNumber;
         this._Clicks = clicks;
         this._ATT = aTT;
         this._AddRess = addRess;
         this._GroupID = groupID;
         this._Content = content;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._Links = links;
         this._Photo = photo;
         this._IsChecks = isChecks;
         this._IsRec = isRec;
         this._Note = note;
     }


     public Int32 Id
     {
         get{return this._Id;}
         set{this._Id = value;}
     }

     public String AtiveName
     {
         get{return this._AtiveName;}
         set{this._AtiveName = value;}
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


     public Int32 AreaID
     {
         get{return this._AreaID;}
         set{this._AreaID = value;}
     }

     public Int32 AreaID1
     {
         get{return this._AreaID1;}
         set{this._AreaID1 = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
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

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 Members
     {
         get{return this._Members;}
         set{this._Members = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public String Money
     {
         get{return this._Money;}
         set{this._Money = value;}
     }

     public DateTime BaoMingTime
     {
         get{return this._BaoMingTime;}
         set{this._BaoMingTime = value;}
     }

     public Int32 PersionNumber
     {
         get{return this._PersionNumber;}
         set{this._PersionNumber = value;}
     }

     public Int32 Clicks
     {
         get{return this._Clicks;}
         set{this._Clicks = value;}
     }

     public Int32 ATT
     {
         get{return this._ATT;}
         set{this._ATT = value;}
     }

     public String AddRess
     {
         get{return this._AddRess;}
         set{this._AddRess = value;}
     }

     public Int32 GroupID
     {
         get{return this._GroupID;}
         set{this._GroupID = value;}
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

     public String Links
     {
         get{return this._Links;}
         set{this._Links = value;}
     }

     public String Photo
     {
         get{return this._Photo;}
         set{this._Photo = value;}
     }

     public Byte IsChecks
     {
         get{return this._IsChecks;}
         set{this._IsChecks = value;}
     }

     public Byte IsRec
     {
         get{return this._IsRec;}
         set{this._IsRec = value;}
     }

     public String Note
     {
         get{return this._Note;}
         set{this._Note = value;}
     }
  }
}