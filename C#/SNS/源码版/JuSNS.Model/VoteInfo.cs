/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: VoteInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class VoteInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private String _Title;
     private String _TrueName;
     private String _Content;
     private DateTime _PostTime;
     private Byte _Mode;
     private DateTime _EndTime;
     private Int32 _JCnt;
     private Int32 _VCnt;
     private Byte _IsFriend;

     public VoteInfo()
     {}

     public VoteInfo(Int32 id, Int32 userID, String title, String content, String trueName, DateTime postTime, Byte mode, DateTime endTime, Int32 jCnt, Int32 vCnt, Byte isFriend)
     {
         this._id = id;
         this._UserID = userID;
         this._Title = title;
         this._Content = content;
         this._PostTime = postTime;
         this._Mode = mode;
         this._EndTime = endTime;
         this._JCnt = jCnt;
         this._VCnt = vCnt;
         this._IsFriend = isFriend;
         this._TrueName = trueName;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public String Title
     {
         get{return this._Title;}
         set{this._Title = value;}
     }

     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
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

     public Byte Mode
     {
         get{return this._Mode;}
         set{this._Mode = value;}
     }

     public DateTime EndTime
     {
         get{return this._EndTime;}
         set{this._EndTime = value;}
     }

     public Int32 JCnt
     {
         get{return this._JCnt;}
         set{this._JCnt = value;}
     }

     public Int32 VCnt
     {
         get{return this._VCnt;}
         set{this._VCnt = value;}
     }

     public Byte IsFriend
     {
         get{return this._IsFriend;}
         set{this._IsFriend = value;}
     }
  }
}