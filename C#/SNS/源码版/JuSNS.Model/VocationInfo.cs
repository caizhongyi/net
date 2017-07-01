using System;

namespace JuSNS.Model
{

  public class VocationInfo
  {
     private Int32 _ID;
     private String _VocName;
     private Boolean _IsLock;

     public VocationInfo()
     {}

     public VocationInfo(Int32 iD,String vocName,Boolean isLock)
     {
         this._ID = iD;
         this._VocName = vocName;
         this._IsLock = isLock;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public String VocName
     {
         get{return this._VocName;}
         set{this._VocName = value;}
     }

     public Boolean IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }
  }
}