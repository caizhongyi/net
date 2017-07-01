using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.UI;
using System.Security.Permissions;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Resources;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using PowerTalkBoxContrls.Design;
using PowerTalkBoxEnum.Enum;
using Events;


/*Programmer:Engine
*PowerBy:EngineSystem
*Oicq:282602809    
*Msn:Cangta2002@hotmail.com
*E-Mail:Hee_jun1985@163.com
*Authorization:Free
*Co:DaLian-NetInfoSoft
*Co:DaLian-NetInfoSoft
*/

[assembly: TagPrefix("PowerTalkBox", "PTB")]
//[assembly: TagPrefix("UserNameTest", "UNT")]
namespace PowerTalkBox
{
    #region PowerTalkBox控件
    [
    ToolboxData("<{0}:PowerTalkBox runat=server></{0}:PowerTalkBox>"),
    ValidationPropertyAttribute("Text"),
     ToolboxBitmap(typeof(PowerTalkBox), "PowerTalkBox.bmp"),
    Designer(typeof(PowerTalkBoxContrls.Design.PowerTalkBoxDesigner)),
    DescriptionAttribute("一个基于 MSHTML 的 ASP.NET 可视化编辑器。")
    ]
    public class PowerTalkBox : WebControl, ICallbackEventHandler
    {
        #region 控件选项
    [
    CategoryAttribute("PowerTalk选项"),
    DescriptionAttribute("刷新频率:以毫秒为单位")
    ]
        public int Interval
        {
            get
            {
                object savedState = this.ViewState["IntervalTimes"];
                return (savedState == null) ? 4800 : (int)savedState;
            }
            set
            {
                ViewState["IntervalTimes"] = value;
            }
        }
        
   [
    CategoryAttribute("PowerTalk选项"),
    DescriptionAttribute("过期次数:HttpContext.Current.Session过期的最大跳转次数")
    ]
        public int LeaveTimes
        {
            get
            {
                object savedState = this.ViewState["LeaveTimes"];
                return (savedState == null) ? 60 : (int)savedState;
            }
            set
            {
                ViewState["LeaveTimes"] = value;
            }
        }

        /// <summary>
        /// 次数
        /// </summary>
        public static int OutTimes
        {
            get
            {
                object oi = HttpContext.Current.Session["Engin_OutTimes"];


                return (oi==null)?0:int.Parse(oi.ToString());
            }
            set
            {
                string otm = value.ToString();
                HttpContext.Current.Session["Engin_OutTimes"] = otm;
            }
        }
    
        [CategoryAttribute("PowerTalk选项"),
         DescriptionAttribute("过期后跳转地址:URL地址")
        ]
        public   string OutLocation
        {
            get { 
                object LocState=this.ViewState["Engin_OutLocation"];
                return (LocState == null) ? "http://www.cnblogs.com/powertalkbox" : LocState.ToString();  
                }
                set { this.ViewState["Engin_OutLocation"] = value.ToString(); }
        }
   [CategoryAttribute("PowerTalk选项"),
    DescriptionAttribute("广告翻滚内容:URL地址")
   ]
        public string ADNetInfoString
        {
            get
            {
                object LocState = this.ViewState["Engin_ADNetInfoString"];
                return (LocState == null) ? "NetInfoAdInterFace.htm" : LocState.ToString();
            }
            set { this.ViewState["Engin_ADNetInfoString"] = value.ToString(); }
        }
             [CategoryAttribute("PowerTalk选项"),
         DescriptionAttribute("广告图片:右下角的广告图片")
        ]
        public string ADNetInfo
        {
            get {
                object LocState = this.ViewState["ADNetInfo"];
                return (LocState == null) ? "images/AD_NetInfo.jpg" : LocState.ToString();  
                }
                set { this.ViewState["ADNetInfo"] = value.ToString(); }
        }
         
        [CategoryAttribute("PowerTalk选项"),
        DescriptionAttribute("聊天模式:群聊模式与一对一模式,群聊适合聊天室,一对一适合网站客户咨询")
        ]
        public  ChatMode CMode
        {
            get
            {
                object savedState = this.ViewState["ChatModeMode"];
                return (savedState == null) ? ChatMode.OneToOne : (ChatMode)savedState;
            }
            set
            {
                ViewState["ChatModeMode"] = value;
            }
        }
  
        [CategoryAttribute("PowerTalk选项"),
        DescriptionAttribute("系统模式:Web之间聊天,Web与其他IM之间聊天")
        ]
        public SystemMode SMode
        {
            get
            {
                object savedState = this.ViewState["SystemMode"];
                return (savedState == null) ? SystemMode.WebToWeb : (SystemMode)savedState;
            }
            set
            {
                ViewState["SystemMode"] = value;
            }
        }
     
  
  
        [CategoryAttribute("PowerTalk选项"),
         DescriptionAttribute("右上角Contrl的显示内容:HTML内容,如果为空则是用户列表")
        ]      
        public  string ChatContrlHtml
        {
            get {
                  object HtmlState=this.ViewState["Engin_ChatContrlHtml"];
                  return (HtmlState == null) ? "" : HtmlState.ToString();
            }
            set { this.ViewState["Engin_ChatContrlHtml"] = value.ToString(); }
        }       
        [CategoryAttribute("PowerTalk选项"),
        DescriptionAttribute("聊天对象:为空则从用户列表中自动选择第一个,适合即时咨询系统")
        ]  
        public  string ToUserIdContent
        {
            get {
                object HtmlState = this.ViewState["Engin_ToUserIdContent"];
                return (HtmlState == null) ? "" : HtmlState.ToString();  
            }
            set {this.ViewState["Engin_ToUserIdContent"] = value.ToString(); }
        }
        [CategoryAttribute("PowerTalk选项"),
         DescriptionAttribute("我的ID:使用者的用户名,适合2次开发固定用户名,如QQ MSN等,如果为空则随机生成用户名!")
        ]
        public string MyUserName
        {
            get {
                object HtmlState = this.ViewState["Engin_UserID"];
                return (HtmlState == null) ? "": HtmlState.ToString();  
            }
            set { this.ViewState["Engin_UserID"] = value.ToString(); }

        }
              [CategoryAttribute("PowerTalk选项"),
        DescriptionAttribute("自动Session:是否自动加入游客到Session")
        ]
        public bool AutoSession
        {
            get
            {
                object LocState = this.ViewState["AutoSession"];
                return (LocState == null) ? true : (bool)LocState;
            }
            set { this.ViewState["AutoSession"] = value; }
        }
        [CategoryAttribute("PowerTalk选项"),
 DescriptionAttribute("是否插入到缓存:如果插入，释放时在Global.asax的Session_End里加入PowerTalkBox.PowerTalk.DeleteUserInfo(Session[\"Engin_UserID\"].ToString());")
 ]
        public bool UserToList
        {
            get
            {
                object LocState = this.ViewState["UserToList"];
                return (LocState == null) ? true : (bool)LocState;
            }
            set { this.ViewState["UserToList"] = value; }
        }
        [CategoryAttribute("PowerTalk选项"),
DescriptionAttribute("是否有传文件功能:如果True就是有传文件功能，如果False就是没有传文件功能")
]
        public bool AllowSendFile
        {
            get
            {
                object LocState = this.ViewState["AllowSendFile"];
                return (LocState == null) ? true : (bool)LocState;
            }
            set { this.ViewState["AllowSendFile"] = value; }
        }

        #endregion
        #region 画控件部分
        public static HtmlGenericControl MyUserId = new HtmlGenericControl();
        public static HtmlGenericControl ToUserId = new HtmlGenericControl();
        public static HtmlGenericControl RightTop = new HtmlGenericControl();
        protected override void CreateChildControls()
        {
          
            HtmlGenericControl htmlStrAA = new HtmlGenericControl();
            HtmlGenericControl htmlStrAB = new HtmlGenericControl();
            HtmlGenericControl htmlStrBA = new HtmlGenericControl();
            HtmlGenericControl htmlStrBB = new HtmlGenericControl();
            MyUserId.ID = "MyUserId";
            ToUserId.ID = "ToUserID";
            RightTop.ID = "RightTop";
            RightTop.InnerHtml = @"
            <p class=""online""><a href=""#"" onclick=""document.getElementById('ToUserID').innerHTML=document.getElementById('MyUserID').innerHTML"" title=""管理"">正在载入..</a></p>";

            #region Table部分
            string TableHalfAA = @"
   <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
    <tr>
      <td><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
          <tr>
            <td width=""136"" style=""height: 49px""><img src=""images/PT_logo.gif"" alt=""PowerTalk"" width=""136"" height=""49"" /></td>
            <td class=""pt-headerbg"" style=""height: 49px""><img src=""images/spacer.gif"" alt=""space"" width=""20"" height=""1"" /></td>
            <td width=""374"" class=""pt-libname"" style=""height: 49px""><h2></h2></td>
          </tr>
      </table></td>
    </tr>
    <tr>
      <td class=""pt-fullbg""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
        <tr>
          <td width=""11"" class=""pt-leftborder"" style=""height: 368px""><img src=""images/PT_leftBorder.gif"" width=""11"" height=""5"" /></td>
          <td valign=""top"" style=""height: 368px; width: 100%;""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
            <tr>
              <td width=""28""><img src=""images/border_ChatHead_left.gif"" width=""28"" height=""26"" /></td>
              <td class=""pt-chatheader"">您正与 <strong>";
            //<span id=""ToUserID""></span>
                string TableHalfAB = @"</strong> 交谈</td>
              <td style=""width: 4px""><img src=""images/border_ChatHead_right.gif"" width=""4"" height=""26"" /></td>
            </tr>
          </table>
            <div class=""pt-chatbox2"">
              <label>
               <iframe src=""ChatList.htm"" id=""ChatListBox""  width=""100%"" height=""218px"" ></iframe>                         
              </label>
            </div>
            <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""pt-iconbox"">
              <tr>
                    <td width=""6""><img src=""images/spacer.gif"" width=""6"" height=""1"" /></td>
                <td width=""40px"" align=""center""><a href=""#"" title=""设置文本""><img onClick=""PowerTalk_showOrHide1(1),PowerTalk_showOrHide(0),PowerTalk_showOrHide2(0)"" src=""images/Icon_txt.gif"" alt=""设置文本"" width=""16"" height=""16"" border=""0"" /></a></td>
                <td width=""6px""><img src=""images/IconSpacer.gif"" width=""6"" height=""26"" /></td>
                <td width=""40px"" align=""center""><a href=""#"" title=""增加表情""><img onClick=""PowerTalk_showOrHide(1),PowerTalk_showOrHide1(0),PowerTalk_showOrHide2(0)"" src=""images/Icon_Face.gif"" alt=""增加表情"" width=""16"" height=""16"" border=""0"" /></a></td>
                <td width=""6px""><img src=""images/IconSpacer.gif"" alt="""" width=""6"" height=""26"" /></td>"+((AllowSendFile)?@"
                <td width=""40px"" align=""center""><a href=""#"" title=""传送文件""><img onClick=""PowerTalk_showOrHide2(1)""  src=""images/Icon_SendFile.gif"" alt=""传送文件"" width=""16"" height=""16"" border=""0"" /></a></td>
                <td width=""6px""><img src=""images/IconSpacer.gif"" alt="""" width=""6"" height=""26"" /></td>":"")+ @"
                <td width=""40px"" align=""center""><a href=""#"" title=""保存通话记录""><img onClick=""PowerTalk_CmdSave()"" src=""images/Icon_SaveData.gif"" alt=""保存通话记录"" width=""16"" height=""16"" border=""0"" /></a></td>
                 <td width=""6px""><img src=""images/IconSpacer.gif"" alt="""" width=""6"" height=""26"" /></td>
                  <td width=""40px"" align=""center""><a href=""#"" title=""清屏""><img  onClick=""PowerTalk_Cls();"" src=""images/cas.gif"" alt=""清屏"" width=""16"" height=""16"" /></a></td>
<td align=""center"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>                
<td  width=""100%""><div id=""PowerTools"">
       <div id=""ADList"" style=""float:left; width:100%; padding-right:5px; height:25px; overflow:hidden;text-align:left; line-height:18px; "" >
<ul style=""margin:3px 0 0 0; padding:0px; list-style-type:none; "">
" +((ADNetInfoString=="NetInfoAdInterFace.htm")? @"<LI style=""HEIGHT: 25px"">
<DIV class=slideFilm 
style=""OVERFLOW: hidden; WIDTH: 100%; HEIGHT: 25px; TEXT-ALIGN: left""><A 
href=""http://www.cnblogs.com/powertalkbox"" target=_blank>不必租用专门的IM即时聊天系统</A></DIV></LI>
<LI style=""HEIGHT: 25px"">
<DIV class=slideFilm 
style=""OVERFLOW: hidden; WIDTH: 100%; HEIGHT: 25px; TEXT-ALIGN: left""><A 
href=""http://www.cnblogs.com/powertalkbox"" target=_blank>帮助您第一时间与客户沟通</A></DIV></LI>
<LI style=""HEIGHT: 25px"">
<DIV class=slideFilm 
style=""OVERFLOW: hidden; WIDTH: 100%; HEIGHT: 25px; TEXT-ALIGN: left""><A 
href=""http://www.cnblogs.com/powertalkbox"" 
target=_blank>整合聊天工具，可以进行二次开发</A></DIV></LI>":PowerTalkBoxContrls.Design.Html.WebClientGetSource(ADNetInfoString)) + @"

</ul>
</div>
</div></td>
              </tr>
            </table>
               <div class=""pt-chatbox2"">
              <label>
              <iframe src=""blankpage.htm"" id=""SendMsg""  width=""100%"" height=""118px"" ></iframe>

              </label>
            </div></td>
          <td width=""6"" style=""height: 368px""><img src=""images/spacer.gif"" alt=""spacer2"" width=""6"" height=""1"" /></td>
          <td valign=""top"" class=""pt-controlbg"" style=""height: 368px; width: 183px;""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
            <tr>
              <td width=""24"" style=""height: 26px""><img src=""images/border_AppraiseHead_Left.gif"" width=""24"" height=""26"" /></td>
              <td class=""pt-rtbg"" style=""height: 26px""><strong>";
          string TableHalfBA = @"</strong></td>
              <td width=""4"" style=""height: 26px""><img src=""images/border_AppraiseHead_right.gif"" width=""4"" height=""26"" /></td>
            </tr>
          </table>
          <div id=""Engin_UserList"" class=""pt-control""><h3 style=""height: 38px"">在线人数:<span id=""OnlineCount"" >-</span></h3>";

         string TableHalfBB = @" </div> 
              <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
              <tr>
              <td class=""pt-appsend"" style=""height: 25px""><label><a href='http://www.powertalkbox.cn/' target='_blank'><img id=""PowerTalkImg"" src=""images/AdMiddle.png"" /></a>
          </table>
          <img id=""PowerTalkImg"" src=""" + ADNetInfo + @""" />
          </td>
          <td class=""pt-rightborder"" style=""height: 368px; width: 11px;""><img src=""images/PT_rightBorder.gif"" width=""11"" height=""5"" /></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
        <tr>
          <td width=""11"" style=""height: 50px""><img src=""images/PT_BottomLeft.gif"" width=""11"" height=""50"" /></td>
          <td class=""pt-bottom"" style=""height: 50px"">
            <p id=""KeyInput"">
                <input type=""radio""  name=""hotkey""  id=""hotkey1"" value=""0"">按 Enter 键发送
                    <input  name=""hotkey"" id=""hotkey2"" type=""radio""  value=""1"" checked>按 Ctrl+Enter 发送</p>
              
            <label><input type=""button"" name=""Submit"" id=""Submit"" value=""发 送"" onclick=""SendInfo()"" class=""sendon"" onmouseover=""this.className='sendover';"" onmouseout=""this.className='sendon';"" />
            </label></td>
          <td width=""190"" class=""pt-bombg"" style=""height: 50px""><img src=""images/spacer.gif"" width=""1"" height=""1"" /></td>
          <td width=""11"" style=""height: 50px""><img src=""images/PT_BottomRight.gif"" width=""11"" height=""50"" /></td>
        </tr>
      </table></td>
    </tr>
  </table>  
  <div id=""Layer2"" style=""z-index: 1; left: 16px; visibility: hidden; width: 205px;
        position: absolute; top: 245px; height: 20px; background-color: #ecf5ff; layer-background-color: #ECF5FF"">
        <table border=""1"" bordercolor=""#cee7ff"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
            <tr>
                <td width=""20%"" height= 20px>
                    <select id=""select1""  name=""select1"" onchange=""changeFont(this);"">
                        <option selected=""selected"" value=""宋体"">字体</option>
                        <option value=""宋体"">宋体</option>
                        <option value=""黑体"">黑体</option>
                        <option value=""幼圆"">幼圆</option>
                        <option value=""楷体"">楷体</option>
                        <option value=""隶书"">隶书</option>
                        <option value=""仿宋"">仿宋</option>
                        <option value=""华文彩云"">彩云</option>
                        <option value=""华文新魏"">新魏</option>
                        <option value=""华文行楷"">行楷</option>
                    </select>
                </td>
                <td width=""20%""  height= 20px>
                    <select id=""select2""  name=""select2"" onchange=""changeSize(this);"">
                        <option selected=""selected"" value=""12"">字号</option>
                        <option value=""10"">10</option>
                        <option value=""12"">12</option>
                        <option value=""14"">14</option>
                        <option value=""16"">16</option>
                        <option value=""18"">18</option>
                        <option value=""20"">20</option>
                        <option value=""24"">24</option>
                        <option value=""36"">36</option>
                    </select>
                </td>
                <td width=""20%""  height= 20px>
                    <select id=""select3""  name=""select3"" onchange=""changeColors(this);"">
                        <option selected=""selected"">颜色</option>
                        <option style=""background-color: #000000"" value=""#000000"">黑色</option>
                        <option style=""background-color: #888888"" value=""#888888"">灰色</option>
                        <option style=""background-color: #ff0000"" value=""#ff0000"">红色</option>
                        <option style=""background-color: #cc3366"" value=""#cc3366"">暗红</option>
                        <option style=""background-color: #ff00ff"" value=""#ff00ff"">紫红</option>
                        <option style=""background-color: #ee9966"" value=""#ee9966"">橘黄</option>
                        <option style=""background-color: #aa00cc"" value=""#aa00cc"">紫色</option>
                        <option style=""background-color: #8800ff"" value=""#8800ff"">蓝紫</option>
                        <option style=""background-color: #ffff00"" value=""#ffff00"">黄色</option>
                        <option style=""background-color: #ccaa00"" value=""#ccaa00"">土黄</option>
                        <option style=""background-color: #ff8800"" value=""#ff8800"">金黄</option>
                        <option style=""background-color: #0000ff"" value=""#0000ff"">蓝色</option>
                        <option style=""background-color: #6699ee"" value=""#6699ee"">天蓝</option>
                        <option style=""background-color: #0088ff"" value=""#0088ff"">海蓝</option>
                        <option style=""background-color: #000088"" value=""#000088"">深蓝</option>
                        <option style=""background-color: #00ff00"" value=""#00ff00"">绿色</option>
                        <option style=""background-color: #888800"" value=""#888800"">黄绿</option>
                        <option style=""background-color: #008888"" value=""#008888"">蓝绿</option>
                    </select></td>
                    <td width=""20%""  height= 20px>
                    <input name=""Submit2"" onclick=""PowerTalk_showOrHide1(0),PowerTalk_showOrHide2(0),PowerTalk_showOrHide(0)"" type=""button"" value=""关闭"" />
                </td>
            </tr>
        </table>
    </div>
    <div id=""Layer1"" style=""z-index: 1; left: 14px; visibility: hidden; width: 335px;
        position: absolute; top: 180px; height: 38px; background-color: #ecf5ff"">
        <table border=""1"" bordercolor=""#c8e3ff"" cellpadding=""0"" cellspacing=""0"" width=""1%"">
            <tr>
                <td style=""height: 21px"">
                    <img id=""img1"" onclick=""selImg(img1.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img1.gif""
                        title="":)"" value=""face/img1.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img2"" onclick=""selImg(img2.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img2.gif""
                        title="":-O"" value=""face/img2.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img3"" onclick=""selImg(img3.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img3.gif""
                        title="":P"" value=""face/img3.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img4"" onclick=""selImg(img4.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img4.gif""
                        title=""(H)"" value=""face/img4.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img5"" onclick=""selImg(img5.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img5.gif""
                        title="":@"" value=""face/img5.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img6"" onclick=""selImg(img6.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img6.gif""
                        title="":S"" value=""face/img6.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img7"" onclick=""selImg(img7.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img7.gif""
                        title="":$"" value=""face/img7.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img8"" onclick=""selImg(img8.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img8.gif""
                        title="":'("" value=""face/img8.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img9"" onclick=""selImg(img9.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img9.gif""
                        title="":|"" value=""face/img9.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img10"" onclick=""selImg(img10.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img10.gif""
                        title=""(A)"" value=""face/img10.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img11"" onclick=""selImg(img11.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img11.gif""
                        title=""8o|"" value=""face/img11.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img12"" onclick=""selImg(img12.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img12.gif""
                        title=""8-|"" value=""face/img12.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img13"" onclick=""selImg(img13.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img13.gif""
                        title=""+o("" value=""face/img13.gif"" /></td>
                <td style=""height: 21px"">
                    <img id=""img14"" onclick=""selImg(img14.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img14.gif""
                        title=""<:o)"" value=""face/img14.gif"" /></td>
                <td style=""height: 21px; width: 22px;"">
                    <img id=""img15"" onclick=""selImg(img15.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img15.gif""
                        title=""|-)"" value=""face/img15.gif"" /></td>
            </tr>
            <tr>
                <td>
                    <img id=""img16"" onclick=""selImg(img16.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img16.gif""
                        title=""*-)"" value=""face/img16.gif"" /></td>
                <td>
                    <img id=""img17"" onclick=""selImg(img17.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img17.gif""
                        title="":-#"" value=""face/img17.gif"" /></td>
                <td>
                    <img id=""img18"" onclick=""selImg(img18.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img18.gif""
                        title="":-*"" value=""face/img18.gif"" /></td>
                <td>
                    <img id=""img19"" onclick=""selImg(img19.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img19.gif""
                        title=""^o)"" value=""face/img19.gif"" /></td>
                <td>
                    <img id=""img20"" onclick=""selImg(img20.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img20.gif""
                        title=""8-)"" value=""face/img20.gif"" /></td>
                <td>
                    <img id=""img21"" onclick=""selImg(img21.value)"" onmouseup=""showOrHide(0)"" src=""face/img21.gif""
                        title=""(L)"" value=""face/img21.gif"" /></td>
                <td>
                    <img id=""img22"" onclick=""selImg(img22.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img22.gif""
                        title=""(U)"" value=""face/img22.gif"" /></td>
                <td>
                    <img id=""img23"" onclick=""selImg(img23.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img23.gif""
                        title=""(M)"" value=""face/img23.gif"" /></td>
                <td>
                    <img id=""img24"" onclick=""selImg(img24.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img24.gif""
                        title=""(@)"" value=""face/img24.gif"" /></td>
                <td>
                    <img id=""img25"" onclick=""selImg(img25.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img25.gif""
                        title=""(&)"" value=""face/img25.gif"" /></td>
                <td>
                    <img id=""img26"" onclick=""selImg(img26.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img26.gif""
                        title=""(sn)"" value=""face/img26.gif"" /></td>
                <td>
                    <img id=""img27"" onclick=""selImg(img27.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img27.gif""
                        title=""(bah)"" value=""face/img27.gif"" /></td>
                <td>
                    <img id=""img28"" onclick=""selImg(img28.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img28.gif""
                        title=""(S)"" value=""face/img28.gif"" /></td>
                <td>
                    <img id=""img29"" onclick=""selImg(img29.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img29.gif""
                        title=""(*)"" value=""face/img29.gif"" /></td>
                <td style=""width: 22px"">
                    <img id=""img30"" onclick=""selImg(img30.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img30.gif""
                        title=""(#)"" value=""face/img30.gif"" /></td>
            </tr>
            <tr>
                <td>
                    <img id=""img31"" onclick=""selImg(img31.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img31.gif""
                        title=""(R)"" value=""face/img31.gif"" /></td>
                <td>
                    <img id=""img32"" onclick=""selImg(img32.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img32.gif""
                        title=""({)"" value=""face/img32.gif"" /></td>
                <td>
                    <img id=""img33"" onclick=""selImg(img33.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img33.gif""
                        title=""(})"" value=""face/img33.gif"" /></td>
                <td>
                    <img id=""img34"" onclick=""selImg(img34.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img34.gif""
                        title=""(K)"" value=""face/img34.gif"" /></td>
                <td>
                    <img id=""img35"" onclick=""selImg(img35.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img35.gif""
                        title=""(F)"" value=""face/img35.gif"" /></td>
                <td>
                    <img id=""img36"" onclick=""selImg(img36.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img36.gif""
                        title=""(W)"" value=""face/img36.gif"" /></td>
                <td>
                    <img id=""img37"" onclick=""selImg(img37.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img37.gif""
                        title=""(O)"" value=""face/img37.gif"" /></td>
                <td>
                    <img id=""img38"" onclick=""selImg(img38.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img38.gif""
                        title="";)"" value=""face/img38.gif"" /></td>
                <td>
                    <img id=""img39"" onclick=""selImg(img39.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img39.gif""
                        title="":D"" value=""face/img39.gif"" /></td>
                <td>
                    <img id=""img40"" onclick=""selImg(img40.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img40.gif""
                        title="":("" value=""face/img40.gif"" /></td>
                <td>
                    <img id=""img41"" onclick=""selImg(img41.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img41.gif""
                        title=""小狗"" value=""face/img41.gif"" /></td>
                <td>
                    <img id=""img42"" onclick=""selImg(img42.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img42.gif""
                        title=""骷髅"" value=""face/img42.gif"" /></td>
                <td>
                    <img id=""img43"" onclick=""selImg(img43.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img43.gif""
                        title=""监视"" value=""face/img43.gif"" /></td>
                <td>
                    <img id=""img44"" onclick=""selImg(img44.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img44.gif""
                        title=""太阳"" value=""face/img44.gif"" /></td>
                <td style=""width: 22px"">
                    <img id=""img45"" onclick=""selImg(img45.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img45.gif""
                        title=""信件"" value=""face/img45.gif"" /></td>
            </tr>
            <tr>
                <td>
                    <img id=""img46"" onclick=""selImg(img46.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img46.gif""
                        title=""飞眼"" value=""face/img46.gif"" /></td>
                <td>
                    <img id=""img47"" onclick=""selImg(img47.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img47.gif""
                        title=""电视"" value=""face/img47.gif"" /></td>
                <td>
                    <img id=""img48"" onclick=""selImg(img48.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img48.gif""
                        title=""钟表"" value=""face/img48.gif"" /></td>
                <td>
                    <img id=""img49"" onclick=""selImg(img49.value)"" onmouseup=""PowerTalk_showOrHide(0)"" src=""face/img49.gif""
                        title=""灯泡"" value=""face/img49.gif"" /></td>
                        <td>
                    </td>
            </tr> 
            					
        </table>
        <input name=""closebutton"" onclick=""PowerTalk_showOrHide1(0),PowerTalk_showOrHide2(0),PowerTalk_showOrHide(0)"" type=""button"" value=""关闭"" />
    </div>
    
    <span id=""Layer3"" style=""border-right: #000000 1px; border-top: #000000 1px; z-index: 1;
        left: 14px; visibility: hidden; overflow: hidden; border-left: #000000 1px; width: 314px;
        border-bottom: #000000 1px; position: absolute; top: 196px; height: 69px; background-color: #f7fbff;
        layer-background-color: #F7FBFF"">
        <div align=""left"">
            <table border=""0"" style=""width: 99%; height: 27px"">
                <tr>
                    <td colspan=""3"" style=""width: 342px; height: 18px"">
                        <div align=""left"">
                            <font color=""#ff0000"" size=""2"">限doc、rar、jpg、gif、png、bmp、swf、zip类型。</font></div>
                    </td>
                </tr>
                <tr>
                    <td style=""width: 4%; height: 26px"">
                        <font size=""2"">
                            <input id=""upfile"" type=""file""  /> 
                        </font>
                    </td>
                    <td style=""width: 10%; height: 26px"">
             <input id=""SendFile"" type=""button"" value=""发送"" onclick=""SendFileInfo();PowerTalk_showOrHide2(0)"" />
                  </td>
                  <td style=""width: 13%; height: 26px"">
                  <input name=""Submit2"" onclick=""PowerTalk_showOrHide2(0)"" type=""button"" value=""关闭"" />
                             </td>
                        
                    
                </tr>
            </table>
        </div>
    </span>
";
            
            #endregion


         /*
            *浏览器判别$1  1为发送信息 2为Ms-Ie中接收信息 3为firefox 中接受信息
            *发送者$0
            *接收者$1  
            *列表$2         
            *信息$3   
            */
         #region Scripts
         string ReceiveScriPowerTalk = @"
 <script type=""text/javascript""> 
window.setInterval('ReadChatInfo()', " + Interval.ToString()+ @");
function ReadChatInfo()
{
GetArea('0&'+document.getElementById('MyUserId').innerHTML)
}

function SendFileInfo()
{

if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{

IframeChatListBox.contentDocument.body.innerHTML+='<font color=red>文件发送中...</font><br/>';
GetArea('2$'+document.getElementById('MyUserId').innerHTML+'$'+document.getElementById('ToUserID').innerHTML+'$'+document.getElementById('upfile').value);
}
else
{
IframeChatListBox.document.body.innerHTML+='<font color=red>文件发送中...</font><br/>';
GetArea('2$'+document.getElementById('MyUserId').innerHTML+'$'+document.getElementById('ToUserID').innerHTML+'$'+document.getElementById('upfile').value);
}
}

function SendInfo()
{
 if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{
GetArea('1$'+document.getElementById('MyUserId').innerHTML+'$'+document.getElementById('ToUserID').innerHTML+'$'+IframeSendMsg.contentDocument.body.innerHTML);
IframeSendMsg.contentDocument.body.innerHTML='';
}
else
{
GetArea('1$'+document.getElementById('MyUserId').innerHTML+'$'+document.getElementById('ToUserID').innerHTML+'$'+IframeSendMsg.document.body.innerHTML);
IframeSendMsg.document.body.innerHTML='';
}
IframeSendMsg.focus();
}

function FormatText(command, option){
IframeSendMsg.document.execCommand(command, true, option);
IframeSendMsg.focus();
}

function changeSize(obj){ 
FormatText('fontsize',obj.value);


} 

function changeFont(obj){ 
FormatText('fontname' ,obj.value);


} 

function changeColors(obj){ 
FormatText('ForeColor',obj.value);

}

function selImg(text){
document.frames(""SendMsg"").focus();

var range =document.frames(""SendMsg"").document.selection.createRange();

var str1=""<img src='""+text+""'/>"";
range.pasteHTML(str1); 
}
function PowerTalk_showOrHide(value) {
if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{
alert('FireFox不支持本功能!');
}
else
{
    if (value==0) {
        if (document.layers)
           document.layers[""Layer1""].visibility='hide';
        else
           document.all[""Layer1""].style.visibility='hidden';
   }
   else if (value==1) {
       if (document.layers)
          document.layers[""Layer1""].visibility='show';
       else
          document.all[""Layer1""].style.visibility='visible';
   }
   }
}
function PowerTalk_showOrHide1(value) {
if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{
alert('FireFox不支持本功能!');
}
else
{
    if (value==0) {
        if (document.layers)
           document.layers[""Layer2""].visibility='hide';
        else
           document.all[""Layer2""].style.visibility='hidden';
   }
   else if (value==1) {
       if (document.layers)
          document.layers[""Layer2""].visibility='show';
       else
          document.all[""Layer2""].style.visibility='visible';
   }
   }
}

function PowerTalk_showOrHide2(value) {
    if (value==0) {
        if (document.layers)
           document.layers[""Layer3""].visibility='hide';
        else
           document.all[""Layer3""].style.visibility='hidden';
   }
   else if (value==1) {
       if (document.layers)
          document.layers[""Layer3""].visibility='show';
       else
          document.all[""Layer3""].style.visibility='visible';
   }
}

function PowerTalk_CmdSave()
{
if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{
alert('FireFox不支持本功能!');
}
else
{
var OW = window.open('','','');
var DD = new Date();
OW.document.open();
OW.document.write(IframeChatListBox.document.body.innerHTML);
}
OW.document.execCommand (""SaveAs"",true,""聊天记录-""+ DD.getYear() + ""-"" + DD.getMonth() + ""-"" + DD.getDate() + ""-"" + DD.getHours() + ""-"" + DD.getMinutes() +"".htm"");
OW.close();
}

function PowerTalk_Cls()
{
if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{
IframeChatListBox.contentDocument.body.innerHTML='';
}
else
{
IframeChatListBox.document.body.innerHTML='';
}
}

function set()
{
     if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
    {
    IframeSendMsg.contentDocument.body.innerHTML='123123';
    IframeChatListBox.contentDocument.body.innerHTML='123123';
    }
    else
    {
    IframeSendMsg.document.body.innerHTML='321321';
    IframeChatListBox.document.body.innerHTML='321321';
    }
 }
var IframeChatListBox;
var IframeSendMsg;
function InitStart()
{
if(window.navigator.userAgent.indexOf(""Firefox"")>=1)
{
IframeChatListBox=document.getElementById(""ChatListBox"");
IframeSendMsg = document.getElementById(""SendMsg"");
IframeSendMsg.contentWindow.document.designMode = ""On"";
document.getElementById(""KeyInput"").style.visibility='hidden';
}
else
{
IframeChatListBox=frames.document.frames(""ChatListBox"");
IframeSendMsg=frames.document.frames(""SendMsg"");
IframeSendMsg.document.designMode=""on"";
IframeSendMsg.document.onkeydown=new Function(""return HotKeyPress(IframeSendMsg.event);"");
}
 new SlideBox('ADList', 5000, 'top');
}

function SlideBox(container, frequency, direction, speed) {
	if (typeof(container) == 'string') {
		container = document.getElementById(container);
	}
	this.container = container;
	this.frequency = frequency;
	this.direction = direction;
	this.speed = speed || 2;
	this.films = [];
	var divs = this.container.getElementsByTagName('div');
	for (var i = 0; i < divs.length; i++) {
		if (divs[i].className == 'slideFilm') {
			divs[i].onmouseover = function(self){return function(){self._mouseover()};}(this);
			divs[i].onmouseout = function(self){return function(){self._mouseout()};}(this);
			this.films[this.films.length] = divs[i];
		}
	}
	this._playTimeoutId = null;
	this._slideTimeoutId = null;
	this._slidable = true;
	var isIE5 = navigator.userAgent.toLowerCase().indexOf(""msie 5.0"")>0;
	if (!isIE5)
		this._loop();
}

SlideBox.prototype = {
	_loop : function() {
		var sb = this;
		this._playTimeoutId = setTimeout(function(){sb._slide()}, this.frequency);
	},

	_slide : function() {
		var sb = this;
		var _slide = function() {
      try{
			if (!sb._slidable) return;
			var c = sb.container;
			if (sb.direction == 'top') {
				if (c.scrollTop < c.offsetHeight-sb.speed) {
					c.scrollTop += sb.speed ;
				} else {
					clearInterval(sb._slideTimeoutId);
					sb._loop();
					var ul = c.getElementsByTagName('ul')[0];
					ul.appendChild(c.getElementsByTagName('li')[0]);
					c.scrollTop = 0;
				}
			} else if (sb.direction == 'left') {
				if (c.scrollLeft < c.offsetWidth-sb.speed) {
					c.scrollLeft += sb.speed ;
				} else {
					clearInterval(sb._slideTimeoutId);
					sb._loop();
					var ul = c.getElementsByTagName('ul')[0];
					ul.appendChild(c.getElementsByTagName('li')[0]);
					c.scrollLeft = 0;
				}
			}
      }catch(e){}
		}
		this._slideTimeoutId = setInterval(_slide, 10);
	},

	_mouseover : function() {
		this._slidable = false;
	},

	_mouseout : function() {
		this._slidable = true;
	}
}





function HotKeyPress(event)
{	
    var hotkey;
    if(document.all.hotkey[0].checked) hotkey=0;
    if(document.all.hotkey[1].checked) hotkey=1;
    if(hotkey)
    {
        if(event.ctrlKey && event.keyCode==13)
        {
    SendInfo();

        }
    }
    else
    {
        if(event.keyCode==13)
        {
    SendInfo();	
        }
    }

}


InitStart();
   function Show(area)
    {
     if(window.navigator.userAgent.indexOf('Firefox')>=1)
    {
    var content = IframeChatListBox.contentDocument.body;
    }
    else
    {
    var content = IframeChatListBox.document.body;
    } 
   var listContrl= document.getElementById('Engin_UserList');//用户列表
   var UserList=(area.split('$'))[0];//取列表的值
        if(UserList=='0')//如果为提取的信息
        {
        UserList=(area.split('$'))[1];
        var MyCount= document.getElementById('OnlineCount');//取现在状态下的人数
        var ServerCount=(UserList.split('|'))[0];//服务区传过来的人数
        if(MyCount.innerHTML!= ServerCount)//如果服务器人数与现在状态的人数不等
        {  
        listContrl.innerHTML='<h3>在线人数:<span id=""OnlineCount"">0</span></h3>'+(UserList.split('|'))[1];//制作列表
        MyCount.innerHTML=ServerCount;//把服务器人数复制到状态人数控件上
        document.getElementById('OnlineCount').innerHTML=ServerCount;
        }
var msg=area.replace('0$'+UserList+'$','');
content.innerHTML+=msg;
content.scrollTop = content.scrollHeight; 
}
if(UserList=='1')//如果为插入的信息
{
  content.innerHTML+=area.replace(UserList+'$','');
  content.scrollTop = content.scrollHeight;  
} 
if(UserList=='2')//自定义方法
{
window.location.href='" + OutLocation + @"';
}
}";

            string CallbackScriPowerTalk = @" 
function GetArea(areaID){ 
 areaId=areaID;
    " + Page.ClientScript.GetCallbackEventReference(this, "areaId", "Show", null) + @"; 
   }</script>
    ";
         #endregion
            htmlStrAA.InnerHtml = TableHalfAA;
            htmlStrAB.InnerHtml = TableHalfAB;
            htmlStrBA.InnerHtml = TableHalfBA;           
            htmlStrBB.InnerHtml =TableHalfBB+ReceiveScriPowerTalk + CallbackScriPowerTalk;
            this.Controls.Add(htmlStrAA);
            this.Controls.Add(ToUserId);
            this.Controls.Add(htmlStrAB);
            this.Controls.Add(MyUserId);
            this.Controls.Add(htmlStrBA);
            this.Controls.Add(RightTop);
            this.Controls.Add(htmlStrBB);
            base.CreateChildControls();
        }
        #endregion
        #region 后台
        #region 初始化设置
        
        public  void InitLoad()
        {
        
          string  WwwPath=  HttpContext.Current.Request.Url.AbsoluteUri;
          PowerTalk.FaceWwwPath  = WwwPath.Replace(WwwPath.Substring(WwwPath.LastIndexOf(@"/")+1),"");
            //设置部分
            if (SMode == SystemMode.WebToWeb)
            {
                object sessionObj = HttpContext.Current.Session["Engin_UserID"];
                if (sessionObj != null)
                {
                    MyUserName = sessionObj.ToString();
                }
                if (string.IsNullOrEmpty(MyUserName))//没值随机赋值
                {
                    MyUserName = PowerTalk.NewClientUserLogin(UserToList);
                    if (AutoSession)//AutoSession应该自动Session，添加用户到列表
                    {
                        HttpContext.Current.Session["Engin_UserID"] = MyUserName;
                    }
                }
                else
                {
                    if (UserToList)//自动插入列表则插入
                    {
                        if (sessionObj == null)//如果没有Session，
                        {
                            if (AutoSession)//AutoSession应该自动Session，添加用户到列表
                            {
                                PowerTalk.NewUserLogin(MyUserName, "信息", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                HttpContext.Current.Session["Engin_UserID"] = MyUserName;
                            }
                        }
                    }
                }          

                MyUserId.InnerHtml = MyUserName;
                ToUserId.InnerHtml = ToUserIdContent;
                if (ChatContrlHtml.Trim() != "")
                {
                    RightTop.InnerHtml = ChatContrlHtml;
                }
                if (OnUserLogin != null)
                {
                    EventArgs ea = new EventArgs();
                    OnUserLogin(MyUserName, ea);//登录事件
                }
            }
            

        }
 
     
        #endregion
        #region 事件定义
    
      
        [Description("接收信息时事件")]
        public event ReceiveMessageEventHandler OnChatInfoRecive;
        [Description("发送信息时事件")]
        public  event EventHandler OnChatInfoSend;

        [Description("登录时的事件")]
        public  event EventHandler OnUserLogin;
        [Description("每隔时间段儿刷新时的事件")]
        public event EventHandler OnRefresh;
        [Description("过期跳转时的事件")]
        public event EventHandler OneLoginOut;
        #endregion
        #region 无刷新回调
        private string area;
        public string GetCallbackResult()
        {
      
            string RtStr = "1";
            switch (area[0].ToString())
            {
                case "0"://取信息
                    string CtMode = "对您说";
                    if (CMode == ChatMode.OneToMore)
                    {
                        CtMode = "说";
                    }
                    OutTimes++;//累加次数
                    string UserList = "";
                    string ChatList = "";
                    string bb = area.Substring(2);
                    //人员列表
                    List<ChatInfo> LCI = PowerTalk.ReadChatInfo(MyUserName, SMode);
                    foreach (ChatInfo ChIf in LCI)
                    {
                        if (OnChatInfoRecive != null)//接收事件
                        {
                            ReceiveMessageEventArgs rmea = new ReceiveMessageEventArgs();
                            rmea.sender = ChIf.Sender;
                            rmea.reciver = CtMode;
                            rmea.Value = ChIf.SendContent;
                            OnChatInfoRecive(RtStr, rmea);
                            ChatList += "<br/><font color='blue'>" + rmea.sender + "</font><font color='red'>" + rmea.reciver + ":</font><font color='blue'>" + ChIf.SendTime.ToString(" HH:mm:ss") + "</font><br/>" + rmea.Value + "<br/>";
                        }
                        else
                        {
                            ChatList += "<br/><font color='blue'>" + ChIf.Sender + "</font><font color='red'>" + CtMode + ":</font><font color='blue'>" + ChIf.SendTime.ToString(" HH:mm:ss") + "</font><br/>" + ChIf.SendContent + "<br/>";
                        }
                    }
                    List<UserInfo> LUI = PowerTalk.UserInfos(MyUserName);
                    int OnlineNum = 0;
                    if (ChatContrlHtml.Trim() == "")
                    {
                        foreach (UserInfo ChIf in LUI)
                        {
                            OnlineNum++;
                            UserList += "<p class=\"online\"><a href=\"#\" onclick=\"document.getElementById('ToUserID').innerHTML='" + ChIf.UserID + "'\" title=\"管理\">" + ChIf.UserID + "</a></p>";
                        }
                        RtStr = "0" + "$" + OnlineNum.ToString() + "|" + UserList + "$" + ChatList;
                    }
                    else
                    {
                        UserList =  ChatContrlHtml ;
                        RtStr = "0" + "$" +"1" + "|" + UserList + "$" + ChatList;
                    }
             
                    //判断是否过期
                    if (OutTimes >= LeaveTimes)
                    {
                        if (OneLoginOut != null)
                        { 
                        EventArgs ags=new EventArgs();                         
                        OneLoginOut(MyUserName, ags);
                        }
                        HttpContext.Current.Session.Abandon();//取消对话
                        RtStr = "2" + "$" + OnlineNum.ToString() + "|" + UserList + "$" + OutLocation;

                    }
                    break;
                case "1"://插入
                    OutTimes = 0;//过期次数执0
                    string SplitStr = area;
                    String[] Str = SplitStr.Split('$');
                    string TopStr = Str[0] + Str[1] + Str[2];
                    string MyUserId = Str[1];
                    string ToUserID = Str[2];
                    string SendMsg = SplitStr.Substring(TopStr.Length + 3);

                    if (SMode == SystemMode.WebToWeb)
                    {
                        if (CMode == ChatMode.OneToOne)
                        {
                            if (ToUserID == "")
                            {
                                SendMsg = "<font color=blue>发送失败,对方不能为空!</font>";
                            }
                            else
                            {
                                if (PowerTalk.FindUserInfo(ToUserID) == null)
                                {
                                    SendMsg = "<font color=blue>发送失败,对方可能不在线!</font>";
                                }
                                else
                                {
                                    PowerTalk.AddChatInfo(MyUserId, ToUserID, SendMsg);
                                }
                            }
                        }
                        else
                        {
                            PowerTalk.AddChatInfo(MyUserId, SendMsg);
                        }
                    }
                  
                    
                    

                    RtStr = "1" + "$" + "<br/><font color=red>您说:</font><font color=blue>" + DateTime.Now.ToString(" HH:mm:ss") + "</font><br/>" + SendMsg + "<br/>";
                    if (OnChatInfoSend != null)//发信息事件
                    {
                        EventArgs ea = new EventArgs();
                       
                        OnChatInfoSend(RtStr,ea);//插入event
                    }
                    break;
                case "2"://传送文件
                    OutTimes = 0;//过期次数执0
                    SplitStr = area;
                    Str = SplitStr.Split('$');
                    TopStr = Str[0] + Str[1] + Str[2];
                    MyUserId = Str[1];
                    ToUserID = Str[2];
                    SendMsg = SplitStr.Substring(TopStr.Length + 3);
                  
                    SendMsg = UpLoadFile(this.OpenFile(SendMsg), SendMsg.Substring(SendMsg.LastIndexOf('\\') + 1) + "_重命名" + DateTime.Now.ToString("_yyyy年MM月dd日HH时mm分ss秒上传"));//取文件名);
                 
                        if (CMode == ChatMode.OneToOne)
                        {
                            PowerTalk.AddChatInfo(MyUserId, ToUserID, SendMsg);
                        }
                        else
                        {
                            PowerTalk.AddChatInfo(MyUserId, SendMsg);
                        }
             
                    RtStr = "1" + "$" + "<font color=red>发送结果:</font><font color=blue>" + DateTime.Now.ToString(" HH:mm:ss") + "</font><br/>" + SendMsg + "<br/>";
                    break;
            }
     
            return RtStr;
        }
        /// <summary>
        /// 强制过期
        /// </summary>
        public void DoOutTime()
        {
            OutTimes = LeaveTimes;
        }

        /// <summary>
        /// 上传文件的路径:WebForm
        /// </summary>
        /// <param name="PathName">服务器路径:直接打地址就可以，不用加mappath</param>
        /// <param name="sm">流</param>
        public string UpLoadFile(Stream sm, string PathName)
        {

            try
            {
                UpLoader sc = new UpLoader();
                sc.SaveStreamToFile(sm, "~/ChatTemp/" + PathName);

                return "<font color='red'>传送成功:点击</font><a href='" + "ChatTemp/" + PathName.Replace("~/", "") + "' target='_blank'>接收</a> ";
            }
            catch (Exception Exp)
            {
                return "<font color='blue'>" + Exp.Message + "</font>";
            }
        }
        EventArgs ea = new EventArgs();
        /// <summary>
        /// ICallbackEventHandler的继承接收方法
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaiseCallbackEvent(string eventArgument)
        {
            area = eventArgument;
            if (OnRefresh != null)
            {                
                OnRefresh(ToUserIdContent, ea);//插入event
            }
        

        }
        /// <summary>
        /// 转换成为MSN表情
        /// </summary>
        /// <param name="MatchStr"></param>
        private string MatchImg(string MatchStr)
        {
            string regstr = @"src\=.+?\.(gif|jpg|png|bmp)";
            Regex myrg = new Regex(regstr);
            Match mt = myrg.Match(MatchStr, 0);
            int i = 0;
            while (mt.Success)
            {
                string Values = mt.Value;
                string Mtch = Values;
                Values = Regex.Replace(Values, "src=", "", RegexOptions.IgnoreCase);
                Values = Regex.Replace(Values, "‘", "", RegexOptions.IgnoreCase);
                Values = Regex.Replace(Values, "\"", "", RegexOptions.IgnoreCase);
                Values = Regex.Replace(Values, "'", "", RegexOptions.IgnoreCase);
                Values = Regex.Replace(Values, "’", "", RegexOptions.IgnoreCase);
              
                Values = Values.Substring(Values.LastIndexOf(@"/") + 1);
              
                    MatchStr = MatchStr.Replace("<IMG " + Mtch + @""">", ToMsnFace(Values));                
     
                mt = mt.NextMatch();
            }
            return MatchStr;
        }
        /// <summary>
        /// 从Image转换为Msn的头像
        /// </summary>
        /// <param name="faceimg">头像图片名称</param>
        /// <returns></returns>
        private string ToMsnFace(string faceimg)
        {
            string MsnFace = "";
            switch (faceimg)
            {

                case "img1.gif":
                    MsnFace = ":)";
                    break;
                case "img2.gif":
                    MsnFace = ":-O";
                    break;
                case "img3.gif":
                    MsnFace = ":P";
                    break;
                case "img4.gif":
                    MsnFace = "(H)";
                    break;
                case "img5.gif":
                    MsnFace = ":@";
                    break;
                case "img6.gif":
                    MsnFace = ":S";
                    break;
                case "img7.gif":
                    MsnFace = ":$";
                    break;
                case "img8.gif":
                    MsnFace = ":'(";
                    break;
                case "img9.gif":
                    MsnFace = ":|";
                    break;
                case "img10.gif":
                    MsnFace = "(A)";
                    break;
                case "img11.gif":
                    MsnFace = "8o|";
                    break;
                case "img12.gif":
                    MsnFace = "8-|";
                    break;
                case "img13.gif":
                    MsnFace = "+o(";
                    break;
                case "img14.gif":
                    MsnFace = "<:o)";
                    break;
                case "img15.gif":
                    MsnFace = "|-)";
                    break;
                case "img16.gif":
                    MsnFace = "*-)";
                    break;
                case "img17.gif":
                    MsnFace = ":-#";
                    break;
                case "img18.gif":
                    MsnFace = ":-*";
                    break;
                case "img19.gif":
                    MsnFace = "^o)";
                    break;
                case "img20.gif":
                    MsnFace = "8-)";
                    break;
                case "img21.gif":
                    MsnFace = "(L)";
                    break;
                case "img22.gif":
                    MsnFace = "(U)";
                    break;
                case "img23.gif":
                    MsnFace = "(M)";
                    break;
                case "img24.gif":
                    MsnFace = "(@)";
                    break;
                case "img25.gif":
                    MsnFace = "(&)";
                    break;
                case "img26.gif":
                    MsnFace = "(sn)";
                    break;
                case "img27.gif":
                    MsnFace = "(bah)";
                    break;
                case "img28.gif":
                    MsnFace = "(S)";
                    break;
                case "img29.gif":
                    MsnFace = "(*)";
                    break;
                case "img30.gif":
                    MsnFace = "(#)";
                    break;
                case "img31.gif":
                    MsnFace = "(R)";
                    break;
                case "img32.gif":
                    MsnFace = "({)";
                    break;
                case "img33.gif":
                    MsnFace = "(})";
                    break;
                case "img34.gif":
                    MsnFace = "(K)";
                    break;
                case "img35.gif":
                    MsnFace = "(F)";
                    break;
                case "img36.gif":
                    MsnFace = "(W)";
                    break;
                case "img37.gif":
                    MsnFace = "(O)";
                    break;
                case "img38.gif":
                    MsnFace = ";)";
                    break;
                case "img39.gif":
                    MsnFace = ":D";
                    break;
                case "img40.gif":
                    MsnFace = ":("; 
                    break;
                default:
                   MsnFace = ":)";
                  break;
              
          
            }
            return MsnFace;
        }

 
        #endregion
        #endregion

    }
   #endregion
}
