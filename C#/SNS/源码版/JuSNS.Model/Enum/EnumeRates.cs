using System;
using System.Collections.Generic;
using System.Text;

namespace JuSNS.Model
{
    /// <summary>
    /// 信息状态
    /// </summary>
    public enum EnumCusState
    {
        /// <summary>
        /// 正常
        /// </summary>
        ForNormal = 0,
        /// <summary>
        /// 审核中
        /// </summary>
        ForLock = 1,
        /// <summary>
        /// 审核未通过
        /// </summary>
        ForUnPass = 2,
        /// <summary>
        /// 已停止
        /// </summary>
        ForStop = 3
    }

    /// <summary>
    /// 分享分类
    /// </summary>
    public enum EnumShareType
    {
        /// <summary>
        /// 新闻
        /// </summary>
        ForNews = 0,
        /// <summary>
        /// 日志
        /// </summary>
        ForBlog = 1,
        /// <summary>
        /// 社群
        /// </summary>
        ForGroup = 2,
        /// <summary>
        /// 问答
        /// </summary>
        ForAsk = 3,
        /// <summary>
        /// 活动
        /// </summary>
        ForActive = 4,
        /// <summary>
        /// 商品
        /// </summary>
        ForGoods = 5,
        /// <summary>
        /// 店铺
        /// </summary>
        ForShop = 6,
        /// <summary>
        /// 网页
        /// </summary>
        ForWeb = 7,
        /// <summary>
        /// Flash
        /// </summary>
        ForFlash = 8,
        /// <summary>
        /// 音乐
        /// </summary>
        ForMusic = 9,
        /// <summary>
        /// 视频
        /// </summary>
        ForVodie = 10,
        /// <summary>
        /// 相册
        /// </summary>
        ForAlbum = 11,
        /// <summary>
        /// 相片
        /// </summary>
        ForPhoto = 12,
        /// <summary>
        /// 投票
        /// </summary>
        ForVote = 13,
        /// <summary>
        /// 帖子
        /// </summary>
        ForTopic = 14,
        /// <summary>
        /// 好友
        /// </summary>
        ForFriend = 15,
        /// <summary>
        /// 团购
        /// </summary>
        ForMulte = 16,
        /// <summary>
        /// 其他
        /// </summary>
        ForOther = 17
    }

    /// <summary>
    /// 动态类型
    /// </summary>
    public enum EnumDynType
    {
        /// <summary>
        /// 微博
        /// </summary>
        CreatTwitter = 0,
        /// <summary>
        /// 微博评论
        /// </summary>
        TwitterComment = 1,
        /// <summary>
        /// 日志
        /// </summary>
        CreatBlog = 2,
        /// <summary>
        /// 日志评论
        /// </summary>
        BlogComment = 3,
        /// <summary>
        /// 创建相册
        /// </summary>
        CreatAlbum = 4,
        /// <summary>
        /// 上传图片
        /// </summary>
        CreatPhoto = 5,
        /// <summary>
        /// 图片评论
        /// </summary>
        PhotoComment = 6,
        /// <summary>
        /// 创建社群
        /// </summary>
        CreatGroup = 7,
        /// <summary>
        /// 发帖
        /// </summary>
        CreatTopic = 8,
        /// <summary>
        /// 回复帖子
        /// </summary>
        ReplyTopic = 9,
        /// <summary>
        /// 分享
        /// </summary>
        CreatShare = 10,
        /// <summary>
        /// 发布新闻
        /// </summary>
        CreatNews = 11,
        /// <summary>
        /// 新闻评论
        /// </summary>
        NewsComment = 12,
        /// <summary>
        /// 发布商品
        /// </summary>
        CreatGoods = 13,
        /// <summary>
        /// 商品评论
        /// </summary>
        GoodsComment = 14,
        /// <summary>
        /// 入住店铺
        /// </summary>
        CreatShop = 15,
        /// <summary>
        /// 发起团购
        /// </summary>
        CreatMulte = 16,
        /// <summary>
        /// 团购评论
        /// </summary>
        MulteComment = 17,
        /// <summary>
        /// 加入了团购
        /// </summary>
        JoinMulte = 18,
        /// <summary>
        /// 发起活动
        /// </summary>
        CreatActive = 19,
        /// <summary>
        /// 活动评论
        /// </summary>
        ActiveComment = 20,
        /// <summary>
        /// 参加活动
        /// </summary>
        JoinActive = 21,
        /// <summary>
        /// 活动上传图片
        /// </summary>
        ActiveCreatPhoto = 22,
        /// <summary>
        /// 发起提问
        /// </summary>
        CreatAsk = 23,
        /// <summary>
        /// 回答提问
        /// </summary>
        AskComment = 24,
        /// <summary>
        /// 赠送礼物
        /// </summary>
        CreatGift = 25,
        /// <summary>
        /// 打招呼
        /// </summary>
        CreatPoke = 26,
        /// <summary>
        /// 创建投票
        /// </summary>
        CreatVote = 27,
        /// <summary>
        /// 参与投票
        /// </summary>
        JoinVote = 28,
        /// <summary>
        /// 添加了收藏
        /// </summary>
        CreatFaviote = 29,
        /// <summary>
        /// 邀请加入了网站
        /// </summary>
        InviteJoinSite = 30,
        /// <summary>
        /// 更新了头像
        /// </summary>
        UpdateHeadPic = 31,
        /// <summary>
        /// 验证了电子邮件
        /// </summary>
        VerfiyEmail = 32,
        /// <summary>
        /// 创建了APP
        /// </summary>
        CreatAPP = 33,
        /// <summary>
        /// 安装了APP
        /// </summary>
        SetUpAPP = 34,
        /// <summary>
        /// 加入了社群
        /// </summary>
        JoinGroup = 35,
        /// <summary>
        /// 成为了好友
        /// </summary>
        Friend = 36,
        /// <summary>
        /// 活动评论
        /// </summary>
        TopicComment = 37,
        /// <summary>
        /// 设置为最佳答案
        /// </summary>
        AskBest = 38,
        /// <summary>
        /// 更新个人基本资料
        /// </summary>
        UpdateBasic = 39,
        /// <summary>
        /// 更换了模板
        /// </summary>
        UpdateTempalte = 40
    }
    /// <summary>
    /// 通知类型
    /// </summary>
    public enum EnumNoticeType
    {
        /// <summary>
        /// 微博
        /// </summary>
        ReplyTwitter = 0,
        /// <summary>
        /// 日志
        /// </summary>
        ReplyBlog = 1,
        /// <summary>
        /// 图片
        /// </summary>
        ReplyPhoto = 2,
        /// <summary>
        /// 申请加入群
        /// </summary>
        JoinGroup = 3,
        /// <summary>
        /// 回帖
        /// </summary>
        ReplyTopic = 4,
        /// <summary>
        /// 新闻
        /// </summary>
        ReplyNews = 5,
        /// <summary>
        /// 商品
        /// </summary>
        ReplyGoods = 6,
        /// <summary>
        /// 针对商品发起了团购
        /// </summary>
        PulishMulte = 7,
        /// <summary>
        /// 加入团购
        /// </summary>
        JoinMulte = 8,
        /// <summary>
        /// 团购评论
        /// </summary>
        ReplyMulte = 9,
        /// <summary>
        /// 加入活动
        /// </summary>
        JoinActive = 10,
        /// <summary>
        /// 活动留言
        /// </summary>
        ReplyActive = 11,
        /// <summary>
        /// 活动上传图片
        /// </summary>
        ActiveCreatPhoto = 12,
        /// <summary>
        /// 回答提问
        /// </summary>
        ReplyAsk = 13,
        /// <summary>
        /// 赠送礼物
        /// </summary>
        CreatGift = 14,
        /// <summary>
        /// 打招呼
        /// </summary>
        CreatPoke = 15,
        /// <summary>
        /// 参与投票
        /// </summary>
        JoinVote = 16,
        /// <summary>
        /// 邀请加入了网站
        /// </summary>
        InviteJoinSite = 17,
        /// <summary>
        /// 短消息
        /// </summary>
        SendBox = 18,
        /// <summary>
        /// 留言
        /// </summary>
        SendBook = 19,
        /// <summary>
        /// 设置问答为最佳答案
        /// </summary>
        SetAskBest = 20,
        /// <summary>
        /// Vip到期
        /// </summary>
        VipTimeout = 21,
        /// <summary>
        /// 成为好友
        /// </summary>
        Friend = 22
    }

    /// <summary>
    /// 注册反回情况
    /// </summary>
    public enum EnumRegister
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0,
        /// <summary>
        /// 注册成功，但未通过电子邮件验证
        /// </summary>
        SucceedNotMail = 4,
        /// <summary>
        /// Email已存在
        /// </summary>
        EmailRepeat = 1,
        /// <summary>
        /// 已接受邀请而注册
        /// </summary>
        RegInviteCode = 2,
        /// <summary>
        /// 无效的验证码
        /// </summary>
        InvalidCode = 3,
        /// <summary>
        /// 其他错误
        /// </summary>
        UnexpectedError = 5,
        /// <summary>
        /// 用户注册写入数据库后,可能Email未能发送成功
        /// </summary>
        EmailMayNotSend = 6
    }

    /// <summary>
    /// 会员状态
    /// </summary>
    public enum EnumUserState
    {
        /// <summary>
        /// 刚刚注册
        ///</summary>
        Register = 0,
        /// <summary>
        /// 用户被锁定
        /// </summary>
        Lock = 1,
        /// <summary>
        /// 状态正常，但未通过电子邮件验证
        /// </summary>
        NormalNotEmail = 2,
        /// <summary>
        /// 状态正常，但未捆绑手机
        /// </summary>
        NormalNotMoblie = 3,
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 5
    }


    /// <summary>
    /// 好友邀请方式
    /// </summary>
    public enum EnumFriendInviteMode
    {
        /// <summary>
        /// email邀请
        /// </summary>
        Email = 1,
        /// <summary>
        /// ulr邀请
        /// </summary>
        URL = 2
    }

    /// <summary>
    /// 用户登录状态
    /// </summary>
    public enum EnumLoginState
    {
        /// <summary>
        /// 超时或未登录
        /// </summary>
        Err_TimeOut,
        /// <summary>
        /// 管理员超时或未登陆
        /// </summary>
        Err_AdminTimeOut,
        /// <summary>
        /// 管理员被锁定
        /// </summary>
        Err_AdminLocked,
        /// <summary>
        /// 用户被锁定
        /// </summary>
        Err_Locked,
        /// <summary>
        /// 用户编号不存在
        /// </summary>
        Err_UserInexistent,
        /// <summary>
        /// 用户IP被限制
        /// </summary>
        Err_IPLimited,
        /// <summary>
        /// 用户权限不足
        /// </summary>
        Err_NoAuthority,
        /// <summary>
        /// 发生数据库异常
        /// </summary>
        Err_DbException,
        /// <summary>
        /// CookesERR
        /// </summary>
        Err_CookesERR,
        /// <summary>
        /// 登录状态正常
        /// </summary>
        Succeed,
        /// <summary>
        /// 帐号未(通过电子邮件)激活
        /// </summary>
        Err_UnActivation,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        Err_NameOrPwdError,
        /// <summary>
        /// 非管理员
        /// </summary>
        Err_NotAdmin,
        /// <summary>
        /// 连续登录错误锁定
        /// </summary>
        Err_DurativeLogError,
        /// <summary>
        /// 会员组超期
        /// </summary>
        Err_GroupExpire,
        /// <summary>
        /// ip发生了变化
        /// </summary>
        Err_IPChange
    }

    /// <summary>
    /// 邮件通知设置类型
    /// </summary>
    public enum EnumEmailSet
    {
        /// <summary>
        /// 新私信
        /// </summary>
        NewLetter = 1,
        /// <summary>
        /// 好友请求
        /// </summary>
        FriendRequest = 2,
        /// <summary>
        /// 新留言
        /// </summary>
        NewWord = 3,
        /// <summary>
        /// 打招呼
        /// </summary>
        Greet = 4,
        /// <summary>
        /// 好友描述请求
        /// </summary>
        FriendDescription = 5,
        /// <summary>
        /// 接受你的邀请加入了NetSNS
        /// </summary>
        AcceptRequest = 6,
        /// <summary>
        /// 在照片中被圈
        /// </summary>
        PhotoBeQ = 7,
        /// <summary>
        /// 收到照片圈人请求
        /// </summary>
        PhotoRequest = 8,
        /// <summary>
        /// 照片新评论
        /// </summary>
        PhotoComments = 9,
        /// <summary>
        /// 你的照片评论有新回复
        /// </summary>
        PhotoResponse = 10,
        /// <summary>
        /// 群组邀请
        /// </summary>
        GroupInvite = 11,
        /// <summary>
        /// 成为群组管理员
        /// </summary>
        ChanageGroupManager = 12,
        /// <summary>
        /// 群组成员申请
        /// </summary>
        GroupJoin = 13,
        /// <summary>
        /// 主题新回复
        /// </summary>
        Response = 14,
        /// <summary>
        /// 日志新评论
        /// </summary>
        LogComments = 15,
        /// <summary>
        /// 你的日志评论有新回复
        /// </summary>
        LogResponse = 16,
        /// <summary>
        /// 班级新留言
        /// </summary>
        ClassNewWord = 17,
        /// <summary>
        /// 班级新同学
        /// </summary>
        ClassNewStudent = 18,
        /// <summary>
        /// 分享新评论
        /// </summary>
        ShareComments = 19,
        /// <summary>
        /// 你的分享评论有新回复
        /// </summary>
        ShareResponse = 20
    }

    /// <summary>
    /// 用户的隐私(公开程度)
    /// </summary>
    public enum EnumPrivacy
    {
        /// <summary>
        /// 全站可见,完全开放
        /// </summary>
        ForWholeSite = 0,
        ///// <summary>
        ///// 网络和好友可见
        ///// </summary>
        //ForNetWorkAndFriends = 1,
        /// <summary>
        /// 只有好友可见
        /// </summary>
        ForFriends = 1,
        /// <summary>
        /// 仅用户本人
        /// </summary>
        ForOwner = 2
    }

    /// <summary>
    /// 请求类型
    /// </summary>
    public enum EnumPublics
    {
        /// <summary>
        /// 不需要审核
        /// </summary>
        NoCheck = 0,
        /// <summary>
        /// 需要审核
        /// </summary>
        Checked = 1,
        /// <summary>
        /// 拒绝加入
        /// </summary>
        None = 2
    }

    /// <summary>
    /// 好友描述类型
    /// </summary>
    public enum EnumFriendDetailType
    {
        /// <summary>
        /// 同学
        /// </summary>
        Schoolmate = 1,
        /// <summary>
        /// 室友
        /// </summary>
        Chum = 2,
        /// <summary>
        /// 社团/协会
        /// </summary>
        Consortium = 3,
        /// <summary>
        /// 聚会
        /// </summary>
        Forgather = 4,
        /// <summary>
        /// 朋友
        /// </summary>
        Friend = 5,
        /// <summary>
        /// 家人/亲戚
        /// </summary>
        Folk = 6,
        /// <summary>
        /// 同事
        /// </summary>
        Colleague = 7,
        /// <summary>
        /// 商务合作
        /// </summary>
        Business = 8,
        /// <summary>
        /// 本站
        /// </summary>
        Us = 9,
        /// <summary>
        /// 其他网站
        /// </summary>
        OtherWebSite = 10,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 11
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum EnumMoudle
    {
        /// <summary>
        /// 相册
        /// </summary>
        Album,
        /// <summary>
        /// 群组
        /// </summary>
        Group,
        /// <summary>
        /// 分享
        /// </summary>
        Share
    }

    /// <summary>
    /// 学习阶段
    /// </summary>
    public enum EnumStudyGrade
    {
        /// <summary>
        /// 初中
        /// </summary>
        JuniorHigh = 1,
        /// <summary>
        /// 高中
        /// </summary>
        SeniorHigh = 2,
        /// <summary>
        /// 大学
        /// </summary>
        College = 3,
        /// <summary>
        /// 研究生
        /// </summary>
        Master = 4,
        /// <summary>
        /// 博士
        /// </summary>
        Doctor = 5

    }
}
