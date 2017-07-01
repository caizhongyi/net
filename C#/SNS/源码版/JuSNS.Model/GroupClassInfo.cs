using System;

namespace JuSNS.Model
{

  public class GroupClassInfo
  {
     private Int32 _ID;
     private String _className;
     private Int32 _parentid;
     private Boolean _isCreat;

     public GroupClassInfo()
     {}

     public GroupClassInfo(Int32 iD,String className,Int32 parentid,Boolean isCreat)
     {
         this._ID = iD;
         this._className = className;
         this._parentid = parentid;
         this._isCreat = isCreat;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public String ClassName
     {
         get{return this._className;}
         set{this._className = value;}
     }

     public Int32 Parentid
     {
         get{return this._parentid;}
         set{this._parentid = value;}
     }

     public Boolean IsCreat
     {
         get{return this._isCreat;}
         set{this._isCreat = value;}
     }
  }
}