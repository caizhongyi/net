/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: GroupInviteInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class GroupInviteInfo
  {
     private Int32 _ID;
     private Int32 _GroupID;
     private Int32 _UserID;
     private Int32 _ReviceID;
     private String _Content;
     private DateTime _PostTime;
     private Boolean _IsAccept;
     private DateTime _AccTime;
     private String _GroupName;
     private String _TrueName;

     public GroupInviteInfo()
     {}

     public GroupInviteInfo(Int32 iD,Int32 groupID,Int32 userID,Int32 reviceID,String content,DateTime postTime,Boolean isAccept,DateTime accTime,String trueName,String groupName)
     {
         this._ID = iD;
         this._GroupID = groupID;
         this._UserID = userID;
         this._ReviceID = reviceID;
         this._Content = content;
         this._PostTime = postTime;
         this._IsAccept = isAccept;
         this._GroupName = groupName;
         this._TrueName = trueName;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public Int32 GroupID
     {
         get{return this._GroupID;}
         set{this._GroupID = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Int32 ReviceID
     {
         get{return this._ReviceID;}
         set{this._ReviceID = value;}
     }

     public String Content
     {
         get { return this._Content; }
         set { this._Content = value; }
     }
     public String GroupName
     {
         get { return this._GroupName; }
         set { this._GroupName = value; }
     }
     public String TrueName
     {
         get { return this._TrueName; }
         set { this._TrueName = value; }
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Boolean IsAccept
     {
         get{return this._IsAccept;}
         set{this._IsAccept = value;}
     }

     public DateTime AccTime
     {
         get{return this._AccTime;}
         set{this._AccTime = value;}
     }
  }
}