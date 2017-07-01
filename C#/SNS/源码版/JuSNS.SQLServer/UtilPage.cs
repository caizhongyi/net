using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using JuSNS.Model;
using JuSNS.Factory;
using JuSNS.Profile;

namespace JuSNS.SQLServer
{
    public class UtilPage : DbBase, IUtilPage
    {
        #region 前台部分
        /// <summary>
        ///教育信息
        /// </summary>
        protected string[] user_edu_aspx = { "id", "ID, UserID, schoolName,leaveyear, PostTime,levels", "NT_UserEducation where UserID=@UserID", "order by id asc" };
        /// <summary>
        /// 工作信息
        /// </summary>
        protected string[] user_career_aspx = { "id", "ID, UserID, Company, JoinTime, PostTime, LeaveTime", "NT_UserCareer where UserID=@UserID", "order by id asc" };

        /// <summary>
        /// 得到好友列表
        /// </summary>
        protected string[] user_friend_aspx = { "a.id", " a.ID, a.FriendID, b.UserName, b.TrueName", "NT_Friend AS a INNER JOIN  NT_User AS b ON b.UserID = a.FriendID WHERE  (a.UserID = @UserID) and a.state=0", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };
        protected string[] user_friend_class_aspx = { "a.id", " a.ID, a.FriendID, b.UserName, b.TrueName", "NT_Friend AS a INNER JOIN  NT_User AS b ON b.UserID = a.FriendID WHERE  (a.UserID = @UserID and a.ClassID=@ClassID) and a.state=0", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };
        protected string[] user_friend_key_aspx = { "a.id", " a.ID, a.FriendID, b.UserName, b.TrueName", "NT_Friend AS a INNER JOIN  NT_User AS b ON b.UserID = a.FriendID WHERE  (a.UserID = @UserID) and a.state=0 and (b.UserName like @KWD or b.TrueName like @KWD)", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };

        /// <summary>
        /// 来访者/我去看过
        /// </summary>
        protected string[] user_visited_aspx = { "a.id", " a.ID, a.LastVisitTime, a.VisitorID,a.UserID, a.VisitTimes, b.TrueName", "NT_Visit AS a INNER JOIN NT_User AS b ON a.VisitorID = b.UserID where a.UserID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.LastVisitTime DESC, a.ID DESC" };
        protected string[] user_visited_my_aspx = { "a.id", " a.ID, a.LastVisitTime, a.VisitorID,a.UserID, a.VisitTimes, b.TrueName,b.UserID", "NT_Visit AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.VisitorID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.LastVisitTime DESC, a.ID DESC" };

        /// <summary>
        /// 我关注/被关注
        /// </summary>
        protected string[] user_ATT_my_aspx = { "a.id", " a.ID, a.UserID, a.atterid, b.TrueName", "NT_att AS a INNER JOIN NT_User AS b ON a.atterID = b.UserID where a.UserID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };
        protected string[] user_ATT_aspx = { "a.id", " a.ID, a.UserID, a.atterid, b.TrueName", "NT_att AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.atterID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };
        /// <summary>
        /// 充值记录
        /// </summary>
        protected string[] user_Charges_aspx = { "id", " id, Money, point, gpoint, orderNumber, UserID, IsSucces, CreatTime, PostIP", "NT_ChargeOrder where UserID=@UserID and IsSucces=@Succ", "order by ID DESC" };
        protected string[] user_Charge_aspx = { "id", " id, Money, point, gpoint, orderNumber, UserID, IsSucces, CreatTime, PostIP", "NT_ChargeOrder where UserID=@UserID", "order by ID DESC" };
       
        protected string[] manager_Charges_aspx = { "id", " id, Money, point, gpoint, orderNumber, UserID, IsSucces, CreatTime, PostIP", "NT_ChargeOrder where IsSucces=@Succ", "order by ID DESC" };
        protected string[] manager_Charge_aspx = { "id", " id, Money, point, gpoint, orderNumber, UserID, IsSucces, CreatTime, PostIP", "NT_ChargeOrder where 1=1", "order by ID DESC" };

        /// <summary>
        /// 道具列表
        /// </summary>
        protected string[] user_magic_aspx = { "id", " id, mName, pic, point, gpoint, number, buynumber, mdesc, mType, CreatTime, state, vTime", "NT_magic where 1=1", "order by ID DESC" };
        protected string[] user_magic_hot_aspx = { "id", " id, mName, pic, point, gpoint, number, buynumber, mdesc, mType, CreatTime, state, vTime", "NT_magic where 1=1", "order by buynumber DESC" };
        protected string[] user_magic_my_aspx = { "id", "id, UserID, MID, PostTime, IsUse, SendUserID, IsUserTime, Number", "NT_MagicInfo where UserID=@UserID", "order by ID DESC" };
        protected string[] user_magic_record_aspx = { "id", "ID, UserID, mID, Num, MDesc, mType, PostTime", "NT_MagicLogs where UserID=@UserID and mType=@R", "order by ID DESC" };

        /// <summary>
        /// 邀请记录
        /// </summary>
        protected string[] user_InviteRecord_aspx = { "id", "ID, UserID, email, Reply, PostTime, PostIP, ReplyTime, ReplyIP, ValidCode, RegUserID", "NT_Friendinvite where UserID=@UserID and Reply=@Succ", "order by ID DESC" };

        //微博
        protected string[] user_twitter_comment_aspx = { "a.id", "a.ID, a.TID, a.UserID, a.[Content], a.PostTime,a.CommentID, b.TrueName", " NT_TwitterComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.tid = @TID) AND (a.IsLock = 0) and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };

        protected string[] user_blog_comment_aspx = { "a.id", "a.ID, a.BlogID, a.UserID, a.[Content], a.PostTime,a.commid, b.TrueName", " NT_Blogcomment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.BlogID = @blogID) AND (a.IsLock = 0) and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        protected string[] user_blog_visit_aspx = { "a.id", " b.Title, b.[Content], b.PostTime, b.attnumber, b.ShareNumber,b.userid, b.Comments,b.click, b.sysClassID, b.ID, b.PicPath,b.isrec,c.TrueName", " NT_Blogfoot AS a INNER JOIN  NT_Blog AS b ON a.BlogID = b.ID INNER JOIN NT_User AS c ON c.UserID = b.UserID and a.UserID=@UserID and c.state=" + (byte)EnumUserState.Lock + "", "order by a.CreatTime DESC,a.id DESC" };

        protected string[] user_goods_comment_aspx = { "a.id", "a.ID, a.PID, a.UserID, a.[Content], a.PostTime,a.commid, b.TrueName", " NT_ShopComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.PID = @GID) AND (a.IsLock = 0) AND a.cType=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        protected string[] user_shop_comment_aspx = { "a.id", "a.ID, a.PID, a.UserID, a.[Content], a.PostTime,a.commid, b.TrueName", " NT_ShopComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.PID = @GID) AND (a.IsLock = 0) AND a.cType=1 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        protected string[] user_multe_comment_aspx = { "a.id", "a.ID, a.PID, a.UserID, a.[Content], a.PostTime,a.commid, b.TrueName", " NT_ShopComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.PID = @MID) AND (a.IsLock = 0) AND a.cType=2 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        protected string[] user_multe_join_aspx = { "a.userid", "a.userID, a.mID, a.tel,a.posttime,b.TrueName", " NT_ShopMulteMember AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.mid = @MID)  and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.userid DESC" };
        //资讯
        protected string[] user_news_comment_aspx = { "a.id", "a.ID, a.NewsID, a.UserID, a.[Content], a.PostTime,a.parentid, b.TrueName", " NT_Newscomment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.NewsID = @NewsID) AND (a.IsLock = 0) and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        /// <summary>
        /// 获得指定相册的图片
        /// </summary>
        protected string[] user_albumsingle_aspx = { "id", "id, AlbumID, UserID, Description, FilePath", " NT_Photo where AlbumID=@AlbumID and islock=0", "order by PostTime DESC,Id DESC" };
        protected string[] user_albumhead_aspx = { "id", "id, AlbumID, UserID, Description, FilePath", " NT_Photo where AlbumID=0 and UserID=@UserID and islock=0", "order by PostTime DESC,Id DESC" };
     
        /// <summary>
        /// 图片评论
        /// </summary>
        protected string[] user_photo_comment_aspx = { "a.id", "a.ID, a.PhotoID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.CommentID,a.userid, b.TrueName", " NT_PhotoComment  AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.PhotoID = @PhotoID) AND (a.IsLock = 0) and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };

        protected string[] user_photo_new_aspx = { "a.id", "a.id,a.AlbumID, a.Description, a.Comments, a.PostTime,a.FilePath,a.userid, b.TrueName", "NT_Photo AS a INNER JOIN  NT_User AS b ON a.UserID = b.UserID where (a.IsLock = 0) AND (a.PhotoType = 1) and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.PostTime DESC, a.id DESC" };
        /// <summary>
        /// 帖子分页
        /// </summary>
        protected string[] user_topics_page_aspx = { "a.id", "a.id, a.title, a.posttime, a.UserID, a.[content], a.PostIP, a.FoolNumber, b.TrueName", "NT_GroupTopic AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where (a.IsLock = 0) AND (a.TopicID = @TopicID) and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.PostTime asc, a.id asc" };
        
        /// <summary>
        /// 群成员
        /// </summary>
        protected string[] user_group_member_aspx = { "a.id", " a.ID, a.UserID,a.grade, b.TrueName", "NT_GroupMember AS a INNER JOIN  NT_User AS b ON b.UserID = a.userid WHERE  (a.groupid = @GroupID) and a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };
        protected string[] user_group_membercheck_aspx = { "a.id", " a.ID, a.UserID,a.grade, b.TrueName", "NT_GroupMember AS a INNER JOIN  NT_User AS b ON b.UserID = a.userid WHERE  (a.groupid = @GroupID) and a.islock=1 and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY b.LastLoginTime DESC, a.ID DESC" };
        protected string[] user_groupalbum_aspx = { "a.albumid", " a.AlbumID, a.UserID, a.Title, a.Description, a.ImagesCount, a.CreateTime, a.Privacy, a.LastUploadTime, a.GroupID, b.TrueName", "NT_Album AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.groupid = @GroupID) and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.CreateTime DESC, a.albumid DESC" };
        protected string[] user_groupfiles_aspx = { "a.id", "a.id,a.UserID, a.title, a.GroupID, a.FileName, a.FileSize, a.PostIP, a.PostTime, a.DownNumber, b.TrueName", "NT_Files AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE  (a.groupid = @GroupID) and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.posttime DESC, a.id DESC" };
        protected string[] user_groupalltopic_aspx = { "a.id", "a.id,a.groupid, a.UserID, a.title, a.posttime, a.TopicID,a.lastpostTime, a.Replynumber, a.Clicks,a.isrec, b.TrueName,c.groupname", "NT_GroupTopic AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID inner Join nt_group as c on a.groupid=c.id where a.Islock=0 and a.topicid=0 and LEN(a.title) >5 and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.posttime DESC, a.id DESC" };
        protected string[] user_askdanan_aspx = { "a.id", "a.id, a.[Content], a.PostTime, a.UserID,a.isBest, b.TrueName", " NT_Ask AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE a.islock=0 and a.parentid=@Aid and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.isbest desc,a.posttime asc, a.id asc" };
        /// <summary>
        /// 活动留言
        /// </summary>
        protected string[] user_ativebook_aspx = { "a.id", "a.id, a.[Content],a.ativeid,a.CommentID, a.PostTime, a.UserID, b.TrueName", " NT_Ativecomment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE a.ativeID=@Aid and a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.posttime desc, a.id desc" };
        /// <summary>
        /// 购物车(所有)
        /// </summary>
        protected string[] user_ordercat_aspx = { "a.id", "a.id, a.GoodsID, a.UserID, a.OrderNumber, a.PostTime, a.IsLock,a.ispost,a.IsRevice, a.PostIP, a.Money, a.GPoint, b.GoodsName,b.userid as uid", " NT_ShopOrder AS a INNER JOIN NT_ShopGoods AS b ON a.GoodsID = b.id WHERE a.UserID=@UserID", "ORDER BY a.posttime desc, a.id desc" };
        /// <summary>
        /// 购物车(状态)
        /// </summary>
        protected string[] user_ordercats_aspx = { "a.id", "a.id, a.GoodsID, a.UserID, a.OrderNumber, a.PostTime, a.IsLock,a.ispost,a.IsRevice, a.PostIP, a.Money, a.GPoint, b.GoodsName,b.userid as uid", " NT_ShopOrder AS a INNER JOIN NT_ShopGoods AS b ON a.GoodsID = b.id WHERE a.UserID=@UserID and a.IsLock=@IsLock", "ORDER BY a.posttime desc, a.id desc" };
        /// <summary>
        /// 销售记录
        /// </summary>
        protected string[] user_ordersell_aspx = { "a.id", "a.id, a.GoodsID, a.UserID, a.OrderNumber, a.PostTime, a.IsLock,a.ispost,a.IsRevice, a.PostIP, a.Money, a.GPoint, b.GoodsName", " NT_ShopOrder AS a INNER JOIN NT_ShopGoods AS b ON a.GoodsID = b.id WHERE b.UserID=@UserID and a.IsLock=" + (byte)EnumCusState.ForNormal + "", "ORDER BY a.posttime desc, a.id desc" };

        protected string[] user_shopcomments_aspx = { "a.id", "a.id, a.GoodsID, a.Sore, a.UserID, a.[Content], a.PostTime, a.PostIP, a.CommentID,b.trueName", " NT_ShopUserComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID WHERE a.GoodsID=@GID and a.CommentID=0 and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.posttime desc, a.id desc" };

        protected string[] user_shopnews_aspx = { "a.id", "a.id, a.ShopID, a.Title, a.[Content], a.creatTime, a.islock, a.click, b.ShopName", " NT_ShopNews AS a INNER JOIN  NT_Shop AS b ON a.ShopID = b.id where a.ShopID=@ShopID", "ORDER BY a.creatTime desc, a.id desc" };
        protected string[] user_shopmulte_aspx = { "a.id", "a.id, a.UserID, a.Title, a.joinmember, a.MinMember, a.MaxMember, a.StartTime, a.EndTime, a.Price, a.PostTime, a.Pic, a.IsRec, c.id AS shopid", "  NT_ShopMultebuy AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN NT_Shop AS c ON c.UserID = b.UserID where c.id=@ShopID and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.PostTime desc, a.id desc" };
        protected string[] user_shopgoods_aspx = { "a.id", "a.goodsname,a.id,a.mPrice,a.pic", "  NT_ShopGoods AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN NT_Shop AS c ON c.UserID = b.UserID where c.id=@ShopID and b.state<>" + (byte)EnumUserState.Lock + "", "ORDER BY a.PostTime desc, a.id desc" };

        /// <summary>
        /// 礼物信息
        /// </summary>
        protected string[] user_giftall_aspx = { "id", "id, GiftName, Pic, GPoint, Point, PostTime, SendNumber, [Content], ClassID, IsAd", "NT_Gift where IsLock=" + (byte)EnumCusState.ForNormal + "", "order by id desc" };
        protected string[] user_giftclass_aspx = { "id", "id, GiftName, Pic, GPoint, Point, PostTime, SendNumber, [Content], ClassID, IsAd", "NT_Gift where IsLock=" + (byte)EnumCusState.ForNormal + " and ClassID=@ClassID", "order by id desc" };
        protected string[] user_giftall1_aspx = { "id", "id, GiftName, Pic, GPoint, Point, PostTime, SendNumber, [Content], ClassID, IsAd", "NT_Gift where 1=1", "order by id desc" };
        protected string[] user_giftclass1_aspx = { "id", "id, GiftName, Pic, GPoint, Point, PostTime, SendNumber, [Content], ClassID, IsAd", "NT_Gift where ClassID=@ClassID", "order by id desc" };
        protected string[] user_giftmy_aspx = { "a.id", "a.id, a.giftID, a.UserID, a.ReviceID, a.PostTime, a.[Content],b.pic, b.GiftName,b.content as giftcontent, c.TrueName", "NT_GiftUser AS a INNER JOIN NT_Gift AS b ON a.giftID = b.id INNER JOIN  NT_User AS c ON c.UserID = a.UserID where a.ReviceID=@UserID and c.state<>" + (byte)EnumUserState.Lock + "", "order by a.id desc" };
        protected string[] user_giftsend_aspx = { "a.id", "a.id, a.giftID, a.UserID, a.ReviceID, a.PostTime, a.[Content],b.pic, b.GiftName,b.content as giftcontent, c.TrueName", "NT_GiftUser AS a INNER JOIN NT_Gift AS b ON a.giftID = b.id INNER JOIN  NT_User AS c ON c.UserID = a.ReviceID where a.UserID=@UserID and c.state<>" + (byte)EnumUserState.Lock + "", "order by a.id desc" };
        /// <summary>
        /// 日历管理
        /// </summary>
        protected string[] user_calend_aspx = { "id", "id, UserID, Title, [Content], PostTime, NoteNumber, StartTime, EndTime", "NT_Calend where UserID=@UserID", "order by id desc" };

        protected string[] user_poke_aspx = { "a.id", "a.id, a.UserID, a.ReviceID, a.PokeKey, a.PokeForm, a.Poketo, a.PostTime,a.IsPub, b.TrueName", "NT_Poke AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.ReviceID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.id desc" };
        /// <summary>
        /// 邮件
        /// </summary>
        protected string[] user_mailbox_aspx = { "a.id", "a.id, a.UserID, a.SendID, a.title, a.Content, a.PostTime,a.isread, b.TrueName", "NT_MailBox AS a INNER JOIN NT_User AS b ON a.SendID = b.UserID where a.UserID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.id desc" };
        protected string[] user_mailsend_aspx = { "a.id", "a.id, a.UserID, a.ReviceID, a.title, a.Content, a.PostTime, b.TrueName", "NT_MailSend AS a INNER JOIN NT_User AS b ON a.ReviceID = b.UserID where a.UserID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.id desc" };


        /// <summary>
        /// 选择投票的好友
        /// </summary>
        protected string[] user_joinvote_aspx = { "a.id", "a.ID,a.UserID,a.VoteID,a.OptionID,b.TrueName", "Nt_VoteTo AS a INNER JOIN NT_User as b on a.UserID=b.UserID where a.VoteID=@VoteID and a.UserID<>@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.id desc" };
        /// <summary>
        /// 留言
        /// </summary>
        protected string[] user_gbook_aspx = { "a.id", "a.ID,a.UserID,a.SendID,a.Content,a.PostTime,b.TrueName", "Nt_Gbook AS a INNER JOIN NT_User as b on a.SendID=b.UserID where a.UserID=@UserID and a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime desc,a.id desc" };
        protected string[] user_gbookd_aspx = { "a.id", "a.ID,a.UserID,a.SendID,a.Content,a.PostTime,b.TrueName", "Nt_Gbook AS a INNER JOIN NT_User as b on a.SendID=b.UserID where a.islock=0 and (a.UserID=@UserID and a.SendID=@SendID or a.UserID=@SendID and a.SendID=@UserID) and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime desc,a.id desc" };
        protected string[] user_gbook_M_aspx = { "a.id", "a.ID,a.UserID,a.SendID,a.Content,a.PostTime,b.TrueName", "Nt_Gbook AS a INNER JOIN NT_User as b on a.SendID=b.UserID where (a.UserID=-1 and a.SendID=@UserID) and a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime desc,a.id desc" };
        protected string[] user_history_aspx = { "id", " id, UserID, [Content], Point, GPoint, Money, UTF, CreatTime", "NT_UserPointHistory where UserID=@UserID", "order by id desc" };
        protected string[] user_history_UT_aspx = { "id", " id, UserID, [Content], Point, GPoint, Money, UTF, CreatTime", "NT_UserPointHistory where UserID=@UserID and UTF=@UTF", "order by id desc" };
        protected string[] user_note_aspx = { "a.id", " a.ID, a.UserID, a.ReviceID, a.[Content], a.IsRead, a.PostTime, a.PostIP, a.MsgType, a.CorrID, b.TrueName", " NT_Notice AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.ReviceID =@UserID", "order by a.id desc" };

        /// <summary>
        ///搜索
        /// </summary>
        protected string[] user_help_aspx = { "id", "ID, HelpID,title", "NT_Help where 1=1", "order by id asc" };
        protected string[] user_helpLike_aspx = { "id", "ID, HelpID,title", "NT_Help where (title like @Key or Content like @Key)", "order by id asc" };

        /*模板*/
        protected string[] user_space_aspx = { "id", "ID, TName, TEName, PostTime, IsLock, IPoint, GPoint, UseNumber", "NT_spacetemplate where islock=0", "order by id desc" };

        #endregion 

        #region 管理员部分
        protected string[] manager_userlist_aspx = { "userid", "*", "NT_user where 1=1", "order by userid desc" };
        protected string[] manager_userlist_q_aspx = { "userid", "*", "NT_user where State=@Q", "order by userid desc" };
        protected string[] manager_userlist_q_key_aspx = { "userid", "*", "NT_user where State=@Q and (truename like @KWD or email like @KWD or username like @KWD)", "order by userid desc" };
        protected string[] manager_userlist_key_aspx = { "userid", "*", "NT_user where (truename like @KWD or email like @KWD or username like @KWD)", "order by userid desc" };
        protected string[] manager_userlist_vip_aspx = { "a.id", "a.*,b.truename", "NT_JoinVip AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1 ", "order by a.id desc" };
        protected string[] manager_news_aspx = { "a.id", "a.*,b.truename", "NT_news AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_news_key_aspx = { "a.id", "a.*,b.truename", "NT_news AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd or keywords like @kwd)", "order by id desc" };
        protected string[] manager_flash_aspx = { "id", "*", "NT_flash where 1=1", "order by orderid asc" };
        protected string[] manager_links_aspx = { "id", "*", "NT_links where 1=1", "order by id asc" };
        protected string[] manager_ads_aspx = { "id", "*", "NT_ads where 1=1", "order by id asc" };
      
        protected string[] manager_flash_make_aspx = { "id", "*", "NT_flash where islock=0", "order by orderid asc" };
        protected string[] manager_group_aspx = { "a.id", "a.*,b.truename", "NT_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_group_light_aspx = { "a.id", "a.*,b.truename", "NT_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islight=1", "order by a.id desc" };
        protected string[] manager_group_key_aspx = { "a.id", "a.*,b.truename", "NT_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.GroupName like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_topic_parentid0_aspx = { "a.id", "a.*,b.truename", "NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.TopicID=0", "order by a.id desc" };
        protected string[] manager_topic_parentid1_aspx = { "a.id", "a.*,b.truename", "NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.TopicID>0", "order by a.id desc" };
        protected string[] manager_topic_key_aspx = { "a.id", "a.*,b.truename", "NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_topicall_aspx = { "a.id", "a.*,b.truename", "NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_blog_draft_aspx = { "a.id", "a.*,b.truename", "NT_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.IsDraft=1", "order by a.id desc" };
        protected string[] manager_blogall_aspx = { "a.id", "a.*,b.truename", "NT_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_blog_key_aspx = { "a.id", "a.*,b.truename", "NT_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_twitterall_aspx = { "a.id", "a.*,b.truename", "NT_Twitter AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_twitter_key_aspx = { "a.id", "a.*,b.truename", "NT_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_photoall_aspx = { "a.id", "a.*,b.truename", "NT_Photo AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_photoalls_aspx = { "a.id", "a.*,b.truename", "NT_Photo AS a INNER JOIN NT_User AS b on a.userid=b.userid where albumid=0", "order by a.id desc" };
        protected string[] manager_photo_key_aspx = { "a.id", "a.*,b.truename", "NT_Photo AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.Description like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_albumall_aspx = { "a.albumid", "a.*,b.truename", "NT_album AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.albumid desc" };
        protected string[] manager_album_key_aspx = { "a.albumid", "a.*,b.truename", "NT_album AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.Description like @kwd or a.title like @kwd or b.truename like @kwd)", "order by a.albumid desc" };
        protected string[] manager_goodsall_aspx = { "a.id", "a.*,b.truename", "NT_ShopGoods AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_goods_key_aspx = { "a.id", "a.*,b.truename", "NT_ShopGoods AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.GoodsName like @kwd or a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_order_key_aspx = { "a.id", "a.*,b.goodsname,c.truename,b.userid as uid", " NT_ShopOrder AS a INNER JOIN NT_ShopGoods AS b ON a.GoodsID = b.id INNER JOIN NT_USER AS c on a.userid=c.userid WHERE  (a.OrderNumber like @kwd or c.truename like @kwd)", "ORDER BY a.posttime desc, a.id desc" };
        protected string[] manager_orderall_aspx = { "a.id", "a.*,b.goodsname,c.truename,b.userid as uid", " NT_ShopOrder AS a INNER JOIN NT_ShopGoods AS b ON a.GoodsID = b.id INNER JOIN NT_USER AS c on a.userid=c.userid WHERE 1=1", "ORDER BY a.posttime desc, a.id desc" };
        protected string[] manager_shopall_aspx = { "a.id", "a.*,b.truename", "NT_Shop AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_shop_key_aspx = { "a.id", "a.*,b.truename", "NT_Shop AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.shopname like @kwd or a.CompanyName like @kwd or a.ShopRName like @kwd or a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_activeall_aspx = { "a.id", "a.*,b.truename", "NT_Ative AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_active_key_aspx = { "a.id", "a.*,b.truename", "NT_Ative AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.AtiveName like @kwd or a.content like @kwd  or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_multeall_aspx = { "a.id", "a.*,b.truename", "NT_ShopMultebuy AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_multe_key_aspx = { "a.id", "a.*,b.truename", "NT_ShopMultebuy AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd  or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_askall_aspx = { "a.id", "a.*,b.truename", "NT_ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_ask_key_aspx = { "a.id", "a.*,b.truename", "NT_ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd  or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_ask_parentid0_aspx = { "a.id", "a.*,b.truename", "NT_ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.parentid=0", "order by a.id desc" };
        protected string[] manager_ask_parentid1_aspx = { "a.id", "a.*,b.truename", "NT_ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.parentid>0", "order by a.id desc" };
        protected string[] manager_voteall_aspx = { "a.id", "a.*,b.truename", "NT_vote AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_vote_key_aspx = { "a.id", "a.*,b.truename", "NT_vote AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd  or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_favall_aspx = { "a.id", "a.*,b.truename", "NT_Favorite AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_fav_key_aspx = { "a.id", "a.*,b.truename", "NT_Favorite AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.title like @kwd or a.content like @kwd or a.url like @kwd  or b.truename like @kwd)", "order by a.id desc" };
         /*评论*/
        protected string[] manager_comment_news_aspx = { "a.id", "a.*,b.truename", "NT_NewsComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_comment_news_key_aspx = { "a.id", "a.*,b.truename", "NT_NewsComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_comment_blog_aspx = { "a.id", "a.*,b.truename", "NT_BlogComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_comment_blog_key_aspx = { "a.id", "a.*,b.truename", "NT_BlogComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_comment_twitter_aspx = { "a.id", "a.*,b.truename", "NT_TwitterComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_comment_twitter_key_aspx = { "a.id", "a.*,b.truename", "NT_TwitterComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_comment_photo_aspx = { "a.id", "a.*,b.truename", "NT_PhotoComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_comment_photo_key_aspx = { "a.id", "a.*,b.truename", "NT_PhotoComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_comment_goods_aspx = { "a.id", "a.*,b.truename", "NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.ctype=0", "order by a.id desc" };
        protected string[] manager_comment_goods_key_aspx = { "a.id", "a.*,b.truename", "NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd) and a.ctype=0", "order by a.id desc" };
        protected string[] manager_comment_shop_aspx = { "a.id", "a.*,b.truename", "NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.ctype=1", "order by a.id desc" };
        protected string[] manager_comment_shop_key_aspx = { "a.id", "a.*,b.truename", "NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd) and a.ctype=1", "order by a.id desc" };
        protected string[] manager_comment_multe_aspx = { "a.id", "a.*,b.truename", "NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.ctype=2", "order by a.id desc" };
        protected string[] manager_comment_multe_key_aspx = { "a.id", "a.*,b.truename", "NT_ShopComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd) and a.ctype=2", "order by a.id desc" };
        protected string[] manager_comment_active_aspx = { "a.id", "a.*,b.truename", "NT_AtiveComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.id desc" };
        protected string[] manager_comment_active_key_aspx = { "a.id", "a.*,b.truename", "NT_AtiveComment AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.truename like @kwd)", "order by a.id desc" };
        protected string[] manager_admin_book_aspx = { "a.id", "a.*,b.truename", "NT_gbook AS a INNER JOIN NT_User AS b on a.sendid=b.userid where a.userid=-1", "order by a.id desc" };
        #endregion
        protected string[] manager_app_aspx = { "a.id", "a.id,a.appname,a.appkey,a.url,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.CreatTime desc,a.id desc" };
        protected string[] manager_app_key_aspx = { "a.id", "a.id,a.appname,a.appkey,a.url,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid where (a.content like @kwd or b.appname like @kwd)", "order by a.CreatTime desc,a.id desc" };
        protected string[] manager_app_dev_aspx = { "a.userid", "a.*,b.truename", "NT_AppDeveloper AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.JoinTime desc,a.userid desc" };
        protected string[] user_app_aspx = { "a.id", "a.id,a.appname,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.CreatTime desc,a.id desc" };
        protected string[] user_hot_aspx = { "a.id", "a.id,a.appname,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.click desc,a.id desc" };
        protected string[] user_app_classid_aspx = { "a.id", "a.id,a.appname,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.classid=@ClassID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.CreatTime desc,a.id desc" };
        protected string[] user_app_mys_aspx = { "a.id", "a.id,a.appname,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.userid=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.CreatTime desc,a.id desc" };
        protected string[] user_app_my_aspx = { "a.id", "a.id,a.appname,a.icon,a.pic,content,a.userid,a.classid,a.CreatTime,a.setupNumber,a.click,a.islock,b.truename", "NT_app AS a INNER JOIN NT_User AS b on a.userid=b.userid INNER JOIN NT_AppSetup AS c on a.id=c.appid where a.islock=0 and c.userid=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by c.PostTime desc,a.id desc" };
        protected string[] manager_report_aspx = { "a.id", "a.*,b.truename", "NT_report AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1", "order by a.PostTime desc,a.id desc" };
    
        /// <summary>
        /// 获取SQL语句
        /// </summary>
        /// <param name="PageName"></param>
        /// <returns></returns>
        protected string[] GetSqlSentence(string PageName)
        {
            FieldInfo fi = this.GetType().GetField(PageName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (fi == null)
                throw new Exception("没有找到SQL");
            return (string[])fi.GetValue(this);
        }

        /// <summary>
        /// 取得分页
        /// </summary>
        /// <param name="pageCode">分页代码,通常为类/param>
        /// <param name="pageIndex">当前页码(从１开始，大于总页数，则为最后一</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">返回记录总数</param>
        /// <param name="pageCount">返回总页/param>
        /// <param name="conditions">查询条件</param>
        /// <returns></returns>
        DataTable IUtilPage.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
        {
            try
            {
                string[] Sql = GetSqlSentence(pageCode);
                string IndexField = Sql[0];
                string AllFields = Sql[1];
                string TablesAndWhere = Sql[2];
                string OrderFields = Sql[3];
                if (conditions != null)
                {
                    foreach (SqlConditionInfo con in conditions)
                    {
                        if (con == null)
                            continue;
                        string paramnme = con.ParamName;
                        if (paramnme.IndexOf("@") == -1)
                        {
                            paramnme = "@" + paramnme; ;
                        }
                        TablesAndWhere = TablesAndWhere.Replace(paramnme, Util.GetStr(con));
                        OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                        AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                    }
                }
                return Pagination.ProcPage(AllFields, TablesAndWhere, IndexField, OrderFields, pageIndex, pageSize, out recordCount, out pageCount);
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception("用于分页的SQL语句无效," + e.Message);
            }
        }

        DataTable IUtilPage.GetUserSearchPage(object r, object keys, int ishead, object city, object syear, object eyear, int sex, object UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string sFilter = " where a.state<>" + (byte)EnumUserState.Lock + "";
            string IndexField = "a.userID";
            string OrderFields = " order by a.LastLoginTime desc,a.userid desc";
            string AllFields = " a.userName,a.userid,a.truename,a.sex";
            string Condition = " nt_user as a inner join nt_userinfo as b on a.UserID=b.UserID";
            if (Convert.ToInt32(UserID) > 0)
            {
                sFilter += " and a.UserID!=" + UserID + "";
            }
            if (string.IsNullOrEmpty(r.ToString()))
            {
                if (!string.IsNullOrEmpty(keys.ToString()))
                {
                    sFilter += " and a.userName like '%" + keys + "%' or a.truename like '%" + keys + "%'";
                }
                if (ishead > 0)
                {
                    sFilter += " and a.Portrait<>0";
                }
                if (Convert.ToInt32(city) > 0)
                {
                    sFilter += " and a.City=" + city + "";
                }
                if (syear.ToString() != "0")
                {
                    sFilter += " and (datepart(year, getdate()) -datepart(year,b.Birthday))>=" + syear + "";
                }
                if (eyear.ToString() != "0")
                {
                    sFilter += " and (datepart(year, getdate()) -datepart(year,b.Birthday))<=" + eyear + "";
                }
                if (sex != -1)
                {
                    sFilter += " and a.Sex=" + sex + "";
                }
            }
            else
            {
                UserBaseInfo basi = null;
                UserInfo uinfo = null;
                switch (r.ToString())
                {
                    case "birday":
                        basi = JuSNS.Home.User.User.Instance.GetUserBaseInfo(UserID);
                        sFilter += " and b.Birthday='" + basi.Birthday.ToString("yyyy-M-d") + "'";
                        break;
                    case "constellation":
                        basi = JuSNS.Home.User.User.Instance.GetUserBaseInfo(UserID);
                        sFilter += " and b.Constellation=" + basi.Constellation + "";
                        break;
                    case "city":
                        uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(UserID);
                        sFilter += " and b.HomeCity=" + uinfo.City + "";
                        break;
                    case "vocation":
                        basi = JuSNS.Home.User.User.Instance.GetUserBaseInfo(UserID);
                        sFilter += " and b.Vocation=" + basi.Vocation + "";
                        break;
                }
            }
            Condition = Condition + sFilter;
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetBlogPage(string q, string keys, int classid, string orderby, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            switch (orderby)
            {
                case "rec":
                    OrderFields = " order by a.isrec desc,a.id desc";
                    break;
                case "click":
                    OrderFields = " order by a.click desc,a.id desc";
                    break;
                case "comments":
                    OrderFields = " order by a.Comments desc,a.id desc";
                    break;
                case "att":
                    OrderFields = " order by a.attnumber desc,a.id desc";
                    break;
                case "share":
                    OrderFields = " order by a.ShareNumber desc,a.id desc";
                    break;
                case "last":
                    OrderFields = " order by a.LastModTime desc,a.id desc";
                    break;
            }
            string AllFields = "a.id,a.title,a.content,a.PostTime,a.userid,a.attnumber,a.click,a.ShareNumber,a.isRec,b.truename,a.islock,a.Comments,a.IsDraft,a.sysClassID,a.PicPath";
            string Condition = " nt_blog as a inner join nt_user as b on a.UserID=b.UserID";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            if (!string.IsNullOrEmpty(q.ToString()))
            {
                if (JuSNS.Common.Input.IsInteger(q))
                {
                    if (Convert.ToInt32(q) == UserID)
                    {
                        sFilter += " and a.UserID=" + q;
                    }
                    else
                    {
                        sFilter += " and a.UserID=" + q + " and a.IsDraft=0 and a.islock=0 and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
                    }
                }
                else
                {
                    switch (q)
                    {
                        case "my":
                            sFilter += " and a.UserID=" + UserID;
                            break;
                        case "draft":
                            sFilter += " and a.UserID=" + UserID + " and (a.IsDraft=1 or a.islock=1)";
                            break;
                        case "friend":
                            if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                            sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + UserID + " and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
                            break;
                    }
                }
            }
            else
            {
                sFilter += " and a.IsDraft=0 and a.islock=0 and a.Privacy=" + (int)EnumPrivacy.ForWholeSite + "";
            }
            if (classid > 0)
            {
                sFilter += " and (a.myClassID=" + classid + " or a.sysClassID=" + classid + ")";
            }
            if (SqlCondition != null)
            {
                string BlogSearchText = JuSNS.Common.Public.GetXMLBaseValue("BlogSearchText");
                if (BlogSearchText == "1")
                {
                    sFilter += " and (a.title like @kwd or a.content like @kwd or b.truename like @kwd or b.UserName like @kwd)";
                }
                else
                {
                    sFilter += " and a.title like @kwd";
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetNewsPage(string q, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where a.islock=0";
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.ClassID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "rec":
                        OrderFields = " order by a.isrec desc,a.id desc";
                        break;
                    case "click":
                        OrderFields = " order by a.click desc,a.id desc";
                        break;
                    case "comments":
                        OrderFields = " order by a.Comments desc,a.id desc";
                        break;
                    case "att":
                        OrderFields = " order by a.attnumber desc,a.id desc";
                        break;
                    case "share":
                        OrderFields = " order by a.ShareNumber desc,a.id desc";
                        break;
                    case "ram":
                        OrderFields = " order by newid()";
                        break;
                }
            }
            string AllFields = "a.id,a.title,a.color,a.bold,a.italic,a.content,a.PostTime,a.userid,a.attnumber,a.click,a.ShareNumber,a.isRec,a.islock,a.Comments,a.ClassID,a.Pic,b.ChannelName";
            string Condition = " nt_news AS a INNER JOIN nt_newschannel as b on a.ClassID=b.id";
            if (SqlCondition != null)
            {
                string ContentSearchText = JuSNS.Common.Public.GetXMLBaseValue("ContentSearchText");
                if (ContentSearchText == "1")
                {
                    sFilter += " and (a.title like @kwd or a.content like @kwd or  Keywords like @kwd)";
                }
                else
                {
                    sFilter += " and (a.title like @kwd or  Keywords like @kwd)";
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetNewsInfoPage(string q, string keys, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id,a.title,a.color,a.bold,a.italic,a.content,a.PostTime,a.userid,a.attnumber,a.click,a.ShareNumber,a.isRec,a.islock,a.Comments,a.ClassID,a.Pic,b.truename,c.ChannelName";
            string Condition = " nt_news AS a INNER JOIN NT_user as b on a.userid=b.userid INNER JOIN NT_NewsChannel AS c on a.ClassID=c.id";
            if (SqlCondition != null)
            {
                    string ContentSearchText = JuSNS.Common.Public.GetXMLBaseValue("ContentSearchText");
                    if (ContentSearchText == "1")
                    {
                        sFilter += " and (a.title like @kwd or a.content like @kwd or  Keywords like @kwd)";
                    }
                    else
                    {
                        sFilter += " and (a.title like @kwd or  Keywords like @kwd)";
                    }
            }
            switch (q)
            {
                case "friend":
                    if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                    sFilter += " and a.islock=0 and a.UserID in (" + keys + ") and a.UserID<>" + UserID;
                    break;
                case "islock":
                    sFilter += " and a.UserID=" + UserID + " and a.islock=1";
                    break;
                case "info":
                    sFilter += " and a.UserID=" + UserID + " and a.islock=0";
                    break;
                default:
                    sFilter += " and a.UserID=" + UserID;
                    break;
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetAlbumPage(string q,string keys, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.AlbumID";
            string OrderFields = " order by a.CreateTime desc,a.albumid desc";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.AlbumID,a.title,a.Description,a.CreateTime,a.ImagesCount,a.userid,a.LastUploadTime,a.GroupID,a.isrec,b.truename";
            string Condition = " nt_album AS a INNER JOIN NT_user as b on a.userid=b.userid";
            if (SqlCondition != null)
            {
                sFilter += " and (a.title like @kwd or a.Description like @kwd or b.truename like @kwd or b.username like @kwd)";
            }
            if (!string.IsNullOrEmpty(q))
            {
                if (JuSNS.Common.Input.IsInteger(q))
                {
                    if (Convert.ToInt32(q) == UserID)
                    {
                        sFilter += " and a.UserID=" + q;
                    }
                    else
                    {
                        sFilter += " and a.UserID=" + q + " and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
                    }
                }
                else
                {
                    switch (q)
                    {
                        case "friend":
                            if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                            sFilter += " and a.UserID in (" + keys + ") and a.Privacy<>" + (int)EnumPrivacy.ForOwner + " and a.UserID<>" + UserID;
                            break;
                        case "my":
                            sFilter += " and a.UserID=" + UserID + "";
                            break;
                    }
                }
            }
            else
            {
                sFilter += " and a.Privacy=" + (int)EnumPrivacy.ForWholeSite + "";
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetGroupPage(string q, string keys, int classid, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where a.islock=0";
            if (q == "my") sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id,a.UserID,a.GroupName,a.Members,a.Bulletin,a.CityID,a.Portrait,a.ClassID,a.skinDir,a.PostTime,a.Islight,a.isrec,b.truename";
            string Condition = " nt_group AS a INNER JOIN NT_user as b on a.userid=b.userid";
            if (classid > 0)
            {
                sFilter += " and a.ClassID=" + classid;
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.GroupName like @kwd or a.Bulletin like @kwd or b.truename like @kwd or b.username like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q + " and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.Privacy<>" + (int)EnumPrivacy.ForOwner + " and a.UserID<>" + UserID;
                        break;
                    case "join":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.Id in (" + keys + ")";
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + UserID + "";
                        break;
                    case "rec":
                        sFilter += " and a.isRec=1 and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
                        break;
                    case "light":
                        sFilter += " and a.Islight=1 and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
                        break;
                    default:
                        sFilter += " and a.Privacy<>" + (int)EnumPrivacy.ForOwner + "";
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetGroupTopicPage(string q, int gid, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.istop desc,a.isbest desc, a.lastposttime desc,a.PostTime desc,a.id desc";
            string sFilter = " where a.islock=0 and a.topicid=0 and b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id,a.groupid,a.userid,a.title,a.content,a.posttime,a.topicid,a.istop,a.lastposttime,a.replynumber,a.clicks,a.isrec,a.islock,a.isbest,b.truename";
            string Condition = " nt_groupTopic AS a INNER JOIN NT_user as b on a.userid=b.userid";
            if (gid > 0)
            {
                sFilter += " and a.groupid=" + gid;
            }
            if (SqlCondition != null)
            {
                int SearchContent = Convert.ToInt32(JuSNS.Common.Public.GetXMLGroupValue("SearchContent"));
                if (SearchContent == 1)
                {
                    sFilter += " and (a.title like @kwd or a.content like @kwd or b.truename like @kwd or b.username like @kwd)";
                }
                else
                {
                    sFilter += " and a.title like @kwd";
                }
            }
            switch (q)
            {
                case "best":
                    sFilter += " and a.isbest=1";
                    break;
                case "top":
                    sFilter += " and a.top=1";
                    break;
                case "clicks":
                    OrderFields = "  order by a.clicks desc,a.lastposttime desc,a.PostTime desc,a.id desc";
                    break;
                case "reply":
                    OrderFields = "  order by a.replynumber desc,a.lastposttime desc,a.PostTime desc,a.id desc";
                    break;
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetAskPage(string q, string keys, int classid, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.isJinji desc,a.PostTime desc,a.id desc";
            string sFilter = " where a.isLock=0 and a.parentid=0 and b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id, a.ClassID, a.ParentID, a.Title, a.[Content], a.PostTime, a.jiFen, a.UserID, a.isLock, a.Tag, a.click, a.Pic, a.isClose, a.isJinji, a.isBest,a.isRec, b.TrueName,c.classname,c.id as classid";
            string Condition = "NT_Ask AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN NT_AskClass AS c ON a.ClassID = c.id";
            if (classid > 0)
            {
                sFilter += " and a.ClassID=" + classid;
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.title like @kwd or a.Content like @kwd or a.Tag like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + UserID;
                        break;
                    case "quick":
                        if (UserID > 0)
                        {
                            sFilter += " and a.isJinji=1 and a.UserID<>" + UserID;
                        }
                        else
                        {
                            sFilter += " and a.isJinji=1";
                        }
                        break;
                    case "all":
                        OrderFields = " order by a.PostTime desc,a.id desc";
                        break;
                    case "ok":
                        sFilter += " and a.isClose=1";
                        OrderFields = " order by a.PostTime desc,a.id desc";
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + UserID + "";
                        OrderFields = " order by a.PostTime desc,a.id desc";
                        break;
                    case "hight":
                        sFilter += " and a.jiFen>=" + JuSNS.Common.Public.GetXMLAskValue("hightpoint");
                        OrderFields = " order by a.PostTime desc,a.id desc";
                        break;
                    default:
                        sFilter += " and a.isClose=0";
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetAtivePage(string q, string keys, int classid, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where a.isLock=0 and b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.Id, a.AtiveName, a.AreaID, a.AreaID1, a.ClassID, a.StartTime, a.EndTime, a.UserID, a.Members, a.IsLock, a.Money, a.BaoMingTime, a.PersionNumber, a.Clicks, a.ATT, a.AddRess, a.GroupID, a.[Content], a.PostTime, a.PostIP, a.Links, a.Photo, a.IsChecks, a.IsRec, a.Note, b.TrueName,c.ClassName";
            string Condition = "NT_Ative AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN NT_AtiveClass AS c on a.classid=c.id";
            if (classid > 0)
            {
                sFilter += " and a.ClassID=" + classid;
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.AtiveName like @kwd or a.[Content] like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + UserID;
                        break;
                    case "ing":
                        sFilter += " and DATEDIFF(dd,EndTime,getdate())<=0";
                        break;
                    case "ed":
                        sFilter += " and DATEDIFF(dd,EndTime,getdate())>0";
                        break;
                    case "rec":
                        sFilter += " and a.isRec=1";
                        break;
                    case "city":
                        sFilter += " and b.City=" + keys + " and a.UserID<>" + UserID;
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + UserID;
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetGoodsPage(string q, string keys, int classid, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where a.isLock=0 and b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id, a.GoodsName, a.UserID, a.ShopID, a.Price, a.mPrice,  a.StartTime, a.EndTime,  a.PostTime,  a.ClassID, a.MulteBuy, a.MulteMinNumber, a.MulteMaxNumber,a.multeprice, a.Pic, a.IsRec,a.IsOld, b.TrueName,c.ClassName";
            string Condition = "NT_ShopGoods AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN NT_ShopClass AS c on a.classid=c.id";
            if (classid > 0)
            {
                sFilter += " and a.ClassID=" + classid;
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.GoodsName like @kwd or a.[Content] like @kwd or a.[keywords] like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + UserID;
                        break;
                    case "rec":
                        sFilter += " and a.isRec=1";
                        break;
                    case "city":
                        sFilter += " and b.City=" + keys + " and a.UserID<>" + UserID;
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + UserID;
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetMultePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where a.isLock=0 and b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id, a.UserID, a.GoodsID, a.Title, a.[Content], a.MinMember, a.MaxMember, a.StartTime, a.EndTime, a.JoinMember, a.AttMember, a.Price, a.PostTime, a.ProvinceID, a.CityID, a.AddRess,  a.Pic,a.IsRec, b.TrueName";
            string Condition = "NT_ShopMultebuy AS a INNER JOIN  NT_User AS b ON a.UserID = b.UserID";
            if (SqlCondition != null)
            {
                sFilter += " and (a.Title like @kwd or a.[Content] like @kwd or a.[keywords] like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + userid;
                        break;
                    case "rec":
                        sFilter += " and a.isRec=1";
                        break;
                    case "city":
                        sFilter += " and b.City=" + keys + " and a.UserID<>" + userid;
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + userid;
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }

        DataTable IUtilPage.GetShopPage(string q, string keys, int classid, int UserID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where a.isLock="+(int)EnumCusState.ForNormal+"";
            if (q == "my")
            {
                sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            }
            string AllFields = "a.*, b.TrueName,c.ClassName";
            string Condition = "NT_Shop AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN NT_ShopClass AS c on a.classid=c.id";
            if (classid > 0)
            {
                sFilter += " and a.ClassID=" + classid;
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.ShopName like @kwd or a.ShopRName like @kwd  or a.[Content] like @kwd or a.[keywords] like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + UserID;
                        break;
                    case "rec":
                        sFilter += " and a.isRec=1";
                        break;
                    case "city":
                        sFilter += " and b.City=" + keys + " and a.UserID<>" + UserID;
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + UserID;
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        DataTable IUtilPage.GetSharePage(string q, string keys, string t, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + " and a.isLock=" + (byte)EnumCusState.ForNormal;
            string AllFields = "a.id, a.infoid, a.ShareType, a.PostTime, a.webURL, a.IsLock,a.isrec, a.Title, a.[Content], a.Comments, a.userid, b.TrueName";
            string Condition = "NT_Share AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID";
            if (!string.IsNullOrEmpty(t))
            {
                sFilter += " and a.ShareType=" + t;
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.Title like @kwd or a.[Content] like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + userid;
                        break;
                    case "rec":
                        sFilter += " and a.isRec=1";
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + userid;
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }

        DataTable IUtilPage.GetVotePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.ID,a.Title,a.Content,a.UserID,a.PostTime,a.Mode,a.EndTime,a.JCnt,a.IsRec,b.trueName";
            string Condition = "NT_Vote AS a INNER JOIN NT_User AS b ON a.[userid]=b.[userid]";
            if (q == "join")
            {
                AllFields = "a.ID,a.Title,a.Content,a.UserID,a.PostTime,a.Mode,a.EndTime,a.JCnt,a.IsRec,c.trueName";
                Condition = "NT_Vote As a Inner Join Nt_VoteTo As b On a.ID=b.VoteID inner join NT_User AS c on a.[userid]=c.[userid]";
                sFilter = " where c.state<>" + (byte)EnumUserState.Lock + "";
            }
            if (SqlCondition != null)
            {
                sFilter += " and (a.Title like @kwd or a.[Content] like @kwd) and DATEDIFF(dd,a.EndTime,getdate())<0";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q + " and DATEDIFF(dd,a.EndTime,getdate())<0";
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + userid + " and DATEDIFF(dd,a.EndTime,getdate())<0";
                        break;
                    case "join":
                        sFilter += " and b.UserID=" + userid + " and DATEDIFF(dd,a.EndTime,getdate())<0";
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + userid;
                        break;
                    case "hot":
                        sFilter += " and a.isFriend!=1 and DATEDIFF(dd,a.EndTime,getdate())<0";
                        OrderFields = "order by a.JCnt desc,a.ID desc";
                        break;
                    default:
                        sFilter += " and a.isFriend!=1 and DATEDIFF(dd,a.EndTime,getdate())<0";
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }
        ///// <summary>
        ///// 微博
        ///// </summary>
        //protected string[] user_twitter_my_aspx = { "a.id", "a.ID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.Comments, a.MType, a.isRec,a.pic,a.media,b.truename", "NT_twitter as A INNER JOIN nt_user As b on a.userid=b.userid where a.UserID=@UserID and a.islock=0  and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        //protected string[] user_twitter_Friend_aspx = { "a.id", "a.ID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.Comments, a.MType, a.isRec,a.pic,a.media,b.truename", "NT_twitter as A INNER JOIN nt_user As b on a.userid=b.userid where a.UserID in (@FriendID) and a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        //protected string[] user_twitter_all_aspx = { "a.id", "a.ID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.Comments, a.MType, a.isRec,a.pic,a.media,b.truename", "NT_twitter as A INNER JOIN nt_user As b on a.userid=b.userid where a.islock=0 and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };
        protected string[] user_twitter_UserID_aspx = { "a.id", "a.ID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.Comments, a.MType, a.isRec,a.pic,a.media,b.truename", "NT_twitter as A INNER JOIN nt_user As b on a.userid=b.userid where a.islock=0 and a.UserID=@UserID and b.state<>" + (byte)EnumUserState.Lock + "", "order by a.PostTime DESC,a.id DESC" };

        DataTable IUtilPage.GetOnlinePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.lasttime DESC,a.id DESC";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.ID, a.UserID, a.lastip, a.lasturl, a.lasttime, a.username,b.truename";
            string Condition = " NT_Onlineuser as A INNER JOIN nt_user As b on a.userid=b.userid";
            if (q == "friend")
            {
                if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + userid;
            }
            Condition = Condition + sFilter;
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }

        DataTable IUtilPage.GetTwitterPage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime DESC,a.id DESC";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + " and a.islock=0";
            string AllFields = "a.ID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.Comments, a.MType, a.isRec,a.pic,a.media,b.truename";
            string Condition = " NT_twitter as A INNER JOIN nt_user As b on a.userid=b.userid";
             if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.UserID in (" + keys + ") and a.UserID<>" + userid;
                        break;
                    case "my":
                        sFilter += " and a.UserID=" + userid;
                        break;
                }
            }
            Condition = Condition + sFilter;
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }

        DataTable IUtilPage.GetFavoritePage(string q, int classid, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.PostTime desc,a.id desc";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.id,a.UserID, a.URL, a.ClassID, a.IsPub, a.title, a.[content], a.PostTime, b.TrueName";
            string Condition = " NT_Favorite AS a INNER JOIN   NT_User AS b ON a.UserID = b.UserID";
            if (SqlCondition != null)
            {
                sFilter += " and (a.Title like @kwd or a.[Content] like @kwd)";
            }
            if (JuSNS.Common.Input.IsInteger(q))
            {
                sFilter += " and a.UserID=" + q;
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        if (keys.Length > 2) keys = keys.Replace("0,", string.Empty);
                        sFilter += " and a.ispub=1 and a.UserID in (" + keys + ") and a.UserID<>" + userid;
                        break;
                    case "my":
                        if (classid > 0)
                        {
                            sFilter += " and a.classid="+classid+" and a.UserID=" + userid;
                        }
                        else
                        {
                            sFilter += " and a.UserID=" + userid;
                        }
                        break;
                    default:
                        sFilter += " and a.IsPub=1 and a.UserID<>" + userid;
                        break;
                }
            }
            Condition = Condition + sFilter;
            if (SqlCondition != null)
            {
                foreach (SqlConditionInfo con in SqlCondition)
                {
                    if (con == null)
                        continue;
                    string paramnme = con.ParamName;
                    if (paramnme.IndexOf("@") == -1)
                    {
                        paramnme = "@" + paramnme; ;
                    }
                    Condition = Condition.Replace(paramnme, Util.GetStr(con));
                    OrderFields = OrderFields.Replace(con.ParamName, con.ParamValue.ToString());
                    AllFields = AllFields.Replace(paramnme, Util.GetStr(con));
                }
            }
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }

        DataTable IUtilPage.GetFriendPage(string keys, int userid, int classid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            string IndexField = "a.id";
            string OrderFields = " ORDER BY b.LastLoginTime DESC, a.ID DESC";
            string sFilter = " where a.state=0 and b.state<>" + (byte)EnumUserState.Lock + "";
            string AllFields = "a.ID, a.FriendID, b.UserName, b.TrueName";
            string Condition = " NT_Friend AS a INNER JOIN  NT_User AS b ON b.UserID = a.FriendID";
            sFilter += " and  (a.UserID = "+userid+")";
            if (classid > 0)
            {
                sFilter += " and a.ClassID=" + classid;
            }
            sFilter += " and a.FriendID not in (" + keys + ")";
            Condition = Condition + sFilter;
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }

        DataTable IUtilPage.GetDynAllPage(int userid, string dyntype, string killuser, int pageindex, int pagesize, out int recordcount, out int pagecount)
        {
            string IndexField = "a.id";
            string OrderFields = " order by a.id desc";
            string sFilter = " where b.state<>" + (byte)EnumUserState.Lock + " and b.state<>" + (byte)EnumUserState.Lock;
            string AllFields = "a.ID, a.UserID, a.cUserID, a.dynType, a.[Content], a.PostTime, a.infoarr,b.truename";
            string Condition = "nt_dyn AS a INNER JOIN NT_User AS b on a.userid=b.userid";
            bool ishide = true;
            if (userid>0)
            {
                sFilter += " and a.userid =" + userid;
                ishide = false;
            }
            if (!string.IsNullOrEmpty(dyntype))
            {
                switch (dyntype)
                {
                    case "dyn-photo":
                        sFilter += " and (dynType=" + (byte)EnumDynType.CreatAlbum + " or  dynType=" + (byte)EnumDynType.CreatPhoto + " or  dynType=" + (byte)EnumDynType.PhotoComment + ")";
                        break;
                    case "dyn-blog":
                        sFilter += " and (dynType=" + (byte)EnumDynType.CreatBlog + " or  dynType=" + (byte)EnumDynType.BlogComment + ")";
                        break;
                    case "dyn-twitter":
                        sFilter += " and (dynType=" + (byte)EnumDynType.CreatTwitter + " or  dynType=" + (byte)EnumDynType.TwitterComment + ")";
                        break;
                    case "dyn-group":
                        sFilter += " and (dynType=" + (byte)EnumDynType.TopicComment + " or  dynType=" + (byte)EnumDynType.CreatTopic + " or  dynType=" + (byte)EnumDynType.JoinGroup + " or  dynType=" + (byte)EnumDynType.CreatGroup + " or  dynType=" + (byte)EnumDynType.ReplyTopic + ")";
                        break;
                    case "dyn-share":
                        sFilter += " and (dynType=" + (byte)EnumDynType.CreatShare + ")";
                        break;
                    case "dyn-state":
                        sFilter += " and (dynType=" + (byte)EnumDynType.Friend + " or  dynType=" + (byte)EnumDynType.InviteJoinSite + " or  dynType=" + (byte)EnumDynType.UpdateBasic + " or  dynType=" + (byte)EnumDynType.UpdateHeadPic + " or  dynType=" + (byte)EnumDynType.VerfiyEmail + ")";
                        break;
                    case "dyn-other":
                        sFilter += " and (dynType=" + (byte)EnumDynType.ActiveComment + " or  dynType=" + (byte)EnumDynType.ActiveCreatPhoto + " or  dynType=" + (byte)EnumDynType.AskBest + " or  dynType=" + (byte)EnumDynType.AskComment + " or  dynType=" + (byte)EnumDynType.CreatActive + "";
                        sFilter += " or  dynType=" + (byte)EnumDynType.CreatAPP + " or  dynType=" + (byte)EnumDynType.CreatAsk + " or  dynType=" + (byte)EnumDynType.CreatFaviote + " or  dynType=" + (byte)EnumDynType.CreatGift + " or  dynType=" + (byte)EnumDynType.CreatGoods + " or  dynType=" + (byte)EnumDynType.CreatMulte + " or  dynType=" + (byte)EnumDynType.CreatNews + "";
                        sFilter += " or  dynType=" + (byte)EnumDynType.CreatPoke + " or  dynType=" + (byte)EnumDynType.CreatShop + " or  dynType=" + (byte)EnumDynType.CreatVote + " or  dynType=" + (byte)EnumDynType.GoodsComment + " or  dynType=" + (byte)EnumDynType.JoinVote + " or  dynType=" + (byte)EnumDynType.MulteComment + " or  dynType=" + (byte)EnumDynType.JoinMulte + " or  dynType=" + (byte)EnumDynType.NewsComment + ")";
                        break;
                }
            }
            if (ishide)
            {
                if (!string.IsNullOrEmpty(killuser))
                {
                    sFilter += " and a.userid not in (" + killuser + ")";
                }
            }
            Condition = Condition + sFilter;
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, pagesize, out recordcount, out pagecount, null);
        }
    }
}
