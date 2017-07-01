/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: LinksInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class LinksInfo
  {
     private Int32 _id;
     private String _LinkName;
     private String _URL;
     private Byte _LinkType;
     private String _Pic;
     private Boolean _Islock;

     public LinksInfo()
     {}

     public LinksInfo(Int32 id,String linkName,String uRL,Byte linkType,String pic,Boolean islock)
     {
         this._id = id;
         this._LinkName = linkName;
         this._URL = uRL;
         this._LinkType = linkType;
         this._Pic = pic;
         this._Islock = islock;
     }


     public Int32 Id
     {
         get{return this._id;}
         set{this._id = value;}
     }

     public String LinkName
     {
         get{return this._LinkName;}
         set{this._LinkName = value;}
     }

     public String URL
     {
         get{return this._URL;}
         set{this._URL = value;}
     }

     public Byte LinkType
     {
         get{return this._LinkType;}
         set{this._LinkType = value;}
     }

     public String Pic
     {
         get{return this._Pic;}
         set{this._Pic = value;}
     }

     public Boolean Islock
     {
         get{return this._Islock;}
         set{this._Islock = value;}
     }
  }
}