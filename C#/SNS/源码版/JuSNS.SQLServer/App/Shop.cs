using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.App;
using JuSNS.Profile;
using JuSNS.Model;
using JuSNS.Config;

namespace JuSNS.SQLServer.App
{
    public class Shop : DbBase, IShop
    {
        public ShopGoodsInfo GetGoodsInfo(object gid)
        {
            ShopGoodsInfo info = new ShopGoodsInfo();
            string sql = "select a.*,b.className from nt_shopgoods AS a INNER JOIN NT_ShopClass AS b on b.id=a.ClassID where a.id=" + gid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.AddRess = Convert.ToString(dr["AddRess"]);
                info.AreaID = Convert.ToInt32(dr["AreaID"]);
                info.ClassID = Convert.ToInt32(dr["ClassID"]);
                info.Click = Convert.ToInt32(dr["Click"]);
                info.Content = Convert.ToString(dr["Content"]);
                info.DownNumber = Convert.ToInt32(dr["DownNumber"]);
                info.EndTime = Convert.ToDateTime(dr["EndTime"]);
                info.ExpressContent = Convert.ToString(dr["ExpressContent"]);
                info.ExpressStyle = Convert.ToByte(dr["ExpressStyle"]);
                info.GoodsName = Convert.ToString(dr["GoodsName"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.IsRec = Convert.ToBoolean(dr["IsRec"]);
                info.IsOld = Convert.ToBoolean(dr["IsOld"]);
                info.Keywords = Convert.ToString(dr["Keywords"]);
                info.MPrice = Convert.ToDouble(dr["MPrice"]);
                info.MultePrice = Convert.ToDouble(dr["MultePrice"]);
                info.MulteBuy = Convert.ToByte(dr["MulteBuy"]);
                info.MulteMaxNumber = Convert.ToInt32(dr["MulteMaxNumber"]);
                info.BuyNumber = Convert.ToInt32(dr["BuyNumber"]);
                info.MulteMinNumber = Convert.ToInt32(dr["MulteMinNumber"]);
                info.Number = Convert.ToInt32(dr["Number"]);
                info.Pic = Convert.ToString(dr["Pic"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.Price = Convert.ToDouble(dr["Price"]);
                info.ShopID = Convert.ToInt32(dr["ShopID"]);
                info.StartTime = Convert.ToDateTime(dr["StartTime"]);
                info.Tel = Convert.ToString(dr["Tel"]);
                info.ClassName = Convert.ToString(dr["className"]);
                info.TopNumber = Convert.ToInt32(dr["TopNumber"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.GPoint = Convert.ToInt32(dr["GPoint"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public ShopClassInfo GetShopClassInfo(object cid)
        {
            ShopClassInfo info = new ShopClassInfo();
            string sql = "select * from nt_shopclass  where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.ClassName = Convert.ToString(dr["ClassName"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                info.IsLock = Convert.ToBoolean(dr["IsLock"]);
                info.OrderID = Convert.ToInt32(dr["OrderID"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public List<ShopClassInfo> GetShopClass(int parentid)
        {
            List<ShopClassInfo> infolist = new List<ShopClassInfo>();
            string sql = "select  id, ClassName, ParentID, IsLock, OrderID from nt_shopclass where IsLock =0 and parentid=" + parentid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopClassInfo info = new ShopClassInfo();
                info.ClassName = Convert.ToString(dr["ClassName"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                info.IsLock = Convert.ToBoolean(dr["IsLock"]);
                info.OrderID = Convert.ToInt32(dr["OrderID"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public int InsertShopGoods(ShopGoodsInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[32];
                param[0] = new SqlParameter("@AddRess", SqlDbType.NVarChar, 100);
                param[0].Value = info.AddRess;
                param[1] = new SqlParameter("@AreaID", SqlDbType.Int);
                param[1].Value = info.AreaID;
                param[2] = new SqlParameter("@ClassID", SqlDbType.Int);
                param[2].Value = info.ClassID;
                param[3] = new SqlParameter("@Click", SqlDbType.Int);
                param[3].Value = info.Click;
                param[4] = new SqlParameter("@Content", SqlDbType.NText);
                param[4].Value = info.Content;
                param[5] = new SqlParameter("@DownNumber", SqlDbType.Int);
                param[5].Value = info.DownNumber;
                param[6] = new SqlParameter("@EndTime", SqlDbType.DateTime);
                param[6].Value = info.EndTime;
                param[7] = new SqlParameter("@ExpressContent", SqlDbType.NVarChar, 100);
                param[7].Value = info.ExpressContent;
                param[8] = new SqlParameter("@ExpressStyle", SqlDbType.TinyInt);
                param[8].Value = info.ExpressStyle;
                param[9] = new SqlParameter("@GoodsName", SqlDbType.NVarChar, 60);
                param[9].Value = info.GoodsName;
                param[10] = new SqlParameter("@Id", SqlDbType.Int);
                param[10].Value = info.Id;
                param[11] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[11].Value = info.IsLock;
                param[12] = new SqlParameter("@Keywords", SqlDbType.NVarChar, 30);
                param[12].Value = info.Keywords;
                param[13] = new SqlParameter("@MPrice", SqlDbType.Float);
                param[13].Value = info.MPrice;
                param[14] = new SqlParameter("@MulteBuy", SqlDbType.TinyInt);
                param[14].Value = info.MulteBuy;
                param[15] = new SqlParameter("@MulteMaxNumber", SqlDbType.Int);
                param[15].Value = info.MulteMaxNumber;
                param[16] = new SqlParameter("@BuyNumber", SqlDbType.Int);
                param[16].Value = info.BuyNumber;
                param[17] = new SqlParameter("@MulteMinNumber", SqlDbType.Int);
                param[17].Value = info.MulteMinNumber;
                param[18] = new SqlParameter("@Number", SqlDbType.Int);
                param[18].Value = info.Number;
                param[19] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[19].Value = info.PostIP;
                param[20] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[20].Value = info.PostTime;
                param[21] = new SqlParameter("@Price", SqlDbType.Float);
                param[21].Value = info.Price;
                param[22] = new SqlParameter("@ShopID", SqlDbType.Int);
                param[22].Value = info.ShopID;
                param[23] = new SqlParameter("@StartTime", SqlDbType.DateTime);
                param[23].Value = info.StartTime;
                param[24] = new SqlParameter("@Tel", SqlDbType.NVarChar, 50);
                param[24].Value = info.Tel;
                param[25] = new SqlParameter("@TopNumber", SqlDbType.Int);
                param[25].Value = info.TopNumber;
                param[26] = new SqlParameter("@UserID", SqlDbType.Int);
                param[26].Value = info.UserID;
                param[27] = new SqlParameter("@Pic", SqlDbType.NVarChar, 30);
                param[27].Value = info.Pic;
                param[28] = new SqlParameter("@IsRec", SqlDbType.Bit);
                param[28].Value = info.IsRec;
                param[29] = new SqlParameter("@GPoint", SqlDbType.Int);
                param[29].Value = info.GPoint;
                param[30] = new SqlParameter("@MultePrice", SqlDbType.Float);
                param[30].Value = info.MultePrice;
                param[31] = new SqlParameter("@IsOld", SqlDbType.Bit);
                param[31].Value = info.IsOld;
                string sql = string.Empty;
                if (info.Id > 0)
                {
                    sql = "update NT_ShopGoods set GoodsName=@GoodsName,IsOld=@IsOld, ShopID=@ShopID, Price=@Price, MPrice=@MPrice,MultePrice=@MultePrice, Tel=@Tel, AddRess=@AddRess, Keywords=@Keywords, Content=@Content, StartTime=@StartTime, EndTime=@EndTime, Number=@Number,ClassID=@ClassID, AreaID=@AreaID, ExpressStyle=@ExpressStyle, ExpressContent=@ExpressContent, MulteBuy=@MulteBuy,GPoint=@GPoint, MulteMinNumber=@MulteMinNumber, MulteMaxNumber=@MulteMaxNumber, IsLock=@IsLock, BuyNumber=@BuyNumber, Pic=@Pic where id=@Id and userid=@UserID";
                    int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                    if (m > 0)
                        return info.Id;
                    else
                        return 0;
                }
                else
                {
                    sql = "Insert Into NT_ShopGoods(GoodsName, UserID, ShopID, Price, mPrice,MultePrice, Tel, AddRess, keywords, [Content], Click, TopNumber, DownNumber, StartTime, EndTime, Number, PostTime, PostIP,ClassID, AreaID, ExpressStyle, ExpressContent, MulteBuy, MulteMinNumber, MulteMaxNumber, IsLock, BuyNumber, Pic,IsRec,GPoint,IsOld)values(@GoodsName, @UserID, @ShopID, @Price, @MPrice,@MultePrice, @Tel, @AddRess, @Keywords, @Content, @Click, @TopNumber, @DownNumber, @StartTime, @EndTime, @Number, @PostTime, @PostIP,@ClassID, @AreaID, @ExpressStyle, @ExpressContent, @MulteBuy, @MulteMinNumber, @MulteMaxNumber, @IsLock, @BuyNumber, @Pic,@IsRec,@GPoint,@IsOld)";
                    sql += ";Select SCOPE_IDENTITY()";
                    return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public ShopInfo GetShopForID(object sid)
        {
            ShopInfo mdl = new ShopInfo();
            string sql = "select id, ShopName, CompanyName, Faren, FarenMobile, linkMan, Mobile, Tel, Fax, AddRess, PostCode, ClassID, AreaID, ShopRName, ShopAddress,IsRec, JoinCase, HasSerive, [Content], PostTime, PostIP, IsLock, UserID, Keywords, TopNumber, DownNumber, Click, Pic from nt_shop where id=" + sid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.AddRess = Convert.ToString(dr["address"]);
                mdl.AreaID = Convert.ToInt32(dr["AreaID"]);
                mdl.ClassID = Convert.ToInt32(dr["ClassID"]);
                mdl.Click = Convert.ToInt32(dr["Click"]);
                mdl.CompanyName = Convert.ToString(dr["CompanyName"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.DownNumber = Convert.ToInt32(dr["DownNumber"]);
                mdl.Faren = Convert.ToString(dr["Faren"]);
                mdl.FarenMobile = Convert.ToString(dr["FarenMobile"]);
                mdl.Fax = Convert.ToString(dr["Fax"]);
                mdl.HasSerive = Convert.ToByte(dr["HasSerive"]);
                mdl.Id = Convert.ToInt32(dr["Id"]);
                mdl.IsLock = Convert.ToByte(dr["IsLock"]);
                mdl.JoinCase = Convert.ToString(dr["JoinCase"]);
                mdl.Keywords = Convert.ToString(dr["Keywords"]);
                mdl.LinkMan = Convert.ToString(dr["LinkMan"]);
                mdl.Mobile = Convert.ToString(dr["Mobile"]);
                mdl.Pic = Convert.ToString(dr["Pic"]);
                mdl.PostCode = Convert.ToString(dr["PostCode"]);
                mdl.PostIP = Convert.ToString(dr["PostIP"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.ShopAddress = Convert.ToString(dr["ShopAddress"]);
                mdl.ShopName = Convert.ToString(dr["ShopName"]);
                mdl.ShopRName = Convert.ToString(dr["ShopRName"]);
                mdl.Tel = Convert.ToString(dr["Tel"]);
                mdl.TopNumber = Convert.ToInt32(dr["TopNumber"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                mdl.IsRec = Convert.ToBoolean(dr["IsRec"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public ShopInfo GetShopForUserID(object userid)
        {
            ShopInfo mdl = new ShopInfo();
            string sql = "select id, ShopName, CompanyName, Faren, FarenMobile, linkMan, Mobile, Tel, Fax, AddRess, PostCode, ClassID, AreaID,IsRec, ShopRName, ShopAddress, JoinCase, HasSerive, [Content], PostTime, PostIP, IsLock, UserID, Keywords, TopNumber, DownNumber, Click, Pic from nt_shop where userid=" + userid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.AddRess = Convert.ToString(dr["address"]);
                mdl.AreaID = Convert.ToInt32(dr["AreaID"]);
                mdl.ClassID = Convert.ToInt32(dr["ClassID"]);
                mdl.Click = Convert.ToInt32(dr["Click"]);
                mdl.CompanyName = Convert.ToString(dr["CompanyName"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.DownNumber = Convert.ToInt32(dr["DownNumber"]);
                mdl.Faren = Convert.ToString(dr["Faren"]);
                mdl.FarenMobile = Convert.ToString(dr["FarenMobile"]);
                mdl.Fax = Convert.ToString(dr["Fax"]);
                mdl.HasSerive = Convert.ToByte(dr["HasSerive"]);
                mdl.Id = Convert.ToInt32(dr["Id"]);
                mdl.IsLock = Convert.ToByte(dr["IsLock"]);
                mdl.JoinCase = Convert.ToString(dr["JoinCase"]);
                mdl.Keywords = Convert.ToString(dr["Keywords"]);
                mdl.LinkMan = Convert.ToString(dr["LinkMan"]);
                mdl.Mobile = Convert.ToString(dr["Mobile"]);
                mdl.Pic = Convert.ToString(dr["Pic"]);
                mdl.PostCode = Convert.ToString(dr["PostCode"]);
                mdl.PostIP = Convert.ToString(dr["PostIP"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.ShopAddress = Convert.ToString(dr["ShopAddress"]);
                mdl.ShopName = Convert.ToString(dr["ShopName"]);
                mdl.ShopRName = Convert.ToString(dr["ShopRName"]);
                mdl.Tel = Convert.ToString(dr["Tel"]);
                mdl.TopNumber = Convert.ToInt32(dr["TopNumber"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                mdl.IsRec = Convert.ToBoolean(dr["IsRec"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

 

        public int InsertShop(ShopInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[28];
                param[0] = new SqlParameter("@AddRess", SqlDbType.NVarChar, 60);
                param[0].Value = info.AddRess;
                param[1] = new SqlParameter("@AreaID", SqlDbType.Int);
                param[1].Value = info.AreaID;
                param[2] = new SqlParameter("@ClassID", SqlDbType.Int);
                param[2].Value = info.ClassID;
                param[3] = new SqlParameter("@Click", SqlDbType.Int);
                param[3].Value = info.Click;
                param[4] = new SqlParameter("@Content", SqlDbType.NText);
                param[4].Value = info.Content;
                param[5] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 50);
                param[5].Value = info.CompanyName;
                param[6] = new SqlParameter("@DownNumber", SqlDbType.Int);
                param[6].Value = info.DownNumber;
                param[7] = new SqlParameter("@Faren", SqlDbType.NVarChar, 20);
                param[7].Value = info.Faren;
                param[8] = new SqlParameter("@FarenMobile", SqlDbType.NVarChar, 26);
                param[8].Value = info.FarenMobile;
                param[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 15);
                param[9].Value = info.Fax;
                param[10] = new SqlParameter("@HasSerive", SqlDbType.TinyInt);
                param[10].Value = info.HasSerive;
                param[11] = new SqlParameter("@Id", SqlDbType.Int);
                param[11].Value = info.Id;
                param[12] = new SqlParameter("@Keywords", SqlDbType.NVarChar, 30);
                param[12].Value = info.Keywords;
                param[13] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[13].Value = info.IsLock;
                param[14] = new SqlParameter("@JoinCase", SqlDbType.NVarChar, 100);
                param[14].Value = info.JoinCase;
                param[15] = new SqlParameter("@LinkMan", SqlDbType.NVarChar, 20);
                param[15].Value = info.LinkMan;
                param[16] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 26);
                param[16].Value = info.Mobile;
                param[17] = new SqlParameter("@Pic", SqlDbType.NVarChar, 30);
                param[17].Value = info.Pic;
                param[18] = new SqlParameter("@PostCode", SqlDbType.NVarChar, 10);
                param[18].Value = info.PostCode;
                param[19] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[19].Value = info.PostIP;
                param[20] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[20].Value = info.PostTime;
                param[21] = new SqlParameter("@ShopAddress", SqlDbType.NVarChar, 60);
                param[21].Value = info.ShopAddress;
                param[22] = new SqlParameter("@ShopName", SqlDbType.NVarChar, 80);
                param[22].Value = info.ShopName;
                param[23] = new SqlParameter("@ShopRName", SqlDbType.NVarChar, 50);
                param[23].Value = info.ShopRName;
                param[24] = new SqlParameter("@Tel", SqlDbType.NVarChar, 20);
                param[24].Value = info.Tel;
                param[25] = new SqlParameter("@TopNumber", SqlDbType.Int);
                param[25].Value = info.TopNumber;
                param[26] = new SqlParameter("@UserID", SqlDbType.Int);
                param[26].Value = info.UserID;
                param[27] = new SqlParameter("@IsRec", SqlDbType.Bit);
                param[27].Value = info.IsRec;
                string sql = string.Empty;
                if (info.Id > 0)
                {
                    sql = "update NT_Shop set ShopName=@ShopName, CompanyName=@CompanyName, Faren=@Faren, FarenMobile=@FarenMobile, linkMan=@LinkMan, Mobile=@Mobile, Tel=@Tel, Fax=@Fax, AddRess=@AddRess, PostCode=@PostCode, ClassID=@ClassID, AreaID=@AreaID, ShopRName=@ShopRName, ShopAddress=@ShopAddress, JoinCase=@JoinCase, HasSerive=@HasSerive,Content=@Content, PostTime=@PostTime, PostIP=@PostIP, IsLock=@IsLock, Keywords=@Keywords,  Pic=@Pic where id=@Id and userid=@UserID";
                    int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                    if (m > 0)
                        return info.Id;
                    else
                        return 0;
                }
                else
                {
                    sql = "Insert Into NT_Shop(ShopName, CompanyName, Faren, FarenMobile, linkMan, Mobile, Tel, Fax, AddRess, PostCode, ClassID, AreaID, ShopRName, ShopAddress, JoinCase, HasSerive,[Content], PostTime, PostIP, IsLock, UserID, Keywords, TopNumber, DownNumber, Click, Pic, IsRec)values(@ShopName, @CompanyName, @Faren, @FarenMobile, @LinkMan, @Mobile, @Tel, @Fax, @AddRess, @PostCode, @ClassID, @AreaID, @ShopRName, @ShopAddress, @JoinCase, @HasSerive,@Content, @PostTime, @PostIP, @IsLock, @UserID, @Keywords, @TopNumber, @DownNumber, @Click, @Pic,@IsRec)";
                    sql += ";Select SCOPE_IDENTITY()";
                    return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int GetShopID(int userid)
        {
            try
            {
                string sql = "select id from nt_SHOP where userid=" + userid + " and islock=" + (int)EnumCusState.ForNormal;
                return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
            }
            catch
            {
                return 0;
            }
        }

        public List<ShopInfo> GetShopList(int number, int isrec)
        {
            List<ShopInfo> infolist = new List<ShopInfo>();
            string sql = string.Empty;
            if (isrec == 1)
            {
                sql = "select top " + number + " Id,ShopName,UserID from nt_shop where isrec=1 and islock=" + (byte)EnumCusState.ForNormal + " order by PostTime desc,id desc";
            }
            else
            {
                sql = "select top " + number + " Id,ShopName,UserID from nt_shop where islock=" + (byte)EnumCusState.ForNormal + "  order by PostTime desc,id desc";
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopInfo info = new ShopInfo();
                info.ShopName = Convert.ToString(dr["ShopName"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<ShopGoodsInfo> GetGoodsList(int number, int isrec)
        {
            List<ShopGoodsInfo> infolist = new List<ShopGoodsInfo>();
            string sql = string.Empty;
            if (isrec == 1)
            {
                sql = "select top " + number + " Id,GoodsName,UserID from nt_shopgoods where isrec=1 and islock=" + (byte)EnumCusState.ForNormal + " order by PostTime desc,id desc";
            }
            else
            {
                sql = "select top " + number + " Id,GoodsName,UserID from nt_shopgoods where islock=" + (byte)EnumCusState.ForNormal + "  order by PostTime desc,id desc";
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopGoodsInfo info = new ShopGoodsInfo();
                info.GoodsName = Convert.ToString(dr["GoodsName"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<ShopMulteBuyInfo> GetMulteList(int number, int isrec)
        {
            List<ShopMulteBuyInfo> infolist = new List<ShopMulteBuyInfo>();
            string sql = string.Empty;
            if (isrec == 1)
            {
                sql = "select top " + number + " Id,TITLE,UserID from NT_ShopMultebuy where isrec=1 and islock=" + (byte)EnumCusState.ForNormal + " order by PostTime desc,id desc";
            }
            else
            {
                sql = "select top " + number + " Id,TITLE,UserID from NT_ShopMultebuy where islock=" + (byte)EnumCusState.ForNormal + "  order by PostTime desc,id desc";
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopMulteBuyInfo info = new ShopMulteBuyInfo();
                info.Title = Convert.ToString(dr["Title"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<ShopGoodsInfo> GetUserGoodsList(int number, int userid)
        {
            List<ShopGoodsInfo> infolist = new List<ShopGoodsInfo>();
            string sql = "select top " + number + " Id,GoodsName,UserID from nt_shopgoods where userid=" + userid + " and islock=" + (byte)EnumCusState.ForNormal + " order by PostTime desc,id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopGoodsInfo info = new ShopGoodsInfo();
                info.GoodsName = Convert.ToString(dr["GoodsName"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        /// <summary>
        /// 更改商品状态
        /// </summary>
        /// <param name="gid">商品ID</param>
        /// <param name="gtype">0更新率，1顶一下，2踩一下，3增加累计销售量</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateGoodsState(int gid, int gtype)
        {
            string sql = string.Empty;
            switch (gtype)
            {
                case 0:
                    sql = "update nt_shopgoods set click=click+1 where id=" + gid;
                    break;
                case 1:
                    sql = "update nt_shopgoods set topnumber=topnumber+1 where id=" + gid;
                    break;
                case 2:
                    sql = "update nt_shopgoods set downnumber=downnumber+1 where id=" + gid;
                    break;
                case 3:
                    sql = "update nt_shopgoods set buynumber=buynumber+1 where id=" + gid;
                    break;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DeleteGoods(int infoid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetGoodsInfo(infoid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_ShopGoods where id=" + infoid;
                }
                else
                {
                    sql = "delete from NT_ShopGoods where id=" + infoid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(31) * downinter), 0, 1, "删除商品");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteShop(int infoid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetShopForID(infoid).UserID;
                sql = "update nt_shopgoods set shopid=0 where shopid=" + infoid;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_Shop where id=" + infoid;
                }
                else
                {
                    sql = "delete from NT_Shop where id=" + infoid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(35) * downinter), 0, 1, "删除店铺");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteShopClass(int infoid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                sql = "update nt_shopgoods set ClassID=0 where ClassID=" + infoid;
                sql += ";update nt_shop set ClassID=0 where ClassID=" + infoid;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_ShopClass where id=" + infoid;
                }
                else
                {
                    sql = "delete from NT_ShopClass where id=" + infoid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);

                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertShopClass(ShopClassInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 50);
                param[0].Value = info.ClassName;
                param[1] = new SqlParameter("@Id", SqlDbType.Int);
                param[1].Value = info.Id;
                param[2] = new SqlParameter("@IsLock", SqlDbType.Bit);
                param[2].Value = info.IsLock;
                param[3] = new SqlParameter("@OrderID", SqlDbType.Int);
                param[3].Value = info.OrderID;
                param[4] = new SqlParameter("@ParentID", SqlDbType.Int);
                param[4].Value = info.ParentID;
                string sql = string.Empty;
                if (info.Id > 0)
                {
                    sql = "update NT_ShopClass set ClassName=@ClassName, ParentID=@ParentID, IsLock=@IsLock, OrderID=@OrderID where id=@Id";
                }
                else
                {
                    sql = "Insert Into NT_ShopClass(ClassName, ParentID, IsLock, OrderID)values(@ClassName, @ParentID, @IsLock, @OrderID)";
                }
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteMulte(int infoid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetMulteBuyInfo(infoid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_ShopMultebuy where id=" + infoid;
                }
                else
                {
                    sql = "delete from NT_ShopMultebuy where id=" + infoid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(33) * downinter), 0, 1, "删除团购");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteOrder(int infoid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_ShopOrder where id=" + infoid;
                }
                else
                {
                    sql = "delete from NT_ShopOrder where id=" + infoid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);

                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int JoinMulte(int mid, int uid, string cont)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "select count(*) from NT_ShopMulteMember where userid=" + uid + " and mid=" + mid + "";
                if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null)) > 0)
                    return -1;
                sql = "insert into NT_ShopMulteMember(UserID, MID, PostTime, Tel)values(" + uid + ", " + mid + ", '" + DateTime.Now + "', '" + cont + "')";
                int n= DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    sql = "update NT_ShopMultebuy set JoinMember=JoinMember+1 where id=" + mid;
                    DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InserShopOrder(ShopOrderInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@GoodsID", SqlDbType.Int);
                param[0].Value = info.GoodsID;
                param[1] = new SqlParameter("@GPoint", SqlDbType.Int);
                param[1].Value = info.GPoint;
                param[2] = new SqlParameter("@Id", SqlDbType.Int);
                param[2].Value = info.Id;
                param[3] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[3].Value = info.IsLock;
                param[4] = new SqlParameter("@Money", SqlDbType.Money);
                param[4].Value = info.Money;
                param[5] = new SqlParameter("@OrderNumber", SqlDbType.NVarChar, 16);
                param[5].Value = info.OrderNumber;
                param[6] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[6].Value = info.PostIP;
                param[7] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[7].Value = info.PostTime;
                param[8] = new SqlParameter("@UserID", SqlDbType.Int);
                param[8].Value = info.UserID;
                string sql = "insert into nt_shoporder(GoodsID, UserID, OrderNumber, PostTime, IsLock, PostIP, Money, GPoint)values(@GoodsID, @UserID, @OrderNumber, @PostTime, @IsLock, @PostIP, @Money, @GPoint);Select SCOPE_IDENTITY()";
                return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int UpdateShopOrder(int oid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "select goodsid from nt_shoporder where id=" + oid;
                int goodsid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                //更新销售数
                sql = "update nt_shopgoods set buyNumber=buyNumber+1 where id=" + goodsid;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                //更新库存
                sql = "select number from nt_shopgoods where id=" + goodsid;
                int numbers = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                if (numbers > 0 && numbers != 1000000)
                {
                    sql = "update nt_shopgoods set Number=Number-1 where id=" + goodsid;
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }
                sql = "update nt_shoporder set islock=" + (byte)EnumCusState.ForNormal + " where id=" + oid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int PostOrder(string ordernumber, int gid, int orderid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                ShopGoodsInfo mdl = GetGoodsInfo(gid);
                if (mdl.UserID != uid)
                {
                    return 0;
                }
                sql = "update nt_shoporder set ispost=1 where id=" + orderid + " and ordernumber='" + ordernumber + "'";
                return DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int ReviceOrder(int orderid, int uid)
        {
            string sql = "update nt_shoporder set IsRevice=1 where id=" + orderid + " and userid=" + uid + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public GoodsCommentInfo GetGoodsCommentInfo(object id)
        {
            GoodsCommentInfo mdl = new GoodsCommentInfo();
            string sql = "select  a.id, a.PID, a.UserID,a.Content, a.PostTime, a.PostIP, a.Islock, a.commid, a.cType, a.ShopID,b.TrueName from NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.id=" + id;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.Commid = Convert.ToInt32(dr["Commid"]);
                mdl.CType = Convert.ToByte(dr["CType"]);
                mdl.Id = Convert.ToInt32(dr["Id"]);
                mdl.Islock = Convert.ToBoolean(dr["Islock"]);
                mdl.PID = Convert.ToInt32(dr["PID"]);
                mdl.PostIP = Convert.ToString(dr["PostIP"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.ShopID = Convert.ToInt32(dr["ShopID"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                mdl.TrueName = Convert.ToString(dr["TrueName"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InsertShopComment(GoodsCommentInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Commid", SqlDbType.Int);
                param[0].Value = info.Commid;
                param[1] = new SqlParameter("@Content", SqlDbType.NText);
                param[1].Value = info.Content;
                param[2] = new SqlParameter("@CType", SqlDbType.TinyInt);
                param[2].Value = info.CType;
                param[3] = new SqlParameter("@Islock", SqlDbType.Bit);
                param[3].Value = info.Islock;
                param[4] = new SqlParameter("@PID", SqlDbType.Int);
                param[4].Value = info.PID;
                param[5] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
                param[5].Value = info.PostIP;
                param[6] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[6].Value = info.PostTime;
                param[7] = new SqlParameter("@UserID", SqlDbType.Int);
                param[7].Value = info.UserID;
                string sql = "insert into NT_ShopComment(PID, UserID, PostTime, PostIP, Islock, commid, cType, [Content])values(@PID, @UserID, @PostTime, @PostIP, @Islock, @Commid, @CType, @Content);Select SCOPE_IDENTITY()";
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteShopComment(int cid, int userid)
        {
            string sql=string.Empty;
            GoodsCommentInfo info = GetGoodsCommentInfo(cid);
            int uids = info.UserID;
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                sql = "delete from NT_ShopComment where id=" + cid;
            }
            else
            {
                sql = "delete from NT_ShopComment where id=" + cid + " and userid=" + userid;
            }
            sql += ";update NT_ShopComment set commid=0 where commid=" + cid;
            int n= DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                //扣除积分
                int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                JuSNS.Home.User.User.Instance.UpdateInte(uids, (JuSNS.Common.Public.JSplit(32) * downinter), 0, 1, "删除商品/团购/店铺评论");
            }
            return n;
        }

        public int InsertUserComment(ShopUserCommentInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CommentID", SqlDbType.Int);
                param[0].Value = info.CommentID;
                param[1] = new SqlParameter("@Content", SqlDbType.NText);
                param[1].Value = info.Content;
                param[2] = new SqlParameter("@GoodsID", SqlDbType.Int);
                param[2].Value = info.GoodsID;
                param[3] = new SqlParameter("@Id", SqlDbType.Int);
                param[3].Value = info.Id;
                param[4] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[4].Value = info.PostIP;
                param[5] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[5].Value = info.PostTime;
                param[6] = new SqlParameter("@Sore", SqlDbType.TinyInt);
                param[6].Value = info.Sore;
                param[7] = new SqlParameter("@UserID", SqlDbType.Int);
                param[7].Value = info.UserID;
                string sql = "select count(*) from NT_ShopUserComment where UserID=@UserID and GoodsID=@GoodsID";
                if (Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param)) > 0)
                    return -1;
                sql = "insert into NT_ShopUserComment(GoodsID, Sore, UserID, [Content], PostTime, PostIP, CommentID)values(@GoodsID, @Sore, @UserID, @Content, @PostTime, @PostIP, @CommentID)";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public double GetGoodsSore(int gid)
        {
            string sql = "select count(*),sum(Sore) from NT_ShopUserComment where goodsid=" + gid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                double count = Convert.ToDouble(dr[0]);
                double sum = 0;
                try
                {
                    sum = Convert.ToDouble(dr[1]);
                }
                catch
                {
                    sum = 0;
                    dr.Close();
                    return 0;
                }
                dr.Close();
                double reuls = Convert.ToDouble(sum / count);
                return reuls;
            }
            dr.Close();
            return 0;
        }

        public bool isShopUserComment(int gid, int userid)
        {
            string sql = "select count(*) from NT_ShopUserComment where UserID=" + userid + " and GoodsID=" + gid + "";
            if (Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0)
                return true;
            else
                return false;
        }

        public ShopMulteBuyInfo GetMulteBuyInfo(object mid)
        {
            ShopMulteBuyInfo info = new ShopMulteBuyInfo();
            string sql = "select  id, UserID, GoodsID, Title, [Content], MinMember, MaxMember, IsLock, StartTime, EndTime,Pic, JoinMember, AttMember, Price, IsRec, PostTime, PostIP, ProvinceID, CityID,  AddRess, LinkStyle, Keywords from NT_ShopMultebuy where id=" + mid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.AddRess = Convert.ToString(dr["AddRess"]);
                info.AttMember = Convert.ToInt32(dr["AttMember"]);
                info.CityID = Convert.ToInt32(dr["CityID"]);
                info.Content = Convert.ToString(dr["Content"]);
                info.EndTime = Convert.ToDateTime(dr["EndTime"]);
                info.GoodsID = Convert.ToInt32(dr["GoodsID"]);
                info.Id = Convert.ToInt32(mid);
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.JoinMember = Convert.ToInt32(dr["JoinMember"]);
                info.Keywords = Convert.ToString(dr["Keywords"]);
                info.LinkStyle = Convert.ToString(dr["LinkStyle"]);
                info.MaxMember = Convert.ToInt32(dr["MaxMember"]);
                info.MinMember = Convert.ToInt32(dr["MinMember"]);
                info.PostIP = Convert.ToString(dr["PostIP"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.Price = Convert.ToDecimal(dr["Price"]);
                info.ProvinceID = Convert.ToInt32(dr["ProvinceID"]);
                info.StartTime = Convert.ToDateTime(dr["StartTime"]);
                info.Title = Convert.ToString(dr["Title"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Pic = Convert.ToString(dr["Pic"]);
                info.IsRec = Convert.ToBoolean(dr["IsRec"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InsertMulteBuy(ShopMulteBuyInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@AddRess", SqlDbType.NVarChar, 60);
                param[0].Value = info.AddRess;
                param[1] = new SqlParameter("@AttMember", SqlDbType.Int);
                param[1].Value = info.AttMember;
                param[2] = new SqlParameter("@CityID", SqlDbType.Int);
                param[2].Value = info.CityID;
                param[3] = new SqlParameter("@Content", SqlDbType.NText);
                param[3].Value = info.Content;
                param[4] = new SqlParameter("@EndTime", SqlDbType.DateTime);
                param[4].Value = info.EndTime;
                param[5] = new SqlParameter("@GoodsID", SqlDbType.Int);
                param[5].Value = info.GoodsID;
                param[6] = new SqlParameter("@Id", SqlDbType.Int);
                param[6].Value = info.Id;
                param[7] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[7].Value = info.IsLock;
                param[8] = new SqlParameter("@JoinMember", SqlDbType.Int);
                param[8].Value = info.JoinMember;
                param[9] = new SqlParameter("@Keywords", SqlDbType.NVarChar, 30);
                param[9].Value = info.Keywords;
                param[10] = new SqlParameter("@LinkStyle", SqlDbType.NVarChar,50);
                param[10].Value = info.LinkStyle;
                param[11] = new SqlParameter("@MaxMember", SqlDbType.Int);
                param[11].Value = info.MaxMember;
                param[12] = new SqlParameter("@MinMember", SqlDbType.Int);
                param[12].Value = info.MinMember;
                param[13] = new SqlParameter("@Pic", SqlDbType.NVarChar,30);
                param[13].Value = info.Pic;
                param[14] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[14].Value = info.PostIP;
                param[15] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[15].Value = info.PostTime;
                param[16] = new SqlParameter("@Price", SqlDbType.Money);
                param[16].Value = info.Price;
                param[17] = new SqlParameter("@ProvinceID", SqlDbType.Int);
                param[17].Value = info.ProvinceID;
                param[18] = new SqlParameter("@StartTime", SqlDbType.DateTime);
                param[18].Value = info.StartTime;
                param[19] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
                param[19].Value = info.Title;
                param[20] = new SqlParameter("@UserID", SqlDbType.Int);
                param[20].Value = info.UserID;
                param[21] = new SqlParameter("@IsRec", SqlDbType.Bit);
                param[21].Value = info.IsRec;
                string sql = string.Empty;
                if (info.Id > 0)
                {
                    sql = "update NT_ShopMultebuy set Title=@Title, Content=@Content, MinMember=@MinMember, MaxMember=@MaxMember, IsLock=@IsLock, StartTime=@StartTime, EndTime=@EndTime,  Price=@Price, PostTime=@PostTime, PostIP=@PostIP, ProvinceID=@ProvinceID, CityID=@CityID, AddRess=@AddRess, LinkStyle=@LinkStyle, Keywords=@Keywords, Pic=@Pic where id=@Id and userid=@UserID";
                    int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                    if (m > 0)
                        return info.Id;
                    else
                        return 0;
                }
                else
                {
                    sql = "Insert Into NT_ShopMultebuy(UserID, GoodsID, Title, [Content], MinMember, MaxMember, IsLock, StartTime, EndTime, JoinMember, AttMember, Price, PostTime, PostIP, ProvinceID, CityID, AddRess, LinkStyle, Keywords, Pic)values(@UserID, @GoodsID, @Title, @Content, @MinMember, @MaxMember, @IsLock, @StartTime, @EndTime, @JoinMember, @AttMember, @Price, @PostTime, @PostIP, @ProvinceID, @CityID, @AddRess, @LinkStyle, @Keywords, @Pic)";
                    sql += ";Select SCOPE_IDENTITY()";
                    return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteShopNews(int nid)
        {
            string sql = "delete from nt_shopnews where id=" + nid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public ShopNewsInfo GetShopNewsInfo(int nid)
        {
            ShopNewsInfo info = new ShopNewsInfo();
            string sql = "select id, ShopID, Title, [Content], creatTime, islock, click from nt_shopnews where id=" + nid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Click = int.Parse(dr["click"].ToString());
                info.Content = dr["Content"].ToString();
                info.CreatTime = DateTime.Parse(dr["CreatTime"].ToString());
                info.Id = nid;
                info.Islock = bool.Parse(dr["Islock"].ToString());
                info.ShopID = int.Parse(dr["ShopID"].ToString());
                info.Title = dr["Title"].ToString();
            }
            dr.Close();
            return info;
        }

        public int InsertShopNews(ShopNewsInfo info)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Click", SqlDbType.Int);
            param[0].Value = info.Click;
            param[1] = new SqlParameter("@Content", SqlDbType.NText);
            param[1].Value = info.Content;
            param[2] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
            param[2].Value = info.CreatTime;
            param[3] = new SqlParameter("@Id", SqlDbType.Int);
            param[3].Value = info.Id;
            param[4] = new SqlParameter("@Islock", SqlDbType.Bit);
            param[4].Value = info.Islock;
            param[5] = new SqlParameter("@ShopID", SqlDbType.Int);
            param[5].Value = info.ShopID;
            param[6] = new SqlParameter("@Title", SqlDbType.NVarChar,60);
            param[6].Value = info.Title;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update nt_shopnews  set Content=@Content,CreatTime=@CreatTime,Title=@Title where id=@Id";
            }
            else
            {
                sql = "insert into nt_shopnews(ShopID, Title, [Content], creatTime, islock, click)values(@ShopID, @Title, @Content, @CreatTime, @Islock, @Click)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateShopNewsClicks(int nid)
        {
            string sql = "update nt_shopnews set click=click+1 where id=" + nid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }
    }
}