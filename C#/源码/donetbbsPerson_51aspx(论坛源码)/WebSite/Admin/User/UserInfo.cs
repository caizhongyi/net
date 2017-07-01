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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace WebSite.Admin.User
{
    public class UserInfo : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label LabelType1;
        Label LabelType2;
        Label userID;
        TextBox UserName;
        TextBox UserNickName;
        TextBox UserPassWord;
        TextBox UserPassWordAnswer;
        TextBox UserIdCard;
        RadioButtonList UserFalse;
        DropDownList UserPrivacy;
        DropDownList UserReceiveType;
        TextBox UserEmail;
        TextBox UserPoint;
        TextBox UserPrestige;
        DropDownList UserLevelID;
        DropDownList UserOnLineStatic;
        TextBox UserComeFrom;
        TextBox UserMobile;
        TextBox UserTrueName;
        RadioButtonList UserSex;
        TextBox UserSchool;
        TextBox UserBirthday;
        TextBox UserRecommendUser;
        RadioButtonList UserMaritalStatus;
        TextBox UserFace;
        TextBox UserSign;
        TextBox UserAbout;
        TextBox UserLoginNumber;
        TextBox UserRegTime;
        TextBox UserGroup;
        TextBox UserRole;
        TextBox UserExp;
        TextBox UserCP;
        TextBox UserMoney;
        TextBox UserTrueMoney;
        TextBox UserTicket;
        TextBox UserOICQ;
        TextBox UserPostNumber;
        TextBox UserLastActTime;
        TextBox UserTopicNumber;
        TextBox UserReTopicNumber;
        TextBox UserOnlineTime;
        TextBox UserContactTel;
        TextBox UserCode;
        TextBox UserWebAddress;
        TextBox UserWebLog;
        TextBox UserWebGallery;
        TextBox UserWorkUnit;
        TextBox UserContactAddress;
        TextBox UserInterests;
        TextBox UserLastLoginTime;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            userID = (Label)FindControl("userID");
            LabelType1 = (Label)FindControl("LabelType1");
            LabelType2 = (Label)FindControl("LabelType2");
            UserName = (TextBox)FindControl("UserName");
            UserNickName = (TextBox)FindControl("UserNickName");
            UserPassWord = (TextBox)FindControl("UserPassWord");
            UserPassWordAnswer = (TextBox)FindControl("UserPassWordAnswer");
            UserIdCard = (TextBox)FindControl("UserIdCard");
            UserFalse = (RadioButtonList)FindControl("UserFalse");
            UserPrivacy = (DropDownList)FindControl("UserPrivacy");
            UserReceiveType = (DropDownList)FindControl("UserReceiveType");
            UserEmail = (TextBox)FindControl("UserEmail");
            UserPoint = (TextBox)FindControl("UserPoint");
            UserPrestige = (TextBox)FindControl("UserPrestige");
            UserLevelID = (DropDownList)FindControl("UserLevelID");
            UserOnLineStatic = (DropDownList)FindControl("UserOnLineStatic");
            UserComeFrom = (TextBox)FindControl("UserComeFrom");
            UserMobile = (TextBox)FindControl("UserMobile");
            UserTrueName = (TextBox)FindControl("UserTrueName");
            UserSex = (RadioButtonList)FindControl("UserSex");
            UserSchool = (TextBox)FindControl("UserSchool");
            UserBirthday = (TextBox)FindControl("UserBirthday");
            UserRecommendUser = (TextBox)FindControl("UserRecommendUser");
            UserMaritalStatus = (RadioButtonList)FindControl("UserMaritalStatus");
            UserFace = (TextBox)FindControl("UserFace");
            UserSign = (TextBox)FindControl("UserSign");
            UserAbout = (TextBox)FindControl("UserAbout");
            UserLoginNumber = (TextBox)FindControl("UserLoginNumber");
            UserRegTime = (TextBox)FindControl("UserRegTime");
            UserGroup = (TextBox)FindControl("UserGroup");
            UserRole = (TextBox)FindControl("UserRole");
            UserExp = (TextBox)FindControl("UserExp");
            UserCP = (TextBox)FindControl("UserCP");
            UserMoney = (TextBox)FindControl("UserMoney");
            UserTrueMoney = (TextBox)FindControl("UserTrueMoney");
            UserTicket = (TextBox)FindControl("UserTicket");
            UserOICQ = (TextBox)FindControl("UserOICQ");
            UserPostNumber = (TextBox)FindControl("UserPostNumber");
            UserLastActTime = (TextBox)FindControl("UserLastActTime");
            UserTopicNumber = (TextBox)FindControl("UserTopicNumber");
            UserReTopicNumber = (TextBox)FindControl("UserReTopicNumber");
            UserOnlineTime = (TextBox)FindControl("UserOnlineTime");
            UserContactTel = (TextBox)FindControl("UserContactTel");
            UserCode = (TextBox)FindControl("UserCode");
            UserWebAddress = (TextBox)FindControl("UserWebAddress");
            UserWebLog = (TextBox)FindControl("UserWebLog");
            UserWebGallery = (TextBox)FindControl("UserWebGallery");
            UserWorkUnit = (TextBox)FindControl("UserWorkUnit");
            UserContactAddress = (TextBox)FindControl("UserContactAddress");
            UserInterests = (TextBox)FindControl("UserInterests");
            UserLastLoginTime = (TextBox)FindControl("UserLastLoginTime");

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator && !currentUser.IsMembershipAdministrator)
            {
                IDoNetBbs.WriteAlert("��û�в�����Ȩ��", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            if (IsPostBack)
            {
                PostData();
            }
            else
            {
                DataBind();
            }
        }
        public override void DataBind()
        {
            base.DataBind();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();

            UserPrivacy.Items.Add(new ListItem("�û����Ϲ���", "1"));
            UserPrivacy.Items.Add(new ListItem("�û����ϱ���","0"));
            UserPrivacy.Items.Add(new ListItem("ֻ��ע���û�����","2"));
            UserPrivacy.Items.Add(new ListItem("ֻ�Ժ��ѹ���", "3"));

            UserReceiveType.Items.Add(new ListItem("ֻ����ע���û��Ķ���", "1"));
            UserReceiveType.Items.Add(new ListItem("���������˵Ķ���", "0"));
            UserReceiveType.Items.Add(new ListItem("ֻ���պ��ѵĶ���", "2"));
            UserReceiveType.Items.Add(new ListItem("�������κζ���", "3"));

            UserOnLineStatic.Items.Add(new ListItem("����(O)", "UserOnLine"));
            UserOnLineStatic.Items.Add(new ListItem("æµ(B)", "UserBusyLine"));
            UserOnLineStatic.Items.Add(new ListItem("���ϻ���(E)", "UserRightBack"));
            UserOnLineStatic.Items.Add(new ListItem("�뿪(A)", "UserLeave"));
            UserOnLineStatic.Items.Add(new ListItem("�����绰(P)", "UserTelephone"));
            UserOnLineStatic.Items.Add(new ListItem("����Ͳ�(L)", "UserOutMeal"));
            UserOnLineStatic.Items.Add(new ListItem("�ѻ�(F)", "UserOffLine"));

            UserFalse.Items.Add(new ListItem("��", "0"));
            UserFalse.Items.Add(new ListItem("��", "1"));
            UserFalse.Items[0].Selected = true;
            UserMaritalStatus.Items.Add(new ListItem("����", "0"));
            UserMaritalStatus.Items.Add(new ListItem("δ��", "1"));
            UserMaritalStatus.Items.Add(new ListItem("�ѻ�", "2"));
            UserMaritalStatus.Items.Add(new ListItem("����", "3"));
            UserMaritalStatus.Items.Add(new ListItem("ɥż", "4"));

            UserMaritalStatus.Items[0].Selected = true;


            UserSex.Items.Add(new ListItem("����", "M"));
            UserSex.Items.Add(new ListItem("Ů��", "F"));
            UserSex.Items.Add(new ListItem("����", "N"));
            UserSex.Items[0].Selected = true;

            UserRegTime.Text = DateTime.Now.ToString();
            UserLastActTime.Text = DateTime.Now.ToString();
            UserLastLoginTime.Text = DateTime.Now.ToString();

            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.UserLevel IUserLevel = new Components.Components.UserLevel();

            Components.Components.User IUser = new Components.Components.User();
            //DataProviders.UserLevelDataProviders userLevel = new DataProviders.UserLevelDataProviders();
            //userLevel.SetUserLevel(0, 0, true);
            IUserLevel.Arraylist = MyUser.SetUserLevel(true);
            foreach (Components.Components.UserLevel rs in IUserLevel.Arraylist)
            {
                UserLevelID.Items.Add(new ListItem(rs.UserLevelTitle, rs.UserLevelID.ToString()));
            }

            DataRow dr;
            if (IDoNetBbs.GetQueryInt("UserID") == 0)
            {
                LabelType1.Visible = false;
                LabelType2.Visible = true;
            }
            else
            {
                userID.Text = IDoNetBbs.GetQueryString("UserID");
                LabelType1.Visible = true;
                LabelType2.Visible = false;

                dr = MyUser.SetUserInfo(IDoNetBbs.GetQueryInt("UserID"), false);
                //DataProviders.UserInfoDataProviders userInfo = new DataProviders.UserInfoDataProviders();
                //userInfo.SetUserInfo(IDoNetBbs.GetQueryInt("UserID"), false);
                if (dr == null)
                {
                    IDoNetBbs.WriteAlert("���û�������", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IUser.SetDataProviders(dr);
                try
                {
                    UserPrivacy.Items.FindByValue(IUser.UserPrivacy.ToString()).Selected = true;
                }
                catch
                {
                }
                try
                {
                    UserReceiveType.Items.FindByValue(IUser.UserReceiveType.ToString()).Selected = true;
                }
                catch
                {
                }
                try
                {
                    UserOnLineStatic.Items.FindByValue(IUser.UserOnLineStatic.ToString()).Selected = true;
                }
                catch
                {
                }
                try
                {
                    UserLevelID.Items.FindByValue(IUser.UserLevelID.ToString()).Selected = true;
                }
                catch
                {
                }
                UserFalse.Items.FindByValue(IUser.UserFalse.ToString()).Selected = true;
                UserMaritalStatus.Items.FindByValue(IUser.UserMaritalStatus.ToString()).Selected = true;
                try
                {
                    UserSex.Items.FindByValue(IUser.UserSex.ToString()).Selected = true;
                }
                catch
                {
                }


                UserName.Text = IUser.UserName;
                UserNickName.Text = IUser.UserNickName;
                UserPassWord.Text = IUser.UserPassWord;
                UserPassWordAnswer.Text = IUser.UserPassWordAnswer;
                UserIdCard.Text = IUser.UserIdCard;
                UserEmail.Text = IUser.UserEmail;
                UserPoint.Text = IUser.UserPoint.ToString();
                UserPrestige.Text = IUser.UserPrestige.ToString();
                UserComeFrom.Text = IUser.UserComeFrom;
                UserMobile.Text = IUser.UserMobile;
                UserTrueName.Text = IUser.UserTrueName;
                UserSchool.Text = IUser.UserSchool;
                UserBirthday.Text = IUser.UserBirthday.ToString();
                if (IUser.UserRecommendUserID != 0)
                {
                    Components.Components.User recommUserInfo = new Components.Components.User();
                    //DataProviders.UserInfoDataProviders recommUserInfo = new DataProviders.UserInfoDataProviders();
                    dr = MyUser.SetUserInfo(IUser.UserRecommendUserID, true);
                    //recommUserInfo.SetUserInfo(userInfo.UserRecommendUserID, true);
                    if (dr!=null)
                    {
                        recommUserInfo.SetDataProviders(dr);
                        UserRecommendUser.Text = recommUserInfo.UserName;
                    }
                }
                UserFace.Text = IUser.UserFace;
                UserSign.Text = IUser.UserSign;
                UserAbout.Text = IUser.UserAbout;
                UserLoginNumber.Text = IUser.UserLoginNumber.ToString();
                UserRegTime.Text = IUser.UserRegTime.ToString();
                UserLastLoginTime.Text = IUser.UserLastLoginTime.ToString();
                UserGroup.Text = IUser.UserGroup.ToString();
                UserRole.Text = IUser.UserRole.ToString();
                UserExp.Text = IUser.UserExp.ToString();
                UserCP.Text = IUser.UserCP.ToString();
                UserMoney.Text = IUser.UserMoney.ToString();
                UserTrueMoney.Text = IUser.UserTrueMoney.ToString();
                UserTicket.Text = IUser.UserTicket.ToString();
                UserOICQ.Text = IUser.UserOICQ;
                UserPostNumber.Text = IUser.UserPostNumber.ToString();
                UserLastActTime.Text = IUser.UserLastActTime.ToString();
                UserTopicNumber.Text = IUser.UserTopicNumber.ToString();
                UserReTopicNumber.Text = IUser.UserReTopicNumber.ToString();
                UserOnlineTime.Text = IUser.UserOnlineTime.ToString();
                UserContactTel.Text = IUser.UserContactTel;
                UserCode.Text = IUser.UserCode;
                UserWebAddress.Text = IUser.UserWebAddress;
                UserWebLog.Text = IUser.UserWebLog;
                UserWebGallery.Text = IUser.UserWebGallery;
                UserWorkUnit.Text = IUser.UserWorkUnit;
                UserContactAddress.Text = IUser.UserContactAddress;
                UserInterests.Text = IUser.UserInterests;
            }
        }

        private void PostData()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.User IUser = new Components.Components.User();
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DataRow dr;
            //DataProviders.UserInfoDataProviders userInfo = new DataProviders.UserInfoDataProviders();
            if (LabelType1.Visible)
            {//�޸� 
                if (int.Parse(userID.Text) == 0)
                {
                    IDoNetBbs.WriteAlert("���û�������", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                dr = MyUser.SetUserInfo(int.Parse(userID.Text), false);
                //userInfo.SetUserInfo(int.Parse(userID.Text), false);
                if (dr==null)
                {
                    IDoNetBbs.WriteAlert("���û�������", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IUser.SetDataProviders(dr);
            }
            if (UserName.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("�������û�����", false);
                //IDoNetBbs.WriteJavaScript("JsUserInfoTable(1);", false);
                //IDoNetBbs.WriteFocus("UserName", false);//δ���
                Page.Response.End();
            }
            if (UserPassWord.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("�������û����룡", false);//δ���
                Response.End();
            }
            if (UserPassWordAnswer.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("�������û�����𰸣�", false);//δ���
                Response.End();
            }

            if (UserEmail.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("�������û��ʼ���ַ��", false);//δ���
                Response.End();
            }
            if (!IDoNetBbs.GetEmailFormat(UserEmail.Text.Trim()))
            {
                IDoNetBbs.WriteAlert("�û��ʼ���ַ��ʽ���ԣ�", false);//δ���
                Response.End();
            }
            //Components.Components.User falseUser = new Components.Components.User();
            //DataProviders.UserInfoDataProviders falseUserInfo = new DataProviders.UserInfoDataProviders();
            if (UserName.Text.Trim() != IUser.UserName)
            {
                dr = MyUser.SetUserInfo(UserName.Text.Trim(), false);
                if (dr!=null)
                {
                    IDoNetBbs.WriteAlert("���û������Ѿ��������û�����ע���ˣ�", false);//δ���
                    Response.End();
                }
            }
            if (UserEmail.Text.Trim() != IUser.UserEmail)
            {
                dr = MyUser.SetUserEmailInfo(UserEmail.Text.Trim(), false);
                if (dr != null)
                {
                    IDoNetBbs.WriteAlert("�õ����ʼ���ַ�������û�����ע���ˣ�", false);//δ���
                    Response.End();
                }
            }
            if (LabelType1.Visible)
            {//�޸�
                if (UserPassWord.Text != IUser.UserPassWord)
                {
                    IUser.UserPassWord = IDoNetBbs.GetPassword(UserPassWord.Text);
                }
                if (UserPassWordAnswer.Text != IUser.UserPassWordAnswer)
                {
                    IUser.UserPassWordAnswer = IDoNetBbs.GetPassword(UserPassWordAnswer.Text);
                }
            }
            else
            {
                IUser.UserPassWord = IDoNetBbs.GetPassword(UserPassWord.Text);
                IUser.UserPassWordAnswer = IDoNetBbs.GetPassword(UserPassWordAnswer.Text);
            }

            IUser.UserName = UserName.Text;
            if (UserNickName.Text.Trim() == string.Empty)
            {
                IUser.UserNickName = IUser.UserName;
            }
            else
            {
                IUser.UserNickName = UserNickName.Text;
            }
            IUser.UserIdCard = UserIdCard.Text;
            IUser.UserFalse = int.Parse(UserFalse.SelectedValue);
            IUser.UserPrivacy = int.Parse(UserPrivacy.SelectedValue);
            IUser.UserReceiveType = int.Parse(UserReceiveType.SelectedValue);
            IUser.UserEmail = UserEmail.Text;
            IUser.UserPoint = int.Parse(UserPoint.Text);
            IUser.UserPrestige = int.Parse(UserPrestige.Text);
            IUser.UserLevelID = int.Parse(UserLevelID.SelectedValue);
            IUser.UserOnLineStatic = UserOnLineStatic.SelectedValue;
            IUser.UserComeFrom = UserComeFrom.Text;
            IUser.UserMobile = UserMobile.Text;
            IUser.UserTrueName = UserTrueName.Text;
            IUser.UserSex = UserSex.SelectedValue;
            IUser.UserSchool = UserSchool.Text;
            IUser.UserBirthday = System.Convert.ToDateTime(UserBirthday.Text);
            IUser.UserRecommendUserID = 0;
            if (UserRecommendUser.Text.Trim() != string.Empty)
            {
                dr = MyUser.SetUserInfo(UserRecommendUser.Text.Trim(), false);
                if (dr != null)
                {
                    Components.Components.User recommendUser = new Components.Components.User();
                    IUser.UserRecommendUserID = recommendUser.UserID;
                }
            }
            IUser.UserMaritalStatus = int.Parse(UserMaritalStatus.SelectedValue);
            IUser.UserFace = UserFace.Text;
            IUser.UserSign = UserSign.Text;
            IUser.UserAbout = UserAbout.Text;
            IUser.UserLoginNumber = int.Parse(UserLoginNumber.Text);
            IUser.UserRegTime = System.Convert.ToDateTime(UserRegTime.Text);
            IUser.UserGroup = UserGroup.Text;
            //Components.Current.Users users = new Components.Current.Users();
            if (currentUser.IsSystemAdministrator)
            {
                IUser.UserRole = UserRole.Text;
            }
            else
            {
                if (LabelType2.Visible)
                {//���� 
                    IUser.UserRole = IDoNetBbs.GetConfiguration("WebSite_UserGroup");
                }
            }


            IUser.UserExp = int.Parse(UserExp.Text);
            IUser.UserCP = int.Parse(UserCP.Text);
            IUser.UserMoney = int.Parse(UserMoney.Text);
            IUser.UserTrueMoney = int.Parse(UserTrueMoney.Text);
            IUser.UserTicket = int.Parse(UserTicket.Text);
            IUser.UserOICQ = UserOICQ.Text;
            IUser.UserPostNumber = int.Parse(UserPostNumber.Text);
            IUser.UserLastActTime = System.Convert.ToDateTime(UserLastActTime.Text);
            IUser.UserLastLoginTime = System.Convert.ToDateTime(UserLastLoginTime.Text);
            IUser.UserTopicNumber = int.Parse(UserTopicNumber.Text);
            IUser.UserReTopicNumber = int.Parse(UserReTopicNumber.Text);
            IUser.UserOnlineTime = int.Parse(UserOnlineTime.Text);
            IUser.UserContactTel = UserContactTel.Text;
            IUser.UserCode = UserCode.Text;
            IUser.UserWebAddress = UserWebAddress.Text;
            IUser.UserWebLog = UserWebLog.Text;
            IUser.UserWebGallery = UserWebGallery.Text;
            IUser.UserWorkUnit = UserWorkUnit.Text;
            IUser.UserContactAddress = UserContactAddress.Text;
            IUser.UserInterests = UserInterests.Text;
            //Components.Current.Users users = new Components.Current.Users();
            IUser.UserLastIP = currentUser.UserIP;
            if (LabelType2.Visible)
            {//���� 
                MyUser.InsertUserInfo(IUser);
            }
            else
            {//�޸� 
                MyUser.UpdateUserInfo(IUser);
            }
            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>alert('�����ɹ�!');dialogArguments.window.location.reload();window.close();</script>");
            Response.End();
        }
    }
}
