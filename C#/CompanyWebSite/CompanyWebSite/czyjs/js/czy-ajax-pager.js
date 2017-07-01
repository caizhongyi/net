if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}
//************************
//js 翻页导航
//************************
//json.contenter: 
//beforeNumCount:当前导航前显示个数[默认5]
//behindNumCount:当前导航后显示个数[默认5]
//ntotalPage :总页数
//dataPageSize :页大小[默认10]
//fun :更新数据事件
//***************************************
czyjs.UI.AjaxPager=Class.create();
czyjs.UI.AjaxPager.prototype = {

    initialize: function (param) {

        this.firstLabel = "首页";
        this.provLabel = "上一页";
        this.nextLabel = "下一页";
        this.lastLabel = "尾页";
        this.curCss = "cur";
        this.nomalCss = "nomal";
        this.prevCss = "nav-prev";
        this.nextCss = "nav-next";
        this.firstCss = "nav-first";
        this.lastCss = "nav-last";
        this.beforeBtnCss = "left";
        this.beHideBtnCss = "left";

        var json = eval(param);
        this.obj = document.getElementById(json.randerTo) == null ? json.randerTo : document.getElementById(json.randerTo); //容器对像
        this.fun = json.fun == null ? function (index) { } : json.fun; 								//更新数据事件
        this.beforeNumCount = json.beforeNumCount; 		//当前页前显示的导航个数
        this.behindNumCount = json.behindNumCount; 		//当前页后显示的导航个数
        this.pageSize = json.pageSize; 					//当前页大小
        this.navLabelVis = json.navLabelVis; 				//是否显示Label
        this.id = json.id == null ? "czy-ajaxPager" : json.id;
        this.className = json.className == null ? "AjaxPager-Nav" : json.className;

        this.currentPage = 0;
        this.totalPage = 0;
        this.totalCount = 0;
        /*************初化变量***************/

        var tempPage = json.totalCount / this.pageSize;
        //当前页
        if ((tempPage + "").indexOf('.') == -1)
        { this.totalPage = parseInt(tempPage); }
        else
        { this.totalPage = parseInt(tempPage) + 1; }
        //设置可见性
        if (this.totalPage == 1 || this.totalPage == 0) { this.obj.style.display = "none"; }
        if (this.navLabelVis == null) { this.navLabelVis = true; }
        if (json.beforeNumCount == null) { this.beforeNumCount = 5; }
        if (json.behindNumCount == null) { this.behindNumCount = 5; }
        if (json.dataPageSize == null) { this.pageSize = 10; }
        if (json.provLabel != null) { this.provLabel = json.provLabel; }
        if (json.nextLabel != null) { this.nextLabel = json.nextLabel; }
        this.totalCount = this.pageSize * this.totalPage;  			//总页
        //生成HTML导航
        this.GetNav();

    },

    GetNav: function () {

        var title = "<div id=\"" + this.id + "_title\">共<span class=\"nav-font\">";
        title += this.totalPage
        title += "</span>页&nbsp;&nbsp;第<span class=\"nav-font\">"
        title += (this.currentPage + 1);
        title += "</span>页&nbsp;&nbsp;第<span class=\"nav-font\">";
        title += (this.currentPage * this.pageSize + 1);
        title += "-" + (this.currentPage + 1) * this.pageSize;
        title += " </span>页&nbsp;&nbsp;共<span class=\"nav-font\">";
        title += this.totalPage * this.pageSize;
        title += "</span>条</div> ";

        var html = "<div id='" + this.id + "' class='" + this.className + "'>";
        html += "<div class=\"nav-left\">" + title + "</div>"; //" + this.totalPage * pageSize +" of "
        html += "<div id=\"" + this.id + "_content\" >";
        html += "</div>";

        this.obj.innerHTML = html;


        if (!this.navLabelVis) {
            document.getElementById(this.id + "_title").style.display = "none";
        }


        this.nav_pagerObj = document.getElementById(this.id + "_content");
        this.nav_pagerObj.className = "ajax-pager-content";


        this.nav_ulObj = document.createElement("ul");
        this.nav_liObj1 = document.createElement("div");
        this.nav_liObj2 = document.createElement("div");

        this.nav_ulObj.id = this.id + "_AjaxPagerUl";
        this.nav_ulObj.className = "ajax-parer-list";

        this.prevObj = document.createElement("input");
        this.nextObj = document.createElement("input");
        this.firstObj = document.createElement("input");
        this.lastObj = document.createElement("input");
        this.allObj = document.createElement("input");

        this.prevObj.type = "button";
        this.firstObj.type = "button";
        this.lastObj.type = "button";
        this.nextObj.type = "button";

        this.prevObj.style.cursor = "pointer";
        this.firstObj.style.cursor = "pointer";
        this.lastObj.style.cursor = "pointer";
        this.nextObj.style.cursor = "pointer";

        this.prevObj.value = this.provLabel;
        this.nextObj.value = this.nextLabel;
        this.firstObj.value = this.firstLabel;
        this.lastObj.value = this.lastLabel;

        this.prevObj.className = this.prevCss;
        this.nextObj.className = this.nextCss;
        this.firstObj.className = this.firstCss;
        this.lastObj.className = this.lastCss;
        /**********翻页事件************/
        this._First = BindAsEventListener(this, this.First);
        this._Last = BindAsEventListener(this, this.Last);
        this._Previous = BindAsEventListener(this, this.Previous);
        this._Next = BindAsEventListener(this, this.Next);
        this._SetCurrentPage = BindAsEventListener(this, function () { this.SetCurrentPage(); });
        /*************事件添加***************/

        removeEventHandler(this.nav_ulObj, "click", this._SetCurrentPage);
        addEventHandler(this.nav_ulObj, "click", this._SetCurrentPage);
        addEventHandler(this.firstObj, "click", this._First);
        addEventHandler(this.lastObj, "click", this._Last);
        /**********添加效果************/
        /*
        this._NavMouseOver=BindAsEventListener(this,this.NavMouseOver);
        this._NavMouseOut=BindAsEventListener(this,this.NavMouseOut);
		
        addEventHandler(this.nav_ulObj, "mouseover",this._NavMouseOver );
        addEventHandler(this.nav_ulObj, "mouseout",this._NavMouseOut );
        */
        /*******************************/
        this.nav_liObj1.className = this.beforeBtnCss;
        this.nav_liObj2.className = this.beHideBtnCss;
        this.nav_liObj1.appendChild(this.firstObj);
        this.nav_liObj1.appendChild(this.prevObj);
        this.nav_liObj2.appendChild(this.nextObj);
        this.nav_liObj2.appendChild(this.lastObj);

        //********************************************
        //  count = this.totalPage;

        if (this.currentPage != 0) {
            addEventHandler(this.prevObj, "click", this._Previous);
        }
        this.nav_pagerObj.appendChild(this.nav_liObj1); //


        /******************生成导航**************************/
        var navCount = this.beforeNumCount + this.behindNumCount + 1; //navCount 
        var start = 0;
        var count = (this.currentPage - this.beforeNumCount);
        var count1 = (this.totalPage - this.currentPage - this.behindNumCount)

        if (count < 0) {
            start = 0;

        }
        else if (count1 <= 0) {
            start = this.totalPage - navCount;
        }
        else {
            start = this.currentPage - this.beforeNumCount;
        }

        if (this.totalPage < navCount) {
            start = 0;
            count = this.totalPage;
        }
        else {
            count = start + navCount;
        }

        for (var i = start; i < count; i++) {
            var na_li = document.createElement("li");
            na_li.style.listStyleType = "none";
            var a = document.createElement("a");
            a.innerHTML = i + 1;
            a.href = "javascript:void(0)";
            a.style.textDecoration = "none";
            a.index = i;
            a.style.display = "block";
            if (this.currentPage == i) {
                a.className = this.curCss;
            }
            else {
                a.className = this.nomalCss;
            }
            na_li.appendChild(a);
            this.nav_ulObj.appendChild(na_li);

        }
        /********************************************/


        if (this.currentPage != this.totalPage - 1) {
            addEventHandler(this.nextObj, "click", this._Next);
        }
        this.nav_ulObj.appendChild(this.nav_liObj2);
        //********************************************

        /**********元素添加************/
        this.nav_pagerObj.appendChild(this.nav_ulObj);
        this.nav_pagerObj.appendChild(this.nav_liObj2);

        /*****************************/




    },
    /*
    * 第一页
    */
    First: function () {

        this.currentPage = 0;
        this.fun(this.currentPage);
        this.GetNav();

    },
    /*
    * 上一页
    */
    Previous: function () {
      
        if (this.currentPage > 0) {
            this.currentPage = this.currentPage - 1;
        }
        this.fun(this.currentPage);
        this.GetNav();

    },
    /*
    * 下一页
    */
    Next: function () {
      
        if (this.currentPage < this.totalPage - 1) {
            this.currentPage = this.currentPage + 1;
        }
        this.fun(this.currentPage);
        this.GetNav();

    },
    /*
    * 最后一页
    */
    Last: function () {

        this.currentPage = this.totalPage - 1;
        this.fun(this.currentPage);
        this.GetNav();

    },
    /*
    * 选择页
    */
    SetCurrentPage: function () {
     
        var o;
        if (!document.all) {
            o = arguments.callee.caller.arguments[0].target;
        }
        else {
            o = event.srcElement;
        }
        if (o.tagName == "A") {
            this.currentPage = parseInt(o.index);


        }
        this.fun(this.currentPage);
        this.GetNav();

    },
    /*
    * 鼠标移上去效果
    */
    NavMouseOver: function () {
        var e = czyjs.Event.GetEventObj();
        if (e.tagName == "LI") {
            e.className = "nav-mouseOver";
        }
    },
    /*
    * 鼠标移开效果
    */
    NavMouseOut: function () {
        var e = czyjs.Event.GetEventObj();
        if (e.tagName == "LI") {
            e.className = "nav-mouseOut";
        }
    }
}