using System;

namespace JuSNS.Model
{

  public class DictAreaInfo
  {
     private Int32 _ID;
     private Int32 _ParentID;
     private String _Name;
     private Boolean _IsLock;

     public DictAreaInfo()
     {}

     public DictAreaInfo(Int32 iD,Int32 parentID,String name,Boolean isLock)
     {
         this._ID = iD;
         this._ParentID = parentID;
         this._Name = name;
         this._IsLock = isLock;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
     }

     public String Name
     {
         get{return this._Name;}
         set{this._Name = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}