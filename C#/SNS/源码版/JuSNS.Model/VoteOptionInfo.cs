/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: VoteOptionInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class VoteOptionInfo
  {
     private Int32 _ID;
     private Int32 _VoteID;
     private String _OptionName;
     private Int32 _Cnt;

     public VoteOptionInfo()
     {}

     public VoteOptionInfo(Int32 iD,Int32 voteID,String optionName,Int32 cnt)
     {
         this._ID = iD;
         this._VoteID = voteID;
         this._OptionName = optionName;
         this._Cnt = cnt;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 VoteID
     {
         get{return this._VoteID;}
         set{this._VoteID = value;}
     }

     public String OptionName
     {
         get{return this._OptionName;}
         set{this._OptionName = value;}
     }

     public Int32 Cnt
     {
         get{return this._Cnt;}
         set{this._Cnt = value;}
     }
  }
}