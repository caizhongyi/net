using System;

namespace JuSNS.Model
{

  public class ConstellationInfo
  {
     private Int32 _Id;
     private String _Constellation;

     public ConstellationInfo()
     {}

     public ConstellationInfo(Int32 id,String constellation)
     {
         this._Id = id;
         this._Constellation = constellation;
     }


     public Int32 Id
     {
         get{return this._Id;}
         set{this._Id = value;}
     }

     public String Constellation
     {
         get{return this._Constellation;}
         set{this._Constellation = value;}
     }
  }
}