/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: VoteToInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class VoteToInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private Int32 _VoteID;
     private String _OptionID;
     private String _Content;
     private DateTime _PostTime;

     public VoteToInfo()
     {}

     public VoteToInfo(Int32 iD,Int32 userID,Int32 voteID,String optionID,String content,DateTime postTime)
     {
         this._ID = iD;
         this._UserID = userID;
         this._VoteID = voteID;
         this._OptionID = optionID;
         this._Content = content;
         this._PostTime = postTime;
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

     public Int32 VoteID
     {
         get{return this._VoteID;}
         set{this._VoteID = value;}
     }

     public String OptionID
     {
         get{return this._OptionID;}
         set{this._OptionID = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }
  }
}