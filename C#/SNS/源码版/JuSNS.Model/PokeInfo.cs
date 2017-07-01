/******************************************************************************************
 			copyright (c) 2007 -  LeadNT.COM 

  class name: PokeInfo.cs
  Description: This entity class is crate by LeadNT DBTable2EC.

 *****************************************************************************************/

using System;

namespace JuSNS.Model
{

  public class PokeInfo
  {
     private Int32 _id;
     private Int32 _UserID;
     private Int32 _ReviceID;
     private Int32 _PokeKey;
     private String _PokeForm;
     private String _Poketo;
     private DateTime _PostTime;
     private Byte _IsPub;

     public PokeInfo()
     {}

     public PokeInfo(Int32 id,Int32 userID,Int32 reviceID,Int32 pokeKey,String pokeForm,String poketo,DateTime postTime,Byte isPub)
     {
         this._id = id;
         this._UserID = userID;
         this._ReviceID = reviceID;
         this._PokeKey = pokeKey;
         this._PokeForm = pokeForm;
         this._Poketo = poketo;
         this._PostTime = postTime;
         this._IsPub = isPub;
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

     public Int32 ReviceID
     {
         get{return this._ReviceID;}
         set{this._ReviceID = value;}
     }

     public Int32 PokeKey
     {
         get{return this._PokeKey;}
         set{this._PokeKey = value;}
     }

     public String PokeForm
     {
         get{return this._PokeForm;}
         set{this._PokeForm = value;}
     }

     public String Poketo
     {
         get{return this._Poketo;}
         set{this._Poketo = value;}
     }

     public DateTime PostTime
     {
         get{return this._PostTime;}
         set{this._PostTime = value;}
     }

     public Byte IsPub
     {
         get{return this._IsPub;}
         set{this._IsPub = value;}
     }
  }
}