<!--
    // ◎◎◎◎◎图片路径。◎◎◎◎◎
    var ImgMnb = "Images/bar1.gif";
    var ImgMnbH = "Images/MainNavBack_Hover.gif";
    var ImgMnbC = "Images/bar.gif";
// 可加入状态保存。
// 设定鼠标移入和移出主菜单的效果
function ShowMainNav(objNav)
{
    var objSubNav = objNav.getElementsByTagName("ul").item(0);
    var objNavLink = objNav.getElementsByTagName("a").item(0);
    if ((objSubNav == null && objNav.style.backgroundImage != "url("+ImgMnbC+")") || (objSubNav != null && objSubNav.style.display != "block"))
    {
        if (objNav.style.backgroundImage == "url("+ImgMnbH+")")
        {
            objNav.style.backgroundRepeat="no-repeat";
            objNav.style.backgroundImage = "url("+ImgMnb+")";
            objNavLink.style.color = "#ffffff";
        }
        else
        {
            objNav.style.backgroundRepeat="no-repeat";
            objNav.style.backgroundImage = "url("+ImgMnbH+")";
            objNavLink.style.color = "#336699";
        } 
    }
}
// 设定鼠标移入和移出子菜单的效果
function ShowSubNav(objNav)
{
    var objNavLink = objNav.getElementsByTagName("a").item(0);
    if (objNavLink.style.textDecoration != "underline") 
    {
        if (this.y == 1)
        {
            objNav.style.backgroundColor = "#85a3c2";
            objNavLink.style.color = "#ffffff";
            this.y = 0;
        }
        else
        {
            objNav.style.backgroundColor = "#ffffff";
            objNavLink.style.color = "#336699";
            this.y = 1;
        } 
    } 
   else
   {
            this.y = 0;
   } 
}

// 设定鼠标点击主导航时的效果
function MainNavClick(objNav)
{
    var objSubNav = objNav.getElementsByTagName("ul").item(0);
    var iLength = window.document.getElementById("UlMainNav").childNodes.length;
    for (i = 0; i < iLength; i++)
    {
        var objMainNavList = window.document.getElementById("UlMainNav").childNodes[i]; 
        if (objMainNavList != null && objMainNavList.nodeName == "LI")
        {
            if (objMainNavList.style.backgroundImage == "url("+ImgMnbC+")")
            {
              ChangeClickState(objMainNavList);
            }
        }
    }
    ChangeClickState(objNav);
}
// 设定鼠标点击主导航时的效果切换
function ChangeClickState(objNav)
{
    var objSubNav = objNav.getElementsByTagName("ul").item(0);
    var objNavLink = objNav.getElementsByTagName("a").item(0);
    if (objNav.style.backgroundImage == "url("+ImgMnbC+")") 
    {
        if (objSubNav != null)
        {　　
        　　
            objSubNav.style.display = "none";
        } 
       objNav.style.backgroundRepeat="no-repeat";
       objNav.style.backgroundImage = "url("+ImgMnb+")"; 
       
      // objNavLink.style.color = "#000000";
    }
    else
    {
        if (objSubNav != null)
        
        {
           objSubNav.style.display = "block";
        }
     objNav.style.backgroundRepeat="no-repeat";
     objNav.style.backgroundImage = "url("+ImgMnbC+")";
    //objNavLink.style.color = "#000000";
    }
}

// 设定鼠标点击子导航时的效果
function SubNavClick(objNav, bState)
{
    var objNavLink = objNav.getElementsByTagName("a").item(0);    
    
    var MainNavs = window.document.getElementById("UlMainNav").childNodes;
    for (i = 0; i < MainNavs.length; i++)
    {
        if(MainNavs[i].nodeName == "LI") 
        {
            var SubNavs = MainNavs[i].childNodes; 
            for (x = 0; x < SubNavs.length; x++)
            {
                var objSubNav = SubNavs[x]; 
                if (objSubNav != null && objSubNav.nodeName == "UL")
                {
                    var objSubNavList = objSubNav.childNodes; 
                    for (y = 0; y < objSubNavList.length; y++)
                    {
                        if (objSubNavList[y] != null && objSubNavList[y].nodeName == "LI")
                        {
                            ChangeSubClickState(objSubNavList[y],  false);
                        }
                    }
                }
            }
        }
    } 
    if (bState == true)
    { 
        ChangeSubClickState(objNav, true);
    } 
}
// 设定鼠标点击子导航时的效果切换
function ChangeSubClickState(objNav, bState)
{
    var objNavLink = objNav.getElementsByTagName("a").item(0);
    if (bState == false) 
    {
        objNav.style.backgroundColor = "#85a3c2";
        objNavLink.style.color = "#ffffff";
        objNavLink.style.textDecoration = "none";
    }
    else
    {
        objNav.style.backgroundColor = "#ffffff";
        objNavLink.style.color = "#336699";
        objNavLink.style.textDecoration = "underline";
    }
}

function MainNavClick1(objNav)
{
    var objSubNav = objNav.getElementsByTagName("ul").item(0);
    var iLength = window.document.getElementById("UlMainNav").childNodes.length;
    for (i = 0; i < iLength; i++)
    {
        var objMainNavList = window.document.getElementById("UlMainNav").childNodes[i]; 
      if (objMainNavList != null && objMainNavList.nodeName == "LI")
        {
            if (objMainNavList.style.backgroundImage == "url("+ImgMnbC+")")
            {
                ChangeClickState(objMainNavList);
            }
        }
    }
   ChangeClickState(objNav);
}
-->