using System;

namespace JuSNS.Model
{

  public class DictAcademyInfo
  {
     private Int32 _ID;
     private String _Name;
     private Int32 _SchoolID;
     private Boolean _IsLock;

     public DictAcademyInfo()
     {}

     public DictAcademyInfo(Int32 iD,String name,Int32 schoolID,Boolean isLock)
     {
         this._ID = iD;
         this._Name = name;
         this._SchoolID = schoolID;
         this._IsLock = isLock;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public String Name
     {
         get{return this._Name;}
         set{this._Name = value;}
     }

     public Int32 SchoolID
     {
         get{return this._SchoolID;}
         set{this._SchoolID = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}