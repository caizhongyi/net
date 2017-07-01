using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class Product_NewsInfo
    {
       private string _p_n_productId;
       public string p_n_productId
       {
            get{return _p_n_productId;}
            set{_p_n_productId = value;}
       }

       private Int64 _p_n_id;
       public Int64 p_n_id
       {
            get{return _p_n_id;}
            set{_p_n_id = value;}
       }

       private Int64 _p_n_newsId;
       public Int64 p_n_newsId
       {
            get{return _p_n_newsId;}
            set{_p_n_newsId = value;}
       }

    }
}
