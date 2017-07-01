/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: AskClassInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class AskClassInfo
  {
     private Int32 _id;
     private String _ClassName;
     private Int32 _ParentID;

     public AskClassInfo()
     {}

     public AskClassInfo(Int32 id,String className,Int32 parentID)
     {
         this._id = id;
         this._ClassName = className;
         this._ParentID = parentID;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String ClassName
     {
         get{return this._ClassName;}
         set{this._ClassName = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
     }
  }
}