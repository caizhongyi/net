using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.Web.Controls
{
    public class AjaxParams
    {
       private bool _ansy=true ;
       private string _mathod="Get" ;
       private string _url = string.Empty; 
       private string _callBack=string .Empty ;
       private string _params = string.Empty;

       public string URL { get { return _url; } set { value = _url; } }
       public string Params { get { return _params; } set { value = _params; } }
       public string Mathod { get {return _mathod;} set{ value =_mathod;} }
       public bool Ansy { get {return _ansy;} set{ value =_ansy;} }
        /// <summary>
        /// function(data){};
        /// </summary>
       public string CallBack { get { return _callBack; } set { value = _callBack; } }
       public AjaxParams()
       {
       }
       public AjaxParams(string url, string method, bool ansy, string param, string callback)
       {
           _ansy = ansy;
           _mathod = method;
           _url = url;
           _callBack = callback;
           _params = param;
       }
       
    }
}
