/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: ATTInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class ATTInfo
  {
     private Int32 _id;
     private Int32 _userid;
     private Int32 _atterid;

     public ATTInfo()
     {}

     public ATTInfo(Int32 id,Int32 userid,Int32 atterid)
     {
         this._id = id;
         this._userid = userid;
         this._atterid = atterid;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 Userid
     {
         get{return this._userid;}
         set{this._userid = value;}
     }

     public Int32 Atterid
     {
         get{return this._atterid;}
         set{this._atterid = value;}
     }
  }
}