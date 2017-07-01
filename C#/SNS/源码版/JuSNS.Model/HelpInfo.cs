/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: HelpInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class HelpInfo
  {
     private Int32 _ID;
     private String _HelpID;
     private String _Title;
     private String _Content;

     public HelpInfo()
     {}

     public HelpInfo(Int32 iD,String helpID,String title,String content)
     {
         this._ID = iD;
         this._HelpID = helpID;
         this._Title = title;
         this._Content = content;
     }


     public Int32 ID
     {
         get{return this._ID;}
         set{this._ID = value;}
     }

     public String HelpID
     {
         get{return this._HelpID;}
         set{this._HelpID = value;}
     }

     public String Title
     {
         get{return this._Title;}
         set{this._Title = value;}
     }

     public String Content
     {
         get{return this._Content;}
         set{this._Content = value;}
     }
  }
}