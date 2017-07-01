using System;

namespace JuSNS.Model
{

  public class GroupInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private String _GroupName;
     private Int32 _Members;
     private String _Bulletin;
     private Int32 _CityID;
     private Int32 _Privacy;
     private Int32 _Publics;
     private String _Portrait;
     private Boolean _IsLock;
     private DateTime _PostTime;
     private String _PostIP;
     private Byte _isRec;
     private Int32 _ClassID;
     private String _skinDir;
     private Int32 _Click;
     private Int32 _Count;
     private Boolean _Islight;

     public GroupInfo()
     {}

     public GroupInfo(Int32 id,Int32 userID,String groupName,Int32 members,String bulletin,Int32 cityID,Int32 privacy,Int32 publics,String portrait,Boolean isLock,DateTime postTime,String postIP,Byte isRec,Int32 classID,String skinDir,Int32 click,Boolean islight,Int32 count)
     {
         this._id = id;
         this._UserID = userID;
         this._GroupName = groupName;
         this._Members = members;
         this._Bulletin = bulletin;
         this._CityID = cityID;
         this._Privacy = privacy;
         this._Publics = publics;
         this._Portrait = portrait;
         this._IsLock = isLock;
         this._PostTime = postTime;
         this._PostIP = postIP;
         this._isRec = isRec;
         this._ClassID = classID;
         this._skinDir = skinDir;
         this._Click = click;
         this._Islight = islight;
         this._Count = count;
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

     public String GroupName
     {
         get{return this._GroupName;}
         set{this._GroupName = value;}
     }

     public Int32 Members
     {
         get{return this._Members;}
         set{this._Members = value;}
     }

     public String Bulletin
     {
         get{return this._Bulletin;}
         set{this._Bulletin = value;}
     }

     public Int32 CityID
     {
         get { return this._CityID; }
         set { this._CityID = value; }
     }

     public Int32 Count
     {
         get { return this._Count; }
         set { this._Count = value; }
     }

     public Int32 Privacy
     {
         get{return this._Privacy;}
         set{this._Privacy = value;}
     }

     public Int32 Publics
     {
         get{return this._Publics;}
         set{this._Publics = value;}
     }

     public String Portrait
     {
         get{return this._Portrait;}
         set{this._Portrait = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
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

     public Byte IsRec
     {
         get{return this._isRec;}
         set{this._isRec = value;}
     }

     public Int32 ClassID
     {
         get{return this._ClassID;}
         set{this._ClassID = value;}
     }

     public String SkinDir
     {
         get{return this._skinDir;}
         set{this._skinDir = value;}
     }

     public Int32 Click
     {
         get{return this._Click;}
         set{this._Click = value;}
     }

     public Boolean Islight
     {
         get{return this._Islight;}
         set{this._Islight = value;}
     }
  }
}