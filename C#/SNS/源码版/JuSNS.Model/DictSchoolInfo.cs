using System;

namespace JuSNS.Model
{

  public class DictSchoolInfo
  {
     private Int32 _SchoolID;
     private Int32 _DegreeID;
     private Int32 _AreaID;
     private String _SchoolName;
     private Boolean _IsLock;

     public DictSchoolInfo()
     {}

     public DictSchoolInfo(Int32 schoolID,Int32 degreeID,Int32 areaID,String schoolName,Boolean isLock)
     {
         this._SchoolID = schoolID;
         this._DegreeID = degreeID;
         this._AreaID = areaID;
         this._SchoolName = schoolName;
         this._IsLock = isLock;
     }


     public Int32 SchoolID
     {
         get{return this._SchoolID;}
         set{this._SchoolID = value;}
     }

     public Int32 DegreeID
     {
         get{return this._DegreeID;}
         set{this._DegreeID = value;}
     }

     public Int32 AreaID
     {
         get{return this._AreaID;}
         set{this._AreaID = value;}
     }

     public String SchoolName
     {
         get{return this._SchoolName;}
         set{this._SchoolName = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}