//===============================================
//��������������������\\\|///                      
//��������������������\\��- -��//                   
//��������������������  ( @ @ )                    
//��������������������oOOo-(_)-oOOo��������          
//��                                     ��
//��             �� �� ԭ ����           ��
//��      lenlong ��Ʒ���뱣������Ϣ��   ��
//��      ** lenlenlong@hotmail.com **   ��
//��                                     ��
//����������������������������Dooo��     ��
//�������������������� oooD��-(�� )��������
//�������������������� (  )��  ) /
//����������������������\ (�� (_/
//���������������������� \_)
//===============================================
using System;
using System.Data;
using System.Collections;
namespace Components.Components
{
    public class User
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }
        public void SetDataProviders(DataRow rs)
        {
            UserID = int.Parse(rs["UserID"].ToString());
            UserName = rs["UserName"].ToString();
            UserNickName = rs["UserNickName"].ToString();
            UserPassWord = rs["UserPassWord"].ToString();
            UserPassWordAnswer = rs["UserPassWordAnswer"].ToString();
            UserEmail = rs["UserEmail"].ToString();
            UserRecommendUserID = int.Parse(rs["UserRecommendUserID"].ToString());
            UserSex = rs["UserSex"].ToString();
            UserBirthday = System.Convert.ToDateTime(rs["UserBirthday"].ToString());
            UserTrueName = rs["UserTrueName"].ToString();
            UserComeFrom = rs["UserComeFrom"].ToString();
            UserContactAddress = rs["UserContactAddress"].ToString();
            UserCode = rs["UserCode"].ToString();
            UserContactTel = rs["UserContactTel"].ToString();
            UserMobile = rs["UserMobile"].ToString();
            UserOICQ = rs["UserOICQ"].ToString();
            UserIdCard = rs["UserIdCard"].ToString();
            UserMaritalStatus = int.Parse(rs["UserMaritalStatus"].ToString());
            UserWorkUnit = rs["UserWorkUnit"].ToString();
            UserSchool = rs["UserSchool"].ToString();
            UserSign = rs["UserSign"].ToString();
            UserAbout = rs["UserAbout"].ToString();
            UserFace = rs["UserFace"].ToString();
            UserLastIP = rs["UserLastIP"].ToString();
            UserPrivacy = int.Parse(rs["UserPrivacy"].ToString());
            UserRegTime = System.Convert.ToDateTime(rs["UserRegTime"].ToString());
            UserLastLoginTime = System.Convert.ToDateTime(rs["UserLastLoginTime"].ToString());
            UserLoginNumber = int.Parse(rs["UserLoginNumber"].ToString());
            UserOnlineTime = int.Parse(rs["UserOnlineTime"].ToString());
            UserLastActTime = System.Convert.ToDateTime(rs["UserLastActTime"].ToString());
            UserPoint = int.Parse(rs["UserPoint"].ToString());
            UserPrestige = int.Parse(rs["UserPrestige"].ToString());
            UserRmb = int.Parse(rs["UserRmb"].ToString());
            UserTicket = int.Parse(rs["UserTicket"].ToString());
            UserMoney = int.Parse(rs["UserMoney"].ToString());
            UserExp = int.Parse(rs["UserExp"].ToString());
            UserCP = int.Parse(rs["UserCP"].ToString());
            UserOnLineStatic = rs["UserOnLineStatic"].ToString();
            UserGroup = rs["UserGroupID"].ToString();
            if (rs["UserLevelID"].ToString() != string.Empty)
            {
                UserLevelID = int.Parse(rs["UserLevelID"].ToString());
            }
            UserRole = rs["UserRole"].ToString();
            UserPostNumber = int.Parse(rs["UserPostNumber"].ToString());
            UserTopicNumber = int.Parse(rs["UserTopicNumber"].ToString());
            UserReTopicNumber = int.Parse(rs["UserReTopicNumber"].ToString());
            UserDelTopicNumber = int.Parse(rs["UserDelTopicNumber"].ToString());
            UserWebAddress = rs["UserWebAddress"].ToString();
            UserWebLog = rs["UserWebLog"].ToString();
            UserWebGallery = rs["UserWebGallery"].ToString();
            UserInterests = rs["UserInterests"].ToString();
            UserTrueMoney = double.Parse(rs["UserTrueMoney"].ToString());
            UserFalse = int.Parse(rs["UserFalse"].ToString());
            UserReceiveType = int.Parse(rs["UserReceiveType"].ToString());
        }//

        private int _UserID;
        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int _UserFalse;
        /// <summary>
        /// �û��Ƿ񱻽���
        /// </summary>
        public int UserFalse
        {
            get { return _UserFalse; }
            set { _UserFalse = value; }
        }
        private string _UserName;
        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _UserNickName;
        /// <summary>
        /// �û��ǳ�
        /// </summary>
        public string UserNickName
        {
            get { return _UserNickName; }
            set { _UserNickName = value; }
        }
        private string _UserPassWord;
        /// <summary>
        /// �û�����
        /// </summary>
        public string UserPassWord
        {
            get { return _UserPassWord; }
            set { _UserPassWord = value; }
        }
        private string _UserPassWordAnswer;
        /// <summary>
        /// �����
        /// </summary>
        public string UserPassWordAnswer
        {
            get { return _UserPassWordAnswer; }
            set { _UserPassWordAnswer = value; }
        }
        private string _UserEmail;
        /// <summary>
        /// �ʼ���ַ
        /// </summary>
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }
        private int _UserRecommendUserID;
        /// <summary>
        /// �Ƽ��û���ID
        /// </summary>
        public int UserRecommendUserID
        {
            get { return _UserRecommendUserID; }
            set { _UserRecommendUserID = value; }
        }
        private string _UserSex;
        /// <summary>
        /// �û��Ա�,M�У�FŮ��N������
        /// </summary>
        public string UserSex
        {
            get { return _UserSex; }
            set { _UserSex = value; }
        }
        private DateTime _UserBirthday;
        /// <summary>
        /// �û�����
        /// </summary>
        public DateTime UserBirthday
        {
            get { return _UserBirthday; }
            set { _UserBirthday = value; }
        }
        private string _UserTrueName;
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string UserTrueName
        {
            get { return _UserTrueName; }
            set { _UserTrueName = value; }
        }
        private string _UserComeFrom;
        /// <summary>
        /// ��������
        /// </summary>
        public string UserComeFrom
        {
            get { return _UserComeFrom; }
            set { _UserComeFrom = value; }
        }
        private string _UserContactAddress;
        /// <summary>
        /// ��ϵ��ַ
        /// </summary>
        public string UserContactAddress
        {
            get { return _UserContactAddress; }
            set { _UserContactAddress = value; }
        }
        private string _UserCode;
        /// <summary>
        /// �ʱ����
        /// </summary>
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        private string _UserContactTel;
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string UserContactTel
        {
            get { return _UserContactTel; }
            set { _UserContactTel = value; }
        }
        private string _UserMobile;
        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string UserMobile
        {
            get { return _UserMobile; }
            set { _UserMobile = value; }
        }
        private string _UserOICQ;
        /// <summary>
        /// OICQ����
        /// </summary>
        public string UserOICQ
        {
            get { return _UserOICQ; }
            set { _UserOICQ = value; }
        }
        private string _UserIdCard;
        /// <summary>
        /// ���֤
        /// </summary>
        public string UserIdCard
        {
            get { return _UserIdCard; }
            set { _UserIdCard = value; }
        }
        private int _UserMaritalStatus;
        /// <summary>
        /// ���״̬,0δ��,1�ѻ�,2����,3ɥż
        /// </summary>
        public int UserMaritalStatus
        {
            get { return _UserMaritalStatus; }
            set { _UserMaritalStatus = value; }
        }
        private string _UserWorkUnit;
        /// <summary>
        /// ������λ
        /// </summary>
        public string UserWorkUnit
        {
            get { return _UserWorkUnit; }
            set { _UserWorkUnit = value; }
        }
        private string _UserSchool;
        /// <summary>
        /// ��ҵԺУ
        /// </summary>
        public string UserSchool
        {
            get { return _UserSchool; }
            set { _UserSchool = value; }
        }
        private string _UserSign;
        /// <summary>
        /// ����ǩ��
        /// </summary>
        public string UserSign
        {
            get { return _UserSign; }
            set { _UserSign = value; }
        }
        private string _UserAbout;
        /// <summary>
        /// ����˵��
        /// </summary>
        public string UserAbout
        {
            get { return _UserAbout; }
            set { _UserAbout = value; }
        }
        private string _UserFace;
        /// <summary>
        /// ͷ��
        /// </summary>
        public string UserFace
        {
            get { return _UserFace; }
            set { _UserFace = value; }
        }
        private string _UserLastIP;
        /// <summary>
        /// IP��ַ
        /// </summary>
        public string UserLastIP
        {
            get { return _UserLastIP; }
            set { _UserLastIP = value; }
        }
        private int _UserPrivacy;
        /// <summary>
        /// ���ܷ�ʽ
        /// </summary>
        public int UserPrivacy
        {
            get { return _UserPrivacy; }
            set { _UserPrivacy = value; }
        }
        private DateTime _UserRegTime;
        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime UserRegTime
        {
            get { return _UserRegTime; }
            set { _UserRegTime = value; }
        }
        private DateTime _UserLastLoginTime;
        /// <summary>
        /// ����¼ʱ��
        /// </summary>
        public DateTime UserLastLoginTime
        {
            get { return _UserLastLoginTime; }
            set { _UserLastLoginTime = value; }
        }
        private int _UserLoginNumber;
        /// <summary>
        /// ��¼����
        /// </summary>
        public int UserLoginNumber
        {
            get { return _UserLoginNumber; }
            set { _UserLoginNumber = value; }
        }
        private int _UserOnlineTime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int UserOnlineTime
        {
            get { return _UserOnlineTime; }
            set { _UserOnlineTime = value; }
        }
        private DateTime _UserLastActTime;
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime UserLastActTime
        {
            get { return _UserLastActTime; }
            set { _UserLastActTime = value; }
        }
        private int _UserPoint;
        /// <summary>
        /// ����
        /// </summary>
        public int UserPoint
        {
            get { return _UserPoint; }
            set { _UserPoint = value; }
        }
        private int _UserPrestige;
        /// <summary>
        /// ����
        /// </summary>
        public int UserPrestige
        {
            get { return _UserPrestige; }
            set { _UserPrestige = value; }
        }
        private int _UserRmb;
        /// <summary>
        /// �����
        /// </summary>
        public int UserRmb
        {
            get { return _UserRmb; }
            set { _UserRmb = value; }
        }
        private int _UserTicket;
        /// <summary>
        /// ��ȯ
        /// </summary>
        public int UserTicket
        {
            get { return _UserTicket; }
            set { _UserTicket = value; }
        }
        private int _UserMoney;
        /// <summary>
        /// �����Ǯ
        /// </summary>
        public int UserMoney
        {
            get { return _UserMoney; }
            set { _UserMoney = value; }
        }
        private int _UserExp;
        /// <summary>
        /// ����ֵ
        /// </summary>
        public int UserExp
        {
            get { return _UserExp; }
            set { _UserExp = value; }
        }
        private int _UserCP;
        /// <summary>
        /// ����ֵ
        /// </summary>
        public int UserCP
        {
            get { return _UserCP; }
            set { _UserCP = value; }
        }
        private string _UserOnLineStatic;
        /// <summary>
        /// ����״̬
        /// </summary>
        public string UserOnLineStatic
        {
            get { return _UserOnLineStatic; }
            set { _UserOnLineStatic = value; }
        }
        private string _UserGroup;
        /// <summary>
        /// ���ֲ�
        /// </summary>
        public string UserGroup
        {
            get { return _UserGroup; }
            set { _UserGroup = value; }
        }
        private int _UserLevelID;
        /// <summary>
        /// �ȼ�
        /// </summary>
        public int UserLevelID
        {
            get { return _UserLevelID; }
            set { _UserLevelID = value; }
        }
        private string _UserRole;
        /// <summary>
        /// �û���ɫ
        /// </summary>
        public string UserRole
        {
            get { return _UserRole; }
            set { _UserRole = value; }
        }
        private int _UserPostNumber;
        /// <summary>
        /// ������������
        /// </summary>
        public int UserPostNumber
        {
            get { return _UserPostNumber; }
            set { _UserPostNumber = value; }
        }
        private int _UserTopicNumber;
        /// <summary>
        /// ����������
        /// </summary>
        public int UserTopicNumber
        {
            get { return _UserTopicNumber; }
            set { _UserTopicNumber = value; }
        }
        private int _UserReTopicNumber;
        /// <summary>
        /// �ظ�������
        /// </summary>
        public int UserReTopicNumber
        {
            get { return _UserReTopicNumber; }
            set { _UserReTopicNumber = value; }
        }
        private int _UserDelTopicNumber;
        /// <summary>
        /// ��ɾ��������
        /// </summary>
        public int UserDelTopicNumber
        {
            get { return _UserDelTopicNumber; }
            set { _UserDelTopicNumber = value; }
        }
        private string _UserWebAddress;
        /// <summary>
        /// ������ҳ
        /// </summary>
        public string UserWebAddress
        {
            get { return _UserWebAddress; }
            set { _UserWebAddress = value; }
        }
        private string _UserWebLog;
        /// <summary>
        /// ���˲��͵�ַ
        /// </summary>
        public string UserWebLog
        {
            get { return _UserWebLog; }
            set { _UserWebLog = value; }
        }
        private string _UserWebGallery;
        /// <summary>
        /// ��������ַ
        /// </summary>
        public string UserWebGallery
        {
            get { return _UserWebGallery; }
            set { _UserWebGallery = value; }
        }
        private string _UserInterests;
        /// <summary>
        /// ���˰���
        /// </summary>
        public string UserInterests
        {
            get { return _UserInterests; }
            set { _UserInterests = value; }
        }
        private double _UserTrueMoney;
        /// <summary>
        /// ��ʵ��Ǯ
        /// </summary>
        public double UserTrueMoney
        {
            get { return _UserTrueMoney; }
            set { _UserTrueMoney = value; }
        }
        /// <summary>
        /// ���ն��ŷ�ʽ 0���������˵Ķ���,1ֻ����ע���û��Ķ���,2ֻ���պ��ѵĶ���,3�������κζ���
        /// </summary>
        private int _UserReceiveType;
        public int UserReceiveType
        {
            get { return _UserReceiveType; }
            set { _UserReceiveType = value; }
        }
    }
}
