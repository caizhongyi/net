using System;

namespace JuSNS.Model
{

  public class UserInfo
  {
     private Int32 _UserID;
     private String _Email;
     private String _UserName;
     private String _Mobile;
     private String _Password;
     private String _TrueName;
     private Byte _Sex;
     private Byte _Marriage;
     private Boolean _BindMoblie;
     private Byte _State;
     private DateTime _LastLoginTime;
     private String _LastLoginIP;
     private Int32 _LoginTimes;
     private Int32 _City;
     private Int32 _ProvinceID;
     private DateTime _RegTime;
     private String _RegIP;
     private Int32 _Portrait;
     private Int32 _InviterID;
     private String _VerifyCode;
     private DateTime _ConfirmTime;
     private Byte _isRec;
     private Int32 _integral;
     private Int32 _inteyb;
     private Int32 _Click;
     private Byte _isAdmin;
     private Int32 _AttNumber;
     private Int32 _MobileCode;
     private Double _Money;
     private Boolean _IsVip;
     private Int32 _MemberLevels;
      
     public UserInfo()
     {}

     public UserInfo(Int32 userID, String email, String userName, String mobile, String password, String trueName, Byte sex, Byte marriage, Boolean bindMoblie, Byte state, DateTime lastLoginTime, String lastLoginIP, Int32 loginTimes, Int32 provinceID, Int32 city, DateTime regTime, String regIP, Int32 portrait, Int32 inviterID, String verifyCode, DateTime confirmTime, Byte isRec, Int32 integral, Int32 inteyb, Int32 click, Byte isAdmin, Int32 attNumber, Int32 mobileCode, Boolean isVip, Double money, Int32 memberLevels)
     {
         this._UserID = userID;
         this._Email = email;
         this._UserName = userName;
         this._Mobile = mobile;
         this._Password = password;
         this._TrueName = trueName;
         this._Sex = sex;
         this._Marriage = marriage;
         this._BindMoblie = bindMoblie;
         this._State = state;
         this._LastLoginTime = lastLoginTime;
         this._LastLoginIP = lastLoginIP;
         this._LoginTimes = loginTimes;
         this._City = city;
         this._ProvinceID = provinceID;
         this._RegTime = regTime;
         this._RegIP = regIP;
         this._Portrait = portrait;
         this._InviterID = inviterID;
         this._VerifyCode = verifyCode;
         this._ConfirmTime = confirmTime;
         this._isRec = isRec;
         this._integral = integral;
         this._inteyb = inteyb;
         this._Click = click;
         this._isAdmin = isAdmin;
         this._AttNumber = attNumber;
         this._MobileCode = mobileCode;
         this._IsVip = isVip;
         this._Money = money;
         this._MemberLevels = memberLevels;
     }


     public Int32 UserID
     {
         get { return this._UserID; }
         set { this._UserID = value; }
     }

     public Double Money
     {
         get { return this._Money; }
         set { this._Money = value; }
     }

     public String Email
     {
         get{return this._Email;}
         set{this._Email = value;}
     }

     public String UserName
     {
         get{return this._UserName;}
         set{this._UserName = value;}
     }

     public String Mobile
     {
         get{return this._Mobile;}
         set{this._Mobile = value;}
     }

     public String Password
     {
         get{return this._Password;}
         set{this._Password = value;}
     }

     public String TrueName
     {
         get{return this._TrueName;}
         set{this._TrueName = value;}
     }

     public Byte Sex
     {
         get{return this._Sex;}
         set{this._Sex = value;}
     }

     public Byte Marriage
     {
         get{return this._Marriage;}
         set{this._Marriage = value;}
     }

     public Boolean BindMoblie
     {
         get{return this._BindMoblie;}
         set{this._BindMoblie = value;}
     }

     public Byte State
     {
         get{return this._State;}
         set{this._State = value;}
     }

     public DateTime LastLoginTime
     {
         get{return this._LastLoginTime;}
         set{this._LastLoginTime = value;}
     }

     public String LastLoginIP
     {
         get{return this._LastLoginIP;}
         set{this._LastLoginIP = value;}
     }

     public Int32 LoginTimes
     {
         get{return this._LoginTimes;}
         set{this._LoginTimes = value;}
     }

     public Int32 MemberLevels
     {
         get { return this._MemberLevels; }
         set { this._MemberLevels = value; }
     }

     public Int32 ProvinceID
     {
         get { return this._ProvinceID; }
         set { this._ProvinceID = value; }
     }

     public Int32 City
     {
         get{return this._City;}
         set{this._City = value;}
     }

      

     public DateTime RegTime
     {
         get{return this._RegTime;}
         set{this._RegTime = value;}
     }

     public String RegIP
     {
         get{return this._RegIP;}
         set{this._RegIP = value;}
     }

     public Int32 Portrait
     {
         get{return this._Portrait;}
         set{this._Portrait = value;}
     }

     public Int32 InviterID
     {
         get{return this._InviterID;}
         set{this._InviterID = value;}
     }

     public String VerifyCode
     {
         get{return this._VerifyCode;}
         set{this._VerifyCode = value;}
     }

     public DateTime ConfirmTime
     {
         get{return this._ConfirmTime;}
         set{this._ConfirmTime = value;}
     }

     public Byte IsRec
     {
         get{return this._isRec;}
         set{this._isRec = value;}
     }

     public Int32 Integral
     {
         get{return this._integral;}
         set{this._integral = value;}
     }

     public Int32 Inteyb
     {
         get{return this._inteyb;}
         set{this._inteyb = value;}
     }

     public Int32 Click
     {
         get{return this._Click;}
         set{this._Click = value;}
     }

     public Byte IsAdmin
     {
         get{return this._isAdmin;}
         set{this._isAdmin = value;}
     }

     public Int32 AttNumber
     {
         get{return this._AttNumber;}
         set{this._AttNumber = value;}
     }

     public Int32 MobileCode
     {
         get{return this._MobileCode;}
         set{this._MobileCode = value;}
     }

     public Boolean IsVip
     {
         get{return this._IsVip;}
         set{this._IsVip = value;}
     }
  }
}