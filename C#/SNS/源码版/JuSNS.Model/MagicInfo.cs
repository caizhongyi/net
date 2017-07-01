using System;

namespace JuSNS.Model
{

  public class MagicInfo
  {
     private Int32 _id;
     private String _mName;
     private String _pic;
     private Int32 _point;
     private Int32 _gpoint;
     private Int32 _number;
     private Int32 _buynumber;
     private String _mdesc;
     private Int32 _mType;
     private DateTime _CreatTime;
     private Byte _state;
     private Int32 _vTime;

     public MagicInfo()
     {}

     public MagicInfo(Int32 id,String mName,String pic,Int32 point,Int32 gpoint,Int32 number,Int32 buynumber,String mdesc,Int32 mType,DateTime creatTime,Byte state,Int32 vTime)
     {
         this._id = id;
         this._mName = mName;
         this._pic = pic;
         this._point = point;
         this._gpoint = gpoint;
         this._number = number;
         this._buynumber = buynumber;
         this._mdesc = mdesc;
         this._mType = mType;
         this._CreatTime = creatTime;
         this._state = state;
         this._vTime = vTime;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String MName
     {
         get{return this._mName;}
         set{this._mName = value;}
     }

     public String Pic
     {
         get{return this._pic;}
         set{this._pic = value;}
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

     public Int32 Number
     {
         get{return this._number;}
         set{this._number = value;}
     }

     public Int32 Buynumber
     {
         get{return this._buynumber;}
         set{this._buynumber = value;}
     }

     public String Mdesc
     {
         get{return this._mdesc;}
         set{this._mdesc = value;}
     }

     public Int32 MType
     {
         get{return this._mType;}
         set{this._mType = value;}
     }

     public DateTime CreatTime
     {
         get{return this._CreatTime;}
         set{this._CreatTime = value;}
     }

     public Byte State
     {
         get{return this._state;}
         set{this._state = value;}
     }

     public Int32 VTime
     {
         get{return this._vTime;}
         set{this._vTime = value;}
     }
  }
}