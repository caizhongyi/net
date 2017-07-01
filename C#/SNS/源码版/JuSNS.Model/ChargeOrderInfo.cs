using System;
namespace JuSNS.Model
{
  public class ChargeOrderInfo
  {
     private Int32 _id;
     private Int32 _Money;
     private Int32 _point;
     private Int32 _gpoint;
     private String _orderNumber;
     private Int32 _UserID;
     private Boolean _IsSucces;
     private DateTime _CreatTime;
     private String _PostIP;

     public ChargeOrderInfo()
     {}

     public ChargeOrderInfo(Int32 id,Int32 money,Int32 point,Int32 gpoint,String orderNumber,Int32 userID,Boolean isSucces,DateTime creatTime,String postIP)
     {
         this._id = id;
         this._Money = money;
         this._point = point;
         this._gpoint = gpoint;
         this._orderNumber = orderNumber;
         this._UserID = userID;
         this._IsSucces = isSucces;
         this._CreatTime = creatTime;
         this._PostIP = postIP;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public Int32 Money
     {
         get{return this._Money;}
         set{this._Money = value;}
     }

     public Int32 Point
     {
         get{return this._point;}
         set{this._point = value;}
     }

     public Int32 Gpoint
     {
         get{return this._gpoint;}
         set{this._gpoint = value;}
     }

     public String OrderNumber
     {
         get{return this._orderNumber;}
         set{this._orderNumber = value;}
     }

     public Int32 UserID
     {
         get{return this._UserID;}
         set{this._UserID = value;}
     }

     public Boolean IsSucces
     {
         get{return this._IsSucces;}
         set{this._IsSucces = value;}
     }

     public DateTime CreatTime
     {
         get{return this._CreatTime;}
         set{this._CreatTime = value;}
     }

     public String PostIP
     {
         get{return this._PostIP;}
         set{this._PostIP = value;}
     }
  }
}