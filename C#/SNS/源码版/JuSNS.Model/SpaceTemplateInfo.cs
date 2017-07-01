/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: SpaceTemplateInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class SpaceTemplateInfo
  {
     private Int32 _ID;
     private String _TName;
     private String _TEName;
     private DateTime _PostTime;
     private Byte _IsLock;
     private Int32 _IPoint;
     private Int32 _GPoint;
     private Int32 _UseNumber;

     public SpaceTemplateInfo()
     {}

     public SpaceTemplateInfo(Int32 iD,String tName,String tEName,DateTime postTime,Byte isLock,Int32 iPoint,Int32 gPoint,Int32 useNumber)
     {
         this._ID = iD;
         this._TName = tName;
         this._TEName = tEName;
         this._PostTime = postTime;
         this._IsLock = isLock;
         this._IPoint = iPoint;
         this._GPoint = gPoint;
         this._UseNumber = useNumber;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public String TName
     {
         get{return this._TName;}
         set{this._TName = value;}
     }

     public String TEName
     {
         get{return this._TEName;}
         set{this._TEName = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Byte IsLock
     {
         get{return this._IsLock;}
         set{this._IsLock = value;}
     }

     public Int32 IPoint
     {
         get{return this._IPoint;}
         set{this._IPoint = value;}
     }

     public Int32 GPoint
     {
         get{return this._GPoint;}
         set{this._GPoint = value;}
     }

     public Int32 UseNumber
     {
         get{return this._UseNumber;}
         set{this._UseNumber = value;}
     }
  }
}