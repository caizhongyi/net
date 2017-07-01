var ajaxUrl = RootDir+"/library/page/ajax"+ExName;
//检查用户名或邮件是否存在
function CheckUserExsitAjax(vals,flags)
{
    loading();
    jQuery.get(
    ajaxUrl,
    {
        action:"CheckUserExsit",
        flag:flags,
        values:vals,
        param:Math.random()
    }, 
    function(msg)
    {
         loadingclear();
         var rmsg="";
         var uflg="用户名";
         if(flags==1)
         {
             if(msg=="0")
             {
                rmsg="电子邮件("+vals+")可以注册！"
                jQuery("#spanemail1").show();
                jQuery("#spanemail").hide();
                jQuery("#spanemail1").html(rmsg);
                return true;
             }
             else
             {
                rmsg="电子邮件("+vals+")已经存在！"
                jQuery("#spanemail").show();
                jQuery("#spanemail1").hide();
                jQuery("#spanemail").html(rmsg);
                return false;
             }
         }
         else
         {
             if(msg=="0")
             {
                rmsg="用户名("+vals+")可以注册！"
                jQuery("#spanusername1").show();
                jQuery("#spanusername").hide();
                jQuery("#spanusername1").html(rmsg);
                return true;
             }
             else
             {
                rmsg="用户名("+vals+")已经存在！"
                jQuery("#spanusername").show();
                jQuery("#spanusername1").hide();
                jQuery("#spanusername").html(rmsg);
                return false;
             }
         }
    }
);
}
//更改邮件
function modifyemail(uid,email)
{
        if(isEmail(jQuery("#mEmail").val()))
        {
          var W = WinTip("提示：", "电子邮件不正确！");
             setTimeout(function() 
             {
                W.Close();
              }, 2000);
              return;
        }
        if (!confirm("确定要修改电子邮件吗？\n修改后要重新验证才能登录。")) return;
        loading();
        jQuery.get(
          ajaxUrl,
          {
            action:"ModifyEmail",
            uid:uid,
            values:escape(email),
            param:Math.random()
          }, 
          function(msg)
          {
             loadingclear();
             showRightResult(msg);
          }
        );    
}
//更改手机
function modifymobile(uid,mobile)
{     
        loading();
        jQuery.get(
          ajaxUrl,
          {
            action:"ModifyMobile",
            uid:uid,
            values:escape(mobile),
            param:Math.random()
          }, 
          function(msg)
          {
             loadingclear();
             if(msg.indexOf("成功")>-1)
             {
                 jQuery("#bmobiles").html("手机："+mobile+"");
                 jQuery("#hideMobile").css("display","none");
             }
             showmsg(msg);
          }
        );        
}
//删除教育信息
function DeleteEduAjax(eid,uid)
{
        if (!confirm("确定要删除教育信息吗？\n删除后不能恢复。")) return;
        loading();
        jQuery.get(
          ajaxUrl,
          {
            action:"DeleteEduAjax",
            uid:uid,
            eid:eid,
            param:Math.random()
          }, 
          function(msg)
          {
             loadingclear();
             $("#param" + eid + "").remove();
             if(msg.indexOf("succs")>-1)
             {
                 $("#param" + eid + "").remove();
                 showRightResult(msg);
             }
             else
             {
                 showErrorResult(msg);
             }
          }
        );        
}
//删除工作信息
function DeleteCarAjax(cid,uid)
{
        if (!confirm("确定要删除工作信息吗？\n删除后不能恢复。")) return;
        loading();
        jQuery.get(
          ajaxUrl,
          {
            action:"DeleteCarAjax",
            uid:uid,
            cid:cid,
            param:Math.random()
          }, 
          function(msg)
          {
             loadingclear();
             if(msg.indexOf("succs")>-1)
             {
                 $("#param" + cid + "").remove();
                 showRightResult(msg);
             }
             else
             {
                 showErrorResult(msg);
             }
          }
        );        
}

//设置默认头像
function SetHead(hid,uid)
{
    if (!confirm("确定要设置此头像为默认头像吗？")) return;
    loading();
    jQuery.get(
      ajaxUrl,
      {
        action:"SetHeadAjax",
        uid:uid,
        hid:hid,
        param:Math.random()
      }, 
      function(msg)
      {
         loadingclear();if(msg.indexOf("succs")>-1)
         {
             showRightResult(msg);
             setTimeout(function() 
             {
                 window.location.href=window.location.href;
              }, 2000);
         }
         else{showErrorResult(msg);}
      }
    );            
}

//删除头像
function DeleteHead(hid,uid)
{
    if (!confirm("确定要删除此头像吗？\n删除后不可恢复！")) return;
    loading();
    jQuery.get(
      ajaxUrl,
      {
        action:"DeleteHeadAjax",
        uid:uid,
        hid:hid,
        param:Math.random()
      }, 
      function(msg)
      {
         loadingclear();if(msg.indexOf("succs")>-1){$("#param" + hid + "").remove();showRightResult(msg);}else{showErrorResult(msg);}
      }
    );            
}

//删除好友
function delfriend(id,fname,fid,uid)
{
    if (!confirm("断开你和"+fname+"之间的好友关系，你也将不再出现在"+fname+"的好友列表中。\n系统不会发消息通知对方。\n确定要删除好友吗？")) return;
    loading();
    jQuery.get(
      ajaxUrl,
      {
        action:"DeleteFriendAjax",
        uid:uid,
        fid:fid,
        truename:fname,
        param:Math.random()
      }, 
      function(msg)
      {
         loadingclear();if(msg.indexOf("succs")>-1){$("#param" + id + "").remove();showRightResult(msg);}else{showErrorResult(msg);}
      }
    );  
}

//修改，增加好友分类
function AddorModifFriendClass(id,uid,fids,fid)
{
    if(jQuery("#cname").val()=="")
    {
        alert("请填写好友分类组名称！");return;
    }
    jQuery.get(
      ajaxUrl,
      {
        action:"InsertOrModifyFriendClassAjax",
        uid:uid,
        fid:fids,
        cname:jQuery("#cname").val(),
        param:Math.random()
      }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
            showRightResult("操作成功");
            window.location.href="class"+ExName+"?id="+id+"&fid="+fid+"&uid="+uid+"";
         }
         else
         {
             showErrorResult("操作失败");
         }
      }
    );            
}

//删除好友分类
function DeleteFriendClass(fid,uid)
{
    if (!confirm("确定要删除此好友分组吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteFriendClassAjax", fid:fid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param" + fid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
}

//删除来访记录
function DeleteVisit(vid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteVisitAjax", vid:vid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param" + vid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
}

//删除微博
function deletetwitter(tid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteTwitterAjax", tid:tid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + tid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
}

function deletetblog(bid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteBlogAjax", bid:bid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + bid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
}

function deletetblogcomment(cid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteBlogCommentAjax", cid:cid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + cid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
 }
 
 function deletetgoodscomment(cid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteGoodsCommentAjax", cid:cid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + cid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
 }
 
 function deletetnewscomment(cid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteNewsCommentAjax", cid:cid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + cid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
 }

function addsort()
{
        jQuery("#showsortspan").show();
}
function addsortsave(uid)
{
    var svalue= jQuery("#sortname").val();
    if(svalue=="")
    {
        showErrorResult("请填写分类名称");
        document.getElementById("sortname").focus();return;
    }
    else
    {
        jQuery.get(ajaxUrl, { action:"AddBlogClassAjax", sortname:svalue, uid:uid, param:Math.random() }, 
          function(msg)
          {
             if(msg>0)
             {
                var oldSelect = document.getElementsByName("myclassid")[0]; //获取原有的SELECT数据
                var varItem = new Option(svalue,msg);  
                document.getElementById("myclassid").options.add(varItem);
                for(var i=0;i<oldSelect.length;i++)
                {
                    if(i==oldSelect.length-1)
                    {
                        document.getElementById("myclassid").options[i].selected=true;
                    }
                }
                jQuery("#sortname").val("");
                showblogclassdelete(document.getElementById("myclassid"));
             }
             else
             {
                 showErrorResult("添加失败");return;
             }
          }
        );                 
    }
}
function showblogclassdelete(obj)
{
    if(obj.value!=""&&obj.value!="0")
    {
        jQuery("#deleteblogclass").show();
    }
    else
    {
        jQuery("#deleteblogclass").hide();
    }
}
function deleteblogc(uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    var v=jQuery("#myclassid").val();
    var ist=false;
    jQuery.get(ajaxUrl, { action:"DeleteBlogClassAjax", bid:v, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
            var oldSelect = document.getElementsByName("myclassid")[0]; //获取原有的SELECT数据
            for(var i=0;i<oldSelect.length;i++)
            {
                if(ist)
                {
                    break;
                }
                else
                {
                    if(document.getElementById("myclassid").options[i].selected==true)
                    {
                        document.getElementById("myclassid").options.remove(i);
                        ist=true;
                    }
                }
            }
            showblogclassdelete(document.getElementById("myclassid"));
            showRightResult("删除成功");
              return;
         }
         else
         {
              showErrorResult("删除成功");
              return;
         }
      }
    );         
}
function postatt(bid,uid,info)
{
        if(uid==0)
        {
              showErrorResult("请登录后操作。");
        }
        else
        {
            jQuery.get(ajaxUrl, { action:"AttInfoAjax", bid:bid, uid:uid,info:info, param:Math.random() }, 
              function(msg)
              {
                 if(msg>0)
                 {
                    if(info=="user")
                    {
                      showRightResult("关注成功");
                    }
                    else
                    {
                         jQuery("#att_"+bid+"").html(msg);
                     }
                 }
                 else
                 {
                     if(info=="user")
                     {
                        if(msg==-1)
                        {
                          showErrorResult("此用户已经关注过了。");
                        }
                        else if(msg==-2)
                        {
                          showErrorResult("不能自己关注自己。");
                        }
                        else
                        {
                          showErrorResult("关注失败");
                        }
                     }
                     else
                     {
                          showErrorResult("关注失败");
                      }
                 }
              }
            );        
        }
}

function deletenews(nid,uid)
{
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteNewsAjax", nid:nid, uid:uid, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + nid + "").remove();
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
 }
 
 function CheckInfoState(infoid,uid,flag,type)
 {
    if (!confirm("确定要进行此操作吗？")) return;
    jQuery.get(ajaxUrl, { action:"CheckInfoState", infoid:infoid, uid:uid,flag:flag,type:type, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             showRightResult("操作成功");
                setTimeout(function() 
             {
                window.location.href=window.location.href;
              }, 2000);             
       }
         else
         {
             showErrorResult("操作失败");
         }
      }
    );           
  }
  
  function deletestart(type)
  {
    if (!confirm("确定要初始化吗？\n初始化后资讯里的所有信息将清空，此操作不可逆！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteStartAjax", type:type, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             showRightResult("初始化成功");
            setTimeout(function() 
             {
                window.location.href=window.location.href;
              }, 2000);             
         }
         else
         {
             showErrorResult("初始化失败");
         }
      }
    );           
 }
 
 //通用删除
 function deleteAll(infoid,uid,type)
 {
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteALLAjax", infoid:infoid, uid:uid,type:type, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             $("#param_" + infoid + "").remove();
             if(type=="groupmember")
             {
                  setTimeout(function() 
                 {
                    window.location.href=window.location.href;
                  }, 2000);             
             }
             else
             {
                 if(document.getElementById("param1_" + infoid)!=null)
                 {
                     $("#param1_" + infoid + "").remove();
                 }
             }
             showRightResult("删除成功");
         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
 }
 
function SetALLState(infoid,uid,flag,type)
{
    if(flag==1)
    {
         firmstr="确定要锁定吗？";
    }
    else
    {
         firmstr="确定要设置为正常状态吗？";
    }
    if (!confirm(firmstr)) return;
    jQuery.get(ajaxUrl, { action:"SetALLState", infoid:infoid, uid:uid,flag:flag,type:type, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             showRightResult("操作成功");
              setTimeout(function() 
             {
                window.location.href=window.location.href;
              }, 1000);             
         }
         else
         {
             showErrorResult("操作失败");
         }
      }
    );             
}

 function RecAll(infoid,uid,flag,type)
 {
    var firmstr="";
    if(type=="useradmin")
    {
        if(flag==0)
        {
             firmstr="确定取消管理员吗？";
        }
        else
        {
             firmstr="确定设为管理员吗？";
        }
    }
    else if(type=="userVip")
    {
        if(flag==0)
        {
             firmstr="确定取消VIP会员吗？";
        }
        else
        {
             firmstr="确定设为VIP会员吗？";
        }
    }
    else
    {
        if(flag==0)
        {
             firmstr="确定取消推荐此信息吗？";
        }
        else
        {
             firmstr="确定把此信息设为推荐吗？";
        }
    }    
    if (!confirm(firmstr)) return;
    jQuery.get(ajaxUrl, { action:"RecALLAjax", infoid:infoid, uid:uid,type:type,flag:flag, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
            if(type=="useradmin")
            {
                if(flag==0)
                {
                     showRightResult("取消管理员成功");
                }
                else
                {
                     showRightResult("设为管理员成功");
                }
            }
            else if(type=="userVip")
            {
                if(flag==0)
                {
                     showRightResult("取消VIP会员成功");
                }
                else
                {
                     showRightResult("设为VIP会员成功");
                }
            }
            else
            {
                if(flag==0)
                {
                     showRightResult("取消推荐成功");
                }
                else
                {
                     showRightResult("设为推荐成功");
                }
            }
              setTimeout(function() 
             {
                window.location.href=window.location.href;
              }, 1000);             

         }
         else
         {
             showErrorResult("操作失败");
         }
      }
    );                 
 }
 
  function deletetopics(infoid,uid,gid)
 {
    if (!confirm("确定要删除吗？\n删除后不可恢复！")) return;
    jQuery.get(ajaxUrl, { action:"DeleteALLAjax", infoid:infoid, uid:uid,type:"grouptopic", param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             showRightResult("删除成功");
              setTimeout(function() 
             {
                window.location.href=RootDir+"/app/group/list"+ExName+"?gid="+gid;
              }, 2000);             

         }
         else
         {
             showErrorResult("删除失败");
         }
      }
    );                 
 }

function uploadPic(flag)
{
    if(flag==0)
    {
        jQuery("#uploadpic").show();
        jQuery("#uploadpic1").hide();
    }
    else
    {
        jQuery("#uploadpic1").show();
        jQuery("#uploadpic").hide();
    }
}

function poke(fid,truename,uid)
{
    window.location.href=RootDir+"/app/poke?fid="+fid+"&uid="+uid;
}
function mail(fid,uid)
{
    window.location.href=RootDir+"/home/box/mail"+ExName+"?fid="+fid+"&uid="+uid;
}

function JoinGroup(gid,uid)
{
    if(uid==0)
    {
             showErrorResult("登录后才能操作。");return;
    }
    jQuery.get(ajaxUrl, { action:"JoinGroupAjax", gid:gid, uid:uid, param:Math.random() }, 
      function(msg)
      {
      
         if(msg.indexOf("succs")>-1)
         {
             showRightResult("加入成功");
         }
         else if(msg.indexOf("joined")>-1)
         {
             showErrorResult("已经加过了。等待审核。");
         }
         else if(msg.indexOf("check")>-1)
         {
             showRightResult("加入成功。但需要管理员审核");
         }
         else if(msg.indexOf("none")>-1)
         {
             showErrorResult("本群拒绝加入！");
         }
         else if(msg.indexOf("max")>-1)
         {
             showErrorResult("本群人数已经达到上限！");
         }
         else
         {
             showErrorResult("加入失败");
         }
      }
    );                 
}

function OutGroup(gid,uid)
{
    if (!confirm("确定要退出吗？")) return;
    jQuery.get(ajaxUrl, { action:"OutGroupAjax", gid:gid, uid:uid, param:Math.random() }, 
      function(msg)
      {
      
         if(msg.indexOf("succs")>-1)
         {
             showRightResult("已经成功退出成功");
         }
         else if(msg.indexOf("creat")>-1)
         {
             showErrorResult("您是创始人，不能退出。");
         }
         else
         {
             showErrorResult("退出失败");
         }
      }
    );                 
}

function setgrouptop(infoid,uid,flag)
{
    var charSTR="设置置顶";
    switch(flag)
    {
        case 0:
            charSTR="取消置顶"
            break;
    }
    jQuery.get(ajaxUrl, { action:"SetGroupTopAjax", infoid:infoid, uid:uid,flag:flag, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             showRightResult(charSTR+"成功");
             setTimeout(function() 
             {
                window.location.href=location.href;
              }, 2000);             
         }
         else
         {
             showErrorResult(charSTR+"失败");
         }
      }
    );          
}

function setaskbest(userid,aid,uid,mid)
{
    if (!confirm("确定要把此答案设为最佳答案吗？")) return;
        jQuery.get(ajaxUrl, { action:"SetAskBestAjax", userid:userid,infoid:aid, uid:uid,mid:mid, param:Math.random() }, 
      function(msg)
      {
        //0成功，1已经有最佳答案了，2问题已经关闭，3不是自己的问题，4设置失败
        switch(msg)
        {
            case "0":
                 showRightResult("设定最佳答案成功");
                 setTimeout(function() 
                 {
                    window.location.href=location.href;
                  }, 1000);             
                break;
            case "1":
                showErrorResult("已经有最佳答案，不能再设置了");
                break;
            case "2":
                showErrorResult("问题已经关闭");
                break;
            case "3":
                showErrorResult("不是你的问题，不能设置！");
                break;
            default:
                showErrorResult("设置失败！");
                break;
        }
      }
    );          
}

function setgroupbest(infoid,uid,flag)
{
    var charSTR="设置精华";
    switch(flag)
    {
        case 0:
            charSTR="取消精华"
            break;
    }
    jQuery.get(ajaxUrl, { action:"SetGroupBestAjax", infoid:infoid, uid:uid,flag:flag, param:Math.random() }, 
      function(msg)
      {
         if(msg.indexOf("succs")>-1)
         {
             showRightResult(charSTR+"成功");
             setTimeout(function() 
             {
                window.location.href=location.href;
              }, 2000);
         }
         else
         {
             showErrorResult(charSTR+"失败");
         }
      }
    );          
}

function addfriend(fid,fname,uid)
{
    var W="";
    var url=""+RootDir+"/home/friend/addw"+ExName+"?fid="+fid+"&uid=" + uid + "";
    if(uid==0)
    {
         W = WinTip("请求加【"+fname+"】为好友", "请先登陆");
    }
    else
    {
         W = WinTip("请求加【"+fname+"】为好友", "<iframe src=\"\" style=\"width:100%;height:320px;border:0;\" frameborder=\"NO\" border=\"0\" framespacing=\"0\" id=\"addfrieandsdiv\"></iframe>");
     }
     W.ConfirmButton="关闭窗口";
    W.Create();     
    document.getElementsByTagName("iframe")[0].src = url;
}

function sharetofriend(flag,oid,uid,title)
{
        var content="<div style=\"text-align:left;\">标题：<input id=\"sharetitle\" name=\"sharetitle\" value=\""+title+"\"  class=\"f-text\" style=\"width:70%;\"></input></div>";
        content+="<div style=\"text-align:left;\">描述：<textarea id=\"sharecontent\" name=\"sharecontent\" cols=\"20\" rows=\"2\" class=\"f-area\" style=\"width:70%;\"></textarea></div>";
        var  W = WinTip("分享", content);
        W.Width=500;
        W.ConfirmButton="分享给好友";
        if(uid==0)
        {
             W=WinTip("请先登录", "登录后才能操作");
             setTimeout(function() 
             {
                W.Close();
              }, 2000);
        }
        else
        {
            W.Confirm=function()
            {
                var sharetitle=jQuery("#sharetitle").val();
                var sharecontent=jQuery("#sharecontent").val();
                if(sharetitle=="")
                {
                    alert("请填写分享标题");return;
                }
                   jQuery.post(ajaxUrl, { action:"ShareAjax", oid:oid,uid:uid,flag:flag,sharetitle:sharetitle,sharecontent:sharecontent,param:Math.random() }, 
                  function(msg)
                  {
                     var  W1="";
                     if(msg=="1")
                     {
                        W1 = WinTip("分享成功", "<h3 style=\"color:Green;\">分享成功。</h3>");
                         W1.Create();        
                         setTimeout(function() 
                         {
                            W1.Close();
                          }, 2000);
                     }
                     else
                     {
                         W1 = WinTip("分享失败", "<h3 style=\"color:Red;\">分享失败，发生异常</h3>");
                         W1.Create();        
                         return;
                     }
                  }
                );                         
            }
        }
        W.Create();            
}

function search(type,q,v)
{
   var kwd="";
   if(v==undefined)
   {
        kwd=jQuery("#kwd").val();
        if(kwd==""||kwd.length<2)
        {
            showErrorResult("请输入关键字！至少2个字符");return;
        }
   }
   else
   {
        kwd=v;
   }
    switch(type)
    {
        case "user":
            window.location.href=RootDir+"/home/friend/Search"+ExName+"?keys="+kwd;
            break;
        case "blog":
            window.location.href=RootDir+"/app/blog/default"+ExName+"?kwd="+kwd+"&q="+q;
            break;
        case "news":
            window.location.href=RootDir+"/app/news/default"+ExName+"?kwd="+kwd+"&q="+q;
            break;
        case "news1":
            window.location.href=RootDir+"/app/news/index"+ExName+"?kwd="+kwd+"&q="+q;
            break;
        case "album":
            window.location.href=RootDir+"/app/album/default"+ExName+"?kwd="+kwd+"&q="+q;
            break;
        case "group":
            window.location.href=RootDir+"/app/group/default"+ExName+"?kwd="+kwd;
            break;
        case "grouptopic":
            window.location.href=RootDir+"/app/group/list"+ExName+"?gid="+q+"&kwd="+kwd;
            break;
        case "ask":
            window.location.href=RootDir+"/app/ask/default"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "ative":
            window.location.href=RootDir+"/app/ative/default"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "goods":
            window.location.href=RootDir+"/app/shop/list"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "shop":
            window.location.href=RootDir+"/app/shop/default"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "multe":
            window.location.href=RootDir+"/app/shop/multebuy"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "share":
            window.location.href=RootDir+"/app/share/default"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "favorite":
            window.location.href=RootDir+"/app/favorite/default"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "help":
            window.location.href=RootDir+"/help/default"+ExName+"?kwd="+kwd;
            break;
        case "users":
            window.location.href=RootDir+"/system/default"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "charge":
            window.location.href=RootDir+"/system/charge"+ExName+"?q="+q+"&kwd="+kwd;
            break;
        case "newsm":
            window.location.href=RootDir+"/system/news"+ExName+"?kwd="+kwd;
            break;
        case "groupm":
            window.location.href=RootDir+"/system/group"+ExName+"?kwd="+kwd;
            break;
        case "topicm":
            window.location.href=RootDir+"/system/topic"+ExName+"?kwd="+kwd;
            break;
        case "blogm":
            window.location.href=RootDir+"/system/blog"+ExName+"?kwd="+kwd;
            break;
        case "twitterm":
            window.location.href=RootDir+"/system/twitter"+ExName+"?kwd="+kwd;
            break;
        case "photom":
            window.location.href=RootDir+"/system/photo"+ExName+"?kwd="+kwd;
            break;
        case "albumm":
            window.location.href=RootDir+"/system/album"+ExName+"?kwd="+kwd;
            break;
        case "shopm":
            window.location.href=RootDir+"/system/shop"+ExName+"?kwd="+kwd;
            break;
        case "activem":
            window.location.href=RootDir+"/system/active"+ExName+"?kwd="+kwd;
            break;
        case "multem":
            window.location.href=RootDir+"/system/multe"+ExName+"?kwd="+kwd;
            break;
        case "askm":
            window.location.href=RootDir+"/system/ask"+ExName+"?kwd="+kwd;
            break;
        case "votem":
            window.location.href=RootDir+"/system/ask"+ExName+"?kwd="+kwd;
            break;
        case "favm":
            window.location.href=RootDir+"/system/fav"+ExName+"?kwd="+kwd;
            break;
        case "comment_newsm":
            window.location.href=RootDir+"/system/comment"+ExName+"?kwd="+kwd;
            break;
        case "comment_blogm":
            window.location.href=RootDir+"/system/comment_blog"+ExName+"?kwd="+kwd;
            break;
        case "comment_twitterm":
            window.location.href=RootDir+"/system/comment_twitter"+ExName+"?kwd="+kwd;
            break;    
        case "comment_photom":
            window.location.href=RootDir+"/system/comment_photo"+ExName+"?kwd="+kwd;
            break;    
        case "comment_goodsm":
            window.location.href=RootDir+"/system/comment_goods"+ExName+"?kwd="+kwd;
            break;    
        case "comment_shopm":
            window.location.href=RootDir+"/system/comment_shop"+ExName+"?kwd="+kwd;
            break;    
        case "comment_multem":
            window.location.href=RootDir+"/system/comment_multe"+ExName+"?kwd="+kwd;
            break;    
        case "comment_activem":
            window.location.href=RootDir+"/system/comment_active"+ExName+"?kwd="+kwd;
            break;    
     }
}

function replaycomment(cid,bid,uid,truename,type)
{
    var content="<div style=\"text-align:center;\"><textarea id=\"replay_cmt_window\" name=\"replay_cmt_window\" cols=\"20\" rows=\"2\" class=\"f-area\"></textarea>";
    var  W = WinTip("回复"+truename+"的评论", content);
    W.ConfirmButton="回复";
    if(uid==0)
    {
         W=WinTip("请先登录", "登录后才能操作");
         setTimeout(function() 
         {
            W.Close();
          }, 2000);
    }
    else
    {
        W.Confirm=function()
        {
            var cont=jQuery("#replay_cmt_window").val();
            if(cont=="")
            {
                alert("请填写回复内容");return;
            }
               jQuery.post(ajaxUrl, { action:"SendCommentAjax", cid:cid,bid:bid,uid:uid,cont:cont,tp:type,param:Math.random() }, 
              function(msg)
              {
                 var  W1="";
                 if(msg.indexOf("succs")>-1)
                 {
                    W1 = WinTip("状态", "回复成功…… 2秒后刷新本页。");
                     W1.Create();        
                     setTimeout(function() 
                     {
                        window.location.href=location.href;
                        W1.Close();
                      }, 1000);
                 }
                 else
                 {
                     W1 = WinTip("状态", msg);
                     W1.Create();        
                     return;
                 }
              }
            );                         
        }
    }
    W.Create();        
}

function login(urls,iscode)
{
    var content="<div style=\"text-align:center;\">用户：<input id=\"username_window\" name=\"username_window\" type=\"text\" class=\"f-text\" style=\"width:150px;\" /><br />";
    content+="密码：<input id=\"password_window\" name=\"password_window\" type=\"password\" class=\"f-text\" style=\"width:150px;\" />";
    if(iscode==1)
    {
        content+="<br />答案：<img id=\"login_code_window\" src=\""+RootDir+"/library/page/verifycode"+ExName+"\" align=\"absmiddle\" style=\"width:75px;height:30px;\" /><input id=\"vcode_window\" name=\"vcode_window\" type=\"text\" class=\"f-text\" style=\"width:74px;\" /> <a href=\"javascript:;\" onclick=\"document.getElementById('login_code_window').src='"+RootDir+"/library/page/verifycode"+ExName+"?v='+Math.random()+''\">换一个</a>";
    }
    content+="</div>";
    var  W = WinTip("登录", content);
    W.ConfirmButton="登 录";
    W.Confirm=function()
    {
        var username=jQuery("#username_window").val();
        var password=jQuery("#password_window").val();
        if(username==""||password=="")
        {
            alert("用户名和密码必须填写。");return;
        }
        var vcode="";
        if(iscode==1)
        {
            vcode=jQuery("#vcode_window").val();
            if(vcode=="")
            {
                alert("请填写验证答案。");return;
            }
        }
         loading();
         jQuery.post(ajaxUrl, { action:"LoginAjax", username:username,password:password,vcode:vcode,param:Math.random() }, 
          function(msg)
          {
            var  W1 = "";
             if(msg.indexOf("Succeed")>-1)
             {
                 loadingclear();
                 W1 = WinTip("登录成功", "登录成功…… 2秒后将刷新本页。");
                 W1.Create();        
                 setTimeout(function() 
                 {
                    location.href=urls;
                    W1.Close();
                  }, 2000);
             }
             else
             {
                 W1 = WinTip("登录失败", msg);
                 W1.Create();        
                 loadingclear();
                 return;
             }
          }
        );                         
    }
    W.Create();        
}

//编码
function EncodeParam() {
    var s = "";
    for (var i = 0; i < arguments.length; i++) {
        s += arguments[i] + "|";
    }
    if (s != "") {
        s = s.slice(0, -1);
    }
    return s;
}

function outative(aid,uid)
{
        if (!confirm("确定退出吗？")) return;
        jQuery.get(ajaxUrl, { action:"OutAtiveAjax", aid:aid, uid:uid, param:Math.random() }, 
          function(msg)
          {
             //0失败，1参与了但是需要审核，2成功，3已经参与了
             switch(msg)
             {
                 case "0":
                     showErrorResult("退出失败");
                     break;
                 case "1":
                     showRightResult("退出成功");
                     break;
             }
              setTimeout(function() 
             {
                window.location.href=window.location.href;
              }, 1000);
          }
        );         
 }

function baoming(aid,uid,flag)
{
        if(uid==0)
        {
             showErrorResult("需要登录后才能操作");
            return;
        }
        if(flag==2)
        {
            if (!confirm("确定要参加此活动吗？")) return;
        }
        jQuery.get(ajaxUrl, { action:"JoinAtiveAjax", aid:aid, uid:uid,flag:flag, param:Math.random() }, 
          function(msg)
          {
             //0失败，1参与了但是需要审核，2成功，3已经参与了
             switch(msg)
             {
                 case "0":
                     showErrorResult("操作失败");
                     break;
                 case "1":
                     showRightResult("已参加活动，但需要管理员审核");
                     break;
                 case "2":
                     if(flag==1)
                    {
                         showRightResult("关注成功。");
                     }
                    else
                    {
                         showRightResult("报名成功！");
                     }
                     break;
                 case "3":
                    if(flag==1)
                    {
                         showErrorResult("已经关注过了。");
                     }
                     else
                     {
                         showErrorResult("已经报名了，无需再报名。");
                     }
                     break;
             }
              setTimeout(function() 
             {
                window.location.href=window.location.href;
              }, 1000);

          }
        );                 
}

function checks(userid,mid,aid,flag)
{
        if (!confirm("您确定要进行此操作吗？")) return;
        jQuery.get(ajaxUrl, { action:"CheckAtiveMemberAjax", userid:userid,mid:mid,aid:aid, flag:flag, param:Math.random() }, 
          function(msg)
          {
             //0失败，1成功，2人数达到上限
             switch(msg)
             {
                 case "0":
                     showErrorResult("操作失败");
                     break;
                 case "1":
                     showRightResult("操作成功");
                      $("#param_" + mid + "").remove();
                     break;
                 case "2":
                     showRightResult("人数达到上限，不能审核！");
                     break;
                 default:
                     showRightResult("发生错误，操作失败");
                     break;
             }
          }
        );         
 }
 
function ATTUser(aid,uid)
{
        if(aid==uid)
        {
             showErrorResult("不能自己关注自己");return;
        }
        jQuery.get(ajaxUrl, { action:"ATTUserAjax", uid:uid,aid:aid, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                     showRightResult("关注成功");
                     break;
                 case "-1":
                     showErrorResult("已经关注过了。");
                     break;
                 default:
                     showErrorResult("发生错误，操作失败");
                     break;
             }
          }
        );         
 }
 
 function postorder(ordernumber,goodsid,orderid,uid)
 {
        if (!confirm("确定要发货吗？")) return;
        jQuery.get(ajaxUrl, { action:"PostOrderAjax", ordernumber:ordernumber,goodsid:goodsid, orderid:orderid,uid:uid, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                     showRightResult("操作成功");
                     setTimeout(function() 
                     {
                        window.location.href=window.location.href;
                      }, 1000);
                     break;
                 default:
                     showErrorResult("操作失败");
                     break;
             }
          }
        );          
}

function reviceorder(orderid,uid)
{
        if (!confirm("确定要确认收货吗？")) return;
        jQuery.get(ajaxUrl, { action:"ReviceOrderAjax", orderid:orderid,uid:uid, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                     showRightResult("操作成功");
                     setTimeout(function() 
                     {
                        window.location.href=window.location.href;
                      }, 1000);
                     break;
                 default:
                     showErrorResult("操作失败");
                     break;
             }
          }
        );          
}

function joinapp(appid,uid,flag)
{
    var confimstr="";
    var rstr="";
    if(flag==1)
    {
        confimstr="确定要安装此应用程序吗？"
        rstr="安装应用程序"
    }
    else
    {
        confimstr="确定要卸载此应用程序吗？"
        rstr="卸载应用程序"
    }
       if (!confirm(confimstr)) return;
        jQuery.get(ajaxUrl, { action:"JoinAppAjax", appid:appid,uid:uid,flag:flag, param:Math.random() }, 
          function(msg)
          {
             if(msg.indexOf("succs")>-1)
             {
                     showRightResult(rstr+"成功");
                     setTimeout(function() 
                     {
                        window.location.href=window.location.href;
                      }, 1000);
             }
             else
             {
                     showErrorResult(msg);
             }
          }
        );         
}

function setgroupadmin(gid,userid,uid,flag)
{
        if(flag==1)
        {
            if (!confirm("确定要设置他为管理员吗？")) return;
        }
        else
        {
            if (!confirm("确定要取消管理员吗？")) return;
        }
        jQuery.get(ajaxUrl, { action:"SetAdminAjax", gid:gid,userid:userid,uid:uid,flag:flag, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                     showRightResult("操作成功");
                     setTimeout(function() 
                     {
                        window.location.href=window.location.href;
                      }, 1000);
                     break;
                 default:
                     showErrorResult("操作失败");
                     break;
             }
          }
        );          
}

function Checklight(gid,uid,flag)
{
     jQuery.get(ajaxUrl, { action:"SetLightAjax", gid:gid,uid:uid,flag:flag, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                     showRightResult("操作成功");
                     setTimeout(function() 
                     {
                        window.location.href=window.location.href;
                      }, 1000);
                     break;
                 default:
                     showErrorResult("操作失败");
                     break;
             }
          }
        );     
}

function checkfriend(id,fid,uid,truename,flag)
{
        if(flag==1)
        {
            if (!confirm("要拒绝加他为好友？")) return;
        }
        jQuery.get(ajaxUrl, { action:"CheckFriendAjax", fid:fid,uid:uid,flag:flag, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                    if(flag==0)
                    {
                         document.getElementById("param_"+id).className="libackright";
                         document.getElementById("param_"+id).innerHTML="成功添加<a href=\""+RootDir+"/user?uid="+fid+"\">"+truename+"</a>为好友。你现在可以：<a href=\""+RootDir+"/app/poke?uid="+fid+"\">招呼</a>，<a href=\""+RootDir+"/app/gift?uid="+fid+"\">送礼物</a>，<a href=\""+RootDir+"/home/box/mail"+ExName+"?fid="+fid+"\">短消息</a>，<a href=\""+RootDir+"/user?uid="+fid+"\">查看他的空间</a>"
                    }
                    else
                    {
                         document.getElementById("param_"+id).className="libackerror";
                         document.getElementById("param_"+id).innerHTML="你拒绝了<a href=\""+RootDir+"/user?uid="+fid+"\">"+truename+"</a>的请求"
                     }
                     break;
                 default:
                     showErrorResult("操作失败");
                      break;
             }
          }
        );   
 }
 
 function checkgroupinvite(id,userid,gid,uid,groupname,flag)
 {
        if(flag==1)
        {
            if (!confirm("要拒绝群组邀请吗？")) return;
        }
        jQuery.get(ajaxUrl, { action:"CheckGroupAjax", userid:userid,gid:gid,uid:uid,flag:flag, param:Math.random() }, 
          function(msg)
          {
             switch(msg)
             {
                 case "1":
                    if(flag==0)
                    {
                         document.getElementById("param_"+id).className="libackright";
                         document.getElementById("param_"+id).innerHTML="成功加入了群组<a href=\""+RootDir+"/app/group/group"+ExName+"?gid="+gid+"\">"+groupname+"</a>，进入群组<a href=\""+RootDir+"/app/group/group"+ExName+"?gid="+gid+"\">"+groupname+"</a>"
                    }
                    else
                    {
                         document.getElementById("param_"+id).className="libackerror";
                         document.getElementById("param_"+id).innerHTML="你拒绝了加入群组<a href=\""+RootDir+"/app/group/group"+ExName+"?gid="+gid+"\">"+groupname+"</a>"
                     }
                     break;
                 case "-1":
                      document.getElementById("param_"+id).className="libackerror";
                      document.getElementById("param_"+id).innerHTML="群组<a href=\""+RootDir+"/app/group/group"+ExName+"?gid="+gid+"\">"+groupname+"</a>已经加入过了！";
                      break;
                 default:
                     showErrorResult("操作失败");
                     break;
             }
          }
        );    
 
 }

function checkgroup(gid,userid,uid,flag)
{
    if(flag==1)
    {
        if (!confirm("要拒绝他加为群成员？")) return;
    }
    jQuery.get(ajaxUrl, { action:"CheckGroupMemberAjax", gid:gid,userid:userid,uid:uid,flag:flag, param:Math.random() }, 
    function(msg)
    {
        switch(msg)
        {
             case "1":
                if(flag==0)
                {
                     showRightResult("通过了会员的群组申请。");
                 }
                 else
                 {
                     showRightResult("拒绝加入成功！");
                 }
                 setTimeout(function() 
                 {
                    window.location.href=window.location.href;
                  }, 1000);
                 break;
             case "-1":
                 showErrorResult("您不是管理员，没有权限");
                 break;
             default:
                 showErrorResult("操作失败");
                 break;
        }
    }
   );          
}
