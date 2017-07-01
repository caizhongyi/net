using System;

namespace JuSNS.Model
{

  public class EducationInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private String _schoolName;
     private String _leaveyear;
     private DateTime _PostTime;
     private Byte _levels;

     public EducationInfo()
     {}

     public EducationInfo(Int32 iD,Int32 userID,String schoolName,String leaveyear,DateTime postTime,Byte levels)
     {
         this._ID = iD;
         this._UserID = userID;
         this._schoolName = schoolName;
         this._leaveyear = leaveyear;
         this._PostTime = postTime;
         this._levels = levels;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String SchoolName
     {
         get{return this._schoolName;}
         set{this._schoolName = value;}
     }

     public String Leaveyear
     {
         get{return this._leaveyear;}
         set{this._leaveyear = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Byte Levels
     {
         get{return this._levels;}
         set{this._levels = value;}
     }
  }
}