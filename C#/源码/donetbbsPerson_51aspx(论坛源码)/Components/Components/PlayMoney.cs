//===============================================
//　　　　　　　　　　\\\|///                      
//　　　　　　　　　　\\　- -　//                   
//　　　　　　　　　　  ( @ @ )                    
//┏━━━━━━━━━oOOo-(_)-oOOo━━━┓          
//┃                                     ┃
//┃             东 网 原 创！           ┃
//┃      lenlong 作品，请保留此信息！   ┃
//┃      ** lenlenlong@hotmail.com **   ┃
//┃                                     ┃
//┃　　　　　　　　　　　　　Dooo　     ┃
//┗━━━━━━━━━ oooD━-(　 )━━━┛
//　　　　　　　　　　 (  )　  ) /
//　　　　　　　　　　　\ (　 (_/
//　　　　　　　　　　　 \_)
//===============================================
using System;
using System.Data;
using System.Collections;
namespace Components.Components
{
    public class PlayMoney
    {
        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            PlayMoneyID = int.Parse(rs["PlayMoneyID"].ToString());
            PlayMoneys = double.Parse(rs["PlayMoneys"].ToString());
            PlayMoneyFromUserID = int.Parse(rs["PlayMoneyFromUserID"].ToString());
            PlayMoneyReceiveUserID = int.Parse(rs["PlayMoneyReceiveUserID"].ToString());
            PlayMoneyCreateTime = System.Convert.ToDateTime(rs["PlayMoneyCreateTime"].ToString());
            PlayMoneyType = int.Parse(rs["PlayMoneyType"].ToString());
            PlayMoneyContent = rs["PlayMoneyContent"].ToString();
        }//
        private int _PlayMoneyID;
        public int PlayMoneyID
        {
            get { return _PlayMoneyID; }
            set { _PlayMoneyID = value; }
        }
        private double _PlayMoneys;
        public double PlayMoneys
        {
            get { return _PlayMoneys; }
            set { _PlayMoneys = value; }
        }
        private int _PlayMoneyFromUserID;
        /// <summary>
        /// 支付人ID
        /// </summary>
        public int PlayMoneyFromUserID
        {
            get { return _PlayMoneyFromUserID; }
            set { _PlayMoneyFromUserID = value; }
        }
        private int _PlayMoneyReceiveUserID;
        /// <summary>
        /// 收款人ID
        /// </summary>
        public int PlayMoneyReceiveUserID
        {
            get { return _PlayMoneyReceiveUserID; }
            set { _PlayMoneyReceiveUserID = value; }
        }
        private DateTime _PlayMoneyCreateTime;
        public DateTime PlayMoneyCreateTime
        {
            get { return _PlayMoneyCreateTime; }
            set { _PlayMoneyCreateTime = value; }
        }
        private int _PlayMoneyType;
        /// <summary>
        /// 0虚拟金钱，1真实金钱
        /// </summary>
        public int PlayMoneyType
        {
            get { return _PlayMoneyType; }
            set { _PlayMoneyType = value; }
        }
        private string _PlayMoneyContent;
        public string PlayMoneyContent
        {
            get { return _PlayMoneyContent; }
            set { _PlayMoneyContent = value; }
        }
    }
}
