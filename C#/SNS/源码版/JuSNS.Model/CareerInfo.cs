using System;

namespace JuSNS.Model
{

  public class CareerInfo
  {
     private Int32 _ID;
     private Int32 _UserID;
     private String _Company;
     private String _JoinTime;
     private DateTime _PostTime;
     private String _LeaveTime;

     public CareerInfo()
     {}

     public CareerInfo(Int32 iD,Int32 userID,String company,String joinTime,DateTime postTime,String leaveTime)
     {
         this._ID = iD;
         this._UserID = userID;
         this._Company = company;
         this._JoinTime = joinTime;
         this._PostTime = postTime;
         this._LeaveTime = leaveTime;
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

     public String Company
     {
         get{return this._Company;}
         set{this._Company = value;}
     }

     public String JoinTime
     {
         get{return this._JoinTime;}
         set{this._JoinTime = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public String LeaveTime
     {
         get{return this._LeaveTime;}
         set{this._LeaveTime = value;}
     }
  }
}