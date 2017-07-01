using System;

namespace JuSNS.Model
{

  public class NewsChannelInfo
  {
     private Int32 _id;
     private String _ChannelName;
     private Int32 _ParentID;
     private Int32 _PerPageNumber;
     private String _Pic;
     private Byte _ChannelType;
     private Int32 _OrderID;

     public NewsChannelInfo()
     {}

     public NewsChannelInfo(Int32 id,String channelName,Int32 parentID,Int32 perPageNumber,String pic,Byte channelType,Int32 orderID)
     {
         this._id = id;
         this._ChannelName = channelName;
         this._ParentID = parentID;
         this._PerPageNumber = perPageNumber;
         this._Pic = pic;
         this._ChannelType = channelType;
         this._OrderID = orderID;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String ChannelName
     {
         get{return this._ChannelName;}
         set{this._ChannelName = value;}
     }

     public Int32 ParentID
     {
         get{return this._ParentID;}
         set{this._ParentID = value;}
     }

     public Int32 PerPageNumber
     {
         get{return this._PerPageNumber;}
         set{this._PerPageNumber = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Byte ChannelType
     {
         get{return this._ChannelType;}
         set{this._ChannelType = value;}
     }

     public Int32 OrderID
     {
         get{return this._OrderID;}
         set{this._OrderID = value;}
     }
  }
}